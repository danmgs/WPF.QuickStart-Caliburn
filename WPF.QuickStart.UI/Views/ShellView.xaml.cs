using MahApps.Metro;
using MahApps.Metro.Controls;
using System.Linq;
using System.Windows;

namespace WPF.QuickStart.UI.Views
{

    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ShellView : MetroWindow
    {
        public ShellView()
        {
            InitializeComponent();
            Title = "TITLE NOT WORKING PROGRAMMATICALLY, NEITHER IN VIEW XAML, NEITHER IN RESOURCES XAML ...";

            var theme = ThemeManager.DetectAppStyle(Application.Current);
            var appTheme = ThemeManager.GetAppTheme(this.Name);
            
            Accent expectedAccent = ThemeManager.Accents.First(x => x.Name == "Teal");
            AppTheme expectedTheme = ThemeManager.GetAppTheme("BaseDark");
            ThemeManager.ChangeAppStyle(Application.Current, expectedAccent, expectedTheme);
        }
    }
}
