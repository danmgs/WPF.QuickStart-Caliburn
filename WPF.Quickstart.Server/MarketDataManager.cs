using log4net;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using WPF.Quickstart.Model.Twitter;
using WPF.QuickStart.UI.Utils.Api.Twitter;

namespace WPF.Quickstart.Server
{
    public class MarketDataManager : IMarketData
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MarketDataManager));

        private IClientCallback m_pIClientCallback = null;

        public MarketDataManager()
        {
            log.Info(string.Format("MarketDataManager({0}) Created...", GetHashCode()));
            m_pIClientCallback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
            OperationContext.Current.Channel.Faulted += new EventHandler(FaultedHandler);
        }

        private void FaultedHandler(object sender, EventArgs e)
        {
            log.Warn(string.Format("Detect client disconnection"));
            throw new NotImplementedException();
        }

        public StringCollection GetDataSourceList()
        {
            StringCollection collection = new StringCollection();
            Random rdm = new Random();
            var rdnNum = rdm.Next(1, 100);

            log.Debug(string.Format("GetDataSourceList Random number is {0}", rdnNum));

            for (var i = 1; i <= rdnNum; i++)
            {
                collection.Add(string.Format("item{0}", i));
            }

            log.Debug(string.Format("GetDataSourceList with {0} items", collection.Count));

            // On peut appeler cette ligne plusieurs fois !!
            // Q : Est ce qu'on peut renvoyer la collection, si oui, a mon avis elle devrait etre serializable avant :). 
            m_pIClientCallback.SendTickUpdate(collection.Count);
            
            return collection;
        }

        public void GetRandomTweet(string keyword)
        {
            while (true)
            {
                log.Warn(string.Format("GetRandomTweet('{0}')", keyword));
                Tweet t = SearchRandomTweet(keyword);
                m_pIClientCallback.PullRandomTweet(t);
                Thread.Sleep(Convert.ToInt32(AppSettings.WCFService.MarketData.RefreshFrequency.Tweet));
            }
        }

        private Tweet SearchRandomTweet(string keyword)
        {
            if(string.IsNullOrEmpty(keyword))
            {
                keyword = "finance";
            }
            TwitterHelper twitterHelper = new TwitterHelper(AppSettings.Twitter.OAuth.ConsumerKey, AppSettings.Twitter.OAuth.ConsumerSecret, AppSettings.Twitter.OAuth.ApiUrl);
            var resTweets = twitterHelper.SearchTweets(keyword);

            Random rand = new Random();
            int toSkip = rand.Next(0, resTweets.Count);

            return resTweets.Skip(toSkip).Take(1).FirstOrDefault();
        }
    }
}
