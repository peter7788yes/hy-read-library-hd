using AES_ECB_PKCS5;
using BookManagerModule;
using DataAccessObject;
using PaperLess_Emeeting.App_Code;
using PaperLess_Emeeting.App_Code.ClickOnce;
using PaperLess_Emeeting.App_Code.DownloadItem;
using PaperLess_Emeeting.App_Code.MessageBox;
using PaperLess_Emeeting.App_Code.Socket;
using PaperLess_Emeeting.App_Code.ViewModel;
using PaperLess_Emeeting.App_Code.WS;
using PaperLess_Emeeting.Properties;
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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace PaperLess_Emeeting
{
	public class FileRow : UserControl, IComponentConnector
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass1e
		{
			public FileRow _003C_003E4__this;

			public string dbPath;

			public string bookId;

			public string account;

			public string meetingId;

			public void _003CInitSyncCenter_003Eb__1c()
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
		private sealed class _003C_003Ec__DisplayClass2a
		{
			public Home Home_Window;

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

			public FileRow _003C_003E4__this;

			public void _003CCallOpenBook_003Eb__20()
			{
				_003C_003E4__this.Dispatcher.BeginInvoke(new Action(_003CCallOpenBook_003Eb__21));
			}

			public void _003CCallOpenBook_003Eb__22()
			{
				_003C_003E4__this.Dispatcher.BeginInvoke(new Action(_003CCallOpenBook_003Eb__23));
			}

			public void _003CCallOpenBook_003Eb__24()
			{
				_003C_003E4__this.Dispatcher.BeginInvoke(new Action(_003CCallOpenBook_003Eb__25));
			}

			public void _003CCallOpenBook_003Eb__21()
			{
				byte[] defaultKey = new byte[1];
				try
				{
					if (!_003C_003E4__this.fileItem.EncryptionKey.Equals(""))
					{
						defaultKey = ReaderDecodeTool.GetReaderKey(_003C_003E4__this.fileItem.EncryptionKey);
					}
				}
				catch (Exception ex)
				{
					LogTool.Debug(ex);
				}
				ReadWindow readWindow = new ReadWindow(cbBooksData, new Home_OpenBookFromReader_Function(Home_Window.OpenBookFromReader), _bookPath, _bookId, _account, _userName, _email, _meetingId, _watermark, _dbPath, _isSync, _isSyncOwner, _webServiceUrl, defaultKey, _socketMessage, _socket);
				readWindow.WindowStyle = WindowStyle.None;
				readWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				readWindow.WindowState = WindowState.Maximized;
				Settings.Default.AssemblyName.Contains("TPI4F");
				readWindow.Show();
			}

			public void _003CCallOpenBook_003Eb__23()
			{
				HTML5ReadWindow hTML5ReadWindow = new HTML5ReadWindow(cbBooksData, new Home_OpenBookFromReader_Function(Home_Window.OpenBookFromReader), _bookPath, _bookId, _account, _userName, _email, _meetingId, _watermark, _dbPath, _isSync, _isSyncOwner, _webServiceUrl, _socketMessage, _socket);
				hTML5ReadWindow.WindowStyle = WindowStyle.None;
				hTML5ReadWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				hTML5ReadWindow.WindowState = WindowState.Maximized;
				Settings.Default.AssemblyName.Contains("TPI4F");
				hTML5ReadWindow.Show();
			}

			public void _003CCallOpenBook_003Eb__25()
			{
				MVWindow mVWindow = new MVWindow(cbBooksData, new Home_OpenBookFromReader_Function(Home_Window.OpenBookFromReader), _bookPath, _bookId);
				mVWindow.WindowStyle = WindowStyle.None;
				mVWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				mVWindow.WindowState = WindowState.Maximized;
				Settings.Default.AssemblyName.Contains("TPI4F");
				mVWindow.Show();
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass2c
		{
			public _003C_003Ec__DisplayClass2a CS_0024_003C_003E8__locals2b;

			public SocketClient socketClient;

			public void _003CCallOpenBook_003Eb__26()
			{
				if (socketClient != null && socketClient.GetIsConnected())
				{
					string msg = "{\"bookId\":\"" + CS_0024_003C_003E8__locals2b._003C_003E4__this.meetingDataDownloadFileFile.ID + "\",\"cmd\":\"R.OB\"}";
					socketClient.broadcast(msg);
				}
			}
		}

		private Storyboard sb;

		private MeetingDataCT_RaiseAllDownload_Function MeetingDataCT_RaiseAllDownload_Event;

		private MeetingDataCT_HangTheDownloadEvent_Function MeetingDataCT_HangTheDownloadEvent_Event;

		private MeetingDataCT_IsAllFileRowFinished_AddInitUIFinished_Function MeetingDataCT_IsAllFileRowFinished_AddInitUIFinished_Event;

		private MeetingDataCT_GetBookVMs_ByMeetingFileCate_Function MeetingDataCT_GetBookVMs_ByMeetingFileCate_Event;

		private MeetingDataCT_GetWatermark_Function MeetingDataCT_GetWatermark_Event;

		private MeetingDataCT_Counting_Finished_FileCount_Function MeetingDataCT_Counting_Finished_FileCount_Event;

		public bool IsAllFileRowFinished;

		public bool IsWaitingForDownload;

		internal Label lineCenter;

		internal TextBlock txtIndex;

		internal TextBlock txtFileName;

		internal Image btnOpen;

		internal Image btnDownload;

		internal TextBlock txtPercent;

		internal Image btnPause;

		internal Image btnPausing;

		internal TextBlock txtUnzip;

		internal Image btnUpdate;

		internal ProgressBar pb;

		private bool _contentLoaded;

		[CompilerGenerated]
		private MeetingDataDownloadFileFile _003CmeetingDataDownloadFileFile_003Ek__BackingField;

		[CompilerGenerated]
		private int _003Cindex_003Ek__BackingField;

		[CompilerGenerated]
		private File_DownloadItemViewModel _003CfileItem_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserID_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserName_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserPWD_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CMeetingID_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserEmail_003Ek__BackingField;

		[CompilerGenerated]
		private bool _003CIsLastRow_003Ek__BackingField;

		[CompilerGenerated]
		private MeetingRoomButtonType _003CmeetingRoomButtonType_003Ek__BackingField;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate11;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate12;

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

		public MeetingDataDownloadFileFile meetingDataDownloadFileFile
		{
			[CompilerGenerated]
			get
			{
				return _003CmeetingDataDownloadFileFile_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CmeetingDataDownloadFileFile_003Ek__BackingField = value;
			}
		}

		public int index
		{
			[CompilerGenerated]
			get
			{
				return _003Cindex_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003Cindex_003Ek__BackingField = value;
			}
		}

		public File_DownloadItemViewModel fileItem
		{
			[CompilerGenerated]
			get
			{
				return _003CfileItem_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CfileItem_003Ek__BackingField = value;
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

		public bool IsLastRow
		{
			[CompilerGenerated]
			get
			{
				return _003CIsLastRow_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CIsLastRow_003Ek__BackingField = value;
			}
		}

		public MeetingRoomButtonType meetingRoomButtonType
		{
			[CompilerGenerated]
			get
			{
				return _003CmeetingRoomButtonType_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CmeetingRoomButtonType_003Ek__BackingField = value;
			}
		}

		public FileRow(string UserID, string UserName, string UserPWD, string MeetingID, string UserEmail, int index, bool IsLastRow, MeetingDataDownloadFileFile meetingDataDownloadFileFile, MeetingDataCT_RaiseAllDownload_Function callback1, MeetingDataCT_HangTheDownloadEvent_Function callback2, MeetingDataCT_IsAllFileRowFinished_AddInitUIFinished_Function callback3, MeetingDataCT_GetBookVMs_ByMeetingFileCate_Function callback4, MeetingDataCT_GetWatermark_Function callback5, MeetingRoomButtonType meetingRoomButtonType, MeetingDataCT_Counting_Finished_FileCount_Function callback6)
		{
			InitializeComponent();
			this.UserID = UserID;
			this.UserName = UserName;
			this.UserPWD = UserPWD;
			this.MeetingID = MeetingID;
			this.UserEmail = UserEmail;
			this.index = index;
			this.IsLastRow = IsLastRow;
			this.meetingDataDownloadFileFile = meetingDataDownloadFileFile;
			MeetingDataCT_RaiseAllDownload_Event = callback1;
			MeetingDataCT_HangTheDownloadEvent_Event = callback2;
			MeetingDataCT_IsAllFileRowFinished_AddInitUIFinished_Event = callback3;
			MeetingDataCT_GetBookVMs_ByMeetingFileCate_Event = callback4;
			MeetingDataCT_GetWatermark_Event = callback5;
			MeetingDataCT_Counting_Finished_FileCount_Event = callback6;
			this.meetingRoomButtonType = meetingRoomButtonType;
			fileItem = null;
			base.Loaded += new RoutedEventHandler(FileRow_Loaded);
			base.Unloaded += new RoutedEventHandler(FileRow_Unloaded);
		}

		private void FileRow_Unloaded(object sender, RoutedEventArgs e)
		{
			MeetingDataCT_RaiseAllDownload_Event = null;
			MeetingDataCT_HangTheDownloadEvent_Event = null;
		}

		private void FileRow_Loaded(object sender, RoutedEventArgs e)
		{
			InitSelectDB();
		}

		private void InitSelectDB()
		{
			Action action = null;
			txtIndex.Text = index.ToString();
			txtFileName.Text = meetingDataDownloadFileFile.FileName;
			FileDownloader instance = Singleton_FileDownloader.GetInstance(MeetingID);
			fileItem = instance.GetInList(meetingDataDownloadFileFile.ID);
			if (fileItem != null)
			{
				IsWaitingForDownload = true;
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003CInitSelectDB_003Eb__0);
				}
				factory.StartNew(action);
			}
			else
			{
				Task.Factory.StartNew(new Action(_003CInitSelectDB_003Eb__4)).ContinueWith(new Action<Task>(_003CInitSelectDB_003Eb__2));
			}
		}

		private void wc_FileRow_ChangeProgress_Event(int ProgressPercentage)
		{
			pb.Value = ProgressPercentage;
		}

		private void InitEvent()
		{
			Image image = btnDownload;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate11 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate11 = new MouseEventHandler(_003CInitEvent_003Eb__6);
			}
			image.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate11;
			Image image2 = btnDownload;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate12 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate12 = new MouseEventHandler(_003CInitEvent_003Eb__7);
			}
			image2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate12;
			btnDownload.MouseLeftButtonDown += new MouseButtonEventHandler(btnDownload_MouseLeftButtonDown);
			Image image3 = btnOpen;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate13 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate13 = new MouseEventHandler(_003CInitEvent_003Eb__8);
			}
			image3.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate13;
			Image image4 = btnOpen;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate14 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate14 = new MouseEventHandler(_003CInitEvent_003Eb__9);
			}
			image4.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate14;
			btnOpen.MouseLeftButtonDown += new MouseButtonEventHandler(btnOpen_MouseLeftButtonDown);
			Image image5 = btnPause;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate15 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate15 = new MouseEventHandler(_003CInitEvent_003Eb__a);
			}
			image5.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate15;
			Image image6 = btnPause;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate16 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate16 = new MouseEventHandler(_003CInitEvent_003Eb__b);
			}
			image6.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate16;
			btnPause.MouseLeftButtonDown += new MouseButtonEventHandler(btnPause_MouseLeftButtonDown);
			Image image7 = btnPausing;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate17 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate17 = new MouseEventHandler(_003CInitEvent_003Eb__c);
			}
			image7.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate17;
			Image image8 = btnPausing;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate18 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate18 = new MouseEventHandler(_003CInitEvent_003Eb__d);
			}
			image8.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate18;
			btnPausing.MouseLeftButtonDown += new MouseButtonEventHandler(btnPausing_MouseLeftButtonDown);
			if (fileItem.CanUpdate)
			{
				TextBlock textBlock = txtFileName;
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate19 == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate19 = new MouseEventHandler(_003CInitEvent_003Eb__e);
				}
				textBlock.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate19;
				TextBlock textBlock2 = txtFileName;
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate1a == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate1a = new MouseEventHandler(_003CInitEvent_003Eb__f);
				}
				textBlock2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate1a;
				txtFileName.MouseLeftButtonDown += new MouseButtonEventHandler(txtFileName_MouseLeftButtonDown);
			}
			Task.Factory.StartNew(new Action(_003CInitEvent_003Eb__10));
		}

		private void btnOpen_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			CallOpenBook(false);
		}

		private void txtFileName_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			CallOpenBook(true);
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

		public void InitSyncCenter(string dbPath, string bookId, string account, string meetingId)
		{
			Action action = null;
			_003C_003Ec__DisplayClass1e _003C_003Ec__DisplayClass1e = new _003C_003Ec__DisplayClass1e();
			_003C_003Ec__DisplayClass1e.dbPath = dbPath;
			_003C_003Ec__DisplayClass1e.bookId = bookId;
			_003C_003Ec__DisplayClass1e.account = account;
			_003C_003Ec__DisplayClass1e.meetingId = meetingId;
			_003C_003Ec__DisplayClass1e._003C_003E4__this = this;
			if (Settings.Default.HasSyncCenterModule)
			{
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003C_003Ec__DisplayClass1e._003CInitSyncCenter_003Eb__1c);
				}
				factory.StartNew(action);
			}
		}

		private void CallOpenBook(bool HasOpenFinishedFileVersion = false)
		{
			MouseTool.ShowLoading();
			try
			{
				Action action = null;
				Action action2 = null;
				Action action3 = null;
				_003C_003Ec__DisplayClass2a _003C_003Ec__DisplayClass2a = new _003C_003Ec__DisplayClass2a();
				_003C_003Ec__DisplayClass2a._003C_003E4__this = this;
				_003C_003Ec__DisplayClass2a.Home_Window = Enumerable.FirstOrDefault(Enumerable.OfType<Home>(Application.Current.Windows));
				if (_003C_003Ec__DisplayClass2a.Home_Window.IsInSync && !_003C_003Ec__DisplayClass2a.Home_Window.IsSyncOwner)
				{
					AutoClosingMessageBox.Show("同步中需由主控人員進行操作");
					return;
				}
				string filePath = ClickOnceTool.GetFilePath();
				string file_UnZipFileFolder = Settings.Default.File_UnZipFileFolder;
				_003C_003Ec__DisplayClass2a._bookPath = fileItem.UnZipFilePath;
				if (HasOpenFinishedFileVersion)
				{
					DataTable dataTable = MSCE.GetDataTable("SELECT FinishedFileVersion FROM FileRow where ID=@1 and UserID=@2 and MeetingID=@3", meetingDataDownloadFileFile.ID, UserID, MeetingID);
					if (dataTable.Rows.Count > 0)
					{
						_003C_003Ec__DisplayClass2a._bookPath = filePath + "\\" + file_UnZipFileFolder + "\\" + UserID + "\\" + MeetingID + "\\" + fileItem.ID + "\\" + dataTable.Rows[0]["FinishedFileVersion"].ToString();
					}
				}
				_003C_003Ec__DisplayClass2a._bookId = fileItem.ID;
				_003C_003Ec__DisplayClass2a._account = UserID;
				_003C_003Ec__DisplayClass2a._userName = UserName;
				_003C_003Ec__DisplayClass2a._email = UserEmail;
				_003C_003Ec__DisplayClass2a._meetingId = MeetingID;
				_003C_003Ec__DisplayClass2a._watermark = "";
				if (MeetingDataCT_GetWatermark_Event != null)
				{
					_003C_003Ec__DisplayClass2a._watermark = MeetingDataCT_GetWatermark_Event();
				}
				_003C_003Ec__DisplayClass2a._dbPath = Path.Combine(ClickOnceTool.GetDataPath(), Settings.Default.bookInfo_Path);
				_003C_003Ec__DisplayClass2a._isSync = _003C_003Ec__DisplayClass2a.Home_Window.IsInSync;
				_003C_003Ec__DisplayClass2a._isSyncOwner = _003C_003Ec__DisplayClass2a.Home_Window.IsSyncOwner;
				_003C_003Ec__DisplayClass2a._webServiceUrl = WsTool.GetUrl() + "/AnnotationUpload";
				_003C_003Ec__DisplayClass2a._socketMessage = "";
				_003C_003Ec__DisplayClass2a._socket = null;
				SocketClient instance = Singleton_Socket.GetInstance(MeetingID, UserID, UserName, _003C_003Ec__DisplayClass2a.Home_Window.IsInSync);
				if (instance != null && instance.GetIsConnected())
				{
					_003C_003Ec__DisplayClass2a._socket = instance;
				}
				_003C_003Ec__DisplayClass2a.cbBooksData = new Dictionary<string, BookVM>();
				if (MeetingDataCT_GetBookVMs_ByMeetingFileCate_Event != null)
				{
					_003C_003Ec__DisplayClass2a.cbBooksData = MeetingDataCT_GetBookVMs_ByMeetingFileCate_Event(fileItem);
				}
				_003C_003Ec__DisplayClass2a.Home_Window.CloseAllWindow("", true);
				switch (fileItem.FileCate)
				{
				case MeetingFileCate.電子書:
				{
					InitSyncCenter(_003C_003Ec__DisplayClass2a._dbPath, _003C_003Ec__DisplayClass2a._bookId, _003C_003Ec__DisplayClass2a._account, _003C_003Ec__DisplayClass2a._meetingId);
					TaskFactory factory2 = Task.Factory;
					if (action == null)
					{
						action = new Action(_003C_003Ec__DisplayClass2a._003CCallOpenBook_003Eb__20);
					}
					factory2.StartNew(action);
					break;
				}
				case MeetingFileCate.Html5投影片:
				{
					_003C_003Ec__DisplayClass2a._bookPath = _003C_003Ec__DisplayClass2a._bookPath + "\\" + new FileInfo(Directory.GetFiles(_003C_003Ec__DisplayClass2a._bookPath)[0]).Name;
					InitSyncCenter(_003C_003Ec__DisplayClass2a._dbPath, _003C_003Ec__DisplayClass2a._bookId, _003C_003Ec__DisplayClass2a._account, _003C_003Ec__DisplayClass2a._meetingId);
					TaskFactory factory3 = Task.Factory;
					if (action2 == null)
					{
						action2 = new Action(_003C_003Ec__DisplayClass2a._003CCallOpenBook_003Eb__22);
					}
					factory3.StartNew(action2);
					break;
				}
				case MeetingFileCate.影片檔:
				{
					_003C_003Ec__DisplayClass2a._bookPath = _003C_003Ec__DisplayClass2a._bookPath + "\\" + new FileInfo(Directory.GetFiles(_003C_003Ec__DisplayClass2a._bookPath)[0]).Name;
					TaskFactory factory = Task.Factory;
					if (action3 == null)
					{
						action3 = new Action(_003C_003Ec__DisplayClass2a._003CCallOpenBook_003Eb__24);
					}
					factory.StartNew(action3);
					break;
				}
				}
				if (_003C_003Ec__DisplayClass2a.Home_Window.IsInSync && _003C_003Ec__DisplayClass2a.Home_Window.IsSyncOwner)
				{
					_003C_003Ec__DisplayClass2c _003C_003Ec__DisplayClass2c = new _003C_003Ec__DisplayClass2c();
					_003C_003Ec__DisplayClass2c.CS_0024_003C_003E8__locals2b = _003C_003Ec__DisplayClass2a;
					_003C_003Ec__DisplayClass2c.socketClient = Singleton_Socket.GetInstance(MeetingID, UserID, UserName, _003C_003Ec__DisplayClass2a.Home_Window.IsInSync);
					Task.Factory.StartNew(new Action(_003C_003Ec__DisplayClass2c._003CCallOpenBook_003Eb__26));
				}
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
			MouseTool.ShowArrow();
		}

		private void btnPausing_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			btnPausing.Visibility = Visibility.Collapsed;
			FileDownloader instance = Singleton_FileDownloader.GetInstance(MeetingID);
			File_DownloadItemViewModel inList = instance.GetInList(MeetingID);
			instance.AddItem(fileItem);
			if (inList != null)
			{
				if (instance.NowFileItem != null && instance.NowFileItem.ID.Equals(fileItem.ID))
				{
					btnPausing.Visibility = Visibility.Collapsed;
					txtPercent.Text = (int)inList.LastPercentage + " %";
					txtPercent.Foreground = Brushes.Black;
					txtPercent.Visibility = Visibility.Visible;
					pb.Value = inList.NowPercentage;
					pb.Foreground = Brushes.Orange;
					pb.Background = Brushes.Black;
					pb.Visibility = Visibility.Visible;
					btnPause.Visibility = Visibility.Visible;
				}
				else
				{
					btnPausing.Visibility = Visibility.Collapsed;
					txtPercent.Text = "等待中";
					txtPercent.Foreground = Brushes.Gray;
					txtPercent.Visibility = Visibility.Visible;
					pb.Value = inList.NowPercentage;
					pb.Foreground = Brushes.Wheat;
					pb.Background = Brushes.Gray;
					pb.Visibility = Visibility.Visible;
					btnPause.Visibility = Visibility.Visible;
				}
			}
			else
			{
				btnPausing.Visibility = Visibility.Collapsed;
				txtPercent.Text = "等待中";
				txtPercent.Foreground = Brushes.Gray;
				txtPercent.Visibility = Visibility.Visible;
				pb.Foreground = Brushes.Wheat;
				pb.Background = Brushes.Gray;
				pb.Visibility = Visibility.Visible;
				btnPause.Visibility = Visibility.Visible;
			}
		}

		private void btnPause_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			btnPause.Visibility = Visibility.Collapsed;
			FileDownloader instance = Singleton_FileDownloader.GetInstance(MeetingID);
			File_DownloadItemViewModel inList = instance.GetInList(MeetingID);
			instance.Pause(meetingDataDownloadFileFile.ID);
			int num = 0;
			num = ((inList == null) ? ((int)fileItem.NowPercentage) : ((int)inList.NowPercentage));
			btnPause.Visibility = Visibility.Collapsed;
			txtPercent.Text = num + " %";
			txtPercent.Foreground = Brushes.Gray;
			txtPercent.Visibility = Visibility.Visible;
			pb.Value = num;
			pb.Foreground = Brushes.Wheat;
			pb.Background = Brushes.Gray;
			pb.Visibility = Visibility.Visible;
			btnPausing.Visibility = Visibility.Visible;
		}

		private void btnDownload_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			FileDownloader instance = Singleton_FileDownloader.GetInstance(MeetingID);
			instance.AddItem(fileItem);
			btnDownload.Visibility = Visibility.Collapsed;
			txtPercent.Text = "等待中";
			txtPercent.Foreground = Brushes.Gray;
			txtPercent.Visibility = Visibility.Visible;
			pb.Foreground = Brushes.Wheat;
			pb.Background = Brushes.Gray;
			pb.Visibility = Visibility.Visible;
			btnPause.Visibility = Visibility.Visible;
			if (MeetingDataCT_RaiseAllDownload_Event != null)
			{
				MeetingDataCT_RaiseAllDownload_Event(fileItem.ID, false, false);
			}
		}

		private void InitUI()
		{
			switch (fileItem.FileCate)
			{
			case MeetingFileCate.影片檔:
				btnOpen.Source = new BitmapImage(new Uri("images/icon_video@2x.png", UriKind.Relative));
				break;
			}
			switch (fileItem.FileType)
			{
			case MeetingFileType.從未下載:
				ShowNeverDownload();
				break;
			case MeetingFileType.暫停中:
				ShowInPause(true);
				break;
			case MeetingFileType.正在下載中:
				txtPercent.Text = (int)fileItem.NowPercentage + " %";
				ShowInPause(false);
				break;
			case MeetingFileType.已下載完成:
				ShowCanOpen();
				break;
			case MeetingFileType.排入下載中:
			{
				int num = (int)fileItem.NowPercentage;
				if (num > 0)
				{
					txtPercent.Text = num + " %";
				}
				else
				{
					txtPercent.Text = "等待中";
				}
				ShowInPause(false);
				break;
			}
			case MeetingFileType.已下載過但是未完成的檔案:
				txtPercent.Text = (int)fileItem.NowPercentage + " %";
				ShowInPause(true);
				break;
			case MeetingFileType.解壓縮中:
			{
				Storyboard storyboard = (Storyboard)TryFindResource("sb");
				if (storyboard != null)
				{
					storyboard.Begin();
				}
				break;
			}
			case MeetingFileType.解壓縮失敗:
				ShowNeverDownload();
				break;
			case MeetingFileType.已經下載過一次且可以更新版本的檔案_目前下載未完成:
				txtPercent.Text = (int)fileItem.NowPercentage + " %";
				ShowInPause(true);
				btnUpdate.Visibility = Visibility.Visible;
				break;
			case MeetingFileType.已經下載過一次且可以更新版本的檔案_目前下載已完成:
				ShowCanOpen();
				btnUpdate.Visibility = Visibility.Visible;
				break;
			}
			if (IsWaitingForDownload)
			{
				btnOpen.Visibility = Visibility.Collapsed;
				btnDownload.Visibility = Visibility.Collapsed;
				btnPausing.Visibility = Visibility.Collapsed;
				txtPercent.Text = "等待中";
				txtPercent.Foreground = Brushes.Gray;
				txtPercent.Visibility = Visibility.Visible;
				pb.Foreground = Brushes.Wheat;
				pb.Background = Brushes.Gray;
				pb.Value = fileItem.NowPercentage;
				pb.Visibility = Visibility.Visible;
				btnPause.Visibility = Visibility.Visible;
			}
			switch (meetingRoomButtonType)
			{
			case MeetingRoomButtonType.ON:
			case MeetingRoomButtonType.OY:
				if (fileItem.DownloadBytes == 0 || fileItem.DownloadBytes < fileItem.TotalBytes)
				{
					pb.Visibility = Visibility.Collapsed;
					btnOpen.Visibility = Visibility.Collapsed;
					btnDownload.Visibility = Visibility.Collapsed;
					txtPercent.Text = "下載過期";
					txtPercent.Foreground = Brushes.Gray;
					txtPercent.Visibility = Visibility.Visible;
					btnUpdate.Visibility = Visibility.Collapsed;
				}
				break;
			case MeetingRoomButtonType.NO:
			case MeetingRoomButtonType.YO:
			case MeetingRoomButtonType.OO:
				pb.Visibility = Visibility.Collapsed;
				btnOpen.Visibility = Visibility.Collapsed;
				btnDownload.Visibility = Visibility.Collapsed;
				txtPercent.Text = "瀏覽過期";
				txtPercent.Foreground = Brushes.Gray;
				txtPercent.Visibility = Visibility.Visible;
				btnUpdate.Visibility = Visibility.Collapsed;
				break;
			}
			if (MeetingDataCT_IsAllFileRowFinished_AddInitUIFinished_Event != null)
			{
				IsAllFileRowFinished = MeetingDataCT_IsAllFileRowFinished_AddInitUIFinished_Event();
			}
		}

		private void ShowInPause(bool IsPausing)
		{
			txtPercent.Foreground = Brushes.Gray;
			txtPercent.Visibility = Visibility.Visible;
			pb.Value = fileItem.NowPercentage;
			pb.Foreground = Brushes.Wheat;
			pb.Background = Brushes.Gray;
			pb.Visibility = Visibility.Visible;
			if (IsPausing)
			{
				btnPausing.Visibility = Visibility.Visible;
			}
			else
			{
				btnPause.Visibility = Visibility.Visible;
			}
		}

		private void ShowNeverDownload()
		{
			txtPercent.Text = "0 %";
			pb.Value = pb.Minimum;
			pb.Visibility = Visibility.Collapsed;
			txtPercent.Visibility = Visibility.Collapsed;
			btnOpen.Visibility = Visibility.Collapsed;
			btnDownload.Visibility = Visibility.Visible;
		}

		private void ShowInDownload()
		{
		}

		private void ShowCanOpen()
		{
			btnOpen.Visibility = Visibility.Visible;
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/filerow.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				lineCenter = (Label)target;
				break;
			case 2:
				txtIndex = (TextBlock)target;
				break;
			case 3:
				txtFileName = (TextBlock)target;
				break;
			case 4:
				btnOpen = (Image)target;
				break;
			case 5:
				btnDownload = (Image)target;
				break;
			case 6:
				txtPercent = (TextBlock)target;
				break;
			case 7:
				btnPause = (Image)target;
				break;
			case 8:
				btnPausing = (Image)target;
				break;
			case 9:
				txtUnzip = (TextBlock)target;
				break;
			case 10:
				btnUpdate = (Image)target;
				break;
			case 11:
				pb = (ProgressBar)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}

		[CompilerGenerated]
		private void _003CInitSelectDB_003Eb__0()
		{
			base.Dispatcher.BeginInvoke(new Action(_003CInitSelectDB_003Eb__1));
		}

		[CompilerGenerated]
		private void _003CInitSelectDB_003Eb__2(Task task)
		{
			base.Dispatcher.BeginInvoke(new Action(_003CInitSelectDB_003Eb__3));
		}

		[CompilerGenerated]
		private void _003CInitSelectDB_003Eb__4()
		{
			fileItem = FileItemTool.Gen(meetingDataDownloadFileFile, UserID, MeetingID);
			if (fileItem.FileType == MeetingFileType.已下載完成 && MeetingDataCT_Counting_Finished_FileCount_Event != null)
			{
				MeetingDataCT_Counting_Finished_FileCount_Event();
			}
		}

		[CompilerGenerated]
		private void _003CInitSelectDB_003Eb__1()
		{
			InitUI();
			InitEvent();
		}

		[CompilerGenerated]
		private void _003CInitSelectDB_003Eb__3()
		{
			InitUI();
			InitEvent();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__6(object sender, MouseEventArgs e)
		{
			MouseTool.ShowHand();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__7(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__8(object sender, MouseEventArgs e)
		{
			MouseTool.ShowHand();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__9(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__a(object sender, MouseEventArgs e)
		{
			MouseTool.ShowHand();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__b(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__c(object sender, MouseEventArgs e)
		{
			MouseTool.ShowHand();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__d(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__e(object sender, MouseEventArgs e)
		{
			MouseTool.ShowHand();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__f(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__10()
		{
			if (IsAllFileRowFinished && MeetingDataCT_HangTheDownloadEvent_Event != null)
			{
				MeetingDataCT_HangTheDownloadEvent_Event(fileItem.ID);
			}
		}
	}
}
