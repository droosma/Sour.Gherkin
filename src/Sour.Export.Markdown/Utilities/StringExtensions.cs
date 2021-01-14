namespace Sour.Export.Markdown.Utilities
{
    internal static class StringExtensions
    {
        public static bool IsEmpty(this string value)
            => string.IsNullOrWhiteSpace(value);
    }
}