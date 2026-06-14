using ShortLink.Api.Services;
using ShortLink.Api.Storage;
using ShortLink.Api.Utilities;

namespace ShortLink.Tests;

public sealed class ShortLinkServiceTests
{
    [Fact]
    public void CreateShortLink_ReturnsMapping_WithGeneratedCode()
    {
        var store = new InMemoryShortLinkStore();
        var generator = new FakeShortCodeGenerator();

        var service = new ShortLinkService(
            store,
            generator);

        var result = service.CreateShortLink("https://google.com");

        Assert.Equal("abc123", result.ShortCode);
        Assert.Equal("https://google.com", result.OriginalUrl);
    }

    private sealed class FakeShortCodeGenerator : IShortCodeGenerator
    {
        public string Generate()
        {
            return "abc123";
        }
    }
    [Fact]
public void CreateShortLink_WithInvalidUrl_ThrowsArgumentException()
{
    var store = new InMemoryShortLinkStore();
    var generator = new FakeShortCodeGenerator();

    var service = new ShortLinkService(
        store,
        generator);

    var exception = Assert.Throws<ArgumentException>(() =>
        service.CreateShortLink("not-a-valid-url"));

    Assert.Equal("url", exception.ParamName);
}
[Fact]
public void TryGetOriginalUrl_WhenShortCodeExists_ReturnsOriginalUrl()
{
    var store = new InMemoryShortLinkStore();
    var generator = new FakeShortCodeGenerator();

    var service = new ShortLinkService(
        store,
        generator);

    var mapping = service.CreateShortLink("https://google.com");

    var found = service.TryGetOriginalUrl(mapping.ShortCode, out var originalUrl);

    Assert.True(found);
    Assert.Equal("https://google.com", originalUrl);
}
}