namespace ShortLink.Api.Models;

public sealed record UrlMapping(
    string ShortCode,
    string OriginalUrl
);