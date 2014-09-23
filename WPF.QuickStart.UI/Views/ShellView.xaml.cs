using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro;
using WPF.QuickStart.UI.ViewModels.Flyouts;
using Caliburn.Micro;

namespace WPF.QuickStart.UI.Views
{

    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ShellView : MetroWindow
    {
        //private readonly IObservableCollection<FlyoutBaseViewModel> flyouts =
        //    new BindableCollection<FlyoutBaseViewModel>();

        //public IObservableCollection<FlyoutBaseViewModel> Flyouts
        //{
        //    get
        //    {
        //        return this.flyouts;
        //    }
        //}

        public ShellView()
        {
            InitializeComponent();
            Title = "TITLE NOT WORKING PROGRAMMATICALLY, NEITHER IN VIEW XAML, NEITHER IN RESOURCES XAML ...";
            //LoadFlyouts();

            var theme = ThemeManager.DetectAppStyle(Application.Current);
            var appTheme = ThemeManager.GetAppTheme(this.Name);
            
            Accent expectedAccent = ThemeManager.Accents.First(x => x.Name == "Teal");
            AppTheme expectedTheme = ThemeManager.GetAppTheme("BaseDark");
            ThemeManager.ChangeAppStyle(Application.Current, expectedAccent, expectedTheme);
        }

        //private void ShowSettings(object sender, RoutedEventArgs e)
        //{
        //    //IsSettingsFlyoutOpen = true;
        //    //this.ToggleFlyout(0);
        //}

        //private void ToggleFlyout(int index)
        //{
        //    var flyout = this.Flyouts.Items[index] as Flyout;
        //    if (flyout == null)
        //    {
        //        return;
        //    }

        //    flyout.IsOpen = !flyout.IsOpen;
        //}

        //private void LoadFlyouts()
        //{
        //    this.flyouts.Add(new FlyoutLeftViewModel());
        //}
    }
}
