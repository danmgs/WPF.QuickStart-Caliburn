using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF.QuickStart.UI.ViewModels
{
    public class ChildTabViewModel : Conductor<IScreen>.Collection.OneActive
    {
        [ImportingConstructor()]
        public ChildTabViewModel(string displayName, IEventAggregator eventAgg, IWindowManager windowManager)
        {
            DisplayName = displayName;
            IsEnabled = true;
            Items.Add(new ChildViewModel("Tab 1", eventAgg, windowManager));
            Items.Add(new ChildViewModel("Tab 2", eventAgg, windowManager));

            //var twitterViewModel = new TwitterViewModel("Tab 3", eventAgg, windowManager);
            //var twitterViewModel = IoC.Get<ITwitterViewModel>();
            Items.Add(TwitterViewModel);
            ActivateItem(TwitterViewModel);
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
