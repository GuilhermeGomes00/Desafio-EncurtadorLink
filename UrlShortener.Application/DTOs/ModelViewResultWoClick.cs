namespace UrlShortener.Application.DTOs;

public record ModelViewResponseWoClick(int Id, string LongUrl, string ShortUrl, DateOnly date);
public record ModelViewResponse(int Id, string LongUrl, string ShortUrl, DateOnly date, int click);
public record ModelViewRedirectResponse(string Url);