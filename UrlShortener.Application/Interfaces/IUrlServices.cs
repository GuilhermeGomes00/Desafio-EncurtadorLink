using System.Collections;
using UrlShortener.Application.DTOs;

namespace UrlShortener.Application.Interfaces;

public interface IUrlServices
{
    Task<IEnumerable<ModelViewResponseWoClick>> GetAllAsync(int? page);
    Task<ModelViewResponse> Status(string code);
    Task<ModelViewRedirectResponse> Redirect(string code);
    Task<ModelViewResponseWoClick> Encurtar(string Url);
    Task DeletarAsync(string code);
}