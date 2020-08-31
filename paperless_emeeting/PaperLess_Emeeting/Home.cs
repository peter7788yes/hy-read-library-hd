using AES_ECB_PKCS5;
using AutoLogOffInWPF;
using BookManagerModule;
using DataAccessObject;
using Network;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PaperLess_Emeeting.App_Code;
using PaperLess_Emeeting.App_Code.ClickOnce;
using PaperLess_Emeeting.App_Code.MessageBox;
using PaperLess_Emeeting.App_Code.Socket;
using PaperLess_Emeeting.App_Code.ViewModel;
using PaperLess_Emeeting.App_Code.WS;
using PaperLess_Emeeting.Properties;
using PaperLess_ViewModel;
using PaperlessSync.Broadcast.Socket;
using SyncCenterModule;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace PaperLess_Emeeting
{
	public class Home : Window, IComponentConnector
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass10
		{
			public Home _003C_003E4__this;

			public string dbPath;

			public string bookId;

			public string account;

			public string meetingId;

			public void _003CInitSyncCenter_003Eb__e()
			{
				try
				{
					SyncCenter syncCenter = new SyncCenter();
					syncCenter.bookManager = new BookManager(dbPath);
					int bookPath = _003C_003E4__this.getBookPath(dbPath, bookId, account, meetingId);
					Dictionary<string, object> dictionary = new Dictionary<string, object>();
					dictionary.Add("SBookmark", new BookMarkData());
					dictionary.Add("SAnnotation", new NoteData());
					dictionary.Add("SSpline", new StrokesData());
					dictionary.Add("SLastPage", new LastPageData());
					Dictionary<string, object> dictionary2 = dictionary;
					foreach (KeyValuePair<string, object> item in dictionary2)
					{
						string key = item.Key;
						Type typeFromHandle = typeof(SyncManager<>);
						Type type = typeFromHandle.MakeGenericType(item.Value.GetType());
						AbstractSyncManager syncManager = (AbstractSyncManager)Activator.CreateInstance(type, account, "free", bookId, bookPath, key, 0, "0", WsTool.GetAbstractSyncCenter_BASE_URL());
						syncCenter.addSyncConditions(key, syncManager);
					}
				}
				catch (Exception ex)
				{
					LogTool.Debug(ex);
				}
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass18
		{
			public Home _003C_003E4__this;

			public string InitMsg;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass1d
		{
			public _003C_003Ec__DisplayClass18 CS_0024_003C_003E8__locals19;

			public string _bookPath;

			public string _bookId;

			public string _account;

			public string _userName;

			public string _email;

			public string _meetingId;

			public string _watermark;

			public string _dbPath;

			public bool _isSync;

			public bool _isSyncOwner;

			public string _webServiceUrl;

			public string _socketMessage;

			public SocketClient _socket;

			public Dictionary<string, BookVM> cbBooksData;

			public void _003COpenBook_003Eb__12()
			{
				CS_0024_003C_003E8__locals19._003C_003E4__this.Dispatcher.BeginInvoke(new Action(_003COpenBook_003Eb__13));
			}

			public void _003COpenBook_003Eb__14()
			{
				CS_0024_003C_003E8__locals19._003C_003E4__this.Dispatcher.BeginInvoke(new Action(_003COpenBook_003Eb__15));
			}

			public void _003COpenBook_003Eb__16()
			{
				CS_0024_003C_003E8__locals19._003C_003E4__this.Dispatcher.BeginInvoke(new Action(_003COpenBook_003Eb__17));
			}

			public void _003COpenBook_003Eb__13()
			{
				byte[] defaultKey = new byte[1];
				try
				{
					DataTable dataTable = MSCE.GetDataTable("SELECT EncryptionKey FROM FileRow where ID=@1 and UserID=@2 and MeetingID=@3", _bookId, _account, _meetingId);
					if (dataTable.Rows.Count > 0)
					{
						defaultKey = ReaderDecodeTool.GetReaderKey(dataTable.Rows[0]["EncryptionKey"].ToString());
					}
				}
				catch (Exception ex)
				{
					LogTool.Debug(ex);
				}
				ReadWindow readWindow = new ReadWindow(cbBooksData, new Home_OpenBookFromReader_Function(CS_0024_003C_003E8__locals19._003C_003E4__this.OpenBookFromReader), _bookPath, _bookId, _account, _userName, _email, _meetingId, _watermark, _dbPath, _isSync, _isSyncOwner, _webServiceUrl, defaultKey, _socketMessage, _socket);
				readWindow.WindowStyle = WindowStyle.None;
				readWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				readWindow.WindowState = WindowState.Maximized;
				Settings.Default.AssemblyName.Contains("TPI4F");
				readWindow.Show();
			}

			public void _003COpenBook_003Eb__15()
			{
				HTML5ReadWindow hTML5ReadWindow = new HTML5ReadWindow(cbBooksData, new Home_OpenBookFromReader_Function(CS_0024_003C_003E8__locals19._003C_003E4__this.OpenBookFromReader), _bookPath, _bookId, _account, _userName, _email, _meetingId, _watermark, _dbPath, _isSync, _isSyncOwner, _webServiceUrl, _socketMessage, _socket);
				hTML5ReadWindow.WindowStyle = WindowStyle.None;
				hTML5ReadWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				hTML5ReadWindow.WindowState = WindowState.Maximized;
				Settings.Default.AssemblyName.Contains("TPI4F");
				hTML5ReadWindow.Show();
			}

			public void _003COpenBook_003Eb__17()
			{
				MVWindow mVWindow = new MVWindow(cbBooksData, new Home_OpenBookFromReader_Function(CS_0024_003C_003E8__locals19._003C_003E4__this.OpenBookFromReader), _bookPath, CS_0024_003C_003E8__locals19.InitMsg);
				mVWindow.WindowStyle = WindowStyle.None;
				mVWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				mVWindow.WindowState = WindowState.Maximized;
				Settings.Default.AssemblyName.Contains("TPI4F");
				mVWindow.Show();
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass28
		{
			public Home _003C_003E4__this;

			public Action Callback;

			public void _003CShowMeetingListCT_003Eb__22(User userObj, DateTime dateTime)
			{
				Action action = null;
				_003C_003E4__this.GetUserData_DoAction(userObj, dateTime);
				if (Callback != null)
				{
					Dispatcher dispatcher = _003C_003E4__this.Dispatcher;
					if (action == null)
					{
						action = new Action(_003CShowMeetingListCT_003Eb__23);
					}
					dispatcher.BeginInvoke(action);
				}
			}

			public void _003CShowMeetingListCT_003Eb__23()
			{
				Callback();
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass39
		{
			public Home _003C_003E4__this;

			public SigninData sid;

			public void _003CGetSigninData_DoAction_003Eb__36()
			{
				_003C_003E4__this.Dispatcher.BeginInvoke(new Action(_003CGetSigninData_DoAction_003Eb__37));
			}

			public void _003CGetSigninData_DoAction_003Eb__37()
			{
				SigninDataUser[] userList = sid.UserList;
				foreach (SigninDataUser signinDataUser in userList)
				{
					if (signinDataUser.ID.Equals(_003C_003E4__this.UserID))
					{
						_003C_003E4__this.CC.Content = new SignPadCT("", "", "", signinDataUser.SignedPic);
					}
				}
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass42
		{
			public Home _003C_003E4__this;

			public string NowSeriesID;

			public void _003CShowBtnSeriesCT_003Eb__40(SeriesData sd)
			{
				_003C_003E4__this.GetSeriesData_DoAction(sd, NowSeriesID);
				_003C_003E4__this.PreLoadSeriesData();
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass47
		{
			public Home _003C_003E4__this;

			public SeriesData sd;

			public string NowSeriesID;

			public void _003CGetSeriesData_DoAction_003Eb__44()
			{
				_003C_003E4__this.Dispatcher.BeginInvoke(new Action(_003CGetSeriesData_DoAction_003Eb__45));
			}

			public void _003CGetSeriesData_DoAction_003Eb__45()
			{
				_003C_003E4__this.CC.Content = new SeriesMeetingCT(_003C_003E4__this.UserID, _003C_003E4__this.UserPWD, sd, NowSeriesID);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass51
		{
			public Home _003C_003E4__this;

			public string MeetingID;

			public Dictionary<string, BookVM> cbBooksData;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass56
		{
			public _003C_003Ec__DisplayClass51 CS_0024_003C_003E8__locals52;

			public IEnumerable<ReadWindow> rws;

			public IEnumerable<HTML5ReadWindow> html5rws;

			public IEnumerable<MVWindow> mvWindows;

			public string _bookPath;

			public string _bookId;

			public string _account;

			public string _userName;

			public string _email;

			public string _meetingId;

			public string _watermark;

			public string _dbPath;

			public bool _isSync;

			public bool _isSyncOwner;

			public string _webServiceUrl;

			public string _socketMessage;

			public SocketClient _socket;

			public void _003COpenBookFromReader_003Eb__49()
			{
				foreach (ReadWindow rw in rws)
				{
					if (rw != null)
					{
						rw.RecordPage();
						rw.Hide();
						rw.Close();
					}
				}
				foreach (HTML5ReadWindow html5rw in html5rws)
				{
					if (html5rw != null)
					{
						html5rw.RecordPage();
						html5rw.Hide();
						html5rw.Close();
					}
				}
				foreach (MVWindow mvWindow in mvWindows)
				{
					if (mvWindow != null)
					{
						mvWindow.Hide();
						mvWindow.Close();
					}
				}
			}

			public void _003COpenBookFromReader_003Eb__4a()
			{
				CS_0024_003C_003E8__locals52._003C_003E4__this.Dispatcher.BeginInvoke(new Action(_003COpenBookFromReader_003Eb__4b));
			}

			public void _003COpenBookFromReader_003Eb__4c()
			{
				CS_0024_003C_003E8__locals52._003C_003E4__this.Dispatcher.BeginInvoke(new Action(_003COpenBookFromReader_003Eb__4d));
			}

			public void _003COpenBookFromReader_003Eb__4e()
			{
				CS_0024_003C_003E8__locals52._003C_003E4__this.Dispatcher.BeginInvoke(new Action(_003COpenBookFromReader_003Eb__4f));
			}

			public void _003COpenBookFromReader_003Eb__4b()
			{
				byte[] defaultKey = new byte[1];
				try
				{
					DataTable dataTable = MSCE.GetDataTable("SELECT EncryptionKey FROM FileRow where ID=@1 and UserID=@2 and MeetingID=@3", _bookId, _account, _meetingId);
					if (dataTable.Rows.Count > 0)
					{
						defaultKey = ReaderDecodeTool.GetReaderKey(dataTable.Rows[0]["EncryptionKey"].ToString());
					}
				}
				catch (Exception ex)
				{
					LogTool.Debug(ex);
				}
				ReadWindow readWindow = new ReadWindow(CS_0024_003C_003E8__locals52.cbBooksData, new Home_OpenBookFromReader_Function(CS_0024_003C_003E8__locals52._003C_003E4__this.OpenBookFromReader), _bookPath, _bookId, _account, _userName, _email, _meetingId, _watermark, _dbPath, _isSync, _isSyncOwner, _webServiceUrl, defaultKey, _socketMessage, _socket);
				readWindow.WindowStyle = WindowStyle.None;
				readWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				readWindow.WindowState = WindowState.Maximized;
				Settings.Default.AssemblyName.Contains("TPI4F");
				readWindow.Show();
			}

			public void _003COpenBookFromReader_003Eb__4d()
			{
				HTML5ReadWindow hTML5ReadWindow = new HTML5ReadWindow(CS_0024_003C_003E8__locals52.cbBooksData, new Home_OpenBookFromReader_Function(CS_0024_003C_003E8__locals52._003C_003E4__this.OpenBookFromReader), _bookPath, _bookId, _account, _userName, _email, _meetingId, _watermark, _dbPath, _isSync, _isSyncOwner, _webServiceUrl, _socketMessage, _socket);
				hTML5ReadWindow.WindowStyle = WindowStyle.None;
				hTML5ReadWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				hTML5ReadWindow.WindowState = WindowState.Maximized;
				Settings.Default.AssemblyName.Contains("TPI4F");
				hTML5ReadWindow.Show();
			}

			public void _003COpenBookFromReader_003Eb__4f()
			{
				MVWindow mVWindow = new MVWindow(CS_0024_003C_003E8__locals52.cbBooksData, new Home_OpenBookFromReader_Function(CS_0024_003C_003E8__locals52._003C_003E4__this.OpenBookFromReader), _bookPath, _bookId);
				mVWindow.WindowStyle = WindowStyle.None;
				mVWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				mVWindow.WindowState = WindowState.Maximized;
				Settings.Default.AssemblyName.Contains("TPI4F");
				mVWindow.Show();
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass58
		{
			public _003C_003Ec__DisplayClass56 CS_0024_003C_003E8__locals57;

			public _003C_003Ec__DisplayClass51 CS_0024_003C_003E8__locals52;

			public SocketClient socketClient;

			public void _003COpenBookFromReader_003Eb__50()
			{
				if (socketClient != null && socketClient.GetIsConnected())
				{
					string msg = "{\"bookId\":\"" + CS_0024_003C_003E8__locals52.MeetingID + "\",\"cmd\":\"R.OB\"}";
					socketClient.broadcast(msg);
				}
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass5b
		{
			public Home _003C_003E4__this;

			public string DeptID;

			public string PicUrl;

			public void _003CShowSignPictureCT_003Eb__5a(string UserID, string UserName)
			{
				_003C_003E4__this.CC.Content = new SignPadCT(UserID, UserName, DeptID, PicUrl, new Home_GoBackTogSignPictureCT_Function(_003C_003E4__this.ShowSignPictureCT));
			}
		}

		private Home_UnZipError_Function UnZipError_callback;

		public Home_OpenBook_Function Home_OpenBook_Callback;

		public Home_IsInSync_And_IsSyncOwner_Function Home_IsInSync_And_IsSyncOwner_Callback;

		public Home_CloseAllWindow_Function Home_CloseAllWindow_Callback;

		public Home_TurnOffSyncButton_Function Home_TurnOffSyncButton_Callback;

		public Home_SetSocketClientNull_Function Home_SetSocketClientNull_Callback;

		public Home_Change2MeetingDataCT_Function Home_Change2MeetingDataCT_Callback;

		public Home_OpenBookFromReader_Function Home_OpenBookFromReader_Callback;

		private DisplayUserNameMode displayUserNameMode;

		public string NowPressButtonID = "BtnHome";

		public Dictionary<string, SeriesData> PreLoadSeriesDataDict = new Dictionary<string, SeriesData>();

		public int CacheMinuteTTL;

		public Thread CacheThread;

		internal RowDefinition Row1;

		internal Image imgBeta;

		internal DockPanel DP1;

		internal Image imgLogo;

		internal Grid blockUserName;

		internal TextBlock txtUserName;

		internal StackPanel btnSP;

		internal ContentControl CC;

		private bool _contentLoaded;

		[CompilerGenerated]
		private User _003Cuser_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserID_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserName_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserPWD_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserEmail_003Ek__BackingField;

		[CompilerGenerated]
		private DateTime _003CMeetingListDate_003Ek__BackingField;

		[CompilerGenerated]
		private UserButton[] _003CUserButtonAry_003Ek__BackingField;

		[CompilerGenerated]
		private UserMeeting[] _003CUserMeetingAry_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CSyncMeetingID_003Ek__BackingField;

		[CompilerGenerated]
		private bool _003CIsInSync_003Ek__BackingField;

		[CompilerGenerated]
		private bool _003CIsSyncOwner_003Ek__BackingField;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegatea;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegateb;

		[CompilerGenerated]
		private static Action CS_0024_003C_003E9__CachedAnonymousMethodDelegate35;

		public User user
		{
			[CompilerGenerated]
			get
			{
				return _003Cuser_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003Cuser_003Ek__BackingField = value;
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

		public string UserPWD
		{
			[CompilerGenerated]
			get
			{
				return _003CUserPWD_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CUserPWD_003Ek__BackingField = value;
			}
		}

		public string UserEmail
		{
			[CompilerGenerated]
			get
			{
				return _003CUserEmail_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CUserEmail_003Ek__BackingField = value;
			}
		}

		public DateTime MeetingListDate
		{
			[CompilerGenerated]
			get
			{
				return _003CMeetingListDate_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CMeetingListDate_003Ek__BackingField = value;
			}
		}

		public UserButton[] UserButtonAry
		{
			[CompilerGenerated]
			get
			{
				return _003CUserButtonAry_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CUserButtonAry_003Ek__BackingField = value;
			}
		}

		public UserMeeting[] UserMeetingAry
		{
			[CompilerGenerated]
			get
			{
				return _003CUserMeetingAry_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CUserMeetingAry_003Ek__BackingField = value;
			}
		}

		public string SyncMeetingID
		{
			[CompilerGenerated]
			get
			{
				return _003CSyncMeetingID_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CSyncMeetingID_003Ek__BackingField = value;
			}
		}

		public bool IsInSync
		{
			[CompilerGenerated]
			get
			{
				return _003CIsInSync_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CIsInSync_003Ek__BackingField = value;
			}
		}

		public bool IsSyncOwner
		{
			[CompilerGenerated]
			get
			{
				return _003CIsSyncOwner_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CIsSyncOwner_003Ek__BackingField = value;
			}
		}

		public Home(User user, string UserPWD)
		{
			MouseTool.ShowLoading();
			base.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			InitializeComponent();
			this.user = user;
			this.UserPWD = UserPWD;
			MeetingListDate = DateTime.Now;
			UserButtonAry = user.EnableButtonList;
			UserMeetingAry = user.MeetingList;
			base.Loaded += new RoutedEventHandler(Home_Loaded);
			base.Unloaded += new RoutedEventHandler(Home_Unloaded);
			CacheMinuteTTL = Settings.Default.CacheMinuteTTL;
		}

		private void Home_Unloaded(object sender, RoutedEventArgs e)
		{
			StopAllBackground();
		}

		private void Home_Loaded(object sender, RoutedEventArgs e)
		{
			Task.Factory.StartNew(new Action(_003CHome_Loaded_003Eb__0));
			try
			{
				InitializeAutoLogoffFeature();
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}

		private void PreLoadSeriesData()
		{
			Action action = null;
			if (CacheMinuteTTL >= 0)
			{
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003CPreLoadSeriesData_003Eb__2);
				}
				factory.StartNew(action);
			}
		}

		private void InitSelectDB()
		{
			UserID = user.ID;
			UserName = user.Name;
			UserEmail = user.Email;
		}

		private void InitEvent()
		{
			MouseButtonEventHandler mouseButtonEventHandler = null;
			LawDownloader instance = Singleton_LawDownloader.GetInstance();
			UnZipError_callback = new Home_UnZipError_Function(Home_UnZipError_Callback);
			instance.Home_UnZipError_Event += UnZipError_callback;
			Singleton_FileDownloader.Home_UnZipError_Callback = UnZipError_callback;
			Home_OpenBook_Callback = new Home_OpenBook_Function(OpenBook);
			Home_IsInSync_And_IsSyncOwner_Callback = new Home_IsInSync_And_IsSyncOwner_Function(IsInSync_And_IsSyncOwner);
			Home_CloseAllWindow_Callback = new Home_CloseAllWindow_Function(CloseAllWindow);
			Home_TurnOffSyncButton_Callback = new Home_TurnOffSyncButton_Function(TurnOffSyncButton);
			Home_SetSocketClientNull_Callback = new Home_SetSocketClientNull_Function(SetSocketClientNull);
			Home_Change2MeetingDataCT_Callback = new Home_Change2MeetingDataCT_Function(Change2MeetingDataCT);
			Home_OpenBookFromReader_Callback = new Home_OpenBookFromReader_Function(OpenBookFromReader);
			Singleton_Socket.home_OpenIEventManager.Home_OpenBook_Event += Home_OpenBook_Callback;
			Singleton_Socket.home_OpenIEventManager.Home_IsInSync_And_IsSyncOwner_Event += Home_IsInSync_And_IsSyncOwner_Callback;
			Singleton_Socket.home_OpenIEventManager.Home_CloseAllWindow_Event += Home_CloseAllWindow_Callback;
			Singleton_Socket.home_CloseIEventManager.Home_CloseAllWindow_Event += Home_CloseAllWindow_Callback;
			Singleton_Socket.home_CloseIEventManager.Home_TurnOffSyncButton_Event += Home_TurnOffSyncButton_Callback;
			Singleton_Socket.home_CloseIEventManager.Home_SetSocketClientNull_Event += Home_SetSocketClientNull_Callback;
			if (Settings.Default.IsDebugMode)
			{
				TextBlock textBlock = txtUserName;
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegatea == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegatea = new MouseEventHandler(_003CInitEvent_003Eb__7);
				}
				textBlock.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegatea;
				TextBlock textBlock2 = txtUserName;
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegateb == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegateb = new MouseEventHandler(_003CInitEvent_003Eb__8);
				}
				textBlock2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegateb;
				TextBlock textBlock3 = txtUserName;
				if (mouseButtonEventHandler == null)
				{
					mouseButtonEventHandler = new MouseButtonEventHandler(_003CInitEvent_003Eb__9);
				}
				textBlock3.MouseLeftButtonDown += mouseButtonEventHandler;
			}
		}

		private void SetSocketClientNull()
		{
			Singleton_Socket.ClearInstance();
		}

		private int getBookPath(string dbPath, string bookId, string account, string meetingId)
		{
			int result = 0;
			try
			{
				BookManager bookManager = new BookManager(dbPath);
				result = getUserBookSno(dbPath, bookId, account, meetingId);
				if (!result.Equals(-1))
				{
					return result;
				}
				string text = "Insert into bookInfo( bookId, account, meetingId )";
				string text2 = text;
				text = text2 + " values('" + bookId + "', '" + account + "', '" + meetingId + "')";
				bookManager.sqlCommandNonQuery(text);
				result = getBookPath(dbPath, bookId, account, meetingId);
				return result;
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
				return result;
			}
		}

		public int getUserBookSno(string dbPath, string bookId, string account, string meetingId)
		{
			string sqlCommand = "Select sno from bookInfo as bi Where bi.bookId ='" + bookId + "' And bi.account ='" + account + "' And bi.meetingId='" + meetingId + "' ";
			QueryResult queryResult = null;
			try
			{
				BookManager bookManager = new BookManager(dbPath);
				queryResult = bookManager.sqlCommandQuery(sqlCommand);
				int result = -1;
				if (queryResult.fetchRow())
				{
					result = queryResult.getInt("sno");
				}
				return result;
			}
			catch
			{
				return -1;
			}
		}

		private void InitSyncCenter(string dbPath, string bookId, string account, string meetingId)
		{
			Action action = null;
			_003C_003Ec__DisplayClass10 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass10();
			_003C_003Ec__DisplayClass.dbPath = dbPath;
			_003C_003Ec__DisplayClass.bookId = bookId;
			_003C_003Ec__DisplayClass.account = account;
			_003C_003Ec__DisplayClass.meetingId = meetingId;
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			if (Settings.Default.HasSyncCenterModule)
			{
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003C_003Ec__DisplayClass._003CInitSyncCenter_003Eb__e);
				}
				factory.StartNew(action);
			}
		}

		private void OpenBook(string BookID, string InitMsg)
		{
			_003C_003Ec__DisplayClass18 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass18();
			_003C_003Ec__DisplayClass.InitMsg = InitMsg;
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<string, string>(OpenBook), BookID, _003C_003Ec__DisplayClass.InitMsg);
			}
			else
			{
				try
				{
					Action action = null;
					Action action2 = null;
					Action action3 = null;
					_003C_003Ec__DisplayClass1d _003C_003Ec__DisplayClass1d = new _003C_003Ec__DisplayClass1d();
					_003C_003Ec__DisplayClass1d.CS_0024_003C_003E8__locals19 = _003C_003Ec__DisplayClass;
					DataTable dataTable = MSCE.GetDataTable("select fr.DownloadBytes, fr.TotalBytes, fr.MeetingID ,fr.FileVersion ,fr.FinishedFileVersion from FileRow as fr \r\n                                                inner join NowLogin as nl on  nl.MeetingID = fr.MeetingID\r\n                                                where  fr.id=@1 and  fr.UserID=@2", BookID, UserID);
					if (dataTable.Rows.Count < 0)
					{
						AutoClosingMessageBox.Show(string.Format("尚未下載{0}", BookID));
					}
					else
					{
						double num = double.Parse(dataTable.Rows[0]["DownloadBytes"].ToString());
						double num2 = double.Parse(dataTable.Rows[0]["TotalBytes"].ToString());
						string text = dataTable.Rows[0]["MeetingID"].ToString();
						int.Parse((dataTable.Rows[0]["FileVersion"].ToString().Equals("") || dataTable.Rows[0]["FileVersion"].ToString().Equals("0")) ? "1" : dataTable.Rows[0]["FileVersion"].ToString());
						int num3 = int.Parse(dataTable.Rows[0]["FinishedFileVersion"].ToString().Equals("") ? "0" : dataTable.Rows[0]["FileVersion"].ToString());
						if (num < num2)
						{
							string text2 = "";
							text2 = ((num3 <= 0) ? string.Format("尚未下載檔案: {0}", BookID) : string.Format("尚未更新檔案: {0}", BookID));
							AutoClosingMessageBox.Show(text2);
						}
						else
						{
							string filePath = ClickOnceTool.GetFilePath();
							string file_UnZipFileFolder = Settings.Default.File_UnZipFileFolder;
							_003C_003Ec__DisplayClass1d._bookPath = filePath + "\\" + file_UnZipFileFolder + "\\" + UserID + "\\" + text + "\\" + BookID;
							_003C_003Ec__DisplayClass1d._bookPath = filePath + "\\" + file_UnZipFileFolder + "\\" + UserID + "\\" + text + "\\" + BookID + "\\" + num3;
							_003C_003Ec__DisplayClass1d._bookId = BookID;
							_003C_003Ec__DisplayClass1d._account = UserID;
							_003C_003Ec__DisplayClass1d._userName = UserName;
							_003C_003Ec__DisplayClass1d._email = UserEmail;
							_003C_003Ec__DisplayClass1d._meetingId = text;
							_003C_003Ec__DisplayClass1d._watermark = "";
							_003C_003Ec__DisplayClass1d._dbPath = Path.Combine(ClickOnceTool.GetDataPath(), Settings.Default.bookInfo_Path);
							_003C_003Ec__DisplayClass1d._isSync = (!_003C_003Ec__DisplayClass.InitMsg.Equals("") || IsInSync);
							_003C_003Ec__DisplayClass1d._isSyncOwner = IsSyncOwner;
							_003C_003Ec__DisplayClass1d._webServiceUrl = WsTool.GetUrl() + "/AnnotationUpload";
							_003C_003Ec__DisplayClass1d._socketMessage = _003C_003Ec__DisplayClass.InitMsg;
							_003C_003Ec__DisplayClass1d._socket = Singleton_Socket.GetInstance(text, UserID, UserName, true);
							_003C_003Ec__DisplayClass1d.cbBooksData = new Dictionary<string, BookVM>();
							dataTable = MSCE.GetDataTable("select ID,DisplayFileName from FileRow where id=@1 and UserID=@2 and MeetingID=@3", BookID, UserID, text);
							MeetingFileCate meetingFileCate = MeetingFileCate.電子書;
							switch (Enumerable.Last(BookID.Split('-')))
							{
							case "P":
								meetingFileCate = MeetingFileCate.電子書;
								break;
							case "H":
								meetingFileCate = MeetingFileCate.Html5投影片;
								break;
							case "V":
								meetingFileCate = MeetingFileCate.影片檔;
								break;
							}
							if (dataTable.Rows.Count > 0)
							{
								string str = filePath + "\\" + file_UnZipFileFolder + "\\" + UserID + "\\" + text;
								foreach (DataRow row in dataTable.Rows)
								{
									_003C_003Ec__DisplayClass1d.cbBooksData[row["DisplayFileName"].ToString()] = new BookVM(BookID, str + "\\" + row["ID"].ToString(), meetingFileCate);
								}
							}
							switch (meetingFileCate)
							{
							case MeetingFileCate.電子書:
							{
								InitSyncCenter(_003C_003Ec__DisplayClass1d._dbPath, _003C_003Ec__DisplayClass1d._bookId, _003C_003Ec__DisplayClass1d._account, _003C_003Ec__DisplayClass1d._meetingId);
								TaskFactory factory2 = Task.Factory;
								if (action == null)
								{
									action = new Action(_003C_003Ec__DisplayClass1d._003COpenBook_003Eb__12);
								}
								factory2.StartNew(action);
								break;
							}
							case MeetingFileCate.Html5投影片:
							{
								_003C_003Ec__DisplayClass1d._bookPath = _003C_003Ec__DisplayClass1d._bookPath + "\\" + new FileInfo(Directory.GetFiles(_003C_003Ec__DisplayClass1d._bookPath)[0]).Name;
								InitSyncCenter(_003C_003Ec__DisplayClass1d._dbPath, _003C_003Ec__DisplayClass1d._bookId, _003C_003Ec__DisplayClass1d._account, _003C_003Ec__DisplayClass1d._meetingId);
								TaskFactory factory3 = Task.Factory;
								if (action2 == null)
								{
									action2 = new Action(_003C_003Ec__DisplayClass1d._003COpenBook_003Eb__14);
								}
								factory3.StartNew(action2);
								break;
							}
							case MeetingFileCate.影片檔:
							{
								_003C_003Ec__DisplayClass1d._bookPath = _003C_003Ec__DisplayClass1d._bookPath + "\\" + new FileInfo(Directory.GetFiles(_003C_003Ec__DisplayClass1d._bookPath)[0]).Name;
								TaskFactory factory = Task.Factory;
								if (action3 == null)
								{
									action3 = new Action(_003C_003Ec__DisplayClass1d._003COpenBook_003Eb__16);
								}
								factory.StartNew(action3);
								break;
							}
							}
						}
					}
				}
				catch (Exception ex)
				{
					LogTool.Debug(ex);
				}
			}
		}

		private void TurnOffSyncButton()
		{
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action(TurnOffSyncButton));
			}
			else
			{
				foreach (MenuButton item in Enumerable.OfType<MenuButton>(btnSP.Children))
				{
					if (item.userButton.ID.Equals("BtnSync"))
					{
						IsInSync = false;
						IsSyncOwner = false;
						item.btnImg.Source = ButtonTool.GetSyncButtonImage(IsInSync, IsSyncOwner);
					}
				}
			}
		}

		public void CloseAllWindow(string AlertMessage, bool fromInit = false)
		{
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<string, bool>(CloseAllWindow), AlertMessage, fromInit);
			}
			else
			{
				try
				{
					if (IsInSync || fromInit)
					{
						Application current = Application.Current;
						IEnumerable<ReadWindow> enumerable = Enumerable.OfType<ReadWindow>(current.Windows);
						IEnumerable<HTML5ReadWindow> enumerable2 = Enumerable.OfType<HTML5ReadWindow>(current.Windows);
						IEnumerable<MVWindow> enumerable3 = Enumerable.OfType<MVWindow>(current.Windows);
						int num = Enumerable.Count(enumerable) + Enumerable.Count(enumerable2) + Enumerable.Count(enumerable3);
						AlertMessage = ((!AlertMessage.Equals("NoConsole") || num <= 0) ? "" : "現在沒有主控者");
						if (!AlertMessage.Equals(""))
						{
							AutoClosingMessageBox.Show(AlertMessage);
						}
						foreach (ReadWindow item in enumerable)
						{
							if (item != null)
							{
								item.RecordPage();
								item.Hide();
								item.Close();
							}
						}
						foreach (HTML5ReadWindow item2 in enumerable2)
						{
							if (item2 != null)
							{
								item2.RecordPage();
								item2.Hide();
								item2.Close();
							}
						}
						foreach (MVWindow item3 in enumerable3)
						{
							if (item3 != null)
							{
								item3.mediaPlayer.Stop();
								item3.mediaPlayer.Close();
								item3.Close();
							}
						}
					}
				}
				catch (Exception ex)
				{
					LogTool.Debug(ex);
				}
			}
		}

		private void IsInSync_And_IsSyncOwner(JArray jArry)
		{
			foreach (JToken item in jArry)
			{
				Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(item.ToString());
				string userID = dictionary["clientId"].ToString();
				userID = Socket_FixEmailUserID.FromSocket(userID);
				dictionary["clientName"].ToString();
				dictionary["clientType"].ToString();
				string text = dictionary["status"].ToString();
				if (userID.Equals(UserID))
				{
					switch (text)
					{
					case "-1":
						IsInSync = true;
						IsSyncOwner = true;
						break;
					case "0":
						IsInSync = true;
						IsSyncOwner = false;
						break;
					case "1":
						IsInSync = false;
						IsSyncOwner = false;
						break;
					default:
						IsInSync = false;
						IsSyncOwner = false;
						break;
					}
					break;
				}
			}
			ChangeSyncButtonLight(IsInSync, IsSyncOwner);
			if (!jArry.ToString().Replace(" ", "").Contains("\"status\":-1,") && IsInSync)
			{
				CloseAllWindow("NoConsole");
			}
		}

		private void ChangeSyncButtonLight(bool IsInSync, bool IsSyncOwner)
		{
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<bool, bool>(ChangeSyncButtonLight), IsInSync, IsSyncOwner);
			}
			else
			{
				foreach (MenuButton item in Enumerable.OfType<MenuButton>(btnSP.Children))
				{
					if (item.userButton.ID.Equals("BtnSync"))
					{
						item.btnImg.Source = ButtonTool.GetSyncButtonImage(IsInSync, IsSyncOwner);
					}
				}
			}
		}

		private void Home_UnZipError_Callback(string message)
		{
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<string>(Home_UnZipError_Callback), message);
			}
			else
			{
				AutoClosingMessageBox.Show(message);
			}
		}

		private void InitUI()
		{
			base.Title = Settings.Default.AppName;
			if (Settings.Default.AssemblyName.Contains("TPI4F"))
			{
				Row1.Height = new GridLength(100.0);
				imgBeta.Height = 98.0;
				imgBeta.Width = 98.0;
				imgLogo.Width = 340.0;
				imgLogo.Height = 85.0;
				imgLogo.Stretch = Stretch.Uniform;
			}
			imgLogo.Source = new BitmapImage(new Uri(Settings.Default.Home_Logo_Image, UriKind.Relative));
			Enum.TryParse(Settings.Default.DisplayUserNameMode, out displayUserNameMode);
			switch (displayUserNameMode)
			{
			case DisplayUserNameMode.UserID_UserName:
				txtUserName.Text = string.Format("{0}\r\n{1}", UserID, UserName);
				blockUserName.Visibility = Visibility.Visible;
				break;
			case DisplayUserNameMode.UserName:
				txtUserName.Text = UserName;
				blockUserName.Visibility = Visibility.Visible;
				break;
			}
			ShowMeetingListCT(true);
			Task.Factory.StartNew(new Action(_003CInitUI_003Eb__1f));
			if (Settings.Default.IsDebugMode)
			{
				imgBeta.Visibility = Visibility.Visible;
			}
		}

		private void ChangeBtnSP(UserButton[] UserButtonAry, string ActiveButtonID)
		{
			Home_ReturnSyncStatus_Function home_ReturnSyncStatus_Function = null;
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<UserButton[], string>(ChangeBtnSP), UserButtonAry, ActiveButtonID);
				return;
			}
			btnSP.Children.Clear();
			foreach (UserButton userButton in UserButtonAry)
			{
				if (Settings.Default.AssemblyName.Contains("TPI4F") && (userButton.ID.Equals("BtnAttendance") || userButton.ID.Equals("BtnIndividualSign")))
				{
					continue;
				}
				MenuButton menuButton = null;
				if (userButton.ID.Equals("BtnQuit"))
				{
					menuButton = new MenuButton(userButton, new Home_ChangeCC_Function(Home_ChangeCC_Event_Callback), new Home_PopUpButtons_Function(PopUpButtons), new Home_ChangeBtnSP_Function(ChangeBtnSP));
				}
				else
				{
					menuButton = new MenuButton(userButton, new Home_ChangeCC_Function(Home_ChangeCC_Event_Callback), new Home_PopUpButtons_Function(PopUpButtons));
					if (userButton.ID.Equals("BtnSync"))
					{
						MenuButton menuButton2 = menuButton;
						if (home_ReturnSyncStatus_Function == null)
						{
							home_ReturnSyncStatus_Function = new Home_ReturnSyncStatus_Function(_003CChangeBtnSP_003Eb__20);
						}
						menuButton2.Home_ReturnSyncStatus_Event += home_ReturnSyncStatus_Function;
						menuButton.btnImg.Source = ButtonTool.GetSyncButtonImage(IsInSync, IsSyncOwner);
					}
				}
				if (!userButton.ID.Equals("BtnSync"))
				{
					bool isActived = false;
					if (userButton.ID.Equals(ActiveButtonID))
					{
						isActived = true;
					}
					menuButton.btnImg.Source = ButtonTool.GetButtonImage(userButton.ID, isActived);
					NowPressButtonID = ActiveButtonID;
				}
				else
				{
					menuButton.btnImg.Source = ButtonTool.GetSyncButtonImage(IsInSync, IsSyncOwner);
				}
				btnSP.Children.Add(menuButton);
			}
		}

		private void ShowMeetingListCT(bool GotoToday, Action Callback = null)
		{
			Action<User, DateTime> action = null;
			Action action2 = null;
			_003C_003Ec__DisplayClass28 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass28();
			_003C_003Ec__DisplayClass.Callback = Callback;
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			DateTime dateTime = DateTime.Today;
			if (!GotoToday)
			{
				dateTime = MeetingListDate;
			}
			MouseTool.ShowLoading();
			new HttpRequest();
			if (NetworkTool.CheckNetwork() > 0)
			{
				if (NetworkTool.CheckNetwork() > 0)
				{
					string userID = UserID;
					string userPWD = UserPWD;
					DateTime date = dateTime;
					if (action == null)
					{
						action = new Action<User, DateTime>(_003C_003Ec__DisplayClass._003CShowMeetingListCT_003Eb__22);
					}
					Action<User, DateTime> doAction = action;
					if (action2 == null)
					{
						action2 = new Action(_003CShowMeetingListCT_003Eb__24);
					}
					GetUserData.AsyncPOST(userID, userPWD, date, doAction, action2);
					return;
				}
				DataTable dataTable = MSCE.GetDataTable("select UserJson from UserData where UserID =@1 and ListDate=@2", UserID, DateTool.MonthFirstDate(MeetingListDate).ToString("yyyyMMdd"));
				User user = new User();
				if (dataTable.Rows.Count > 0)
				{
					user = JsonConvert.DeserializeObject<User>(dataTable.Rows[0]["UserJson"].ToString());
				}
				else
				{
					dataTable = MSCE.GetDataTable("select top 1 UserJson from UserData where UserID =@1", UserID);
					if (dataTable.Rows.Count > 0)
					{
						user = JsonConvert.DeserializeObject<User>(dataTable.Rows[0]["UserJson"].ToString());
					}
					user.MeetingList = new UserMeeting[0];
				}
				GetUserData_DoAction(user, MeetingListDate);
			}
			else
			{
				GetUserData_DoAction(this.user, dateTime);
			}
		}

		private void GetUserData_DoAction(User user, DateTime date)
		{
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<User, DateTime>(GetUserData_DoAction), user, date);
				return;
			}
			if (user != null)
			{
				CC.Content = new MeetingListCT(user.MeetingList, date, new Home_Change2MeetingDataCT_Function(Change2MeetingDataCT));
			}
			else
			{
				CC.Content = new MeetingListCT(null, date, new Home_Change2MeetingDataCT_Function(Change2MeetingDataCT));
				AutoClosingMessageBox.Show("無法取得資料，請稍後再試");
			}
			MouseTool.ShowArrow();
		}

		public void Change2MeetingDataCT(string MeetingID, MeetingData meetingData = null)
		{
			Action<MeetingData> action = null;
			MouseTool.ShowLoading();
			new HttpRequest();
			if (NetworkTool.CheckNetwork() > 0)
			{
				if (meetingData == null)
				{
					string userID = UserID;
					string userPWD = UserPWD;
					if (action == null)
					{
						action = new Action<MeetingData>(_003CChange2MeetingDataCT_003Eb__2b);
					}
					GetMeetingData.AsyncPOST(MeetingID, userID, userPWD, action);
				}
				else
				{
					GetMeetingData_DoAction(meetingData, true);
				}
			}
			else
			{
				DataTable dataTable = MSCE.GetDataTable("select MeetingJson from MeetingData where MeetingID=@1 and UserID =@2", MeetingID, UserID);
				if (dataTable.Rows.Count > 0)
				{
					MeetingData md = JsonConvert.DeserializeObject<MeetingData>(dataTable.Rows[0]["MeetingJson"].ToString());
					GetMeetingData_DoAction(md);
				}
				else
				{
					AutoClosingMessageBox.Show("無法取得資料，請稍後再試");
					MouseTool.ShowArrow();
				}
			}
		}

		private void Home_ChangeCC_Event_Callback(string ButtonID)
		{
			Action<SigninData> action = null;
			Action action2 = null;
			Action action3 = null;
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<string>(Home_ChangeCC_Event_Callback), ButtonID);
			}
			else
			{
				if (ButtonID.Equals("BtnSync"))
				{
					return;
				}
				switch (ButtonID)
				{
				case "BtnHome":
				{
					string sQL = "update NowLogin Set MeetingListDate=@1";
					MSCE.ExecuteNonQuery(sQL, DateTime.Today.ToString("yyyy/MM/dd"));
					ShowMeetingListCT(true);
					break;
				}
				case "BtnSeries":
					ShowBtnSeriesCT("");
					break;
				case "BtnLaw":
					CC.Content = new LawListCT();
					break;
				case "BtnExportPDF":
					ShowPDFFactoryCT();
					break;
				case "BtnSignin":
					ShowSignPictureCT("", "");
					break;
				case "BtnSigninList":
					CC.Content = new SignListCT();
					break;
				case "BtnAttendance":
					CC.Content = new SignListCT_Mix();
					break;
				case "BtnMeeting":
					ShowMeetingDataCT();
					break;
				case "BtnIndividualSign":
				{
					DataTable dataTable2 = MSCE.GetDataTable("select MeetingID from NowLogin");
					if (dataTable2.Rows.Count > 0)
					{
						string meetingID = dataTable2.Rows[0]["MeetingID"].ToString();
						if (action == null)
						{
							action = new Action<SigninData>(_003CHome_ChangeCC_Event_Callback_003Eb__2d);
						}
						GetSigninData.AsyncPOST(meetingID, action);
					}
					break;
				}
				case "BtnVote":
					AutoClosingMessageBox.Show("敬請期待");
					PopUpButtons(NowPressButtonID);
					break;
				case "BtnQuit":
				{
					DataTable dataTable = MSCE.GetDataTable("select MeetingListDate,HomeUserButtonAryJSON from NowLogin");
					if (dataTable.Rows.Count > 0)
					{
						MeetingListDate = (DateTime)dataTable.Rows[0]["MeetingListDate"];
						if (action2 == null)
						{
							action2 = new Action(_003CHome_ChangeCC_Event_Callback_003Eb__2e);
						}
						ShowMeetingListCT(false, action2);
					}
					break;
				}
				case "BtnBroadcast":
					CC.Content = new BroadcastCT();
					break;
				case "BtnLogout":
				{
					StopAllBackground();
					Login login = new Login();
					login.Show();
					Close();
					break;
				}
				}
				TaskFactory factory = Task.Factory;
				if (action3 == null)
				{
					action3 = new Action(_003CHome_ChangeCC_Event_Callback_003Eb__2f);
				}
				factory.StartNew(action3);
			}
		}

		private void ShowMeetingListCT_Callback()
		{
			DataTable dataTable = MSCE.GetDataTable("select MeetingListDate,HomeUserButtonAryJSON from NowLogin");
			if (dataTable.Rows.Count > 0)
			{
				string value = dataTable.Rows[0]["HomeUserButtonAryJSON"].ToString();
				ChangeBtnSP(JsonConvert.DeserializeObject<UserButton[]>(value), "BtnHome");
				NowPressButtonID = "BtnQuit";
			}
			Task.Factory.StartNew(new Action(_003CShowMeetingListCT_Callback_003Eb__33));
		}

		public void PopUpButtons(string ButtonID)
		{
			foreach (MenuButton item in Enumerable.OfType<MenuButton>(btnSP.Children))
			{
				if (ButtonID.Equals("BtnQuit") && item.userButton.ID.Equals("BtnHome"))
				{
					item.btnImg.Source = ButtonTool.GetButtonImage(item.userButton.ID, true);
				}
				else if (!item.userButton.ID.Equals("BtnSync"))
				{
					bool isActived = false;
					if (item.userButton.ID.Equals(ButtonID))
					{
						isActived = true;
					}
					item.btnImg.Source = ButtonTool.GetButtonImage(item.userButton.ID, isActived);
				}
				if (!ButtonID.Equals("BtnMeeting") && !ButtonID.Equals("BtnQuit") && !ButtonID.Equals("BtnSeries") && !ButtonID.Equals("BtnVote"))
				{
					NowPressButtonID = ButtonID;
				}
			}
		}

		private void ShowPDFFactoryCT()
		{
			PDFFactoryCT content = new PDFFactoryCT();
			CC.Content = content;
		}

		private void StopAllBackground()
		{
			try
			{
				Singleton_Socket.home_OpenIEventManager.Home_OpenBook_Event -= Home_OpenBook_Callback;
				Singleton_Socket.home_OpenIEventManager.Home_IsInSync_And_IsSyncOwner_Event -= Home_IsInSync_And_IsSyncOwner_Callback;
				Singleton_Socket.home_OpenIEventManager.Home_CloseAllWindow_Event -= Home_CloseAllWindow_Callback;
				Singleton_Socket.home_CloseIEventManager.Home_CloseAllWindow_Event -= Home_CloseAllWindow_Callback;
				Singleton_Socket.home_CloseIEventManager.Home_TurnOffSyncButton_Event -= Home_TurnOffSyncButton_Callback;
				Singleton_Socket.home_CloseIEventManager.Home_SetSocketClientNull_Event -= Home_SetSocketClientNull_Callback;
				Singleton_Socket.ClearInstance();
				IsInSync = false;
				IsSyncOwner = false;
				TaskFactory factory = Task.Factory;
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate35 == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate35 = new Action(_003CStopAllBackground_003Eb__34);
				}
				factory.StartNew(CS_0024_003C_003E9__CachedAnonymousMethodDelegate35);
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}

		private void GetSigninData_DoAction(SigninData sid)
		{
			Action action = null;
			_003C_003Ec__DisplayClass39 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass39();
			_003C_003Ec__DisplayClass.sid = sid;
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<SigninData>(GetSigninData_DoAction), _003C_003Ec__DisplayClass.sid);
				return;
			}
			if (_003C_003Ec__DisplayClass.sid != null)
			{
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003C_003Ec__DisplayClass._003CGetSigninData_DoAction_003Eb__36);
				}
				factory.StartNew(action);
			}
			else
			{
				AutoClosingMessageBox.Show("無法取得資料，請稍後再試");
			}
			MouseTool.ShowArrow();
		}

		private void ShowMeetingDataCT()
		{
			Action<MeetingData> action = null;
			Action action2 = null;
			string text = "";
			DataTable dataTable = MSCE.GetDataTable("select MeetingID from NowLogin");
			if (dataTable.Rows.Count > 0)
			{
				text = dataTable.Rows[0]["MeetingID"].ToString().Trim();
			}
			MouseTool.ShowLoading();
			if (NetworkTool.CheckNetwork() > 0)
			{
				string meetingID = text;
				string userID = UserID;
				string userPWD = UserPWD;
				if (action == null)
				{
					action = new Action<MeetingData>(_003CShowMeetingDataCT_003Eb__3b);
				}
				Action<MeetingData> doAction = action;
				if (action2 == null)
				{
					action2 = new Action(_003CShowMeetingDataCT_003Eb__3c);
				}
				GetMeetingData.AsyncPOST(meetingID, userID, userPWD, doAction, action2);
			}
			else
			{
				dataTable = MSCE.GetDataTable("select MeetingJson from MeetingData where MeetingID=@1 and UserID =@2", text, UserID);
				if (dataTable.Rows.Count > 0)
				{
					MeetingData md = JsonConvert.DeserializeObject<MeetingData>(dataTable.Rows[0]["MeetingJson"].ToString());
					GetMeetingData_DoAction(md);
				}
				else
				{
					AutoClosingMessageBox.Show("無法取得資料，請稍後再試");
					MouseTool.ShowArrow();
				}
			}
		}

		private void GetMeetingData_DoAction(MeetingData md, bool isFirstAutoTurnOnSync = false)
		{
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<MeetingData, bool>(GetMeetingData_DoAction), md, isFirstAutoTurnOnSync);
			}
			else if (md != null)
			{
				CC.Content = new MeetingDataCT(UserID, UserName, UserPWD, UserEmail, md, new Home_ChangeBtnSP_Function(ChangeBtnSP), isFirstAutoTurnOnSync);
				DataTable dataTable = MSCE.GetDataTable("select MeetingID from MeetingData where MeetingID=@1 and UserID =@2", md.ID, user.ID);
				if (dataTable.Rows.Count > 0)
				{
					MSCE.ExecuteNonQuery("UPDATE [MeetingData] SET \r\n                                                 [MeetingJson] = @1 where MeetingID=@2 and UserID =@3", JsonConvert.SerializeObject(md), md.ID, user.ID);
				}
				else
				{
					MSCE.ExecuteNonQuery("INSERT INTO [MeetingData] ([MeetingID],[MeetingJson],UserID)\r\n                                                            VALUES (@1,@2,@3)", md.ID, JsonConvert.SerializeObject(md), user.ID);
				}
			}
			else
			{
				AutoClosingMessageBox.Show("無法取得資料，請稍後再試");
				MouseTool.ShowArrow();
			}
		}

		public void ShowBtnSeriesCT(string NowSeriesID = "")
		{
			Action<SeriesData> action = null;
			_003C_003Ec__DisplayClass42 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass42();
			_003C_003Ec__DisplayClass.NowSeriesID = NowSeriesID;
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			MouseTool.ShowLoading();
			new HttpRequest();
			if (NetworkTool.CheckNetwork() > 0)
			{
				if (PreLoadSeriesDataDict.ContainsKey("BtnSeries"))
				{
					GetSeriesData_DoAction(PreLoadSeriesDataDict["BtnSeries"], _003C_003Ec__DisplayClass.NowSeriesID);
					PreLoadSeriesData();
					return;
				}
				string userID = UserID;
				if (action == null)
				{
					action = new Action<SeriesData>(_003C_003Ec__DisplayClass._003CShowBtnSeriesCT_003Eb__40);
				}
				GetSeriesData.AsyncPOST(userID, action);
			}
			else
			{
				DataTable dataTable = MSCE.GetDataTable("select SeriesJson from SeriesData where UserID =@1 ", UserID, DateTool.MonthFirstDate(MeetingListDate).ToString("yyyyMMdd"));
				if (dataTable.Rows.Count > 0)
				{
					SeriesData sd = JsonConvert.DeserializeObject<SeriesData>(dataTable.Rows[0]["SeriesJson"].ToString());
					GetSeriesData_DoAction(sd, _003C_003Ec__DisplayClass.NowSeriesID);
				}
				else
				{
					AutoClosingMessageBox.Show("無法取得資料，請稍後再試");
					MouseTool.ShowArrow();
				}
			}
		}

		private void GetSeriesData_DoAction(SeriesData sd, string NowSeriesID = "")
		{
			Action action = null;
			_003C_003Ec__DisplayClass47 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass47();
			_003C_003Ec__DisplayClass.sd = sd;
			_003C_003Ec__DisplayClass.NowSeriesID = NowSeriesID;
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<SeriesData, string>(GetSeriesData_DoAction), _003C_003Ec__DisplayClass.sd, _003C_003Ec__DisplayClass.NowSeriesID);
				return;
			}
			if (_003C_003Ec__DisplayClass.sd != null)
			{
				if (_003C_003Ec__DisplayClass.sd.SeriesMeeting == null)
				{
					AutoClosingMessageBox.Show(string.Format("現在尚無系列{0}", Settings.Default.CourseOrMeeting_String));
					PopUpButtons(NowPressButtonID);
					MouseTool.ShowArrow();
					return;
				}
				NowPressButtonID = "BtnSeries";
				if (_003C_003Ec__DisplayClass.NowSeriesID.Equals("") && _003C_003Ec__DisplayClass.sd.SeriesMeeting.Length > 0)
				{
					_003C_003Ec__DisplayClass.NowSeriesID = _003C_003Ec__DisplayClass.sd.SeriesMeeting[0].Series.ID;
				}
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003C_003Ec__DisplayClass._003CGetSeriesData_DoAction_003Eb__44);
				}
				factory.StartNew(action);
				DataTable dataTable = MSCE.GetDataTable("select SeriesJson from SeriesData where UserID =@1", user.ID);
				if (dataTable.Rows.Count > 0)
				{
					MSCE.ExecuteNonQuery("UPDATE [SeriesData] SET \r\n\t\t                                         [SeriesJson] = @1\r\n\t\t                                         where UserID = @2", JsonConvert.SerializeObject(_003C_003Ec__DisplayClass.sd), user.ID);
				}
				else
				{
					MSCE.ExecuteNonQuery("INSERT INTO [SeriesData] ([SeriesJson],[UserID])\r\n                                                            VALUES (@1,@2)", JsonConvert.SerializeObject(_003C_003Ec__DisplayClass.sd), user.ID);
				}
			}
			else
			{
				AutoClosingMessageBox.Show("無法取得資料，請稍後再試");
			}
			MouseTool.ShowArrow();
		}

		public void OpenBookFromReader(string MeetingID, BookVM bookVM, Dictionary<string, BookVM> cbBooksData, string watermark = "")
		{
			_003C_003Ec__DisplayClass51 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass51();
			_003C_003Ec__DisplayClass.MeetingID = MeetingID;
			_003C_003Ec__DisplayClass.cbBooksData = cbBooksData;
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<string, BookVM, Dictionary<string, BookVM>, string>(OpenBookFromReader), _003C_003Ec__DisplayClass.MeetingID, bookVM, _003C_003Ec__DisplayClass.cbBooksData, watermark);
				return;
			}
			MouseTool.ShowLoading();
			try
			{
				Action action = null;
				Action action2 = null;
				Action action3 = null;
				_003C_003Ec__DisplayClass56 _003C_003Ec__DisplayClass2 = new _003C_003Ec__DisplayClass56();
				_003C_003Ec__DisplayClass2.CS_0024_003C_003E8__locals52 = _003C_003Ec__DisplayClass;
				_003C_003Ec__DisplayClass2.rws = Enumerable.OfType<ReadWindow>(Application.Current.Windows);
				_003C_003Ec__DisplayClass2.html5rws = Enumerable.OfType<HTML5ReadWindow>(Application.Current.Windows);
				_003C_003Ec__DisplayClass2.mvWindows = Enumerable.OfType<MVWindow>(Application.Current.Windows);
				base.Dispatcher.BeginInvoke(new Action(_003C_003Ec__DisplayClass2._003COpenBookFromReader_003Eb__49));
				string dataPath = ClickOnceTool.GetDataPath();
				ClickOnceTool.GetFilePath();
				_003C_003Ec__DisplayClass2._bookPath = bookVM.BookPath;
				_003C_003Ec__DisplayClass2._bookId = bookVM.FileID;
				_003C_003Ec__DisplayClass2._account = UserID;
				_003C_003Ec__DisplayClass2._userName = UserName;
				_003C_003Ec__DisplayClass2._email = UserEmail;
				_003C_003Ec__DisplayClass2._meetingId = _003C_003Ec__DisplayClass.MeetingID;
				_003C_003Ec__DisplayClass2._watermark = "";
				_003C_003Ec__DisplayClass2._dbPath = Path.Combine(dataPath, Settings.Default.bookInfo_Path);
				_003C_003Ec__DisplayClass2._isSync = IsInSync;
				_003C_003Ec__DisplayClass2._isSyncOwner = IsSyncOwner;
				_003C_003Ec__DisplayClass2._webServiceUrl = WsTool.GetUrl() + "/AnnotationUpload";
				_003C_003Ec__DisplayClass2._socketMessage = "";
				_003C_003Ec__DisplayClass2._socket = Singleton_Socket.GetInstance(_003C_003Ec__DisplayClass.MeetingID, UserID, UserName, IsInSync);
				switch (bookVM.FileCate)
				{
				case MeetingFileCate.電子書:
				{
					TaskFactory factory2 = Task.Factory;
					if (action == null)
					{
						action = new Action(_003C_003Ec__DisplayClass2._003COpenBookFromReader_003Eb__4a);
					}
					factory2.StartNew(action);
					break;
				}
				case MeetingFileCate.Html5投影片:
				{
					_003C_003Ec__DisplayClass2._bookPath = _003C_003Ec__DisplayClass2._bookPath + "\\" + new FileInfo(Directory.GetFiles(_003C_003Ec__DisplayClass2._bookPath)[0]).Name;
					TaskFactory factory3 = Task.Factory;
					if (action2 == null)
					{
						action2 = new Action(_003C_003Ec__DisplayClass2._003COpenBookFromReader_003Eb__4c);
					}
					factory3.StartNew(action2);
					break;
				}
				case MeetingFileCate.影片檔:
				{
					_003C_003Ec__DisplayClass2._bookPath = _003C_003Ec__DisplayClass2._bookPath + "\\" + new FileInfo(Directory.GetFiles(_003C_003Ec__DisplayClass2._bookPath)[0]).Name;
					TaskFactory factory = Task.Factory;
					if (action3 == null)
					{
						action3 = new Action(_003C_003Ec__DisplayClass2._003COpenBookFromReader_003Eb__4e);
					}
					factory.StartNew(action3);
					break;
				}
				}
				if (IsInSync && IsSyncOwner)
				{
					_003C_003Ec__DisplayClass58 _003C_003Ec__DisplayClass3 = new _003C_003Ec__DisplayClass58();
					_003C_003Ec__DisplayClass3.CS_0024_003C_003E8__locals57 = _003C_003Ec__DisplayClass2;
					_003C_003Ec__DisplayClass3.CS_0024_003C_003E8__locals52 = _003C_003Ec__DisplayClass;
					_003C_003Ec__DisplayClass3.socketClient = Singleton_Socket.GetInstance(_003C_003Ec__DisplayClass.MeetingID, UserID, UserName, IsInSync);
					Task.Factory.StartNew(new Action(_003C_003Ec__DisplayClass3._003COpenBookFromReader_003Eb__50));
				}
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
			MouseTool.ShowArrow();
		}

		private void Clear_LawDownloaderEvent()
		{
			LawDownloader instance = Singleton_LawDownloader.GetInstance();
			instance.ClearHomeEvent();
		}

		private void Clear_FileDownloaderEvent()
		{
			Dictionary<string, PaperLess_Emeeting.App_Code.WS.FileDownloader> instance = Singleton_FileDownloader.GetInstance();
			foreach (KeyValuePair<string, PaperLess_Emeeting.App_Code.WS.FileDownloader> item in instance)
			{
				item.Value.ClearAllEvent();
			}
		}

		public void ShowSignPictureCT(string DeptID = "", string PicUrl = "")
		{
			_003C_003Ec__DisplayClass5b _003C_003Ec__DisplayClass5b = new _003C_003Ec__DisplayClass5b();
			_003C_003Ec__DisplayClass5b.DeptID = DeptID;
			_003C_003Ec__DisplayClass5b.PicUrl = PicUrl;
			_003C_003Ec__DisplayClass5b._003C_003E4__this = this;
			SignPictureCT content = new SignPictureCT(new Home_ChangeTogSignPadCT_Function(_003C_003Ec__DisplayClass5b._003CShowSignPictureCT_003Eb__5a), new Home_GoBackTogSignPictureCT_Function(ShowSignPictureCT));
			CC.Content = content;
		}

		private void InitializeAutoLogoffFeature()
		{
			try
			{
				HwndSource hwndSource = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
				hwndSource.AddHook(new HwndSourceHook(CallBackMethod));
				AutoLogOffHelper.LogOffTime = Settings.Default.AutoLogoutMinutes;
				AutoLogOffHelper.MakeAutoLogOffEvent += new AutoLogOffHelper.MakeAutoLogOff(AutoLogOffHelper_MakeAutoLogOffEvent);
				AutoLogOffHelper.StartAutoLogoffOption();
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}

		private IntPtr CallBackMethod(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			try
			{
				if (IsInSync)
				{
					AutoLogOffHelper.ResetLogoffTimer();
				}
				if ((msg >= 512 && msg <= 522) || (msg <= 262 && msg >= 160) || msg == 33)
				{
					AutoLogOffHelper.ResetLogoffTimer();
				}
				return IntPtr.Zero;
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
			return IntPtr.Zero;
		}

		private void AutoLogOffHelper_MakeAutoLogOffEvent()
		{
			try
			{
				lock (this)
				{
					AutoLogOffHelper.StopAutoLogoffOption();
					Thread.Sleep(3000);
					MessageBoxResult messageBoxResult = MessageBoxResult.None;
					messageBoxResult = MessageBox.Show("您已閒置超過" + Settings.Default.AutoLogoutMinutes + "分鐘，是否登出", "系統訊息", MessageBoxButton.YesNo, MessageBoxImage.Asterisk);
					if (messageBoxResult == MessageBoxResult.Yes)
					{
						Logoff();
					}
					else
					{
						AutoLogOffHelper.StartAutoLogoffOption();
					}
				}
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}

		private void Logoff()
		{
			try
			{
				CloseAllWindow("", true);
				Login login = new Login();
				login.Show();
				Close();
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/home.xaml", UriKind.Relative);
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
				Row1 = (RowDefinition)target;
				break;
			case 2:
				imgBeta = (Image)target;
				break;
			case 3:
				DP1 = (DockPanel)target;
				break;
			case 4:
				imgLogo = (Image)target;
				break;
			case 5:
				blockUserName = (Grid)target;
				break;
			case 6:
				txtUserName = (TextBlock)target;
				break;
			case 7:
				btnSP = (StackPanel)target;
				break;
			case 8:
				CC = (ContentControl)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}

		[CompilerGenerated]
		private void _003CHome_Loaded_003Eb__0()
		{
			InitSelectDB();
			base.Dispatcher.BeginInvoke(new Action(_003CHome_Loaded_003Eb__1));
			PreLoadSeriesData();
		}

		[CompilerGenerated]
		private void _003CHome_Loaded_003Eb__1()
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
		private void _003CPreLoadSeriesData_003Eb__2()
		{
			GetSeriesData.AsyncPOST(UserID, new Action<SeriesData>(_003CPreLoadSeriesData_003Eb__3));
		}

		[CompilerGenerated]
		private void _003CPreLoadSeriesData_003Eb__3(SeriesData sd)
		{
			ThreadStart threadStart = null;
			try
			{
				PreLoadSeriesDataDict["BtnSeries"] = sd;
				if (CacheMinuteTTL > 0)
				{
					if (CacheThread != null)
					{
						CacheThread.Abort();
					}
					if (threadStart == null)
					{
						threadStart = new ThreadStart(_003CPreLoadSeriesData_003Eb__4);
					}
					CacheThread = new Thread(threadStart);
					CacheThread.IsBackground = false;
					CacheThread.Start();
				}
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}

		[CompilerGenerated]
		private void _003CPreLoadSeriesData_003Eb__4()
		{
			Thread.Sleep(CacheMinuteTTL * 60 * 1000);
			if (PreLoadSeriesDataDict.ContainsKey("BtnSeries"))
			{
				PreLoadSeriesDataDict.Remove("BtnSeries");
			}
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
			CC.Content = new BroadcastCT();
		}

		[CompilerGenerated]
		private void _003CInitUI_003Eb__1f()
		{
			ChangeBtnSP(UserButtonAry, "BtnHome");
		}

		[CompilerGenerated]
		private Tuple<bool, bool> _003CChangeBtnSP_003Eb__20()
		{
			return new Tuple<bool, bool>(IsInSync, IsSyncOwner);
		}

		[CompilerGenerated]
		private void _003CShowMeetingListCT_003Eb__24()
		{
			base.Dispatcher.BeginInvoke(new Action(_003CShowMeetingListCT_003Eb__25));
		}

		[CompilerGenerated]
		private void _003CShowMeetingListCT_003Eb__25()
		{
			PopUpButtons(NowPressButtonID);
		}

		[CompilerGenerated]
		private void _003CChange2MeetingDataCT_003Eb__2b(MeetingData md)
		{
			GetMeetingData_DoAction(md, true);
		}

		[CompilerGenerated]
		private void _003CHome_ChangeCC_Event_Callback_003Eb__2d(SigninData sid)
		{
			GetSigninData_DoAction(sid);
		}

		[CompilerGenerated]
		private void _003CHome_ChangeCC_Event_Callback_003Eb__2e()
		{
			ShowMeetingListCT_Callback();
		}

		[CompilerGenerated]
		private void _003CHome_ChangeCC_Event_Callback_003Eb__2f()
		{
			Clear_FileDownloaderEvent();
			Clear_LawDownloaderEvent();
		}

		[CompilerGenerated]
		private void _003CShowMeetingListCT_Callback_003Eb__33()
		{
			try
			{
				SocketClient instance = Singleton_Socket.GetInstance();
				if (instance != null && instance.GetIsConnected() && IsInSync)
				{
					instance.syncSwitch(false);
					instance.logout();
				}
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
			IsInSync = false;
			IsSyncOwner = false;
			Singleton_Socket.ClearInstance();
		}

		[CompilerGenerated]
		private static void _003CStopAllBackground_003Eb__34()
		{
			LawDownloader instance = Singleton_LawDownloader.GetInstance();
			instance.ClearAllEvent();
			instance.Stop();
			Dictionary<string, PaperLess_Emeeting.App_Code.WS.FileDownloader> instance2 = Singleton_FileDownloader.GetInstance();
			foreach (KeyValuePair<string, PaperLess_Emeeting.App_Code.WS.FileDownloader> item in instance2)
			{
				item.Value.ClearAllEvent();
				item.Value.Stop();
			}
			instance2.Clear();
			Singleton_PDFFactory.ClearInstance();
		}

		[CompilerGenerated]
		private void _003CShowMeetingDataCT_003Eb__3b(MeetingData md)
		{
			GetMeetingData_DoAction(md);
			NowPressButtonID = "BtnMeeting";
		}

		[CompilerGenerated]
		private void _003CShowMeetingDataCT_003Eb__3c()
		{
			base.Dispatcher.BeginInvoke(new Action(_003CShowMeetingDataCT_003Eb__3d));
		}

		[CompilerGenerated]
		private void _003CShowMeetingDataCT_003Eb__3d()
		{
			PopUpButtons(NowPressButtonID);
		}
	}
}
