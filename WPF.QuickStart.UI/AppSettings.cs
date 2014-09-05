using System.Configuration;

namespace WPF.QuickStart.UI
{
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
    public static class AppSettings
    {
        public static class Application
        {
            public static string Name
            {
                get { return ConfigurationManager.AppSettings["Application.name"]; }
            }
        }
    }
}

