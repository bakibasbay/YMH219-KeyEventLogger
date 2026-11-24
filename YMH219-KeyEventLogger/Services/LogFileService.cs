using System.Text;
using KeyEventLogger.Models;

namespace KeyEventLogger.Services;

/// <summary>
/// Kayıtları yerel bir .txt dosyasına yazar.
/// Dosya işlemleri form kodundan ayrı tutulur (tek sorumluluk ilkesi).
/// Hata olursa istisna fırlatır; kullanıcıya mesaj göstermek formun işidir.
/// </summary>
public class LogFileService
{
    /// <summary>
    /// Kayıt dosyası için tarih-saatli varsayılan bir ad önerir.
    /// Örn. keylog_20261124_153045.txt
    /// </summary>
    public string SuggestFileName()
    {
        return $"keylog_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
    }

    /// <summary>
    /// Kayıtları verilen yola UTF-8 metin dosyası olarak kaydeder.
    /// Dosya varsa üzerine yazılır.
    /// </summary>
    /// <exception cref="ArgumentException">Yol boşsa.</exception>
    /// <exception cref="UnauthorizedAccessException">Klasöre yazma izni yoksa.</exception>
    /// <exception cref="IOException">Dosya başka programda açıksa veya disk hatası olursa.</exception>
    public void SaveToFile(string filePath, IReadOnlyList<KeyLogEntry> entries)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("Dosya yolu boş olamaz.", nameof(filePath));

        // using: blok bitince dosya otomatik kapatılır (hata olsa bile)
        using var writer = new StreamWriter(filePath, append: false, Encoding.UTF8);

        // Dosya başlığı
        writer.WriteLine("YMH219 Key Event Logger - Kayıt Dosyası");
        writer.WriteLine($"Oluşturulma zamanı : {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
        writer.WriteLine($"Toplam kayıt       : {entries.Count}");
        writer.WriteLine(new string('-', 40));

        // Her kayıt bir satır: "HH:mm:ss.fff – KeyDown – A"
        foreach (var entry in entries)
        {
            writer.WriteLine(entry.ToString());
        }
    }
}
