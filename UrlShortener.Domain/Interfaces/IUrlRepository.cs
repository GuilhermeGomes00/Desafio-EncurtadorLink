using UrlShortener.Domain.Entity;

namespace UrlShortener.Domain.Interfaces;

public interface IUrlRepository
{
    Task AddAsync(UrlItem item);
    Task<UrlItem> GetByCodeAsync(string shortCode);
    Task<UrlItem> GetLongUrlAsync(string Url);
    Task<IEnumerable<UrlItem>> GetAllAsync(int? page = 1);
    Task RemoveAsync(UrlItem item);
    Task UpdateAsync(UrlItem item);
}