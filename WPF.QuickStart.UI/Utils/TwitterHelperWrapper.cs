using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Quickstart.Model.Twitter;
using WPF.QuickStart.UI.Utils.Twitter;

namespace WPF.QuickStart.UI.Utils
{
    internal static class TwitterHelperWrapper
    {
        internal static List<Tweet> GetTweets(string screenname, int count)
        {
            TwitterHelper th = new TwitterHelper(AppSettings.Twitter.OAuth.ConsumerKey, AppSettings.Twitter.OAuth.ConsumerSecret, AppSettings.Twitter.OAuth.ApiUrl);
            return th.GetOrderedTweets(screenname, count);
        }

        internal static Tweet GetLastTweet(string screenname)
        {
            TwitterHelper th = new TwitterHelper(AppSettings.Twitter.OAuth.ConsumerKey, AppSettings.Twitter.OAuth.ConsumerSecret, AppSettings.Twitter.OAuth.ApiUrl);
            return th.GetLastTweet(screenname);
        }
    }
}
