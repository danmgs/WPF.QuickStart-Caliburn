using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using WPF.QuickStart.UI.MarketDataProxy;
using WPF.QuickStart.UI.ViewModels;
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
                    m_viewModel.ShowConnected(param);
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
    }
}
