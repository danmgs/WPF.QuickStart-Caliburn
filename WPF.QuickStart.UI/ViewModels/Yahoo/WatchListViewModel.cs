using Caliburn.Micro;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Quickstart.Model.Yahoo;
using WPF.QuickStart.UI.ViewModels.Common;
using WPF.QuickStart.UI.ViewModels.Common.Dialog;

namespace WPF.QuickStart.UI.ViewModels.Yahoo
{
    public class WatchListViewModel : ExtendedScreen
    {
        public WatchListViewModel(string displayName, IEventAggregator eventAgg, IWindowManager windowManager)
			: base(eventAgg, windowManager)
		{
            base.DisplayName = displayName;
        }

        public string _securityCode;
        public string SecurityCode
        {
            get
            {
                return _securityCode;
            }
            set
            {
                _securityCode = value;
                this.NotifyOfPropertyChange(() => SecurityCode);
                this.NotifyOfPropertyChange(() => CanAddSingleStock);
            }
        }

        private BindableCollection<Quote> _quotes;
        public BindableCollection<Quote> Quotes
        {
            get
            {
                if (_quotes == null)
                {
                    _quotes = new BindableCollection<Quote>();
                }
                return _quotes;
            }
            set
            {
                _quotes = value;
                this.NotifyOfPropertyChange(() => Quotes);
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            PublishStatusEvent(string.Format("Load {0} ...", DisplayName));
            LoadDefaultStocks();
        }

        public void AddSingleStock()
        {
            try
            {
                using (var webClient = new System.Net.WebClient())
                {
                    var url = @"http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20=%22{0}%22&env=http%3A%2F%2Fdatatables.org%2Falltables.env&format=json";
                    var json = webClient.DownloadString(string.Format(url, SecurityCode));
                    QuotationSingleResult.RootObject quotationRes = Newtonsoft.Json.JsonConvert.DeserializeObject<QuotationSingleResult.RootObject>(json);
                    if (Quotes != null && quotationRes != null)
                    {
                        Quotes.Add(quotationRes.query.results.quote);
                    }
                }
            }
            catch(JsonSerializationException ex)
            {
                var message = "Wrong security code or no data for security code .....";
                _windowManager.ShowDialog(new DialogViewModel()
                {
                    Text = String.Format(message),
                    DisplayName = message,
                    NotificationType = NotificationType.Error
                });
                PublishStatusEvent(message);
            }
        }

        public bool CanAddSingleStock
        {
            get { return !string.IsNullOrWhiteSpace(SecurityCode); }
        }

        private void LoadDefaultStocks()
        {
            using (var webClient = new System.Net.WebClient())
            {
                var url = @"http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20%28%22YHOO%22%2C%22AAPL%22%2C%22GOOG%22%2C%22MSFT%22%29%0A%09%09&env=http%3A%2F%2Fdatatables.org%2Falltables.env&format=json";
                var json = webClient.DownloadString(url);

                QuotationResults.RootObject quotationRes = Newtonsoft.Json.JsonConvert.DeserializeObject<QuotationResults.RootObject>(json);
                Quotes = new BindableCollection<Quote>(quotationRes.query.results.quote);
            }
        }
    }
}
