using Caliburn.Micro;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Quickstart.Model.Yahoo;
using WPF.QuickStart.UI.ViewModels.Common;
using WPF.QuickStart.UI.ViewModels.Common.Dialog;

namespace WPF.QuickStart.UI.ViewModels.WinOs
{
    [Export(typeof(IWinOsChildViewModel))]
    public class WinOsChildViewModel : ExtendedScreen, IWinOsChildViewModel
    {
        [ImportingConstructor]
        public WinOsChildViewModel(IEventAggregator eventAgg, IWindowManager windowManager)
            : base("", eventAgg, windowManager)
        {
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            PublishStatusEvent(string.Format("Load {0} ...", DisplayName));
        }
    }

    public interface IWinOsChildViewModel { }
}
