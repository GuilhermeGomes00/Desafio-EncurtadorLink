namespace UrlShortener.Domain.Entity;

public class UrlItem
{
    public int Id { get; init; }
    
    public string LongUrl { get; private set; }
    
    public string ShortCode { get; private set; }
    
    public DateOnly CreatedAt { get; init; }
    
    public int ClickCount { get; private set; }

    public UrlItem(string longUrl, string shortCode)
    {
        if (!UrlValida(longUrl))
            throw new ArgumentException("Url em formato inválido");
        
        LongUrl = longUrl;
        ShortCode = shortCode;
        CreatedAt = DateOnly.FromDateTime(DateTime.Now);
        ClickCount = 0;
    }

    public void IncrementadorClick()
    {
        ClickCount++;
    }

    public bool UrlValida(string url)
    {
        bool FormatoValido = Uri.TryCreate(url, UriKind.Absolute, out Uri ResultadoUri);
        bool CorpoValido = ResultadoUri != null &&
                           (ResultadoUri.Scheme == Uri.UriSchemeHttp || ResultadoUri.Scheme == Uri.UriSchemeHttps);
        
        return FormatoValido && CorpoValido;
    }
    
    
}