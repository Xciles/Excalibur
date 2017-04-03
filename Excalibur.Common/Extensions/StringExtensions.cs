namespace Excalibur.Common.Extensions
{
    public static class StringExtensions
    {
        public static string Normalize(this string normalizeString)
        {
            var norm = normalizeString.Trim();
            if (norm.Length > 60)
            {
                norm = norm.Substring(0, 60);
            }
            norm = norm.Replace("&", "");
            norm = norm.Replace("%", "");
            norm = norm.Replace("/", "-");
            norm = norm.Replace("-", "");
            norm = norm.Replace("'", "");
            norm = norm.Replace(" ", "-");
            norm = norm.Replace("--", "-");
            norm = norm.ToLower();

            return norm;
        }
    }
}
