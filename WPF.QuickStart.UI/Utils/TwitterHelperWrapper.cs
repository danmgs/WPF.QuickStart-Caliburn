using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Quickstart.Model.Twitter;
using WPF.QuickStart.UI.Utils.Twitter;

namespace WPF.QuickStart.UI.Utils
{
    internal class TwitterHelperWrapper
    {
        public static List<Tweet> GetTweets(string screenname, int count)
        {
            return TwitterHelper.GetTweets(screenname, count, AppSettings.Twitter.OAuth.ConsumerKey, AppSettings.Twitter.OAuth.ConsumerSecret, AppSettings.Twitter.OAuth.ApiUrl);
        }
    }
}
