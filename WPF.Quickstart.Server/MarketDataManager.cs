using Common.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Quickstart.Server
{
    public class MarketDataManager : IMarketData
    {
        static ILog Logger = LogManager.GetCurrentClassLogger();

        private IClientCallback m_pIClientCallback = null;

        public MarketDataManager()
        {
            Logger.Info(string.Format("MarketDataManager({0}) Created...", GetHashCode()));
            m_pIClientCallback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
        }

        public StringCollection GetDataSourceList()
        {
            StringCollection collection = new StringCollection();
            Random rdm = new Random();
            var rdnNum = rdm.Next(1, 100);

            Logger.Debug(string.Format("GetDataSourceList Random number is {0}", rdnNum));

            for (var i = 0; i < rdnNum; i++)
            {
                collection.Add(string.Format("item{0}", i));
            }

            Logger.Debug(string.Format("GetDataSourceList with {0} items", collection.Count));

            m_pIClientCallback.SendTickUpdate(collection.Count); // est ce qu'on peut renvoyer la collection, si oui, a mon avis elle devrait etre serializable avant :)
            
            return collection;
        }
    }
}
