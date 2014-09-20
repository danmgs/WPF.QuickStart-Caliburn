using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF.QuickStart.UI.ViewModels.Yahoo;

namespace WPF.QuickStart.UI.ViewModels.Twitter
{
    public class ChildTabYahooViewModel : Conductor<IScreen>.Collection.OneActive
    {
        [ImportingConstructor()]
        public ChildTabYahooViewModel(string displayName, IEventAggregator eventAgg, IWindowManager windowManager)
        {
            DisplayName = displayName;
            IsEnabled = true;
            Items.Add(new HistoricalQuoteViewModel("Historical Quotations", eventAgg, windowManager));
            Items.Add(new WatchListViewModel("Watch List", eventAgg, windowManager));            
        }

        [Import]
        ITwitterViewModel TwitterViewModel { get; set; }

        private bool _isEnabled;

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (value.Equals(_isEnabled)) return;
                _isEnabled = value;
                NotifyOfPropertyChange(() => IsEnabled);
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            //MessageBox.Show(string.Format("Init: '{0}'", DisplayName));
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);

            if (close)
            {
                //MessageBox.Show(string.Format("Closed: '{0}'", DisplayName));
            }
        }
    }
}
