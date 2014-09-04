using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF.QuickStart.UI.ViewModels
{
    public class ChildViewModel : Screen
    {
        public ChildViewModel(string displayName)
        {
            DisplayName = displayName;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            MessageBox.Show(string.Format("Init: {0}", DisplayName));
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);

            if (close)
            {
                MessageBox.Show(string.Format("Closed: {0}", DisplayName));
            }
        }
    }
}
