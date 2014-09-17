using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.QuickStart.UI.Events;

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

        [ImportingConstructor()]
        protected ExtendedScreen(IEventAggregator eventAgg, IWindowManager windowManager)
        {
            Enabled = true;
            _eventAgg = eventAgg;
            _eventAgg.Subscribe(this);
            _windowManager = windowManager;
        }

        #endregion

        #region Properties

        bool enabled;

        public bool Enabled
        {
            get { return enabled; }
            set
            {
                enabled = value;
                NotifyOfPropertyChange(() => Enabled);
            }
        }


        #endregion

        #region Methods

        protected virtual void PublishStatusEvent(string content)
        {
            var ev = new StatusEvent()
            {
                Content = content
            };
            _eventAgg.PublishOnUIThread(ev);
        }

        #endregion

    }
}
