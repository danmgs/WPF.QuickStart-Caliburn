using Caliburn.Micro;
using System.ComponentModel.Composition;
using System.Windows;
using WPF.QuickStart.UI.Events;
using WPF.QuickStart.UI.ViewModels;
using WPF.QuickStart.UI.ViewModels.ClientServer;
using WPF.QuickStart.UI.ViewModels.Twitter;
using WPF.QuickStart.UI.ViewModels.Yahoo;
namespace WPF.QuickStart.UI.ViewModels
{
    [Export(typeof(IShell))]
    public class ShellViewModel : Conductor<Screen>, IShell, IHandle<StatusEvent>
    {
        #region Fields

        protected static readonly ILog Logger = LogManager.GetLog(typeof(ShellViewModel));
        private readonly IEventAggregator _eventAgg;
        private IWindowManager _windowManager;

        #endregion

        #region Constructor 

        [ImportingConstructor()]
        public ShellViewModel(IEventAggregator eventAgg, IWindowManager windowManager)
        {
            base.DisplayName = AppSettings.Application.Name;
            StatusBarContent = "Application started ... ";

            _eventAgg = eventAgg;
            _eventAgg.Subscribe(this);
            _windowManager = windowManager;
        }

        #endregion Constructor

        #region Properties

        string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyOfPropertyChange(() => Name);
                NotifyOfPropertyChange(() => CanSayHello);
            }
        }

        string customLbl;

        public string CustomLbl
        {
            get { return "Test de CustomLbl "; }
            set
            {
                customLbl = value;
                NotifyOfPropertyChange(() => CustomLbl);
            }
        }

        string _statusBarContent;

        public string StatusBarContent
        {
            get { return _statusBarContent; }
            set
            {
                _statusBarContent = value;
                this.NotifyOfPropertyChange(() => StatusBarContent);
            }
        }

        #endregion

        #region Methods

        protected override void OnInitialize()
        {
            base.OnInitialize();
            ShowYahooMultiTabsScreen("Twitter");
        }

        public bool CanSayHello
        {
            get { return !string.IsNullOrWhiteSpace(Name); }
        }

        public void ShowTwitterMultiTabsScreen(string titleParam)
        {
            ActivateItem(new ChildTabTwitterViewModel(titleParam, _eventAgg, _windowManager));
        }

        public void ShowYahooMultiTabsScreen(string titleParam)
        {
            ActivateItem(new ChildTabYahooViewModel(titleParam, _eventAgg, _windowManager));
        }

        public void ShowClientServerMultiTabsScreen(string titleParam)
        {
            ActivateItem(new ChildViewModel(titleParam, _eventAgg, _windowManager));
        }

        public void Handle(StatusEvent status)
        {
            StatusBarContent = string.Format("Status : {0} ", status.Content);
        }
        
        #endregion
    }
}