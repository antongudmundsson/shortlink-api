using ShortLink.Api.Models.Requests;
using ShortLink.Api.Models.Responses;
using ShortLink.Api.Services;

namespace ShortLink.Api.Endpoints;

public static class ShortLinkEndpoints
{
    public static void MapShortLinkEndpoints(this WebApplication app)
    {
        app.MapPost("/api/links", (
            CreateShortLinkRequest request,
            HttpContext httpContext,
            IShortLinkService shortLinkService) =>
        {
            try
            {
                var mapping = shortLinkService.CreateShortLink(request.Url);

                var shortUrl =
                    $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/{mapping.ShortCode}";

                var response = new CreateShortLinkResponse(
                    mapping.OriginalUrl,
                    mapping.ShortCode,
                    shortUrl);

                return Results.Created(shortUrl, response);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new
                {
                    error = ex.Message
                });
            }
        });

        app.MapGet("/{shortCode}", (
            string shortCode,
            IShortLinkService shortLinkService) =>
        {
            if (!shortLinkService.TryGetOriginalUrl(shortCode, out var originalUrl) ||
                string.IsNullOrWhiteSpace(originalUrl))
            {
                return Results.NotFound(new
                {
                    error = "Short link not found."
                });
            }

            return Results.Redirect(originalUrl);
        });
    }
}