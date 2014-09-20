using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WPF.Quickstart.Model.Yahoo;
using WPF.QuickStart.UI.ViewModels.Common;
using WPF.QuickStart.UI.ViewModels.Common.Dialog;

namespace WPF.QuickStart.UI.ViewModels
{
    public class HistoricalQuoteViewModel : ExtendedScreen
    {
        private const string _dateformat = "yyyy-MM-dd";

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
                this.NotifyOfPropertyChange(() => CanLoad);                
            }
        }

        public DateTime _startDate;
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
                this.NotifyOfPropertyChange(() => StartDate);
                this.NotifyOfPropertyChange(() => CanLoad);
            }
        }

        public DateTime _endDate;
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
                this.NotifyOfPropertyChange(() => EndDate);
                this.NotifyOfPropertyChange(() => CanLoad);
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

        public HistoricalQuoteViewModel(string displayName, IEventAggregator eventAgg, IWindowManager windowManager)
			: base(eventAgg, windowManager)
		{
            base.DisplayName = displayName;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            PublishStatusEvent(string.Format("Load HistoricalQuoteViewModel {0} ...", DisplayName));

            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
        }

        public void Load()
        {
            using (var webClient = new System.Net.WebClient())
            {
                var url = string.Format(@"http://query.yahooapis.com/v1/public/yql?q=select * from yahoo.finance.historicaldata where symbol = %22{0}%22 and startDate = %22{1}%22 and endDate = %22{2}%22&diagnostics=true&env=store://datatables.org/alltableswithkeys&format=json", SecurityCode, StartDate.ToString(_dateformat), EndDate.ToString(_dateformat));
                var json = webClient.DownloadString(url);
                HistoricalQuotationResults.RootObject histoQuotationRes = Newtonsoft.Json.JsonConvert.DeserializeObject<HistoricalQuotationResults.RootObject>(json);
                if (histoQuotationRes.query.results != null)
                {
                    MyCollectionItems = CollectionViewSource.GetDefaultView(histoQuotationRes.query.results.quote);
                }
                else
                {
                    var message = "Wrong security code or no data in the specified timeline ! .....";
                    _windowManager.ShowDialog(new DialogViewModel()
                    {
                        Text = String.Format(message),
                        DisplayName = message,
                        NotificationType = NotificationType.Error
                    });
                    PublishStatusEvent(message);
                }
            }
        }

        public bool CanLoad
        {
            get { return !string.IsNullOrWhiteSpace(SecurityCode) && (StartDate <= EndDate) && (EndDate <= DateTime.Today); } // Add date start / end constraints
        }
    }
}
