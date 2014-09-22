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
        }

        private void GoYahoo(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://yahoo.com");
        }

        private void GoTwitter(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://twitter.com");
        }
    }
}
