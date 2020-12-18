namespace Sour.Core.Utilities
{
    internal static class StringExtensions
    {
        public static bool IsEmpty(this string value)
            => string.IsNullOrWhiteSpace(value);
    }
}