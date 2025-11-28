using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolPortalApp.Extensions
{
    public static class ScriptRegistrar
    {
        private const string Key = "__RegisteredScripts";
        public static void RegisterScript(this IHtmlHelper html, string scriptHtml)
        {
            var items = html.ViewContext.HttpContext.Items;
            var list = items[Key] as List<string> ?? new List<string>();
            list.Add(scriptHtml);
            items[Key] = list;
        }

        public static IHtmlContent RenderRegisteredScripts(this IHtmlHelper html)
        {
            var items = html.ViewContext.HttpContext.Items;
            var list = items[Key] as List<string>;
            if (list == null) return HtmlString.Empty;
            return new HtmlString(string.Join(Environment.NewLine, list));
        }
    }
}
