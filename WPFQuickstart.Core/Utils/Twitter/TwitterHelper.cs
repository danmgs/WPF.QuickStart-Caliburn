using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WPF.Quickstart.Model.Twitter;

namespace WPF.QuickStart.UI.Utils.Twitter
{
    public class TwitterHelper
    {
        public static List<Tweet> GetTweets(string screenname, int count, string oAuthConsumerKey, string oAuthConsumerSecret, string oAuthUrl)
        {
            // Do the Authenticate
            var authHeaderFormat = "Basic {0}";

            var authHeader = string.Format(authHeaderFormat,
                Convert.ToBase64String(Encoding.UTF8.GetBytes(Uri.EscapeDataString(oAuthConsumerKey) + ":" +
                Uri.EscapeDataString((oAuthConsumerSecret)))
            ));

            var postBody = "grant_type=client_credentials";

            HttpWebRequest authRequest = (HttpWebRequest)WebRequest.Create(oAuthUrl);
            authRequest.Headers.Add("Authorization", authHeader);
            authRequest.Method = "POST";
            authRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            authRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (Stream stream = authRequest.GetRequestStream())
            {
                byte[] content = ASCIIEncoding.ASCII.GetBytes(postBody);
                stream.Write(content, 0, content.Length);
            }

            authRequest.Headers.Add("Accept-Encoding", "gzip");

            WebResponse authResponse = authRequest.GetResponse();
            // deserialize into an object
            TwitAuthenticateResponse twitAuthResponse;
            using (authResponse)
            {
                using (var reader = new StreamReader(authResponse.GetResponseStream()))
                {
                    var objectText = reader.ReadToEnd();
                    twitAuthResponse = JsonConvert.DeserializeObject<TwitAuthenticateResponse>(objectText);
                }
            }

            // Do the timeline
            var timelineFormat = "https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name={0}&include_rts=1&exclude_replies=1&count={1}";
            var timelineUrl = string.Format(timelineFormat, screenname, count);
            HttpWebRequest timeLineRequest = (HttpWebRequest)WebRequest.Create(timelineUrl);
            var timelineHeaderFormat = "{0} {1}";
            timeLineRequest.Headers.Add("Authorization", string.Format(timelineHeaderFormat, twitAuthResponse.token_type, twitAuthResponse.access_token));
            timeLineRequest.Method = "Get";

            WebResponse timeLineResponse = timeLineRequest.GetResponse();
            var timeLineJson = string.Empty;
            using (timeLineResponse)
            {
                using (var reader = new StreamReader(timeLineResponse.GetResponseStream()))
                {
                    timeLineJson = reader.ReadToEnd();
                }
            }

            List<Tweet> tweets = new List<Tweet>();
            JArray jsonDat = JArray.Parse(timeLineJson);
            for (int x = 0; x < jsonDat.Count(); x++)
            {
                JObject tweet = JObject.Parse(jsonDat[x].ToString());

                Tweet t = new Tweet()
                {
                    Text = tweet["text"].ToString(),
                    Date = DateTime.ParseExact(tweet["created_at"].ToString(), "ddd MMM dd HH:mm:ss zzz yyyy", CultureInfo.InvariantCulture)
                };

                tweets.Add(t);
                //whatever else you want to look up
            }

            return tweets.OrderByDescending(t => t.Date).ToList();
        }
    }
}
