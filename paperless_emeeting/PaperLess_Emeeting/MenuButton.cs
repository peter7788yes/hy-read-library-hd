using PaperLess_Emeeting.App_Code.MessageBox;
using PaperLess_Emeeting.App_Code.Socket;
using PaperLess_Emeeting.Properties;
using PaperLess_ViewModel;
using PaperlessSync.Broadcast.Socket;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace PaperLess_Emeeting
{
	public class MenuButton : UserControl, IComponentConnector
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass6
		{
			public bool syncSwitch;

			public MenuButton _003C_003E4__this;

			public void _003CUserControl_MouseLeftButtonDown_003Eb__4()
			{
				AutoClosingMessageBox.Show("連線中");
				int num = 1;
				SocketClient instance;
				while (true)
				{
					if (num <= 10)
					{
						instance = Singleton_Socket.GetInstance(_003C_003E4__this.MeetingID, _003C_003E4__this.UserID, _003C_003E4__this.UserName, syncSwitch);
						Thread.Sleep(1);
						if (instance != null && instance.GetIsConnected())
						{
							break;
						}
						Singleton_Socket.ClearInstance();
						if (num == 10)
						{
							AutoClosingMessageBox.Show("同步伺服器尚未啟動，請聯絡議事管理員開啟同步");
						}
						Thread.Sleep(10);
						num++;
						continue;
					}
					return;
				}
				instance.syncSwitch(syncSwitch);
			}
		}

		internal Image btnImg;

		internal TextBlock txtBtnName;

		private bool _contentLoaded;

		[CompilerGenerated]
		private UserButton _003CuserButton_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserID_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserName_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CMeetingID_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CAllowIpRange_003Ek__BackingField;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate2;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate3;

		public UserButton userButton
		{
			[CompilerGenerated]
			get
			{
				return _003CuserButton_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CuserButton_003Ek__BackingField = value;
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

		public string AllowIpRange
		{
			[CompilerGenerated]
			get
			{
				return _003CAllowIpRange_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CAllowIpRange_003Ek__BackingField = value;
			}
		}

		public event Home_ReturnSyncStatus_Function Home_ReturnSyncStatus_Event;

		public event Home_ChangeCC_Function Home_ChangeCC_Event;

		public event Home_ChangeBtnSP_Function Home_ChangeBtnSP_Event;

		public event Home_PopUpButtons_Function Home_PopUpButtons_Event;

		public MenuButton(UserButton userButton, Home_ChangeCC_Function callback1, Home_PopUpButtons_Function callback2, Home_ChangeBtnSP_Function callback3 = null)
		{
			InitializeComponent();
			Home_ChangeCC_Event += callback1;
			Home_PopUpButtons_Event += callback2;
			Home_ChangeBtnSP_Event += callback3;
			this.userButton = userButton;
			base.Loaded += new RoutedEventHandler(MenuButton_Loaded);
			base.Unloaded += new RoutedEventHandler(MenuButton_Unloaded);
			InitSelectDB();
			InitUI();
			InitEvent();
		}

		private void MenuButton_Unloaded(object sender, RoutedEventArgs e)
		{
		}

		private void MenuButton_Loaded(object sender, RoutedEventArgs e)
		{
		}

		private void InitSelectDB()
		{
			if (userButton.ID.Equals("BtnSync") || userButton.ID.Equals("BtnIndividualSign") || userButton.ID.Equals("BtnBroadcast"))
			{
				DataTable dataTable = MSCE.GetDataTable("select UserID,UserName,UserPWD,MeetingID,AllowIpRange from NowLogin");
				if (dataTable.Rows.Count > 0)
				{
					UserID = dataTable.Rows[0]["UserID"].ToString();
					UserName = dataTable.Rows[0]["UserName"].ToString();
					MeetingID = dataTable.Rows[0]["MeetingID"].ToString();
					AllowIpRange = dataTable.Rows[0]["AllowIpRange"].ToString();
				}
			}
		}

		private void InitEvent()
		{
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate2 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate2 = new MouseEventHandler(_003CInitEvent_003Eb__0);
			}
			base.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate2;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate3 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate3 = new MouseEventHandler(_003CInitEvent_003Eb__1);
			}
			base.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate3;
			base.MouseLeftButtonDown += new MouseButtonEventHandler(UserControl_MouseLeftButtonDown);
		}

		private void InitUI()
		{
			btnImg.Source = ButtonTool.GetButtonImage(userButton.ID);
			txtBtnName.Text = userButton.Name;
			if (userButton.ID.Equals("BtnSync"))
			{
				base.Height = 55.0;
				base.Width = 60.0;
				btnImg.Height = 55.0;
				btnImg.Width = 60.0;
				txtBtnName.Text = "";
			}
		}

		private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (userButton.ID.Equals("BtnSync"))
			{
				if (!AllowIpRange.Equals("") && !IpTool.CheckInNowWifi(AllowIpRange))
				{
					string courseOrMeeting_String = Settings.Default.CourseOrMeeting_String;
					AutoClosingMessageBox.Show(string.Format("您不在{0}室範圍內，無法使用此功能", courseOrMeeting_String.Equals("課程") ? "教" : courseOrMeeting_String));
					return;
				}
				if (this.Home_ReturnSyncStatus_Event != null)
				{
					_003C_003Ec__DisplayClass6 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass6();
					_003C_003Ec__DisplayClass._003C_003E4__this = this;
					Tuple<bool, bool> tuple = this.Home_ReturnSyncStatus_Event();
					_003C_003Ec__DisplayClass.syncSwitch = false;
					if (!tuple.Item1)
					{
						int num = 0;
						DataTable dataTable = MSCE.GetDataTable("select count(ID) as FileNotFinished from NowLogin as nl\r\n                                                       inner join FileRow as fr on nl.UserID=fr.UserID and nl.MeetingID=fr.MeetingID\r\n                                                       where DownloadBytes=0 or DownloadBytes<TotalBytes");
						if (dataTable.Rows.Count > 0)
						{
							num = (int)dataTable.Rows[0]["FileNotFinished"];
						}
						if (num > 0)
						{
							AutoClosingMessageBox.Show(string.Format("請將{0}資料下載完成後，再同步", Settings.Default.CourseOrMeeting_String));
							return;
						}
						_003C_003Ec__DisplayClass.syncSwitch = true;
					}
					else
					{
						_003C_003Ec__DisplayClass.syncSwitch = false;
					}
					btnImg.Source = ButtonTool.GetSyncButtonImage(tuple.Item1, tuple.Item2);
					Task.Factory.StartNew(new Action(_003C_003Ec__DisplayClass._003CUserControl_MouseLeftButtonDown_003Eb__4));
				}
			}
			else
			{
				if ((userButton.ID.Equals("BtnIndividualSign") || userButton.ID.Equals("BtnBroadcast")) && !AllowIpRange.Equals("") && !IpTool.CheckInNowWifi(AllowIpRange))
				{
					string courseOrMeeting_String2 = Settings.Default.CourseOrMeeting_String;
					AutoClosingMessageBox.Show(string.Format("您不在{0}室範圍內，無法使用此功能", courseOrMeeting_String2.Equals("課程") ? "教" : courseOrMeeting_String2));
					return;
				}
				btnImg.Source = ButtonTool.GetButtonImage(userButton.ID, true);
				if (this.Home_PopUpButtons_Event != null)
				{
					this.Home_PopUpButtons_Event(userButton.ID);
				}
			}
			Task.Factory.StartNew(new Action(_003CUserControl_MouseLeftButtonDown_003Eb__5));
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/menubutton.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				btnImg = (Image)target;
				break;
			case 2:
				txtBtnName = (TextBlock)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__0(object sender, MouseEventArgs e)
		{
			MouseTool.ShowHand();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__1(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private void _003CUserControl_MouseLeftButtonDown_003Eb__5()
		{
			this.Home_ChangeCC_Event(userButton.ID);
		}
	}
}
