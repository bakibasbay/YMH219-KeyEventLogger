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

        // KeyLogger yeni kayıt eklediğinde listeye yazdır
        _logger.EntryAdded += Logger_EntryAdded;

        // Geçici: Başlat/Durdur butonları eklenene kadar kayıt açık başlar
        _logger.Start();

        // Açılışta odak listede olsun
        ActiveControl = lstLog;
    }

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

    // Yeni kaydı listeye ekler ve listeyi en alta kaydırır
    private void Logger_EntryAdded(object? sender, KeyLogEntry entry)
    {
        lstLog.Items.Add(entry.ToString());
        lstLog.TopIndex = lstLog.Items.Count - 1;
    }
}
