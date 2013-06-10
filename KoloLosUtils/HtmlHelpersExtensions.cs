using System.Text.RegularExpressions;

// ReSharper disable CheckNamespace
namespace System.Web.Mvc
// ReSharper restore CheckNamespace
{
    public static class HtmlHelpersExtensions
    {
        private const string HtmlElementPattern = "<[^>]*>";

        public static string RemoveMarkup(this HtmlHelper htmlHelper, string markup)
        {
            return Regex.Replace(markup, HtmlElementPattern, string.Empty, RegexOptions.None);
        }
    }
}
