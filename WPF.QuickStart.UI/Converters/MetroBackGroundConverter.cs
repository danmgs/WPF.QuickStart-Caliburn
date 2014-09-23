using MahApps.Metro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPF.QuickStart.UI.Utils.Metro;

namespace WPF.QuickStart.UI.Converters
{
    public class MetroBackGroundConverter : BackgroundConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ListViewItem item = (ListViewItem)value;
            ListView listView = ItemsControl.ItemsControlFromItemContainer(item) as ListView; //Get the index of a ListViewItem 
            int index = listView.ItemContainerGenerator.IndexFromContainer(item);

            var rsr = MetroHelper.FindBrushResources();

            Brush brush1 = MetroHelper.FindBrushResources().Where(r => r.Item1.Equals("AccentColorBrush")).First().Item2;
            Brush brush2 = MetroHelper.FindBrushResources().Where(r => r.Item1.Equals("AccentColorBrush3")).First().Item2 as Brush;

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
