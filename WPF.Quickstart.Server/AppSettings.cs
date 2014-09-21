using System.Configuration;

namespace WPF.Quickstart.Server
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

        public static class Twitter
        {
            public static class OAuth
            {
                public static string ApiUrl
                {
                    get { return ConfigurationManager.AppSettings["Twitter.oAuth.ApiUrl"]; }
                }

                public static string ConsumerKey
                {
                    get { return ConfigurationManager.AppSettings["Twitter.oAuth.ConsumerKey"]; }
                }

                public static string ConsumerSecret
                {
                    get { return ConfigurationManager.AppSettings["Twitter.oAuth.ConsumerSecret"]; }
                }
            }
        }

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

                public static class RefreshFrequency
                {
                    public static string Tweet
                    {
                        get { return ConfigurationManager.AppSettings["WCFService.MarketData.RefreshFrequency.Tweet"]; }
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

