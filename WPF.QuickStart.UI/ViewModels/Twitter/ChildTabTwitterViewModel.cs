using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF.QuickStart.UI.ViewModels.Twitter
{
    public class ChildTabTwitterViewModel : Conductor<IScreen>.Collection.OneActive
    {
        [ImportingConstructor()]
        public ChildTabTwitterViewModel(string displayName, IEventAggregator eventAgg, IWindowManager windowManager)
        {
            DisplayName = displayName;
            IsEnabled = true;
            Items.Add(new TwitterSummaryViewModel("Financial News", eventAgg, windowManager));
            Items.Add(new TwitterViewModel("View tweets by", eventAgg, windowManager));

            //Items.Add(TwitterViewModel);
            //ActivateItem(TwitterViewModel);
        }

        [Import]
        ITwitterViewModel TwitterViewModel { get; set;  }

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
