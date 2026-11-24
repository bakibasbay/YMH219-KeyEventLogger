using KeyEventLogger.Models;

namespace KeyEventLogger.Services;

/// <summary>
/// Kayıtlardan istatistik üretir: toplam tuş basışı, toplam olay ve en sık kullanılan tuşlar.
/// Her yeni kayıtta sayaçlar güncellenir; listeyi baştan taramaya gerek kalmaz.
/// </summary>
public class KeyStatistics
{
    // Her tuşun kaç kez basıldığını tutan sözlük (anahtar: tuş, değer: adet)
    private readonly Dictionary<Keys, int> _pressCounts = new();

    /// <summary>Toplam tuş basışı (yalnızca KeyDown olayları sayılır).</summary>
    public int TotalKeyPresses { get; private set; }

    /// <summary>Toplam olay sayısı (KeyDown + KeyUp).</summary>
    public int TotalEvents { get; private set; }

    /// <summary>Farklı tuş sayısı.</summary>
    public int DistinctKeyCount => _pressCounts.Count;

    /// <summary>Yeni bir kaydı istatistiğe ekler.</summary>
    public void Add(KeyLogEntry entry)
    {
        TotalEvents++;

        // Tuş basışı olarak sadece KeyDown'ı sayıyoruz; KeyUp aynı tuşun bırakılmasıdır
        if (entry.EventType != KeyEventType.KeyDown) return;

        TotalKeyPresses++;

        if (_pressCounts.ContainsKey(entry.Key))
            _pressCounts[entry.Key]++;
        else
            _pressCounts[entry.Key] = 1;
    }

    /// <summary>Tüm sayaçları sıfırlar.</summary>
    public void Clear()
    {
        _pressCounts.Clear();
        TotalKeyPresses = 0;
        TotalEvents = 0;
    }

    /// <summary>
    /// En sık basılan tuşları çoktan aza sıralı döndürür (varsayılan: ilk 5).
    /// Eşit sayıda basılan tuşlar ada göre sıralanır.
    /// </summary>
    public List<(string KeyName, int Count)> GetTopKeys(int count = 5)
    {
        return _pressCounts
            .OrderByDescending(pair => pair.Value)
            .ThenBy(pair => KeyLogEntry.GetDisplayName(pair.Key))
            .Take(count)
            .Select(pair => (KeyLogEntry.GetDisplayName(pair.Key), pair.Value))
            .ToList();
    }
}
