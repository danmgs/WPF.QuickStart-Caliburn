using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.QuickStart.UI.ViewModels.Common
{
    public abstract class ExtendedScreen : Screen
    {
        #region Fields

        protected static readonly ILog Logger = LogManager.GetLog(typeof(ExtendedScreen));
        protected readonly IEventAggregator _eventAgg;
        protected IWindowManager _windowManager;

        #endregion

        #region Constructor

        protected ExtendedScreen(IEventAggregator eventAgg, IWindowManager windowManager)
        {
            _eventAgg = eventAgg;
            _eventAgg.Subscribe(this);
            _windowManager = windowManager;
        }

        #endregion

    }
}
