using KeyEventLogger.Models;
using KeyEventLogger.Services;

namespace KeyEventLogger;

/// <summary>
/// Uygulamanın ana penceresi. Sadece arayüz işlerini yapar;
/// kayıt mantığı KeyLogger sınıfındadır.
/// </summary>
public partial class Form1 : Form
{
    // Kayıt durumunu ve listeyi yöneten nesne
    private readonly KeyLogger _logger = new();

    // Kayıtları .txt dosyasına yazan servis
    private readonly LogFileService _fileService = new();

    // Toplam tuş ve en sık 5 tuş istatistiği
    private readonly KeyStatistics _statistics = new();

    public Form1()
    {
        InitializeComponent();

        // KeyLogger'daki olaylara abone ol: yeni kayıt ve durum değişikliği
        _logger.EntryAdded += Logger_EntryAdded;
        _logger.StateChanged += Logger_StateChanged;

        // Uygulama kayıt KAPALI olarak açılır; kullanıcı Başlat'a basmalıdır
        UpdateUi();
        UpdateStatistics();

        // Açılışta odak listede olsun
        ActiveControl = lstLog;
    }

    #region Tuş olayları

    // KeyPreview = true olduğu için form, odaktaki kontrolden önce tuş olaylarını alır.
    // Bu olaylar yalnızca BU pencere aktifken gelir (global hook kullanılmaz).
    private void Form1_KeyDown(object? sender, KeyEventArgs e)
    {
        _logger.Log(e.KeyCode, KeyEventType.KeyDown);
    }

    private void Form1_KeyUp(object? sender, KeyEventArgs e)
    {
        _logger.Log(e.KeyCode, KeyEventType.KeyUp);
    }

    // Tab tuşu normalde odak değiştirmek için kullanılır ve KeyDown oluşmaz.
    // Burada Tab'ı "giriş tuşu" sayarak onun da kaydedilmesini sağlıyoruz.
    private void lstLog_PreviewKeyDown(object? sender, PreviewKeyDownEventArgs e)
    {
        if (e.KeyCode == Keys.Tab)
            e.IsInputKey = true;
    }

    #endregion

    #region Buton olayları

    private void btnStart_Click(object? sender, EventArgs e)
    {
        _logger.Start();
        FocusLog();
    }

    private void btnStop_Click(object? sender, EventArgs e)
    {
        _logger.Stop();
        FocusLog();
    }

    private void btnClear_Click(object? sender, EventArgs e)
    {
        // Yanlışlıkla silmeyi önlemek için onay iste
        var answer = MessageBox.Show(
            "Ekrandaki tüm kayıtlar silinecek. Emin misiniz?",
            "Temizle", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (answer == DialogResult.Yes)
        {
            _logger.Clear();
            _statistics.Clear();
            lstLog.Items.Clear();
            UpdateStatistics();
        }
        FocusLog();
    }

    private void btnSave_Click(object? sender, EventArgs e)
    {
        // Kullanıcıya dosya adı ve konumu seçtir
        using var dialog = new SaveFileDialog
        {
            Title = "Kayıtları Kaydet",
            Filter = "Metin dosyası (*.txt)|*.txt",
            DefaultExt = "txt",
            AddExtension = true,
            FileName = _fileService.SuggestFileName()
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            FocusLog();
            return; // Kullanıcı vazgeçti
        }

        // Dosya işlemleri hata verebilir; uygulama çökmesin diye try/catch kullanıyoruz
        try
        {
            _fileService.SaveToFile(dialog.FileName, _logger.Entries);

            MessageBox.Show(
                $"{_logger.Count} kayıt başarıyla kaydedildi.\n\n{dialog.FileName}",
                "Kaydedildi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (UnauthorizedAccessException)
        {
            ShowError("Seçilen konuma yazma izniniz yok. Lütfen başka bir klasör seçin.");
        }
        catch (IOException ex)
        {
            ShowError("Dosya yazılamadı. Dosya başka bir programda açık olabilir.\n\nAyrıntı: " + ex.Message);
        }
        catch (Exception ex)
        {
            ShowError("Beklenmeyen bir hata oluştu.\n\nAyrıntı: " + ex.Message);
        }

        FocusLog();
    }

    #endregion

    #region KeyLogger olayları ve arayüz güncelleme

    // Yeni kaydı listeye ekler ve listeyi en alta kaydırır
    private void Logger_EntryAdded(object? sender, KeyLogEntry entry)
    {
        lstLog.Items.Add(entry.ToString());
        lstLog.TopIndex = lstLog.Items.Count - 1;

        _statistics.Add(entry);
        UpdateStatistics();
        UpdateUi();
    }

    /// <summary>
    /// İstatistik panelindeki etiketleri ve "en sık 5 tuş" listesini yeniler.
    /// </summary>
    private void UpdateStatistics()
    {
        lblTotalKeys.Text = $"Toplam tuş basışı: {_statistics.TotalKeyPresses}";
        lblTotalEvents.Text = $"Toplam olay (Down + Up): {_statistics.TotalEvents}";
        lblDistinctKeys.Text = $"Farklı tuş sayısı: {_statistics.DistinctKeyCount}";

        lstTopKeys.BeginUpdate();
        lstTopKeys.Items.Clear();

        var topKeys = _statistics.GetTopKeys(5);
        if (topKeys.Count == 0)
        {
            lstTopKeys.Items.Add("(henüz kayıt yok)");
        }
        else
        {
            int rank = 1;
            foreach (var (keyName, count) in topKeys)
            {
                // Örn. "1. A          12 kez"
                lstTopKeys.Items.Add($"{rank}. {keyName,-12} {count,4} kez");
                rank++;
            }
        }

        lstTopKeys.EndUpdate();
    }

    private void Logger_StateChanged(object? sender, EventArgs e)
    {
        UpdateUi();
    }

    /// <summary>
    /// Durum göstergesini ve butonları kayıt durumuna göre günceller.
    /// </summary>
    private void UpdateUi()
    {
        bool recording = _logger.IsRecording;
        bool hasEntries = _logger.Count > 0;

        // Durum göstergesi: açıkken yeşil, kapalıyken kırmızı
        if (recording)
        {
            lblStatus.Text = "● KAYIT AÇIK";
            lblStatus.BackColor = Color.SeaGreen;
        }
        else
        {
            lblStatus.Text = "● KAYIT KAPALI";
            lblStatus.BackColor = Color.Firebrick;
        }

        // Pencere başlığında da durum ve kayıt sayısı görünsün
        Text = $"YMH219 - Key Event Logger [{(recording ? "Kayıt açık" : "Kayıt kapalı")}] - {_logger.Count} kayıt";

        btnStart.Enabled = !recording;   // Kayıt açıkken tekrar başlatılamaz
        btnStop.Enabled = recording;     // Kayıt kapalıyken durdurulamaz
        btnClear.Enabled = hasEntries;   // Silinecek kayıt yoksa pasif
        btnSave.Enabled = hasEntries;    // Kaydedilecek kayıt yoksa pasif
    }

    // Hata mesajlarını tek bir yerden, aynı biçimde gösterir
    private void ShowError(string message)
    {
        MessageBox.Show(message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    // Butona tıklandıktan sonra odak butonda kalırsa Boşluk/Enter butonu tekrar tetikler.
    // Bunu önlemek için odağı listeye geri veriyoruz.
    private void FocusLog()
    {
        lstLog.Focus();
    }

    #endregion
}
