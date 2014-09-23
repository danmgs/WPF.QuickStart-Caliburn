using MahApps.Metro.Controls;

namespace WPF.QuickStart.UI.ViewModels.Flyouts
{
    public class FlyoutLeftViewModel : FlyoutBaseViewModel
    {
        public FlyoutLeftViewModel()
        {
            this.Header = "left";
            this.Position = Position.Left;
        }
    }
}