public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return true;
        }

        foreach (var c in str)
        {
            if (!char.IsWhiteSpace(c))
            {
                return false;
            }
        }

        return true;
    }
}