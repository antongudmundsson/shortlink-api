using ShortLink.Api.Models;

namespace ShortLink.Api.Storage;

public interface IShortLinkStore
{
    bool TryAdd(UrlMapping mapping);
    bool TryGetByCode(string shortCode, out UrlMapping? mapping);
}