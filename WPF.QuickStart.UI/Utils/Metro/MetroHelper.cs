using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace WPF.QuickStart.UI.Utils.Metro
{
    public static class MetroHelper
    {
        public static IEnumerable<Tuple<string, Brush>> FindBrushResources()
        {
            var rd = new ResourceDictionary
            {
                Source = new Uri(@"/MahApps.Metro;component/Styles/Colors.xaml", UriKind.RelativeOrAbsolute)
            };

            var resources = rd.Keys.Cast<object>()
                    .Where(key => rd[key] is Brush)
                    .Select(key => new Tuple<string, Brush>(key.ToString(), rd[key] as Brush))
                    .OrderBy(s => s)
                    .ToList();

            return resources;
        }
    }
}
