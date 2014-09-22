using MahApps.Metro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF.QuickStart.UI.Utils.Themes
{
    public class AppThemeMenuData : AccentColorMenuData
    {
        protected override void DoChangeTheme(object sender)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            var appTheme = ThemeManager.GetAppTheme(this.Name);
            ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, appTheme);
        }
    }
}
