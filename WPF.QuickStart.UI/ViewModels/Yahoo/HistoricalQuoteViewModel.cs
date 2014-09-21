using Caliburn.Micro;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WPF.Quickstart.Model.Yahoo;
using WPF.QuickStart.UI.ViewModels.Common;
using WPF.QuickStart.UI.ViewModels.Common.Dialog;
using WPFQuickstart.Core.Utils;

namespace WPF.QuickStart.UI.ViewModels.Yahoo
{
    public class HistoricalQuoteViewModel : ExtendedScreen
    {
        private const string _dateformat = "yyyy-MM-dd";

        public ObservableCollection<TestClass> Errors { get; private set; }

        public ObservableCollection<DataPoint> _chartQuotePointsList;

        public ObservableCollection<DataPoint> ChartQuotePointsList
        {
            get
            {
                return _chartQuotePointsList;
            }
            set
            {
                _chartQuotePointsList = value;
                this.NotifyOfPropertyChange(() => ChartQuotePointsList);
            }
        }

        private object selectedItem = null;
        public object SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                // selected item has changed
                selectedItem = value;
            }
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
                this.NotifyOfPropertyChange(() => CanLoadHistoricalData);                
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
                this.NotifyOfPropertyChange(() => CanLoadHistoricalData);
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
                this.NotifyOfPropertyChange(() => CanLoadHistoricalData);
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

        private PriceTypeOptions _priceTypeOptions;

        public PriceTypeOptions PriceTypeOptions
        {
            get { return _priceTypeOptions; }
            set
            {
                _priceTypeOptions = value;
                this.NotifyOfPropertyChange(() => PriceTypeOptions);
                LoadHistoricalData();
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
            PublishStatusEvent(string.Format("Load {0} ...", DisplayName));

            StartDate = DateTime.Today;
            EndDate = DateTime.Today;

            PriceTypeOptions = PriceTypeOptions.Open;

            FillForCharts();
        }

        private void LoadChartQuotesModel(List<HistoricalQuotationResults.Quote> quotes)
        {
            var list = new List<DataPoint>(); 
            quotes.ForEach(q =>
            {
                double yPrice;
                if (PriceTypeOptions == PriceTypeOptions.Open)
                {
                    yPrice = Convert.ToDouble(q.Open.Replace(".", ","));
                }
                else if (PriceTypeOptions == PriceTypeOptions.Close)
                {
                    yPrice = Convert.ToDouble(q.Close.Replace(".", ","));
                }
                else if (PriceTypeOptions == PriceTypeOptions.High)
                {
                    yPrice = Convert.ToDouble(q.High.Replace(".", ","));
                }
                else if (PriceTypeOptions == PriceTypeOptions.Low)
                {
                    yPrice = Convert.ToDouble(q.Low.Replace(".", ","));
                }
                else
                {
                    throw new NotImplementedException(string.Format("Price type '{0}' is not managed.", PriceTypeOptions));
                }

                DateTime qDate = DateTime.ParseExact(q.Date, "yyyy-MM-dd", null);
                list.Add(new DataPoint(DateTimeAxis.ToDouble(qDate), yPrice));
            });

            ChartQuotePointsList = new ObservableCollection<DataPoint>(list);
        }

        public void LoadHistoricalData()
        {
            if (!CanLoadHistoricalData)
            {
                return;
            }

            PublishStatusEvent("Begin Load historical data");

            using (var webClient = new System.Net.WebClient())
            {
                var url = string.Format(@"http://query.yahooapis.com/v1/public/yql?q=select * from yahoo.finance.historicaldata where symbol = %22{0}%22 and startDate = %22{1}%22 and endDate = %22{2}%22&diagnostics=true&env=store://datatables.org/alltableswithkeys&format=json", SecurityCode, StartDate.ToString(_dateformat), EndDate.ToString(_dateformat));
                var json = webClient.DownloadString(url);
                HistoricalQuotationResults.RootObject histoQuotationRes = Newtonsoft.Json.JsonConvert.DeserializeObject<HistoricalQuotationResults.RootObject>(json);
                if (histoQuotationRes.query.results != null)
                {
                    MyCollectionItems = CollectionViewSource.GetDefaultView(histoQuotationRes.query.results.quote);
                    LoadChartQuotesModel(histoQuotationRes.query.results.quote);
                    PublishStatusEvent("End Load historical data");
                }
                else
                {
                    var message = "Wrong security code or no data in the specified timeline ! ...";
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

        public bool CanLoadHistoricalData
        {
            get { return !string.IsNullOrWhiteSpace(SecurityCode) && (StartDate <= EndDate) && (EndDate <= DateTime.Today); }
        }

        private void FillForCharts()
        {
            Errors = new ObservableCollection<TestClass>();
                        Errors.Add(new TestClass() { Category = "Globalization", Number = 75 });
                        Errors.Add(new TestClass() { Category = "Features", Number = 2 });
                        Errors.Add(new TestClass() { Category = "ContentTypes", Number = 12 });
                        Errors.Add(new TestClass() { Category = "Correctness", Number = 83});
                        Errors.Add(new TestClass() { Category = "Best Practices", Number = 29 });
        }
    }

    // class which represent a data point in the chart
    public class TestClass
    {
        public string Category { get; set; }

        public int Number { get; set; }
    }
}
