using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace WPF.QuickStart.UI.Converters
{
    public class BackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ListViewItem item = (ListViewItem)value; 
            ListView listView = ItemsControl.ItemsControlFromItemContainer(item) as ListView; //Get the index of a ListViewItem 
            int index= listView.ItemContainerGenerator.IndexFromContainer(item); 
            if (index% 2 == 0) {
                return Brushes.LightBlue;
            }
            else  { 
                return Brushes.Beige;
            }
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


    }
}
