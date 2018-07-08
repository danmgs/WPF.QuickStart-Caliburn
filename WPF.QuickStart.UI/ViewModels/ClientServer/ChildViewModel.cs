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
using WPF.Quickstart.Model.Twitter;
using WPF.Quickstart.Model.Yahoo;
using WPF.QuickStart.UI.Events;
using WPF.QuickStart.UI.MarketDataProxy;
using WPF.QuickStart.UI.Proxy.MarketData;
using WPF.QuickStart.UI.ViewModels.Common;
using WPF.QuickStart.UI.ViewModels.Common.Dialog;

namespace WPF.QuickStart.UI.ViewModels.ClientServer
{
    public class ChildViewModel : ExtendedScreen
    {
        private MarketDataClient m_pMarketDataClient = null;

        private BindableCollection<string> _serverResponseItems = new BindableCollection<string>();
        public BindableCollection<string> ServerResponseItems
        {
            get
            {
                return _serverResponseItems;
            }
            set
            {
                _serverResponseItems = value;
                this.NotifyOfPropertyChange(() => ServerResponseItems);
            }
        }

        private BindableCollection<Tweet> _serverResponseTweets = new BindableCollection<Tweet>();
        public BindableCollection<Tweet> ServerResponseTweets
        {
            get
            {
                return _serverResponseTweets;
            }
            set
            {
                _serverResponseTweets = value;
                this.NotifyOfPropertyChange(() => ServerResponseTweets);
            }
        }

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

        private bool _connected;

        public bool Connected
        {
            get { return _connected; }
            set
            {
                _connected = value;
                NotifyOfPropertyChange(() => Connected);
            }
        }

        private bool _connectedTwitter;

        public bool ConnectedTwitter
        {
            get { return _connectedTwitter; }
            set
            {
                _connectedTwitter = value;
                NotifyOfPropertyChange(() => ConnectedTwitter);
            }
        }        

        public string _keyword;

        public string Keyword
        {
            get { return _keyword; }
            set
            {
                _keyword = value;
                NotifyOfPropertyChange(() => Keyword);
            }
        }

		public ChildViewModel(string displayName, IEventAggregator eventAgg, IWindowManager windowManager)
            : base(displayName, eventAgg, windowManager)
		{
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            PublishStatusEvent(string.Format("Load {0} ...", DisplayName));
            Connected = false;
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);

            if (close)
            {
                //MessageBox.Show(string.Format("Closed: '{0}'", DisplayName));
                Disconnect();
            }
        }

        // TODO : provide a fix to manage exception because client is still receiving callbacks from server
        private void Disconnect()
        {
            try
            {

                if (m_pMarketDataClient != null)
                {
                    if (m_pMarketDataClient.State == System.ServiceModel.CommunicationState.Opened)
                    {
                        m_pMarketDataClient.Close();
                    }
                }
            }
            catch(Exception)
            {

            }
        }

        #region Methods

        private async Task<List<object>> LongRetrieveMockElements(int count)
        {
            PublishStatusEvent(string.Format("Loading {0} elements ...", count));
            var context = TaskScheduler.FromCurrentSynchronizationContext();
            var listElements = new List<object>();
            IsBusy = true;

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
                    IsBusy = false;
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

        public void ConnectBtn()
        {
            try
            {
                PublishStatusEvent("Try to connect to server ...");

                // Disconnect surrent connection
                if (Connected && m_pMarketDataClient != null)
                    if (m_pMarketDataClient.State == System.ServiceModel.CommunicationState.Opened)
                    {
                        //Log("Closing connection...");
                        m_pMarketDataClient.Close();
                    }
            }
            catch (Exception)
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
            catch (Exception)
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

            if (Connected)
            {
                PublishStatusEvent("Connected to server ...");
            }
        }

        public void GetDataSourceList()
        {
            var list = m_pMarketDataClient.GetDataSourceList();
            MyCollectionItems = CollectionViewSource.GetDefaultView(list);
        }

        public void GetNewsFeed()
        {
            ConnectedTwitter = true;
            m_pMarketDataClient.GetRandomTweet(Keyword);
        }        

        #endregion

        #region Callbacks called by Server

        public void OnSendTickUpdate(int param)
        {
            var serverResponse = String.Format("The number of items returned by Server is '{0}'", param);
            //_windowManager.ShowDialog(new DialogViewModel()
            //{
            //    Text = serverResponse,
            //    DisplayName = "Callback from Server",
            //    NotificationType = NotificationType.Info
            //});

            PublishStatusEvent(serverResponse);
            ServerResponseItems.Add(serverResponse);
        }

        public void OnPullRandomTweet(Tweet t)
        {
            ServerResponseTweets.Add(t);
        }

        #endregion
    }
}
