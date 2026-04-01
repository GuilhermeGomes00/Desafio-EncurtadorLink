using NanoidDotNet;
using UrlShortener.Application.Interfaces;
using UrlShortener.Domain.Interfaces;

namespace UrlShortener.Application.Services;

public class Encurtador(IUrlRepository repository) : IEncurtador
{
    public async Task<string> EncurtadorLink()
    {
        string id;
        do
        {
            id = Nanoid.Generate(Nanoid.Alphabets.LettersAndDigits, size: 6);
        } while (await repository.GetByCodeAsync(id) != null);
        
        return id;
    }
}