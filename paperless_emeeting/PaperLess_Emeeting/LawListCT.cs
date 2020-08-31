using Network;
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
	public class LawListCT : UserControl, IComponentConnector
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass3
		{
			public Law_DownloadItemViewModel lawItem;

			public bool _003CLawListCT_UnZipError_Callback_003Eb__1(LawRow x)
			{
				return x.lawDataLaw.ID.Equals(lawItem.ID);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass7
		{
			public Law_DownloadItemViewModel lawItem;

			public bool _003CLawListCT_UnZip_Callback_003Eb__5(LawRow x)
			{
				return x.lawDataLaw.ID.Equals(lawItem.ID);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClassb
		{
			public Law_DownloadItemViewModel lawItem;

			public bool _003CLawListCT_DownloadFileStart_Callback_003Eb__9(LawRow x)
			{
				return x.lawDataLaw.ID.Equals(lawItem.ID);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClassf
		{
			public Law_DownloadItemViewModel lawItem;

			public bool _003CLawListCT_DownloadFileCompleted_Callback_003Eb__d(LawRow x)
			{
				return x.lawDataLaw.ID.Equals(lawItem.ID);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass13
		{
			public Law_DownloadItemViewModel lawItem;

			public bool _003CLawListCT_DownloadProgressChanged_Callback_003Eb__11(LawRow x)
			{
				return x.lawDataLaw.ID.Equals(lawItem.ID);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass19
		{
			public Law_DownloadItemViewModel lawItem;

			public bool _003CLawListCT_GetBookVMs_ByMeetingFileCate_Callback_003Eb__18(LawRow x)
			{
				if (x.lawItem.FileCate == lawItem.FileCate && x.lawItem.DownloadBytes != 0)
				{
					return x.lawItem.DownloadBytes >= x.lawItem.TotalBytes;
				}
				return false;
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass20
		{
			public LawListCT _003C_003E4__this;

			public LawData ld;

			public void _003CGetLawData_DoAction_003Eb__1d()
			{
				_003C_003E4__this.Dispatcher.BeginInvoke(new Action(_003CGetLawData_DoAction_003Eb__1e));
			}

			public void _003CGetLawData_DoAction_003Eb__1e()
			{
				int num = 0;
				LawDataLaw[] lawList = ld.LawList;
				foreach (LawDataLaw lawDataLaw in lawList)
				{
					num++;
					bool isLastRow = num == ld.LawList.Length;
					_003C_003E4__this.LawRowSP.Children.Add(new LawRow(_003C_003E4__this.UserID, _003C_003E4__this.UserName, _003C_003E4__this.UserPWD, isLastRow, lawDataLaw, new LawListCT_HangTheDownloadEvent_Function(_003C_003E4__this.LawListCT_HangTheDownloadEvent_Callback), new LawListCT_IsAllLawRowFinished_AddInitUIFinished_Function(_003C_003E4__this.LawListCT_IsAllLawRowFinished_AddInitUIFinished_Callback), new LawListCT_GetBookVMs_ByMeetingFileCate_Function(_003C_003E4__this.LawListCT_GetBookVMs_ByMeetingFileCate_Callback)));
				}
			}
		}

		private LawListCT_DownloadFileStart_Function Start_callback;

		private LawListCT_DownloadProgressChanged_Function Progress_callback;

		private LawListCT_DownloadFileCompleted_Function Finish_callback;

		private LawListCT_UnZip_Function UnZip_callback;

		private LawListCT_UnZipError_Function UnZipError_callback;

		private LawListCT_GetBookVMs_ByMeetingFileCate_Function GetBookVMs_ByMeetingFileCate_callback;

		public int All_FileCount;

		public int Loaded_FileCount;

		internal TextBlock LawCT_Title;

		internal TextBox txtKeyword;

		internal StackPanel LawRowSP;

		private bool _contentLoaded;

		[CompilerGenerated]
		private string _003CUserID_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserName_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserPWD_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CMeetingID_003Ek__BackingField;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate17;

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

		public LawListCT()
		{
			MouseTool.ShowLoading();
			InitializeComponent();
			base.Loaded += new RoutedEventHandler(LawListCT_Loaded);
			base.Unloaded += new RoutedEventHandler(LawListCT_Unloaded);
			base.IsVisibleChanged += new DependencyPropertyChangedEventHandler(LawListCT_IsVisibleChanged);
		}

		private void LawListCT_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (!(bool)e.NewValue)
			{
				Singleton_LawDownloader.GetInstance().ClearLawListCTEvent();
			}
		}

		private void LawListCT_Unloaded(object sender, RoutedEventArgs e)
		{
			Singleton_LawDownloader.GetInstance().ClearLawListCTEvent();
		}

		private void LawListCT_Loaded(object sender, RoutedEventArgs e)
		{
			InitSelectDB();
			base.Dispatcher.BeginInvoke(new Action(_003CLawListCT_Loaded_003Eb__0));
		}

		private void InitSelectDB()
		{
			DataTable dataTable = MSCE.GetDataTable("select UserID,UserName,UserPWD,MeetingID from NowLogin");
			if (dataTable.Rows.Count > 0)
			{
				UserID = dataTable.Rows[0]["UserID"].ToString();
				UserName = dataTable.Rows[0]["UserName"].ToString();
				UserPWD = dataTable.Rows[0]["UserPWD"].ToString();
				MeetingID = dataTable.Rows[0]["MeetingID"].ToString();
			}
		}

		private void LawListCT_UnZipError_Callback(Law_DownloadItemViewModel lawItem)
		{
			Func<LawRow, bool> func = null;
			_003C_003Ec__DisplayClass3 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass3();
			_003C_003Ec__DisplayClass.lawItem = lawItem;
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<Law_DownloadItemViewModel>(LawListCT_UnZipError_Callback), _003C_003Ec__DisplayClass.lawItem);
				return;
			}
			IEnumerable<LawRow> source = Enumerable.OfType<LawRow>(LawRowSP.Children);
			if (func == null)
			{
				func = new Func<LawRow, bool>(_003C_003Ec__DisplayClass._003CLawListCT_UnZipError_Callback_003Eb__1);
			}
			LawRow lawRow = Enumerable.FirstOrDefault(Enumerable.Where(source, func));
			if (lawRow == null)
			{
				return;
			}
			if (_003C_003Ec__DisplayClass.lawItem.FileType == LawFileType.更新檔解壓縮失敗)
			{
				Storyboard storyboard = (Storyboard)lawRow.TryFindResource("sbUpdate");
				if (storyboard != null)
				{
					storyboard.Stop();
				}
				lawRow.btnUpdate.Visibility = Visibility.Visible;
			}
			else
			{
				Storyboard storyboard = (Storyboard)lawRow.TryFindResource("sb");
				if (storyboard != null)
				{
					storyboard.Stop();
				}
				lawRow.btnDownload.Visibility = Visibility.Visible;
			}
		}

		private void LawListCT_UnZip_Callback(Law_DownloadItemViewModel lawItem)
		{
			Func<LawRow, bool> func = null;
			_003C_003Ec__DisplayClass7 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass7();
			_003C_003Ec__DisplayClass.lawItem = lawItem;
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<Law_DownloadItemViewModel>(LawListCT_UnZip_Callback), _003C_003Ec__DisplayClass.lawItem);
				return;
			}
			IEnumerable<LawRow> source = Enumerable.OfType<LawRow>(LawRowSP.Children);
			if (func == null)
			{
				func = new Func<LawRow, bool>(_003C_003Ec__DisplayClass._003CLawListCT_UnZip_Callback_003Eb__5);
			}
			LawRow lawRow = Enumerable.FirstOrDefault(Enumerable.Where(source, func));
			if (lawRow == null)
			{
				return;
			}
			if (_003C_003Ec__DisplayClass.lawItem.FileType == LawFileType.更新檔解壓縮中)
			{
				lawRow.txtUpdatePercent.Text = "100 %";
				lawRow.pbUpdate.Value = lawRow.pb.Maximum;
				lawRow.txtUpdatePercent.Visibility = Visibility.Collapsed;
				lawRow.pbUpdate.Visibility = Visibility.Collapsed;
				Storyboard storyboard = (Storyboard)lawRow.TryFindResource("sbUpdate");
				if (storyboard != null)
				{
					storyboard.Begin();
				}
			}
			else
			{
				lawRow.txtPercent.Text = "100 %";
				lawRow.pb.Value = lawRow.pb.Maximum;
				lawRow.txtPercent.Visibility = Visibility.Collapsed;
				lawRow.pb.Visibility = Visibility.Collapsed;
				Storyboard storyboard = (Storyboard)lawRow.TryFindResource("sb");
				if (storyboard != null)
				{
					storyboard.Begin();
				}
			}
		}

		private void LawListCT_DownloadFileStart_Callback(Law_DownloadItemViewModel lawItem)
		{
			Func<LawRow, bool> func = null;
			_003C_003Ec__DisplayClassb _003C_003Ec__DisplayClassb = new _003C_003Ec__DisplayClassb();
			_003C_003Ec__DisplayClassb.lawItem = lawItem;
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<Law_DownloadItemViewModel>(LawListCT_DownloadFileStart_Callback), _003C_003Ec__DisplayClassb.lawItem);
				return;
			}
			IEnumerable<LawRow> source = Enumerable.OfType<LawRow>(LawRowSP.Children);
			if (func == null)
			{
				func = new Func<LawRow, bool>(_003C_003Ec__DisplayClassb._003CLawListCT_DownloadFileStart_Callback_003Eb__9);
			}
			LawRow lawRow = Enumerable.FirstOrDefault(Enumerable.Where(source, func));
			if (lawRow != null)
			{
				lawRow.txtPercent.Text = "0 %";
				lawRow.txtPercent.Foreground = Brushes.Black;
				lawRow.pb.Value = 0.0;
				lawRow.pb.Foreground = Brushes.Orange;
				lawRow.pb.Background = Brushes.Black;
			}
		}

		private void LawListCT_DownloadFileCompleted_Callback(Law_DownloadItemViewModel lawItem)
		{
			Func<LawRow, bool> func = null;
			_003C_003Ec__DisplayClassf _003C_003Ec__DisplayClassf = new _003C_003Ec__DisplayClassf();
			_003C_003Ec__DisplayClassf.lawItem = lawItem;
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<Law_DownloadItemViewModel>(LawListCT_DownloadFileCompleted_Callback), _003C_003Ec__DisplayClassf.lawItem);
				return;
			}
			IEnumerable<LawRow> source = Enumerable.OfType<LawRow>(LawRowSP.Children);
			if (func == null)
			{
				func = new Func<LawRow, bool>(_003C_003Ec__DisplayClassf._003CLawListCT_DownloadFileCompleted_Callback_003Eb__d);
			}
			LawRow lawRow = Enumerable.FirstOrDefault(Enumerable.Where(source, func));
			if (lawRow == null)
			{
				return;
			}
			if (_003C_003Ec__DisplayClassf.lawItem.FileType == LawFileType.更新檔已下載完成)
			{
				Storyboard storyboard = (Storyboard)lawRow.TryFindResource("sb");
				if (storyboard != null)
				{
					storyboard.Stop();
				}
				lawRow.txtIsNew.Visibility = Visibility.Visible;
				return;
			}
			Storyboard storyboard2 = (Storyboard)lawRow.TryFindResource("sb");
			if (storyboard2 != null)
			{
				storyboard2.Stop();
			}
			lawRow.btnDownload.Visibility = Visibility.Collapsed;
			lawRow.btnOpen.Visibility = Visibility.Visible;
			lawRow.btnDelete.Visibility = Visibility.Visible;
			lawRow.txtIsNew.Visibility = Visibility.Visible;
		}

		private void LawListCT_DownloadProgressChanged_Callback(Law_DownloadItemViewModel lawItem)
		{
			Func<LawRow, bool> func = null;
			_003C_003Ec__DisplayClass13 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass13();
			_003C_003Ec__DisplayClass.lawItem = lawItem;
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<Law_DownloadItemViewModel>(LawListCT_DownloadProgressChanged_Callback), _003C_003Ec__DisplayClass.lawItem);
				return;
			}
			IEnumerable<LawRow> source = Enumerable.OfType<LawRow>(LawRowSP.Children);
			if (func == null)
			{
				func = new Func<LawRow, bool>(_003C_003Ec__DisplayClass._003CLawListCT_DownloadProgressChanged_Callback_003Eb__11);
			}
			LawRow lawRow = Enumerable.FirstOrDefault(Enumerable.Where(source, func));
			if (lawRow != null)
			{
				lawRow.txtPercent.Text = (int)_003C_003Ec__DisplayClass.lawItem.NowPercentage + " %";
				lawRow.pb.Value = _003C_003Ec__DisplayClass.lawItem.NowPercentage;
				lawRow.btnDownload.Visibility = Visibility.Collapsed;
				lawRow.txtPercent.Visibility = Visibility.Visible;
				lawRow.pb.Visibility = Visibility.Visible;
			}
		}

		private void InitEvent()
		{
			LawDownloader instance = Singleton_LawDownloader.GetInstance();
			Start_callback = (LawListCT_DownloadFileStart_Function)Delegate.Combine(Start_callback, new LawListCT_DownloadFileStart_Function(LawListCT_DownloadFileStart_Callback));
			Progress_callback = (LawListCT_DownloadProgressChanged_Function)Delegate.Combine(Progress_callback, new LawListCT_DownloadProgressChanged_Function(LawListCT_DownloadProgressChanged_Callback));
			Finish_callback = (LawListCT_DownloadFileCompleted_Function)Delegate.Combine(Finish_callback, new LawListCT_DownloadFileCompleted_Function(LawListCT_DownloadFileCompleted_Callback));
			UnZip_callback = (LawListCT_UnZip_Function)Delegate.Combine(UnZip_callback, new LawListCT_UnZip_Function(LawListCT_UnZip_Callback));
			UnZipError_callback = (LawListCT_UnZipError_Function)Delegate.Combine(UnZipError_callback, new LawListCT_UnZipError_Function(LawListCT_UnZipError_Callback));
			GetBookVMs_ByMeetingFileCate_callback = new LawListCT_GetBookVMs_ByMeetingFileCate_Function(LawListCT_GetBookVMs_ByMeetingFileCate_Callback);
			instance.LawListCT_DownloadFileStart_Event += Start_callback;
			instance.LawListCT_DownloadProgressChanged_Event += Progress_callback;
			instance.LawListCT_DownloadFileCompleted_Event += Finish_callback;
			instance.LawListCT_UnZip_Event += UnZip_callback;
			instance.LawListCT_UnZipError_Event += UnZipError_callback;
			txtKeyword.MouseEnter += new MouseEventHandler(_003CInitEvent_003Eb__15);
			TextBox textBox = txtKeyword;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate17 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate17 = new MouseEventHandler(_003CInitEvent_003Eb__16);
			}
			textBox.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate17;
			txtKeyword.KeyUp += new KeyEventHandler(txtKeyword_KeyUp);
			txtKeyword.Focus();
		}

		private Dictionary<string, BookVM> LawListCT_GetBookVMs_ByMeetingFileCate_Callback(Law_DownloadItemViewModel lawItem)
		{
			_003C_003Ec__DisplayClass19 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass19();
			_003C_003Ec__DisplayClass.lawItem = lawItem;
			Dictionary<string, BookVM> dictionary = new Dictionary<string, BookVM>();
			IEnumerable<LawRow> enumerable = Enumerable.Where(Enumerable.OfType<LawRow>(LawRowSP.Children), new Func<LawRow, bool>(_003C_003Ec__DisplayClass._003CLawListCT_GetBookVMs_ByMeetingFileCate_Callback_003Eb__18));
			string filePath = ClickOnceTool.GetFilePath();
			string file_UnZipFileFolder = Settings.Default.File_UnZipFileFolder;
			string str = filePath + "\\" + file_UnZipFileFolder + "\\" + UserID;
			if (enumerable != null)
			{
				foreach (LawRow item in enumerable)
				{
					dictionary[item.lawItem.Name] = new BookVM(item.lawItem.ID, str + "\\" + item.lawItem.ID, _003C_003Ec__DisplayClass.lawItem.FileCate);
				}
				return dictionary;
			}
			return dictionary;
		}

		private void txtKeyword_KeyUp(object sender, KeyEventArgs e)
		{
			string text = txtKeyword.Text.ToLower().Trim();
			if (!text.Equals(""))
			{
				foreach (LawRow item in Enumerable.OfType<LawRow>(LawRowSP.Children))
				{
					if (item.lawDataLaw.Name.ToLower().Contains(text) || item.lawDataLaw.UpDate.Split(' ')[0].Contains(text))
					{
						item.Visibility = Visibility.Visible;
					}
					else
					{
						item.Visibility = Visibility.Collapsed;
					}
				}
			}
			else
			{
				foreach (LawRow item2 in Enumerable.OfType<LawRow>(LawRowSP.Children))
				{
					item2.Visibility = Visibility.Visible;
				}
			}
		}

		private void InitUI()
		{
			Action<LawData> action = null;
			MouseTool.ShowLoading();
			LawCT_Title.Text = Settings.Default.LawButtonName;
			new HttpRequest();
			if (NetworkTool.CheckNetwork() > 0)
			{
				if (action == null)
				{
					action = new Action<LawData>(_003CInitUI_003Eb__1b);
				}
				GetLawData.AsyncPOST(action);
				return;
			}
			DataTable dataTable = MSCE.GetDataTable("select LawJson from LawData");
			if (dataTable.Rows.Count > 0)
			{
				LawData ld = JsonConvert.DeserializeObject<LawData>(dataTable.Rows[0]["LawJson"].ToString());
				GetLawData_DoAction(ld);
			}
			else
			{
				AutoClosingMessageBox.Show("無法取得資料，請稍後再試");
				MouseTool.ShowArrow();
			}
		}

		private void GetLawData_DoAction(LawData ld)
		{
			Action action = null;
			_003C_003Ec__DisplayClass20 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass20();
			_003C_003Ec__DisplayClass.ld = ld;
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<LawData>(GetLawData_DoAction), _003C_003Ec__DisplayClass.ld);
				return;
			}
			if (_003C_003Ec__DisplayClass.ld != null)
			{
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003C_003Ec__DisplayClass._003CGetLawData_DoAction_003Eb__1d);
				}
				factory.StartNew(action);
				DataTable dataTable = MSCE.GetDataTable("select LawJson from LawData");
				if (dataTable.Rows.Count > 0)
				{
					MSCE.ExecuteNonQuery("UPDATE [LawData] SET [LawJson] = @1", JsonConvert.SerializeObject(_003C_003Ec__DisplayClass.ld));
				}
				else
				{
					MSCE.ExecuteNonQuery("INSERT INTO [LawData] ([LawJson])\r\n                                                            VALUES (@1)", JsonConvert.SerializeObject(_003C_003Ec__DisplayClass.ld));
				}
			}
			else
			{
				AutoClosingMessageBox.Show("無法取得資料，請稍後再試");
			}
			MouseTool.ShowArrow();
		}

		private bool LawListCT_IsAllLawRowFinished_AddInitUIFinished_Callback()
		{
			return ++Loaded_FileCount == All_FileCount;
		}

		private void LawListCT_HangTheDownloadEvent_Callback(string LastLawItemID)
		{
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/lawlistct.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				LawCT_Title = (TextBlock)target;
				break;
			case 2:
				txtKeyword = (TextBox)target;
				break;
			case 3:
				LawRowSP = (StackPanel)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}

		[CompilerGenerated]
		private void _003CLawListCT_Loaded_003Eb__0()
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
		private void _003CInitEvent_003Eb__15(object sender, MouseEventArgs e)
		{
			MouseTool.ShowIBeam();
			txtKeyword.Focus();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__16(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private void _003CInitUI_003Eb__1b(LawData ld)
		{
			GetLawData_DoAction(ld);
		}
	}
}
