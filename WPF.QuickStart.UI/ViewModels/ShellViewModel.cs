using Caliburn.Micro;
using MahApps.Metro;
using System.ComponentModel.Composition;
using System.Windows;
using WPF.QuickStart.UI.Events;
using WPF.QuickStart.UI.Utils.Themes;
using WPF.QuickStart.UI.ViewModels;
using WPF.QuickStart.UI.ViewModels.ClientServer;
using WPF.QuickStart.UI.ViewModels.Twitter;
using WPF.QuickStart.UI.ViewModels.Yahoo;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Media;

namespace WPF.QuickStart.UI.ViewModels
{
    [Export(typeof(IShell))]
    public class ShellViewModel : Conductor<Screen>, IShell, IHandle<StatusEvent>
    {
        #region Fields

        protected static readonly ILog Logger = LogManager.GetLog(typeof(ShellViewModel));
        private readonly IEventAggregator _eventAgg;
        private IWindowManager _windowManager;

        public List<AccentColorMenuData> AccentColors { get; set; }
        public List<AppThemeMenuData> AppThemes { get; set; }

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

            // create accent color menu items for the demo
            this.AccentColors = ThemeManager.Accents
                                            .Select(a => new AccentColorMenuData() { Name = a.Name, ColorBrush = a.Resources["AccentColorBrush"] as Brush })
                                            .ToList();

            // create metro theme color menu items for the demo
            this.AppThemes = ThemeManager.AppThemes
                                           .Select(a => new AppThemeMenuData() { Name = a.Name, BorderColorBrush = a.Resources["BlackColorBrush"] as Brush, ColorBrush = a.Resources["WhiteColorBrush"] as Brush })
                                           .ToList();
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


        bool _isSettingsFlyoutOpen;

        public bool IsSettingsFlyoutOpen
        {
            get { return _isSettingsFlyoutOpen; }
            set
            {
                _isSettingsFlyoutOpen = value;
                this.NotifyOfPropertyChange(() => IsSettingsFlyoutOpen);
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

        public void ShowTwitterMultiTabsScreen(string content)
        {
            ActivateItem(new ChildTabTwitterViewModel(content, _eventAgg, _windowManager));
        }

        public void ShowYahooMultiTabsScreen(string content)
        {
            ActivateItem(new ChildTabYahooViewModel(content, _eventAgg, _windowManager));
        }

        public void ShowClientServerMultiTabsScreen(string content)
        {
            ActivateItem(new ChildViewModel(content, _eventAgg, _windowManager));
        }

        public void Handle(StatusEvent status)
        {
            StatusBarContent = string.Format("Status : {0} ", status.Content);
        }

        #endregion

        #region Menu Commands

        public void GoGitHub()
        {
            System.Diagnostics.Process.Start("https://github.com/danmgs/WPF.QuickStart-Caliburn");
        }

        public void GoYahoo()
        {
            System.Diagnostics.Process.Start("https://yahoo.com");
        }

        public void GoTwitter()
        {
            System.Diagnostics.Process.Start("https://twitter.com");
        }

        public void ShowSettings()
        {
            IsSettingsFlyoutOpen = true;
            //this.ToggleFlyout(0);
        }

        #endregion
    }
}