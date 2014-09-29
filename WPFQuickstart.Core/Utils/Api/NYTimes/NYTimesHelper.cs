using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WPF.Quickstart.Model.NYTimes;

namespace WPFQuickstart.Core.Utils.Api.NYTimes
{
    public class NYTimesHelper : IProvider
    {
        string ApiKey { get; set; }

        public NYTimesHelper(string apiKey)
        {
            ApiKey = apiKey;
        }

        public string GetJsonFrom(string url)
        {
            string reponseJson = string.Empty;
            // Do the timeline
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "Get";

            WebResponse response = request.GetResponse();
            using (response)
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    reponseJson = reader.ReadToEnd();
                }
            }

            return reponseJson;
        }

        public List<NYTimesResults.Doc> SearchArticles(string query, string sort)
        {
            //http://api.nytimes.com/svc/search/v2/articlesearch.json?q=new+york+times&page=2&sort=oldest&api-key=934cf916cb79fb35082225c6b7229be9:4:69888635
            string urlFormat = @"http://api.nytimes.com/svc/search/v2/articlesearch.json?q={0}&sort={1}&api-key={2}";
            var url = string.Format(urlFormat, query, sort, ApiKey);
            var urlResponseJson = GetJsonFrom(url);
            NYTimesResults.RootObject resultSearch;

            try
            {
                resultSearch = Newtonsoft.Json.JsonConvert.DeserializeObject<NYTimesResults.RootObject>(urlResponseJson);
            }
            catch (JsonException ex)
            {
                return null;
            }

            var articles = new List<NYTimesResults.Doc>();
            resultSearch.response.docs.ForEach(
                d => { 
                    articles.Add(d);                
                });

            return articles.Take(10).ToList();
        }
    }
}
