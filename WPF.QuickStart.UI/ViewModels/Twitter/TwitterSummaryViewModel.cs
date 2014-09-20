using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Quickstart.Model.Twitter;
using WPF.QuickStart.UI.ViewModels.Common;
using WPF.QuickStart.UI.Utils.Twitter;
using WPF.QuickStart.UI.Utils;
using System.Windows.Controls;
using System.Net;
using WPF.QuickStart.UI.ViewModels.Common.Dialog;
using System.ComponentModel.Composition;
using WPF.Quickstart.Data.Mocks;

namespace WPF.QuickStart.UI.ViewModels.Twitter
{
    public class TwitterSummaryViewModel : ExtendedScreen
    {
        List<string> TwitterScreenNames { get; set; }

        public TwitterSummaryViewModel(string displayName, IEventAggregator eventAgg, IWindowManager windowManager)
            : base(eventAgg, windowManager)
        {
            base.DisplayName = displayName;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            PublishStatusEvent(string.Format("Load {0} ...", DisplayName));

            TwitterScreenNames = TwitterRepository.GetSummaryScreenNames();
            Load();
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                NotifyOfPropertyChange(() => IsBusy);
            }
        }

        private BindableCollection<Tweet> tweets = new BindableCollection<Tweet>();

        public BindableCollection<Tweet> Tweets
        {
            get
            {
                return tweets;
            }

            set
            {
                tweets = value;
                this.NotifyOfPropertyChange(() => Tweets);
            }
        }

        #region Methods

        public void Load()
        {
            PublishStatusEvent(string.Format("Begin loading tweets ..."));
            IsBusy = true;
            var context = TaskScheduler.FromCurrentSynchronizationContext();
            List<Tweet> tmpTweetList = new List<Tweet>();

            try
            {
                Task.Run(() =>
                    {
                        Tweets.Clear();
                        TwitterScreenNames.ForEach(
                            screenname =>
                            {
                                var lastTweet = TwitterHelperWrapper.GetLastTweet(screenname);
                                tmpTweetList.Add(lastTweet);
                            });
                        Tweets.AddRange(tmpTweetList);
                    }).ContinueWith(previousTask =>
                    {
                        PublishStatusEvent(string.Format("End loading tweets ..."));
                        IsBusy = false;
                    }, context);
            }
            catch (WebException ex)
            {
                var message = string.Format("WebException : {0}", ex.Message);
                _windowManager.ShowDialog(new DialogViewModel()
                {
                    Text = String.Format(message),
                    DisplayName = "WebException",
                    NotificationType = NotificationType.Error
                });
                PublishStatusEvent(message);
                IsBusy = false;
            }
        }

        #endregion
    }
}
