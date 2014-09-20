using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using WPF.Quickstart.Model.Yahoo;
using WPF.QuickStart.UI.Events;
using WPF.QuickStart.UI.MarketDataProxy;
using WPF.QuickStart.UI.Proxy.MarketData;
using WPF.QuickStart.UI.ViewModels.Common;
using WPF.QuickStart.UI.ViewModels.Common.Dialog;

namespace WPF.QuickStart.UI.ViewModels
{
    public class ChildViewModel : ExtendedScreen
    {
        private MarketDataClient m_pMarketDataClient = null;

        private ICollectionView _myCollectionItems;
        public ICollectionView MyCollectionItems
        {
            get
            {
                return _myCollectionItems;
            }
            set
            {
                _myCollectionItems = value;
                this.NotifyOfPropertyChange(() => MyCollectionItems);
            }
        }

        private bool connected;

        public bool Connected
        {
            get { return connected; }
            set
            {
                connected = value;
                NotifyOfPropertyChange(() => Connected);
            }
        }

		public ChildViewModel(string displayName, IEventAggregator eventAgg, IWindowManager windowManager)
			: base(eventAgg, windowManager)
		{
            base.DisplayName = displayName;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            PublishStatusEvent(string.Format("Load {0} ...", DisplayName));
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);

            if (close)
            {
                //MessageBox.Show(string.Format("Closed: '{0}'", DisplayName));
            }
        }

        private async Task<List<object>> LongRetrieveMockElements(int count)
        {
            PublishStatusEvent(string.Format("Loading {0} elements ...", count));
            var context = TaskScheduler.FromCurrentSynchronizationContext();
            var listElements = new List<object>();
            Enabled = false;

            await Task.Delay(5000);

            await Task.Run(() =>
            {
                for (var i = 0; i < count; i++)
                {
                    listElements.Add(new { index = i, label = string.Concat("elem", i) });
                }
                MyCollectionItems = CollectionViewSource.GetDefaultView(listElements);
            })
            .ContinueWith(previousTask =>
                {
                    PublishStatusEvent(string.Format("Finish loading {0} elements !", count));
                    Enabled = true;
                    // Add Caliburn Logger.Error ...
                }, context);

            return listElements;
        }

        public async void GenerateItems(string elementsCount)
        {
            int count;
            bool res = Int32.TryParse(elementsCount, out count);

            if (res)
            {
                await LongRetrieveMockElements(count);
            }
            else
            {
                var message = "Wrong input. Must be a number ...";
                _windowManager.ShowDialog(new DialogViewModel()
                {
                    Text = String.Format(message),
                    DisplayName = "Wrong Input",
                    NotificationType = NotificationType.Error
                });
                PublishStatusEvent(message);
            }
        }

        public void ShowConnected(int param)
        {
            var message = Connected ? "Connected" : "Not Connected";
            _windowManager.ShowDialog(new DialogViewModel()
            {
                Text = String.Format("{0}  with {1} items in collection", message, param),
                DisplayName = "Connection State",
                NotificationType = NotificationType.Info
            });
        }

        public void ConnectBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                PublishStatusEvent("Try to connect to server ...");

                // Disconnect surrent connection
                if (m_pMarketDataClient != null)
                    if (m_pMarketDataClient.State == System.ServiceModel.CommunicationState.Opened)
                    {
                        //Log("Closing connection...");
                        m_pMarketDataClient.Close();
                    }
            }
            catch (Exception ex)
            {
                //Log(ex);
                PublishStatusEvent("Error connecting to server ...");
            }

            try
            {
                // Construct InstanceContext to handle messages on callback interface.
                InstanceContext instanceContext = new InstanceContext(new ClientCallback(this));

                // Connect
                m_pMarketDataClient = new MarketDataClient(instanceContext);
                //Log(string.Format("Connecting to WCF Service at {0}...", m_pMarketDataClient.Endpoint.Address));
                m_pMarketDataClient.Open();

                // Load Symbols list
                if (m_pMarketDataClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    Connected = true;
                }
            }
            catch (Exception ex)
            {
                //Log(ex);
                var message = "Server is not reachable";
                _windowManager.ShowDialog(new DialogViewModel()
                {
                    Text = String.Format("{0}: \n{1}", message, "Please check if the server is running."),
                    DisplayName = message,
                    NotificationType = NotificationType.Error
                });
                PublishStatusEvent(message);
            }
        }

        public void GetDataSourceList()
        {
            var list = m_pMarketDataClient.GetDataSourceList();
        }
    }
}
