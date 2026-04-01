using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Entity;
using UrlShortener.Domain.Interfaces;
using UrlShortener.Infrastructure.DataBase;

namespace UrlShortener.Infrastructure.Repository;

public class UrlRepository(AppDbContext context) : IUrlRepository
{
    public async Task AddAsync(UrlItem item)
    {
        await context.UrlItems.AddAsync(item);
        await context.SaveChangesAsync();
    }

    public async Task<UrlItem> GetByCodeAsync(string shortCode)
    {
        return await context.UrlItems.FirstOrDefaultAsync(url => url.ShortCode == shortCode);
    }

    public async Task<UrlItem> GetLongUrlAsync(string Url)
    {
        return await context.UrlItems.FirstOrDefaultAsync(url => url.LongUrl == Url);
    }


    public async Task<IEnumerable<UrlItem>> GetAllAsync(int? page = 1)
    {
        int enittyPage = 10;
        var query = context.UrlItems.AsQueryable();

        if (page.HasValue)
        {
            query = query.OrderBy(url => url.Id).Skip((page.Value - 1) * enittyPage).Take(enittyPage);
        }
        
        return await query.ToListAsync();
    }

    public async Task RemoveAsync(UrlItem item)
    {
        context.UrlItems.Remove(item);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(UrlItem item)
    {
        context.UrlItems.Update(item);
        await context.SaveChangesAsync();
    }
}