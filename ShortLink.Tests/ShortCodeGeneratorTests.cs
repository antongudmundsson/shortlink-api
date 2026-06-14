using ShortLink.Api.Utilities;

namespace ShortLink.Tests;

public sealed class ShortCodeGeneratorTests
{
    [Fact]
    public void Generate_ReturnsCodeWithExpectedLength()
    {
        var generator = new ShortCodeGenerator();

        var code = generator.Generate();

        Assert.Equal(6, code.Length);
    }
    [Fact]
public void Generate_ReturnsOnlyAllowedCharacters()
{
    var generator = new ShortCodeGenerator();

    var code = generator.Generate();

    Assert.Matches("^[a-zA-Z0-9]+$", code);
}
}