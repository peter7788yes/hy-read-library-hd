using PaperLess_Emeeting.App_Code.ClickOnce;
using PaperLess_Emeeting.App_Code.MessageBox;
using PaperLess_Emeeting.Properties;
using PaperLess_ViewModel;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PaperLess_Emeeting
{
	public class SignPadCT : UserControl, IComponentConnector
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass23
		{
			public BitmapImage bitmapImage;

			public SignPadCT _003C_003E4__this;

			public void _003CInitUI_003Eb__21()
			{
				if (_003C_003E4__this.sb != null)
				{
					_003C_003E4__this.sb.Stop();
				}
				_003C_003E4__this.imgSignedPic.Source = bitmapImage;
			}
		}

		private Point lastPoint = default(Point);

		private Point currentPoint = default(Point);

		private List<Line> tempLine = new List<Line>();

		private Stack<List<Line>> StackLines = new Stack<List<Line>>();

		internal Image btnBack;

		internal TextBlock txtName;

		internal StackPanel btnSignOut;

		internal Image imgSignOut;

		internal TextBlock txtSignOut;

		internal TextBlock txtPicLoading;

		internal TextBlock txtPLS;

		internal Border SignPadPanel;

		internal Canvas SignPad;

		internal Border imgSignedPicPanel;

		internal Image imgSignedPic;

		internal Image btnNO;

		internal Image btnYes;

		private bool _contentLoaded;

		[CompilerGenerated]
		private Storyboard _003Csb_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserID_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserID_Origin_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserName_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CMeetingID_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CDeptID_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CPicUrl_003Ek__BackingField;

		[CompilerGenerated]
		private bool _003CFromIndividualSign_003Ek__BackingField;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate13;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate14;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate15;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate16;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate17;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate18;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate19;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate1a;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate1b;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate1c;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate1d;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate1e;

		public Storyboard sb
		{
			[CompilerGenerated]
			get
			{
				return _003Csb_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003Csb_003Ek__BackingField = value;
			}
		}

		public string UserID
		{
			[CompilerGenerated]
			get
			{
				return _003CUserID_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CUserID_003Ek__BackingField = value;
			}
		}

		public string UserID_Origin
		{
			[CompilerGenerated]
			get
			{
				return _003CUserID_Origin_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CUserID_Origin_003Ek__BackingField = value;
			}
		}

		public string UserName
		{
			[CompilerGenerated]
			get
			{
				return _003CUserName_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CUserName_003Ek__BackingField = value;
			}
		}

		public string MeetingID
		{
			[CompilerGenerated]
			get
			{
				return _003CMeetingID_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CMeetingID_003Ek__BackingField = value;
			}
		}

		public string DeptID
		{
			[CompilerGenerated]
			get
			{
				return _003CDeptID_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CDeptID_003Ek__BackingField = value;
			}
		}

		public string PicUrl
		{
			[CompilerGenerated]
			get
			{
				return _003CPicUrl_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CPicUrl_003Ek__BackingField = value;
			}
		}

		public bool FromIndividualSign
		{
			[CompilerGenerated]
			get
			{
				return _003CFromIndividualSign_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CFromIndividualSign_003Ek__BackingField = value;
			}
		}

		public event Home_GoBackTogSignPictureCT_Function Home_GoBackTogSignPictureCT_Event;

		public SignPadCT(string UserID = "", string UserName = "", string DeptID = "", string PicUrl = "", Home_GoBackTogSignPictureCT_Function callback1 = null)
		{
			MouseTool.ShowLoading();
			InitializeComponent();
			this.UserID = UserID;
			this.UserName = UserName;
			this.DeptID = DeptID;
			this.PicUrl = PicUrl;
			Home_GoBackTogSignPictureCT_Event += callback1;
			sb = (Storyboard)TryFindResource("sb");
			base.Loaded += new RoutedEventHandler(SignPadCT_Loaded);
			base.Unloaded += new RoutedEventHandler(SignPadCT_Unloaded);
		}

		private void SignPadCT_Loaded(object sender, RoutedEventArgs e)
		{
			Task.Factory.StartNew(new Action(_003CSignPadCT_Loaded_003Eb__0));
		}

		private void SignPadCT_Unloaded(object sender, RoutedEventArgs e)
		{
			Home home = Enumerable.FirstOrDefault(Enumerable.OfType<Home>(Application.Current.Windows));
			if (home != null)
			{
				home.KeyDown -= new KeyEventHandler(Home_Window_KeyDown);
			}
		}

		private void InitSelectDB()
		{
		}

		private void InitEvent()
		{
			Home home = Enumerable.FirstOrDefault(Enumerable.OfType<Home>(Application.Current.Windows));
			if (home != null)
			{
				home.KeyDown += new KeyEventHandler(Home_Window_KeyDown);
			}
			Image image = btnYes;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate13 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate13 = new MouseEventHandler(_003CInitEvent_003Eb__2);
			}
			image.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate13;
			Image image2 = btnYes;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate14 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate14 = new MouseEventHandler(_003CInitEvent_003Eb__3);
			}
			image2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate14;
			btnYes.MouseDown += new MouseButtonEventHandler(btnYes_MouseDown);
			Image image3 = btnNO;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate15 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate15 = new MouseEventHandler(_003CInitEvent_003Eb__4);
			}
			image3.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate15;
			Image image4 = btnNO;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate16 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate16 = new MouseEventHandler(_003CInitEvent_003Eb__5);
			}
			image4.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate16;
			btnNO.MouseDown += new MouseButtonEventHandler(_003CInitEvent_003Eb__6);
			Image image5 = btnBack;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate17 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate17 = new MouseEventHandler(_003CInitEvent_003Eb__7);
			}
			image5.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate17;
			Image image6 = btnBack;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate18 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate18 = new MouseEventHandler(_003CInitEvent_003Eb__8);
			}
			image6.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate18;
			btnBack.MouseLeftButtonDown += new MouseButtonEventHandler(_003CInitEvent_003Eb__9);
			Canvas signPad = SignPad;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate19 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate19 = new MouseEventHandler(_003CInitEvent_003Eb__a);
			}
			signPad.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate19;
			Canvas signPad2 = SignPad;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate1a == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate1a = new MouseEventHandler(_003CInitEvent_003Eb__b);
			}
			signPad2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate1a;
			SignPad.MouseDown += new MouseButtonEventHandler(SignPad_MouseDown);
			SignPad.MouseMove += new MouseEventHandler(SignPad_MouseMove);
			SignPad.MouseUp += new MouseButtonEventHandler(SignPad_MouseUp);
			Border border = imgSignedPicPanel;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate1b == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate1b = new MouseEventHandler(_003CInitEvent_003Eb__c);
			}
			border.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate1b;
			Border border2 = imgSignedPicPanel;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate1c == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate1c = new MouseEventHandler(_003CInitEvent_003Eb__d);
			}
			border2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate1c;
			imgSignedPicPanel.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(_003CInitEvent_003Eb__e);
			StackPanel stackPanel = btnSignOut;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate1d == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate1d = new MouseEventHandler(_003CInitEvent_003Eb__f);
			}
			stackPanel.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate1d;
			StackPanel stackPanel2 = btnSignOut;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate1e == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate1e = new MouseEventHandler(_003CInitEvent_003Eb__10);
			}
			stackPanel2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate1e;
			btnSignOut.MouseLeftButtonDown += new MouseButtonEventHandler(_003CInitEvent_003Eb__11);
		}

		private void GetSignOutUpload_DoAction(SignOut so)
		{
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<SignOut>(GetSignOutUpload_DoAction), so);
				return;
			}
			if (so != null && so.Reception.Status.ToLower().Trim().Equals("true"))
			{
				AutoClosingMessageBox.Show("簽退成功");
			}
			else
			{
				UserID = UserID_Origin;
				AutoClosingMessageBox.Show("簽退失敗");
			}
			MouseTool.ShowArrow();
		}

		private void Home_Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key != Key.Z || (Keyboard.Modifiers & ModifierKeys.Control) != ModifierKeys.Control)
			{
				return;
			}
			List<Line> list = StackLines.Pop();
			if (list != null)
			{
				foreach (Line item in list)
				{
					if (SignPad.Children.Contains(item))
					{
						SignPad.Children.Remove(item);
					}
				}
				if (SignPad.Children.Count < 1)
				{
					txtPLS.Visibility = Visibility.Visible;
				}
			}
		}

		private void btnYes_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (StackLines.Count < 1 || txtPLS.Visibility == Visibility.Visible)
			{
				if (!PicUrl.Equals(""))
				{
					MessageBox.Show("已簽名，欲重新簽名請先按x清除");
				}
				else
				{
					MessageBox.Show("請簽名後上傳");
				}
				return;
			}
			string text = "";
			DataTable dataTable = MSCE.GetDataTable("select AllowIpRange from NowLogin");
			if (dataTable.Rows.Count > 0)
			{
				text = dataTable.Rows[0]["AllowIpRange"].ToString();
			}
			if (!text.Equals("") && !IpTool.CheckInNowWifi(text))
			{
				string courseOrMeeting_String = Settings.Default.CourseOrMeeting_String;
				AutoClosingMessageBox.Show(string.Format("您不在{0}室範圍內，無法使用此功能", courseOrMeeting_String.Equals("課程") ? "教" : courseOrMeeting_String));
				return;
			}
			string path = System.IO.Path.Combine(ClickOnceTool.GetFilePath(), Settings.Default.SignInFolder);
			path = System.IO.Path.Combine(path, MeetingID, UserID);
			Directory.CreateDirectory(path);
			string str = Guid.NewGuid().ToString();
			string path2 = str + ".png";
			string text2 = System.IO.Path.Combine(path, path2);
			Application current = Application.Current;
			CanvasTool.SaveCanvas(current.Windows[0], SignPad, 96, text2);
			MouseTool.ShowLoading();
			if (UserID.Equals("guest"))
			{
				UserID_Origin = UserID;
				UserID = "";
			}
			else if (UserID.Equals("dept"))
			{
				UserID_Origin = UserID;
				UserID = "";
			}
			GetSigninDataUpload.AsyncPOST(MeetingID, UserID, DeptID, text2, new Action<SigninDataUpload>(_003CbtnYes_MouseDown_003Eb__1f));
		}

		private void GetSigninDataUpload_DoAction(SigninDataUpload sdu)
		{
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<SigninDataUpload>(GetSigninDataUpload_DoAction), sdu);
				return;
			}
			if (sdu != null && sdu.File.Status.ToLower().Trim().Equals("true"))
			{
				AutoClosingMessageBox.Show("上傳成功");
				if (FromIndividualSign)
				{
					SignPad.IsEnabled = false;
					btnNO.Visibility = Visibility.Collapsed;
					btnYes.Visibility = Visibility.Collapsed;
				}
				else
				{
					this.Home_GoBackTogSignPictureCT_Event("", "");
				}
			}
			else
			{
				UserID = UserID_Origin;
				AutoClosingMessageBox.Show("上傳失敗");
			}
			MouseTool.ShowArrow();
		}

		private void SignPad_MouseDown(object sender, MouseButtonEventArgs e)
		{
			txtPLS.Visibility = Visibility.Collapsed;
			if (e.ButtonState == MouseButtonState.Pressed)
			{
				currentPoint = e.GetPosition(this);
			}
		}

		private void SignPad_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed && !Point.Equals(currentPoint, default(Point)))
			{
				lastPoint = currentPoint;
				int signPenThickness = Settings.Default.SignPenThickness;
				int num = Math.Abs(Settings.Default.SignPenThickness - 15);
				Line line = new Line();
				line.StrokeThickness = signPenThickness;
				line.Stroke = Brushes.Black;
				line.X1 = currentPoint.X - 31.0 - (double)num;
				line.Y1 = currentPoint.Y - 53.0 - (double)(num * 2);
				line.X2 = e.GetPosition(this).X - 31.0 - (double)num;
				line.Y2 = e.GetPosition(this).Y - 53.0 - (double)(num * 2);
				line.StrokeStartLineCap = PenLineCap.Round;
				line.StrokeStartLineCap = PenLineCap.Round;
				line.StrokeEndLineCap = PenLineCap.Round;
				line.SnapsToDevicePixels = true;
				SignPad.Children.Add(line);
				tempLine.Add(line);
				line = new Line();
				line.StrokeThickness = signPenThickness;
				line.Stroke = Brushes.Black;
				line.X1 = currentPoint.X - 31.0 - (double)num;
				line.Y1 = currentPoint.Y - 53.0 - (double)(num * 2);
				line.X2 = e.GetPosition(this).X - 31.0 - (double)num;
				line.Y2 = e.GetPosition(this).Y - 53.0 - (double)(num * 2);
				currentPoint = e.GetPosition(this);
				line.SnapsToDevicePixels = true;
				SignPad.Children.Add(line);
				tempLine.Add(line);
			}
		}

		private void SignPad_MouseUp(object sender, MouseButtonEventArgs e)
		{
			StackLines.Push(tempLine);
			tempLine = new List<Line>();
		}

		private void InitUI()
		{
			Action action = null;
			DataTable dataTable = MSCE.GetDataTable("select UserID,UserName,MeetingID from NowLogin");
			if (dataTable.Rows.Count > 0)
			{
				if (UserID.Equals(""))
				{
					FromIndividualSign = true;
					UserID = dataTable.Rows[0]["UserID"].ToString();
					UserName = dataTable.Rows[0]["UserName"].ToString();
				}
				else
				{
					FromIndividualSign = false;
					btnBack.Visibility = Visibility.Visible;
				}
				MeetingID = dataTable.Rows[0]["MeetingID"].ToString();
				txtName.Text = UserName + " 您好";
			}
			if (!PicUrl.Equals(""))
			{
				txtPLS.Visibility = Visibility.Collapsed;
				SignPadPanel.Visibility = Visibility.Collapsed;
				imgSignedPicPanel.Visibility = Visibility.Visible;
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003CInitUI_003Eb__20);
				}
				factory.StartNew(action);
			}
			if (Settings.Default.EnableSignOut && FromIndividualSign)
			{
				btnSignOut.Visibility = Visibility.Visible;
				if (PicUrl.Equals(""))
				{
					FormatConvertedBitmap formatConvertedBitmap = new FormatConvertedBitmap();
					formatConvertedBitmap.BeginInit();
					formatConvertedBitmap.Source = (BitmapSource)imgSignOut.Source;
					formatConvertedBitmap.DestinationFormat = PixelFormats.Gray32Float;
					formatConvertedBitmap.EndInit();
					imgSignOut.Source = formatConvertedBitmap;
					txtSignOut.Foreground = Brushes.Gray;
					btnSignOut.ToolTip = "您尚未簽到，無法簽退";
					btnSignOut.IsEnabled = false;
					btnSignOut.Visibility = Visibility.Hidden;
				}
				else
				{
					btnSignOut.Visibility = Visibility.Visible;
				}
			}
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/signpadct.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				btnBack = (Image)target;
				break;
			case 2:
				txtName = (TextBlock)target;
				break;
			case 3:
				btnSignOut = (StackPanel)target;
				break;
			case 4:
				imgSignOut = (Image)target;
				break;
			case 5:
				txtSignOut = (TextBlock)target;
				break;
			case 6:
				txtPicLoading = (TextBlock)target;
				break;
			case 7:
				txtPLS = (TextBlock)target;
				break;
			case 8:
				SignPadPanel = (Border)target;
				break;
			case 9:
				SignPad = (Canvas)target;
				break;
			case 10:
				imgSignedPicPanel = (Border)target;
				break;
			case 11:
				imgSignedPic = (Image)target;
				break;
			case 12:
				btnNO = (Image)target;
				break;
			case 13:
				btnYes = (Image)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}

		[CompilerGenerated]
		private void _003CSignPadCT_Loaded_003Eb__0()
		{
			InitSelectDB();
			base.Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(_003CSignPadCT_Loaded_003Eb__1));
		}

		[CompilerGenerated]
		private void _003CSignPadCT_Loaded_003Eb__1()
		{
			try
			{
				InitUI();
				InitEvent();
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__2(object sender, MouseEventArgs e)
		{
			MouseTool.ShowHand();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__3(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__4(object sender, MouseEventArgs e)
		{
			MouseTool.ShowHand();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__5(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__6(object sender, MouseButtonEventArgs e)
		{
			if (!PicUrl.Equals("") || StackLines.Count > 0)
			{
				MessageBoxResult messageBoxResult = MessageBox.Show("是否要清除簽名檔", "系統訊息", MessageBoxButton.OKCancel);
				if (messageBoxResult == MessageBoxResult.Cancel)
				{
					return;
				}
				PicUrl = "";
			}
			imgSignedPicPanel.Visibility = Visibility.Collapsed;
			txtPLS.Visibility = Visibility.Visible;
			SignPadPanel.Visibility = Visibility.Visible;
			SignPad.Children.Clear();
			StackLines.Clear();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__7(object sender, MouseEventArgs e)
		{
			MouseTool.ShowHand();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__8(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__9(object sender, MouseButtonEventArgs e)
		{
			this.Home_GoBackTogSignPictureCT_Event("", "");
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__a(object sender, MouseEventArgs e)
		{
			MouseTool.ShowPen();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__b(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__c(object sender, MouseEventArgs e)
		{
			MouseTool.ShowPen();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__d(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__e(object sender, MouseButtonEventArgs e)
		{
			imgSignedPicPanel.Visibility = Visibility.Collapsed;
			txtPLS.Visibility = Visibility.Visible;
			SignPadPanel.Visibility = Visibility.Visible;
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__f(object sender, MouseEventArgs e)
		{
			MouseTool.ShowHand();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__10(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__11(object sender, MouseButtonEventArgs e)
		{
			MessageBoxResult messageBoxResult = MessageBox.Show("您確定要簽退?", "系統訊息", MessageBoxButton.YesNo);
			if (messageBoxResult == MessageBoxResult.Yes)
			{
				MouseTool.ShowLoading();
				GetSignOutUpload.AsyncPOST(MeetingID, DeptID, UserID, new Action<SignOut>(_003CInitEvent_003Eb__12));
			}
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__12(SignOut so)
		{
			GetSignOutUpload_DoAction(so);
		}

		[CompilerGenerated]
		private void _003CbtnYes_MouseDown_003Eb__1f(SigninDataUpload sdu)
		{
			GetSigninDataUpload_DoAction(sdu);
		}

		[CompilerGenerated]
		private void _003CInitUI_003Eb__20()
		{
			try
			{
				_003C_003Ec__DisplayClass23 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass23();
				_003C_003Ec__DisplayClass._003C_003E4__this = this;
				WebClient webClient = new WebClient();
				webClient.CachePolicy = new RequestCachePolicy(RequestCacheLevel.Revalidate);
				byte[] buffer = webClient.DownloadData(PicUrl);
				_003C_003Ec__DisplayClass.bitmapImage = new BitmapImage();
				using (MemoryStream streamSource = new MemoryStream(buffer))
				{
					_003C_003Ec__DisplayClass.bitmapImage.BeginInit();
					_003C_003Ec__DisplayClass.bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
					_003C_003Ec__DisplayClass.bitmapImage.StreamSource = streamSource;
					_003C_003Ec__DisplayClass.bitmapImage.EndInit();
					_003C_003Ec__DisplayClass.bitmapImage.Freeze();
				}
				base.Dispatcher.BeginInvoke(new Action(_003C_003Ec__DisplayClass._003CInitUI_003Eb__21));
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}
	}
}
