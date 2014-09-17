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

namespace WPF.QuickStart.UI.ViewModels
{
    [Export(typeof(ITwitterViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TwitterViewModel : ExtendedScreen, ITwitterViewModel
    {
        [ImportingConstructor()]
        public TwitterViewModel(string displayName, IEventAggregator eventAgg, IWindowManager windowManager)
			: base(eventAgg, windowManager)
		{
            base.DisplayName = displayName;
            Count = 1;
        }

        string screenName;

        public string ScreenName
        {
            get { return screenName; }
            set
            {
                screenName = value;
                NotifyOfPropertyChange(() => ScreenName);
                NotifyOfPropertyChange(() => CanLoad);
            }
        }

        int count;

        public int Count
        {
            get { return count; }
            set
            {
                count = value;
                NotifyOfPropertyChange(() => Count);
                NotifyOfPropertyChange(() => CanLoad);
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

        bool showInfoPanel;

        public bool ShowInfoPanel
        {
            get { return showInfoPanel; }
            set
            {
                showInfoPanel = value;
                NotifyOfPropertyChange(() => ShowInfoPanel);
            }
        }    

        #region Methods

        public bool CanLoad
        {
            get { return !string.IsNullOrWhiteSpace(ScreenName) && Count > 0; }
        }

        public void CountSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
        }

        public void Load()
        {
            PublishStatusEvent(string.Format("Begin loading {0} tweets from profile '{1}'...", Count, ScreenName));

            //BNPPwarrants
            try
            {
                Tweets.Clear();
                var tweets = TwitterHelperWrapper.GetTweets(ScreenName, Count);
                Tweets = new BindableCollection<Tweet>(tweets);
                PublishStatusEvent(string.Format("End loading {0} tweets from profile '{1}'...", Count, ScreenName));
            }
            catch(WebException ex)
            {
                var message = string.Format("Profile '{0}' not found ... : {1}", ScreenName, ex.Message);
                _windowManager.ShowDialog(new DialogViewModel()
                {
                    Text = String.Format(message),
                    DisplayName = "Incorrect screen name",
                    NotificationType = NotificationType.Error
                });
                PublishStatusEvent(message);
            }            
        }

        //private ShowTwitterPanel(bool bShow)
        //{
        //    if (bShow)
        //    {
        //        ShowInfoPanel = true;
        //        ShowInfoPanel = true;
        //    }
            
        //}

        #endregion
    }
}
