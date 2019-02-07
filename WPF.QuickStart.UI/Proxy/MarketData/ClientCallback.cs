using System;
using System.ServiceModel;
using System.Threading;
using WPF.Quickstart.Model.Twitter;
using WPF.QuickStart.UI.MarketDataProxy;
using WPF.QuickStart.UI.ViewModels.ClientServer;

namespace WPF.QuickStart.UI.Proxy.MarketData
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    class ClientCallback : IMarketDataCallback
    {
        private ChildViewModel m_viewModel;

        private SynchronizationContext _uiSyncContext = null;

        public ClientCallback(ChildViewModel viewModel)
        {
            m_viewModel = viewModel;
            _uiSyncContext = SynchronizationContext.Current;
        }

        /// <summary>
        /// Call back form WCF server
        /// </summary>
        public void SendTickUpdate(int param)
        {
            SendOrPostCallback callback =
                delegate(object state)
                {
                    m_viewModel.OnSendTickUpdate(param);
                };

            _uiSyncContext.Post(callback, null);
        }

        public IAsyncResult BeginSendTickUpdate(int param, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndSendTickUpdate(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public void PullRandomTweet(Tweet t)
        {
            SendOrPostCallback callback =
                delegate(object state)
                {
                    m_viewModel.OnPullRandomTweet(t);
                };

            _uiSyncContext.Post(callback, null);
        }
        
        public IAsyncResult BeginPullRandomTweet(Quickstart.Model.Twitter.Tweet t, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndPullRandomTweet(IAsyncResult result)
        {
            throw new NotImplementedException();
        }
    }
}
