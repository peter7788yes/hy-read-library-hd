using Newtonsoft.Json;
using PaperLess_Emeeting.App_Code;
using PaperLess_Emeeting.App_Code.ClickOnce;
using PaperLess_Emeeting.App_Code.DownloadItem;
using PaperLess_Emeeting.App_Code.MessageBox;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace PaperLess_Emeeting
{
	public class MeetingRoom : UserControl, IComponentConnector
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass21
		{
			public MeetingRoom _003C_003E4__this;

			public object sender;

			public void _003CbtnDownload_btnPausing_MouseLeftButtonDown_003Eb__1f(MeetingData md)
			{
				_003C_003E4__this.GetMeetingData_DoAction(md, (Image)sender);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass25
		{
			public MeetingRoom _003C_003E4__this;

			public MeetingData md;

			public void _003CGetMeetingData_DoAction_003Eb__23()
			{
				FileDownloader instance = Singleton_FileDownloader.GetInstance(md.ID);
				instance.Stop();
				List<MeetingDataDownloadFileFile> list = new List<MeetingDataDownloadFileFile>();
				try
				{
					MeetingDataMeetingsFileFile[] fileList = md.MeetingsFile.FileList;
					foreach (MeetingDataMeetingsFileFile meetingDataMeetingsFileFile in fileList)
					{
						MeetingDataDownloadFileFile meetingDataDownloadFileFile = new MeetingDataDownloadFileFile();
						meetingDataDownloadFileFile.AgendaID = "record";
						meetingDataDownloadFileFile.FileName = meetingDataMeetingsFileFile.FileName;
						meetingDataDownloadFileFile.ID = meetingDataMeetingsFileFile.ID;
						meetingDataDownloadFileFile.Url = meetingDataMeetingsFileFile.Url;
						meetingDataDownloadFileFile.version = meetingDataMeetingsFileFile.version;
						list.Add(meetingDataDownloadFileFile);
					}
				}
				catch (Exception ex)
				{
					LogTool.Debug(ex);
				}
				list.AddRange(Enumerable.ToList(md.DownloadFile.DownloadFileList));
				_003C_003E4__this.TotalFiles = list.Count;
				List<File_DownloadItemViewModel> list2 = new List<File_DownloadItemViewModel>();
				foreach (MeetingDataDownloadFileFile item in list)
				{
					File_DownloadItemViewModel file_DownloadItemViewModel = FileItemTool.Gen(item, _003C_003E4__this.UserID, md.ID);
					if (file_DownloadItemViewModel.DownloadBytes == 0 || file_DownloadItemViewModel.DownloadBytes < file_DownloadItemViewModel.TotalBytes)
					{
						list2.Add(file_DownloadItemViewModel);
					}
				}
				if (!instance.HasMeetingRoom_DownloadFileStart_Event())
				{
					instance.MeetingRoom_DownloadFileStart_Event += _003C_003E4__this.Start_callback;
				}
				if (!instance.HasMeetingRoom_DownloadProgressChanged_Event())
				{
					instance.MeetingRoom_DownloadProgressChanged_Event += _003C_003E4__this.Progress_callback;
				}
				if (!instance.HasMeetingRoom_DownloadFileToErrorCompleted_Event())
				{
					instance.MeetingRoom_DownloadFileToErrorCompleted_Event += _003C_003E4__this.ErrorFinish_callback;
				}
				instance.AddItem(list2);
			}
		}

		private MeetingRoomButtonType meetingRoomButtonType;

		public string NewAddMeetingID = "";

		public bool invisible;

		private MeetingRoom_DownloadFileStart_Function Start_callback;

		private MeetingRoom_DownloadProgressChanged_Function Progress_callback;

		private MeetingRoom_DownloadFileToErrorCompleted_Function ErrorFinish_callback;

		private int TotalFiles;

		private int FinishedFiles;

		private int FinishedPercent;

		public DispatcherTimer ajaxTimer;

		public bool ForceStopAjaxLoader;

		public bool HasReceiveStart_callback;

		public MeetingData PreLoadMeetingData;

		public int CacheMinuteTTL;

		public Thread CacheThread;

		private int i;

		internal Grid grid;

		internal Border RoomBG;

		internal Border RoomInnerBG;

		internal Image btnSeries;

		internal TextBlock txtMeetingName;

		internal TextBlock txtLocation;

		internal TextBlock txtTime;

		internal Image ajaxLoader;

		internal Image btnDownload;

		internal Image btnDownloadForbidden;

		internal Image btnRead2Forbidden;

		internal Image btnDelete;

		internal Image btnRefresh;

		internal TextBlock txtPercent;

		internal Image btnPause;

		internal Image btnPausing;

		internal ProgressBar pb;

		private bool _contentLoaded;

		[CompilerGenerated]
		private UserMeeting _003CuserMeeting_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserID_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserPWD_003Ek__BackingField;

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

		public UserMeeting userMeeting
		{
			[CompilerGenerated]
			get
			{
				return _003CuserMeeting_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CuserMeeting_003Ek__BackingField = value;
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

		public event Home_Change2MeetingDataCT_Function Home_Change2MeetingDataCT_Event;

		public MeetingRoom(string UserID, string UserPWD, UserMeeting userMeeting, Home_Change2MeetingDataCT_Function callback, string NewAddMeetingID = "", bool invisible = false)
		{
			InitializeComponent();
			this.UserID = UserID;
			this.UserPWD = UserPWD;
			this.userMeeting = userMeeting;
			Home_Change2MeetingDataCT_Event += callback;
			this.NewAddMeetingID = NewAddMeetingID;
			this.invisible = invisible;
			if (this.invisible)
			{
				base.Visibility = Visibility.Collapsed;
			}
			base.Loaded += new RoutedEventHandler(MeetingRoom_Loaded);
			base.Unloaded += new RoutedEventHandler(MeetingRoom_Unloaded);
			CacheMinuteTTL = Settings.Default.CacheMinuteTTL;
		}

		private void MeetingRoom_Loaded(object sender, RoutedEventArgs e)
		{
			Task.Factory.StartNew(new Action(_003CMeetingRoom_Loaded_003Eb__0));
		}

		private void PreLoadMeeting()
		{
			Action action = null;
			if (CacheMinuteTTL >= 0)
			{
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003CPreLoadMeeting_003Eb__2);
				}
				factory.StartNew(action);
			}
		}

		private void MeetingRoom_Unloaded(object sender, RoutedEventArgs e)
		{
			Singleton_FileDownloader.GetInstance(userMeeting.ID).ClearMeetingRoomEvent();
		}

		private void InitSelectDB()
		{
			DataTable dataTable = MSCE.GetDataTable("select count(ID) as TotalFiles from FileRow where UserID =@1 and MeetingID=@2;", UserID, userMeeting.ID);
			if (dataTable.Rows.Count > 0)
			{
				int.TryParse(dataTable.Rows[0]["TotalFiles"].ToString(), out TotalFiles);
			}
			FinishedPercent = GetFinishedPercent();
			if (userMeeting.isDownload != null && userMeeting.isBrowserd != null)
			{
				Enum.TryParse(userMeeting.isDownload + userMeeting.isBrowserd, out meetingRoomButtonType);
			}
			else
			{
				meetingRoomButtonType = MeetingRoomButtonType.YY;
			}
		}

		private void InitEvent()
		{
			FileDownloader instance = Singleton_FileDownloader.GetInstance(userMeeting.ID);
			Start_callback = new MeetingRoom_DownloadFileStart_Function(MeetingRoom_DownloadFileStart_Callback);
			Progress_callback = new MeetingRoom_DownloadProgressChanged_Function(MeetingRoom_DownloadProgressChanged_Callback);
			ErrorFinish_callback = new MeetingRoom_DownloadFileToErrorCompleted_Function(MeetingRoom_DownloadFileToErrorCompleted_Callback);
			instance.MeetingRoom_DownloadFileStart_Event += Start_callback;
			instance.MeetingRoom_DownloadProgressChanged_Event += Progress_callback;
			instance.MeetingRoom_DownloadFileToErrorCompleted_Event += ErrorFinish_callback;
			base.MouseLeftButtonDown += new MouseButtonEventHandler(MeetingRoom_MouseLeftButtonDown);
			Image image = btnSeries;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate13 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate13 = new MouseEventHandler(_003CInitEvent_003Eb__7);
			}
			image.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate13;
			Image image2 = btnSeries;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate14 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate14 = new MouseEventHandler(_003CInitEvent_003Eb__8);
			}
			image2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate14;
			btnSeries.MouseLeftButtonDown += new MouseButtonEventHandler(btnSeries_MouseLeftButtonDown);
			Image image3 = btnDelete;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate15 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate15 = new MouseEventHandler(_003CInitEvent_003Eb__9);
			}
			image3.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate15;
			Image image4 = btnDelete;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate16 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate16 = new MouseEventHandler(_003CInitEvent_003Eb__a);
			}
			image4.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate16;
			btnDelete.MouseLeftButtonDown += new MouseButtonEventHandler(btnDelete_MouseLeftButtonDown);
			Image image5 = btnRefresh;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate17 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate17 = new MouseEventHandler(_003CInitEvent_003Eb__b);
			}
			image5.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate17;
			Image image6 = btnRefresh;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate18 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate18 = new MouseEventHandler(_003CInitEvent_003Eb__c);
			}
			image6.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate18;
			btnRefresh.MouseLeftButtonDown += new MouseButtonEventHandler(btnDownload_btnPausing_MouseLeftButtonDown);
			Image image7 = btnDownload;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate19 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate19 = new MouseEventHandler(_003CInitEvent_003Eb__d);
			}
			image7.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate19;
			Image image8 = btnDownload;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate1a == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate1a = new MouseEventHandler(_003CInitEvent_003Eb__e);
			}
			image8.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate1a;
			btnDownload.MouseLeftButtonDown += new MouseButtonEventHandler(btnDownload_btnPausing_MouseLeftButtonDown);
			Image image9 = btnPause;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate1b == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate1b = new MouseEventHandler(_003CInitEvent_003Eb__f);
			}
			image9.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate1b;
			Image image10 = btnPause;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate1c == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate1c = new MouseEventHandler(_003CInitEvent_003Eb__10);
			}
			image10.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate1c;
			btnPause.MouseLeftButtonDown += new MouseButtonEventHandler(btnPause_MouseLeftButtonDown);
			Image image11 = btnPausing;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate1d == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate1d = new MouseEventHandler(_003CInitEvent_003Eb__11);
			}
			image11.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate1d;
			Image image12 = btnPausing;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate1e == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate1e = new MouseEventHandler(_003CInitEvent_003Eb__12);
			}
			image12.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate1e;
			btnPausing.MouseLeftButtonDown += new MouseButtonEventHandler(btnDownload_btnPausing_MouseLeftButtonDown);
		}

		private void btnDownload_btnPausing_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Action<MeetingData> action = null;
			_003C_003Ec__DisplayClass21 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass21();
			_003C_003Ec__DisplayClass.sender = sender;
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			e.Handled = true;
			MouseTool.ShowLoading();
			if (NetworkTool.CheckNetwork() > 0)
			{
				string iD = userMeeting.ID;
				string userID = UserID;
				string userPWD = UserPWD;
				if (action == null)
				{
					action = new Action<MeetingData>(_003C_003Ec__DisplayClass._003CbtnDownload_btnPausing_MouseLeftButtonDown_003Eb__1f);
				}
				GetMeetingData.AsyncPOST(iD, userID, userPWD, action);
			}
			else
			{
				DataTable dataTable = MSCE.GetDataTable("select MeetingJson from MeetingData where MeetingID=@1 and UserID =@2", userMeeting.ID, UserID);
				if (dataTable.Rows.Count > 0)
				{
					MeetingData md = JsonConvert.DeserializeObject<MeetingData>(dataTable.Rows[0]["MeetingJson"].ToString());
					GetMeetingData_DoAction(md, (Image)_003C_003Ec__DisplayClass.sender);
				}
				else
				{
					AutoClosingMessageBox.Show("無法取得資料，請稍後再試");
					MouseTool.ShowArrow();
				}
			}
		}

		private void GetMeetingData_DoAction(MeetingData md, Image btnImage)
		{
			Action action = null;
			_003C_003Ec__DisplayClass25 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass25();
			_003C_003Ec__DisplayClass.md = md;
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<MeetingData, Image>(GetMeetingData_DoAction), _003C_003Ec__DisplayClass.md, btnImage);
				return;
			}
			if (_003C_003Ec__DisplayClass.md != null)
			{
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003C_003Ec__DisplayClass._003CGetMeetingData_DoAction_003Eb__23);
				}
				factory.StartNew(action);
				btnImage.Visibility = Visibility.Collapsed;
				if (btnImage.Name.Equals(btnDownload.Name))
				{
					txtPercent.Text = "0 %";
					pb.Value = pb.Minimum;
				}
				txtPercent.Foreground = Brushes.Black;
				pb.Foreground = Brushes.Orange;
				pb.Background = Brushes.Black;
				txtPercent.Visibility = Visibility.Visible;
				pb.Visibility = Visibility.Visible;
				btnPause.Visibility = Visibility.Visible;
			}
			else
			{
				AutoClosingMessageBox.Show("無法取得資料，請稍後再試");
			}
			MouseTool.ShowArrow();
		}

		private void btnDelete_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
			string messageBoxText = "您確定要刪除檔案?";
			switch (meetingRoomButtonType)
			{
			case MeetingRoomButtonType.OY:
				messageBoxText = string.Format("{0}附件資料下載時間已過期，是否確認刪除？", Settings.Default.CourseOrMeeting_String);
				break;
			case MeetingRoomButtonType.YO:
				messageBoxText = string.Format("{0}附件資料下載時間已過期，是否確認刪除？", Settings.Default.CourseOrMeeting_String);
				break;
			}
			MessageBoxResult messageBoxResult = MessageBox.Show(messageBoxText, "系統訊息", MessageBoxButton.YesNo);
			if (messageBoxResult == MessageBoxResult.Yes)
			{
				MouseTool.ShowLoading();
				bool flag = false;
				try
				{
					DataTable dataTable = MSCE.GetDataTable("select ID,StorageFileName from FileRow where UserID =@1 and MeetingID=@2 and DownloadBytes!=0 and DownloadBytes >= TotalBytes;", UserID, userMeeting.ID);
					foreach (DataRow row in dataTable.Rows)
					{
						string filePath = ClickOnceTool.GetFilePath();
						string file_StorageFileFolder = Settings.Default.File_StorageFileFolder;
						string file_UnZipFileFolder = Settings.Default.File_UnZipFileFolder;
						string path = filePath + "\\" + file_StorageFileFolder + "\\" + row["StorageFileName"].ToString();
						string dirPath = filePath + "\\" + file_UnZipFileFolder + "\\" + UserID + "\\" + userMeeting.ID + "\\" + row["ID"].ToString();
						if (File.Exists(path))
						{
							File.Delete(path);
						}
						DirectoryTool.FullDeleteDirectories(dirPath);
					}
					string text = "update FileRow set DownloadBytes=0,TotalBytes=0,FinishedFileVersion=0 where UserID =@1 and MeetingID=@2 ";
					int num = MSCE.ExecuteNonQuery(text, UserID, userMeeting.ID);
					if (num < 1)
					{
						LogTool.Debug(new Exception("DB失敗: " + text));
					}
					else
					{
						flag = true;
					}
				}
				catch (Exception ex)
				{
					LogTool.Debug(ex);
				}
				MouseTool.ShowArrow();
				if (!flag)
				{
					AutoClosingMessageBox.Show("刪除失敗");
					return;
				}
				btnDelete.Visibility = Visibility.Collapsed;
				btnRefresh.Visibility = Visibility.Visible;
				pb.Value = pb.Minimum;
				txtPercent.Text = "0 %";
			}
		}

		private void btnPause_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
			Task.Factory.StartNew(new Action(_003CbtnPause_MouseLeftButtonDown_003Eb__27));
			btnPause.Visibility = Visibility.Collapsed;
			txtPercent.Foreground = Brushes.Gray;
			pb.Foreground = Brushes.Wheat;
			pb.Background = Brushes.Gray;
			txtPercent.Visibility = Visibility.Visible;
			btnPausing.Visibility = Visibility.Visible;
		}

		private void MeetingRoom_DownloadFileToErrorCompleted_Callback(File_DownloadItemViewModel fileItem)
		{
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action<File_DownloadItemViewModel>(MeetingRoom_DownloadFileToErrorCompleted_Callback), fileItem);
				return;
			}
			ForceStopAjaxLoader = true;
			double nowMeetingFilesTotalPercent = GetNowMeetingFilesTotalPercent(fileItem);
			btnDownloadForbidden.Visibility = Visibility.Collapsed;
			btnRead2Forbidden.Visibility = Visibility.Collapsed;
			btnDelete.Visibility = Visibility.Collapsed;
			btnDownload.Visibility = Visibility.Collapsed;
			btnPause.Visibility = Visibility.Collapsed;
			txtPercent.Text = (int)nowMeetingFilesTotalPercent + " %";
			txtPercent.Foreground = Brushes.Black;
			txtPercent.Visibility = Visibility.Visible;
			pb.Value = nowMeetingFilesTotalPercent;
			pb.Foreground = Brushes.Orange;
			pb.Background = Brushes.Black;
			pb.Visibility = Visibility.Visible;
			btnPausing.Visibility = Visibility.Visible;
		}

		private void MeetingRoom_DownloadProgressChanged_Callback(File_DownloadItemViewModel fileItem, bool ForceUpdate)
		{
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<File_DownloadItemViewModel, bool>(MeetingRoom_DownloadProgressChanged_Callback), fileItem, ForceUpdate);
				return;
			}
			ForceStopAjaxLoader = true;
			double nowMeetingFilesTotalPercent = GetNowMeetingFilesTotalPercent(fileItem);
			if (nowMeetingFilesTotalPercent >= 100.0)
			{
				btnDownloadForbidden.Visibility = Visibility.Collapsed;
				btnRead2Forbidden.Visibility = Visibility.Collapsed;
				btnDownload.Visibility = Visibility.Collapsed;
				btnPausing.Visibility = Visibility.Collapsed;
				txtPercent.Visibility = Visibility.Collapsed;
				pb.Visibility = Visibility.Collapsed;
				btnPause.Visibility = Visibility.Collapsed;
				btnDelete.Visibility = Visibility.Visible;
			}
			else if (nowMeetingFilesTotalPercent - pb.Value > 1.0 || ForceUpdate)
			{
				btnDownloadForbidden.Visibility = Visibility.Collapsed;
				btnRead2Forbidden.Visibility = Visibility.Collapsed;
				btnDelete.Visibility = Visibility.Collapsed;
				btnDownload.Visibility = Visibility.Collapsed;
				btnPausing.Visibility = Visibility.Collapsed;
				txtPercent.Text = (int)nowMeetingFilesTotalPercent + " %";
				txtPercent.Foreground = Brushes.Black;
				txtPercent.Visibility = Visibility.Visible;
				pb.Value = nowMeetingFilesTotalPercent;
				pb.Foreground = Brushes.Orange;
				pb.Background = Brushes.Black;
				pb.Visibility = Visibility.Visible;
				btnPause.Visibility = Visibility.Visible;
			}
		}

		private double GetNowMeetingFilesTotalPercent(File_DownloadItemViewModel fileItem)
		{
			double num = 0.0;
			DataTable dataTable = MSCE.GetDataTable("select count(ID) as FinishedFiles from FileRow where UserID =@1 and MeetingID=@2 and DownloadBytes!=0 and DownloadBytes >= TotalBytes;", UserID, userMeeting.ID);
			if (dataTable.Rows.Count > 0)
			{
				int.TryParse(dataTable.Rows[0]["FinishedFiles"].ToString(), out FinishedFiles);
			}
			if (TotalFiles > 0)
			{
				num = (double)FinishedFiles / (double)TotalFiles * 100.0;
				if (fileItem.NowPercentage < 100.0)
				{
					num += fileItem.NowPercentage / 100.0 * (100.0 / (double)TotalFiles);
				}
			}
			return num;
		}

		private void MeetingRoom_DownloadFileStart_Callback(File_DownloadItemViewModel fileItem)
		{
			if (!HasReceiveStart_callback)
			{
				HasReceiveStart_callback = true;
				MeetingRoom_DownloadProgressChanged_Callback(fileItem, true);
			}
		}

		private void btnSeries_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
			Home home = Enumerable.FirstOrDefault(Enumerable.OfType<Home>(Application.Current.Windows));
			home.ShowBtnSeriesCT(userMeeting.SeriesMeetingID);
		}

		private void MeetingRoom_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			string text = "Update NowLogin set MeetingID=@1,AllowIpRange='' ";
			int num = MSCE.ExecuteNonQuery(text, userMeeting.ID);
			if (num < 1)
			{
				LogTool.Debug(new Exception("DB失敗: " + text));
			}
			else
			{
				this.Home_Change2MeetingDataCT_Event(userMeeting.ID, PreLoadMeetingData);
			}
		}

		private void InitUI()
		{
			try
			{
				if (DateTime.Parse(userMeeting.BeginTime) <= DateTime.Now && DateTime.Now < DateTime.Parse(userMeeting.EndTime))
				{
					txtMeetingName.Foreground = Brushes.White;
					txtLocation.Foreground = Brushes.White;
					txtTime.Foreground = Brushes.White;
					RoomInnerBG.Visibility = Visibility.Visible;
					if (NewAddMeetingID.Equals(userMeeting.ID))
					{
						RoomBG.Background = Brushes.Yellow;
					}
				}
				else
				{
					txtMeetingName.Foreground = Brushes.Black;
					txtLocation.Foreground = Brushes.Black;
					txtTime.Foreground = Brushes.Black;
					if (NewAddMeetingID.Equals(userMeeting.ID))
					{
						RoomBG.Background = Brushes.Yellow;
						RoomInnerBG.Background = Brushes.White;
						RoomInnerBG.Visibility = Visibility.Visible;
					}
				}
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
			txtMeetingName.Text = userMeeting.Name;
			txtLocation.Text = userMeeting.Location;
			txtTime.Text = DateTime.Parse(userMeeting.BeginTime).ToString("HH:mm");
			if (userMeeting.SeriesMeetingID != null && userMeeting.SeriesMeetingID.Length > 0)
			{
				btnSeries.Visibility = Visibility.Visible;
			}
			base.Visibility = Visibility.Visible;
			switch (meetingRoomButtonType)
			{
			case MeetingRoomButtonType.YY:
				if (FinishedPercent >= 100)
				{
					btnDelete.Visibility = Visibility.Visible;
					break;
				}
				if (FinishedPercent < 1)
				{
					btnDownload.Visibility = Visibility.Visible;
					break;
				}
				ajaxTimer = new DispatcherTimer();
				ajaxTimer.Interval = TimeSpan.FromMilliseconds(30.0);
				ajaxTimer.Tick += new EventHandler(ajaxLoader_ChangeImage);
				ajaxTimer.Start();
				break;
			case MeetingRoomButtonType.OY:
				if (FinishedPercent >= 100)
				{
					btnDelete.Visibility = Visibility.Visible;
				}
				else
				{
					btnDownloadForbidden.Visibility = Visibility.Visible;
				}
				break;
			case MeetingRoomButtonType.YO:
				btnRead2Forbidden.Visibility = Visibility.Visible;
				break;
			case MeetingRoomButtonType.OO:
				btnRead2Forbidden.Visibility = Visibility.Visible;
				break;
			}
			if (invisible)
			{
				base.Visibility = Visibility.Collapsed;
			}
		}

		private void ShowBtnPercent(int Percent)
		{
			btnDelete.Visibility = Visibility.Collapsed;
			btnDownload.Visibility = Visibility.Collapsed;
			btnPause.Visibility = Visibility.Collapsed;
			txtPercent.Text = FinishedPercent + " %";
			pb.Value = FinishedPercent;
			pb.Foreground = Brushes.Wheat;
			pb.Background = Brushes.Gray;
			txtPercent.Visibility = Visibility.Visible;
			pb.Visibility = Visibility.Visible;
			btnPausing.Visibility = Visibility.Visible;
		}

		private void ajaxLoader_ChangeImage(object sender, EventArgs e)
		{
			string uriString = string.Format("images/ajaxLoader/{0}.gif", i);
			ajaxLoader.Source = new BitmapImage(new Uri(uriString, UriKind.Relative));
			i++;
			if (ForceStopAjaxLoader || i >= 11)
			{
				ajaxTimer.Stop();
				ajaxLoader.Visibility = Visibility.Collapsed;
				i = 1;
				FileDownloader instance = Singleton_FileDownloader.GetInstance(userMeeting.ID);
				if (instance.downloaderType != DownloaderType.正在下載中)
				{
					ShowBtnPercent(FinishedPercent);
				}
			}
		}

		private int GetFinishedPercent()
		{
			int num = 0;
			try
			{
				DataTable dataTable = MSCE.GetDataTable("select count(ID) as FinishedFiles from FileRow where UserID =@1 and MeetingID=@2 and DownloadBytes!=0 and DownloadBytes >= TotalBytes;", UserID, userMeeting.ID);
				if (dataTable.Rows.Count > 0)
				{
					int.TryParse(dataTable.Rows[0]["FinishedFiles"].ToString(), out FinishedFiles);
				}
				if (FinishedFiles > 0 && FinishedFiles >= TotalFiles)
				{
					num = 100;
					return num;
				}
				if (TotalFiles > 0)
				{
					num = (int)((double)FinishedFiles / (double)TotalFiles * 100.0);
				}
				double result = 0.0;
				double result2 = 0.0;
				dataTable = MSCE.GetDataTable("select sum(DownloadBytes) as sumDownloadBytes from FileRow where UserID =@1 and MeetingID=@2 and DownloadBytes!=0 and DownloadBytes < TotalBytes;", UserID, userMeeting.ID);
				if (dataTable.Rows.Count > 0)
				{
					double.TryParse(dataTable.Rows[0]["SumDownloadBytes"].ToString(), out result);
				}
				dataTable = MSCE.GetDataTable("select sum(TotalBytes) as SumTotalBytes from FileRow where UserID =@1 and MeetingID=@2 and DownloadBytes!=0 and DownloadBytes < TotalBytes;", UserID, userMeeting.ID);
				if (dataTable.Rows.Count > 0)
				{
					double.TryParse(dataTable.Rows[0]["SumTotalBytes"].ToString(), out result2);
				}
				if (!(result2 > 0.0))
				{
					return num;
				}
				num += (int)(result / result2 / (double)(TotalFiles - FinishedFiles) * (100.0 / (double)TotalFiles));
				return num;
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
				return num;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/meetingroom.xaml", UriKind.Relative);
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
				grid = (Grid)target;
				break;
			case 2:
				RoomBG = (Border)target;
				break;
			case 3:
				RoomInnerBG = (Border)target;
				break;
			case 4:
				btnSeries = (Image)target;
				break;
			case 5:
				txtMeetingName = (TextBlock)target;
				break;
			case 6:
				txtLocation = (TextBlock)target;
				break;
			case 7:
				txtTime = (TextBlock)target;
				break;
			case 8:
				ajaxLoader = (Image)target;
				break;
			case 9:
				btnDownload = (Image)target;
				break;
			case 10:
				btnDownloadForbidden = (Image)target;
				break;
			case 11:
				btnRead2Forbidden = (Image)target;
				break;
			case 12:
				btnDelete = (Image)target;
				break;
			case 13:
				btnRefresh = (Image)target;
				break;
			case 14:
				txtPercent = (TextBlock)target;
				break;
			case 15:
				btnPause = (Image)target;
				break;
			case 16:
				btnPausing = (Image)target;
				break;
			case 17:
				pb = (ProgressBar)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}

		[CompilerGenerated]
		private void _003CMeetingRoom_Loaded_003Eb__0()
		{
			InitSelectDB();
			base.Dispatcher.BeginInvoke(new Action(_003CMeetingRoom_Loaded_003Eb__1));
			if (DateTool.IsSameDate(DateTime.Parse(userMeeting.BeginTime), DateTime.Now))
			{
				PreLoadMeeting();
			}
		}

		[CompilerGenerated]
		private void _003CMeetingRoom_Loaded_003Eb__1()
		{
			InitUI();
			InitEvent();
		}

		[CompilerGenerated]
		private void _003CPreLoadMeeting_003Eb__2()
		{
			GetMeetingData.AsyncPOST(userMeeting.ID, UserID, UserPWD, new Action<MeetingData>(_003CPreLoadMeeting_003Eb__3));
		}

		[CompilerGenerated]
		private void _003CPreLoadMeeting_003Eb__3(MeetingData md)
		{
			ThreadStart threadStart = null;
			try
			{
				PreLoadMeetingData = md;
				if (CacheMinuteTTL > 0)
				{
					if (CacheThread != null)
					{
						CacheThread.Abort();
					}
					if (threadStart == null)
					{
						threadStart = new ThreadStart(_003CPreLoadMeeting_003Eb__4);
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
		private void _003CPreLoadMeeting_003Eb__4()
		{
			Thread.Sleep(CacheMinuteTTL * 60 * 1000);
			PreLoadMeetingData = null;
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
		private static void _003CInitEvent_003Eb__9(object sender, MouseEventArgs e)
		{
			MouseTool.ShowHand();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__a(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__b(object sender, MouseEventArgs e)
		{
			MouseTool.ShowHand();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__c(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__d(object sender, MouseEventArgs e)
		{
			MouseTool.ShowHand();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__e(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
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
		private static void _003CInitEvent_003Eb__11(object sender, MouseEventArgs e)
		{
			MouseTool.ShowHand();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__12(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private void _003CbtnPause_MouseLeftButtonDown_003Eb__27()
		{
			FileDownloader instance = Singleton_FileDownloader.GetInstance(userMeeting.ID);
			instance.ClearMeetingRoomEvent();
			instance.Stop();
		}
	}
}
