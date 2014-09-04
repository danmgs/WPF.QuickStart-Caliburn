using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

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

            MessageBox.Show(string.Format("Init: '{0}'", DisplayName));

            IEnumerable<string> myElements = new List<string>(){ "elem1", "elem2" };
            MyCollectionItems = CollectionViewSource.GetDefaultView(myElements);
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);

            if (close)
            {
                MessageBox.Show(string.Format("Closed: '{0}'", DisplayName));
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
    }
}
