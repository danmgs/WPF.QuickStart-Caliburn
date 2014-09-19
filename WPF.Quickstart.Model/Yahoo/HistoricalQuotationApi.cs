using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Quickstart.Model.Yahoo
{
    public class HistoricalQuotationResults
    {
        //public class Url
        //{
        //    public string execution-start-time { get; set; }
        //    public string execution-stop-time { get; set; }
        //    public string execution-time { get; set; }
        //    public string content { get; set; }
        //}

        //public class Cache
        //{
        //    public string execution-start-time { get; set; }
        //    public string execution-stop-time { get; set; }
        //    public string execution-time { get; set; }
        //    public string method { get; set; }
        //    public string type { get; set; }
        //    public string content { get; set; }
        //}

        //public class Query2
        //{
        //    public string execution-start-time { get; set; }
        //    public string execution-stop-time { get; set; }
        //    public string execution-time { get; set; }
        //    public string @params { get; set; }
        //    public string content { get; set; }
        //}

        //public class Javascript
        //{
        //    public string execution-start-time { get; set; }
        //    public string execution-stop-time { get; set; }
        //    public string execution-time { get; set; }
        //    public string instructions-used { get; set; }
        //    public string table-name { get; set; }
        //}

        //public class Diagnostics
        //{
        //    public List<Url> url { get; set; }
        //    public string publiclyCallable { get; set; }
        //    public List<Cache> cache { get; set; }
        //    public List<Query2> query { get; set; }
        //    public Javascript javascript { get; set; }
        //    public string user-time { get; set; }
        //    public string service-time { get; set; }
        //    public string build-version { get; set; }
        //}

        public class Quote
        {
            public string Symbol { get; set; }
            public string Date { get; set; }
            public string Open { get; set; }
            public string High { get; set; }
            public string Low { get; set; }
            public string Close { get; set; }
            public string Volume { get; set; }
            public string Adj_Close { get; set; }
        }

        public class Results
        {
            public List<Quote> quote { get; set; }
        }

        public class Query
        {
            public int count { get; set; }
            public string created { get; set; }
            public string lang { get; set; }
            //public Diagnostics diagnostics { get; set; }
            public Results results { get; set; }
        }

        public class RootObject
        {
            public Query query { get; set; }
        }
    }
}
