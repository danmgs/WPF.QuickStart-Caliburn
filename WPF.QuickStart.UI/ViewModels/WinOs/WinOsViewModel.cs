using Caliburn.Micro;
using System.ComponentModel.Composition;
using WPF.QuickStart.UI.ViewModels.Common;

namespace WPF.QuickStart.UI.ViewModels.WinOs
{
    [Export(typeof(IWinOsViewModel))]
    public class WinOsViewModel : ExtendedScreen, IWinOsViewModel
    {
        [ImportingConstructor]
        public WinOsViewModel(string displayName, IEventAggregator eventAgg, IWindowManager windowManager)
            : base(displayName, eventAgg, windowManager)
        {
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            PublishStatusEvent(string.Format("Load {0} ...", DisplayName));
        }
    }

    public interface IWinOsViewModel { }
}
