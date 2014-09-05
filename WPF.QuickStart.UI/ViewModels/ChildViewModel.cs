using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using WPF.QuickStart.UI.Events;
using WPF.QuickStart.UI.ViewModels.Common;

namespace WPF.QuickStart.UI.ViewModels
{
    public class ChildViewModel : ExtendedScreen
    {
		public ChildViewModel(string displayName, IEventAggregator eventAgg, IWindowManager windowManager)
			: base(eventAgg, windowManager)
		{
            base.DisplayName = displayName;
        }

        private void PublishStatusEvent(string content)
        {
            var le = new StatusEvent() 
            {
                Content = content
            };
            _eventAgg.Publish(le, s => { Console.Write(""); });
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            PublishStatusEvent(string.Format("Load ChildViewModel {0} ...", DisplayName));

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

        public void GenerateItems(int elementsCount)
        {
            var myElements = new List<object>();
            for (var i = 0; i < elementsCount; i++)
            {
                myElements.Add(new { index = i, label = string.Concat("elem" , i) });
            }
            MyCollectionItems = CollectionViewSource.GetDefaultView(myElements);
            PublishStatusEvent(string.Format("Load {0} elements ...", elementsCount));
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
