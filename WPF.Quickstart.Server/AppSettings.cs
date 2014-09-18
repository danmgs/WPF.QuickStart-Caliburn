using System.Configuration;

namespace WPF.Quickstart.Server
{
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
    public static class AppSettings
    {
        public static class WCFService
        {
            public static class MarketData
            {
                public static class Metadata
                {
                    public static string Url
                    {
                        get { return ConfigurationManager.AppSettings["WCFService.MarketData.Metadata.Url"]; }
                    }
                }

                public static string Url
                {
                    get { return ConfigurationManager.AppSettings["WCFService.MarketData.Url"]; }
                }
            }
        }
    }
}

