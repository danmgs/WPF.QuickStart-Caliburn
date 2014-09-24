using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPF.QuickStart.UI.Common.Controls
{
    public class YahooInputControl : Control
    {
        #region DependencyProperties

        #region BtnTitleAction

        public static readonly DependencyProperty BtnTitleActionProperty = DependencyProperty.Register(
        "BtnTitleAction", typeof(string), typeof(YahooInputControl), new PropertyMetadata("Action"));
        public string BtnTitleAction
        {
            get { return (string)GetValue(BtnTitleActionProperty); }
            set { SetValue(BtnTitleActionProperty, value); }
        }

        #endregion

        #region Watermark

        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register(
        "Watermark", typeof(string), typeof(YahooInputControl), new PropertyMetadata("Type something .."));
        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        #endregion     
   
        #region Tooltip

        public static readonly DependencyProperty TooltipProperty = DependencyProperty.Register(
        "Tooltip", typeof(string), typeof(YahooInputControl), null);
        public string Tooltip
        {
            get { return (string)GetValue(TooltipProperty); }
            set { SetValue(TooltipProperty, value); }
        }

        #endregion  

        #region ButtonContent

        public static readonly DependencyProperty ButtonContentProperty = DependencyProperty.Register(
        "ButtonContent", typeof(char), typeof(YahooInputControl), new PropertyMetadata('r'));
        public char ButtonContent
        {
            get { return (char)GetValue(ButtonContentProperty); }
            set { SetValue(ButtonContentProperty, value); }
        }

        #endregion

        /*

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

        */
        #endregion
    }
}
