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

    public Form1()
    {
        InitializeComponent();

        // KeyLogger'daki olaylara abone ol: yeni kayıt ve durum değişikliği
        _logger.EntryAdded += Logger_EntryAdded;
        _logger.StateChanged += Logger_StateChanged;

        // Uygulama kayıt KAPALI olarak açılır; kullanıcı Başlat'a basmalıdır
        UpdateUi();

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
            lstLog.Items.Clear();
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
        UpdateUi();
    }

    private void Logger_StateChanged(object? sender, EventArgs e)
    {
        UpdateUi();
    }

    /// <summary>
    /// Butonları kayıt durumuna göre aktif/pasif yapar.
    /// </summary>
    private void UpdateUi()
    {
        bool recording = _logger.IsRecording;
        bool hasEntries = _logger.Count > 0;

        btnStart.Enabled = !recording;   // Kayıt açıkken tekrar başlatılamaz
        btnStop.Enabled = recording;     // Kayıt kapalıyken durdurulamaz
        btnClear.Enabled = hasEntries;   // Silinecek kayıt yoksa pasif
    }

    // Butona tıklandıktan sonra odak butonda kalırsa Boşluk/Enter butonu tekrar tetikler.
    // Bunu önlemek için odağı listeye geri veriyoruz.
    private void FocusLog()
    {
        lstLog.Focus();
    }

    #endregion
}
