using System.Security.Cryptography;

namespace ShortLink.Api.Utilities;

public sealed class ShortCodeGenerator : IShortCodeGenerator
{
    // Base62 ger korta URL-vänliga koder utan specialtecken.
    private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    // 6 tecken ger över 56 miljarder möjliga kombinationer.
    private const int CodeLength = 6;

    public string Generate()
    {
        // Kryptografiskt säker slumpgenerator som minskar risken för förutsägbara koder.
        return RandomNumberGenerator.GetString(Alphabet, CodeLength);
    }
}