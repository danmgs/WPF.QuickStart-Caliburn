﻿using Caliburn.Micro;
using System.ComponentModel.Composition;
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
        protected ExtendedScreen(string displayName, IEventAggregator eventAgg, IWindowManager windowManager)
        {
            IsBusy = false;
            _eventAgg = eventAgg;
            _eventAgg.Subscribe(this);
            _windowManager = windowManager;
            DisplayName = displayName;
        }

        #endregion

        #region Properties

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                NotifyOfPropertyChange(() => IsBusy);
            }
        }

        #endregion

        #region Methods

        protected virtual void PublishStatusEvent(string content, bool isLoading = false)
        {
            var ev = new StatusEvent()
            {
                Content = content,
                IsLoading = isLoading
            };
            _eventAgg.PublishOnUIThread(ev);
        }

        #endregion

    }
}
