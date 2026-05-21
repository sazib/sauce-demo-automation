using System.Text.RegularExpressions;

namespace SauceDemo.Framework.Utilities;

public static class StringHelper
{
    public static string ExtractPrice(string text)
    {
        var match = Regex.Match(text, @"\$?(\d+\.?\d*)");
        return match.Success ? match.Groups[1].Value : string.Empty;
    }

    public static decimal ParsePrice(string priceText)
    {
        var price = ExtractPrice(priceText);
        return decimal.TryParse(price, out var result) ? result : 0m;
    }

    public static string RemoveWhitespace(string text)
    {
        return Regex.Replace(text, @"\s+", "");
    }

    public static string NormalizeWhitespace(string text)
    {
        return Regex.Replace(text.Trim(), @"\s+", " ");
    }

    public static bool ContainsIgnoreCase(this string source, string value)
    {
        return source?.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
    }
}
