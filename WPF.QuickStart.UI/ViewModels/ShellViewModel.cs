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
using WPF.QuickStart.UI.ViewModels.WinOs;
using WPF.QuickStart.UI.Utils.Enum;
using WPF.QuickStart.UI.Views.Common;

namespace WPF.QuickStart.UI.ViewModels
{
    [Export(typeof(IShell))]
    public class ShellViewModel : Conductor<Screen>, IShell, IHandle<StatusEvent>, IHandle<ActivateScreenEvent>
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

        bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                this.NotifyOfPropertyChange(() => IsLoading);
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
            ShowWindowsOsMultiTabsScreen("Windows8");
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

        public void ShowWindowsOsMultiTabsScreen(string content)
        {
            ActivateItem(new WinOsViewModel(content, _eventAgg, _windowManager));
        }        

        #endregion

        #region Events Handler

        public void Handle(StatusEvent status)
        {
            StatusBarContent = string.Format("Status : {0} ", status.Content);
            IsLoading = status.IsLoading;
        }

        public void Handle(ActivateScreenEvent ev)
        {
            switch (ev.TypeView)
            {
                case TypeView.Twitter:
                    ActivateItem(new ChildTabTwitterViewModel(TypeView.Twitter.ToString(), _eventAgg, _windowManager));
                    break;
                case TypeView.ClientServer:
                    ActivateItem(new ChildViewModel(TypeView.ClientServer.ToString(), _eventAgg, _windowManager));
                    break;
                case TypeView.Yahoo:
                    ActivateItem(new ChildTabYahooViewModel(TypeView.Yahoo.ToString(), _eventAgg, _windowManager));
                    break;
            }
        }

        #endregion

        #region Menu Commands

        public void OpenBrowser(string url)
        {
            System.Diagnostics.Process.Start(url);
        }

        public void ShowSettings()
        {
            IsSettingsFlyoutOpen = true;
        }

        #endregion
    }
}