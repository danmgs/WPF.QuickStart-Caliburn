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
using WPF.QuickStart.UI.ViewModels.Common.Dialog;

namespace WPF.QuickStart.UI.ViewModels
{
    public class ChildViewModel : ExtendedScreen
    {
		public ChildViewModel(string displayName, IEventAggregator eventAgg, IWindowManager windowManager)
			: base(eventAgg, windowManager)
		{
            base.DisplayName = displayName;
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

        private async Task<List<object>> LongRetrieveMockElements(int count)
        {
            PublishStatusEvent(string.Format("Loading {0} elements ...", count));
            var context = TaskScheduler.FromCurrentSynchronizationContext();
            var listElements = new List<object>();
            Enabled = false;

            await Task.Delay(5000);

            await Task.Run(() =>
            {
                for (var i = 0; i < count; i++)
                {
                    listElements.Add(new { index = i, label = string.Concat("elem", i) });
                }
                MyCollectionItems = CollectionViewSource.GetDefaultView(listElements);
            })
            .ContinueWith(previousTask =>
                {
                    PublishStatusEvent(string.Format("Finish loading {0} elements !", count));
                    Enabled = true;
                    // Add Caliburn Logger.Error ...
                }, context);

            return listElements;
        }

        public async void GenerateItems(string elementsCount)
        {
            int count;
            bool res = Int32.TryParse(elementsCount, out count);

            if (res)
            {
                await LongRetrieveMockElements(count);
            }
            else
            {
                var message = "Wrong input. Must be a number ...";
                _windowManager.ShowDialog(new DialogViewModel()
                {
                    Text = String.Format(message),
                    DisplayName = "Wrong Input",
                    NotificationType = NotificationType.Error
                });
                PublishStatusEvent(message);
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
