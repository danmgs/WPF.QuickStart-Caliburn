using Caliburn.Micro;
using System;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPF.QuickStart.UI.Utils;

namespace WPF.QuickStart.UI.ViewModels.Common.Dialog
{
    [PartCreationPolicy(CreationPolicy.NonShared)] // TODO : à vérifier l'utilité ici.
	public class DialogViewModel : Screen
	{
		private static readonly string prefixIMG = @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/Images/DialogWindow";
		private static readonly string prefixLargeIMG = prefixIMG + "/48x48/";
		private static readonly string prefixSmallIMG = prefixIMG + "/16x16/";

		#region Properties

		private Guid _id = Guid.NewGuid();
		public Guid Id
		{
			get { return _id; }
		}

		private string _header = "";
		public string Header
		{
			get { return _header; }
			set
			{
				_header = value;
				NotifyOfPropertyChange(() => Header);
			}
		}

		private string _text = "";
		public string Text
		{
			get { return _text; }
			set
			{
				_text = value;
				NotifyOfPropertyChange(() => Text);
			}
		}

		private NotificationType _notificationType = Dialog.NotificationType.Info;
		public NotificationType NotificationType
		{
			get { return _notificationType; }
			set
			{
				_notificationType = value;
				SetDialogStyle(value);
			}
		}

		private ImageSource _icon = new BitmapImage(new Uri(prefixLargeIMG + "info.png", UriKind.RelativeOrAbsolute));
		public ImageSource Icon
		{
			get { return _icon; }
			set
			{
				_icon = value;
				NotifyOfPropertyChange(() => Icon);
			}
		}

		private ImageSource _smallIcon = new BitmapImage(new Uri(prefixSmallIMG + "info.png", UriKind.RelativeOrAbsolute));
		public ImageSource SmallIcon
		{
			get { return _smallIcon; }
			set
			{
				_smallIcon = value;
				NotifyOfPropertyChange(() => SmallIcon);
			}
		}

		private Brush _fillBrush = new SolidColorBrush(ColorHelper.ToColor("#FF98CFE8"));
		public Brush FillBrush
		{
			get { return _fillBrush; }
			set
			{
				_fillBrush = value;
				NotifyOfPropertyChange(() => FillBrush);
			}
		}

		private DialogResult _dialogMessageResult;
		public DialogResult DialogMessageResult
		{
			get { return _dialogMessageResult; }
			set
			{
				_dialogMessageResult = value;
				NotifyOfPropertyChange(() => DialogMessageResult);
			}
		}

		private bool hasOk;
		public bool HasOk
		{
			get { return hasOk; }
			set
			{
				hasOk = value;
				NotifyOfPropertyChange(() => HasOk);
			}
		}

		private bool hasCancel;
		public bool HasCancel
		{
			get { return hasCancel; }
			set
			{
				hasCancel = value;
				NotifyOfPropertyChange(() => HasCancel);
			}
		}

		private bool hasYes;
		public bool HasYes
		{
			get { return hasYes; }
			set
			{
				hasYes = value;
				NotifyOfPropertyChange(() => HasYes);
			}
		}

		private bool hasNo;
		public bool HasNo
		{
			get { return hasNo; }
			set
			{
				hasNo = value;
				NotifyOfPropertyChange(() => HasNo);
			}
		}

		private bool hasIgnore;
		public bool HasIgnore
		{
			get { return hasIgnore; }
			set
			{
				hasIgnore = value;
				NotifyOfPropertyChange(() => HasIgnore);
			}
		}

		private bool hasRetry;
		public bool HasRetry
		{
			get { return hasRetry; }
			set
			{
				hasRetry = value;
				NotifyOfPropertyChange(() => HasRetry);
			}
		}

		#endregion

		#region Methods

		private void SetDialogStyle(NotificationType type)
		{
			switch (type)
			{
				case NotificationType.Info:
					this.FillBrush = new SolidColorBrush(ColorHelper.ToColor("#FF98CFE8"));
					this.Icon = new BitmapImage(new Uri(prefixLargeIMG + "info.png", UriKind.RelativeOrAbsolute));
					this.SmallIcon = new BitmapImage(new Uri(prefixSmallIMG + "info.ico", UriKind.RelativeOrAbsolute));
					this.HasOk = true;
					break;
				case NotificationType.Question:
					this.FillBrush = new SolidColorBrush(ColorHelper.ToColor("#FF98CFE8"));
					this.Icon = new BitmapImage(new Uri(prefixLargeIMG + "question.png", UriKind.RelativeOrAbsolute));
					this.SmallIcon = new BitmapImage(new Uri(prefixSmallIMG + "question.ico", UriKind.RelativeOrAbsolute));
					this.HasYes = true;
					this.HasNo = true;
					break;
				case NotificationType.Warning:
					this.FillBrush = new SolidColorBrush(ColorHelper.ToColor("Yellow"));
					this.Icon = new BitmapImage(new Uri(prefixLargeIMG + "warning.png", UriKind.RelativeOrAbsolute));
					this.SmallIcon = new BitmapImage(new Uri(prefixSmallIMG + "warning.ico", UriKind.RelativeOrAbsolute));
					this.HasOk = true;
					this.HasCancel = true;
					break;
				case NotificationType.Error:
					this.FillBrush = new SolidColorBrush(ColorHelper.ToColor("Red"));
					this.Icon = new BitmapImage(new Uri(prefixLargeIMG + "error.png", UriKind.RelativeOrAbsolute));
					this.SmallIcon = new BitmapImage(new Uri(prefixSmallIMG + "error.ico", UriKind.RelativeOrAbsolute));
					this.HasOk = true;
					break;
				case NotificationType.Custom:
					this.FillBrush = new SolidColorBrush(ColorHelper.ToColor("#FF87DCEA"));
					this.Icon = new BitmapImage(new Uri(prefixLargeIMG + "custom.png", UriKind.RelativeOrAbsolute));
					this.SmallIcon = new BitmapImage(new Uri(prefixSmallIMG + "custom.ico", UriKind.RelativeOrAbsolute));
					this.HasOk = true;
					break;
				default:
					this.FillBrush = new SolidColorBrush(ColorHelper.ToColor("#FF98CFE8"));
					this.Icon = new BitmapImage(new Uri(prefixLargeIMG + "info.png", UriKind.RelativeOrAbsolute));
					this.SmallIcon = new BitmapImage(new Uri(prefixSmallIMG + "info.ico", UriKind.RelativeOrAbsolute));
					this.HasOk = true;
					break;
			}
		}

		#endregion

		#region Actions

		public void Ok()
		{
			DialogMessageResult = DialogResult.Ok;
			TryClose();
		}

		public void Cancel()
		{
			DialogMessageResult = DialogResult.Cancel;
			TryClose();
		}

		public void Yes()
		{
			DialogMessageResult = DialogResult.Yes;
			TryClose();
		}

		public void No()
		{
			DialogMessageResult = DialogResult.No;
			TryClose();
		}

		public void Retry()
		{
			DialogMessageResult = DialogResult.Retry;
			TryClose();
		}

		public void Ignore()
		{
			DialogMessageResult = DialogResult.Ignore;
			TryClose();
		}

		#endregion
	};
}
