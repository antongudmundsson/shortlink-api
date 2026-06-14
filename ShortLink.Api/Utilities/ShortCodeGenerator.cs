using System.Security.Cryptography;

namespace ShortLink.Api.Utilities;

public sealed class ShortCodeGenerator : IShortCodeGenerator
{
    private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private const int CodeLength = 6;

    public string Generate()
    {
        return RandomNumberGenerator.GetString(Alphabet, CodeLength);
    }
}