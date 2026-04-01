using UrlShortener.Application.DTOs;
using UrlShortener.Application.Interfaces;
using UrlShortener.Domain.Entity;
using UrlShortener.Domain.Interfaces;

namespace UrlShortener.Application.Services;

public class UrlService(IUrlRepository repository, IEncurtador encurtador) : IUrlServices
{
    public async Task<IEnumerable<ModelViewResponseWoClick>> GetAllAsync(int? page)
    {
        var Response = await repository.GetAllAsync(page);

        var listResponse = Response.Select(R => new ModelViewResponseWoClick(
            R.Id,
            R.LongUrl,
            R.ShortCode,
            R.CreatedAt
        ));
        
        return listResponse;
    }

    public async Task<ModelViewResponse> Status(string code)
    {
        var response = await repository.GetByCodeAsync(code);
        if (response == null)
            return null;
        
        return new ModelViewResponse(
            response.Id,
            response.LongUrl,
            response.ShortCode,
            response.CreatedAt,
            response.ClickCount
        );

    }

    public async Task<ModelViewRedirectResponse> Redirect(string code)
    {
        var response = await  repository.GetByCodeAsync(code);
        if (response == null)
            return null;
        
        response.IncrementadorClick();
        await repository.UpdateAsync(response);
        
        return new ModelViewRedirectResponse(response.LongUrl);
    }

    public async Task<ModelViewResponseWoClick> Encurtar(string Url)
    {
        var validaUrl = await repository.GetLongUrlAsync(Url);

        if (validaUrl != null)
        {
            return new ModelViewResponseWoClick(validaUrl.Id, validaUrl.LongUrl, validaUrl.ShortCode, validaUrl.CreatedAt);
        }
        string shortCode = await encurtador.EncurtadorLink();
        var url = new UrlItem(Url, shortCode);
        
        await repository.AddAsync(url);
        return new ModelViewResponseWoClick(url.Id, url.LongUrl, url.ShortCode, url.CreatedAt);
        
    }

    public async Task DeletarAsync(string code)
    {
        var url = await repository.GetByCodeAsync(code);
        if (url == null)
            throw new KeyNotFoundException("Url não encontrada!");
            
        await repository.RemoveAsync(url);
    }
}