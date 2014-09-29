using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WPF.Quickstart.Model.NYTimes;
using WPF.QuickStart.UI.ViewModels.Common;
using WPF.QuickStart.UI.ViewModels.Common.Dialog;
using WPFQuickstart.Core.Utils.Api.NYTimes;

namespace WPF.QuickStart.UI.ViewModels.NYTimes
{
    public class NewsViewModel : ExtendedScreen
    {
        public NewsViewModel(string displayName, IEventAggregator eventAgg, IWindowManager windowManager)
            : base(displayName, eventAgg, windowManager)
        {
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            PublishStatusEvent(string.Format("Load {0} ...", DisplayName));
            Load();
        }

        private BindableCollection<NYTimesResults.Doc> news = new BindableCollection<NYTimesResults.Doc>();

        public BindableCollection<NYTimesResults.Doc> News
        {
            get
            {
                return news;
            }

            set
            {
                news = value;
                this.NotifyOfPropertyChange(() => News);
            }
        }

        #region Methods

        public void Load()
        {
            PublishStatusEvent(string.Format("Begin loading NYTimes news"), true);
            IsBusy = true;
            var context = TaskScheduler.FromCurrentSynchronizationContext();
            var newsTmpList = new List<NYTimesResults.Doc>();

            try
            {
                Task.Run(() =>
                {
                    News.Clear();
                    var nYTimesHelper = new NYTimesHelper(AppSettings.Nytimes.Articles.Search.ApiKey);
                    newsTmpList = nYTimesHelper.SearchArticles("new+york+times", "oldest");
                    News.AddRange(newsTmpList);
                }).ContinueWith(previousTask =>
                {
                    PublishStatusEvent(string.Format("End loading NYTimes news"));
                    IsBusy = false;
                }, context);
            }
            catch (WebException ex)
            {
                IsBusy = false;
                var message = string.Format("WebException : {0}", ex.Message);
                _windowManager.ShowDialog(new DialogViewModel()
                {
                    Text = String.Format(message),
                    DisplayName = "WebException",
                    NotificationType = NotificationType.Error
                });
                PublishStatusEvent(message);
            }
        }

        #endregion
    }
}
