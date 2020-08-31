using PaperLess_Emeeting.App_Code;
using PaperLess_Emeeting.App_Code.ClickOnce;
using PaperLess_Emeeting.App_Code.DownloadItem;
using PaperLess_Emeeting.App_Code.MessageBox;
using PaperLess_Emeeting.App_Code.ViewModel;
using PaperLess_Emeeting.App_Code.WS;
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
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace PaperLess_Emeeting
{
	public class LawRow : UserControl, IComponentConnector
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass14
		{
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

			public Dictionary<string, BookVM> cbBooksData;

			public Home Home_Window;

			public LawRow _003C_003E4__this;

			public void _003CbtnOpen_MouseLeftButtonDown_003Eb__12()
			{
				_003C_003E4__this.Dispatcher.BeginInvoke(new Action(_003CbtnOpen_MouseLeftButtonDown_003Eb__13));
			}

			public void _003CbtnOpen_MouseLeftButtonDown_003Eb__13()
			{
				ReadWindow readWindow = new ReadWindow(cbBooksData, new Home_OpenBookFromReader_Function(Home_Window.OpenBookFromReader), _bookPath, _bookId, _account, _userName, _email, _meetingId, _watermark, _dbPath, _isSync, _isSyncOwner, _webServiceUrl, _socketMessage);
				readWindow.WindowStyle = WindowStyle.None;
				readWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				readWindow.WindowState = WindowState.Maximized;
				Settings.Default.AssemblyName.Contains("TPI4F");
				readWindow.Show();
			}
		}

		private Storyboard sb;

		private LawListCT_HangTheDownloadEvent_Function LawListCT_HangTheDownloadEvent_Event;

		private LawListCT_IsAllLawRowFinished_AddInitUIFinished_Function LawListCT_IsAllLawRowFinished_AddInitUIFinished_Event;

		private LawListCT_GetBookVMs_ByMeetingFileCate_Function LawListCT_GetBookVMs_ByMeetingFileCate_Event;

		public bool IsAllLawRowFinished;

		internal TextBlock txtDate;

		internal TextBlock txtLawName;

		internal Image btnUpdate;

		internal TextBlock txtIsNew;

		internal TextBlock txtUpdatePercent;

		internal TextBlock txtUpdateUnzip;

		internal ProgressBar pbUpdate;

		internal Image btnOpen;

		internal Image btnDownload;

		internal TextBlock txtPercent;

		internal TextBlock txtUnzip;

		internal ProgressBar pb;

		internal Image btnDelete;

		private bool _contentLoaded;

		[CompilerGenerated]
		private LawDataLaw _003ClawDataLaw_003Ek__BackingField;

		[CompilerGenerated]
		private Law_DownloadItemViewModel _003ClawItem_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserID_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserName_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserPWD_003Ek__BackingField;

		[CompilerGenerated]
		private bool _003CIsLastRow_003Ek__BackingField;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegatea;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegateb;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegatec;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegated;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegatee;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegatef;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate10;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate11;

		public LawDataLaw lawDataLaw
		{
			[CompilerGenerated]
			get
			{
				return _003ClawDataLaw_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003ClawDataLaw_003Ek__BackingField = value;
			}
		}

		public Law_DownloadItemViewModel lawItem
		{
			[CompilerGenerated]
			get
			{
				return _003ClawItem_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003ClawItem_003Ek__BackingField = value;
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

		public LawRow(string UserID, string UserName, string UserPWD, bool IsLastRow, LawDataLaw lawDataLaw, LawListCT_HangTheDownloadEvent_Function callback1, LawListCT_IsAllLawRowFinished_AddInitUIFinished_Function callback2, LawListCT_GetBookVMs_ByMeetingFileCate_Function callback3)
		{
			InitializeComponent();
			this.UserID = UserID;
			this.UserName = UserName;
			this.UserPWD = UserPWD;
			this.IsLastRow = IsLastRow;
			this.lawDataLaw = lawDataLaw;
			base.Loaded += new RoutedEventHandler(LawRow_Loaded);
			LawListCT_HangTheDownloadEvent_Event = callback1;
			LawListCT_IsAllLawRowFinished_AddInitUIFinished_Event = callback2;
			LawListCT_GetBookVMs_ByMeetingFileCate_Event = callback3;
			lawItem = new Law_DownloadItemViewModel();
		}

		private void LawRow_Loaded(object sender, RoutedEventArgs e)
		{
			txtDate.Text = lawDataLaw.UpDate.Split(' ')[0];
			txtLawName.Text = lawDataLaw.Name;
			Task.Factory.StartNew(new Action(_003CLawRow_Loaded_003Eb__0));
		}

		private void InitSelectDB()
		{
			DataTable dataTable = new DataTable();
			LawDownloader instance = Singleton_LawDownloader.GetInstance();
			lawItem = instance.GetInList(lawDataLaw.ID);
			if (lawItem != null)
			{
				return;
			}
			DateTime dateTime = DateTime.Parse("2010/01/01");
			long num = 0L;
			long num2 = 0L;
			dataTable = MSCE.GetDataTable("SELECT ID,AtDownloadFinished_XmlUpDate,Link,StorageFileName,DownloadBytes,TotalBytes FROM LawRow where ID=@1 and UserID=@2", lawDataLaw.ID, UserID);
			if (dataTable.Rows.Count > 0)
			{
				dataTable.Rows[0]["ID"].ToString();
				dateTime = (DateTime)dataTable.Rows[0]["AtDownloadFinished_XmlUpDate"];
				dataTable.Rows[0]["Link"].ToString();
				dataTable.Rows[0]["StorageFileName"].ToString();
				num = long.Parse(dataTable.Rows[0]["DownloadBytes"].ToString().Equals("") ? "0" : dataTable.Rows[0]["DownloadBytes"].ToString());
				num2 = long.Parse(dataTable.Rows[0]["TotalBytes"].ToString().Equals("") ? "0" : dataTable.Rows[0]["TotalBytes"].ToString());
			}
			else
			{
				string text = "INSERT INTO LawRow(ID,AtDownloadFinished_XmlUpDate,DownloadBytes,TotalBytes,UserID) \r\n                                                    VALUES(@1,'2010/01/01',0,0,@2)";
				int num3 = MSCE.ExecuteNonQuery(text, lawDataLaw.ID, UserID);
				if (num3 < 1)
				{
					LogTool.Debug(new Exception("DB失敗: " + text));
				}
			}
			lawItem = new Law_DownloadItemViewModel();
			lawItem.ID = lawDataLaw.ID;
			lawItem.UserID = UserID;
			DateTime result;
			DateTime.TryParse(lawDataLaw.UpDate, out result);
			lawItem.UpDate = result;
			lawItem.Name = lawDataLaw.Name;
			lawItem.Link = lawDataLaw.Link;
			lawItem.Status = (LawDataStatus)Enum.Parse(typeof(LawDataStatus), lawDataLaw.Status);
			string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			string law_StorageFileFolder = Settings.Default.Law_StorageFileFolder2;
			lawItem.StorageFileFolder = Path.Combine(baseDirectory, law_StorageFileFolder);
			string arg = DateTime.Now.ToFileTime().ToString();
			try
			{
				Uri uri = new Uri(lawItem.Link);
				string fileName = Path.GetFileName(uri.LocalPath);
				if (!fileName.Equals("/"))
				{
					arg = fileName;
				}
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
			lawItem.StorageFileName = string.Format("{0}_{1}_{2}", UserID, lawDataLaw.ID, arg);
			lawItem.UnZipFileFolder = Path.Combine(ClickOnceTool.GetFilePath(), Settings.Default.Law_UnZipFileFolder);
			if (num == 0 || num < num2)
			{
				if (File.Exists(lawItem.StorageFilePath))
				{
					File.Delete(lawItem.StorageFilePath);
				}
				lawItem.DownloadBytes = 0L;
				lawItem.TotalBytes = 0L;
				lawItem.FileType = LawFileType.從未下載;
			}
			else if (TimeSpan.Compare(new TimeSpan(result.Ticks), new TimeSpan(dateTime.Ticks)) > 0)
			{
				lawItem.StorageFileName += ".update";
				lawItem.DownloadBytes = 0L;
				lawItem.TotalBytes = 0L;
				lawItem.FileType = LawFileType.更新檔未下載;
			}
			else
			{
				lawItem.FileType = LawFileType.已下載完成;
			}
		}

		private void InitEvent()
		{
			Image image = btnDownload;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegatea == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegatea = new MouseEventHandler(_003CInitEvent_003Eb__2);
			}
			image.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegatea;
			Image image2 = btnDownload;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegateb == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegateb = new MouseEventHandler(_003CInitEvent_003Eb__3);
			}
			image2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegateb;
			btnDownload.MouseLeftButtonDown += new MouseButtonEventHandler(btnDownload_MouseLeftButtonDown);
			Image image3 = btnDelete;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegatec == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegatec = new MouseEventHandler(_003CInitEvent_003Eb__4);
			}
			image3.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegatec;
			Image image4 = btnDelete;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegated == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegated = new MouseEventHandler(_003CInitEvent_003Eb__5);
			}
			image4.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegated;
			btnDelete.MouseLeftButtonDown += new MouseButtonEventHandler(btnDelete_MouseLeftButtonDown);
			Image image5 = btnOpen;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegatee == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegatee = new MouseEventHandler(_003CInitEvent_003Eb__6);
			}
			image5.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegatee;
			Image image6 = btnOpen;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegatef == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegatef = new MouseEventHandler(_003CInitEvent_003Eb__7);
			}
			image6.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegatef;
			btnOpen.MouseLeftButtonDown += new MouseButtonEventHandler(btnOpen_MouseLeftButtonDown);
			Image image7 = btnUpdate;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate10 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate10 = new MouseEventHandler(_003CInitEvent_003Eb__8);
			}
			image7.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate10;
			Image image8 = btnUpdate;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate11 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate11 = new MouseEventHandler(_003CInitEvent_003Eb__9);
			}
			image8.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate11;
			btnUpdate.MouseLeftButtonDown += new MouseButtonEventHandler(btnDownload_MouseLeftButtonDown);
			if (IsAllLawRowFinished && LawListCT_HangTheDownloadEvent_Event != null)
			{
				LawListCT_HangTheDownloadEvent_Event(lawItem.ID);
			}
		}

		private void btnOpen_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			MouseTool.ShowLoading();
			try
			{
				_003C_003Ec__DisplayClass14 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass14();
				_003C_003Ec__DisplayClass._003C_003E4__this = this;
				ClickOnceTool.GetFilePath();
				_003C_003Ec__DisplayClass._bookPath = lawItem.UnZipFilePath;
				_003C_003Ec__DisplayClass._bookId = "";
				_003C_003Ec__DisplayClass._account = "";
				_003C_003Ec__DisplayClass._userName = "";
				_003C_003Ec__DisplayClass._email = "";
				_003C_003Ec__DisplayClass._meetingId = "";
				_003C_003Ec__DisplayClass._watermark = "";
				_003C_003Ec__DisplayClass._dbPath = Path.Combine(ClickOnceTool.GetDataPath(), Settings.Default.bookInfo_Path);
				_003C_003Ec__DisplayClass._isSync = false;
				_003C_003Ec__DisplayClass._isSyncOwner = false;
				_003C_003Ec__DisplayClass._webServiceUrl = WsTool.GetUrl() + "/AnnotationUpload";
				_003C_003Ec__DisplayClass._socketMessage = "";
				_003C_003Ec__DisplayClass.cbBooksData = null;
				_003C_003Ec__DisplayClass.Home_Window = Enumerable.FirstOrDefault(Enumerable.OfType<Home>(Application.Current.Windows));
				_003C_003Ec__DisplayClass.Home_Window.CloseAllWindow("", true);
				Task.Factory.StartNew(new Action(_003C_003Ec__DisplayClass._003CbtnOpen_MouseLeftButtonDown_003Eb__12));
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
			MouseTool.ShowArrow();
		}

		private void btnDelete_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			MessageBoxResult messageBoxResult = MessageBox.Show("您確定要刪除檔案?", "系統訊息", MessageBoxButton.YesNo);
			if (messageBoxResult == MessageBoxResult.No)
			{
				return;
			}
			string text = "update  LawRow set DownloadBytes=0,TotalBytes=0 where ID=@1 and UserID=@2";
			int num = MSCE.ExecuteNonQuery(text, lawDataLaw.ID, UserID);
			if (num < 1)
			{
				AutoClosingMessageBox.Show("刪除失敗");
				LogTool.Debug(new Exception("DB失敗: " + text));
				return;
			}
			if (File.Exists(lawItem.StorageFilePath))
			{
				File.Delete(lawItem.StorageFilePath);
			}
			ShowNeverDownload(true);
		}

		private void btnDownload_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (lawItem != null)
			{
				btnDownload.Visibility = Visibility.Collapsed;
				txtPercent.Text = "等待中";
				txtPercent.Foreground = Brushes.Gray;
				txtPercent.Visibility = Visibility.Visible;
				pb.Foreground = Brushes.Wheat;
				pb.Background = Brushes.Gray;
				pb.Visibility = Visibility.Visible;
				if (lawItem.StorageFileName.EndsWith(".update"))
				{
					lawItem.FileType = LawFileType.更新檔排入下載中;
				}
				else
				{
					lawItem.FileType = LawFileType.排入下載中;
				}
				LawDownloader instance = Singleton_LawDownloader.GetInstance();
				instance.AddItem(lawItem);
			}
		}

		private void InitUI()
		{
			switch (lawItem.FileType)
			{
			case LawFileType.從未下載:
				ShowNeverDownload(false);
				break;
			case LawFileType.正在下載中:
				ShowInDownload();
				txtPercent.Text = (int)lawItem.NowPercentage + " %";
				pb.Value = lawItem.NowPercentage;
				pb.Foreground = Brushes.Orange;
				pb.Background = Brushes.Black;
				txtPercent.Visibility = Visibility.Visible;
				pb.Visibility = Visibility.Visible;
				break;
			case LawFileType.已下載完成:
				ShowCanOpen();
				txtIsNew.Visibility = Visibility.Visible;
				break;
			case LawFileType.排入下載中:
				ShowInDownload();
				txtPercent.Text = "等待中";
				txtPercent.Foreground = Brushes.Gray;
				txtPercent.Visibility = Visibility.Visible;
				pb.Foreground = Brushes.Wheat;
				pb.Background = Brushes.Gray;
				pb.Visibility = Visibility.Visible;
				break;
			case LawFileType.解壓縮中:
			{
				Storyboard storyboard = (Storyboard)TryFindResource("sb");
				if (storyboard != null)
				{
					storyboard.Begin();
				}
				break;
			}
			case LawFileType.解壓縮失敗:
				ShowNeverDownload(false);
				break;
			case LawFileType.更新檔未下載:
				ShowCanOpen();
				break;
			case LawFileType.更新檔暫停中:
				ShowCanOpen();
				break;
			case LawFileType.更新檔正在下載中:
				ShowCanOpen();
				break;
			case LawFileType.更新檔已下載完成:
				ShowCanOpen();
				break;
			case LawFileType.更新檔排入下載中:
				ShowCanOpen();
				txtUpdatePercent.Text = "等待中";
				txtIsNew.Foreground = Brushes.Gray;
				txtUpdatePercent.Visibility = Visibility.Visible;
				pb.Foreground = Brushes.Wheat;
				pb.Background = Brushes.Gray;
				pb.Visibility = Visibility.Visible;
				break;
			case LawFileType.更新檔解壓縮中:
			{
				Storyboard storyboard = (Storyboard)TryFindResource("sbUpdate");
				if (storyboard != null)
				{
					storyboard.Begin();
				}
				break;
			}
			case LawFileType.更新檔解壓縮失敗:
				ShowCanOpen();
				break;
			}
			if (LawListCT_IsAllLawRowFinished_AddInitUIFinished_Event != null)
			{
				IsAllLawRowFinished = LawListCT_IsAllLawRowFinished_AddInitUIFinished_Event();
			}
		}

		private void HideAll()
		{
			btnDelete.Visibility = Visibility.Collapsed;
			btnOpen.Visibility = Visibility.Collapsed;
			btnUpdate.Visibility = Visibility.Collapsed;
			pb.Visibility = Visibility.Collapsed;
			txtIsNew.Visibility = Visibility.Collapsed;
			btnDownload.Visibility = Visibility.Visible;
			pbUpdate.Visibility = Visibility.Collapsed;
			txtUpdatePercent.Visibility = Visibility.Collapsed;
		}

		private void ShowNeverDownload(bool fromBtnDelete)
		{
			if (fromBtnDelete)
			{
				btnDelete.Visibility = Visibility.Collapsed;
				btnOpen.Visibility = Visibility.Collapsed;
				btnUpdate.Visibility = Visibility.Collapsed;
				pb.Visibility = Visibility.Collapsed;
				txtIsNew.Visibility = Visibility.Collapsed;
				pbUpdate.Visibility = Visibility.Collapsed;
				txtUpdatePercent.Visibility = Visibility.Collapsed;
			}
			btnDownload.Visibility = Visibility.Visible;
		}

		private void ShowInDownload()
		{
		}

		private void ShowCanOpen()
		{
			btnDelete.Visibility = Visibility.Visible;
			btnOpen.Visibility = Visibility.Visible;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/lawrow.xaml", UriKind.Relative);
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
				txtDate = (TextBlock)target;
				break;
			case 2:
				txtLawName = (TextBlock)target;
				break;
			case 3:
				btnUpdate = (Image)target;
				break;
			case 4:
				txtIsNew = (TextBlock)target;
				break;
			case 5:
				txtUpdatePercent = (TextBlock)target;
				break;
			case 6:
				txtUpdateUnzip = (TextBlock)target;
				break;
			case 7:
				pbUpdate = (ProgressBar)target;
				break;
			case 8:
				btnOpen = (Image)target;
				break;
			case 9:
				btnDownload = (Image)target;
				break;
			case 10:
				txtPercent = (TextBlock)target;
				break;
			case 11:
				txtUnzip = (TextBlock)target;
				break;
			case 12:
				pb = (ProgressBar)target;
				break;
			case 13:
				btnDelete = (Image)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}

		[CompilerGenerated]
		private void _003CLawRow_Loaded_003Eb__0()
		{
			InitSelectDB();
			base.Dispatcher.BeginInvoke(new Action(_003CLawRow_Loaded_003Eb__1));
		}

		[CompilerGenerated]
		private void _003CLawRow_Loaded_003Eb__1()
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
	}
}
