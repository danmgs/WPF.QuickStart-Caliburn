using MahApps.Metro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF.QuickStart.UI.Converters
{
    public class MetroBackGroundConverter : BackgroundConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ListViewItem item = (ListViewItem)value;
            ListView listView = ItemsControl.ItemsControlFromItemContainer(item) as ListView; //Get the index of a ListViewItem 
            int index = listView.ItemContainerGenerator.IndexFromContainer(item);

            var theme = ThemeManager.DetectAppStyle(Application.Current);

            Accent currentUseAccent = ThemeManager.Accents
                                            .Where(a => a.Name.Equals(theme.Item2.Name)).First();

            Brush brush1 = currentUseAccent.Resources["AccentColorBrush"] as Brush;
            Brush brush2 = currentUseAccent.Resources["AccentColorBrush3"] as Brush;

            if (index % 2 == 0)
            {
                return brush1;
            }
            else
            {
                return brush2;
            }
        }
    }
}
