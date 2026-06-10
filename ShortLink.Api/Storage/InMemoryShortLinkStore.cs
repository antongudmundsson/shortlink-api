using System.Collections.Concurrent;
using ShortLink.Api.Models;

namespace ShortLink.Api.Storage;

public sealed class InMemoryShortLinkStore : IShortLinkStore
{
    private readonly ConcurrentDictionary<string, UrlMapping> _links = new();

    public bool TryAdd(UrlMapping mapping)
    {
        return _links.TryAdd(mapping.ShortCode, mapping);
    }

    public bool TryGetByCode(string shortCode, out UrlMapping? mapping)
    {
        return _links.TryGetValue(shortCode, out mapping);
    }
}