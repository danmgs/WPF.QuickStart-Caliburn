namespace WPF.QuickStart.UI.Common.Controls
{
	using System;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Data;
	using System.Windows.Media;
	using System.Windows.Media.Imaging;
	using System.Windows.Threading;
	using System.Windows.Shapes;

	public class DialogControl : ContentControl
	{
		//DispatcherTimer _timer = new DispatcherTimer();

		//#region ctor

		//public DialogControl(NotificationModel item, int interval)
		//{
		//    this.DefaultStyleKey = typeof(DialogControl);
		//    this._id = item.Id;
		//    this.Interval = interval;

		//    if (Interval > 0)
		//    {
		//        _timer.Interval = new TimeSpan(0, 0, 0, 0, Interval); // 5 Seconds 
		//        _timer.Tick += new EventHandler(Each_Tick);
		//        _timer.Start();
		//    }

		//    // Set the Header binding.
		//    Binding binding = new Binding();
		//    binding.Source = item;
		//    binding.Path = new PropertyPath("Header");
		//    this.SetBinding(DialogControl.HeaderProperty, binding);
		//    // Set the Text binding.
		//    binding = new Binding();
		//    binding.Source = item;
		//    binding.Path = new PropertyPath("Text");
		//    this.SetBinding(DialogControl.TextProperty, binding);
		//    // Set the Dock property.
		//    DockPanel.SetDock(this, Dock.Bottom);
		//    // Add a little margin to the bottom and top.
		//    this.Margin = new Thickness(0, 2, 0, 2);
		//    // Swap out the icon based on the NotificationType.
		//    switch (item.NotificationType)
		//    {
		//        case NotificationType.Info:
		//            this.FillBrush = new SolidColorBrush(ColorHelper.ToColor("#FF98CFE8"));
		//            this.Icon = new BitmapImage(new Uri("/NotificationSample;component/Assets/Images/48x48/about.png", UriKind.Relative));
		//            break;
		//        case NotificationType.Question:
		//            this.FillBrush = new SolidColorBrush(ColorHelper.ToColor("#FF2C3CF5"));
		//            this.Icon = new BitmapImage(new Uri("/NotificationSample;component/Assets/Images/48x48/help2.png", UriKind.Relative));
		//            break;
		//        case NotificationType.Warning:
		//            this.FillBrush = new SolidColorBrush(ColorHelper.ToColor("Yellow"));
		//            this.Icon = new BitmapImage(new Uri("/NotificationSample;component/Assets/Images/48x48/warning.png", UriKind.Relative));
		//            break;
		//        case NotificationType.Error:
		//            this.FillBrush = new SolidColorBrush(ColorHelper.ToColor("Red"));
		//            this.Icon = new BitmapImage(new Uri("/NotificationSample;component/Assets/Images/48x48/error.png", UriKind.Relative));
		//            break;
		//        case NotificationType.Custom:
		//            this.FillBrush = new SolidColorBrush(ColorHelper.ToColor("White"));
		//            this.Icon = new BitmapImage(new Uri("/NotificationSample;component/Assets/Images/48x48/user1.png", UriKind.Relative));
		//            break;
		//    }
		//}

		//#endregion

		//private Guid _id = Guid.NewGuid();
		//public Guid Id
		//{
		//    get { return _id; }
		//}

		#region Dependency Properties

		#region FillBrush

		public static readonly DependencyProperty FillBrushProperty = DependencyProperty.Register(
			"FillBrush",
			typeof(Brush),
			typeof(DialogControl),
			null);

		/// <summary>
		///   Gets or sets a value that indicates whether 
		///   the <see cref="P:System.Windows.Controls.Label.Target" /> field is required. 
		/// </summary>
		public Brush FillBrush
		{
			get { return (Brush)GetValue(DialogControl.FillBrushProperty); }
			set { SetValue(DialogControl.FillBrushProperty, value); }
		}

		#endregion

		#region Interval

		public static readonly DependencyProperty IntervalProperty = DependencyProperty.Register(
			"Interval",
			typeof(int),
			typeof(DialogControl),
			new PropertyMetadata(0));

		/// <summary>
		///   Gets or sets a value that indicates whether 
		///   the <see cref="P:System.Windows.Controls.Label.Target" /> field is required. 
		/// </summary>
		public int Interval
		{
			get { return (int)GetValue(DialogControl.IntervalProperty); }
			set { SetValue(DialogControl.IntervalProperty, value); }
		}

		#endregion

		#region Icon
		public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
			"Icon",
			typeof(ImageSource),
			typeof(DialogControl));

		/// <summary>
		///   Gets or sets a value that indicates whether 
		///   the <see cref="P:System.Windows.Controls.Label.Target" /> field is required. 
		/// </summary>
		public ImageSource Icon
		{
			get { return (ImageSource)GetValue(DialogControl.IconProperty); }
			set { SetValue(DialogControl.IconProperty, value); }
		}
		#endregion

		#region Header
		public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
			"Header",
			typeof(string),
			typeof(DialogControl),
			new PropertyMetadata(OnHeaderPropertyChanged));

		/// <summary>
		///   Gets or sets a value that indicates whether 
		///   the <see cref="P:System.Windows.Controls.Label.Target" /> field is required. 
		/// </summary>
		public string Header
		{
			get { return (string)GetValue(DialogControl.HeaderProperty); }
			set { SetValue(DialogControl.HeaderProperty, value); }
		}

		private static void OnHeaderPropertyChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
		{
		}
		#endregion

		#region Text
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
			"Text",
			typeof(string),
			typeof(DialogControl),
			new PropertyMetadata(OnTextPropertyChanged));

		/// <summary>
		///   Gets or sets a value that indicates whether 
		///   the <see cref="P:System.Windows.Controls.Label.Target" /> field is required. 
		/// </summary>
		public string Text
		{
			get { return (string)GetValue(DialogControl.TextProperty); }
			set { SetValue(DialogControl.TextProperty, value); }
		}

		private static void OnTextPropertyChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
		{
		}
		#endregion

		#endregion

		#region Overrides

		//public override void OnApplyTemplate()
		//{
		//    base.OnApplyTemplate();
		//    Button closeButton = GetTemplateChild("closeButton") as Button;
		//    if (closeButton != null)
		//    {
		//        closeButton.Click += new RoutedEventHandler(closeButton_Click);
		//    }
		//}

		#endregion

		#region Events

		//public event EventHandler<EventArgs> Closed;

		//void closeButton_Click(object sender, RoutedEventArgs e)
		//{
		//    EventHandler<EventArgs> handler = this.Closed;
		//    if (handler != null)
		//    {
		//        _timer.Stop();
		//        handler(this, EventArgs.Empty);
		//    }
		//}

		//public void Each_Tick(object o, EventArgs sender)
		//{
		//    closeButton_Click(this, new RoutedEventArgs());
		//}

		#endregion

	};
}
