using PaperLess_Emeeting.App_Code.Socket;
using PaperlessSync.Broadcast.Socket;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace PaperLess_Emeeting
{
	public class BroadcastRow : UserControl, IComponentConnector
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass7
		{
			public string SyncOwnerID;

			public BroadcastRow _003C_003E4__this;

			public void _003CLightGrid_MouseLeftButtonDown_003Eb__6()
			{
				try
				{
					SocketClient instance = Singleton_Socket.GetInstance(_003C_003E4__this.MeetingID, _003C_003E4__this.UserID, _003C_003E4__this.UserName, false);
					if (instance != null && instance.GetIsConnected())
					{
						instance.setSyncOwner(Socket_FixEmailUserID.ToSocket(SyncOwnerID));
					}
				}
				catch (Exception ex)
				{
					LogTool.Debug(ex);
				}
			}
		}

		internal TextBlock txtUserID;

		internal TextBlock txtUserName;

		internal TextBlock txtUserDevice;

		internal Grid LightGrid;

		internal Image btnLight;

		internal TextBlock txtLight;

		private bool _contentLoaded;

		[CompilerGenerated]
		private string _003CclientId_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CclientName_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CclientType_003Ek__BackingField;

		[CompilerGenerated]
		private string _003Cstatus_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserID_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserName_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CMeetingID_003Ek__BackingField;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate4;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate5;

		public string clientId
		{
			[CompilerGenerated]
			get
			{
				return _003CclientId_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CclientId_003Ek__BackingField = value;
			}
		}

		public string clientName
		{
			[CompilerGenerated]
			get
			{
				return _003CclientName_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CclientName_003Ek__BackingField = value;
			}
		}

		public string clientType
		{
			[CompilerGenerated]
			get
			{
				return _003CclientType_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CclientType_003Ek__BackingField = value;
			}
		}

		public string status
		{
			[CompilerGenerated]
			get
			{
				return _003Cstatus_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003Cstatus_003Ek__BackingField = value;
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

		public BroadcastRow(string clientId, string clientName, string clientType, string status, string UserID, string UserName, string MeetingID)
		{
			InitializeComponent();
			this.clientId = clientId;
			this.clientName = clientName;
			this.clientType = clientType;
			this.status = status;
			this.UserID = UserID;
			this.UserName = UserName;
			this.MeetingID = MeetingID;
			base.Loaded += new RoutedEventHandler(BroadcastRow_Loaded);
		}

		private void BroadcastRow_Loaded(object sender, RoutedEventArgs e)
		{
			Task.Factory.StartNew(new Action(_003CBroadcastRow_Loaded_003Eb__0));
		}

		private void InitSelectDB()
		{
		}

		private void InitEvent()
		{
			Grid lightGrid = LightGrid;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate4 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate4 = new MouseEventHandler(_003CInitEvent_003Eb__2);
			}
			lightGrid.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate4;
			Grid lightGrid2 = LightGrid;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate5 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate5 = new MouseEventHandler(_003CInitEvent_003Eb__3);
			}
			lightGrid2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate5;
			LightGrid.MouseLeftButtonDown += new MouseButtonEventHandler(LightGrid_MouseLeftButtonDown);
		}

		private void LightGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			_003C_003Ec__DisplayClass7 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass7();
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			_003C_003Ec__DisplayClass.SyncOwnerID = "";
			if (txtLight.Text.Equals("關"))
			{
				_003C_003Ec__DisplayClass.SyncOwnerID = clientId;
			}
			Task.Factory.StartNew(new Action(_003C_003Ec__DisplayClass._003CLightGrid_MouseLeftButtonDown_003Eb__6));
		}

		private void InitUI()
		{
			txtUserID.Text = clientId;
			txtUserName.Text = clientName;
			switch (clientType)
			{
			case "1":
				txtUserDevice.Text = "Android";
				break;
			case "2":
				txtUserDevice.Text = "PC";
				break;
			default:
				txtUserDevice.Text = "iOS";
				break;
			}
			if (status.Equals("-1"))
			{
				btnLight.Source = new BitmapImage(new Uri("images/btn_On@2x.png", UriKind.Relative));
				txtLight.Text = "開";
			}
			else
			{
				btnLight.Source = new BitmapImage(new Uri("images/btn_Off@2x.png", UriKind.Relative));
				txtLight.Text = "關";
			}
			LightGrid.Visibility = Visibility.Visible;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/broadcastrow.xaml", UriKind.Relative);
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
				txtUserID = (TextBlock)target;
				break;
			case 2:
				txtUserName = (TextBlock)target;
				break;
			case 3:
				txtUserDevice = (TextBlock)target;
				break;
			case 4:
				LightGrid = (Grid)target;
				break;
			case 5:
				btnLight = (Image)target;
				break;
			case 6:
				txtLight = (TextBlock)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}

		[CompilerGenerated]
		private void _003CBroadcastRow_Loaded_003Eb__0()
		{
			InitSelectDB();
			base.Dispatcher.BeginInvoke(new Action(_003CBroadcastRow_Loaded_003Eb__1));
		}

		[CompilerGenerated]
		private void _003CBroadcastRow_Loaded_003Eb__1()
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
	}
}
