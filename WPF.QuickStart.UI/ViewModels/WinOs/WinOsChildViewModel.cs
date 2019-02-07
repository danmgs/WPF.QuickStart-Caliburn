using Caliburn.Micro;
using System.ComponentModel.Composition;
using WPF.QuickStart.UI.Events;
using WPF.QuickStart.UI.Utils.Enum;

namespace WPF.QuickStart.UI.ViewModels.WinOs
{
    [Export(typeof(IWinOsChildViewModel))]
    public class WinOsChildViewModel : IWinOsChildViewModel
    {
        #region Fields

        protected static readonly ILog Logger = LogManager.GetLog(typeof(WinOsChildViewModel));
        
        public readonly IEventAggregator _eventAgg;
        
        public IWindowManager _windowManager;

        #endregion

        [ImportingConstructor]
        public WinOsChildViewModel(IEventAggregator eventAgg, IWindowManager windowManager)
        {
            _eventAgg = eventAgg;
            _windowManager = windowManager;
        }

        #region Methods

        public void GoGmail()
        {
            System.Diagnostics.Process.Start("https://mail.google.com");
        }

        public void ShowTwitterMultiTabsScreen(string content)
        {
            PublishActivateScreenEvent(TypeView.Twitter);
        }

        public void ShowYahooMultiTabsScreen(string content)
        {
            PublishActivateScreenEvent(TypeView.Yahoo);
        }

        public void ShowClientServerMultiTabsScreen(string content)
        {
            PublishActivateScreenEvent(TypeView.ClientServer);
        }

        public void ShowNYTimesMultiTabsScreen(string content)
        {
            PublishActivateScreenEvent(TypeView.NYTimes);
        }
        
        protected virtual void PublishActivateScreenEvent(TypeView typeView)
        {
            _eventAgg.PublishOnUIThread(new ActivateScreenEvent() { TypeView = typeView });
        }

        #endregion
    }

    public interface IWinOsChildViewModel { }
}
