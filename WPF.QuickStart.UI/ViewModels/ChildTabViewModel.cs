using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF.QuickStart.UI.ViewModels
{
    public class ChildTabViewModel : Conductor<IScreen>.Collection.OneActive
    {
        public ChildTabViewModel(string displayName)
        {
            DisplayName = displayName;
            IsEnabled = true;
            Items.Add(new ChildViewModel("Tab 1", null, null)); // TODO add eventAgg + windowManager
            var tab2ViewModel = new ChildViewModel("Tab 2",  null, null); // TODO add eventAgg + windowManager
            Items.Add(tab2ViewModel);
            ActivateItem(tab2ViewModel);
        }

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

            MessageBox.Show(string.Format("Init: '{0}'", DisplayName));
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);

            if (close)
            {
                MessageBox.Show(string.Format("Closed: '{0}'", DisplayName));
            }
        }
    }
}
