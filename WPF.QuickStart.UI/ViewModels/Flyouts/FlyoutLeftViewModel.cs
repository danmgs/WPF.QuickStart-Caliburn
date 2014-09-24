using MahApps.Metro.Controls;
using System.ComponentModel.Composition;

namespace WPF.QuickStart.UI.ViewModels.Flyouts
{
    [Export(typeof(IFlyoutLeftViewModel))]
    public class FlyoutLeftViewModel : FlyoutBaseViewModel, IFlyoutLeftViewModel
    {
        public FlyoutLeftViewModel()
        {
            this.Header = "left";
            this.Position = Position.Left;
        }
    }

    public interface IFlyoutLeftViewModel { }
}