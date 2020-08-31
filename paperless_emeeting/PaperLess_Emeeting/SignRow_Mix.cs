using PaperLess_Emeeting.App_Code.MessageBox;
using PaperLess_Emeeting.Properties;
using System;
using System.CodeDom.Compiler;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace PaperLess_Emeeting
{
	public class SignRow_Mix : UserControl, IComponentConnector
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClasse
		{
			public Home Home_Window;

			public void _003CtxtUnSigned_MouseLeftButtonDown_003Eb__a(string x, string y)
			{
				Home_Window.CC.Content = new SignListCT_Mix();
			}

			public void _003CtxtUnSigned_MouseLeftButtonDown_003Eb__b(string x, string y)
			{
				Home_Window.CC.Content = new SignListCT_Mix();
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass13
		{
			private sealed class _003C_003Ec__DisplayClass15
			{
				public _003C_003Ec__DisplayClass13 CS_0024_003C_003E8__locals14;

				public BitmapImage bitmapImage;

				public void _003CGetHttpImage_003Eb__11()
				{
					if (CS_0024_003C_003E8__locals14._003C_003E4__this.sb != null)
					{
						CS_0024_003C_003E8__locals14._003C_003E4__this.sb.Stop();
					}
					CS_0024_003C_003E8__locals14._003C_003E4__this.imgSignedPic.Source = bitmapImage;
				}
			}

			public string imgUrl;

			public SignRow_Mix _003C_003E4__this;

			public void _003CGetHttpImage_003Eb__10()
			{
				try
				{
					_003C_003Ec__DisplayClass15 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass15();
					_003C_003Ec__DisplayClass.CS_0024_003C_003E8__locals14 = this;
					WebClient webClient = new WebClient();
					webClient.CachePolicy = new RequestCachePolicy(RequestCacheLevel.Revalidate);
					byte[] buffer = webClient.DownloadData(imgUrl);
					_003C_003Ec__DisplayClass.bitmapImage = new BitmapImage();
					using (MemoryStream streamSource = new MemoryStream(buffer))
					{
						_003C_003Ec__DisplayClass.bitmapImage.BeginInit();
						_003C_003Ec__DisplayClass.bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
						_003C_003Ec__DisplayClass.bitmapImage.StreamSource = streamSource;
						_003C_003Ec__DisplayClass.bitmapImage.EndInit();
						_003C_003Ec__DisplayClass.bitmapImage.Freeze();
					}
					_003C_003E4__this.Dispatcher.BeginInvoke(new Action(_003C_003Ec__DisplayClass._003CGetHttpImage_003Eb__11));
				}
				catch (Exception ex)
				{
					LogTool.Debug(ex);
				}
			}
		}

		public MeetingUserType meetingUserType;

		public bool EnableTxtPLSSigned;

		internal TextBlock txtIndex;

		internal TextBlock txtDept;

		internal TextBlock txtTitle;

		internal TextBlock txtUserName;

		internal TextBlock txtAgent;

		internal TextBlock txtPicLoading;

		internal TextBlock txtPLSSigned;

		internal TextBlock txtUnSigned;

		internal Image imgSignedPic;

		private bool _contentLoaded;

		[CompilerGenerated]
		private SigninDataUser _003CsigninDataUser_003Ek__BackingField;

		[CompilerGenerated]
		private Storyboard _003Csb_003Ek__BackingField;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate6;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate7;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate8;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate9;

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

		public SignRow_Mix(SigninDataUser signinDataUser, MeetingUserType meetingUserType, bool EnableTxtPLSSigned)
		{
			InitializeComponent();
			this.meetingUserType = meetingUserType;
			this.EnableTxtPLSSigned = EnableTxtPLSSigned;
			sb = (Storyboard)TryFindResource("sb");
			this.signinDataUser = signinDataUser;
			base.Loaded += new RoutedEventHandler(SignRow_Loaded);
		}

		private void SignRow_Loaded(object sender, RoutedEventArgs e)
		{
			Task.Factory.StartNew(new Action(_003CSignRow_Loaded_003Eb__0));
		}

		private void InitSelectDB()
		{
		}

		private void InitEvent()
		{
			switch (meetingUserType)
			{
			case MeetingUserType.與會人員:
			case MeetingUserType.代理人:
			case MeetingUserType.其它:
				break;
			case MeetingUserType.議事管理人員:
			{
				TextBlock textBlock = txtPLSSigned;
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate6 == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate6 = new MouseEventHandler(_003CInitEvent_003Eb__2);
				}
				textBlock.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate6;
				TextBlock textBlock2 = txtPLSSigned;
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate7 == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate7 = new MouseEventHandler(_003CInitEvent_003Eb__3);
				}
				textBlock2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate7;
				txtPLSSigned.MouseLeftButtonDown += new MouseButtonEventHandler(txtUnSigned_MouseLeftButtonDown);
				Image image = imgSignedPic;
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate8 == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate8 = new MouseEventHandler(_003CInitEvent_003Eb__4);
				}
				image.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate8;
				Image image2 = imgSignedPic;
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate9 == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate9 = new MouseEventHandler(_003CInitEvent_003Eb__5);
				}
				image2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate9;
				imgSignedPic.MouseLeftButtonDown += new MouseButtonEventHandler(txtUnSigned_MouseLeftButtonDown);
				break;
			}
			}
		}

		private void txtUnSigned_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Home_GoBackTogSignPictureCT_Function home_GoBackTogSignPictureCT_Function = null;
			Home_GoBackTogSignPictureCT_Function home_GoBackTogSignPictureCT_Function2 = null;
			_003C_003Ec__DisplayClasse _003C_003Ec__DisplayClasse = new _003C_003Ec__DisplayClasse();
			string text = "";
			DataTable dataTable = MSCE.GetDataTable("select AllowIpRange from NowLogin");
			if (dataTable.Rows.Count > 0)
			{
				text = dataTable.Rows[0]["AllowIpRange"].ToString();
			}
			if (!text.Equals("") && !IpTool.CheckInNowWifi(text))
			{
				AutoClosingMessageBox.Show("您不在會議室範圍內，無法使用此功能");
				return;
			}
			_003C_003Ec__DisplayClasse.Home_Window = Enumerable.FirstOrDefault(Enumerable.OfType<Home>(Application.Current.Windows));
			if (_003C_003Ec__DisplayClasse.Home_Window == null)
			{
				return;
			}
			string deptID = (signinDataUser.DeptID == null) ? "" : signinDataUser.DeptID;
			if (!signinDataUser.ID.Trim().Equals(""))
			{
				ContentControl cC = _003C_003Ec__DisplayClasse.Home_Window.CC;
				string iD = signinDataUser.ID;
				string name = signinDataUser.Name;
				string signedPic = signinDataUser.SignedPic;
				if (home_GoBackTogSignPictureCT_Function == null)
				{
					home_GoBackTogSignPictureCT_Function = new Home_GoBackTogSignPictureCT_Function(_003C_003Ec__DisplayClasse._003CtxtUnSigned_MouseLeftButtonDown_003Eb__a);
				}
				cC.Content = new SignPadCT(iD, name, deptID, signedPic, home_GoBackTogSignPictureCT_Function);
			}
			else
			{
				ContentControl cC2 = _003C_003Ec__DisplayClasse.Home_Window.CC;
				string userName = string.Format("{0} 來賓", signinDataUser.Dept);
				string signedPic2 = signinDataUser.SignedPic;
				if (home_GoBackTogSignPictureCT_Function2 == null)
				{
					home_GoBackTogSignPictureCT_Function2 = new Home_GoBackTogSignPictureCT_Function(_003C_003Ec__DisplayClasse._003CtxtUnSigned_MouseLeftButtonDown_003Eb__b);
				}
				cC2.Content = new SignPadCT("dept", userName, deptID, signedPic2, home_GoBackTogSignPictureCT_Function2);
			}
		}

		private void InitUI()
		{
			string signListEmptyDash = Settings.Default.SignListEmptyDash;
			if (signinDataUser.Rank != null)
			{
				txtIndex.Text = (signinDataUser.Rank.Equals("") ? signListEmptyDash : signinDataUser.Rank);
			}
			else
			{
				txtIndex.Text = signListEmptyDash;
			}
			txtUserName.Text = (signinDataUser.Name.Equals("") ? signListEmptyDash : signinDataUser.Name);
			txtDept.Text = (signinDataUser.Dept.Equals("") ? signListEmptyDash : signinDataUser.Dept);
			txtTitle.Text = (signinDataUser.Title.Equals("") ? signListEmptyDash : signinDataUser.Title);
			bool.Parse(signinDataUser.IsSigned.Equals("") ? "" : signinDataUser.IsSigned);
			string text = signinDataUser.Attend.Trim();
			string text2 = signinDataUser.AgentName.Trim();
			signinDataUser.SignedPic.Trim();
			if (text.Equals("0") || text.Equals("2"))
			{
				if (!text2.Equals(""))
				{
					txtAgent.Text = string.Format("(指派代表，由{0}出席)", signinDataUser.AgentName);
				}
				else
				{
					txtAgent.Text = "(請假)";
				}
			}
			else
			{
				txtAgent.Visibility = Visibility.Collapsed;
			}
			GetHttpImage();
		}

		private void imgSignedPic_SizeChanged(object sender, SizeChangedEventArgs e)
		{
		}

		private void GetHttpImage()
		{
			Action action = null;
			_003C_003Ec__DisplayClass13 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass13();
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			_003C_003Ec__DisplayClass.imgUrl = signinDataUser.SignedPic.Trim();
			if (!_003C_003Ec__DisplayClass.imgUrl.Equals(""))
			{
				txtUserName.FontWeight = FontWeights.Bold;
				if (sb != null)
				{
					sb.Begin();
				}
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003C_003Ec__DisplayClass._003CGetHttpImage_003Eb__10);
				}
				factory.StartNew(action);
			}
			else if (EnableTxtPLSSigned && meetingUserType == MeetingUserType.議事管理人員)
			{
				txtPLSSigned.Visibility = Visibility.Visible;
			}
			else
			{
				txtUnSigned.Visibility = Visibility.Visible;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/signrow_mix.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				txtIndex = (TextBlock)target;
				break;
			case 2:
				txtDept = (TextBlock)target;
				break;
			case 3:
				txtTitle = (TextBlock)target;
				break;
			case 4:
				txtUserName = (TextBlock)target;
				break;
			case 5:
				txtAgent = (TextBlock)target;
				break;
			case 6:
				txtPicLoading = (TextBlock)target;
				break;
			case 7:
				txtPLSSigned = (TextBlock)target;
				break;
			case 8:
				txtUnSigned = (TextBlock)target;
				break;
			case 9:
				imgSignedPic = (Image)target;
				imgSignedPic.SizeChanged += new SizeChangedEventHandler(imgSignedPic_SizeChanged);
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}

		[CompilerGenerated]
		private void _003CSignRow_Loaded_003Eb__0()
		{
			InitSelectDB();
			base.Dispatcher.BeginInvoke(new Action(_003CSignRow_Loaded_003Eb__1));
		}

		[CompilerGenerated]
		private void _003CSignRow_Loaded_003Eb__1()
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
		private static void _003CInitEvent_003Eb__4(object sender, MouseEventArgs e)
		{
			MouseTool.ShowHand();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__5(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}
	}
}
