namespace KeyEventLogger.Models;

/// <summary>
/// Tek bir klavye olayını temsil eden kayıt (hangi tuş, ne zaman, hangi olay).
/// Nesne oluşturulduktan sonra değiştirilemez (salt okunur özellikler).
/// </summary>
public class KeyLogEntry
{
    /// <summary>Olayın gerçekleştiği tuş.</summary>
    public Keys Key { get; }

    /// <summary>Olayın gerçekleştiği tarih ve saat.</summary>
    public DateTime Timestamp { get; }

    /// <summary>Olay türü (KeyDown / KeyUp).</summary>
    public KeyEventType EventType { get; }

    public KeyLogEntry(Keys key, DateTime timestamp, KeyEventType eventType)
    {
        Key = key;
        Timestamp = timestamp;
        EventType = eventType;
    }

    /// <summary>
    /// Tuşun kullanıcıya gösterilecek okunaklı adı.
    /// Örn. Keys.D1 yerine "1", Keys.Return yerine "Enter".
    /// </summary>
    public string KeyName => GetDisplayName(Key);

    /// <summary>
    /// Kaydı "HH:mm:ss.fff – KeyDown – A" biçiminde metne çevirir.
    /// ListBox ve .txt dosyası bu metni kullanır.
    /// </summary>
    public override string ToString()
    {
        return $"{Timestamp:HH:mm:ss.fff} – {EventType} – {KeyName}";
    }

    /// <summary>
    /// Keys değerini daha anlaşılır bir ada dönüştürür.
    /// </summary>
    public static string GetDisplayName(Keys key)
    {
        // Üst sıradaki rakam tuşları: D0..D9 -> "0".."9"
        if (key >= Keys.D0 && key <= Keys.D9)
            return ((int)(key - Keys.D0)).ToString();

        // Sayısal tuş takımı: NumPad0..NumPad9 -> "Num 0".."Num 9"
        if (key >= Keys.NumPad0 && key <= Keys.NumPad9)
            return "Num " + (int)(key - Keys.NumPad0);

        // Sık karşılaşılan bazı tuşlar için Türkçe/kısa adlar
        switch (key)
        {
            case Keys.Return: return "Enter";
            case Keys.Space: return "Boşluk";
            case Keys.Back: return "Backspace";
            case Keys.ShiftKey: return "Shift";
            case Keys.ControlKey: return "Ctrl";
            case Keys.Menu: return "Alt";
            case Keys.Capital: return "CapsLock";
            case Keys.Escape: return "Esc";
            default: return key.ToString();
        }
    }
}
