using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.DTOs;
using UrlShortener.Application.Interfaces;

namespace UrlShortener.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class EncurtadorController(IUrlServices _services) : ControllerBase
{
    [HttpPost("/encurtar")]
    public async Task<IActionResult> Encurtar([FromBody] UrlRequestDto dto)
    {
        try
        {
            var newUrl = await _services.Encurtar(dto.Url);
            
            return Created($"/{newUrl.ShortUrl}", new ModelViewResponseWoClick(
                newUrl.Id,
                newUrl.LongUrl,
                newUrl.ShortUrl,
                newUrl.date
            ));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("/{shortUrl}")]
    public async Task<IActionResult> Get(string shortUrl)
    {
        var url = await _services.Redirect(shortUrl);
        if (url == null) return NotFound();

        return Redirect(url.Url);
    }

    [HttpGet("/stats/{shortUrl}")]
    public async Task<IActionResult> GetStats(string shortUrl)
    {
        var response = await _services.Status(shortUrl);
        if (response == null) return NotFound();
        
        return Ok(new ModelViewResponse(
            response.Id,
            response.LongUrl,
            response.ShortUrl,
            response.date,
            response.click
            ));
    }

    [HttpGet("/lista")]
    public async Task<IActionResult> GetLista(int? page)
    {
        var response = await _services.GetAllAsync(page);
        
        return Ok(response.Select(r => new ModelViewResponseWoClick(
            r.Id,
            r.LongUrl,
            r.ShortUrl,
            r.date
            ))
        );
    }

    [HttpDelete("/delete/{shortUrl}")]
    public async Task<IActionResult> Delete(string shortUrl)
    {
        try
        {
            await _services.DeletarAsync(shortUrl);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
}