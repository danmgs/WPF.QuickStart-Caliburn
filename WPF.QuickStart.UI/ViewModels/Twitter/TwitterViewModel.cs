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
using System.ComponentModel;
using System.Windows.Data;
using WPF.QuickStart.UI.Views.Twitter;

namespace WPF.QuickStart.UI.ViewModels.Twitter
{
    [Export(typeof(ITwitterViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TwitterViewModel : ExtendedScreen, ITwitterViewModel
    {
        [ImportingConstructor()]
        public TwitterViewModel(string displayName, IEventAggregator eventAgg, IWindowManager windowManager)
            : base(displayName, eventAgg, windowManager)
		{
            
        }
        protected override void OnInitialize()
        {
            base.OnInitialize();
            PublishStatusEvent(string.Format("Load {0} ...", DisplayName));

            TwitterScreenNames = TwitterRepository.GetSummaryScreenNames();
            LoadCountElements();
        }

        protected override void OnActivate()
        {
            base.OnActivate();
        }

        ICollectionView _countElements;

        public ICollectionView CountElements
        {
            get { return _countElements; }
            set
            {
                _countElements = value;
                NotifyOfPropertyChange(() => CountElements);
                NotifyOfPropertyChange(() => CanLoad);
            }
        }

        List<string> _twitterScreenNames;

        public List<string> TwitterScreenNames
        {
            get { return _twitterScreenNames; }
            set
            {
                _twitterScreenNames = value;
                NotifyOfPropertyChange(() => ScreenName);
                NotifyOfPropertyChange(() => CanLoad);
            }
        }

        string _screenName;

        public string ScreenName
        {
            get { return _screenName; }
            set
            {
                _screenName = value;
                NotifyOfPropertyChange(() => ScreenName);
                NotifyOfPropertyChange(() => CanLoad);
            }
        }


        string _keyword;

        public string Keyword
        {
            get { return _keyword; }
            set
            {
                _keyword = value;
                NotifyOfPropertyChange(() => Keyword);
                NotifyOfPropertyChange(() => CanSearch);
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
        private void LoadCountElements()
        {
            IEnumerable<int> countElems = TwitterRepository.GetCountElementsForCombo();
            CountElements = CollectionViewSource.GetDefaultView(countElems);
        }

        public void OnCountSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
        }

        public void OnScreenNameSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
        }

        public bool CanLoad
        {
            get
            {
                int? selectedCountValue = CountElements.CurrentItem as int?;
                return !string.IsNullOrWhiteSpace(ScreenName) && (selectedCountValue.Value > 0);
            }
        }

        public void Load()
        {
            IsBusy = true;
            int? selectedCountValue = CountElements.CurrentItem as int?;
            PublishStatusEvent(string.Format("Begin loading {0} tweets from profile '{1}'...", selectedCountValue, ScreenName), true);
            var context = TaskScheduler.FromCurrentSynchronizationContext();

            try
            {
                Task.Run(() =>
                {
                    Tweets.Clear();
                    var tweets = TwitterHelperWrapper.GetTweets(ScreenName, selectedCountValue.Value);
                    Tweets = new BindableCollection<Tweet>(tweets);
                }).ContinueWith(previousTask =>
                {
                    PublishStatusEvent(string.Format("End loading {0} tweets from profile '{1}'", selectedCountValue, ScreenName));
                    ShowNotFoundTweet(Tweets);
                    IsBusy = false;
                }, context);
            }
            catch(WebException ex)
            {
                IsBusy = false;
                var message = string.Format("Profile '{0}' not found : {1}", ScreenName, ex.Message);
                _windowManager.ShowDialog(new DialogViewModel()
                {
                    Text = String.Format(message),
                    DisplayName = "Incorrect screen name",
                    NotificationType = NotificationType.Error
                });
                PublishStatusEvent(message);
            }            
        }

        public bool CanSearch
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Keyword);
            }
        }   

        public void Search()
        {
            IsBusy = true;
            PublishStatusEvent(string.Format("Begin search with keyword {0} ...", Keyword), true);
            var context = TaskScheduler.FromCurrentSynchronizationContext();

            try
            {
                Task.Run(() =>
                {
                    Tweets.Clear();
                    var tweets = TwitterHelperWrapper.SearchTweets(Keyword);
                    Tweets = new BindableCollection<Tweet>(tweets);
                }).ContinueWith(previousTask =>
                {
                    PublishStatusEvent(string.Format("End search with keyword {0}", Keyword));
                    IsBusy = false;
                    ShowNotFoundTweet(Tweets);
                }, context);
            }
            catch(WebException ex)
            {
                var message = string.Format("An error has occured : '{0}'", ex.Message);
                _windowManager.ShowDialog(new DialogViewModel()
                {
                    Text = String.Format(message),
                    DisplayName = "An error has occured",
                    NotificationType = NotificationType.Error
                });
                PublishStatusEvent(message);
                IsBusy = false;
            }
        }

        public void ShowAdvancedSearch()
        {
            var w = new TwitterSearchInterop();
            w.Browser. Navigate(new Uri(string.Format("https://twitter.com/search?q={0}", Keyword)));
            w.Show();
        }

        private void ShowNotFoundTweet(IEnumerable<Tweet> tweets)
        { 
            if (tweets.Count() == 0)
            {
                var message = "No tweets founds";
                _windowManager.ShowDialog(new DialogViewModel()
                {
                    Text = String.Format(message),
                    DisplayName = message,
                    NotificationType = NotificationType.Info
                });
                PublishStatusEvent(message);
            }
        }

        #endregion
    }
}
