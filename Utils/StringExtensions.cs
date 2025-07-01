namespace Utils;

public static class StringExtensions
{
    public static bool IsNullOrEmptyOrWhiteSpace(this string str) =>
        string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
   

    public static string GetSubStringAfterLastChar(this string str, char c)
    {
        if (str.IsNullOrEmptyOrWhiteSpace()) return str;
        var index = str.LastIndexOf(c);
        if (index < 0) return str;
        return str.Substring(index + 1, str.Length - index - 1);
    }
}