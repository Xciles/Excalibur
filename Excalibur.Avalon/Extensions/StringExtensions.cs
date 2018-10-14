using System.Globalization;
using System.Text;

namespace Excalibur.Avalon.Extensions
{
    /// <summary>
    /// This class contains a few useful extensions on String. 
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Tries to normalize a String. Removing special characters.
        /// </summary>
        /// <param name="normalizeString">The string to normalize</param>
        /// <param name="maxLength">The max length of the string, default 60</param>
        /// <returns>A normalized string</returns>
        public static string Normalize(this string normalizeString, int maxLength = 60)
        {
            var norm = normalizeString.Trim();
            if (norm.Length > maxLength)
            {
                norm = norm.Substring(0, maxLength);
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

        public static string RemoveDiacritics(this string text)
        {
            if (text == null || string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
