using System.Text.RegularExpressions;

namespace TrainManagement.Common.Helpers
{
    public static class HttpsHelper
    {
        public static string AddHttpPrefix(this string input)
        {
            string pattern = "href=\"(?!https://)";
            string replacement = "href=\"https://";

            return Regex.Replace(input, pattern, replacement);
        }
    }
}
