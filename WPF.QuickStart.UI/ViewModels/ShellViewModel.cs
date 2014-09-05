using Caliburn.Micro;
using System.Windows;
using WPF.QuickStart.UI.Events;
using WPF.QuickStart.UI.ViewModels;
namespace WPF.QuickStart.UI.ViewModels
{
    public class ShellViewModel : Conductor<Screen>, IShell, IHandle<StatusEvent>
    {
        #region Fields

        protected static readonly ILog Logger = LogManager.GetLog(typeof(ShellViewModel));
        private readonly IEventAggregator _eventAgg;
        private IWindowManager _windowManager;

        #endregion

        #region Constructor 

        public ShellViewModel(IEventAggregator eventAgg, IWindowManager windowManager)
        {
            base.DisplayName = AppSettings.Application.Name;

            _eventAgg = new SimpleContainer().GetInstance<IEventAggregator>();
            _eventAgg.Subscribe(this);
            _windowManager = windowManager;
        }

        #endregion Constructor

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

        public bool CanSayHello
        {
            get { return !string.IsNullOrWhiteSpace(Name); }
        }

        public void SayHello()
        {
            MessageBox.Show(string.Format("Hello {0}!", Name)); //Don't do this in real life :)
        }

        public void ShowMultiTabsScreen()
        {
            ActivateItem(new ChildTabViewModel("Tab Panel"));
        }

        public void ShowRedScreen()
        {
            ActivateItem(new ChildViewModel("Red", _eventAgg, _windowManager));
        }
 
        public void ShowGreenScreen()
        {
            ActivateItem(new ChildViewModel("Green", _eventAgg, _windowManager));
        }
 
        public void ShowBlueScreen()
        {
            ActivateItem(new ChildViewModel("Blue", _eventAgg, _windowManager));
        }

        public void Handle(StatusEvent status)
        {
            MessageBox.Show(string.Format("Status : {0} ...", status.Content)); 
        }
    }
}