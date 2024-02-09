static class HelperFunctions
{
    public static string ConcatStringsByLine(string a, string b)
    {
        List<string> aLines = a.Split(Environment.NewLine).ToList();
        List<string> bLines = b.Split(Environment.NewLine).ToList();

        int higherCount = Math.Max(aLines.Count, bLines.Count);

        aLines.Pad(higherCount, "");
        bLines.Pad(higherCount, "");

        return string.Join(Environment.NewLine, aLines.Zip(bLines, (a, b) => a + b));
    }

    public static List<T> Pad<T>(this List<T> list, int size, T padValue)
    {
        while(list.Count < size)
            list.Add(padValue);

        return list;
    }

    public static string Repeat(this string s, int n, string? separator = null)
    {
        string ret = s;

        for(int i = 1; i < n; i++){
            ret += separator != null ? separator + s : s;
        }

        return ret;
    }
}