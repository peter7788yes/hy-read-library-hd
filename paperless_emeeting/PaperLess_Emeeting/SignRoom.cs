using PaperLess_Emeeting.App_Code.MessageBox;
using PaperLess_Emeeting.Properties;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace PaperLess_Emeeting
{
	public class SignRoom : UserControl, IComponentConnector
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass9
		{
			public BitmapImage bitmapImage;

			public SignRoom _003C_003E4__this;

			public void _003CInitUI_003Eb__7()
			{
				if (_003C_003E4__this.sb != null)
				{
					_003C_003E4__this.sb.Stop();
				}
				_003C_003E4__this.imgSignedPic.Source = bitmapImage;
			}
		}

		internal Grid grid;

		internal TextBlock txtName;

		internal TextBlock txtDept;

		internal Image imgSignedPic;

		internal TextBlock txtPicLoading;

		internal TextBlock txtAgent;

		internal Image btnSign;

		private bool _contentLoaded;

		[CompilerGenerated]
		private SigninDataUser _003CsigninDataUser_003Ek__BackingField;

		[CompilerGenerated]
		private Storyboard _003Csb_003Ek__BackingField;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate4;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate5;

		public SigninDataUser signinDataUser
		{
			[CompilerGenerated]
			get
			{
				return _003CsigninDataUser_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CsigninDataUser_003Ek__BackingField = value;
			}
		}

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

		public event Home_ChangeTogSignPadCT_Function Home_ChangeTogSignPadCT_Event;

		public SignRoom(SigninDataUser signinDataUser, Home_ChangeTogSignPadCT_Function callback1)
		{
			InitializeComponent();
			sb = (Storyboard)TryFindResource("sb");
			this.signinDataUser = signinDataUser;
			Home_ChangeTogSignPadCT_Event += callback1;
			base.Loaded += new RoutedEventHandler(SignRoom_Loaded);
		}

		private void SignRoom_Loaded(object sender, RoutedEventArgs e)
		{
			Task.Factory.StartNew(new Action(_003CSignRoom_Loaded_003Eb__0));
		}

		private void InitSelectDB()
		{
		}

		private void InitEvent()
		{
			Image image = btnSign;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate4 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate4 = new MouseEventHandler(_003CInitEvent_003Eb__2);
			}
			image.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate4;
			Image image2 = btnSign;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate5 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate5 = new MouseEventHandler(_003CInitEvent_003Eb__3);
			}
			image2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate5;
			btnSign.MouseLeftButtonDown += new MouseButtonEventHandler(btnSign_MouseLeftButtonDown);
		}

		private void btnSign_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
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
			}
			else
			{
				this.Home_ChangeTogSignPadCT_Event(signinDataUser.ID, signinDataUser.Name);
			}
		}

		private void InitUI()
		{
			Action action = null;
			txtName.Text = signinDataUser.Name;
			if (!signinDataUser.Dept.Equals(""))
			{
				txtDept.Text = string.Format("({0})", signinDataUser.Dept);
			}
			else
			{
				txtDept.Text = "";
			}
			txtAgent.Text = signinDataUser.AgentName;
			if (!signinDataUser.SignedPic.Equals(""))
			{
				if (Settings.Default.AssemblyName.Contains("PaperLess_Emeeting_EDU"))
				{
					btnSign.Visibility = Visibility.Collapsed;
				}
				txtAgent.Visibility = Visibility.Collapsed;
				if (sb != null)
				{
					sb.Begin();
				}
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003CInitUI_003Eb__6);
				}
				factory.StartNew(action);
			}
			else if (signinDataUser.AgentName.Equals(""))
			{
				txtAgent.Visibility = Visibility.Collapsed;
			}
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/signroom.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[DebuggerNonUserCode]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				grid = (Grid)target;
				break;
			case 2:
				txtName = (TextBlock)target;
				break;
			case 3:
				txtDept = (TextBlock)target;
				break;
			case 4:
				imgSignedPic = (Image)target;
				break;
			case 5:
				txtPicLoading = (TextBlock)target;
				break;
			case 6:
				txtAgent = (TextBlock)target;
				break;
			case 7:
				btnSign = (Image)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}

		[CompilerGenerated]
		private void _003CSignRoom_Loaded_003Eb__0()
		{
			InitSelectDB();
			base.Dispatcher.BeginInvoke(new Action(_003CSignRoom_Loaded_003Eb__1));
		}

		[CompilerGenerated]
		private void _003CSignRoom_Loaded_003Eb__1()
		{
			InitUI();
			InitEvent();
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
		private void _003CInitUI_003Eb__6()
		{
			try
			{
				_003C_003Ec__DisplayClass9 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass9();
				_003C_003Ec__DisplayClass._003C_003E4__this = this;
				WebClient webClient = new WebClient();
				webClient.CachePolicy = new RequestCachePolicy(RequestCacheLevel.Revalidate);
				byte[] buffer = webClient.DownloadData(new Uri(signinDataUser.SignedPic, UriKind.Absolute));
				_003C_003Ec__DisplayClass.bitmapImage = new BitmapImage();
				using (MemoryStream streamSource = new MemoryStream(buffer))
				{
					_003C_003Ec__DisplayClass.bitmapImage.BeginInit();
					_003C_003Ec__DisplayClass.bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
					_003C_003Ec__DisplayClass.bitmapImage.StreamSource = streamSource;
					_003C_003Ec__DisplayClass.bitmapImage.EndInit();
					_003C_003Ec__DisplayClass.bitmapImage.Freeze();
				}
				base.Dispatcher.BeginInvoke(new Action(_003C_003Ec__DisplayClass._003CInitUI_003Eb__7));
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}
	}
}
