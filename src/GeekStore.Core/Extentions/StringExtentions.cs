namespace GeekStore.Core.Extentions.Exceptions
{
    public static class StringExtentions
    {
        public static string RemoveText(this string text, string removeValue)
        {
            return text.Replace(removeValue, string.Empty);
        }

        public static bool IsInsensitiveEquals(this string text, string compare)
        {
            return string.Equals(text.Trim(), compare.Trim(), System.StringComparison.OrdinalIgnoreCase);
        }
    }
}