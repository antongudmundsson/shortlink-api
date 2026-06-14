using ShortLink.Api.Models;
using ShortLink.Api.Storage;
using ShortLink.Api.Utilities;

namespace ShortLink.Api.Services;

public sealed class ShortLinkService : IShortLinkService
{
    private const int MaxAttempts = 10;

    private readonly IShortLinkStore _store;
    private readonly IShortCodeGenerator _shortCodeGenerator;

    public ShortLinkService(
        IShortLinkStore store,
        IShortCodeGenerator shortCodeGenerator)
    {
        _store = store;
        _shortCodeGenerator = shortCodeGenerator;
    }

    public UrlMapping CreateShortLink(string url)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) ||
            (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
        {
            throw new ArgumentException("The provided URL is not valid.", nameof(url));
        }

        for (var attempt = 0; attempt < MaxAttempts; attempt++)
        {
            var shortCode = _shortCodeGenerator.Generate();
            var mapping = new UrlMapping(shortCode, url);

            if (_store.TryAdd(mapping))
            {
                return mapping;
            }
        }

        throw new InvalidOperationException("Could not generate a unique short code.");
    }

    public bool TryGetOriginalUrl(string shortCode, out string? originalUrl)
{
    if (!_store.TryGetByCode(shortCode, out var mapping) || mapping is null)
    {
        originalUrl = null;
        return false;
    }

    originalUrl = mapping.OriginalUrl;
    return true;
}
}