using ShortLink.Api.Models;

namespace ShortLink.Api.Services;

public interface IShortLinkService
{
    UrlMapping CreateShortLink(string url);
    bool TryGetOriginalUrl(string shortCode, out string? originalUrl);
}