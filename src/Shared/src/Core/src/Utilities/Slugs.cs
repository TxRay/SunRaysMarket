using System.Text.RegularExpressions;

namespace SunRaysMarket.Shared.Core.Utilities;

public static partial class Slugs
{
    public static string CreateSlug(string input)
    {
        var slug = input.ToLowerInvariant();
        slug = MatchInvalidChars().Replace(slug, "");
        slug = MatchMultipleSpaces().Replace(slug, " ").Trim();
        slug = MatchSingleSpace().Replace(slug, "-");
        return slug;
    }

    [GeneratedRegex("\\s")]
    private static partial Regex MatchSingleSpace();

    [GeneratedRegex("\\s{2,}")]
    private static partial Regex MatchMultipleSpaces();

    [GeneratedRegex("[^a-z0-9\\s-]")]
    private static partial Regex MatchInvalidChars();
}