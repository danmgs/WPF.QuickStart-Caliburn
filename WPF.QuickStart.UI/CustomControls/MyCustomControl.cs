using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPF.QuickStart.UI.CustomControls
{
    class MyCustomControl : Control
    {
        #region DependencyProperties

        #region ItemsSource

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
        "ItemsSource", typeof(IEnumerable), typeof(MyCustomControl), new PropertyMetadata(OnItemsSourceChanged));
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        #endregion

        #region EnabledUi

        public static readonly DependencyProperty EnabledUiProperty = DependencyProperty.Register(
        "EnabledUi", typeof(bool), typeof(MyCustomControl), new PropertyMetadata(true));
        public bool EnabledUi
        {
            get { return (bool)GetValue(EnabledUiProperty); }
            set { SetValue(EnabledUiProperty, value); }
        }

        #endregion

        #region ValuationDate

        public static readonly DependencyProperty ValuationDateProperty = DependencyProperty.Register(
        "ValuationDate", typeof(DateTime), typeof(MyCustomControl),
        new FrameworkPropertyMetadata { BindsTwoWayByDefault = true, DefaultValue = DateTime.Now });
        public DateTime ValuationDate
        {
            get { return (DateTime)GetValue(ValuationDateProperty); }
            set { SetValue(ValuationDateProperty, value); }
        }

        #endregion

        #region IsExportable

        public static readonly DependencyProperty IsExportableProperty = DependencyProperty.Register(
        "IsExportable", typeof(Visibility), typeof(MyCustomControl), new PropertyMetadata(Visibility.Hidden));
        public Visibility IsExportable
        {
            get { return (Visibility)GetValue(IsExportableProperty); }
            set { SetValue(IsExportableProperty, value); }
        }

        #endregion

        #endregion
    }
}
