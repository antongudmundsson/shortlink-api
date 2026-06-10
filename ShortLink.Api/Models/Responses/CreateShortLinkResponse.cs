namespace ShortLink.Api.Models.Responses;

public sealed record CreateShortLinkResponse(
    string OriginalUrl,
    string ShortCode,
    string ShortUrl
);
