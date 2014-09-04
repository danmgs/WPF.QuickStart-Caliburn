using Caliburn.Micro;
using System.Windows;
using WPF.QuickStart.UI.ViewModels;
namespace WPF.QuickStart.UI.ViewModels
{
    public class ShellViewModel : Conductor<Screen>, IShell
    {

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
            ActivateItem(new ChildViewModel("Red"));
        }
 
        public void ShowGreenScreen()
        {
            ActivateItem(new ChildViewModel("Green"));
        }
 
        public void ShowBlueScreen()
        {
            ActivateItem(new ChildViewModel("Blue"));
        }    
    }
}