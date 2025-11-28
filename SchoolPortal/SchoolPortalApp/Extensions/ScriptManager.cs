using System.Collections.Generic;

namespace SchoolPortalApp.Extensions
{
    public static class ScriptManager
    {
        private static readonly List<string> Scripts = new List<string>();

        public static void RegisterScript(string script)
        {
            if (!Scripts.Contains(script))
            {
                Scripts.Add(script);
            }
        }

        public static string RenderScripts()
        {
            return string.Join("\n", Scripts);
        }
    }
}