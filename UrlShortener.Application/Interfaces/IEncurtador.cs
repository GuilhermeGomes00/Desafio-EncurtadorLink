namespace UrlShortener.Application.Interfaces;

public interface IEncurtador
{
    Task<string> EncurtadorLink();
}