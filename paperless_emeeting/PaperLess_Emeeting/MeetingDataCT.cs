using Newtonsoft.Json;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace PaperLess_Emeeting
{
	public class MeetingDataCT : UserControl, IComponentConnector
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass17
		{
			public double Height;

			public double Width;

			public GridLength g2;

			public MeetingDataCT _003C_003E4__this;

			public void _003CInitEvent_003Eb__c(object sender, SizeChangedEventArgs e)
			{
				Height = _003C_003E4__this.ActualHeight;
				Width = _003C_003E4__this.ActualWidth;
				if (Width >= 1920.0)
				{
					_003C_003E4__this.LeftDP.Margin = new Thickness(80.0, 120.0, 25.0, 40.0);
					g2 = new GridLength(1.38, GridUnitType.Star);
					_003C_003E4__this.C1.Width = g2;
					g2 = new GridLength(1.05, GridUnitType.Star);
					_003C_003E4__this.FileGrid.Margin = new Thickness(30.0, 50.0, 40.0, 45.0);
				}
				else
				{
					_003C_003E4__this.LeftDP.Margin = new Thickness(60.0, 80.0, 5.0, 40.0);
					g2 = new GridLength(1.38, GridUnitType.Star);
					_003C_003E4__this.C1.Width = g2;
					g2 = new GridLength(1.1, GridUnitType.Star);
					_003C_003E4__this.C2.Width = g2;
					_003C_003E4__this.FileGrid.Margin = new Thickness(20.0, 30.0, 30.0, 30.0);
				}
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass1e
		{
			public MenuButton mb;

			public MeetingDataCT _003C_003E4__this;

			public void _003CMeetingDataCT_Counting_Finished_FileCount_Callback_003Eb__1a()
			{
				Thread.Sleep(1000);
				_003C_003E4__this.Dispatcher.BeginInvoke(new Action(_003CMeetingDataCT_Counting_Finished_FileCount_Callback_003Eb__1b));
			}

			public void _003CMeetingDataCT_Counting_Finished_FileCount_Callback_003Eb__1b()
			{
				MouseButtonEventArgs mouseButtonEventArgs = new MouseButtonEventArgs(Mouse.PrimaryDevice, 100, MouseButton.Left);
				mouseButtonEventArgs.RoutedEvent = UIElement.MouseLeftButtonDownEvent;
				mb.RaiseEvent(mouseButtonEventArgs);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass30
		{
			public string childernIDsString;

			public bool _003CMeetingDataCT_GetBookVMs_ByMeetingFileCate_Callback_003Eb__23(File_DownloadItemViewModel x)
			{
				if (x.DownloadBytes != 0 && x.DownloadBytes >= x.TotalBytes)
				{
					return childernIDsString.Contains("," + x.AgendaID + ",");
				}
				return false;
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass34
		{
			public string fileItem_AgendaID;

			public bool _003CFindChildernIDsString_003Eb__32(AgendaRow x)
			{
				return x.meetingDataAgenda.ID.Equals(fileItem_AgendaID);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass38
		{
			public File_DownloadItemViewModel fileItem;

			public bool _003CMeetingDataCT_DownloadError_Callback_003Eb__36(FileRow x)
			{
				return x.meetingDataDownloadFileFile.ID.Equals(fileItem.ID);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass3b
		{
			public List<File_DownloadItemViewModel> list;

			public MeetingDataCT _003C_003E4__this;

			public void _003CbtnAllFileRowsUpdate_MouseLeftButtonDown_003Eb__3a()
			{
				FileDownloader instance = Singleton_FileDownloader.GetInstance(_003C_003E4__this.meetingData.ID);
				instance.ClearMeetingDataCTEvent();
				instance.Stop();
				AutoClosingMessageBox.Show("更新檢查中");
				instance.AddItem(list);
				instance.ClearMeetingDataCTEvent();
				instance.MeetingDataCT_DownloadFileStart_Event += _003C_003E4__this.Start_callback;
				instance.MeetingDataCT_DownloadProgressChanged_Event += _003C_003E4__this.Progress_callback;
				instance.MeetingDataCT_DownloadFileCompleted_Event += _003C_003E4__this.Finish_callback;
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass3f
		{
			public File_DownloadItemViewModel fileItem;

			public bool _003CMeetingDataCT_UnZipError_Callback_003Eb__3d(FileRow x)
			{
				return x.meetingDataDownloadFileFile.ID.Equals(fileItem.ID);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass43
		{
			public File_DownloadItemViewModel fileItem;

			public bool _003CMeetingDataCT_UnZip_Callback_003Eb__41(FileRow x)
			{
				return x.meetingDataDownloadFileFile.ID.Equals(fileItem.ID);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass47
		{
			public File_DownloadItemViewModel fileItem;

			public bool _003CMeetingDataCT_DownloadFileCompleted_Callback_003Eb__45(FileRow x)
			{
				return x.meetingDataDownloadFileFile.ID.Equals(fileItem.ID);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass4b
		{
			public File_DownloadItemViewModel fileItem;

			public bool _003CMeetingDataCT_DownloadProgressChanged_Callback_003Eb__49(FileRow x)
			{
				return x.meetingDataDownloadFileFile.ID.Equals(fileItem.ID);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass4f
		{
			public File_DownloadItemViewModel fileItem;

			public bool _003CMeetingDataCT_DownloadFileStart_Callback_003Eb__4d(FileRow x)
			{
				return x.meetingDataDownloadFileFile.ID.Equals(fileItem.ID);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass5e
		{
			private sealed class _003C_003Ec__DisplayClass65
			{
				public _003C_003Ec__DisplayClass5e CS_0024_003C_003E8__locals5f;

				public List<MeetingDataDownloadFileFile> FileList;

				public void _003CGetMeetingData_DoAction_003Eb__5b()
				{
					int num = 0;
					foreach (MeetingDataDownloadFileFile file in FileList)
					{
						num++;
						bool isLastRow = num == FileList.Count;
						int index = num;
						if (file.AgendaID.Equals("") || file.AgendaID.Equals("c") || file.AgendaID.Equals("i"))
						{
							CS_0024_003C_003E8__locals5f._003C_003E4__this.HasSubjectFile = true;
							CS_0024_003C_003E8__locals5f._003C_003E4__this.imgSubject.Visibility = Visibility.Visible;
						}
						CS_0024_003C_003E8__locals5f._003C_003E4__this.FileRowSP.Children.Add(new FileRow(CS_0024_003C_003E8__locals5f._003C_003E4__this.UserID, CS_0024_003C_003E8__locals5f._003C_003E4__this.UserName, CS_0024_003C_003E8__locals5f._003C_003E4__this.UserPWD, CS_0024_003C_003E8__locals5f._003C_003E4__this.meetingData.ID, CS_0024_003C_003E8__locals5f._003C_003E4__this.UserEmail, index, isLastRow, file, new MeetingDataCT_RaiseAllDownload_Function(CS_0024_003C_003E8__locals5f._003C_003E4__this.MeetingDataCT_RaiseAllDownload_Callback), new MeetingDataCT_HangTheDownloadEvent_Function(CS_0024_003C_003E8__locals5f._003C_003E4__this.MeetingDataCT_HangTheDownloadEvent_Callback), new MeetingDataCT_IsAllFileRowFinished_AddInitUIFinished_Function(CS_0024_003C_003E8__locals5f._003C_003E4__this.MeetingDataCT_IsAllFileRowFinished_AddInitUIFinished_Callback), new MeetingDataCT_GetBookVMs_ByMeetingFileCate_Function(CS_0024_003C_003E8__locals5f._003C_003E4__this.MeetingDataCT_GetBookVMs_ByMeetingFileCate_Callback), new MeetingDataCT_GetWatermark_Function(CS_0024_003C_003E8__locals5f._003C_003E4__this.MeetingDataCT_GetWatermark_Callback), CS_0024_003C_003E8__locals5f._003C_003E4__this.meetingRoomButtonType, new MeetingDataCT_Counting_Finished_FileCount_Function(CS_0024_003C_003E8__locals5f._003C_003E4__this.MeetingDataCT_Counting_Finished_FileCount_Callback)));
					}
				}
			}

			public MeetingDataCT _003C_003E4__this;

			public MeetingData md;

			public void _003CGetMeetingData_DoAction_003Eb__59()
			{
				Action action = null;
				_003C_003Ec__DisplayClass65 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass65();
				_003C_003Ec__DisplayClass.CS_0024_003C_003E8__locals5f = this;
				_003C_003Ec__DisplayClass.FileList = new List<MeetingDataDownloadFileFile>();
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
						_003C_003Ec__DisplayClass.FileList.Add(meetingDataDownloadFileFile);
						_003C_003E4__this.HasRecordFile = true;
					}
					if (_003C_003E4__this.HasRecordFile)
					{
						Dispatcher dispatcher = _003C_003E4__this.Dispatcher;
						if (action == null)
						{
							action = new Action(_003CGetMeetingData_DoAction_003Eb__5a);
						}
						dispatcher.BeginInvoke(action);
					}
				}
				catch (Exception)
				{
				}
				_003C_003Ec__DisplayClass.FileList.AddRange(Enumerable.ToList(_003C_003E4__this.meetingData.DownloadFile.DownloadFileList));
				_003C_003E4__this.All_FileCount = _003C_003Ec__DisplayClass.FileList.Count;
				_003C_003E4__this.Dispatcher.BeginInvoke(new Action(_003C_003Ec__DisplayClass._003CGetMeetingData_DoAction_003Eb__5b));
			}

			private void _003CGetMeetingData_DoAction_003Eb__5a()
			{
				_003C_003E4__this.btnRecord.Visibility = Visibility.Visible;
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass62
		{
			public List<string> Files_AgendaIDs;

			public List<string> ParentIDs;

			public MeetingDataCT _003C_003E4__this;

			public void _003CGetMeetingData_DoAction_003Eb__58()
			{
				MeetingDataAgenda[] agendaList = _003C_003E4__this.meetingData.AgendaList;
				foreach (MeetingDataAgenda meetingDataAgenda in agendaList)
				{
					bool isHasFile = Files_AgendaIDs.Contains(meetingDataAgenda.ID);
					bool isHasChildren = ParentIDs.Contains(meetingDataAgenda.ID);
					bool isParent = meetingDataAgenda.ParentID.Equals("0") || meetingDataAgenda.ParentID.Equals("c0") || meetingDataAgenda.ParentID.Equals("i0");
					_003C_003E4__this.AgendaRowSP.Children.Add(new AgendaRow(_003C_003E4__this.meetingData.ID, _003C_003E4__this.UserID, isHasFile, isHasChildren, isParent, meetingDataAgenda, new MeetingDataCT_ShowAgendaFile_Function(_003C_003E4__this.MeetingDataCT_ShowAgendaFile_Callback), new MeetingDataCT_GetAgendaInwWorkCount_Function(_003C_003E4__this.MeetingDataCT_GetAgendaInwWorkCount_Callback)));
				}
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass69
		{
			public MeetingDataCT _003C_003E4__this;

			public string LastFileItemID;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass6b
		{
			public _003C_003Ec__DisplayClass69 CS_0024_003C_003E8__locals6a;

			public List<File_DownloadItemViewModel> list;

			public void _003CMeetingDataCT_RaiseAllDownload_Callback_003Eb__67()
			{
				FileDownloader instance = Singleton_FileDownloader.GetInstance(CS_0024_003C_003E8__locals6a._003C_003E4__this.meetingData.ID);
				list = instance.GetNotInList(list);
				list.RemoveAll(new Predicate<File_DownloadItemViewModel>(_003CMeetingDataCT_RaiseAllDownload_Callback_003Eb__68));
				instance.AddItem(list);
			}

			public bool _003CMeetingDataCT_RaiseAllDownload_Callback_003Eb__68(File_DownloadItemViewModel x)
			{
				return x.ID.Equals(CS_0024_003C_003E8__locals6a.LastFileItemID);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass6e
		{
			public string AgendaID;

			public bool _003CMeetingDataCT_GetAgendaInwWorkCount_Callback_003Eb__6d(AgendaRow x)
			{
				if (x.meetingDataAgenda.Progress.Equals("U"))
				{
					return !x.meetingDataAgenda.ID.Equals(AgendaID);
				}
				return false;
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass76
		{
			public string AgendaID;

			public bool _003CGetChildernAgendaIDs_003Eb__70(MeetingDataAgenda x)
			{
				return x.ParentID.Equals(AgendaID);
			}
		}

		private MeetingDataCT_DownloadFileStart_Function Start_callback;

		private MeetingDataCT_DownloadProgressChanged_Function Progress_callback;

		private MeetingDataCT_DownloadFileCompleted_Function Finish_callback;

		private MeetingDataCT_UnZip_Function UnZip_callback;

		private MeetingDataCT_UnZipError_Function UnZipError_callback;

		private MeetingDataCT_DownloadError_Function DownloadError_callback;

		private MeetingDataCT_GetBookVMs_ByMeetingFileCate_Function GetBookVMs_ByMeetingFileCate_callback;

		private MeetingRoomButtonType meetingRoomButtonType;

		private MeetingDataCT_Counting_Finished_FileCount_Function MeetingDataCT_Counting_Finished_FileCount_callback;

		public int All_FileCount;

		public int Loaded_FileCount;

		public int Finished_FileCount;

		private bool isFirstAutoTurnOnSync;

		internal ColumnDefinition C1;

		internal ColumnDefinition C2;

		internal Image btnSeries;

		internal DockPanel LeftDP;

		internal TextBlock txtMeetingName;

		internal Grid btnRecord;

		internal Border mask;

		internal TextBlock txtRecord;

		internal Image imgSubject;

		internal TextBlock txtSubject;

		internal StackPanel AgendaRowSP;

		internal Grid FileGrid;

		internal Border mask2;

		internal TextBlock txtData;

		internal DockPanel btnAllFileRowsUpdate;

		internal StackPanel FileRowSP;

		private bool _contentLoaded;

		[CompilerGenerated]
		private string _003CUserID_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserName_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserPWD_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserEmail_003Ek__BackingField;

		[CompilerGenerated]
		private MeetingData _003CmeetingData_003Ek__BackingField;

		[CompilerGenerated]
		private bool _003CHasRecordFile_003Ek__BackingField;

		[CompilerGenerated]
		private bool _003CHasSubjectFile_003Ek__BackingField;

		[CompilerGenerated]
		private bool _003CAlready_RaiseAllDownload_003Ek__BackingField;

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
		private static Func<MenuButton, bool> CS_0024_003C_003E9__CachedAnonymousMethodDelegate1c;

		[CompilerGenerated]
		private static Func<FileRow, File_DownloadItemViewModel> CS_0024_003C_003E9__CachedAnonymousMethodDelegate28;

		[CompilerGenerated]
		private static Func<File_DownloadItemViewModel, bool> CS_0024_003C_003E9__CachedAnonymousMethodDelegate29;

		[CompilerGenerated]
		private static Func<FileRow, File_DownloadItemViewModel> CS_0024_003C_003E9__CachedAnonymousMethodDelegate2a;

		[CompilerGenerated]
		private static Func<FileRow, File_DownloadItemViewModel> CS_0024_003C_003E9__CachedAnonymousMethodDelegate2c;

		[CompilerGenerated]
		private static Func<File_DownloadItemViewModel, bool> CS_0024_003C_003E9__CachedAnonymousMethodDelegate2d;

		[CompilerGenerated]
		private static Func<FileRow, File_DownloadItemViewModel> CS_0024_003C_003E9__CachedAnonymousMethodDelegate2e;

		[CompilerGenerated]
		private static Func<File_DownloadItemViewModel, bool> CS_0024_003C_003E9__CachedAnonymousMethodDelegate2f;

		[CompilerGenerated]
		private static Func<MeetingDataDownloadFileFile, string> CS_0024_003C_003E9__CachedAnonymousMethodDelegate60;

		[CompilerGenerated]
		private static Func<MeetingDataAgenda, string> CS_0024_003C_003E9__CachedAnonymousMethodDelegate61;

		[CompilerGenerated]
		private static Func<MeetingDataAgenda, string> CS_0024_003C_003E9__CachedAnonymousMethodDelegate74;

		[CompilerGenerated]
		private static Func<MeetingDataAgenda, string> CS_0024_003C_003E9__CachedAnonymousMethodDelegate75;

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

		private MeetingData meetingData
		{
			[CompilerGenerated]
			get
			{
				return _003CmeetingData_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CmeetingData_003Ek__BackingField = value;
			}
		}

		public bool HasRecordFile
		{
			[CompilerGenerated]
			get
			{
				return _003CHasRecordFile_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CHasRecordFile_003Ek__BackingField = value;
			}
		}

		public bool HasSubjectFile
		{
			[CompilerGenerated]
			get
			{
				return _003CHasSubjectFile_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CHasSubjectFile_003Ek__BackingField = value;
			}
		}

		public bool Already_RaiseAllDownload
		{
			[CompilerGenerated]
			get
			{
				return _003CAlready_RaiseAllDownload_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CAlready_RaiseAllDownload_003Ek__BackingField = value;
			}
		}

		public event Home_ChangeBtnSP_Function Home_ChangeBtnSP_Event;

		public MeetingDataCT(string UserID, string UserName, string UserPWD, string UserEmail, MeetingData meetingData, Home_ChangeBtnSP_Function callback, bool isFirstAutoTurnOnSync)
		{
			MouseTool.ShowLoading();
			InitializeComponent();
			this.UserID = UserID;
			this.UserName = UserName;
			this.UserPWD = UserPWD;
			this.UserEmail = UserEmail;
			this.meetingData = meetingData;
			Home_ChangeBtnSP_Event += callback;
			base.Loaded += new RoutedEventHandler(MeetingDataCT_Loaded);
			base.Unloaded += new RoutedEventHandler(MeetingDataCT_Unloaded);
			base.IsVisibleChanged += new DependencyPropertyChangedEventHandler(MeetingDataCT_IsVisibleChanged);
			this.isFirstAutoTurnOnSync = isFirstAutoTurnOnSync;
		}

		private void MeetingDataCT_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (!(bool)e.NewValue)
			{
				Singleton_FileDownloader.GetInstance(meetingData.ID).ClearMeetingDataCTEvent();
			}
		}

		private void MeetingDataCT_Unloaded(object sender, RoutedEventArgs e)
		{
			Singleton_FileDownloader.GetInstance(meetingData.ID).ClearMeetingDataCTEvent();
		}

		private void MeetingDataCT_Loaded(object sender, RoutedEventArgs e)
		{
			Task.Factory.StartNew(new Action(_003CMeetingDataCT_Loaded_003Eb__0));
		}

		private void InitSelectDB()
		{
			DateTime result = new DateTime(2010, 1, 1);
			DateTime result2 = new DateTime(2050, 1, 1);
			DateTime.TryParse(meetingData.BeginTime, out result);
			DateTime.TryParse(meetingData.EndTime, out result2);
			if (result <= new DateTime(2010, 1, 1))
			{
				result = new DateTime(2010, 1, 1);
			}
			if (result2 <= new DateTime(2050, 1, 1))
			{
				result2 = new DateTime(2050, 1, 1);
			}
			string text = "update nowlogin set MeetingBeginTime=@1,MeetingEndTime=@2,MeetingUserType=@3";
			int num = MSCE.ExecuteNonQuery(text, result.ToString("yyyy/MM/dd HH:mm:ss"), result2.ToString("yyyy/MM/dd HH:mm:ss"), meetingData.LoginResult.LoginState.Type);
			if (num < 1)
			{
				LogTool.Debug(new Exception("DB失敗: " + text));
			}
		}

		private void InitEvent()
		{
			_003C_003Ec__DisplayClass17 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass17();
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			FileDownloader instance = Singleton_FileDownloader.GetInstance(meetingData.ID);
			Start_callback = new MeetingDataCT_DownloadFileStart_Function(MeetingDataCT_DownloadFileStart_Callback);
			Progress_callback = new MeetingDataCT_DownloadProgressChanged_Function(MeetingDataCT_DownloadProgressChanged_Callback);
			Finish_callback = new MeetingDataCT_DownloadFileCompleted_Function(MeetingDataCT_DownloadFileCompleted_Callback);
			UnZip_callback = new MeetingDataCT_UnZip_Function(MeetingDataCT_UnZip_Callback);
			UnZipError_callback = new MeetingDataCT_UnZipError_Function(MeetingDataCT_UnZipError_Callback);
			DownloadError_callback = new MeetingDataCT_DownloadError_Function(MeetingDataCT_DownloadError_Callback);
			GetBookVMs_ByMeetingFileCate_callback = new MeetingDataCT_GetBookVMs_ByMeetingFileCate_Function(MeetingDataCT_GetBookVMs_ByMeetingFileCate_Callback);
			MeetingDataCT_Counting_Finished_FileCount_callback = new MeetingDataCT_Counting_Finished_FileCount_Function(MeetingDataCT_Counting_Finished_FileCount_Callback);
			instance.MeetingDataCT_DownloadFileStart_Event += Start_callback;
			instance.MeetingDataCT_DownloadProgressChanged_Event += Progress_callback;
			instance.MeetingDataCT_DownloadFileCompleted_Event += Finish_callback;
			instance.MeetingDataCT_UnZip_Event += UnZip_callback;
			instance.MeetingDataCT_UnZipError_Event += UnZipError_callback;
			instance.MeetingDataCT_DownloadError_Event += DownloadError_callback;
			TextBlock textBlock = txtMeetingName;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegated == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegated = new MouseEventHandler(_003CInitEvent_003Eb__2);
			}
			textBlock.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegated;
			TextBlock textBlock2 = txtMeetingName;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegatee == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegatee = new MouseEventHandler(_003CInitEvent_003Eb__3);
			}
			textBlock2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegatee;
			txtMeetingName.MouseLeftButtonDown += new MouseButtonEventHandler(txtMeetingName_MouseLeftButtonDown);
			Grid grid = btnRecord;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegatef == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegatef = new MouseEventHandler(_003CInitEvent_003Eb__4);
			}
			grid.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegatef;
			Grid grid2 = btnRecord;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate10 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate10 = new MouseEventHandler(_003CInitEvent_003Eb__5);
			}
			grid2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate10;
			btnRecord.MouseLeftButtonDown += new MouseButtonEventHandler(btnRecord_MouseLeftButtonDown);
			TextBlock textBlock3 = txtSubject;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate11 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate11 = new MouseEventHandler(_003CInitEvent_003Eb__6);
			}
			textBlock3.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate11;
			TextBlock textBlock4 = txtSubject;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate12 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate12 = new MouseEventHandler(_003CInitEvent_003Eb__7);
			}
			textBlock4.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate12;
			txtSubject.MouseLeftButtonDown += new MouseButtonEventHandler(txtSubject_MouseLeftButtonDown);
			DockPanel dockPanel = btnAllFileRowsUpdate;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate13 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate13 = new MouseEventHandler(_003CInitEvent_003Eb__8);
			}
			dockPanel.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate13;
			DockPanel dockPanel2 = btnAllFileRowsUpdate;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate14 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate14 = new MouseEventHandler(_003CInitEvent_003Eb__9);
			}
			dockPanel2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate14;
			btnAllFileRowsUpdate.MouseLeftButtonDown += new MouseButtonEventHandler(btnAllFileRowsUpdate_MouseLeftButtonDown);
			Image image = btnSeries;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate15 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate15 = new MouseEventHandler(_003CInitEvent_003Eb__a);
			}
			image.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate15;
			Image image2 = btnSeries;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate16 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate16 = new MouseEventHandler(_003CInitEvent_003Eb__b);
			}
			image2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate16;
			btnSeries.MouseLeftButtonDown += new MouseButtonEventHandler(btnSeries_MouseLeftButtonDown);
			_003C_003Ec__DisplayClass.Height = base.ActualHeight;
			_003C_003Ec__DisplayClass.Width = base.ActualWidth;
			if (_003C_003Ec__DisplayClass.Width >= 1920.0)
			{
				LeftDP.Margin = new Thickness(80.0, 120.0, 25.0, 40.0);
				_003C_003Ec__DisplayClass.g2 = new GridLength(1.38, GridUnitType.Star);
				C1.Width = _003C_003Ec__DisplayClass.g2;
				_003C_003Ec__DisplayClass.g2 = new GridLength(1.05, GridUnitType.Star);
				C2.Width = _003C_003Ec__DisplayClass.g2;
				FileGrid.Margin = new Thickness(10.0, 50.0, 40.0, 45.0);
			}
			else
			{
				LeftDP.Margin = new Thickness(60.0, 80.0, 5.0, 40.0);
				_003C_003Ec__DisplayClass.g2 = new GridLength(1.38, GridUnitType.Star);
				C1.Width = _003C_003Ec__DisplayClass.g2;
				_003C_003Ec__DisplayClass.g2 = new GridLength(1.1, GridUnitType.Star);
				C2.Width = _003C_003Ec__DisplayClass.g2;
				FileGrid.Margin = new Thickness(20.0, 30.0, 30.0, 30.0);
			}
			base.SizeChanged += new SizeChangedEventHandler(_003C_003Ec__DisplayClass._003CInitEvent_003Eb__c);
		}

		private void MeetingDataCT_Counting_Finished_FileCount_Callback()
		{
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action(MeetingDataCT_Counting_Finished_FileCount_Callback));
			}
			else
			{
				if (!Settings.Default.HasAutoTodaySync)
				{
					return;
				}
				Finished_FileCount++;
				if (Finished_FileCount != All_FileCount || !isFirstAutoTurnOnSync)
				{
					return;
				}
				Home home = Enumerable.First(Enumerable.OfType<Home>(Application.Current.Windows));
				if (home == null || home.IsInSync)
				{
					return;
				}
				IEnumerable<MenuButton> source = Enumerable.OfType<MenuButton>(home.btnSP.Children);
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate1c == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate1c = new Func<MenuButton, bool>(_003CMeetingDataCT_Counting_Finished_FileCount_Callback_003Eb__19);
				}
				IEnumerable<MenuButton> enumerable = Enumerable.Where(source, CS_0024_003C_003E9__CachedAnonymousMethodDelegate1c);
				if (enumerable == null)
				{
					return;
				}
				Action action = null;
				_003C_003Ec__DisplayClass1e _003C_003Ec__DisplayClass1e = new _003C_003Ec__DisplayClass1e();
				_003C_003Ec__DisplayClass1e._003C_003E4__this = this;
				_003C_003Ec__DisplayClass1e.mb = null;
				try
				{
					_003C_003Ec__DisplayClass1e.mb = Enumerable.First(enumerable);
				}
				catch (Exception ex)
				{
					LogTool.Debug(ex);
				}
				if (_003C_003Ec__DisplayClass1e.mb != null && _003C_003Ec__DisplayClass1e.mb.btnImg.Source.ToString().Contains("status-onair-off@2x.png"))
				{
					TaskFactory factory = Task.Factory;
					if (action == null)
					{
						action = new Action(_003C_003Ec__DisplayClass1e._003CMeetingDataCT_Counting_Finished_FileCount_Callback_003Eb__1a);
					}
					factory.StartNew(action);
				}
			}
		}

		private void txtSubject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			SetColorBlackOn_TxtName_TxtRecord_TxtSubjectOnGray_AgendaRow();
			MeetingDataCT_ShowAgendaFile_Callback("", "", true);
			txtSubject.Foreground = ColorTool.HexColorToBrush("#0093b0");
		}

		private Dictionary<string, BookVM> MeetingDataCT_GetBookVMs_ByMeetingFileCate_Callback(File_DownloadItemViewModel fileItem)
		{
			Func<File_DownloadItemViewModel, bool> func = null;
			_003C_003Ec__DisplayClass30 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass30();
			Dictionary<string, BookVM> dictionary = new Dictionary<string, BookVM>();
			List<File_DownloadItemViewModel> list = new List<File_DownloadItemViewModel>();
			_003C_003Ec__DisplayClass.childernIDsString = "";
			List<File_DownloadItemViewModel> list2 = new List<File_DownloadItemViewModel>();
			if (!Settings.Default.GetBookVMs_ByMeetingFileCate_ByAgenda)
			{
				IEnumerable<FileRow> source = Enumerable.OfType<FileRow>(FileRowSP.Children);
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate28 == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate28 = new Func<FileRow, File_DownloadItemViewModel>(_003CMeetingDataCT_GetBookVMs_ByMeetingFileCate_Callback_003Eb__20);
				}
				IEnumerable<File_DownloadItemViewModel> source2 = Enumerable.Select(source, CS_0024_003C_003E9__CachedAnonymousMethodDelegate28);
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate29 == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate29 = new Func<File_DownloadItemViewModel, bool>(_003CMeetingDataCT_GetBookVMs_ByMeetingFileCate_Callback_003Eb__21);
				}
				list = Enumerable.ToList(Enumerable.Where(source2, CS_0024_003C_003E9__CachedAnonymousMethodDelegate29));
				if (list == null)
				{
					list = new List<File_DownloadItemViewModel>();
				}
			}
			else
			{
				if (fileItem.AgendaID != null && !fileItem.AgendaID.Equals("record") && !fileItem.AgendaID.Equals("") && !fileItem.AgendaID.Equals("c") && !fileItem.AgendaID.Equals("i"))
				{
					_003C_003Ec__DisplayClass.childernIDsString = FindChildernIDsString(fileItem.AgendaID);
					IEnumerable<FileRow> source3 = Enumerable.OfType<FileRow>(FileRowSP.Children);
					if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate2a == null)
					{
						CS_0024_003C_003E9__CachedAnonymousMethodDelegate2a = new Func<FileRow, File_DownloadItemViewModel>(_003CMeetingDataCT_GetBookVMs_ByMeetingFileCate_Callback_003Eb__22);
					}
					IEnumerable<File_DownloadItemViewModel> source4 = Enumerable.Select(source3, CS_0024_003C_003E9__CachedAnonymousMethodDelegate2a);
					if (func == null)
					{
						func = new Func<File_DownloadItemViewModel, bool>(_003C_003Ec__DisplayClass._003CMeetingDataCT_GetBookVMs_ByMeetingFileCate_Callback_003Eb__23);
					}
					list2 = Enumerable.ToList(Enumerable.Where(source4, func));
				}
				else if (fileItem.AgendaID.Equals("record"))
				{
					IEnumerable<FileRow> source5 = Enumerable.OfType<FileRow>(FileRowSP.Children);
					if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate2c == null)
					{
						CS_0024_003C_003E9__CachedAnonymousMethodDelegate2c = new Func<FileRow, File_DownloadItemViewModel>(_003CMeetingDataCT_GetBookVMs_ByMeetingFileCate_Callback_003Eb__24);
					}
					IEnumerable<File_DownloadItemViewModel> source6 = Enumerable.Select(source5, CS_0024_003C_003E9__CachedAnonymousMethodDelegate2c);
					if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate2d == null)
					{
						CS_0024_003C_003E9__CachedAnonymousMethodDelegate2d = new Func<File_DownloadItemViewModel, bool>(_003CMeetingDataCT_GetBookVMs_ByMeetingFileCate_Callback_003Eb__25);
					}
					list2 = Enumerable.ToList(Enumerable.Where(source6, CS_0024_003C_003E9__CachedAnonymousMethodDelegate2d));
				}
				else
				{
					IEnumerable<FileRow> source7 = Enumerable.OfType<FileRow>(FileRowSP.Children);
					if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate2e == null)
					{
						CS_0024_003C_003E9__CachedAnonymousMethodDelegate2e = new Func<FileRow, File_DownloadItemViewModel>(_003CMeetingDataCT_GetBookVMs_ByMeetingFileCate_Callback_003Eb__26);
					}
					IEnumerable<File_DownloadItemViewModel> source8 = Enumerable.Select(source7, CS_0024_003C_003E9__CachedAnonymousMethodDelegate2e);
					if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate2f == null)
					{
						CS_0024_003C_003E9__CachedAnonymousMethodDelegate2f = new Func<File_DownloadItemViewModel, bool>(_003CMeetingDataCT_GetBookVMs_ByMeetingFileCate_Callback_003Eb__27);
					}
					list2 = Enumerable.ToList(Enumerable.Where(source8, CS_0024_003C_003E9__CachedAnonymousMethodDelegate2f));
				}
				list.AddRange(list2);
			}
			string filePath = ClickOnceTool.GetFilePath();
			string file_UnZipFileFolder = Settings.Default.File_UnZipFileFolder;
			string text = filePath + "\\" + file_UnZipFileFolder + "\\" + UserID + "\\" + meetingData.ID;
			foreach (File_DownloadItemViewModel item in list)
			{
				DataTable dataTable = MSCE.GetDataTable("SELECT FinishedFileVersion FROM FileRow where ID=@1 and UserID=@2 and MeetingID=@3", fileItem.ID, UserID, meetingData.ID);
				string text3 = text + "\\" + item.ID;
				string text2 = "1";
				if (dataTable.Rows.Count > 0)
				{
					text2 = dataTable.Rows[0]["FinishedFileVersion"].ToString();
				}
				dictionary[item.FileName] = new BookVM(item.ID, text + "\\" + item.ID + "\\" + text2, item.FileCate);
			}
			return dictionary;
		}

		private string FindChildernIDsString(string fileItem_AgendaID)
		{
			Func<AgendaRow, bool> func = null;
			_003C_003Ec__DisplayClass34 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass34();
			_003C_003Ec__DisplayClass.fileItem_AgendaID = fileItem_AgendaID;
			string result = "";
			try
			{
				IEnumerable<AgendaRow> source = Enumerable.OfType<AgendaRow>(AgendaRowSP.Children);
				if (func == null)
				{
					func = new Func<AgendaRow, bool>(_003C_003Ec__DisplayClass._003CFindChildernIDsString_003Eb__32);
				}
				AgendaRow agendaRow = Enumerable.FirstOrDefault(Enumerable.Where(source, func));
				if (agendaRow == null)
				{
					return result;
				}
				if (!agendaRow.IsParent)
				{
					result = FindChildernIDsString(agendaRow.meetingDataAgenda.ParentID);
					return result;
				}
				result = GetChildernAgendaIDs(_003C_003Ec__DisplayClass.fileItem_AgendaID, AgendaFilter.顯示父議題和子議題附件);
				return result;
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
				return result;
			}
		}

		private void btnSeries_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
			Home home = Enumerable.FirstOrDefault(Enumerable.OfType<Home>(Application.Current.Windows));
			home.ShowBtnSeriesCT(meetingData.SeriesMeetingID);
		}

		private void MeetingDataCT_DownloadError_Callback(File_DownloadItemViewModel fileItem)
		{
			Func<FileRow, bool> func = null;
			_003C_003Ec__DisplayClass38 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass38();
			_003C_003Ec__DisplayClass.fileItem = fileItem;
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<File_DownloadItemViewModel>(MeetingDataCT_UnZipError_Callback), _003C_003Ec__DisplayClass.fileItem);
				return;
			}
			IEnumerable<FileRow> source = Enumerable.OfType<FileRow>(FileRowSP.Children);
			if (func == null)
			{
				func = new Func<FileRow, bool>(_003C_003Ec__DisplayClass._003CMeetingDataCT_DownloadError_Callback_003Eb__36);
			}
			FileRow fileRow = Enumerable.FirstOrDefault(Enumerable.Where(source, func));
			if (fileRow != null)
			{
				fileRow.txtPercent.Visibility = Visibility.Collapsed;
				fileRow.pb.Visibility = Visibility.Collapsed;
				fileRow.btnPause.Visibility = Visibility.Collapsed;
				fileRow.btnPausing.Visibility = Visibility.Collapsed;
				fileRow.btnDownload.Visibility = Visibility.Visible;
			}
		}

		private void btnRecord_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			SetColorBlackOn_TxtName_TxtRecord_TxtSubjectOnGray_AgendaRow();
			MeetingDataCT_ShowAgendaFile_Callback("record", "record", true);
			txtRecord.Foreground = ColorTool.HexColorToBrush("#0093b0");
		}

		private void btnAllFileRowsUpdate_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			_003C_003Ec__DisplayClass3b _003C_003Ec__DisplayClass3b = new _003C_003Ec__DisplayClass3b();
			_003C_003Ec__DisplayClass3b._003C_003E4__this = this;
			_003C_003Ec__DisplayClass3b.list = new List<File_DownloadItemViewModel>();
			foreach (FileRow item in Enumerable.OfType<FileRow>(FileRowSP.Children))
			{
				if (item.fileItem.DownloadBytes == 0 || item.fileItem.DownloadBytes < item.fileItem.TotalBytes)
				{
					item.btnOpen.Visibility = Visibility.Collapsed;
					item.btnDownload.Visibility = Visibility.Collapsed;
					item.btnPausing.Visibility = Visibility.Collapsed;
					item.txtPercent.Text = "等待中";
					item.txtPercent.Foreground = Brushes.Gray;
					item.txtPercent.Visibility = Visibility.Visible;
					item.pb.Foreground = Brushes.Wheat;
					item.pb.Background = Brushes.Gray;
					item.pb.Value = item.fileItem.NowPercentage;
					item.pb.Visibility = Visibility.Visible;
					item.btnPause.Visibility = Visibility.Visible;
					_003C_003Ec__DisplayClass3b.list.Add(item.fileItem);
				}
			}
			Task.Factory.StartNew(new Action(_003C_003Ec__DisplayClass3b._003CbtnAllFileRowsUpdate_MouseLeftButtonDown_003Eb__3a));
		}

		private void MeetingDataCT_UnZipError_Callback(File_DownloadItemViewModel fileItem)
		{
			Func<FileRow, bool> func = null;
			_003C_003Ec__DisplayClass3f _003C_003Ec__DisplayClass3f = new _003C_003Ec__DisplayClass3f();
			_003C_003Ec__DisplayClass3f.fileItem = fileItem;
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<File_DownloadItemViewModel>(MeetingDataCT_UnZipError_Callback), _003C_003Ec__DisplayClass3f.fileItem);
				return;
			}
			IEnumerable<FileRow> source = Enumerable.OfType<FileRow>(FileRowSP.Children);
			if (func == null)
			{
				func = new Func<FileRow, bool>(_003C_003Ec__DisplayClass3f._003CMeetingDataCT_UnZipError_Callback_003Eb__3d);
			}
			FileRow fileRow = Enumerable.FirstOrDefault(Enumerable.Where(source, func));
			if (fileRow != null)
			{
				Storyboard storyboard = (Storyboard)fileRow.TryFindResource("sb");
				if (storyboard != null)
				{
					storyboard.Stop();
				}
				fileRow.txtPercent.Visibility = Visibility.Collapsed;
				fileRow.pb.Visibility = Visibility.Collapsed;
				fileRow.btnPause.Visibility = Visibility.Collapsed;
				fileRow.btnPausing.Visibility = Visibility.Collapsed;
				fileRow.btnDownload.Visibility = Visibility.Visible;
			}
		}

		private void MeetingDataCT_UnZip_Callback(File_DownloadItemViewModel fileItem)
		{
			Func<FileRow, bool> func = null;
			_003C_003Ec__DisplayClass43 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass43();
			_003C_003Ec__DisplayClass.fileItem = fileItem;
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<File_DownloadItemViewModel>(MeetingDataCT_UnZip_Callback), _003C_003Ec__DisplayClass.fileItem);
				return;
			}
			IEnumerable<FileRow> source = Enumerable.OfType<FileRow>(FileRowSP.Children);
			if (func == null)
			{
				func = new Func<FileRow, bool>(_003C_003Ec__DisplayClass._003CMeetingDataCT_UnZip_Callback_003Eb__41);
			}
			FileRow fileRow = Enumerable.FirstOrDefault(Enumerable.Where(source, func));
			if (fileRow != null)
			{
				Storyboard storyboard = (Storyboard)fileRow.TryFindResource("sb");
				if (storyboard != null)
				{
					fileRow.txtPercent.Visibility = Visibility.Collapsed;
					fileRow.pb.Visibility = Visibility.Collapsed;
					fileRow.btnPause.Visibility = Visibility.Collapsed;
					fileRow.btnPausing.Visibility = Visibility.Collapsed;
					fileRow.btnDownload.Visibility = Visibility.Collapsed;
					storyboard.Begin();
				}
			}
		}

		private void MeetingDataCT_DownloadFileCompleted_Callback(File_DownloadItemViewModel fileItem)
		{
			Func<FileRow, bool> func = null;
			_003C_003Ec__DisplayClass47 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass47();
			_003C_003Ec__DisplayClass.fileItem = fileItem;
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<File_DownloadItemViewModel>(MeetingDataCT_DownloadFileCompleted_Callback), _003C_003Ec__DisplayClass.fileItem);
				return;
			}
			IEnumerable<FileRow> source = Enumerable.OfType<FileRow>(FileRowSP.Children);
			if (func == null)
			{
				func = new Func<FileRow, bool>(_003C_003Ec__DisplayClass._003CMeetingDataCT_DownloadFileCompleted_Callback_003Eb__45);
			}
			FileRow fileRow = Enumerable.FirstOrDefault(Enumerable.Where(source, func));
			if (fileRow != null)
			{
				Storyboard storyboard = (Storyboard)fileRow.TryFindResource("sb");
				if (storyboard != null)
				{
					storyboard.Stop();
				}
				fileRow.txtPercent.Visibility = Visibility.Collapsed;
				fileRow.pb.Visibility = Visibility.Collapsed;
				fileRow.btnPause.Visibility = Visibility.Collapsed;
				fileRow.btnPausing.Visibility = Visibility.Collapsed;
				fileRow.btnDownload.Visibility = Visibility.Collapsed;
				fileRow.btnUpdate.Visibility = Visibility.Collapsed;
				switch (_003C_003Ec__DisplayClass.fileItem.FileCate)
				{
				case MeetingFileCate.電子書:
				case MeetingFileCate.Html5投影片:
					fileRow.btnOpen.Visibility = Visibility.Visible;
					break;
				case MeetingFileCate.影片檔:
					fileRow.btnOpen.Visibility = Visibility.Visible;
					break;
				}
				if (Settings.Default.HasSyncCenterModule)
				{
					fileRow.InitSyncCenter(Path.Combine(ClickOnceTool.GetDataPath(), Settings.Default.bookInfo_Path), fileRow.fileItem.ID, fileRow.UserID, fileRow.MeetingID);
				}
				MeetingDataCT_Counting_Finished_FileCount_Callback();
			}
		}

		private void MeetingDataCT_DownloadProgressChanged_Callback(File_DownloadItemViewModel fileItem)
		{
			Func<FileRow, bool> func = null;
			_003C_003Ec__DisplayClass4b _003C_003Ec__DisplayClass4b = new _003C_003Ec__DisplayClass4b();
			_003C_003Ec__DisplayClass4b.fileItem = fileItem;
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<File_DownloadItemViewModel>(MeetingDataCT_DownloadProgressChanged_Callback), _003C_003Ec__DisplayClass4b.fileItem);
				return;
			}
			IEnumerable<FileRow> source = Enumerable.OfType<FileRow>(FileRowSP.Children);
			if (func == null)
			{
				func = new Func<FileRow, bool>(_003C_003Ec__DisplayClass4b._003CMeetingDataCT_DownloadProgressChanged_Callback_003Eb__49);
			}
			FileRow fileRow = Enumerable.FirstOrDefault(Enumerable.Where(source, func));
			if (fileRow != null)
			{
				fileRow.btnDownload.Visibility = Visibility.Collapsed;
				fileRow.btnPausing.Visibility = Visibility.Collapsed;
				fileRow.txtPercent.Text = (int)_003C_003Ec__DisplayClass4b.fileItem.NowPercentage + " %";
				fileRow.txtPercent.Foreground = Brushes.Black;
				fileRow.txtPercent.Visibility = Visibility.Visible;
				fileRow.pb.Value = _003C_003Ec__DisplayClass4b.fileItem.NowPercentage;
				fileRow.pb.Foreground = Brushes.Orange;
				fileRow.pb.Background = Brushes.Black;
				fileRow.pb.Visibility = Visibility.Visible;
				fileRow.btnPause.Visibility = Visibility.Visible;
			}
		}

		private void MeetingDataCT_DownloadFileStart_Callback(File_DownloadItemViewModel fileItem)
		{
			Func<FileRow, bool> func = null;
			_003C_003Ec__DisplayClass4f _003C_003Ec__DisplayClass4f = new _003C_003Ec__DisplayClass4f();
			_003C_003Ec__DisplayClass4f.fileItem = fileItem;
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<File_DownloadItemViewModel>(MeetingDataCT_DownloadFileStart_Callback), _003C_003Ec__DisplayClass4f.fileItem);
				return;
			}
			IEnumerable<FileRow> source = Enumerable.OfType<FileRow>(FileRowSP.Children);
			if (func == null)
			{
				func = new Func<FileRow, bool>(_003C_003Ec__DisplayClass4f._003CMeetingDataCT_DownloadFileStart_Callback_003Eb__4d);
			}
			FileRow fileRow = Enumerable.FirstOrDefault(Enumerable.Where(source, func));
			if (fileRow != null)
			{
				fileRow.btnDownload.Visibility = Visibility.Collapsed;
				fileRow.btnOpen.Visibility = Visibility.Collapsed;
				fileRow.btnPausing.Visibility = Visibility.Collapsed;
				FileDownloader instance = Singleton_FileDownloader.GetInstance(meetingData.ID);
				File_DownloadItemViewModel inList = instance.GetInList(meetingData.ID);
				if (inList != null)
				{
					fileRow.txtPercent.Text = ((int)inList.NowPercentage).ToString();
				}
				fileRow.txtPercent.Foreground = Brushes.Black;
				fileRow.pb.Foreground = Brushes.Orange;
				fileRow.pb.Background = Brushes.Black;
				fileRow.txtPercent.Visibility = Visibility.Visible;
				fileRow.pb.Visibility = Visibility.Visible;
				fileRow.btnPause.Visibility = Visibility.Visible;
			}
		}

		private void txtMeetingName_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			SetColorBlackOn_TxtName_TxtRecord_TxtSubjectOnGray_AgendaRow();
			MeetingDataCT_ShowAgendaFile_Callback("All", "", true);
			txtMeetingName.Foreground = ColorTool.HexColorToBrush("#0093b0");
		}

		private void InitUI()
		{
			Action<MeetingData> action = null;
			Action<MeetingData> action2 = null;
			string courseOrMeeting_String = Settings.Default.CourseOrMeeting_String;
			txtRecord.Text = string.Format("{0}記錄", courseOrMeeting_String);
			txtData.Text = string.Format("{0}資料", courseOrMeeting_String);
			txtRecord.Visibility = Visibility.Visible;
			txtData.Visibility = Visibility.Visible;
			if (meetingData == null)
			{
				MouseTool.ShowLoading();
				string iD = meetingData.ID;
				string userID = UserID;
				string userPWD = UserPWD;
				if (action == null)
				{
					action = new Action<MeetingData>(_003CInitUI_003Eb__51);
				}
				GetMeetingData.AsyncPOST(iD, userID, userPWD, action);
				if (NetworkTool.CheckNetwork() > 0)
				{
					string iD2 = meetingData.ID;
					string userID2 = UserID;
					string userPWD2 = UserPWD;
					if (action2 == null)
					{
						action2 = new Action<MeetingData>(_003CInitUI_003Eb__52);
					}
					GetMeetingData.AsyncPOST(iD2, userID2, userPWD2, action2);
				}
				else
				{
					DataTable dataTable = MSCE.GetDataTable("select MeetingJson from MeetingData where MeetingID=@1 and UserID =@2", meetingData.ID, UserID);
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
			else
			{
				GetMeetingData_DoAction(meetingData);
			}
		}

		private void GetMeetingData_DoAction(MeetingData md)
		{
			Action action = null;
			Action action2 = null;
			_003C_003Ec__DisplayClass5e _003C_003Ec__DisplayClass5e = new _003C_003Ec__DisplayClass5e();
			_003C_003Ec__DisplayClass5e.md = md;
			_003C_003Ec__DisplayClass5e._003C_003E4__this = this;
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<MeetingData>(GetMeetingData_DoAction), _003C_003Ec__DisplayClass5e.md);
				return;
			}
			meetingData = _003C_003Ec__DisplayClass5e.md;
			if (meetingData != null)
			{
				string text = "update NowLogin set AllowIpRange=@1 ";
				int num = MSCE.ExecuteNonQuery(text, meetingData.IP);
				if (num < 1)
				{
					LogTool.Debug(new Exception("DB失敗: " + text));
					return;
				}
				List<UserButton> list = new List<UserButton>();
				MeetingDataLoginResultButton[] enableButtonList = meetingData.LoginResult.EnableButtonList;
				foreach (MeetingDataLoginResultButton meetingDataLoginResultButton in enableButtonList)
				{
					UserButton userButton = new UserButton();
					userButton.ID = meetingDataLoginResultButton.ID;
					userButton.Name = meetingDataLoginResultButton.Name;
					list.Add(userButton);
				}
				UserButton[] userButtonAry = list.ToArray();
				this.Home_ChangeBtnSP_Event(userButtonAry, "BtnMeeting");
				txtMeetingName.Text = meetingData.Name;
				if (meetingData.SeriesMeetingID != null && !meetingData.SeriesMeetingID.Equals(""))
				{
					btnSeries.Visibility = Visibility.Visible;
				}
				if (meetingData.Subject.Trim().Equals(""))
				{
					txtSubject.Visibility = Visibility.Collapsed;
				}
				else
				{
					txtSubject.Text = meetingData.Subject;
				}
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003CGetMeetingData_DoAction_003Eb__55);
				}
				factory.StartNew(action);
				string str = "N";
				string str2 = "N";
				DateTime result = DateTime.MinValue;
				DateTime result2 = DateTime.MaxValue;
				DateTime result3 = DateTime.MinValue;
				DateTime result4 = DateTime.MaxValue;
				if (!meetingData.DownloadFile.DownloadTime.BeginTime.Equals(""))
				{
					DateTime.TryParse(meetingData.DownloadFile.DownloadTime.BeginTime, out result);
				}
				if (!meetingData.DownloadFile.DownloadTime.EndTime.Equals(""))
				{
					DateTime.TryParse(meetingData.DownloadFile.DownloadTime.EndTime, out result2);
				}
				if (!meetingData.DownloadFile.BrowseTime.BeginTime.Equals(""))
				{
					DateTime.TryParse(meetingData.DownloadFile.BrowseTime.BeginTime, out result3);
				}
				if (!meetingData.DownloadFile.BrowseTime.BeginTime.Equals(""))
				{
					DateTime.TryParse(meetingData.DownloadFile.BrowseTime.EndTime, out result4);
				}
				if (result <= DateTime.Now && DateTime.Now < result2)
				{
					str = "Y";
				}
				else if (DateTime.Now > result2)
				{
					str = "O";
				}
				if (result3 <= DateTime.Now && DateTime.Now < result4)
				{
					str2 = "Y";
				}
				else if (DateTime.Now > result4)
				{
					str2 = "O";
				}
				Enum.TryParse(str + str2, out meetingRoomButtonType);
				switch (meetingRoomButtonType)
				{
				case MeetingRoomButtonType.YY:
					btnAllFileRowsUpdate.Visibility = Visibility.Visible;
					break;
				}
				TaskFactory factory2 = Task.Factory;
				if (action2 == null)
				{
					action2 = new Action(_003C_003Ec__DisplayClass5e._003CGetMeetingData_DoAction_003Eb__59);
				}
				factory2.StartNew(action2);
			}
			else
			{
				AutoClosingMessageBox.Show("無法取得資料，請稍後再試");
			}
			MouseTool.ShowArrow();
		}

		private string MeetingDataCT_GetWatermark_Callback()
		{
			string result = "";
			try
			{
				if (meetingData != null && meetingData.watermark != null && meetingData.watermark.Equals("Y"))
				{
					result = UserID + "-" + UserName;
				}
				if (DateTime.Parse(meetingData.BeginTime) <= DateTime.Now && DateTime.Now < DateTime.Parse(meetingData.EndTime))
				{
					result = "";
				}
				Home home = Enumerable.FirstOrDefault(Enumerable.OfType<Home>(Application.Current.Windows));
				if (!home.IsInSync)
				{
					return result;
				}
				result = "";
				return result;
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
				return result;
			}
		}

		private bool MeetingDataCT_IsAllFileRowFinished_AddInitUIFinished_Callback()
		{
			return ++Loaded_FileCount == All_FileCount;
		}

		private void MeetingDataCT_HangTheDownloadEvent_Callback(string LastFileItemID)
		{
			bool meetingDataCT_AutoDownload = Settings.Default.MeetingDataCT_AutoDownload;
			bool meetingDataCT_AutoUpdate = Settings.Default.MeetingDataCT_AutoUpdate;
			switch (meetingRoomButtonType)
			{
			case MeetingRoomButtonType.NO:
			case MeetingRoomButtonType.YO:
			case MeetingRoomButtonType.ON:
			case MeetingRoomButtonType.OY:
			case MeetingRoomButtonType.OO:
				return;
			}
			if (meetingDataCT_AutoDownload)
			{
				MeetingDataCT_RaiseAllDownload_Callback(LastFileItemID);
			}
			else if (meetingDataCT_AutoUpdate)
			{
				MeetingDataCT_RaiseAllDownload_Callback(LastFileItemID, true, true);
			}
		}

		private void MeetingDataCT_RaiseAllDownload_Callback(string LastFileItemID, bool IsAutoUpdate = false, bool DoNotChangeAlready_RaiseAllDownload = false)
		{
			_003C_003Ec__DisplayClass69 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass69();
			_003C_003Ec__DisplayClass.LastFileItemID = LastFileItemID;
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<string, bool, bool>(MeetingDataCT_RaiseAllDownload_Callback), _003C_003Ec__DisplayClass.LastFileItemID, IsAutoUpdate, DoNotChangeAlready_RaiseAllDownload);
			}
			else if (!Already_RaiseAllDownload)
			{
				_003C_003Ec__DisplayClass6b _003C_003Ec__DisplayClass6b = new _003C_003Ec__DisplayClass6b();
				_003C_003Ec__DisplayClass6b.CS_0024_003C_003E8__locals6a = _003C_003Ec__DisplayClass;
				if (!DoNotChangeAlready_RaiseAllDownload)
				{
					Already_RaiseAllDownload = true;
				}
				_003C_003Ec__DisplayClass6b.list = new List<File_DownloadItemViewModel>();
				foreach (FileRow item in Enumerable.OfType<FileRow>(FileRowSP.Children))
				{
					if (item.fileItem != null && (item.fileItem.DownloadBytes == 0 || item.fileItem.DownloadBytes < item.fileItem.TotalBytes) && (!IsAutoUpdate || item.fileItem.CanUpdate))
					{
						item.btnOpen.Visibility = Visibility.Collapsed;
						item.btnDownload.Visibility = Visibility.Collapsed;
						item.btnPausing.Visibility = Visibility.Collapsed;
						item.txtPercent.Text = "等待中";
						item.txtPercent.Foreground = Brushes.Gray;
						item.txtPercent.Visibility = Visibility.Visible;
						item.pb.Foreground = Brushes.Wheat;
						item.pb.Background = Brushes.Gray;
						item.pb.Value = item.fileItem.NowPercentage;
						item.pb.Visibility = Visibility.Visible;
						item.btnPause.Visibility = Visibility.Visible;
						_003C_003Ec__DisplayClass6b.list.Add(item.fileItem);
					}
				}
				Task.Factory.StartNew(new Action(_003C_003Ec__DisplayClass6b._003CMeetingDataCT_RaiseAllDownload_Callback_003Eb__67));
			}
		}

		private int MeetingDataCT_GetAgendaInwWorkCount_Callback(string AgendaID)
		{
			_003C_003Ec__DisplayClass6e _003C_003Ec__DisplayClass6e = new _003C_003Ec__DisplayClass6e();
			_003C_003Ec__DisplayClass6e.AgendaID = AgendaID;
			return Enumerable.Count(Enumerable.Where(Enumerable.OfType<AgendaRow>(AgendaRowSP.Children), new Func<AgendaRow, bool>(_003C_003Ec__DisplayClass6e._003CMeetingDataCT_GetAgendaInwWorkCount_Callback_003Eb__6d)));
		}

		private void MeetingDataCT_ShowAgendaFile_Callback(string AgendaID, string ParentID, bool IsDbClick)
		{
			SetColorBlackOn_TxtName_TxtRecord_TxtSubjectOnGray_AgendaRow();
			string text = "";
			text = (AgendaID.Equals("All") ? GetChildernAgendaIDs(AgendaID, AgendaFilter.顯示全部附件) : ((!IsDbClick || (!ParentID.Equals("c0") && !ParentID.Equals("i0") && !ParentID.Equals("0"))) ? GetChildernAgendaIDs(AgendaID, AgendaFilter.顯示當前議題附件) : GetChildernAgendaIDs(AgendaID, AgendaFilter.顯示父議題和子議題附件)));
			if (!AgendaID.Equals("") && !AgendaID.Equals("c") && !AgendaID.Equals("i"))
			{
				int num = 0;
				foreach (FileRow item in Enumerable.OfType<FileRow>(FileRowSP.Children))
				{
					string agendaID = item.meetingDataDownloadFileFile.AgendaID;
					if (AgendaID.Equals("All") || !agendaID.Equals("") || !agendaID.Equals("c") || !agendaID.Equals("i"))
					{
						if (AgendaID.Equals("All"))
						{
							num++;
							item.Visibility = Visibility.Visible;
						}
						else if (text.Contains("," + agendaID + ","))
						{
							num++;
							item.txtIndex.Text = num.ToString();
							item.Visibility = Visibility.Visible;
						}
						else
						{
							item.Visibility = Visibility.Collapsed;
						}
					}
					else
					{
						item.Visibility = Visibility.Collapsed;
					}
				}
			}
			else
			{
				int num2 = 0;
				foreach (FileRow item2 in Enumerable.OfType<FileRow>(FileRowSP.Children))
				{
					string agendaID2 = item2.meetingDataDownloadFileFile.AgendaID;
					if (agendaID2.Equals("") || agendaID2.Equals("c") || agendaID2.Equals("i"))
					{
						num2++;
						item2.txtIndex.Text = num2.ToString();
						item2.Visibility = Visibility.Visible;
					}
					else
					{
						item2.Visibility = Visibility.Collapsed;
					}
				}
			}
		}

		private string GetChildernAgendaIDs(string AgendaID, AgendaFilter agendaFilter)
		{
			Func<MeetingDataAgenda, bool> func = null;
			_003C_003Ec__DisplayClass76 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass76();
			_003C_003Ec__DisplayClass.AgendaID = AgendaID;
			List<string> list = new List<string>();
			list.Add(_003C_003Ec__DisplayClass.AgendaID);
			if (meetingData == null)
			{
				return "";
			}
			switch (agendaFilter)
			{
			case AgendaFilter.顯示父議題和子議題附件:
			{
				MeetingDataAgenda[] agendaList2 = meetingData.AgendaList;
				if (func == null)
				{
					func = new Func<MeetingDataAgenda, bool>(_003C_003Ec__DisplayClass._003CGetChildernAgendaIDs_003Eb__70);
				}
				IEnumerable<MeetingDataAgenda> source = Enumerable.Where(agendaList2, func);
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate74 == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate74 = new Func<MeetingDataAgenda, string>(_003CGetChildernAgendaIDs_003Eb__71);
				}
				List<string> collection2 = Enumerable.ToList(Enumerable.Select(source, CS_0024_003C_003E9__CachedAnonymousMethodDelegate74));
				list.AddRange(collection2);
				break;
			}
			case AgendaFilter.顯示全部附件:
			{
				MeetingDataAgenda[] agendaList = meetingData.AgendaList;
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate75 == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate75 = new Func<MeetingDataAgenda, string>(_003CGetChildernAgendaIDs_003Eb__72);
				}
				List<string> collection = Enumerable.ToList(Enumerable.Select(agendaList, CS_0024_003C_003E9__CachedAnonymousMethodDelegate75));
				list.AddRange(collection);
				break;
			}
			}
			return string.Format(",{0},", string.Join(",", list));
		}

		private void SetColorBlackOn_TxtName_TxtRecord_TxtSubjectOnGray_AgendaRow()
		{
			txtMeetingName.Foreground = Brushes.Black;
			txtSubject.Foreground = ColorTool.HexColorToBrush("#A1a19d");
			txtRecord.Foreground = Brushes.Black;
			foreach (AgendaRow item in Enumerable.OfType<AgendaRow>(AgendaRowSP.Children))
			{
				item.txtAgendaName.Foreground = Brushes.Black;
				item.txtAgendaName.Inlines.LastInline.Foreground = new SolidColorBrush(Color.FromRgb(161, 161, 157));
				item.txtCaption.Foreground = new SolidColorBrush(Color.FromRgb(161, 161, 157));
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/meetingdatact.xaml", UriKind.Relative);
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
				C1 = (ColumnDefinition)target;
				break;
			case 2:
				C2 = (ColumnDefinition)target;
				break;
			case 3:
				btnSeries = (Image)target;
				break;
			case 4:
				LeftDP = (DockPanel)target;
				break;
			case 5:
				txtMeetingName = (TextBlock)target;
				break;
			case 6:
				btnRecord = (Grid)target;
				break;
			case 7:
				mask = (Border)target;
				break;
			case 8:
				txtRecord = (TextBlock)target;
				break;
			case 9:
				imgSubject = (Image)target;
				break;
			case 10:
				txtSubject = (TextBlock)target;
				break;
			case 11:
				AgendaRowSP = (StackPanel)target;
				break;
			case 12:
				FileGrid = (Grid)target;
				break;
			case 13:
				mask2 = (Border)target;
				break;
			case 14:
				txtData = (TextBlock)target;
				break;
			case 15:
				btnAllFileRowsUpdate = (DockPanel)target;
				break;
			case 16:
				FileRowSP = (StackPanel)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}

		[CompilerGenerated]
		private void _003CMeetingDataCT_Loaded_003Eb__0()
		{
			InitSelectDB();
			base.Dispatcher.BeginInvoke(new Action(_003CMeetingDataCT_Loaded_003Eb__1));
		}

		[CompilerGenerated]
		private void _003CMeetingDataCT_Loaded_003Eb__1()
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
			MouseTool.ShowHand();
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
		private static bool _003CMeetingDataCT_Counting_Finished_FileCount_Callback_003Eb__19(MenuButton x)
		{
			return x.userButton.ID.Equals("BtnSync");
		}

		[CompilerGenerated]
		private static File_DownloadItemViewModel _003CMeetingDataCT_GetBookVMs_ByMeetingFileCate_Callback_003Eb__20(FileRow x)
		{
			return x.fileItem;
		}

		[CompilerGenerated]
		private static bool _003CMeetingDataCT_GetBookVMs_ByMeetingFileCate_Callback_003Eb__21(File_DownloadItemViewModel x)
		{
			if (x != null && x.DownloadBytes != 0)
			{
				return x.DownloadBytes >= x.TotalBytes;
			}
			return false;
		}

		[CompilerGenerated]
		private static File_DownloadItemViewModel _003CMeetingDataCT_GetBookVMs_ByMeetingFileCate_Callback_003Eb__22(FileRow x)
		{
			return x.fileItem;
		}

		[CompilerGenerated]
		private static File_DownloadItemViewModel _003CMeetingDataCT_GetBookVMs_ByMeetingFileCate_Callback_003Eb__24(FileRow x)
		{
			return x.fileItem;
		}

		[CompilerGenerated]
		private static bool _003CMeetingDataCT_GetBookVMs_ByMeetingFileCate_Callback_003Eb__25(File_DownloadItemViewModel x)
		{
			if (x.DownloadBytes != 0 && x.DownloadBytes >= x.TotalBytes)
			{
				return x.AgendaID.Equals("record");
			}
			return false;
		}

		[CompilerGenerated]
		private static File_DownloadItemViewModel _003CMeetingDataCT_GetBookVMs_ByMeetingFileCate_Callback_003Eb__26(FileRow x)
		{
			return x.fileItem;
		}

		[CompilerGenerated]
		private static bool _003CMeetingDataCT_GetBookVMs_ByMeetingFileCate_Callback_003Eb__27(File_DownloadItemViewModel x)
		{
			if (x.DownloadBytes != 0 && x.DownloadBytes >= x.TotalBytes)
			{
				if (!x.AgendaID.Equals("") && !x.AgendaID.Equals("c"))
				{
					return x.AgendaID.Equals("i");
				}
				return true;
			}
			return false;
		}

		[CompilerGenerated]
		private void _003CInitUI_003Eb__51(MeetingData md)
		{
			GetMeetingData_DoAction(md);
		}

		[CompilerGenerated]
		private void _003CInitUI_003Eb__52(MeetingData md)
		{
			GetMeetingData_DoAction(md);
		}

		[CompilerGenerated]
		private void _003CGetMeetingData_DoAction_003Eb__55()
		{
			_003C_003Ec__DisplayClass62 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass62();
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			List<string> files_AgendaIDs;
			if (meetingData.DownloadFile != null)
			{
				MeetingDataDownloadFileFile[] downloadFileList = meetingData.DownloadFile.DownloadFileList;
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate60 == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate60 = new Func<MeetingDataDownloadFileFile, string>(_003CGetMeetingData_DoAction_003Eb__56);
				}
				files_AgendaIDs = Enumerable.ToList(Enumerable.Select(downloadFileList, CS_0024_003C_003E9__CachedAnonymousMethodDelegate60));
			}
			else
			{
				files_AgendaIDs = new List<string>();
			}
			_003C_003Ec__DisplayClass.Files_AgendaIDs = files_AgendaIDs;
			List<string> parentIDs;
			if (meetingData.AgendaList != null)
			{
				MeetingDataAgenda[] agendaList = meetingData.AgendaList;
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate61 == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate61 = new Func<MeetingDataAgenda, string>(_003CGetMeetingData_DoAction_003Eb__57);
				}
				parentIDs = Enumerable.ToList(Enumerable.Select(agendaList, CS_0024_003C_003E9__CachedAnonymousMethodDelegate61));
			}
			else
			{
				parentIDs = new List<string>();
			}
			_003C_003Ec__DisplayClass.ParentIDs = parentIDs;
			base.Dispatcher.BeginInvoke(new Action(_003C_003Ec__DisplayClass._003CGetMeetingData_DoAction_003Eb__58));
		}

		[CompilerGenerated]
		private static string _003CGetMeetingData_DoAction_003Eb__56(MeetingDataDownloadFileFile x)
		{
			return x.AgendaID;
		}

		[CompilerGenerated]
		private static string _003CGetMeetingData_DoAction_003Eb__57(MeetingDataAgenda x)
		{
			return x.ParentID;
		}

		[CompilerGenerated]
		private static string _003CGetChildernAgendaIDs_003Eb__71(MeetingDataAgenda x)
		{
			return x.ID;
		}

		[CompilerGenerated]
		private static string _003CGetChildernAgendaIDs_003Eb__72(MeetingDataAgenda x)
		{
			return x.ID;
		}
	}
}
