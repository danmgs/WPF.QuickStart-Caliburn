using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Quickstart.Model.Twitter;
using WPF.QuickStart.UI.ViewModels.Common;
using WPF.QuickStart.UI.Utils.Api.Twitter;
using WPF.QuickStart.UI.Utils;
using System.Windows.Controls;
using System.Net;
using WPF.QuickStart.UI.ViewModels.Common.Dialog;
using System.ComponentModel.Composition;
using WPF.Quickstart.Data.Mocks;
using WPF.QuickStart.UI.Views.Common;
using System.Windows.Input;
using System.Windows.Controls.Primitives;

namespace WPF.QuickStart.UI.ViewModels.Twitter
{
    public class TwitterSummaryViewModel : ExtendedScreen
    {
        List<string> TwitterScreenNames { get; set; }

        public TwitterSummaryViewModel(string displayName, IEventAggregator eventAgg, IWindowManager windowManager)
            : base(displayName, eventAgg, windowManager)
        {
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            PublishStatusEvent(string.Format("Load {0} ...", DisplayName));

            TwitterScreenNames = TwitterRepository.GetSummaryScreenNames();
            Load();
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
            PublishStatusEvent(string.Format("Begin loading tweets ..."), true);
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
                        PublishStatusEvent(string.Format("End loading tweets"));
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

        public void ShowTwitterProfile(object sender, MouseButtonEventArgs e)
        {
            var tweet = sender as Tweet;
            if (tweet != null && tweet.ScreenName != null)
            {
                System.Diagnostics.Process.Start(string.Format("https://twitter.com/{0}", tweet.ScreenName));
            }
        }

        #endregion
    }
}
