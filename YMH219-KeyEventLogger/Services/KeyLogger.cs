using KeyEventLogger.Models;

namespace KeyEventLogger.Services;

/// <summary>
/// Kayıt durumunu (açık/kapalı) ve yakalanan kayıtların listesini yönetir.
/// Form sadece bu sınıfın metotlarını çağırır; liste ve durum burada saklanır (kapsülleme).
/// </summary>
public class KeyLogger
{
    // Kayıtlar dışarıdan doğrudan değiştirilemesin diye private tutulur.
    private readonly List<KeyLogEntry> _entries = new();

    /// <summary>Kayıt şu anda açık mı?</summary>
    public bool IsRecording { get; private set; }

    /// <summary>Kayıtların salt okunur görünümü.</summary>
    public IReadOnlyList<KeyLogEntry> Entries => _entries;

    /// <summary>Toplam kayıt sayısı.</summary>
    public int Count => _entries.Count;

    /// <summary>Yeni bir kayıt eklendiğinde tetiklenir.</summary>
    public event EventHandler<KeyLogEntry>? EntryAdded;

    /// <summary>Kayıt durumu değiştiğinde veya liste temizlendiğinde tetiklenir.</summary>
    public event EventHandler? StateChanged;

    /// <summary>Kaydı başlatır.</summary>
    public void Start()
    {
        if (IsRecording) return;
        IsRecording = true;
        StateChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>Kaydı durdurur; mevcut kayıtlar silinmez.</summary>
    public void Stop()
    {
        if (!IsRecording) return;
        IsRecording = false;
        StateChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>Tüm kayıtları siler.</summary>
    public void Clear()
    {
        _entries.Clear();
        StateChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Bir klavye olayını kaydeder. Kayıt kapalıysa hiçbir şey yapmaz ve null döner.
    /// </summary>
    public KeyLogEntry? Log(Keys key, KeyEventType eventType)
    {
        if (!IsRecording) return null;

        var entry = new KeyLogEntry(key, DateTime.Now, eventType);
        _entries.Add(entry);
        EntryAdded?.Invoke(this, entry);
        return entry;
    }
}
