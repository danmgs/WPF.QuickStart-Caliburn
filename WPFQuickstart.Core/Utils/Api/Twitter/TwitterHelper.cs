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
using WPFQuickstart.Core.Utils.Api;

namespace WPF.QuickStart.UI.Utils.Api.Twitter
{
    public class TwitterHelper : IProvider
    {
        TwitAuthenticateResponse TwitAuthResponse { get; set; }

        public TwitterHelper(string oAuthConsumerKey, string oAuthConsumerSecret, string oAuthUrl)
        {
            TwitAuthResponse = Connect(oAuthConsumerKey, oAuthConsumerSecret, oAuthUrl);
        }

        #region Private Methods

        private TwitAuthenticateResponse Connect(string oAuthConsumerKey, string oAuthConsumerSecret, string oAuthUrl)
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

            WebResponse authResponse;
            try
            {
                authResponse = authRequest.GetResponse();
            }
            catch
            {
                // Log error.
                return null;
            }

            // deserialize into an object
            TwitAuthenticateResponse twitAuthResponse = null;
            using (authResponse)
            {
                using (var reader = new StreamReader(authResponse.GetResponseStream()))
                {
                    var objectText = reader.ReadToEnd();
                    twitAuthResponse = JsonConvert.DeserializeObject<TwitAuthenticateResponse>(objectText);
                }
            }

            return twitAuthResponse;
        }

        public string GetJsonFrom(string timelineUrl)
        {
            string timeLineJson = string.Empty;
            // Do the timeline
            HttpWebRequest timeLineRequest = (HttpWebRequest)WebRequest.Create(timelineUrl);
            var timelineHeaderFormat = "{0} {1}";
            timeLineRequest.Headers.Add("Authorization", string.Format(timelineHeaderFormat, TwitAuthResponse.token_type, TwitAuthResponse.access_token));
            timeLineRequest.Method = "Get";

            WebResponse timeLineResponse = timeLineRequest.GetResponse();
            using (timeLineResponse)
            {
                using (var reader = new StreamReader(timeLineResponse.GetResponseStream()))
                {
                    timeLineJson = reader.ReadToEnd();
                }
            }

            return timeLineJson;
        }

        #endregion

        #region Public Methods

        public List<Tweet> GetOrderedTweets(string screenname, int count)
        {
            // Do the timeline
            var timelineFormat = "https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name={0}&include_rts=1&exclude_replies=1&count={1}";
            var timelineUrl = string.Format(timelineFormat, screenname, count);
            var timeLineJson = GetJsonFrom(timelineUrl);

            List<Tweet> tweets = new List<Tweet>();
            JArray jsonDat = JArray.Parse(timeLineJson);
            for (int x = 0; x < jsonDat.Count(); x++)
            {
                JObject tweet = JObject.Parse(jsonDat[x].ToString());

                Tweet t = new Tweet()
                {
                    Text = tweet["text"].ToString(),
                    Date = DateTime.ParseExact(tweet["created_at"].ToString(), "ddd MMM dd HH:mm:ss zzz yyyy", CultureInfo.InvariantCulture),
                    UserProfileImageUrl = tweet["user"]["profile_image_url"].ToString(),
                    ScreenName = tweet["user"]["screen_name"].ToString()
                };

                tweets.Add(t);
                //whatever else you want to look up
            }

            return tweets.OrderByDescending(t => t.Date).ToList();
        }

        public Tweet GetLastTweet(string screename)
        {
            return GetOrderedTweets(screename, 1).FirstOrDefault();
        }

        #endregion

        public List<Tweet> SearchTweets(string keyword)
        {
            var urlFormat = "https://api.twitter.com/1.1/search/tweets.json?q={0}";
            var url = string.Format(urlFormat, keyword);
            var urlResponseJson = GetJsonFrom(url);
            ResultSearchedTweet.RootObject resultSearchedTweets;

            try
            {
                resultSearchedTweets = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultSearchedTweet.RootObject>(urlResponseJson);
            }
            catch(JsonException)
            {
                return null;
            }

            List<Tweet> tweets = new List<Tweet>();
            resultSearchedTweets.statuses.ForEach(
                s => { 
                    Tweet t = new Tweet()
                    {
                        Text = s.text,
                        Date = DateTime.ParseExact(s.created_at, "ddd MMM dd HH:mm:ss zzz yyyy", CultureInfo.InvariantCulture),
                        UserProfileImageUrl = s.user.profile_background_image_url,
                        ScreenName = s.user.screen_name
                    };
                    tweets.Add(t);                
                });

            return tweets;            
        }
    }
}
