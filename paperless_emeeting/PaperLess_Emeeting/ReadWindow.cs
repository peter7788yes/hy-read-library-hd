using BookFormatLoader;
using BookManagerModule;
using CACodec;
using DataAccessObject;
using DownloadManagerModule;
using iTextSharp.text;
using iTextSharp.text.pdf;
using LocalFilesManagerModule;
using MultiLanquageModule;
using Network;
using Newtonsoft.Json;
using O2S.Components.PDFRender4NET;
using PaperLess_Emeeting.App_Code;
using PaperLess_Emeeting.App_Code.MessageBox;
using PaperLess_Emeeting.App_Code.Socket;
using PaperLess_Emeeting.App_Code.Tools;
using PaperLess_Emeeting.App_Code.ViewModel;
using PaperLess_Emeeting.Properties;
using PaperLess_ViewModel;
using PaperlessSync.Broadcast.Service;
using PaperlessSync.Broadcast.Socket;
using PXCView36;
using SyncCenterModule;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;
using Utility;
using Wpf_CustomCursor;

namespace PaperLess_Emeeting
{
	public class ReadWindow : Window, IDisposable, IEventManager, IComponentConnector, IStyleConnector
	{
		public class NativeMethods
		{
			[DllImport("ole32.dll")]
			public static extern void CoTaskMemFree(IntPtr pv);

			[DllImport("ole32.dll")]
			public static extern IntPtr CoTaskMemAlloc(IntPtr cb);

			[DllImport("libpdf2jpg.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
			public static extern IntPtr pdfLoadFromMemory(int dpi, float scale, IntPtr ibuf, int ilen, IntPtr obptr, IntPtr olptr, int pgs);

			[DllImport("libpdf2jpg.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
			public static extern int pdfNumberOfPages(IntPtr ibuf, int pgs);

			[DllImport("libpdf2jpg.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
			public static extern int pdfPageSize(int dpi, float scale, IntPtr ibuf, int ilen, IntPtr pWidth, IntPtr pHeight, int pgs);

			[DllImport("libpdf2jpg.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
			public static extern IntPtr pdfLoadFromMemoryPartial(int dpi, float scale, IntPtr ibuf, int ilen, IntPtr obptr, IntPtr olptr, int x0, int y0, int x1, int y1, int pgs);
		}

		internal delegate void LoadingPageHandler(int pageIndex);

		private delegate void setImgSourceCallback(BitmapImage imgSource, float pdfScale);

		private delegate void setMsgToActionCallback(Dictionary<string, object> msgStrings);

		public enum Definition
		{
			One = 1,
			Two,
			Three,
			Four,
			Five,
			Six,
			Seven,
			Eight,
			Nine,
			Ten
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass2
		{
			public int downloadedFilesCount;

			public int totalFilesCount;

			public ReadWindow _003C_003E4__this;

			public void _003Cfsw_Changed_003Eb__0()
			{
				if (!totalFilesCount.Equals(downloadedFilesCount))
				{
					if (_003C_003E4__this.downloadProgBar.Value < (double)downloadedFilesCount)
					{
						_003C_003E4__this.downloadProgBar.Value = downloadedFilesCount;
					}
				}
				else
				{
					_003C_003E4__this.downloadProgBar.Visibility = Visibility.Collapsed;
					_003C_003E4__this.fsw.EnableRaisingEvents = false;
					_003C_003E4__this.fsw.IncludeSubdirectories = false;
					_003C_003E4__this.fsw.Changed -= new FileSystemEventHandler(_003C_003E4__this.fsw_Changed);
					_003C_003E4__this.fsw = null;
				}
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass9
		{
			public int targetPageIndex;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClassb
		{
			public _003C_003Ec__DisplayClass9 CS_0024_003C_003E8__localsa;

			public IAsyncResult result;

			public ReadWindow _003C_003E4__this;

			public void _003CcheckOtherDevicePage_003Eb__7(IAsyncResult ar)
			{
				LoadingEvent.EndInvoke(result);
				_003C_003E4__this.Dispatcher.BeginInvoke(new Invoker(_003CcheckOtherDevicePage_003Eb__8));
			}

			public void _003CcheckOtherDevicePage_003Eb__8()
			{
				Canvas canvas = FindVisualChildByName<Canvas>(_003C_003E4__this.FR, "zoomCanvas");
				canvas.Background = null;
				int lastPageMode = _003C_003E4__this.lastPageMode;
				if (lastPageMode.Equals(1))
				{
					_003C_003E4__this.bringBlockIntoView(CS_0024_003C_003E8__localsa.targetPageIndex);
				}
				else if (lastPageMode.Equals(2))
				{
					int doubleCurPageIndex = _003C_003E4__this.getDoubleCurPageIndex(CS_0024_003C_003E8__localsa.targetPageIndex);
					_003C_003E4__this.bringBlockIntoView(doubleCurPageIndex);
				}
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass33
		{
			public ReadPagePair singlePagePair;

			public string imagePath;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass35
		{
			public _003C_003Ec__DisplayClass33 CS_0024_003C_003E8__locals34;

			public Border bd;

			public ReadWindow _003C_003E4__this;

			public void _003CRepaintPDF_003Eb__30()
			{
				try
				{
					_003C_003E4__this.getPHEJSingleBitmapImageAsync(_003C_003E4__this.caTool, _003C_003E4__this.defaultKey, CS_0024_003C_003E8__locals34.imagePath, _003C_003E4__this.PDFScale, CS_0024_003C_003E8__locals34.singlePagePair.leftPageIndex, bd);
				}
				catch (Exception ex)
				{
					LogTool.Debug(ex);
				}
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass38
		{
			public Border bd;

			public ReadPagePair item;

			public ReadWindow _003C_003E4__this;

			public void _003CRepaintPDF_003Eb__31()
			{
				_003C_003E4__this.getPHEJSingleBitmapImageAsync(_003C_003E4__this.caTool, _003C_003E4__this.defaultKey, item.leftImagePath, _003C_003E4__this.PDFScale, _003C_003E4__this.curPageIndex, bd);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass3b
		{
			public _003C_003Ec__DisplayClass38 CS_0024_003C_003E8__locals39;

			public string leftImagePath;

			public string rightImagePath;

			public void _003CRepaintPDF_003Eb__32()
			{
				CS_0024_003C_003E8__locals39._003C_003E4__this.getPHEJDoubleBitmapImageAsync(CS_0024_003C_003E8__locals39._003C_003E4__this.caTool, CS_0024_003C_003E8__locals39._003C_003E4__this.defaultKey, leftImagePath, rightImagePath, CS_0024_003C_003E8__locals39._003C_003E4__this.PDFScale, CS_0024_003C_003E8__locals39._003C_003E4__this.curPageIndex, CS_0024_003C_003E8__locals39.bd);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass41
		{
			public Dictionary<string, object> msgStrings;

			public ReadWindow _003C_003E4__this;

			public void _003CparseJSonFromMessage_003Eb__3e()
			{
				_003C_003E4__this.Dispatcher.BeginInvoke(new Action(_003CparseJSonFromMessage_003Eb__3f));
			}

			public void _003CparseJSonFromMessage_003Eb__3f()
			{
				_003C_003E4__this.bringBlockIntoView(1);
				InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(_003C_003E4__this.FR, "penMemoCanvas");
				string msgString = msgStrings["spline"].ToString();
				_003C_003E4__this.setMsgToAction(msgStrings);
				inkCanvas.Strokes.Clear();
				_003C_003E4__this.drawStrokeFromJson(msgString);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass55
		{
			public ToggleButton syncButton;

			public ReadWindow _003C_003E4__this;

			public void _003CAttachEvent_003Eb__49(object sender, MouseButtonEventArgs e)
			{
				Task.Factory.StartNew(new Action(_003CAttachEvent_003Eb__4a));
			}

			public void _003CAttachEvent_003Eb__4c(object sender, MouseButtonEventArgs e)
			{
				Task.Factory.StartNew(new Action(_003CAttachEvent_003Eb__4d));
			}

			public void _003CAttachEvent_003Eb__4f(object sender, MouseButtonEventArgs e)
			{
				Task.Factory.StartNew(new Action(_003CAttachEvent_003Eb__50));
			}

			public void _003CAttachEvent_003Eb__4a()
			{
				if (_003C_003E4__this.socket == null)
				{
					Singleton_Socket.ReaderEvent = _003C_003E4__this;
					_003C_003E4__this.socket = Singleton_Socket.GetInstance(_003C_003E4__this.meetingId, _003C_003E4__this.account, _003C_003E4__this.userName, true);
				}
				_003C_003E4__this.Dispatcher.BeginInvoke(new Action(_003CAttachEvent_003Eb__4b));
			}

			public void _003CAttachEvent_003Eb__4b()
			{
				AutoClosingMessageBox.Show("連線中");
				if (syncButton != null)
				{
					syncButton.IsChecked = true;
					syncButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
				}
			}

			public void _003CAttachEvent_003Eb__4d()
			{
				if (_003C_003E4__this.socket == null)
				{
					Singleton_Socket.ReaderEvent = _003C_003E4__this;
					_003C_003E4__this.socket = Singleton_Socket.GetInstance(_003C_003E4__this.meetingId, _003C_003E4__this.account, _003C_003E4__this.userName, false);
				}
				_003C_003E4__this.Dispatcher.BeginInvoke(new Action(_003CAttachEvent_003Eb__4e));
			}

			public void _003CAttachEvent_003Eb__4e()
			{
				AutoClosingMessageBox.Show("連線中");
				if (syncButton != null)
				{
					syncButton.IsChecked = false;
					syncButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
				}
			}

			public void _003CAttachEvent_003Eb__50()
			{
				if (_003C_003E4__this.socket == null)
				{
					Singleton_Socket.ReaderEvent = _003C_003E4__this;
					_003C_003E4__this.socket = Singleton_Socket.GetInstance(_003C_003E4__this.meetingId, _003C_003E4__this.account, _003C_003E4__this.userName, false);
				}
				_003C_003E4__this.Dispatcher.BeginInvoke(new Action(_003CAttachEvent_003Eb__51));
			}

			public void _003CAttachEvent_003Eb__51()
			{
				AutoClosingMessageBox.Show("連線中");
				if (syncButton != null)
				{
					syncButton.IsChecked = false;
					syncButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
				}
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass6d
		{
			public Action act;

			public void _003CMyAnimation_003Eb__6c(object sender2, EventArgs e2)
			{
				if (act != null)
				{
					act();
				}
			}
		}

		private const int LOGPIXELSX = 88;

		private const int LOGPIXELSY = 90;

		private FlowDocument _FlowDocument;

		private FlowDocument _FlowDocumentDouble;

		private CACodecTools caTool = new CACodecTools();

		private List<ThumbnailImageAndPage> singleThumbnailImageAndPageList;

		private List<ThumbnailImageAndPage> doubleThumbnailImageAndPageList;

		private List<ImageStatus> singleImgStatus;

		private List<ImageStatus> doubleImgStatus;

		private Dictionary<int, ReadPagePair> singleReadPagePair;

		private Dictionary<int, ReadPagePair> doubleReadPagePair;

		private TransformGroup tfgForImage;

		private TransformGroup tfgForHyperLink;

		private System.Windows.Point start;

		private System.Windows.Point imageOrigin;

		private System.Windows.Point hyperlinkOrigin;

		private int curPageIndex;

		private int offsetOfImage;

		private int trialPages;

		private object selectedBook;

		private BookType bookType;

		private string bookId;

		private string account;

		private string vendorId;

		private ConfigurationManager configMng;

		private static DispatcherTimer checkImageStatusTimer;

		private string bookPath;

		private bool isFirstTimeLoaded;

		private int userBookSno;

		private int PDFdpi = 96;

		public double DpiX;

		public double DpiY;

		private float PDFScale = 1f;

		private double baseScale = 1.0;

		private int zoomStep;

		private double[] zoomStepScale = new double[9]
		{
			1.0,
			1.25,
			1.5,
			1.75,
			2.0,
			2.25,
			2.5,
			2.75,
			3.0
		};

		private bool canPrint;

		private byte[][] decodedPDFPages = new byte[2][];

		private XmlDocument XmlDocNcx;

		private HEJMetadata hejMetadata;

		private PageInfoManager pageInfoManager;

		private PageInfoMetadata pageInfo;

		private byte[] defaultKey;

		private Dictionary<int, BookMarkData> bookMarkDictionary;

		private Dictionary<int, NoteData> bookNoteDictionary;

		private Dictionary<int, List<StrokesData>> bookStrokesDictionary;

		private Dictionary<string, LastPageData> lastViewPage;

		private LocalFilesManager localFileMng;

		private DateTime lastTimeOfChangingPage;

		private List<Stroke> tempStrokes;

		private StylusPointCollection stylusPC;

		private Stroke strokeLine;

		private int lastPageMode = 2;

		private bool isStrokeLine;

		private string bookRightsDRM = "";

		private bool isSharedButtonShowed;

		private FileSystemWatcher fsw;

		private bool isWindowsXP;

		private bool needPreload;

		private TimeSpan checkInterval = new TimeSpan(0, 0, 0, 0, 200);

		private string CName = Environment.MachineName;

		private BookManager bookManager;

		private MultiLanquageManager langMng;

		private string userName;

		private string email;

		private string meetingId;

		private string watermark;

		private string dbPath;

		private string webServiceURL;

		private string socketMessage;

		private bool isSyncing;

		private bool isSyncOwner;

		private SocketClient socket;

		private bool needToSendBroadCast;

		private bool IsSmallDisplaySize;

		private bool IsFirstCapture = true;

		private bool isAllBookPageChecked;

		private ObservableCollection<MediaList> ObservableMediaList;

		private bool requestInitialOwnership = true;

		private bool loadMutexWasCreated;

		private bool useOriginalCanvasOnLockStatus;

		private double newImageWidth;

		private double newImageHeight;

		private bool isLockButtonLocked;

		private int firstIndex;

		private bool isAreaButtonAndPenMemoRequestSent;

		private bool isPDFRendering;

		private int checkImageStatusRetryTimes;

		private int checkImageStatusMaxRetryTimes = 5;

		private bool ifAskedJumpPage;

		private bool isFirstTimeChangingPage;

		internal static LoadingPageHandler LoadingEvent;

		private IInputElement pageViewerPager;

		private double originalCanvasWidth = 1.0;

		private double originalCanvasHeight = 1.0;

		private double fullScreenCanvasWidth = 1.0;

		private double fullScreenCanvasHeight = 1.0;

		private double baseStrokesCanvasWidth;

		private double baseStrokesCanvasHeight;

		private string StatusFileName = "originalPenmemoStatus.xml";

		private int i;

		private bool BookMarkInLBIsClicked;

		private bool NoteButtonInLBIsClicked;

		private bool isFullScreenButtonClick;

		private PageMode viewStatusIndex = PageMode.DoublePage;

		private MediaCanvasOpenedBy openedby;

		private int clickedPage;

		private Dictionary<MediaCanvasOpenedBy, StackPanel> RelativePanel = new Dictionary<MediaCanvasOpenedBy, StackPanel>();

		private int thumbNailListBoxStatus;

		private bool thumbNailListBoxOpenedFullScreen;

		private double thumbnailListBoxHeight = 150.0;

		private bool canAreaButtonBeSeen = true;

		private List<Thread> zoomeThread = new List<Thread>();

		private double ratio;

		private bool isSameScale;

		private int allowedSharedTimes = 10;

		private DateTime stopMovingMouseTime;

		private int keyboardMoveParam = 5;

		private string _managerId = "ReadWindow";

		private string _msg;

		private string _clientId;

		private string splineString = "";

		private bool closeBook;

		private bool isReadWindowLoaded;

		private bool CanSentLine;

		public Dictionary<string, BookVM> cbBooksData = new Dictionary<string, BookVM>();

		private bool isFirstLoad;

		internal Grid mainGrid;

		internal FlowDocumentReader FR;

		internal ProgressBar downloadProgBar;

		internal Grid NewUITop;

		internal StackPanel btnFuncSP;

		internal StackPanel btnBoldSP;

		internal Grid btnThin;

		internal Grid btnMedium;

		internal Grid btnLarge;

		internal StackPanel btnPenFuncSP;

		internal StackPanel PenColorSP;

		internal Grid NewUI;

		internal RadioButton btnPen;

		internal System.Windows.Controls.Image PenSlideCtrl;

		internal StackPanel PenSP;

		internal RadioButton btnPenColor;

		internal RadioButton btnBold;

		internal Grid btnEraserGD;

		internal RadioButton btnEraser;

		internal System.Windows.Controls.Image SettingSlideCtrl;

		internal StackPanel SettingSP;

		internal StackPanel BookMarkSP;

		internal RadioButton btnBookMark;

		internal StackPanel MemoSP;

		internal RadioButton btnNoteButton;

		internal StackPanel SentMailSP;

		internal StackPanel ViewThumbSP;

		internal RadioButton btnViewThumb;

		internal System.Windows.Controls.Image btnClose;

		internal Canvas thumnailCanvas;

		internal StackPanel SearchSP;

		internal TextBox txtKeyword;

		internal RadioButton btnTxtKeywordClear;

		internal TextBlock txtFilterCount;

		internal StackPanel thumbNailCanvasStackPanel;

		internal Grid thumbNailCanvasGrid;

		internal StackPanel RadioButtonStackPanel;

		internal RadioButton AllImageButtonInListBox;

		internal StackPanel AllImageButtonInListBoxSP;

		internal RadioButton AllImageButtonInListBoxNew;

		internal RadioButton BookMarkButtonInListBox;

		internal System.Windows.Shapes.Rectangle Rect1;

		internal StackPanel BookMarkButtonInListBoxSP;

		internal RadioButton BookMarkButtonInListBoxNew;

		internal RadioButton NoteButtonInListBox;

		internal System.Windows.Shapes.Rectangle Rect2;

		internal StackPanel NoteButtonInListBoxSP;

		internal RadioButton NoteButtonInListBoxNew;

		internal RadioButton HideListBoxButton;

		internal Grid thumbNailListBoxGD;

		internal ListBox thumbNailListBox;

		internal RadioButton ShowListBoxButton;

		internal RadioButton LockButton;

		internal DocumentViewer DV;

		private bool _contentLoaded;

		[CompilerGenerated]
		private static Func<ThumbnailImageAndPage, int> CS_0024_003C_003E9__CachedAnonymousMethodDelegate12;

		[CompilerGenerated]
		private static Func<ThumbnailImageAndPage, int> CS_0024_003C_003E9__CachedAnonymousMethodDelegate19;

		[CompilerGenerated]
		private static Func<ThumbnailImageAndPage, int> CS_0024_003C_003E9__CachedAnonymousMethodDelegate1b;

		[CompilerGenerated]
		private static Action CS_0024_003C_003E9__CachedAnonymousMethodDelegate25;

		[CompilerGenerated]
		private static EventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate2a;

		[CompilerGenerated]
		private static Comparison<string> CS_0024_003C_003E9__CachedAnonymousMethodDelegate58;

		[CompilerGenerated]
		private static Func<SearchRecord, int> CS_0024_003C_003E9__CachedAnonymousMethodDelegate5e;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate68;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate73;

		public string managerId
		{
			get
			{
				return _managerId;
			}
			set
			{
				_managerId = value;
			}
		}

		public string msg
		{
			get
			{
				return _msg;
			}
			set
			{
				_msg = value;
			}
		}

		public string clientId
		{
			get
			{
				return _clientId;
			}
			set
			{
				_clientId = value;
			}
		}

		public event EventHandler<imageSourceRenderedResultEventArgs> imageSourceRendered;

		public event Home_OpenBookFromReader_Function Home_OpenBookFromReader_Event;

		public void Dispose()
		{
			GC.Collect();
		}

		protected override void OnContentRendered(EventArgs e)
		{
			base.Topmost = true;
			base.Topmost = false;
			base.OnContentRendered(e);
		}

		private void CheckOSVersion()
		{
			OperatingSystem oSVersion = Environment.OSVersion;
			int major = oSVersion.Version.Major;
			if (major == 5 && oSVersion.Version.Minor != 0)
			{
				isWindowsXP = true;
				Console.WriteLine("Windows XP");
			}
		}

		private void savePageMode()
		{
			string sqlCommand = "";
			if (viewStatusIndex.Equals(PageMode.SinglePage))
			{
				sqlCommand = "update userbook_metadata set pdfPageMode = 1 Where Sno= " + userBookSno;
			}
			else if (viewStatusIndex.Equals(PageMode.DoublePage))
			{
				sqlCommand = "update userbook_metadata set pdfPageMode = 2 Where Sno= " + userBookSno;
			}
			bookManager.sqlCommandNonQuery(sqlCommand);
		}

		private void saveLastReadingPage()
		{
			int num = 0;
			if (viewStatusIndex.Equals(PageMode.SinglePage))
			{
				num = curPageIndex;
			}
			else if (viewStatusIndex.Equals(PageMode.DoublePage))
			{
				ReadPagePair readPagePair = doubleReadPagePair[curPageIndex];
				num = Math.Min(readPagePair.leftPageIndex, readPagePair.rightPageIndex);
				if (num == -1)
				{
					num = Math.Max(readPagePair.leftPageIndex, readPagePair.rightPageIndex);
				}
			}
			string machineName = Environment.MachineName;
			DateTime value = new DateTime(1970, 1, 1);
			long num2 = DateTime.Now.ToUniversalTime().Subtract(value).Ticks / 10000000;
			bool flag = false;
			LastPageData lastPageData = null;
			if (lastViewPage == null)
			{
				lastPageData = new LastPageData();
				lastPageData.index = num;
				lastPageData.updatetime = num2;
				lastPageData.objectId = "";
				lastPageData.createtime = num2;
				lastPageData.synctime = 0L;
				lastPageData.status = "0";
				lastPageData.device = machineName;
				flag = false;
			}
			else if (lastViewPage.ContainsKey(machineName))
			{
				lastPageData = lastViewPage[machineName];
				lastPageData.index = num;
				lastPageData.updatetime = num2;
				flag = true;
			}
			else
			{
				lastPageData = new LastPageData();
				lastPageData.index = num;
				lastPageData.updatetime = num2;
				lastPageData.objectId = "";
				lastPageData.createtime = num2;
				lastPageData.synctime = 0L;
				lastPageData.status = "0";
				lastPageData.device = machineName;
				flag = false;
			}
			bookManager.saveLastviewPage(userBookSno, flag, lastPageData);
		}

		private void clearReadPagePairData(Dictionary<int, ReadPagePair> pagePair)
		{
			int count = pagePair.Count;
			for (int i = 0; i < count; i++)
			{
				ReadPagePair readPagePair = pagePair[i];
				if (readPagePair.leftImageSource != null)
				{
					readPagePair.leftImageSource = null;
					readPagePair.decodedPDFPages = new byte[2][];
				}
			}
			pagePair.Clear();
			pagePair = null;
		}

		private bool checkThumbnailBorderAndMediaListStatus()
		{
			int count = hejMetadata.allFileList.Count;
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				string path = bookPath + "\\HYWEB\\" + hejMetadata.allFileList[i];
				if (File.Exists(path))
				{
					num++;
				}
			}
			string[] files = Directory.GetFiles(bookPath + "\\HYWEB\\", "*.pdf");
			if (bookType.Equals(BookType.HEJ))
			{
				files = Directory.GetFiles(bookPath + "\\HYWEB\\", "*.jpg");
			}
			for (int j = 0; j < files.Length; j++)
			{
				for (int k = 0; k < hejMetadata.LImgList.Count; k++)
				{
					if (files[j].Substring(files[j].LastIndexOf("\\") + 1).Equals(hejMetadata.LImgList[k].path.Replace("HYWEB\\", "")) && !singleThumbnailImageAndPageList[k].isDownloaded)
					{
						singleThumbnailImageAndPageList[k].isDownloaded = true;
					}
				}
			}
			if (files.Length.Equals(hejMetadata.LImgList.Count))
			{
				isAllBookPageChecked = true;
			}
			if (ObservableMediaList != null)
			{
				for (int l = 0; l < ObservableMediaList.Count; l++)
				{
					for (int m = 0; m < ObservableMediaList[l].mediaList.Count; m++)
					{
						if (File.Exists(ObservableMediaList[l].mediaList[m].mediaSourcePath) && !ObservableMediaList[l].mediaList[m].downloadStatus)
						{
							ObservableMediaList[l].mediaList[m].downloadStatus = true;
						}
					}
				}
			}
			files = null;
			if (!count.Equals(num))
			{
				downloadProgBar.Value = num;
				return false;
			}
			downloadProgBar.Visibility = Visibility.Collapsed;
			return true;
		}

		private void fsw_Changed(object sender, FileSystemEventArgs e)
		{
			Action action = null;
			_003C_003Ec__DisplayClass2 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass2();
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			if (hejMetadata == null)
			{
				return;
			}
			string name = e.Name;
			string fileName = System.IO.Path.GetFileName(name.Replace(".tmp", ""));
			_003C_003Ec__DisplayClass.downloadedFilesCount = 0;
			int count = hejMetadata.LImgList.Count;
			if (!isAllBookPageChecked)
			{
				for (int i = 0; i < count; i++)
				{
					if (!singleThumbnailImageAndPageList[i].isDownloaded)
					{
						if (hejMetadata.LImgList[i].path.Contains(fileName))
						{
							singleThumbnailImageAndPageList[i].isDownloaded = true;
							_003C_003Ec__DisplayClass.downloadedFilesCount++;
							break;
						}
					}
					else
					{
						_003C_003Ec__DisplayClass.downloadedFilesCount++;
					}
				}
				isAllBookPageChecked = ((count == _003C_003Ec__DisplayClass.downloadedFilesCount) ? true : false);
			}
			else
			{
				_003C_003Ec__DisplayClass.downloadedFilesCount = hejMetadata.LImgList.Count;
				isAllBookPageChecked = ((count == _003C_003Ec__DisplayClass.downloadedFilesCount) ? true : false);
			}
			int num = 0;
			if (isAllBookPageChecked)
			{
				int count2 = ObservableMediaList.Count;
				for (int j = 0; j < count2; j++)
				{
					int count3 = ObservableMediaList[j].mediaList.Count;
					num += count3;
					for (int k = 0; k < count3; k++)
					{
						if (!ObservableMediaList[j].mediaList[k].downloadStatus)
						{
							string fileName2 = System.IO.Path.GetFileName(ObservableMediaList[j].mediaList[k].mediaSourcePath);
							if (fileName2 == fileName)
							{
								ObservableMediaList[j].mediaList[k].downloadStatus = true;
								_003C_003Ec__DisplayClass.downloadedFilesCount++;
								break;
							}
						}
						else
						{
							_003C_003Ec__DisplayClass.downloadedFilesCount++;
						}
					}
				}
			}
			_003C_003Ec__DisplayClass.totalFilesCount = count + num;
			try
			{
				Dispatcher dispatcher = base.Dispatcher;
				if (action == null)
				{
					action = new Action(_003C_003Ec__DisplayClass._003Cfsw_Changed_003Eb__0);
				}
				dispatcher.Invoke(action, new object[0]);
			}
			catch
			{
			}
		}

		private void _InitializedEventHandler(object sender, EventArgs e)
		{
			getBookPath();
			loadBookXMLFiles();
			initializeTransFromGroup();
			GetDpiSetting(out DpiX, out DpiY);
			PDFdpi = Convert.ToInt32(Math.Max(DpiX, DpiY));
			prepareReadingPageDataSource();
			setDirection();
			AddHotKeys();
			base.Initialized -= new EventHandler(_InitializedEventHandler);
		}

		private void AddHotKeys()
		{
			RoutedCommand routedCommand = new RoutedCommand();
			routedCommand.InputGestures.Add(new KeyGesture(Key.OemMinus, ModifierKeys.Control));
			base.CommandBindings.Add(new CommandBinding(routedCommand, new ExecutedRoutedEventHandler(RepeatButton_Click_1)));
			RoutedCommand routedCommand2 = new RoutedCommand();
			routedCommand2.InputGestures.Add(new KeyGesture(Key.OemPlus, ModifierKeys.Control));
			base.CommandBindings.Add(new CommandBinding(routedCommand2, new ExecutedRoutedEventHandler(RepeatButton_Click_2)));
		}

		[DllImport("gdi32.dll")]
		private static extern int GetDeviceCaps(IntPtr hdc, int Index);

		[DllImport("user32.dll")]
		private static extern IntPtr GetDC(IntPtr Hwnd);

		private void GetDpiSetting(out double DpiX, out double DpiY)
		{
			IntPtr dC = GetDC(IntPtr.Zero);
			DpiX = GetDeviceCaps(dC, 88);
			DpiY = GetDeviceCaps(dC, 90);
		}

		private void setDirection()
		{
			if (hejMetadata.direction.Equals("right"))
			{
				_FlowDocument.FlowDirection = FlowDirection.RightToLeft;
				_FlowDocumentDouble.FlowDirection = FlowDirection.RightToLeft;
			}
			else
			{
				_FlowDocument.FlowDirection = FlowDirection.LeftToRight;
				_FlowDocumentDouble.FlowDirection = FlowDirection.LeftToRight;
			}
		}

		public void getBookRightsAsync(string bookId)
		{
			if (bookRightsDRM != null && bookRightsDRM != "")
			{
				try
				{
					XmlDocument xmlDocument = new XmlDocument();
					string xml = caTool.stringDecode(bookRightsDRM, true);
					xmlDocument.LoadXml(xml);
					XmlNodeList xmlNodeList = xmlDocument.SelectNodes("/drm/functions");
					foreach (XmlNode item in xmlNodeList)
					{
						if (item.InnerText.Contains("canPrint"))
						{
							canPrint = true;
							break;
						}
					}
				}
				catch
				{
				}
			}
		}

		private void initializeTransFromGroup()
		{
			tfgForImage = new TransformGroup();
			ScaleTransform value = new ScaleTransform();
			tfgForImage.Children.Add(value);
			TranslateTransform value2 = new TranslateTransform();
			tfgForImage.Children.Add(value2);
			tfgForHyperLink = new TransformGroup();
			ScaleTransform value3 = new ScaleTransform();
			tfgForHyperLink.Children.Add(value3);
			TranslateTransform value4 = new TranslateTransform();
			tfgForHyperLink.Children.Add(value4);
			value = null;
			value2 = null;
			value3 = null;
			value4 = null;
		}

		public string getCipherValue(string encryptionFile)
		{
			string result = "";
			if (!File.Exists(encryptionFile))
			{
				return result;
			}
			XmlDocument xmlDocument = new XmlDocument();
			try
			{
				xmlDocument.Load(encryptionFile);
				XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("enc:CipherValue");
				result = elementsByTagName[0].InnerText;
				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine("getCipherValue error=" + ex.ToString());
				return result;
			}
		}

		private Bitmap renPdfToBitmap(string pageFile, byte[] key, int pg, int dpi, float scal, int decodedPageIndex, bool isSinglePage)
		{
			System.Drawing.Color white = System.Drawing.Color.White;
			Bitmap aBitmap = null;
			try
			{
				if (decodedPDFPages[decodedPageIndex] == null)
				{
					decodedPDFPages[decodedPageIndex] = caTool.fileAESDecode(pageFile, key);
				}
			}
			catch (Exception ex)
			{
				decodedPDFPages[decodedPageIndex] = null;
				LogTool.Debug(ex);
			}
			try
			{
				PDFDoc pDFDoc = new PDFDoc();
				pDFDoc.Init("PVD20-M4IRG-QYZK9-MNJ2U-DFTK1-MAJ4L", "PDFX3$Henry$300604_Allnuts#");
				pDFDoc.OpenFromMemory(decodedPDFPages[decodedPageIndex], (uint)decodedPDFPages[decodedPageIndex].Length, 0);
				PXCV_Lib36.PXV_CommonRenderParameters aCommonRenderParams = prepareCommonRenderParameter(pDFDoc, dpi, pg, scal, 0, 0, isSinglePage);
				pDFDoc.DrawPageToDIBSection(IntPtr.Zero, pg, white, aCommonRenderParams, out aBitmap);
				pDFDoc.ReleasePageCachedData(pg, 1);
				pDFDoc.Delete();
				return aBitmap;
			}
			catch (Exception ex2)
			{
				LogTool.Debug(ex2);
				return aBitmap;
			}
		}

		private PXCV_Lib36.PXV_CommonRenderParameters prepareCommonRenderParameter(PDFDoc pdfDoc, int dpi, int pageNumber, float zoom, int offsetX, int offsetY, Border border, bool isSinglePage)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(PXCV_Helper.RECT)));
			IntPtr intPtr2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(PXCV_Helper.RECT)));
			System.Drawing.Point point = new System.Drawing.Point(offsetX, offsetY);
			System.Drawing.Size empty = System.Drawing.Size.Empty;
			PXCV_Helper.RECT rECT = default(PXCV_Helper.RECT);
			PXCV_Helper.RECT rECT2 = default(PXCV_Helper.RECT);
			PXCV_Lib36.PXV_CommonRenderParameters result = default(PXCV_Lib36.PXV_CommonRenderParameters);
			PageDimension pageDimension = default(PageDimension);
			pdfDoc.GetPageDimensions(pageNumber, out pageDimension.w, out pageDimension.h);
			double num = border.ActualHeight / 96.0 * (double)dpi;
			double num2 = border.ActualWidth / 96.0 * (double)dpi;
			if (zoomStep == 0)
			{
				empty.Width = (int)(pageDimension.w / 72.0 * (double)dpi * (double)zoom);
				empty.Height = (int)(pageDimension.h / 72.0 * (double)dpi * (double)zoom);
				double num3 = num2 / num;
				double num4 = 0.0;
				num4 = ((!isSinglePage) ? ((double)(empty.Width * 2) / (double)empty.Height) : ((double)empty.Width / (double)empty.Height));
				if ((double)empty.Width < num2 && (double)empty.Height < num)
				{
					double num5;
					double num6;
					if (num4 > num3)
					{
						num5 = num2 / 2.0;
						baseScale = num5 / (double)empty.Width;
						num6 = Math.Round(baseScale * (double)empty.Height, 2);
					}
					else
					{
						num6 = num;
						baseScale = num6 / (double)empty.Height;
						num5 = Math.Round(baseScale * (double)empty.Width, 2);
					}
					empty.Width = (int)num5;
					empty.Height = (int)num6;
				}
				else
				{
					double num7;
					double num8;
					if (num4 > num3)
					{
						num7 = num2 / 2.0;
						baseScale = num7 / (double)empty.Width;
						num8 = Math.Round(baseScale * (double)empty.Height, 2);
					}
					else
					{
						num8 = num;
						baseScale = num8 / (double)empty.Height;
						num7 = Math.Round(baseScale * (double)empty.Width, 2);
					}
					empty.Width = (int)num7;
					empty.Height = (int)num8;
				}
			}
			else
			{
				empty.Width = (int)(pageDimension.w / 72.0 * (double)dpi * (double)zoom * baseScale);
				empty.Height = (int)(pageDimension.h / 72.0 * (double)dpi * (double)zoom * baseScale);
			}
			rECT.left = -point.X;
			rECT.top = -point.Y;
			rECT.right = rECT.left + empty.Width;
			rECT.bottom = rECT.top + empty.Height;
			rECT2.left = 0;
			rECT2.top = 0;
			if (zoomStep == 0)
			{
				if ((double)empty.Width < num2)
				{
					rECT2.right = empty.Width;
				}
				else
				{
					rECT2.right = (int)num2;
				}
				if ((double)empty.Height < num)
				{
					rECT2.bottom = empty.Height;
				}
				else
				{
					rECT2.bottom = (int)num;
				}
			}
			else
			{
				rECT2.right = empty.Width;
				rECT2.bottom = empty.Height;
			}
			Marshal.StructureToPtr((object)rECT, intPtr, false);
			Marshal.StructureToPtr((object)rECT2, intPtr2, false);
			result.WholePageRect = intPtr;
			result.DrawRect = intPtr2;
			result.RenderTarget = PXCV_Lib36.PXCV_RenderMode.pxvrm_Viewing;
			result.Flags = 0;
			return result;
		}

		private PXCV_Lib36.PXV_CommonRenderParameters prepareCommonRenderParameter(PDFDoc pdfDoc, int dpi, int pageNumber, float zoom, int offsetX, int offsetY, bool isSinglePage)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(PXCV_Helper.RECT)));
			IntPtr intPtr2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(PXCV_Helper.RECT)));
			System.Drawing.Point point = new System.Drawing.Point(offsetX, offsetY);
			System.Drawing.Size empty = System.Drawing.Size.Empty;
			PXCV_Helper.RECT rECT = default(PXCV_Helper.RECT);
			PXCV_Helper.RECT rECT2 = default(PXCV_Helper.RECT);
			PXCV_Lib36.PXV_CommonRenderParameters result = default(PXCV_Lib36.PXV_CommonRenderParameters);
			PageDimension pageDimension = default(PageDimension);
			pdfDoc.GetPageDimensions(pageNumber, out pageDimension.w, out pageDimension.h);
			Border borderInReader = GetBorderInReader();
			double num = borderInReader.ActualHeight / 96.0 * (double)dpi;
			double num2 = borderInReader.ActualWidth / 96.0 * (double)dpi;
			if (zoomStep == 0)
			{
				empty.Width = (int)(pageDimension.w / 72.0 * (double)dpi * (double)zoom);
				empty.Height = (int)(pageDimension.h / 72.0 * (double)dpi * (double)zoom);
				double num3 = num2 / num;
				double num4 = 0.0;
				num4 = ((!isSinglePage) ? ((double)(empty.Width * 2) / (double)empty.Height) : ((double)empty.Width / (double)empty.Height));
				if ((double)empty.Width < num2 && (double)empty.Height < num)
				{
					double num5;
					double num6;
					if (num4 > num3)
					{
						num5 = num2 / 2.0;
						baseScale = num5 / (double)empty.Width;
						num6 = Math.Round(baseScale * (double)empty.Height, 2);
					}
					else
					{
						num6 = num;
						baseScale = num6 / (double)empty.Height;
						num5 = Math.Round(baseScale * (double)empty.Width, 2);
					}
					empty.Width = (int)num5;
					empty.Height = (int)num6;
				}
				else
				{
					double num7;
					double num8;
					if (num4 > num3)
					{
						num7 = num2 / 2.0;
						baseScale = num7 / (double)empty.Width;
						num8 = Math.Round(baseScale * (double)empty.Height, 2);
					}
					else
					{
						num8 = num;
						baseScale = num8 / (double)empty.Height;
						num7 = Math.Round(baseScale * (double)empty.Width, 2);
					}
					empty.Width = (int)num7;
					empty.Height = (int)num8;
				}
			}
			else
			{
				empty.Width = (int)(pageDimension.w / 72.0 * (double)dpi * (double)zoom * baseScale);
				empty.Height = (int)(pageDimension.h / 72.0 * (double)dpi * (double)zoom * baseScale);
			}
			rECT.left = -point.X;
			rECT.top = -point.Y;
			rECT.right = rECT.left + empty.Width;
			rECT.bottom = rECT.top + empty.Height;
			rECT2.left = 0;
			rECT2.top = 0;
			if (zoomStep == 0)
			{
				if ((double)empty.Width < num2)
				{
					rECT2.right = empty.Width;
				}
				else
				{
					rECT2.right = (int)num2;
				}
				if ((double)empty.Height < num)
				{
					rECT2.bottom = empty.Height;
				}
				else
				{
					rECT2.bottom = (int)num;
				}
			}
			else
			{
				rECT2.right = empty.Width;
				rECT2.bottom = empty.Height;
			}
			Marshal.StructureToPtr((object)rECT, intPtr, false);
			Marshal.StructureToPtr((object)rECT2, intPtr2, false);
			result.WholePageRect = intPtr;
			result.DrawRect = intPtr2;
			result.RenderTarget = PXCV_Lib36.PXCV_RenderMode.pxvrm_Viewing;
			result.Flags = 0;
			return result;
		}

		private BitmapSource CreateBitmapSourceFromBitmap(Bitmap bitmap)
		{
			if (bitmap == null)
			{
				LogTool.Debug(new ArgumentNullException("bitmap"));
			}
			return Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
		}

		private MemoryStream renPdfToStream(string pageFile, byte[] key, int pg, int dpi, float scal)
		{
			Mutex mutex = new Mutex(requestInitialOwnership, "LoadMutex", out loadMutexWasCreated);
			if (!(requestInitialOwnership & loadMutexWasCreated))
			{
				mutex.WaitOne();
			}
			MemoryStream memoryStream = new MemoryStream();
			MemoryStream memoryStream2 = caTool.fileAESDecode(pageFile, key, false);
			byte[] array = new byte[memoryStream2.Length];
			int num = array.Length;
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			try
			{
				Marshal.Copy(memoryStream2.GetBuffer(), 0, intPtr, num);
			}
			catch
			{
				Marshal.Copy(memoryStream2.GetBuffer(), 0, intPtr, num - 1);
			}
			memoryStream2.Close();
			IntPtr intPtr2 = Marshal.AllocHGlobal(4);
			IntPtr intPtr3 = Marshal.AllocHGlobal(4);
			IntPtr intPtr4 = default(IntPtr);
			int num2 = 1;
			try
			{
				IntPtr intPtr5 = Marshal.AllocHGlobal(4);
				IntPtr intPtr6 = Marshal.AllocHGlobal(4);
				NativeMethods.pdfPageSize(dpi, scal, intPtr, num, intPtr5, intPtr6, pg);
				int x = Marshal.ReadInt32(intPtr5);
				int y = Marshal.ReadInt32(intPtr6);
				Marshal.FreeHGlobal(intPtr5);
				Marshal.FreeHGlobal(intPtr6);
				NativeMethods.pdfLoadFromMemoryPartial(dpi, scal, intPtr, num, intPtr3, intPtr2, 0, 0, x, y, pg);
				intPtr4 = (IntPtr)Marshal.ReadInt32(intPtr3);
				num2 = Marshal.ReadInt32(intPtr2);
				byte[] array2 = new byte[num2];
				Marshal.Copy(intPtr4, array2, 0, num2);
				memoryStream.Write(array2, 0, array2.Length);
			}
			catch
			{
				Marshal.FreeHGlobal(intPtr);
				Marshal.FreeHGlobal(intPtr3);
				Marshal.FreeHGlobal(intPtr2);
			}
			NativeMethods.CoTaskMemFree(intPtr4);
			Marshal.FreeHGlobal(intPtr);
			Marshal.FreeHGlobal(intPtr3);
			Marshal.FreeHGlobal(intPtr2);
			memoryStream.Position = 0L;
			mutex.ReleaseMutex();
			return memoryStream;
		}

		private System.Windows.Controls.Image getPHEJSingleBigPageToReplace(CACodecTools caTool, byte[] curKey, string pagePath, float scal)
		{
			System.Windows.Controls.Image image = new System.Windows.Controls.Image();
			image.Source = getPHEJSingleBitmapImage(caTool, curKey, pagePath, scal);
			image.Stretch = Stretch.Uniform;
			image.Margin = new Thickness(offsetOfImage);
			image.Name = "imageInReader";
			image.RenderTransform = tfgForImage;
			image.MouseLeftButtonDown += new MouseButtonEventHandler(ImageInReader_MouseLeftButtonDown);
			return image;
		}

		private BitmapImage getPHEJSingleBitmapImage(CACodecTools caTool, byte[] curKey, string pagePath, float scal)
		{
			BitmapImage bitmapImage = new BitmapImage();
			Bitmap bitmap = renPdfToBitmap(pagePath, curKey, 0, PDFdpi, scal, 0, true);
			using (MemoryStream memoryStream = new MemoryStream())
			{
				bitmap.Save(memoryStream, ImageFormat.Bmp);
				bitmapImage.BeginInit();
				bitmapImage.StreamSource = memoryStream;
				bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapImage.EndInit();
				bitmapImage.CacheOption = BitmapCacheOption.None;
				bitmapImage.StreamSource.Close();
				bitmapImage.StreamSource = null;
				bitmapImage.Freeze();
				memoryStream.Dispose();
				memoryStream.Close();
				bitmap.Dispose();
				bitmap = null;
				return bitmapImage;
			}
		}

		public bool IsWidthPage(string pagePath)
		{
			bool result = false;
			try
			{
				int num = 0;
				string[] array = pagePath.Split('_', '.');
				num = int.Parse(array[array.Length - 2]);
				string path = System.IO.Path.Combine(bookPath, "HYWEB", "thumbs");
				string[] files = Directory.GetFiles(path);
				string[] array2 = files;
				foreach (string text in array2)
				{
					if (text.Contains("_" + num + ".jpg"))
					{
						System.Drawing.Image image = System.Drawing.Image.FromFile(text);
						if (image.Width > image.Height)
						{
							result = true;
						}
					}
				}
				return result;
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
				return result;
			}
		}

		private void getPHEJSingleBitmapImageAsync(CACodecTools caTool, byte[] curKey, string pagePath, float scal, int curPageIndex, Border border)
		{
			try
			{
				BitmapImage bitmapImage = new BitmapImage();
				if (IsSmallDisplaySize)
				{
					int num = (int)SystemParameters.PrimaryScreenHeight;
					if (IsWidthPage(pagePath))
					{
						switch (num)
						{
						case 768:
							PDFdpi = 250;
							break;
						case 960:
							PDFdpi = 250;
							break;
						case 1024:
							PDFdpi = 192;
							break;
						default:
							PDFdpi = 250;
							break;
						}
					}
					else
					{
						switch (num)
						{
						case 600:
							PDFdpi = 250;
							break;
						case 800:
							PDFdpi = 250;
							break;
						default:
							PDFdpi = 96;
							break;
						}
					}
				}
				else
				{
					int num2 = (int)SystemParameters.PrimaryScreenHeight;
					int num3 = num2;
					if (num3 == 600)
					{
						PDFdpi = 144;
					}
					else
					{
						PDFdpi = 96;
					}
				}
				Bitmap bitmap = renPdfToBitmap(pagePath, curKey, 0, PDFdpi, scal, 0, border, true);
				using (MemoryStream memoryStream = new MemoryStream())
				{
					if (bitmap == null)
					{
						bitmap = renPdfToBitmap(pagePath, curKey, 0, PDFdpi, scal, 0, border, true);
					}
					bitmap.Save(memoryStream, ImageFormat.Bmp);
					bitmapImage.BeginInit();
					bitmapImage.StreamSource = memoryStream;
					bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
					bitmapImage.EndInit();
					bitmapImage.CacheOption = BitmapCacheOption.None;
					bitmapImage.StreamSource.Close();
					bitmapImage.StreamSource = null;
					bitmapImage.Freeze();
					memoryStream.Dispose();
					memoryStream.Close();
					bitmap.Dispose();
					bitmap = null;
				}
				EventHandler<imageSourceRenderedResultEventArgs> eventHandler = this.imageSourceRendered;
				if (eventHandler != null)
				{
					eventHandler(this, new imageSourceRenderedResultEventArgs(bitmapImage, curPageIndex, scal));
				}
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}

		private System.Windows.Controls.Image getPHEJSingleBigPageToReplace(CACodecTools caTool, byte[] curKey, string pagePath)
		{
			return getPHEJSingleBigPageToReplace(caTool, curKey, pagePath, PDFScale);
		}

		private BitmapImage getPHEJDoubleBitmapImage(CACodecTools caTool, byte[] curKey, string leftPagePath, string rightPagePath, float scal)
		{
			BitmapImage bitmapImage = new BitmapImage();
			try
			{
				Bitmap bitmap = renPdfToBitmap(leftPagePath, curKey, 0, PDFdpi, scal, 0, false);
				Bitmap bitmap2 = renPdfToBitmap(rightPagePath, curKey, 0, PDFdpi, scal, 1, false);
				int width = Convert.ToInt32(bitmap.Width + bitmap2.Width);
				int height = Convert.ToInt32(Math.Max(bitmap.Height, bitmap2.Height));
				Bitmap bitmap3 = new Bitmap(width, height);
				using (Graphics graphics = Graphics.FromImage(bitmap3))
				{
					graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
					graphics.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
					graphics.DrawImage(bitmap2, bitmap.Width, 0, bitmap2.Width, bitmap2.Height);
					graphics.Dispose();
				}
				using (MemoryStream memoryStream = new MemoryStream())
				{
					bitmap3.Save(memoryStream, ImageFormat.Bmp);
					bitmapImage.BeginInit();
					bitmapImage.StreamSource = memoryStream;
					bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
					bitmapImage.EndInit();
					bitmapImage.CacheOption = BitmapCacheOption.None;
					bitmapImage.StreamSource.Close();
					bitmapImage.StreamSource = null;
					bitmapImage.Freeze();
					memoryStream.Dispose();
					memoryStream.Close();
					bitmap3.Dispose();
					bitmap3 = null;
				}
				bitmap = null;
				bitmap2 = null;
				GC.Collect();
				return bitmapImage;
			}
			catch
			{
				return bitmapImage;
			}
		}

		private void getPHEJDoubleBitmapImageAsync(CACodecTools caTool, byte[] curKey, string leftPagePath, string rightPagePath, float scal, int curPageIndex, Border border)
		{
			BitmapImage bitmapImage = new BitmapImage();
			Bitmap bitmap = null;
			Bitmap bitmap2 = null;
			Bitmap bitmap3 = null;
			try
			{
				bitmap = renPdfToBitmap(leftPagePath, curKey, 0, PDFdpi, scal, 0, border, false);
				bitmap2 = renPdfToBitmap(rightPagePath, curKey, 0, PDFdpi, scal, 1, border, false);
				int width = Convert.ToInt32(bitmap.Width + bitmap2.Width);
				int height = Convert.ToInt32(Math.Max(bitmap.Height, bitmap2.Height));
				bitmap3 = new Bitmap(width, height);
				using (Graphics graphics = Graphics.FromImage(bitmap3))
				{
					graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
					graphics.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
					graphics.DrawImage(bitmap2, bitmap.Width, 0, bitmap2.Width, bitmap2.Height);
					graphics.Dispose();
				}
				using (MemoryStream memoryStream = new MemoryStream())
				{
					bitmap3.Save(memoryStream, ImageFormat.Bmp);
					bitmapImage.BeginInit();
					bitmapImage.StreamSource = memoryStream;
					bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
					bitmapImage.EndInit();
					bitmapImage.CacheOption = BitmapCacheOption.None;
					bitmapImage.StreamSource.Close();
					bitmapImage.StreamSource = null;
					bitmapImage.Freeze();
					memoryStream.Dispose();
					memoryStream.Close();
					bitmap3.Dispose();
					bitmap3 = null;
				}
				bitmap = null;
				bitmap2 = null;
				GC.Collect();
			}
			catch
			{
				bitmap = null;
				bitmap2 = null;
				bitmap3 = null;
			}
			EventHandler<imageSourceRenderedResultEventArgs> eventHandler = this.imageSourceRendered;
			if (eventHandler != null)
			{
				eventHandler(this, new imageSourceRenderedResultEventArgs(bitmapImage, curPageIndex, scal));
			}
		}

		private BitmapImage getHEJSingleBitmapImage(CACodecTools caTool, byte[] curKey, string lastPagePath, float pdfScale)
		{
			BitmapImage bitmapImage = new BitmapImage();
			try
			{
				using (MemoryStream memoryStream = caTool.fileAESDecode(lastPagePath, curKey, false))
				{
					bitmapImage.BeginInit();
					bitmapImage.StreamSource = memoryStream;
					bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
					bitmapImage.EndInit();
					bitmapImage.CacheOption = BitmapCacheOption.None;
					bitmapImage.StreamSource.Close();
					bitmapImage.StreamSource = null;
					bitmapImage.Freeze();
					memoryStream.Dispose();
					memoryStream.Close();
					return bitmapImage;
				}
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
				return bitmapImage;
			}
		}

		private System.Windows.Controls.Image getSingleBigPageToReplace(CACodecTools caTool, byte[] curKey, string lastPagePath)
		{
			System.Windows.Controls.Image image = new System.Windows.Controls.Image();
			try
			{
				image.Source = getHEJSingleBitmapImage(caTool, curKey, lastPagePath, PDFScale);
				image.Stretch = Stretch.Uniform;
				image.Margin = new Thickness(offsetOfImage);
				image.Name = "imageInReader";
				image.RenderTransform = tfgForImage;
				image.MouseLeftButtonDown += new MouseButtonEventHandler(ImageInReader_MouseLeftButtonDown);
				return image;
			}
			catch
			{
				return image;
			}
		}

		private BitmapImage getHEJDoubleBitmapImage(CACodecTools caTool, byte[] curKey, string leftPagePath, string rightPagePath, float pdfScale)
		{
			BitmapImage bitmapImage = new BitmapImage();
			try
			{
				using (MemoryStream memoryStream = caTool.fileAESDecode(leftPagePath, curKey, false))
				{
					using (MemoryStream memoryStream2 = caTool.fileAESDecode(rightPagePath, curKey, false))
					{
						Bitmap bitmap = new Bitmap(memoryStream);
						Bitmap bitmap2 = new Bitmap(memoryStream2);
						int width = Convert.ToInt32(bitmap.Width + bitmap2.Width);
						int height = Convert.ToInt32(Math.Max(bitmap.Height, bitmap2.Height));
						Bitmap bitmap3 = new Bitmap(width, height);
						using (Graphics graphics = Graphics.FromImage(bitmap3))
						{
							graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
							graphics.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
							graphics.DrawImage(bitmap2, bitmap.Width, 0, bitmap2.Width, bitmap2.Height);
							graphics.Dispose();
						}
						using (MemoryStream memoryStream3 = new MemoryStream())
						{
							bitmap3.Save(memoryStream3, ImageFormat.Bmp);
							bitmapImage.BeginInit();
							bitmapImage.StreamSource = memoryStream3;
							bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
							bitmapImage.EndInit();
							bitmapImage.CacheOption = BitmapCacheOption.None;
							bitmapImage.StreamSource.Close();
							bitmapImage.StreamSource = null;
							bitmapImage.Freeze();
							memoryStream3.Dispose();
							memoryStream3.Close();
							bitmap3.Dispose();
							bitmap3 = null;
						}
						memoryStream.Dispose();
						memoryStream.Close();
						memoryStream2.Dispose();
						memoryStream2.Close();
						bitmap = null;
						bitmap2 = null;
						return bitmapImage;
					}
				}
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
				return bitmapImage;
			}
		}

		private System.Windows.Controls.Image getDoubleBigPageToReplace(CACodecTools caTool, byte[] curKey, string leftPagePath, string rightPagePath)
		{
			System.Windows.Controls.Image image = new System.Windows.Controls.Image();
			try
			{
				image.Source = getHEJDoubleBitmapImage(caTool, curKey, leftPagePath, rightPagePath, PDFScale);
				image.Stretch = Stretch.Uniform;
				image.Margin = new Thickness(offsetOfImage);
				image.Name = "imageInReader";
				image.RenderTransform = tfgForImage;
				image.MouseLeftButtonDown += new MouseButtonEventHandler(ImageInReader_MouseLeftButtonDown);
				return image;
			}
			catch
			{
				return image;
			}
		}

		private int getSingleCurPageIndex(int doubleCurPageIndex)
		{
			doubleCurPageIndex = ((doubleCurPageIndex != 0) ? ((doubleCurPageIndex != _FlowDocumentDouble.Blocks.Count - 1) ? (doubleCurPageIndex * 2) : (_FlowDocument.Blocks.Count - 1)) : 0);
			return doubleCurPageIndex;
		}

		private int getDoubleCurPageIndex(int singleCurPageIndex)
		{
			singleCurPageIndex = ((singleCurPageIndex != 0) ? ((singleCurPageIndex == _FlowDocument.Blocks.Count - 1) ? (_FlowDocumentDouble.Blocks.Count - 1) : ((singleCurPageIndex % 2 != 1) ? (singleCurPageIndex / 2) : ((singleCurPageIndex + 1) / 2))) : 0);
			return singleCurPageIndex;
		}

		private bool prepareReadingPageDataSource()
		{
			Action action = null;
			if (hejMetadata != null)
			{
				_FlowDocumentDouble = new FlowDocument();
				_FlowDocument = new FlowDocument();
				singleThumbnailImageAndPageList = new List<ThumbnailImageAndPage>();
				singleImgStatus = new List<ImageStatus>();
				singleReadPagePair = new Dictionary<int, ReadPagePair>();
				for (int i = 0; i < hejMetadata.SImgList.Count; i++)
				{
					try
					{
						string leftPagePath = bookPath + "\\" + hejMetadata.SImgList[i].path;
						if (hejMetadata.SImgList[i].path.Contains("tryPageEnd"))
						{
							leftPagePath = hejMetadata.SImgList[i].path;
						}
						setFlowDocumentData(hejMetadata.LImgList[i].pageNum, leftPagePath, "", singleThumbnailImageAndPageList, singleImgStatus, _FlowDocument);
						string leftImgPath = bookPath + "\\" + hejMetadata.LImgList[i].path;
						if (hejMetadata.LImgList[i].path.Contains("tryPageEnd"))
						{
							leftImgPath = hejMetadata.LImgList[i].path;
						}
						ReadPagePair value = new ReadPagePair(i, -1, leftImgPath, "", hejMetadata.LImgList[i].pageId, "", PDFdpi);
						if (!singleReadPagePair.ContainsKey(i))
						{
							singleReadPagePair.Add(i, value);
						}
					}
					catch (Exception arg)
					{
						Console.WriteLine("Exception: {0}, From getHEJThumbnailAndPage, Single", arg);
					}
				}
				doubleThumbnailImageAndPageList = new List<ThumbnailImageAndPage>();
				doubleImgStatus = new List<ImageStatus>();
				doubleReadPagePair = new Dictionary<int, ReadPagePair>();
				string text = "";
				string text2 = "";
				for (int j = 0; j < hejMetadata.manifestItemList.Count; j++)
				{
					if (hejMetadata.manifestItemList[j].id.Equals("cover") || hejMetadata.manifestItemList[j].id.Equals("backcover"))
					{
						try
						{
							if (hejMetadata.manifestItemList[j].href.StartsWith("thumbs/"))
							{
								hejMetadata.manifestItemList[j].href = hejMetadata.manifestItemList[j].href.Replace("thumbs/", "");
							}
							string text3 = bookPath + "\\HYWEB\\thumbs\\" + hejMetadata.manifestItemList[j].href;
							if (hejMetadata.manifestItemList[j].id.Equals("cover"))
							{
								text = text3;
							}
							else if (hejMetadata.manifestItemList[j].id.Equals("backcover"))
							{
								text2 = text3;
							}
						}
						catch (Exception arg2)
						{
							Console.WriteLine("Exception: {0}, From getHEJThumbnailAndPage, Double, Cover and BackCover", arg2);
						}
					}
				}
				for (int k = 0; k < hejMetadata.SImgList.Count; k++)
				{
					try
					{
						if ((bookPath + "\\" + hejMetadata.SImgList[k].path).Equals(text))
						{
							setFlowDocumentData(hejMetadata.LImgList[k].pageNum, text, "", doubleThumbnailImageAndPageList, doubleImgStatus, _FlowDocumentDouble);
							string leftImgPath2 = bookPath + "\\" + hejMetadata.LImgList[k].path;
							if (hejMetadata.LImgList[k].path.Contains("tryPageEnd"))
							{
								leftImgPath2 = hejMetadata.LImgList[k].path;
							}
							ReadPagePair value2 = new ReadPagePair(0, -1, leftImgPath2, "", hejMetadata.LImgList[k].pageId, "", PDFdpi);
							if (!doubleReadPagePair.ContainsKey(0))
							{
								doubleReadPagePair.Add(0, value2);
							}
						}
						else if ((bookPath + "\\" + hejMetadata.SImgList[k].path).Equals(text2))
						{
							setFlowDocumentData(hejMetadata.LImgList[k].pageNum, text2, "", doubleThumbnailImageAndPageList, doubleImgStatus, _FlowDocumentDouble);
							int key = (k + 1) / 2;
							string leftImgPath3 = bookPath + "\\" + hejMetadata.LImgList[k].path;
							if (hejMetadata.LImgList[k].path.Contains("tryPageEnd"))
							{
								leftImgPath3 = hejMetadata.LImgList[k].path;
							}
							ReadPagePair value3 = new ReadPagePair(k, -1, leftImgPath3, "", hejMetadata.LImgList[k].pageId, "", PDFdpi);
							if (!doubleReadPagePair.ContainsKey(key))
							{
								doubleReadPagePair.Add(key, value3);
							}
						}
						else if (k % 2 == 1)
						{
							if (k + 1 == hejMetadata.SImgList.Count)
							{
								string leftPagePath2 = bookPath + "\\" + hejMetadata.SImgList[k].path;
								if (hejMetadata.SImgList[k].path.Contains("tryPageEnd"))
								{
									leftPagePath2 = hejMetadata.SImgList[k].path;
								}
								setFlowDocumentData(hejMetadata.LImgList[k].pageNum, leftPagePath2, "", doubleThumbnailImageAndPageList, doubleImgStatus, _FlowDocumentDouble);
								int key2 = (k + 1) / 2;
								string leftImgPath4 = bookPath + "\\" + hejMetadata.LImgList[k].path;
								if (hejMetadata.LImgList[k].path.Contains("tryPageEnd"))
								{
									leftImgPath4 = hejMetadata.LImgList[k].path;
								}
								ReadPagePair value4 = new ReadPagePair(k, -1, leftImgPath4, "", hejMetadata.LImgList[k].pageId, "", PDFdpi);
								if (!doubleReadPagePair.ContainsKey(key2))
								{
									doubleReadPagePair.Add(key2, value4);
								}
								break;
							}
							string leftPagePath3 = bookPath + "\\" + hejMetadata.SImgList[k].path;
							string rightPagePath = bookPath + "\\" + hejMetadata.SImgList[k + 1].path;
							if (hejMetadata.SImgList[k].path.Contains("tryPageEnd"))
							{
								leftPagePath3 = hejMetadata.SImgList[k].path;
							}
							if (hejMetadata.SImgList[k + 1].path.Contains("tryPageEnd"))
							{
								rightPagePath = hejMetadata.SImgList[k + 1].path;
							}
							string pageIndexShowed = hejMetadata.LImgList[k].pageNum + "-" + hejMetadata.LImgList[k + 1].pageNum;
							int num = k;
							int num2 = k + 1;
							int key3 = num2 / 2;
							if (hejMetadata.direction.Equals("right"))
							{
								leftPagePath3 = bookPath + "\\" + hejMetadata.SImgList[k + 1].path;
								rightPagePath = bookPath + "\\" + hejMetadata.SImgList[k].path;
								if (hejMetadata.SImgList[k + 1].path.Contains("tryPageEnd"))
								{
									leftPagePath3 = hejMetadata.SImgList[k + 1].path;
								}
								if (hejMetadata.SImgList[k].path.Contains("tryPageEnd"))
								{
									rightPagePath = hejMetadata.SImgList[k].path;
								}
								pageIndexShowed = hejMetadata.LImgList[k + 1].pageNum + "-" + hejMetadata.LImgList[k].pageNum;
								num = k + 1;
								num2 = k;
								key3 = num / 2;
							}
							setFlowDocumentData(pageIndexShowed, leftPagePath3, rightPagePath, doubleThumbnailImageAndPageList, doubleImgStatus, _FlowDocumentDouble);
							string leftImgPath5 = bookPath + "\\" + hejMetadata.LImgList[num].path;
							if (hejMetadata.LImgList[num].path.Contains("tryPageEnd"))
							{
								leftImgPath5 = hejMetadata.LImgList[num].path;
							}
							string rightImgPath = bookPath + "\\" + hejMetadata.LImgList[num2].path;
							if (hejMetadata.LImgList[num2].path.Contains("tryPageEnd"))
							{
								rightImgPath = hejMetadata.LImgList[num2].path;
							}
							ReadPagePair value5 = new ReadPagePair(num, num2, leftImgPath5, rightImgPath, hejMetadata.LImgList[num].pageId, hejMetadata.LImgList[num2].pageId, PDFdpi);
							if (!doubleReadPagePair.ContainsKey(key3))
							{
								doubleReadPagePair.Add(key3, value5);
							}
						}
					}
					catch (Exception arg3)
					{
						Console.WriteLine("Exception: {0}, From getHEJThumbnailAndPage, Double", arg3);
					}
				}
				if (Settings.Default.IsFlatUIReader)
				{
					thumbNailListBox.ItemsSource = singleThumbnailImageAndPageList;
				}
				else
				{
					TaskFactory factory = Task.Factory;
					if (action == null)
					{
						action = new Action(_003CprepareReadingPageDataSource_003Eb__4);
					}
					factory.StartNew(action);
				}
				_FlowDocumentDouble.PagePadding = new Thickness(0.0);
				_FlowDocument.PagePadding = new Thickness(0.0);
				FR.FontSize = 12.0;
				FlowDocumentReader fR = FR;
				FlowDocumentReader fR2 = FR;
				double num4 = FR.MinZoom = 500.0;
				double num7 = fR.Zoom = (fR2.MaxZoom = num4);
				int num8 = lastPageMode;
				if (num8.Equals(1))
				{
					FR.Document = _FlowDocument;
				}
				else if (num8.Equals(2))
				{
					FR.Document = _FlowDocumentDouble;
				}
				GC.Collect();
			}
			return true;
		}

		private void setFlowDocumentData(string PageIndexShowed, string leftPagePath, string rightPagePath, List<ThumbnailImageAndPage> thumbnailImageAndPage, List<ImageStatus> imgStatus, FlowDocument flowDocumentImported)
		{
			ThumbnailImageAndPage item = new ThumbnailImageAndPage(PageIndexShowed, rightPagePath, leftPagePath, false);
			thumbnailImageAndPage.Add(item);
			imgStatus.Add(ImageStatus.SMALLIMAGE);
			System.Windows.Controls.Image thumbnailImageToReplace = getThumbnailImageToReplace(leftPagePath, new Thickness(offsetOfImage));
			System.Windows.Controls.Image image = null;
			if (!rightPagePath.Equals(""))
			{
				image = getThumbnailImageToReplace(rightPagePath, new Thickness(offsetOfImage));
				if (image.Source.Width > image.Source.Height)
				{
					double num = (SystemParameters.PrimaryScreenWidth - 16.0) / 2.0;
					double num2 = num / image.Source.Width;
					double num3 = image.Source.Height * num2;
					double num4 = SystemParameters.PrimaryScreenHeight - 110.0;
					double num5 = Math.Abs(num3 - num4) / 2.0 / num2 / 2.0;
					image.Margin = new Thickness(0.0, num5, 0.0, num5);
				}
				if (thumbnailImageToReplace.Source.Width > thumbnailImageToReplace.Source.Height)
				{
					double num6 = (SystemParameters.PrimaryScreenWidth - 16.0) / 2.0;
					double num7 = num6 / thumbnailImageToReplace.Source.Width;
					double num8 = thumbnailImageToReplace.Source.Height * num7;
					double num9 = SystemParameters.PrimaryScreenHeight - 110.0;
					double num10 = Math.Abs(num8 - num9) / 2.0 / num7 / 2.0;
					thumbnailImageToReplace.Margin = new Thickness(0.0, num10, 0.0, num10);
				}
			}
			else if (SystemParameters.PrimaryScreenWidth <= 1280.0)
			{
				double primaryScreenHeight = SystemParameters.PrimaryScreenHeight;
				double num11 = 768.0;
			}
			if (SystemParameters.PrimaryScreenWidth <= 1280.0 && SystemParameters.PrimaryScreenHeight <= 1024.0)
			{
				try
				{
					bool flag = false;
					if (image != null && image.Source.Width > image.Source.Height)
					{
						flag = true;
					}
					if (thumbnailImageToReplace != null && thumbnailImageToReplace.Source.Width > thumbnailImageToReplace.Source.Height)
					{
						flag = true;
					}
					if (flag)
					{
						switch ((int)SystemParameters.PrimaryScreenHeight)
						{
						case 800:
							PDFdpi = 250;
							break;
						case 960:
							PDFdpi = 250;
							break;
						case 1024:
							PDFdpi = 192;
							break;
						default:
							PDFdpi = 250;
							break;
						}
					}
				}
				catch (Exception ex)
				{
					LogTool.Debug(ex);
				}
			}
			StackPanel uiElement = setStackPanelWithThumbnailImage(thumbnailImageToReplace, image);
			BlockUIContainer item2 = new BlockUIContainer(uiElement);
			flowDocumentImported.Blocks.Add(item2);
			item2 = null;
			thumbnailImageToReplace = null;
			image = null;
			uiElement = null;
			item = null;
		}

		private StackPanel setStackPanelWithThumbnailImage(System.Windows.Controls.Image leftThumbNailImage, System.Windows.Controls.Image rightThumbNailImage)
		{
			StackPanel stackPanel = new StackPanel();
			stackPanel.Children.Add(leftThumbNailImage);
			if (rightThumbNailImage != null)
			{
				stackPanel.Children.Add(rightThumbNailImage);
				if (hejMetadata.direction.Equals("right"))
				{
					stackPanel.FlowDirection = FlowDirection.LeftToRight;
				}
			}
			stackPanel.Orientation = Orientation.Horizontal;
			stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
			stackPanel.VerticalAlignment = VerticalAlignment.Center;
			stackPanel.RenderTransform = tfgForImage;
			stackPanel.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
			stackPanel.MouseLeftButtonDown += new MouseButtonEventHandler(ImageInReader_MouseLeftButtonDown);
			return stackPanel;
		}

		private void SendImageSourceToZoomCanvas(BitmapImage newImage)
		{
			newImageWidth = newImage.Width;
			newImageHeight = newImage.Height;
			Canvas canvas = FindVisualChildByName<Canvas>(FR, "zoomCanvas");
			canvas.RenderTransform = tfgForHyperLink;
			if (bookType.Equals(BookType.HEJ))
			{
				double height = 0.0;
				double width = 0.0;
				if (newImage.Width / 2.0 < newImage.Height)
				{
					Border borderInReader = GetBorderInReader();
					height = borderInReader.ActualHeight;
					width = 0.0;
					width = (double)newImage.PixelWidth * borderInReader.ActualHeight / (double)newImage.PixelHeight;
				}
				else if (newImage.Width / 2.0 > newImage.Height)
				{
					Border borderInReader2 = GetBorderInReader();
					height = 0.0;
					width = borderInReader2.ActualWidth;
					height = (double)newImage.PixelHeight * borderInReader2.ActualWidth / (double)newImage.PixelWidth;
				}
				canvas.Height = height;
				canvas.Width = width;
			}
			else if (bookType.Equals(BookType.PHEJ))
			{
				if (zoomStep == 0)
				{
					canvas.Height = (double)newImage.PixelHeight / zoomStepScale[zoomStep] * 96.0 / DpiY;
					canvas.Width = (double)newImage.PixelWidth / zoomStepScale[zoomStep] * 96.0 / DpiX;
				}
				else if (useOriginalCanvasOnLockStatus)
				{
					canvas.Height = (double)(newImage.PixelHeight * 96) / DpiY;
					canvas.Width = (double)(newImage.PixelWidth * 96) / DpiX;
				}
				else
				{
					canvas.Height = (double)newImage.PixelHeight / zoomStepScale[zoomStep] * 96.0 / DpiY;
					canvas.Width = (double)newImage.PixelWidth / zoomStepScale[zoomStep] * 96.0 / DpiX;
				}
				if (SystemParameters.PrimaryScreenWidth <= 1280.0 && SystemParameters.PrimaryScreenHeight <= 1024.0 && newImage.Width > newImage.Height)
				{
					IsSmallDisplaySize = true;
					canvas.SnapsToDevicePixels = true;
					canvas.Height = SystemParameters.PrimaryScreenHeight;
					canvas.Width = SystemParameters.PrimaryScreenWidth;
				}
			}
			if (SystemParameters.PrimaryScreenHeight <= 768.0)
			{
				double primaryScreenWidth = SystemParameters.PrimaryScreenWidth;
				double num = 1024.0;
			}
			ImageBrush imageBrush = new ImageBrush();
			imageBrush.ImageSource = newImage;
			imageBrush.AlignmentX = AlignmentX.Center;
			imageBrush.AlignmentY = AlignmentY.Center;
			imageBrush.Stretch = Stretch.Uniform;
			if (SystemParameters.PrimaryScreenWidth <= 1280.0 && SystemParameters.PrimaryScreenHeight <= 1024.0)
			{
				double width2 = newImage.Width;
				double height2 = newImage.Height;
			}
			imageBrush.Freeze();
			canvas.Background = imageBrush;
		}

		private System.Windows.Controls.Image getThumbnailImageToReplace(string pagePath, Thickness margin)
		{
			System.Windows.Controls.Image image = new System.Windows.Controls.Image();
			BitmapImage bitmapImage = (BitmapImage)(image.Source = new BitmapImage(new Uri(pagePath)));
			image.Stretch = Stretch.Uniform;
			image.Margin = margin;
			bitmapImage = null;
			return image;
		}

		private byte[] getByteArrayFromImage(BitmapImage imageC)
		{
			JpegBitmapEncoder jpegBitmapEncoder = new JpegBitmapEncoder();
			if (imageC.UriSource != null)
			{
				jpegBitmapEncoder.Frames.Add(BitmapFrame.Create(imageC.UriSource));
			}
			else
			{
				jpegBitmapEncoder.Frames.Add(BitmapFrame.Create(imageC));
			}
			using (MemoryStream memoryStream = new MemoryStream())
			{
				jpegBitmapEncoder.Save(memoryStream);
				byte[] result = memoryStream.ToArray();
				memoryStream.Close();
				memoryStream.Dispose();
				jpegBitmapEncoder = null;
				imageC = null;
				GC.Collect();
				return result;
			}
		}

		private void thumbNailListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				firstIndex++;
				if (firstIndex > 1 && (!isSyncing || isSyncOwner))
				{
					RadioButton radioButton = FindVisualChildByName<RadioButton>(FR, "NoteButton");
					if (radioButton != null)
					{
						doUpperRadioButtonClicked(MediaCanvasOpenedBy.NoteButton, radioButton);
						Canvas mediaTableCanvasInReader = GetMediaTableCanvasInReader();
						mediaTableCanvasInReader.Visibility = Visibility.Collapsed;
					}
				}
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
			if (thumbNailListBox.SelectedIndex.Equals(-1))
			{
				return;
			}
			int num = 0;
			if (NoteButtonInLBIsClicked || BookMarkInLBIsClicked)
			{
				thumbNailListBox.Focus();
				object selectedItem = thumbNailListBox.SelectedItem;
				num = singleThumbnailImageAndPageList.IndexOf((ThumbnailImageAndPage)thumbNailListBox.SelectedItem);
			}
			else
			{
				num = thumbNailListBox.SelectedIndex;
			}
			if (viewStatusIndex.Equals(PageMode.SinglePage))
			{
				bringBlockIntoView(num);
			}
			else if (viewStatusIndex.Equals(PageMode.DoublePage))
			{
				int num2 = num;
				if (num2 % 2 == 1)
				{
					num2++;
				}
				hejMetadata.direction.Equals("right");
				bringBlockIntoView(num2 / 2);
			}
			if (isFirstTimeLoaded)
			{
				isLockButtonLocked = true;
				if (!isLockButtonLocked)
				{
					zoomStep = 0;
					PDFScale = (float)zoomStepScale[0];
					resetTransform();
					LockButton.Visibility = Visibility.Collapsed;
				}
				else
				{
					if (num.Equals(0) || num.Equals(thumbNailListBox.Items.Count - 1))
					{
						setTransformBetweenSingleAndDoublePage();
					}
					LockButton.Visibility = Visibility.Visible;
				}
			}
			if (thumbNailListBoxOpenedFullScreen)
			{
				thumnailCanvas.Visibility = Visibility.Hidden;
				BindingOperations.ClearBinding(thumnailCanvas, FrameworkElement.HeightProperty);
				BindingOperations.ClearBinding(thumbNailListBox, FrameworkElement.HeightProperty);
				RadioButton radioButton2 = FindVisualChildByName<RadioButton>(FR, "ShowAllImageButton");
				radioButton2.IsChecked = false;
				ShowListBoxButton.Visibility = Visibility.Visible;
			}
			ListBoxItem listBoxItem = (ListBoxItem)thumbNailListBox.ItemContainerGenerator.ContainerFromItem(thumbNailListBox.SelectedItem);
			if (listBoxItem != null)
			{
				listBoxItem.Focus();
				if (!thumbNailListBoxOpenedFullScreen)
				{
					if (hejMetadata.direction.Equals("right"))
					{
						ScrollViewer scrollViewer = FindVisualChildByName<ScrollViewer>(thumbNailListBox, "SVInLV");
						scrollViewer.ScrollToRightEnd();
						if ((double)(num + 1) * listBoxItem.ActualWidth > base.ActualWidth / 2.0)
						{
							double offset = scrollViewer.ScrollableWidth - (double)(num + 1) * listBoxItem.ActualWidth + base.ActualWidth / 2.0;
							scrollViewer.ScrollToHorizontalOffset(offset);
						}
					}
					else if ((double)(num + 1) * listBoxItem.ActualWidth > base.ActualWidth / 2.0)
					{
						ScrollViewer scrollViewer2 = FindVisualChildByName<ScrollViewer>(thumbNailListBox, "SVInLV");
						double offset2 = (double)(num + 1) * listBoxItem.ActualWidth - base.ActualWidth / 2.0;
						scrollViewer2.ScrollToHorizontalOffset(offset2);
					}
				}
				thumbNailListBox.SelectedIndex = -1;
				resetFocusBackToReader();
			}
			ShowFilterCount();
		}

		private void checkImageStatus(object sender, EventArgs e)
		{
			bool flag = false;
			bool flag2 = false;
			if (viewStatusIndex.Equals(PageMode.SinglePage))
			{
				flag = true;
			}
			else
			{
				if (!viewStatusIndex.Equals(PageMode.DoublePage))
				{
					return;
				}
				flag2 = true;
			}
			try
			{
				double totalMilliseconds = DateTime.Now.Subtract(lastTimeOfChangingPage).TotalMilliseconds;
				if (totalMilliseconds >= 300.0)
				{
					if (checkImageStatusTimer.Interval != checkInterval)
					{
						checkImageStatusTimer.Interval = checkInterval;
					}
					if (!zoomeThread.Count.Equals(0) && !isPDFRendering)
					{
						for (int num = zoomeThread.Count - 1; num >= 0; num--)
						{
							if (PDFScale.Equals((float)Convert.ToDouble(zoomeThread[num].Name)))
							{
								try
								{
									if (flag)
									{
										singleImgStatus[curPageIndex] = ImageStatus.LARGEIMAGE;
									}
									else if (flag2)
									{
										doubleImgStatus[curPageIndex] = ImageStatus.LARGEIMAGE;
									}
									zoomeThread[num].Start();
									imageSourceRendered += new EventHandler<imageSourceRenderedResultEventArgs>(ReadWindow_imageSourceRendered);
									isPDFRendering = true;
									return;
								}
								catch
								{
								}
							}
						}
					}
					if (flag2)
					{
						try
						{
							if (doubleImgStatus[curPageIndex] != ImageStatus.LARGEIMAGE || !isAreaButtonAndPenMemoRequestSent)
							{
								ReadPagePair readPagePair = doubleReadPagePair[curPageIndex];
								if (readPagePair.rightPageIndex == -1)
								{
									if (File.Exists(readPagePair.leftImagePath))
									{
										resetDoublePage();
										isAreaButtonAndPenMemoRequestSent = true;
									}
								}
								else if (File.Exists(readPagePair.leftImagePath) && File.Exists(readPagePair.rightImagePath))
								{
									resetDoublePage();
									isAreaButtonAndPenMemoRequestSent = true;
								}
							}
						}
						catch (Exception)
						{
						}
					}
					else if (flag)
					{
						try
						{
							if (singleImgStatus[curPageIndex] == ImageStatus.LARGEIMAGE && isAreaButtonAndPenMemoRequestSent)
							{
								checkImageStatusRetryTimes++;
							}
							else
							{
								ReadPagePair readPagePair2 = singleReadPagePair[curPageIndex];
								if (File.Exists(readPagePair2.leftImagePath))
								{
									resetSinglePage();
									isAreaButtonAndPenMemoRequestSent = true;
								}
							}
						}
						catch (Exception)
						{
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private void resetSinglePage()
		{
			int num = curPageIndex;
			List<int> list = new List<int>();
			if (singleImgStatus[num] != ImageStatus.LARGEIMAGE && singleImgStatus[num] != ImageStatus.GENERATING)
			{
				list.Add(num);
			}
			if (needPreload)
			{
				if (num + 1 < singleReadPagePair.Count && singleImgStatus[num + 1] != ImageStatus.LARGEIMAGE && singleImgStatus[num + 1] != ImageStatus.GENERATING)
				{
					list.Add(num + 1);
				}
				if (num - 1 > 0 && singleImgStatus[num - 1] != ImageStatus.LARGEIMAGE && singleImgStatus[num - 1] != ImageStatus.GENERATING)
				{
					list.Add(num - 1);
				}
			}
			for (int i = 0; i < list.Count; i++)
			{
				if (num != curPageIndex)
				{
					return;
				}
				ReadPagePair readPagePair = singleReadPagePair[list[i]];
				if (readPagePair.leftImageSource == null && readPagePair.leftImagePath != "")
				{
					if (num != curPageIndex)
					{
						return;
					}
					try
					{
						if (readPagePair.leftImagePath.Contains("tryPageEnd") || readPagePair.rightImagePath.Contains("tryPageEnd"))
						{
							singleImgStatus[list[i]] = ImageStatus.LARGEIMAGE;
							return;
						}
						if (bookType.Equals(BookType.PHEJ))
						{
							readPagePair.createLargePHEJBitmapImage(caTool, defaultKey, GetBorderInReader(), true);
						}
						else if (bookType.Equals(BookType.HEJ))
						{
							readPagePair.createLargeHEJBitmapImage(caTool, defaultKey);
						}
						singleImgStatus[list[i]] = ImageStatus.GENERATING;
						if (File.Exists(readPagePair.leftImagePath) && !readPagePair.isRendering)
						{
							if (bookType.Equals(BookType.PHEJ))
							{
								readPagePair.createLargePHEJBitmapImage(caTool, defaultKey, GetBorderInReader(), true);
							}
							else if (bookType.Equals(BookType.HEJ))
							{
								readPagePair.createLargeHEJBitmapImage(caTool, defaultKey);
							}
							singleImgStatus[list[i]] = ImageStatus.GENERATING;
						}
					}
					catch (Exception)
					{
						return;
					}
				}
			}
			int count = singleReadPagePair.Count;
			for (int j = 0; j < count; j++)
			{
				if (num != curPageIndex)
				{
					return;
				}
				if (needPreload)
				{
					if (Math.Abs(num - j) <= 1)
					{
						continue;
					}
				}
				else if (num == j)
				{
					continue;
				}
				ReadPagePair readPagePair2 = singleReadPagePair[j];
				if ((singleImgStatus[j] == ImageStatus.GENERATING || singleImgStatus[j] == ImageStatus.LARGEIMAGE) && readPagePair2.leftImageSource != null)
				{
					readPagePair2.leftImageSource = null;
					readPagePair2.decodedPDFPages = new byte[2][];
					singleImgStatus[j] = ImageStatus.SMALLIMAGE;
				}
			}
			if (num != curPageIndex)
			{
				return;
			}
			ReadPagePair readPagePair3 = singleReadPagePair[curPageIndex];
			Canvas canvas = FindVisualChildByName<Canvas>(FR, "zoomCanvas");
			if (num != curPageIndex)
			{
				return;
			}
			if (readPagePair3.leftImageSource != null && !readPagePair3.isRendering)
			{
				try
				{
					baseScale = readPagePair3.baseScale;
					if (canvas.Background == null)
					{
						SendImageSourceToZoomCanvas((BitmapImage)readPagePair3.leftImageSource);
						singleImgStatus[curPageIndex] = ImageStatus.LARGEIMAGE;
					}
				}
				catch (Exception)
				{
					readPagePair3.leftImageSource = null;
					return;
				}
				if (canvas.Background != null)
				{
					if (canAreaButtonBeSeen)
					{
						CheckAndProduceAreaButton(readPagePair3.leftPageIndex, -1, defaultKey, canvas);
					}
					loadCurrentStrokes(hejMetadata.LImgList[readPagePair3.leftPageIndex].pageId);
					loadCurrentStrokes(singleReadPagePair[curPageIndex].leftPageIndex);
					GC.Collect();
				}
			}
			else
			{
				try
				{
					loadCurrentStrokes(singleReadPagePair[curPageIndex].leftPageIndex);
				}
				catch (Exception ex3)
				{
					LogTool.Debug(ex3);
				}
			}
		}

		private void resetDoublePage()
		{
			int num = curPageIndex;
			List<int> list = new List<int>();
			if (doubleImgStatus[num] != ImageStatus.LARGEIMAGE && doubleImgStatus[num] != ImageStatus.GENERATING)
			{
				list.Add(num);
			}
			if (needPreload)
			{
				if (num + 1 < doubleReadPagePair.Count && doubleImgStatus[num + 1] != ImageStatus.LARGEIMAGE && doubleImgStatus[num + 1] != ImageStatus.GENERATING)
				{
					list.Add(num + 1);
				}
				if (num - 1 > 0 && doubleImgStatus[num - 1] != ImageStatus.LARGEIMAGE && doubleImgStatus[num - 1] != ImageStatus.GENERATING)
				{
					list.Add(num - 1);
				}
			}
			for (int i = 0; i < list.Count; i++)
			{
				if (num != curPageIndex)
				{
					return;
				}
				ReadPagePair readPagePair = doubleReadPagePair[list[i]];
				if (readPagePair.leftImageSource == null && readPagePair.leftImagePath != "")
				{
					if (num != curPageIndex)
					{
						return;
					}
					try
					{
						if (readPagePair.leftImagePath.Contains("tryPageEnd") || readPagePair.rightImagePath.Contains("tryPageEnd"))
						{
							doubleImgStatus[list[i]] = ImageStatus.LARGEIMAGE;
							return;
						}
						if (File.Exists(readPagePair.leftImagePath) && !readPagePair.isRendering)
						{
							if (bookType.Equals(BookType.PHEJ))
							{
								readPagePair.createLargePHEJBitmapImage(caTool, defaultKey, GetBorderInReader(), false);
							}
							else if (bookType.Equals(BookType.HEJ))
							{
								readPagePair.createLargeHEJBitmapImage(caTool, defaultKey);
							}
							doubleImgStatus[list[i]] = ImageStatus.GENERATING;
						}
					}
					catch (Exception)
					{
						return;
					}
				}
			}
			int count = doubleReadPagePair.Count;
			for (int j = 0; j < count; j++)
			{
				if (num != curPageIndex)
				{
					return;
				}
				if (needPreload)
				{
					if (Math.Abs(num - j) <= 1)
					{
						continue;
					}
				}
				else if (num == j)
				{
					continue;
				}
				ReadPagePair readPagePair2 = doubleReadPagePair[j];
				if (doubleImgStatus[j] == ImageStatus.GENERATING || doubleImgStatus[j] == ImageStatus.LARGEIMAGE)
				{
					readPagePair2.leftImageSource = null;
					readPagePair2.decodedPDFPages = new byte[2][];
					doubleImgStatus[j] = ImageStatus.SMALLIMAGE;
				}
			}
			if (num != curPageIndex)
			{
				return;
			}
			ReadPagePair readPagePair3 = doubleReadPagePair[curPageIndex];
			Canvas canvas = FindVisualChildByName<Canvas>(FR, "zoomCanvas");
			if (num != curPageIndex || readPagePair3.leftImageSource == null || readPagePair3.isRendering)
			{
				return;
			}
			try
			{
				baseScale = readPagePair3.baseScale;
				if (canvas.Background == null)
				{
					SendImageSourceToZoomCanvas((BitmapImage)readPagePair3.leftImageSource);
					doubleImgStatus[curPageIndex] = ImageStatus.LARGEIMAGE;
				}
			}
			catch (Exception)
			{
				readPagePair3.leftImageSource = null;
				return;
			}
			if (canvas.Background == null)
			{
				return;
			}
			if (readPagePair3.rightPageIndex == -1)
			{
				if (canAreaButtonBeSeen)
				{
					CheckAndProduceAreaButton(readPagePair3.leftPageIndex, -1, defaultKey, canvas);
				}
				loadCurrentStrokes(hejMetadata.LImgList[readPagePair3.leftPageIndex].pageId);
				loadCurrentStrokes(singleReadPagePair[curPageIndex].leftPageIndex);
			}
			else
			{
				if (canAreaButtonBeSeen)
				{
					CheckAndProduceAreaButton(readPagePair3.leftPageIndex, readPagePair3.rightPageIndex, defaultKey, canvas);
				}
				loadDoublePagesStrokes(hejMetadata.LImgList[readPagePair3.leftPageIndex].pageId, hejMetadata.LImgList[readPagePair3.rightPageIndex].pageId);
				loadDoublePagesStrokes(readPagePair3.leftPageIndex, readPagePair3.rightPageIndex);
			}
			GC.Collect();
		}

		private void checkOtherDevicePage()
		{
			if (ifAskedJumpPage)
			{
				return;
			}
			if (lastViewPage.Count > 0)
			{
				LastPageData lastPageData = null;
				if (lastViewPage.ContainsKey(CName))
				{
					lastPageData = lastViewPage[CName];
				}
				foreach (KeyValuePair<string, LastPageData> item in lastViewPage)
				{
					if (item.Key != CName)
					{
						LastPageData lastPageData2 = lastViewPage[item.Key];
						bool flag = false;
						if (lastPageData != null && lastPageData.updatetime < lastPageData2.updatetime)
						{
							flag = true;
						}
						else if (lastPageData == null)
						{
							flag = true;
						}
						if (flag)
						{
							string messageBoxText = string.Format("您最近一次於 {0} 閱讀到第 {1} 頁。是否要跳到該頁？", item.Key, item.Value.index + 1);
							if (lastPageData2.index == curPageIndex)
							{
								ifAskedJumpPage = true;
								return;
							}
							MessageBoxResult messageBoxResult = MessageBox.Show(messageBoxText, "", MessageBoxButton.YesNo);
							if (messageBoxResult.Equals(MessageBoxResult.Yes))
							{
								_003C_003Ec__DisplayClass9 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass9();
								_003C_003Ec__DisplayClass.targetPageIndex = -1;
								if (lastPageData2.index > 0)
								{
									_003C_003Ec__DisplayClassb _003C_003Ec__DisplayClassb = new _003C_003Ec__DisplayClassb();
									_003C_003Ec__DisplayClassb.CS_0024_003C_003E8__localsa = _003C_003Ec__DisplayClass;
									_003C_003Ec__DisplayClassb._003C_003E4__this = this;
									_003C_003Ec__DisplayClass.targetPageIndex = lastPageData2.index;
									LoadingEvent = new LoadingPageHandler(testLoading);
									_003C_003Ec__DisplayClassb.result = null;
									AsyncCallback callback = new AsyncCallback(_003C_003Ec__DisplayClassb._003CcheckOtherDevicePage_003Eb__7);
									_003C_003Ec__DisplayClassb.result = LoadingEvent.BeginInvoke(_003C_003Ec__DisplayClass.targetPageIndex, callback, null);
								}
								break;
							}
						}
					}
				}
			}
			ifAskedJumpPage = true;
		}

		private void testLoading(int Pageindex)
		{
		}

		private void showLastReadPageAndStartPreload()
		{
			if (viewStatusIndex.Equals(PageMode.SinglePage))
			{
				resetSinglePage();
			}
			else if (viewStatusIndex.Equals(PageMode.DoublePage))
			{
				resetDoublePage();
			}
			if (!isWindowsXP)
			{
				needPreload = true;
			}
			if (!checkImageStatusTimer.IsEnabled)
			{
				checkImageStatusTimer.IsEnabled = true;
				checkImageStatusTimer.Start();
			}
		}

		private void FR_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			if (e.OldFocus is FlowDocumentReader)
			{
				pageViewerPager = e.NewFocus;
				FR.PreviewLostKeyboardFocus -= new KeyboardFocusChangedEventHandler(FR_PreviewLostKeyboardFocus);
				e.Handled = true;
			}
		}

		private void saveOriginalStrokeStatus(double originalCanvasWidth, double originalCanvasHeight)
		{
			try
			{
				if (!File.Exists(bookPath + "\\hyweb\\strokes\\" + StatusFileName))
				{
					FileStream fileStream = new FileStream(bookPath + "\\hyweb\\strokes\\" + StatusFileName, FileMode.Create);
					XmlWriter xmlWriter = XmlWriter.Create(fileStream);
					xmlWriter.WriteStartDocument();
					xmlWriter.WriteStartElement("status");
					xmlWriter.WriteElementString("originalCanvasWidth", originalCanvasWidth.ToString());
					xmlWriter.WriteElementString("originalCanvasHeight", originalCanvasHeight.ToString());
					xmlWriter.WriteEndElement();
					xmlWriter.WriteEndDocument();
					xmlWriter.Flush();
					fileStream.Close();
				}
			}
			catch
			{
			}
		}

		private void loadOriginalStrokeStatus()
		{
			try
			{
				if (File.Exists(bookPath + "\\hyweb\\strokes\\" + StatusFileName))
				{
					FileStream input = new FileStream(bookPath + "\\hyweb\\strokes\\" + StatusFileName, FileMode.Open);
					XmlReader reader = XmlReader.Create(input);
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(reader);
					foreach (XmlNode childNode in xmlDocument.ChildNodes)
					{
						if (childNode.Name.Equals("status"))
						{
							foreach (XmlNode childNode2 in childNode.ChildNodes)
							{
								if (childNode2.Name.Equals("originalCanvasWidth"))
								{
									baseStrokesCanvasWidth = Convert.ToDouble(childNode2.InnerText);
								}
								else if (childNode2.Name.Equals("originalCanvasHeight"))
								{
									baseStrokesCanvasHeight = Convert.ToDouble(childNode2.InnerText);
								}
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		private void loadDoublePagesStrokes(string LeftImgID, string RightImgID)
		{
			Canvas canvas = FindVisualChildByName<Canvas>(FR, "zoomCanvas");
			if (canvas.Width.Equals(double.NaN) || canvas.Height.Equals(double.NaN))
			{
				return;
			}
			double offsetX = canvas.Width / 2.0;
			StrokeCollection strokeCollection = new StrokeCollection();
			StrokeCollection strokeCollection2 = new StrokeCollection();
			StrokeCollection strokeCollection3 = new StrokeCollection();
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
			if (!isFullScreenButtonClick)
			{
				originalCanvasWidth = canvas.Width;
				originalCanvasHeight = canvas.Height;
			}
			else
			{
				fullScreenCanvasWidth = canvas.Width;
				fullScreenCanvasHeight = canvas.Height;
			}
			inkCanvas.Width = canvas.Width;
			inkCanvas.Height = canvas.Height;
			inkCanvas.RenderTransform = tfgForHyperLink;
			if (File.Exists(bookPath + "/hyweb/strokes/" + LeftImgID + ".isf"))
			{
				FileStream fileStream = new FileStream(bookPath + "/hyweb/strokes/" + LeftImgID + ".isf", FileMode.Open);
				if (fileStream.Length > 0)
				{
					strokeCollection2 = new StrokeCollection(fileStream);
				}
				fileStream.Close();
			}
			if (File.Exists(bookPath + "/hyweb/strokes/" + RightImgID + ".isf"))
			{
				FileStream fileStream2 = new FileStream(bookPath + "/hyweb/strokes/" + RightImgID + ".isf", FileMode.Open);
				strokeCollection3 = new StrokeCollection(fileStream2);
				fileStream2.Close();
			}
			if (strokeCollection2.Count > 0)
			{
				System.Windows.Media.Matrix transformMatrix = new System.Windows.Media.Matrix(1.0, 0.0, 0.0, 1.0, 0.0, 0.0);
				if (!baseStrokesCanvasHeight.Equals(0.0) && !baseStrokesCanvasWidth.Equals(0.0) && (originalCanvasHeight != baseStrokesCanvasHeight || originalCanvasWidth / 2.0 != baseStrokesCanvasWidth))
				{
					double scaleX = originalCanvasWidth / 2.0 / baseStrokesCanvasWidth;
					double scaleY = originalCanvasHeight / baseStrokesCanvasHeight;
					transformMatrix.Scale(scaleX, scaleY);
				}
				if (isFullScreenButtonClick)
				{
					double scaleX2 = fullScreenCanvasWidth / 2.0 / (originalCanvasWidth / 2.0);
					double scaleY2 = fullScreenCanvasHeight / originalCanvasHeight;
					transformMatrix.Scale(scaleX2, scaleY2);
				}
				strokeCollection2.Transform(transformMatrix, false);
				strokeCollection.Add(strokeCollection2);
			}
			if (strokeCollection3.Count > 0)
			{
				System.Windows.Media.Matrix transformMatrix2 = new System.Windows.Media.Matrix(1.0, 0.0, 0.0, 1.0, offsetX, 0.0);
				if (!baseStrokesCanvasHeight.Equals(0.0) && !baseStrokesCanvasWidth.Equals(0.0) && (originalCanvasHeight != baseStrokesCanvasHeight || originalCanvasWidth / 2.0 != baseStrokesCanvasWidth))
				{
					double num = originalCanvasWidth / 2.0 / baseStrokesCanvasWidth;
					double scaleY3 = originalCanvasHeight / baseStrokesCanvasHeight;
					transformMatrix2.OffsetX /= num;
					transformMatrix2.Scale(num, scaleY3);
				}
				if (isFullScreenButtonClick)
				{
					double num2 = fullScreenCanvasWidth / 2.0 / (originalCanvasWidth / 2.0);
					double scaleY4 = fullScreenCanvasHeight / originalCanvasHeight;
					transformMatrix2.OffsetX /= num2;
					transformMatrix2.Scale(num2, scaleY4);
				}
				strokeCollection3.Transform(transformMatrix2, false);
				strokeCollection.Add(strokeCollection3);
			}
			inkCanvas.Strokes = strokeCollection;
		}

		private void loadCurrentStrokes(string imageID)
		{
			Canvas canvas = FindVisualChildByName<Canvas>(FR, "zoomCanvas");
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
			if (canvas.Width.Equals(double.NaN) || canvas.Height.Equals(double.NaN))
			{
				return;
			}
			if (!isFullScreenButtonClick)
			{
				originalCanvasWidth = canvas.Width;
				originalCanvasHeight = canvas.Height;
			}
			else
			{
				fullScreenCanvasWidth = canvas.Width;
				fullScreenCanvasHeight = canvas.Height;
			}
			inkCanvas.Width = canvas.Width;
			inkCanvas.Height = canvas.Height;
			inkCanvas.RenderTransform = tfgForHyperLink;
			StrokeCollection strokeCollection = new StrokeCollection();
			if (File.Exists(bookPath + "\\hyweb\\strokes\\" + imageID + ".isf"))
			{
				FileStream fileStream = new FileStream(bookPath + "\\hyweb\\strokes\\" + imageID + ".isf", FileMode.Open);
				if (fileStream.Length > 0)
				{
					strokeCollection = new StrokeCollection(fileStream);
				}
				fileStream.Close();
			}
			if (strokeCollection.Count > 0)
			{
				System.Windows.Media.Matrix transformMatrix = new System.Windows.Media.Matrix(1.0, 0.0, 0.0, 1.0, 0.0, 0.0);
				if (!baseStrokesCanvasHeight.Equals(0.0) && !baseStrokesCanvasWidth.Equals(0.0) && (originalCanvasHeight != baseStrokesCanvasHeight || originalCanvasWidth != baseStrokesCanvasWidth))
				{
					double scaleX = originalCanvasWidth / baseStrokesCanvasWidth;
					double scaleY = originalCanvasHeight / baseStrokesCanvasHeight;
					transformMatrix.Scale(scaleX, scaleY);
				}
				if (isFullScreenButtonClick)
				{
					double scaleX2 = fullScreenCanvasWidth / originalCanvasWidth;
					double scaleY2 = fullScreenCanvasHeight / originalCanvasHeight;
					transformMatrix.Scale(scaleX2, scaleY2);
				}
				strokeCollection.Transform(transformMatrix, false);
				inkCanvas.Strokes = strokeCollection;
			}
		}

		private void loadDoublePagesStrokes(int leftIndex, int rightIndex)
		{
			Canvas canvas = FindVisualChildByName<Canvas>(FR, "zoomCanvas");
			if (canvas.Width.Equals(double.NaN) || canvas.Height.Equals(double.NaN))
			{
				return;
			}
			double offsetX = canvas.Width / 2.0;
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
			inkCanvas.Width = canvas.Width;
			inkCanvas.Height = canvas.Height;
			inkCanvas.RenderTransform = tfgForHyperLink;
			bookStrokesDictionary = bookManager.getStrokesDics(userBookSno);
			if (bookStrokesDictionary.ContainsKey(leftIndex))
			{
				List<StrokesData> list = bookStrokesDictionary[leftIndex];
				int count = list.Count;
				for (int i = 0; i < count; i++)
				{
					if (list[i].status == "0")
					{
						paintStrokeOnInkCanvas(list[i], canvas.Width / 2.0, canvas.Height, 0.0, 0.0);
					}
				}
			}
			if (!bookStrokesDictionary.ContainsKey(rightIndex))
			{
				return;
			}
			List<StrokesData> list2 = bookStrokesDictionary[rightIndex];
			int count2 = list2.Count;
			for (int j = 0; j < count2; j++)
			{
				if (list2[j].status == "0")
				{
					paintStrokeOnInkCanvas(list2[j], canvas.Width / 2.0, canvas.Height, offsetX, 0.0);
				}
			}
		}

		private void paintStrokeOnInkCanvas(StrokesData strokeJson, double currentInkcanvasWidth, double currentInkcanvasHeight, double offsetX, double offsetY)
		{
			try
			{
				double num = strokeJson.width;
				double num2 = strokeJson.canvasheight;
				double num3 = strokeJson.canvaswidth;
				double num4 = strokeJson.alpha;
				string color = strokeJson.color;
				double num5 = currentInkcanvasWidth / num3;
				double num6 = currentInkcanvasHeight / num2;
				string[] array = strokeJson.points.Split(';');
				char[] trimChars = new char[2]
				{
					'{',
					'}'
				};
				StylusPointCollection stylusPointCollection = new StylusPointCollection();
				for (int i = 0; i < array.Length; i++)
				{
					System.Windows.Point point = default(System.Windows.Point);
					string text = array[i];
					text = text.TrimEnd(trimChars);
					text = text.TrimStart(trimChars);
					point = System.Windows.Point.Parse(text);
					StylusPoint item = default(StylusPoint);
					item.X = point.X * num5;
					item.Y = point.Y * num6;
					stylusPointCollection.Add(item);
				}
				if (stylusPointCollection.Count < 1)
				{
					StylusPoint item2 = default(StylusPoint);
					item2.X = 0.0;
					item2.Y = 0.0;
					stylusPointCollection.Add(item2);
				}
				Stroke stroke = new Stroke(stylusPointCollection);
				stroke.DrawingAttributes.FitToCurve = true;
				if (num4 != 1.0)
				{
					stroke.DrawingAttributes.IsHighlighter = true;
				}
				else
				{
					stroke.DrawingAttributes.IsHighlighter = false;
				}
				stroke.DrawingAttributes.Width = num * 3.0;
				stroke.DrawingAttributes.Height = num * 3.0;
				new System.Windows.Media.ColorConverter();
				System.Windows.Media.Color color2 = ConvertHexStringToColour(color);
				stroke.DrawingAttributes.Color = color2;
				System.Windows.Media.Matrix transformMatrix = new System.Windows.Media.Matrix(1.0, 0.0, 0.0, 1.0, offsetX, 0.0);
				if (stroke != null)
				{
					InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
					stroke.Transform(transformMatrix, false);
					inkCanvas.Strokes.Add(stroke.Clone());
					stroke = null;
				}
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}

		private void convertCurrentStrokesToDB(string imageID)
		{
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
			if (!File.Exists(bookPath + "\\hyweb\\strokes\\" + imageID + ".isf"))
			{
				return;
			}
			if (inkCanvas.Strokes.Count > 0)
			{
				DateTime value = new DateTime(1970, 1, 1);
				long num = DateTime.Now.ToUniversalTime().Subtract(value).Ticks / 10000000;
				StrokeCollection strokes = inkCanvas.Strokes;
				System.Windows.Media.Matrix transformMatrix = new System.Windows.Media.Matrix(1.0, 0.0, 0.0, 1.0, 0.0, 0.0);
				if (!baseStrokesCanvasHeight.Equals(0.0) && !baseStrokesCanvasWidth.Equals(0.0) && (originalCanvasHeight != baseStrokesCanvasHeight || originalCanvasWidth != baseStrokesCanvasWidth))
				{
					double scaleX = baseStrokesCanvasWidth / originalCanvasWidth;
					double scaleY = baseStrokesCanvasHeight / originalCanvasHeight;
					transformMatrix.Scale(scaleX, scaleY);
				}
				if (isFullScreenButtonClick)
				{
					double scaleX2 = originalCanvasWidth / fullScreenCanvasWidth;
					double scaleY2 = originalCanvasHeight / fullScreenCanvasHeight;
					transformMatrix.Scale(scaleX2, scaleY2);
				}
				strokes.Transform(transformMatrix, false);
				List<string> list = new List<string>();
				int count = inkCanvas.Strokes.Count;
				for (int i = 0; i < count; i++)
				{
					int count2 = inkCanvas.Strokes[i].StylusPoints.Count;
					DrawingAttributes drawingAttributes = inkCanvas.Strokes[i].DrawingAttributes;
					string text = drawingAttributes.Color.ToString();
					text = text.Remove(1, 2);
					string text2 = "";
					for (int j = 0; j < count2; j++)
					{
						StylusPoint stylusPoint = inkCanvas.Strokes[i].StylusPoints[j];
						string text3 = text2;
						text2 = text3 + "{" + stylusPoint.X + ", " + stylusPoint.Y + "};";
					}
					text2 = text2.Substring(0, text2.LastIndexOf(';'));
					StrokesData strokesData = new StrokesData();
					strokesData.objectId = "";
					strokesData.alpha = (float)(drawingAttributes.IsHighlighter ? 0.5 : 1.0);
					strokesData.bookid = "";
					strokesData.canvasheight = (float)inkCanvas.Height;
					strokesData.canvaswidth = (float)inkCanvas.Width;
					strokesData.color = text;
					strokesData.createtime = num;
					strokesData.index = curPageIndex;
					strokesData.points = text2;
					strokesData.status = "0";
					strokesData.synctime = 0L;
					strokesData.updatetime = num;
					strokesData.userid = "";
					strokesData.vendor = "";
					strokesData.width = (float)drawingAttributes.Height;
					string item = bookManager.insertStrokeCmdString(userBookSno, strokesData);
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
				if (list.Count > 0)
				{
					bookManager.saveBatchData(list);
				}
			}
			try
			{
				File.Delete(bookPath + "\\hyweb\\strokes\\" + imageID + ".isf");
			}
			catch (IOException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		private void penMemoCanvas_StrokeErasing(object sender, InkCanvasStrokeErasingEventArgs e)
		{
			Stroke stroke = e.Stroke;
			if (stroke == null)
			{
				return;
			}
			InkCanvas inkCanvas = (InkCanvas)sender;
			List<StrokesData> curPageStrokes = bookManager.getCurPageStrokes(userBookSno, curPageIndex);
			int count = curPageStrokes.Count;
			int num = 0;
			while (true)
			{
				if (num < count)
				{
					if (compareStrokeInDB(stroke, curPageStrokes[num], inkCanvas.Width, inkCanvas.Height))
					{
						break;
					}
					num++;
					continue;
				}
				return;
			}
			DateTime value = new DateTime(1970, 1, 1);
			long updatetime = DateTime.Now.ToUniversalTime().Subtract(value).Ticks / 10000000;
			List<string> list = new List<string>();
			curPageStrokes[num].updatetime = updatetime;
			curPageStrokes[num].status = "1";
			string item = bookManager.deleteStrokeCmdString(userBookSno, curPageStrokes[num]);
			if (!list.Contains(item))
			{
				list.Add(item);
			}
			if (list.Count > 0)
			{
				bookManager.saveBatchData(list);
			}
		}

		private bool compareStrokeInDB(Stroke thisStroke, StrokesData strokeJson, double currentInkcanvasWidth, double currentInkcanvasHeight)
		{
			double num = strokeJson.canvasheight;
			double num2 = strokeJson.canvaswidth;
			double num3 = num2 / currentInkcanvasWidth;
			double num4 = num / currentInkcanvasHeight;
			int count = thisStroke.StylusPoints.Count;
			int num5 = 0;
			string text = strokeJson.points.Replace(" ", "");
			for (int i = 0; i < count; i++)
			{
				StylusPoint stylusPoint = thisStroke.StylusPoints[i];
				string value = "{" + stylusPoint.X * num3 + "," + stylusPoint.Y * num4 + "}";
				if (text.Contains(value))
				{
					num5++;
				}
			}
			double num6 = num5 / count * 100;
			if (num6 == 100.0)
			{
				return true;
			}
			return false;
		}

		private void saveStrokeToDB(Stroke thisStroke)
		{
			lock (this)
			{
				this.i++;
				DateTime value = new DateTime(1970, 1, 1);
				long num = DateTime.Now.ToUniversalTime().Subtract(value).Ticks / 10000000;
				InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
				int count = thisStroke.StylusPoints.Count;
				DrawingAttributes drawingAttributes = thisStroke.DrawingAttributes;
				string text = drawingAttributes.Color.ToString();
				text = text.Remove(1, 2);
				string text2 = "";
				for (int i = 0; i < count; i++)
				{
					StylusPoint stylusPoint = thisStroke.StylusPoints[i];
					string text3 = text2;
					text2 = text3 + "{" + stylusPoint.X + ", " + stylusPoint.Y + "};";
				}
				text2 = text2.Substring(0, text2.LastIndexOf(';'));
				StrokesData strokesData = new StrokesData();
				strokesData.objectId = "";
				strokesData.alpha = (float)(drawingAttributes.IsHighlighter ? 0.5 : 1.0);
				strokesData.bookid = bookId;
				strokesData.canvasheight = (float)inkCanvas.Height;
				strokesData.canvaswidth = (float)inkCanvas.Width;
				strokesData.color = text;
				strokesData.createtime = num + this.i;
				strokesData.index = curPageIndex;
				strokesData.points = text2;
				strokesData.status = "0";
				strokesData.synctime = 0L;
				strokesData.updatetime = num + this.i;
				strokesData.userid = account;
				strokesData.vendor = vendorId;
				strokesData.width = (float)drawingAttributes.Height;
				bookManager.saveStrokesData(userBookSno, false, strokesData);
			}
		}

		public void strokeChaneEventHandler(DrawingAttributes d)
		{
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
			inkCanvas.DefaultDrawingAttributes = d;
		}

		public void strokeUndoEventHandler()
		{
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
			if (inkCanvas.Strokes.Count > 0)
			{
				tempStrokes.Add(inkCanvas.Strokes[inkCanvas.Strokes.Count - 1]);
				inkCanvas.Strokes.RemoveAt(inkCanvas.Strokes.Count - 1);
			}
		}

		public void strokeRedoEventHandler()
		{
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
			while (tempStrokes.Count > 0)
			{
				inkCanvas.Strokes.Add(tempStrokes[tempStrokes.Count - 1]);
				tempStrokes.RemoveAt(tempStrokes.Count - 1);
			}
		}

		public void strokeEraseEventHandler()
		{
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
			inkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
		}

		public void strokeLineEventHandler()
		{
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
			inkCanvas.EditingMode = InkCanvasEditingMode.None;
			inkCanvas.MouseLeftButtonDown += new MouseButtonEventHandler(inkCanvas1_MouseDown);
			inkCanvas.MouseUp += new MouseButtonEventHandler(inkCanvas1_MouseUp);
			inkCanvas.MouseMove += new MouseEventHandler(inkCanvas1_MouseMove);
			isStrokeLine = true;
		}

		public void strokeCurveEventHandler()
		{
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
			inkCanvas.MouseDown -= new MouseButtonEventHandler(inkCanvas1_MouseDown);
			inkCanvas.MouseUp -= new MouseButtonEventHandler(inkCanvas1_MouseUp);
			inkCanvas.MouseMove -= new MouseEventHandler(inkCanvas1_MouseMove);
			inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
			isStrokeLine = false;
		}

		private void inkCanvas1_MouseDown(object sender, MouseButtonEventArgs e)
		{
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
			if (inkCanvas.EditingMode == InkCanvasEditingMode.None)
			{
				stylusPC = new StylusPointCollection();
				System.Windows.Point position = e.GetPosition(inkCanvas);
				stylusPC.Add(new StylusPoint(position.X, position.Y));
			}
		}

		private void inkCanvas1_MouseMove(object sender, MouseEventArgs e)
		{
			FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
		}

		private void inkCanvas1_MouseUp(object sender, MouseButtonEventArgs e)
		{
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
			if (inkCanvas.EditingMode == InkCanvasEditingMode.None && stylusPC != null)
			{
				System.Windows.Point position = e.GetPosition(inkCanvas);
				stylusPC.Add(new StylusPoint(position.X, position.Y));
				strokeLine = new Stroke(stylusPC, inkCanvas.DefaultDrawingAttributes);
				inkCanvas.Strokes.Add(strokeLine.Clone());
				saveStrokeToDB(strokeLine.Clone());
				stylusPC = null;
				strokeLine = null;
			}
		}

		public void strokDelEventHandler()
		{
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
			StackPanel mediaListPanelInReader = GetMediaListPanelInReader();
			Button button = FindVisualChildByName<Button>(mediaListPanelInReader, "delClickButton");
			if (inkCanvas.EditingMode != InkCanvasEditingMode.EraseByStroke)
			{
				inkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
				button.Content = langMng.getLangString("stroke");
				inkCanvas.MouseDown += new MouseButtonEventHandler(penMemoCanvas_MouseDown);
			}
			else
			{
				inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
				inkCanvas.MouseDown -= new MouseButtonEventHandler(penMemoCanvas_MouseDown);
				button.Content = langMng.getLangString("delete");
			}
		}

		public void alterPenmemoAnimation(StrokeToolPanelHorizontal_Reader toolPanel, double f, double t)
		{
			DoubleAnimation doubleAnimation = new DoubleAnimation();
			doubleAnimation.From = f;
			doubleAnimation.To = t;
			doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.3));
			toolPanel.BeginAnimation(FrameworkElement.WidthProperty, doubleAnimation);
		}

		public void showPenToolPanelEventHandler(bool isCanvasShowed)
		{
			Canvas canvas = FindVisualChildByName<Canvas>(FR, "PopupControlCanvas");
			if (isCanvasShowed)
			{
				Panel.SetZIndex(canvas, 901);
				if (canvas.Visibility.Equals(Visibility.Collapsed))
				{
					canvas.Visibility = Visibility.Visible;
				}
			}
			else
			{
				Panel.SetZIndex(canvas, 899);
				if (canvas.Visibility.Equals(Visibility.Visible))
				{
					canvas.Visibility = Visibility.Collapsed;
				}
			}
		}

		private void PopupControlCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Canvas canvas = FindVisualChildByName<Canvas>(FR, "PopupControlCanvas");
			Panel.SetZIndex(canvas, 899);
			if (canvas.Visibility.Equals(Visibility.Visible))
			{
				canvas.Visibility = Visibility.Collapsed;
			}
			Grid grid = FindVisualChildByName<Grid>(FR, "PenMemoToolBar");
			StrokeToolPanelHorizontal_Reader strokeToolPanelHorizontal_Reader = (StrokeToolPanelHorizontal_Reader)grid.Children[grid.Children.Count - 1];
			strokeToolPanelHorizontal_Reader.closePopup();
		}

		private void penMemoCanvas_MouseDown(object sender, MouseButtonEventArgs e)
		{
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
			StrokeCollection selectedStrokes = inkCanvas.GetSelectedStrokes();
			if (selectedStrokes.Count > 0)
			{
				inkCanvas.Strokes.Remove(selectedStrokes);
			}
		}

		public void strokeDelAllEventHandler()
		{
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
			for (int i = 0; i < inkCanvas.Strokes.Count; i++)
			{
				tempStrokes.Add(inkCanvas.Strokes[i]);
			}
			inkCanvas.Strokes.Clear();
			List<string> list = new List<string>();
			List<StrokesData> curPageStrokes = bookManager.getCurPageStrokes(userBookSno, curPageIndex);
			for (int j = 0; j < curPageStrokes.Count; j++)
			{
				list.Add(bookManager.deleteStrokeCmdString(userBookSno, curPageStrokes[j]));
			}
			bookManager.saveBatchData(list);
		}

		private void CheckAndProduceAreaButton(int leftCurPageIndex, int rightCurPageIndex, byte[] curKey, UIElement ImageCanvas)
		{
			if (pageInfoManager == null)
			{
				canAreaButtonBeSeen = false;
				return;
			}
			GetBorderInReader();
			Canvas canvas = (Canvas)ImageCanvas;
			double height = canvas.Height;
			double width = canvas.Width;
			double num = 0.0;
			double num2 = 0.0;
			try
			{
				num = ((ImageBrush)canvas.Background).ImageSource.Height;
				num2 = ((ImageBrush)canvas.Background).ImageSource.Width;
			}
			catch
			{
				return;
			}
			setCanvasSizeAndProduceAreaButton(leftCurPageIndex, rightCurPageIndex, curKey, width, height, num2, num);
		}

		private void setCanvasSizeAndProduceAreaButton(int leftCurPageIndex, int rightCurPageIndex, byte[] curKey, double currentCanvasShowWidth, double currentCanvasShowHeight, double currentImageShowWidth, double currentImageShowHeight)
		{
			double num = currentCanvasShowHeight / currentImageShowHeight;
			if (!rightCurPageIndex.Equals(-1))
			{
				currentImageShowWidth /= 2.0;
				num = currentCanvasShowHeight / currentImageShowHeight;
				double offsetX = currentImageShowWidth * num;
				double offsetY = 0.0;
				ProduceAreaButton(rightCurPageIndex, curKey, currentCanvasShowWidth, currentCanvasShowHeight, num, offsetX, offsetY, currentImageShowHeight, currentImageShowWidth);
			}
			ProduceAreaButton(leftCurPageIndex, curKey, currentCanvasShowWidth, currentCanvasShowHeight, num, 0.0, 0.0, currentImageShowHeight, currentImageShowWidth);
		}

		private void ProduceAreaButton(int pageIndex, byte[] curKey, double currentCanvasShowWidth, double currentCanvasShowHeight, double currentRatio, double offsetX, double offsetY, double currentImageShowHeight, double currentImageShowWidth)
		{
			if (!pageInfoManager.HyperLinkAreaDictionary.ContainsKey(hejMetadata.LImgList[pageIndex].pageId))
			{
				return;
			}
			Canvas stageCanvasInReader = GetStageCanvasInReader();
			pageInfo = pageInfoManager.getHyperLinkAreasByPageId(hejMetadata.LImgList[pageIndex].pageId, curKey);
			stageCanvasInReader.RenderTransform = tfgForHyperLink;
			stageCanvasInReader.Height = currentCanvasShowHeight;
			stageCanvasInReader.Width = currentCanvasShowWidth;
			double num = currentImageShowHeight;
			double num2 = currentImageShowWidth;
			List<HyperLinkArea> hyperLinkAreas = pageInfo.hyperLinkAreas;
			if (pageInfo.refHeight != 0 && pageInfo.refWidth != 0)
			{
				num = pageInfo.refHeight;
				num2 = pageInfo.refWidth;
				currentRatio = currentCanvasShowHeight / num;
				if (!offsetX.Equals(0.0))
				{
					offsetX = num2 * currentRatio;
				}
			}
			for (int i = 0; i < hyperLinkAreas.Count; i++)
			{
				if (!hyperLinkAreas[i].itemRef.Count.Equals(0) || !hyperLinkAreas[i].items.Count.Equals(0))
				{
					createHyperLinkButton(hyperLinkAreas[i], hejMetadata.LImgList[pageIndex].pageId, stageCanvasInReader, pageInfo, currentRatio, offsetX, offsetY);
				}
			}
		}

		private void createHyperLinkButton(HyperLinkArea hyperLinkAreas, string pageID, Canvas canvas, PageInfoMetadata pageInfo, double currentRatio, double offsetX, double offsetY)
		{
			string areaId = hyperLinkAreas.areaId;
			if (areaId.StartsWith("FullText"))
			{
				RadioButton radioButton = FindVisualChildByName<RadioButton>(FR, "FullTextButton");
				radioButton.Visibility = Visibility.Visible;
				radioButton.Uid = areaId;
				radioButton.Tag = pageInfo;
				return;
			}
			float startX = hyperLinkAreas.startX;
			float startY = hyperLinkAreas.startY;
			float endX = hyperLinkAreas.endX;
			float endY = hyperLinkAreas.endY;
			Button button = new Button();
			button.Style = (Style)FindResource("AreaButtonStyle");
			canvas.Children.Add(button);
			double width = Math.Ceiling((double)(endX - startX) * currentRatio);
			double height = Math.Ceiling((double)(endY - startY) * currentRatio);
			double num = Math.Floor((double)startX * currentRatio);
			double num2 = Math.Floor((double)startY * currentRatio);
			button.Width = width;
			button.Height = height;
			button.Uid = areaId;
			button.Tag = pageID;
			button.Click += new RoutedEventHandler(button1_Click);
			Canvas.SetTop(button, num2 + offsetY);
			Canvas.SetLeft(button, num + offsetX);
			if (hyperLinkAreas.shape.Equals("icon"))
			{
				string uriString = bookPath + "\\HYWEB\\" + hyperLinkAreas.imagePath.Replace("/", "\\");
				button.Background = new ImageBrush(new BitmapImage(new Uri(uriString)));
			}
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			string uid = ((Button)sender).Uid;
			string pageId = (string)((Button)sender).Tag;
			HyperLinkArea hyperLinkArea = pageInfoManager.getHyperLinkArea(pageId, uid);
			int targetPageIndex = 0;
			if (hyperLinkArea != null)
			{
				if (hyperLinkArea.items.Count > 0)
				{
					targetPageIndex = getPageIndexByItemId(hyperLinkArea.items[0].id);
				}
			}
			else
			{
				hyperLinkArea = pageInfoManager.getHyperLinkAreaForFullText(pageId, uid);
				targetPageIndex = getPageIndexByItemId(hyperLinkArea.items[0].id);
			}
			string sourcePath = bookPath + "\\HYWEB\\" + hyperLinkArea.items[0].href.Replace("/", "\\");
			doMedia(hyperLinkArea.items[0].mediaType, sourcePath, targetPageIndex);
		}

		private void doMedia(string mediaType, string sourcePath, int targetPageIndex)
		{
		}

		private void showFullText(string sourcePath)
		{
		}

		private int getPageIndexByItemId(string id)
		{
			if (viewStatusIndex.Equals(PageMode.SinglePage))
			{
				for (int i = 0; i < hejMetadata.SImgList.Count; i++)
				{
					if (hejMetadata.SImgList[i].pageId == id)
					{
						return i;
					}
				}
			}
			else if (viewStatusIndex.Equals(PageMode.DoublePage))
			{
				for (int j = 0; j < hejMetadata.SImgList.Count; j++)
				{
					if (hejMetadata.SImgList[j].pageId == id)
					{
						int num = 0;
						return getDoubleCurPageIndex(j);
					}
				}
			}
			return -1;
		}

		private StackPanel getMediaListFromXML()
		{
			GetMediaTableCanvasInReader();
			StackPanel stackPanel = new StackPanel();
			TabControl tabControl = new TabControl();
			for (int i = 0; i < ObservableMediaList.Count; i++)
			{
				TabItem tabItem = new TabItem();
				tabItem.Header = ObservableMediaList[i].categoryName;
				tabItem.HeaderTemplate = (DataTemplate)FindResource("MediaListBoxHeaderTemplateStyle");
				if (!ObservableMediaList[i].mediaList.Count.Equals(0))
				{
					ListBox listBox = new ListBox();
					listBox.ItemsSource = ObservableMediaList[i].mediaList;
					listBox.Style = (Style)FindResource("MediaListBoxStyle");
					listBox.SelectionChanged += new SelectionChangedEventHandler(lv_SelectionChanged);
					tabItem.Content = listBox;
					tabControl.Items.Add(tabItem);
					listBox = null;
				}
			}
			stackPanel.Children.Add(tabControl);
			tabControl = null;
			return stackPanel;
		}

		private void lv_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (!((ListBox)sender).SelectedIndex.Equals(-1))
			{
				GetMediaTableCanvasInReader();
				string mediaSourcePath = ((Media)e.AddedItems[0]).mediaSourcePath;
				string mediaType = ((Media)e.AddedItems[0]).mediaType;
				doMedia(mediaType, mediaSourcePath, -1);
				((ListBox)sender).SelectedIndex = -1;
				e.Handled = true;
			}
		}

		private void ButtonInMediaList_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				GetMediaTableCanvasInReader();
				int num = Convert.ToInt32(((RadioButton)sender).Uid) - 1;
				if (viewStatusIndex.Equals(PageMode.DoublePage))
				{
					num = getDoubleCurPageIndex(num);
				}
				if (num > -1)
				{
					bringBlockIntoView(num);
				}
			}
			catch
			{
			}
		}

		private void BookMarkButtonInListBox_Checked(object sender, RoutedEventArgs e)
		{
			if (!CheckIsNowClick(BookMarkButtonInListBoxSP))
			{
				ShowBookMark();
			}
		}

		private void NoteButtonInListBox_Checked(object sender, RoutedEventArgs e)
		{
			if (!CheckIsNowClick(NoteButtonInListBoxSP))
			{
				ShowNote();
			}
		}

		private void ShowNote()
		{
			Action action = null;
			if (NoteButtonInLBIsClicked)
			{
				NoteButtonInListBox.IsChecked = false;
				NoteButtonInLBIsClicked = false;
				AllImageButtonInListBox.IsChecked = true;
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003CShowNote_003Eb__e);
				}
				factory.StartNew(action);
			}
			else
			{
				if (BookMarkInLBIsClicked)
				{
					BookMarkButtonInListBox.IsChecked = false;
					BookMarkInLBIsClicked = false;
				}
				if (!Settings.Default.IsFlatUIReader)
				{
					List<ThumbnailImageAndPage> list = new List<ThumbnailImageAndPage>();
					foreach (KeyValuePair<int, NoteData> item in bookNoteDictionary)
					{
						if (!item.Value.status.Equals("1"))
						{
							list.Add(singleThumbnailImageAndPageList[item.Key]);
						}
					}
					ListBox listBox = thumbNailListBox;
					if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate12 == null)
					{
						CS_0024_003C_003E9__CachedAnonymousMethodDelegate12 = new Func<ThumbnailImageAndPage, int>(_003CShowNote_003Eb__10);
					}
					listBox.ItemsSource = Enumerable.ToList(Enumerable.OrderBy(list, CS_0024_003C_003E9__CachedAnonymousMethodDelegate12));
				}
				else
				{
					int num = 0;
					foreach (ThumbnailImageAndPage item2 in (IEnumerable)thumbNailListBox.Items)
					{
						ThumbnailImageAndPage thumbnailImageAndPage = item2;
						ListBoxItem listBoxItem = (ListBoxItem)thumbNailListBox.ItemContainerGenerator.ContainerFromIndex(num);
						if (bookNoteDictionary.ContainsKey(num) && bookNoteDictionary[num].status.Equals("0"))
						{
							listBoxItem.Visibility = Visibility.Visible;
						}
						else
						{
							listBoxItem.Visibility = Visibility.Collapsed;
						}
						num++;
					}
				}
				NoteButtonInLBIsClicked = true;
				NoteButtonInListBox.IsChecked = true;
			}
			AllImageButtonInListBoxSP.Background = ColorTool.HexColorToBrush("#000000");
			BookMarkButtonInListBoxSP.Background = ColorTool.HexColorToBrush("#000000");
			NoteButtonInListBoxSP.Background = ColorTool.HexColorToBrush("#F66F00");
			ShowAddition(true);
			txtKeyword.Text = "";
			txtKeyword.Focus();
		}

		private void AllImageButtonInListBox_Checked(object sender, RoutedEventArgs e)
		{
			if (!CheckIsNowClick(AllImageButtonInListBoxSP))
			{
				ShowAll();
			}
		}

		private bool CheckIsNowClick(StackPanel SP)
		{
			System.Windows.Media.Brush background = SP.Background;
			if (background is SolidColorBrush)
			{
				string text = ((SolidColorBrush)background).Color.ToString();
				if (text.Equals("#FFF66F00"))
				{
					return true;
				}
			}
			return false;
		}

		private void FullScreenButton_Checked(object sender, RoutedEventArgs e)
		{
			RoutedCommand routedCommand = new RoutedCommand();
			routedCommand.InputGestures.Add(new KeyGesture(Key.Escape));
			Grid grid = FindVisualChildByName<Grid>(FR, "ToolBarInReader");
			RadioButton radioButton = FindVisualChildByName<RadioButton>(FR, "FullScreenButton");
			LockButton.IsChecked = false;
			isLockButtonLocked = false;
			resetTransform();
			LockButton.Visibility = Visibility.Collapsed;
			base.Visibility = Visibility.Collapsed;
			base.WindowState = WindowState.Maximized;
			if (!isFullScreenButtonClick)
			{
				base.CommandBindings.Add(new CommandBinding(routedCommand, new ExecutedRoutedEventHandler(FullScreenButton_Checked)));
				base.WindowStyle = WindowStyle.None;
				base.Visibility = Visibility.Visible;
				grid.Visibility = Visibility.Collapsed;
				Canvas canvas = FindVisualChildByName<Canvas>(FR, "ToolBarSensor");
				canvas.Visibility = Visibility.Visible;
				canvas.IsMouseDirectlyOverChanged += new DependencyPropertyChangedEventHandler(toolBarSensor_IsMouseDirectlyOverChanged);
				radioButton.IsChecked = true;
				isFullScreenButtonClick = true;
			}
			else
			{
				base.CommandBindings.Remove(base.CommandBindings[base.CommandBindings.Count - 1]);
				setWindowToFitScreen();
				base.WindowStyle = WindowStyle.SingleBorderWindow;
				base.Visibility = Visibility.Visible;
				grid.Visibility = Visibility.Visible;
				Canvas canvas2 = FindVisualChildByName<Canvas>(FR, "ToolBarSensor");
				canvas2.Visibility = Visibility.Collapsed;
				canvas2.IsMouseDirectlyOverChanged -= new DependencyPropertyChangedEventHandler(toolBarSensor_IsMouseDirectlyOverChanged);
				grid.IsMouseDirectlyOverChanged -= new DependencyPropertyChangedEventHandler(toolBarSensor_IsMouseDirectlyOverChanged);
				radioButton.IsChecked = false;
				isFullScreenButtonClick = false;
			}
			resetViewStatus();
			for (int i = 0; i < doubleImgStatus.Count; i++)
			{
				if (doubleImgStatus[i] == ImageStatus.GENERATING || doubleImgStatus[i] == ImageStatus.LARGEIMAGE)
				{
					ReadPagePair readPagePair = doubleReadPagePair[i];
					if (readPagePair.leftImageSource != null)
					{
						readPagePair.leftImageSource = null;
						readPagePair.decodedPDFPages = new byte[2][];
						doubleImgStatus[i] = ImageStatus.SMALLIMAGE;
					}
				}
			}
			for (int j = 0; j < singleImgStatus.Count; j++)
			{
				if (singleImgStatus[j] == ImageStatus.GENERATING || singleImgStatus[j] == ImageStatus.LARGEIMAGE)
				{
					ReadPagePair readPagePair2 = singleReadPagePair[j];
					if (readPagePair2.leftImageSource != null)
					{
						readPagePair2.leftImageSource = null;
						readPagePair2.decodedPDFPages = new byte[2][];
						singleImgStatus[j] = ImageStatus.SMALLIMAGE;
					}
				}
			}
		}

		private void toolBarSensor_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if ((bool)e.NewValue)
			{
				if (sender is Canvas)
				{
					Grid grid = FindVisualChildByName<Grid>(FR, "ToolBarInReader");
					grid.Visibility = Visibility.Visible;
					grid.IsMouseDirectlyOverChanged += new DependencyPropertyChangedEventHandler(toolBarSensor_IsMouseDirectlyOverChanged);
					Canvas canvas = FindVisualChildByName<Canvas>(FR, "ToolBarSensor");
					canvas.Visibility = Visibility.Collapsed;
					canvas.IsMouseDirectlyOverChanged -= new DependencyPropertyChangedEventHandler(toolBarSensor_IsMouseDirectlyOverChanged);
				}
			}
			else if (sender is Grid)
			{
				Grid grid2 = FindVisualChildByName<Grid>(FR, "ToolBarInReader");
				grid2.Visibility = Visibility.Collapsed;
				grid2.IsMouseDirectlyOverChanged -= new DependencyPropertyChangedEventHandler(toolBarSensor_IsMouseDirectlyOverChanged);
				Canvas canvas2 = FindVisualChildByName<Canvas>(FR, "ToolBarSensor");
				canvas2.Visibility = Visibility.Visible;
				canvas2.IsMouseDirectlyOverChanged += new DependencyPropertyChangedEventHandler(toolBarSensor_IsMouseDirectlyOverChanged);
			}
		}

		private void PageViewButton_Checked(object sender, RoutedEventArgs e)
		{
			checkViewStatus(PageMode.SinglePage);
		}

		private void TwoPageViewButton_Checked(object sender, RoutedEventArgs e)
		{
			checkViewStatus(PageMode.DoublePage);
		}

		private void setWindowToFitScreen()
		{
			base.Width = SystemParameters.PrimaryScreenWidth;
			base.Height = SystemParameters.PrimaryScreenHeight - 40.0;
			base.Left = 0.0;
			base.Top = 0.0;
			base.WindowState = WindowState.Normal;
		}

		private void checkViewStatus(PageMode curViewStatusIndex)
		{
			if (viewStatusIndex.Equals(curViewStatusIndex))
			{
				return;
			}
			resetTransform();
			viewStatusIndex = curViewStatusIndex;
			resetViewStatus();
			LockButton.IsChecked = false;
			isLockButtonLocked = false;
			LockButton.Visibility = Visibility.Collapsed;
			bool flag = false;
			if (trialPages > 0)
			{
				flag = true;
			}
			RadioButton radioButton = FindVisualChildByName<RadioButton>(FR, "NoteButton");
			RadioButton radioButton2 = FindVisualChildByName<RadioButton>(FR, "ShareButton");
			FindVisualChildByName<RadioButton>(FR, "PenMemoButton");
			FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
			Canvas canvas = FindVisualChildByName<Canvas>(FR, "zoomCanvas");
			BrushConverter brushConverter = new BrushConverter();
			int num = 0;
			switch (viewStatusIndex)
			{
			case PageMode.SinglePage:
			{
				num = getSingleCurPageIndex(curPageIndex);
				int num2 = num;
				if (num != 0 && num != _FlowDocument.Blocks.Count - 1)
				{
					num--;
				}
				int num3 = 0;
				for (int i = 0; i < singleImgStatus.Count; i++)
				{
					if (singleImgStatus[i] == ImageStatus.LARGEIMAGE)
					{
						num3 += i;
					}
				}
				int num4 = num3 / 3;
				if (num4 == num || num4 == num2)
				{
					num = num4;
				}
				FR.Document = _FlowDocument;
				if (num.Equals(0))
				{
					if (singleReadPagePair[curPageIndex].leftImageSource != null)
					{
						useOriginalCanvasOnLockStatus = true;
						try
						{
							SendImageSourceToZoomCanvas((BitmapImage)singleReadPagePair[curPageIndex].leftImageSource);
							if (canAreaButtonBeSeen)
							{
								CheckAndProduceAreaButton(singleReadPagePair[curPageIndex].leftPageIndex, -1, defaultKey, canvas);
							}
							loadCurrentStrokes(hejMetadata.LImgList[singleReadPagePair[curPageIndex].leftPageIndex].pageId);
							loadCurrentStrokes(singleReadPagePair[curPageIndex].leftPageIndex);
						}
						catch (Exception)
						{
							singleReadPagePair[curPageIndex].leftImageSource = null;
						}
					}
				}
				else
				{
					canvas.Background = (System.Windows.Media.Brush)brushConverter.ConvertFrom("#FF212020");
				}
				bringBlockIntoView(num);
				curPageIndex = num;
				if (radioButton.Visibility.Equals(Visibility.Collapsed) && !flag)
				{
					radioButton.Visibility = Visibility.Visible;
				}
				if (radioButton2.Visibility.Equals(Visibility.Collapsed) && isSharedButtonShowed)
				{
					radioButton2.Visibility = Visibility.Visible;
				}
				if (canPrint)
				{
					RadioButton radioButton4 = FindVisualChildByName<RadioButton>(FR, "PrintButton");
					if (radioButton4.Visibility.Equals(Visibility.Collapsed))
					{
						radioButton4.Visibility = Visibility.Visible;
					}
				}
				break;
			}
			case PageMode.DoublePage:
				num = getDoubleCurPageIndex(curPageIndex);
				FR.Document = _FlowDocumentDouble;
				if (num.Equals(0))
				{
					if (doubleReadPagePair[curPageIndex].leftImageSource != null)
					{
						useOriginalCanvasOnLockStatus = true;
						try
						{
							SendImageSourceToZoomCanvas((BitmapImage)doubleReadPagePair[curPageIndex].leftImageSource);
							if (doubleReadPagePair[curPageIndex].rightPageIndex == -1)
							{
								if (canAreaButtonBeSeen)
								{
									CheckAndProduceAreaButton(doubleReadPagePair[curPageIndex].leftPageIndex, -1, defaultKey, canvas);
								}
								loadCurrentStrokes(hejMetadata.LImgList[doubleReadPagePair[curPageIndex].leftPageIndex].pageId);
								loadCurrentStrokes(singleReadPagePair[curPageIndex].leftPageIndex);
							}
							else
							{
								if (canAreaButtonBeSeen)
								{
									CheckAndProduceAreaButton(doubleReadPagePair[curPageIndex].leftPageIndex, doubleReadPagePair[curPageIndex].rightPageIndex, defaultKey, canvas);
								}
								loadDoublePagesStrokes(hejMetadata.LImgList[doubleReadPagePair[curPageIndex].leftPageIndex].pageId, hejMetadata.LImgList[doubleReadPagePair[curPageIndex].rightPageIndex].pageId);
								loadDoublePagesStrokes(doubleReadPagePair[curPageIndex].leftPageIndex, doubleReadPagePair[curPageIndex].rightPageIndex);
							}
						}
						catch (Exception)
						{
							doubleReadPagePair[curPageIndex].leftImageSource = null;
						}
					}
				}
				else
				{
					canvas.Background = (System.Windows.Media.Brush)brushConverter.ConvertFrom("#FF212020");
				}
				bringBlockIntoView(num);
				curPageIndex = num;
				if (radioButton.Visibility.Equals(Visibility.Visible) && !flag)
				{
					radioButton.Visibility = Visibility.Collapsed;
				}
				if (radioButton2.Visibility.Equals(Visibility.Visible))
				{
					radioButton2.Visibility = Visibility.Collapsed;
				}
				if (canPrint)
				{
					RadioButton radioButton3 = FindVisualChildByName<RadioButton>(FR, "PrintButton");
					if (radioButton3.Visibility.Equals(Visibility.Visible))
					{
						radioButton3.Visibility = Visibility.Collapsed;
					}
				}
				break;
			}
			if (isFullScreenButtonClick)
			{
				Grid grid = FindVisualChildByName<Grid>(FR, "ToolBarInReader");
				grid.Visibility = Visibility.Collapsed;
				grid.IsMouseDirectlyOverChanged -= new DependencyPropertyChangedEventHandler(toolBarSensor_IsMouseDirectlyOverChanged);
				Canvas canvas2 = FindVisualChildByName<Canvas>(FR, "ToolBarSensor");
				canvas2.Visibility = Visibility.Visible;
				canvas2.IsMouseDirectlyOverChanged += new DependencyPropertyChangedEventHandler(toolBarSensor_IsMouseDirectlyOverChanged);
				resetViewStatus();
			}
		}

		private void resetViewStatus()
		{
			Canvas stageCanvasInReader = GetStageCanvasInReader();
			isAreaButtonAndPenMemoRequestSent = false;
			if (stageCanvasInReader.Children.Count > 0)
			{
				stageCanvasInReader.Children.Clear();
				RadioButton radioButton = FindVisualChildByName<RadioButton>(FR, "FullTextButton");
				radioButton.Visibility = Visibility.Collapsed;
			}
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
			if (inkCanvas.Strokes.Count > 0)
			{
				inkCanvas.Strokes.Clear();
			}
			Canvas canvas = FindVisualChildByName<Canvas>(FR, "zoomCanvas");
			canvas.Background = null;
		}

		private void FullTextButton_Checked(object sender, RoutedEventArgs e)
		{
			PageInfoMetadata pageInfoMetadata = (PageInfoMetadata)((RadioButton)sender).Tag;
			HyperLinkArea hyperLinkArea = pageInfoMetadata.hyperLinkAreas[0];
			string sourcePath = bookPath + "\\HYWEB\\" + hyperLinkArea.items[0].href.Replace("/", "\\");
			doMedia(hyperLinkArea.items[0].mediaType, sourcePath, -1);
		}

		private void LockButton_Checked(object sender, RoutedEventArgs e)
		{
			if (isLockButtonLocked.Equals(false))
			{
				LockButton.IsChecked = true;
				isLockButtonLocked = true;
				return;
			}
			LockButton.IsChecked = false;
			isLockButtonLocked = false;
			resetTransform();
			LockButton.Visibility = Visibility.Collapsed;
		}

		private void setBookMark(int pageIndex, bool hasBookMark)
		{
			DateTime value = new DateTime(1970, 1, 1);
			long num = DateTime.Now.ToUniversalTime().Subtract(value).Ticks / 10000000;
			BookMarkData bookMarkData = null;
			if (bookMarkDictionary.ContainsKey(pageIndex))
			{
				bookMarkData = bookMarkDictionary[pageIndex];
				bookMarkData.updatetime = num;
				if (bookMarkData.status == "0")
				{
					bookMarkData.status = "1";
				}
				else
				{
					bookMarkData.status = "0";
				}
			}
			else
			{
				bookMarkData = new BookMarkData();
				bookMarkData.createtime = num;
				bookMarkData.updatetime = num;
				bookMarkData.index = pageIndex;
				bookMarkData.status = "0";
				bookMarkData.synctime = 0L;
				bookMarkData.objectId = "";
				bookMarkDictionary.Add(pageIndex, bookMarkData);
			}
			bookManager.saveBookMarkData(userBookSno, hasBookMark, bookMarkData);
		}

		private void MediaListButton_Checked(object sender, RoutedEventArgs e)
		{
			doUpperRadioButtonClicked(MediaCanvasOpenedBy.MediaButton, sender);
		}

		private void SearchButton_Checked(object sender, RoutedEventArgs e)
		{
			doUpperRadioButtonClicked(MediaCanvasOpenedBy.SearchButton, sender);
		}

		private void TocButton_Checked(object sender, RoutedEventArgs e)
		{
			doUpperRadioButtonClicked(MediaCanvasOpenedBy.CategoryButton, sender);
		}

		private void NoteButton_Checked(object sender, RoutedEventArgs e)
		{
			Action action = null;
			if (Settings.Default.IsFlatUIReader)
			{
				Border border = FindVisualChildByName<Border>(FR, "mediaListBorder");
				if (border != null)
				{
					Canvas.SetTop(border, double.NaN);
					Canvas.SetRight(border, double.NaN);
					Canvas.SetBottom(border, 64.0);
					Canvas.SetLeft(border, PenSP.Width + 64.0);
				}
			}
			doUpperRadioButtonClicked(MediaCanvasOpenedBy.NoteButton, sender);
			Canvas mediaTableCanvasInReader = GetMediaTableCanvasInReader();
			if (mediaTableCanvasInReader == null || mediaTableCanvasInReader.Visibility != 0)
			{
				return;
			}
			MemoSP.Background = ColorTool.HexColorToBrush("#F66F00");
			if (CheckIsNowClick(ViewThumbSP))
			{
				Canvas sp = thumnailCanvas;
				if (action == null)
				{
					action = new Action(_003CNoteButton_Checked_003Eb__13);
				}
				MyAnimation(sp, 500.0, "Height", 150.0, 0.0, action);
				ViewThumbSP.Background = ColorTool.HexColorToBrush("#000000");
			}
		}

		public UIElement cloneElement(UIElement orig)
		{
			if (orig == null)
			{
				return null;
			}
			string s = XamlWriter.Save(orig);
			StringReader input = new StringReader(s);
			XmlReader reader = XmlReader.Create(input, new XmlReaderSettings());
			return (UIElement)XamlReader.Load(reader);
		}

		private void SettingsButton_Checked(object sender, RoutedEventArgs e)
		{
			doUpperRadioButtonClicked(MediaCanvasOpenedBy.SettingButton, sender);
		}

		private void ContentButton_Checked(object sender, RoutedEventArgs e)
		{
			if (thumbNailListBoxOpenedFullScreen)
			{
				thumnailCanvas.Visibility = Visibility.Hidden;
			}
			Canvas mediaTableCanvasInReader = GetMediaTableCanvasInReader();
			if (mediaTableCanvasInReader.Visibility.Equals(Visibility.Visible))
			{
				mediaTableCanvasInReader.Visibility = Visibility.Collapsed;
			}
			int num = hejMetadata.tocPageIndex - 1;
			if (viewStatusIndex.Equals(PageMode.DoublePage))
			{
				num = getDoubleCurPageIndex(num);
			}
			if (num != -1)
			{
				bringBlockIntoView(num);
			}
		}

		private void MediaTableCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Canvas canvas = (Canvas)sender;
			canvas.Visibility = Visibility.Collapsed;
			string text = "";
			switch (openedby)
			{
			case MediaCanvasOpenedBy.SearchButton:
				text = "SearchButton";
				break;
			case MediaCanvasOpenedBy.MediaButton:
				text = "MediaListButton";
				break;
			case MediaCanvasOpenedBy.CategoryButton:
				text = "TocButton";
				break;
			case MediaCanvasOpenedBy.NoteButton:
				text = "NoteButton";
				break;
			case MediaCanvasOpenedBy.ShareButton:
				text = "ShareButton";
				break;
			case MediaCanvasOpenedBy.SettingButton:
				text = "SettingsButton";
				break;
			}
			if (!text.Equals("") && !text.Equals("NoteButton"))
			{
				RadioButton radioButton = FindVisualChildByName<RadioButton>(FR, text);
				radioButton.IsChecked = false;
			}
			else if (text.Equals("NoteButton"))
			{
				TextBox textBox = FindVisualChildByName<TextBox>(FR, "notePanel");
				int targetPageIndex = curPageIndex;
				setNotesInMem(textBox.Text, targetPageIndex);
				RadioButton radioButton2 = FindVisualChildByName<RadioButton>(FR, "NoteButton");
				if (textBox.Text.Equals(""))
				{
					radioButton2.IsChecked = false;
					TriggerBookMark_NoteButtonOrElse(radioButton2);
				}
				else
				{
					radioButton2.IsChecked = true;
					TriggerBookMark_NoteButtonOrElse(radioButton2);
				}
				ShowAddition(true);
			}
			resetFocusBackToReader();
		}

		private void resetFocusBackToReader()
		{
			if (pageViewerPager != null && pageViewerPager.Focusable && pageViewerPager.IsEnabled && !pageViewerPager.IsKeyboardFocused)
			{
				Keyboard.Focus(pageViewerPager);
			}
		}

		private void PenMemoButton_Checked(object sender, RoutedEventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (viewStatusIndex.Equals(PageMode.DoublePage))
			{
				MessageBox.Show(langMng.getLangString("doublePageStrokeModeAlert"), langMng.getLangString("strokeMode"), MessageBoxButton.OK);
				radioButton.IsChecked = false;
				return;
			}
			openedby = MediaCanvasOpenedBy.PenMemo;
			Grid grid = FindVisualChildByName<Grid>(FR, "ToolBarInReader");
			Grid grid2 = FindVisualChildByName<Grid>(FR, "PenMemoToolBar");
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
			StrokeToolPanelHorizontal_Reader strokeToolPanelHorizontal_Reader = new StrokeToolPanelHorizontal_Reader();
			strokeToolPanelHorizontal_Reader.langMng = langMng;
			Canvas element = FindVisualChildByName<Canvas>(FR, "zoomCanvas");
			Canvas stageCanvasInReader = GetStageCanvasInReader();
			if (grid2.Visibility.Equals(Visibility.Collapsed))
			{
				grid.Visibility = Visibility.Collapsed;
				grid2.Visibility = Visibility.Visible;
				radioButton.IsChecked = false;
				strokeToolPanelHorizontal_Reader.determineDrawAtt(inkCanvas.DefaultDrawingAttributes, isStrokeLine);
				Panel.SetZIndex(inkCanvas, 900);
				Panel.SetZIndex(stageCanvasInReader, 2);
				Panel.SetZIndex(element, 850);
				inkCanvas.Background = System.Windows.Media.Brushes.Transparent;
				inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
				inkCanvas.Visibility = Visibility.Visible;
				strokeToolPanelHorizontal_Reader.HorizontalAlignment = HorizontalAlignment.Right;
				grid2.Children.Add(strokeToolPanelHorizontal_Reader);
				alterPenmemoAnimation(strokeToolPanelHorizontal_Reader, 0.0, strokeToolPanelHorizontal_Reader.Width);
				strokeToolPanelHorizontal_Reader.strokeChange += new StrokeChangeEvent(strokeChaneEventHandler);
				strokeToolPanelHorizontal_Reader.strokeUndo += new StrokeUndoEvent(strokeUndoEventHandler);
				strokeToolPanelHorizontal_Reader.strokeDelAll += new StrokeDeleteAllEvent(strokeDelAllEventHandler);
				strokeToolPanelHorizontal_Reader.strokeRedo += new StrokeRedoEvent(strokeRedoEventHandler);
				strokeToolPanelHorizontal_Reader.strokeDel += new StrokeDeleteEvent(strokDelEventHandler);
				strokeToolPanelHorizontal_Reader.showPenToolPanel += new showPenToolPanelEvent(showPenToolPanelEventHandler);
				strokeToolPanelHorizontal_Reader.strokeErase += new StrokeEraseEvent(strokeEraseEventHandler);
				strokeToolPanelHorizontal_Reader.strokeCurve += new StrokeCurveEvent(strokeCurveEventHandler);
				strokeToolPanelHorizontal_Reader.strokeLine += new StrokeLineEvent(strokeLineEventHandler);
				inkCanvas.Focus();
				Canvas canvas = FindVisualChildByName<Canvas>(FR, "HiddenControlCanvas");
				if (canvas.Visibility.Equals(Visibility.Collapsed))
				{
					canvas.Visibility = Visibility.Visible;
				}
				Keyboard.ClearFocus();
				ButtonsStatusWhenOpenPenMemo(0.5, false);
				if (isStrokeLine)
				{
					strokeLineEventHandler();
				}
				else
				{
					strokeCurveEventHandler();
				}
			}
			else
			{
				Panel.SetZIndex(element, 1);
				Panel.SetZIndex(inkCanvas, 2);
				Panel.SetZIndex(stageCanvasInReader, 3);
				((RadioButton)sender).IsChecked = false;
				inkCanvas.EditingMode = InkCanvasEditingMode.None;
				alterPenmemoAnimation(strokeToolPanelHorizontal_Reader, strokeToolPanelHorizontal_Reader.Width, 0.0);
				convertCurrentStrokesToDB(hejMetadata.LImgList[curPageIndex].pageId);
				grid2.Children.Remove(grid2.Children[grid2.Children.Count - 1]);
				Canvas canvas2 = FindVisualChildByName<Canvas>(FR, "PopupControlCanvas");
				if (canvas2.Visibility.Equals(Visibility.Visible))
				{
					canvas2.Visibility = Visibility.Collapsed;
				}
				Canvas canvas3 = FindVisualChildByName<Canvas>(FR, "HiddenControlCanvas");
				if (canvas3.Visibility.Equals(Visibility.Visible))
				{
					canvas3.Visibility = Visibility.Collapsed;
				}
				grid2.Visibility = Visibility.Collapsed;
				grid.Visibility = Visibility.Visible;
				ButtonsStatusWhenOpenPenMemo(1.0, true);
				resetFocusBackToReader();
			}
		}

		private void ButtonsStatusWhenOpenPenMemo(double opacity, bool isEnabled)
		{
			RadioButton radioButton = FindVisualChildByName<RadioButton>(FR, "leftPageButton");
			RadioButton radioButton2 = FindVisualChildByName<RadioButton>(FR, "rightPageButton");
			radioButton.Opacity = opacity;
			radioButton2.Opacity = opacity;
			LockButton.IsEnabled = isEnabled;
			ShowListBoxButton.IsEnabled = base.IsEnabled;
		}

		private void BackToBookShelfButton_Click(object sender, RoutedEventArgs e)
		{
			if (!isSyncing || isSyncOwner)
			{
				Close();
			}
		}

		private void AllImageButtonInListBoxSP_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (!CheckIsNowClick(AllImageButtonInListBoxSP))
			{
				RadioButton allImageButtonInListBox = AllImageButtonInListBox;
				bool? isChecked = AllImageButtonInListBox.IsChecked;
				allImageButtonInListBox.IsChecked = (isChecked.HasValue ? new bool?(!isChecked.GetValueOrDefault()) : null);
				ShowAll();
			}
		}

		private void NoteButtonInListBoxSP_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (!CheckIsNowClick(NoteButtonInListBoxSP))
			{
				RadioButton noteButtonInListBox = NoteButtonInListBox;
				bool? isChecked = NoteButtonInListBox.IsChecked;
				noteButtonInListBox.IsChecked = (isChecked.HasValue ? new bool?(!isChecked.GetValueOrDefault()) : null);
				ShowNote();
			}
		}

		private void BookMarkButtonInListBoxSP_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (!CheckIsNowClick(BookMarkButtonInListBoxSP))
			{
				RadioButton noteButtonInListBox = NoteButtonInListBox;
				bool? isChecked = NoteButtonInListBox.IsChecked;
				noteButtonInListBox.IsChecked = (isChecked.HasValue ? new bool?(!isChecked.GetValueOrDefault()) : null);
				ShowBookMark();
			}
		}

		private void ShowBookMark()
		{
			Action action = null;
			if (BookMarkInLBIsClicked)
			{
				BookMarkButtonInListBox.IsChecked = false;
				BookMarkInLBIsClicked = false;
				AllImageButtonInListBox.IsChecked = true;
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003CShowBookMark_003Eb__15);
				}
				factory.StartNew(action);
			}
			else
			{
				if (NoteButtonInLBIsClicked)
				{
					NoteButtonInListBox.IsChecked = false;
					NoteButtonInLBIsClicked = false;
				}
				if (!Settings.Default.IsFlatUIReader)
				{
					List<ThumbnailImageAndPage> list = new List<ThumbnailImageAndPage>();
					foreach (KeyValuePair<int, BookMarkData> item in bookMarkDictionary)
					{
						if (item.Value.status == "0")
						{
							list.Add(singleThumbnailImageAndPageList[item.Key]);
						}
					}
					ListBox listBox = thumbNailListBox;
					if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate19 == null)
					{
						CS_0024_003C_003E9__CachedAnonymousMethodDelegate19 = new Func<ThumbnailImageAndPage, int>(_003CShowBookMark_003Eb__17);
					}
					listBox.ItemsSource = Enumerable.ToList(Enumerable.OrderBy(list, CS_0024_003C_003E9__CachedAnonymousMethodDelegate19));
				}
				else
				{
					int num = 0;
					foreach (ThumbnailImageAndPage item2 in (IEnumerable)thumbNailListBox.Items)
					{
						ThumbnailImageAndPage thumbnailImageAndPage = item2;
						ListBoxItem listBoxItem = (ListBoxItem)thumbNailListBox.ItemContainerGenerator.ContainerFromIndex(num);
						if (bookMarkDictionary.ContainsKey(num) && bookMarkDictionary[num].status.Equals("0"))
						{
							listBoxItem.Visibility = Visibility.Visible;
						}
						else
						{
							listBoxItem.Visibility = Visibility.Collapsed;
						}
						num++;
					}
				}
				BookMarkInLBIsClicked = true;
				BookMarkButtonInListBox.IsChecked = true;
			}
			NoteButtonInListBoxSP.Background = ColorTool.HexColorToBrush("#000000");
			AllImageButtonInListBoxSP.Background = ColorTool.HexColorToBrush("#000000");
			BookMarkButtonInListBoxSP.Background = ColorTool.HexColorToBrush("#F66F00");
			ShowAddition(true);
			txtKeyword.Text = "";
			txtKeyword.Focus();
		}

		private void ShowAll()
		{
			try
			{
				BookMarkInLBIsClicked = false;
				NoteButtonInLBIsClicked = false;
				AllImageButtonInListBox.IsChecked = true;
				if (!Settings.Default.IsFlatUIReader)
				{
					ListBox listBox = thumbNailListBox;
					List<ThumbnailImageAndPage> source = singleThumbnailImageAndPageList;
					if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate1b == null)
					{
						CS_0024_003C_003E9__CachedAnonymousMethodDelegate1b = new Func<ThumbnailImageAndPage, int>(_003CShowAll_003Eb__1a);
					}
					listBox.ItemsSource = Enumerable.ToList(Enumerable.OrderBy(source, CS_0024_003C_003E9__CachedAnonymousMethodDelegate1b));
				}
				else
				{
					int num = 0;
					foreach (ThumbnailImageAndPage item in (IEnumerable)thumbNailListBox.Items)
					{
						ThumbnailImageAndPage thumbnailImageAndPage = item;
						ListBoxItem listBoxItem = (ListBoxItem)thumbNailListBox.ItemContainerGenerator.ContainerFromIndex(num);
						if (listBoxItem != null)
						{
							listBoxItem.Visibility = Visibility.Visible;
						}
						num++;
					}
				}
				NoteButtonInListBoxSP.Background = ColorTool.HexColorToBrush("#000000");
				BookMarkButtonInListBoxSP.Background = ColorTool.HexColorToBrush("#000000");
				AllImageButtonInListBoxSP.Background = ColorTool.HexColorToBrush("#F66F00");
				ShowAddition(true);
				txtKeyword.Text = "";
				txtKeyword.Focus();
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}

		private void ShowAddition(bool ShowFilter = true)
		{
			if (ShowFilter)
			{
				ShowFilterCount();
			}
			ShowImageCenter();
		}

		private void ShowImageCenter()
		{
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action(ShowImageCenter));
				return;
			}
			int num = (thumbNailListBox.SelectedIndex >= 0) ? thumbNailListBox.SelectedIndex : 0;
			ListBoxItem listBoxItem = (ListBoxItem)thumbNailListBox.ItemContainerGenerator.ContainerFromItem(thumbNailListBox.SelectedItem);
			if (listBoxItem != null)
			{
				listBoxItem.Focus();
				if (!thumbNailListBoxOpenedFullScreen && (double)(num + 1) * listBoxItem.ActualWidth > base.ActualWidth / 2.0)
				{
					ScrollViewer scrollViewer = FindVisualChildByName<ScrollViewer>(thumbNailListBox, "SVInLV");
					double offset = (double)(num + 1) * listBoxItem.ActualWidth - base.ActualWidth / 2.0;
					scrollViewer.ScrollToHorizontalOffset(offset);
				}
			}
		}

		private void ChangeThumbNailListBoxRelativeStatus()
		{
			Action action = null;
			Canvas mediaTableCanvasInReader = GetMediaTableCanvasInReader();
			if (mediaTableCanvasInReader.Visibility.Equals(Visibility.Visible))
			{
				mediaTableCanvasInReader.Visibility = Visibility.Collapsed;
			}
			RadioButton radioButton = FindVisualChildByName<RadioButton>(FR, "ShowAllImageButton");
			ScrollViewer scrollViewer = FindVisualChildByName<ScrollViewer>(thumbNailListBox, "SVInLV");
			FindVisualChildByName<WrapPanel>(FR, "wrapPanel");
			BookMarkInLBIsClicked = false;
			NoteButtonInLBIsClicked = false;
			AllImageButtonInListBox.IsChecked = true;
			if (Settings.Default.IsFlatUIReader)
			{
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003CChangeThumbNailListBoxRelativeStatus_003Eb__1c);
				}
				factory.StartNew(action);
			}
			switch (thumbNailListBoxStatus)
			{
			case 0:
				thumbNailListBoxOpenedFullScreen = false;
				thumnailCanvas.Visibility = Visibility.Hidden;
				ShowListBoxButton.Visibility = Visibility.Visible;
				radioButton.IsChecked = false;
				AllImageButtonInListBox.IsChecked = true;
				if (!downloadProgBar.Visibility.Equals(Visibility.Collapsed))
				{
					downloadProgBar.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
				}
				LockButton.Margin = new Thickness(0.0, 0.0, 15.0, 15.0);
				break;
			case 1:
			{
				thumbNailListBoxOpenedFullScreen = false;
				Binding binding4 = new Binding();
				binding4.Source = FR;
				binding4.Path = new PropertyPath("ActualWidth");
				binding4.Converter = new thumbNailListBoxWidthHeightConverter();
				binding4.ConverterParameter = 30;
				if (Settings.Default.IsFlatUIReader)
				{
					binding4.ConverterParameter = 105;
				}
				thumbNailListBox.SetBinding(FrameworkElement.WidthProperty, binding4);
				thumbNailListBox.Height = thumbnailListBoxHeight;
				thumnailCanvas.Height = thumbnailListBoxHeight;
				try
				{
					if (scrollViewer != null)
					{
						scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
						scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
					}
				}
				catch (Exception ex)
				{
					LogTool.Debug(ex);
				}
				HideListBoxButton.ToolTip = langMng.getLangString("hideThumbnails");
				thumbNailCanvasStackPanel.Orientation = Orientation.Horizontal;
				RadioButtonStackPanel.Orientation = Orientation.Vertical;
				thumbNailCanvasGrid.HorizontalAlignment = HorizontalAlignment.Center;
				thumnailCanvas.Visibility = Visibility.Visible;
				radioButton.IsChecked = false;
				ShowListBoxButton.Visibility = Visibility.Hidden;
				if (!downloadProgBar.Visibility.Equals(Visibility.Collapsed))
				{
					downloadProgBar.Margin = new Thickness(0.0, 0.0, 0.0, thumbnailListBoxHeight);
				}
				LockButton.Margin = new Thickness(0.0, 0.0, 15.0, 15.0 + thumbnailListBoxHeight);
				break;
			}
			case 2:
			{
				thumbNailListBoxOpenedFullScreen = true;
				Binding binding = new Binding();
				binding.Source = FR;
				binding.Path = new PropertyPath("ActualHeight");
				thumnailCanvas.SetBinding(FrameworkElement.HeightProperty, binding);
				Binding binding2 = new Binding();
				binding2.Source = FR;
				binding2.Path = new PropertyPath("ActualWidth");
				Binding binding3 = new Binding();
				binding3.Source = FR;
				binding3.Path = new PropertyPath("ActualHeight");
				binding3.Converter = new thumbNailListBoxWidthHeightConverter();
				binding3.ConverterParameter = 30;
				thumbNailListBox.SetBinding(FrameworkElement.HeightProperty, binding3);
				thumbNailListBox.SetBinding(FrameworkElement.WidthProperty, binding2);
				scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
				scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
				thumbNailCanvasStackPanel.Orientation = Orientation.Vertical;
				RadioButtonStackPanel.Orientation = Orientation.Horizontal;
				thumbNailCanvasGrid.HorizontalAlignment = HorizontalAlignment.Right;
				thumnailCanvas.Visibility = Visibility.Visible;
				radioButton.IsChecked = true;
				ShowListBoxButton.Visibility = Visibility.Hidden;
				HideListBoxButton.ToolTip = langMng.getLangString("closeThumbnail");
				if (!downloadProgBar.Visibility.Equals(Visibility.Collapsed))
				{
					downloadProgBar.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
				}
				LockButton.Margin = new Thickness(0.0, 0.0, 15.0, 15.0);
				break;
			}
			}
		}

		private void ShowAllImageButton_Checked(object sender, RoutedEventArgs e)
		{
			thumbNailListBoxStatus = 2;
			ChangeThumbNailListBoxRelativeStatus();
		}

		private void ShowListBoxButtonNew_Click(object sender, RoutedEventArgs e)
		{
			Action action = null;
			Action action2 = null;
			MouseTool.ShowLoading();
			if (thumnailCanvas.Visibility == Visibility.Visible)
			{
				ChangeThumbNailListBoxRelativeStatus();
				Canvas sp = thumnailCanvas;
				if (action == null)
				{
					action = new Action(_003CShowListBoxButtonNew_Click_003Eb__1f);
				}
				MyAnimation(sp, 500.0, "Height", 150.0, 0.0, action);
				ViewThumbSP.Background = ColorTool.HexColorToBrush("#000000");
			}
			else
			{
				Canvas sp2 = thumnailCanvas;
				if (action2 == null)
				{
					action2 = new Action(_003CShowListBoxButtonNew_Click_003Eb__20);
				}
				MyAnimation(sp2, 500.0, "Height", 0.0, 150.0, action2);
				ViewThumbSP.Background = ColorTool.HexColorToBrush("#F66F00");
				ShowAll();
				thumbNailListBoxStatus = 1;
				ChangeThumbNailListBoxRelativeStatus();
				MoveBoxPage();
				txtKeyword.Select(txtKeyword.Text.Length, 0);
				txtKeyword.Focus();
			}
			if (btnPenFuncSP.Height > 0.0)
			{
				btnPenColor.Background = ColorTool.HexColorToBrush("#000000");
				MyAnimation(btnPenFuncSP, 300.0, "Height", btnPenFuncSP.ActualHeight, 0.0);
			}
			if (btnFuncSP.Height > 0.0)
			{
				btnBold.Background = ColorTool.HexColorToBrush("#000000");
				MyAnimation(btnFuncSP, 300.0, "Height", btnFuncSP.ActualHeight, 0.0);
			}
			Task.Factory.StartNew(new Action(_003CShowListBoxButtonNew_Click_003Eb__21));
		}

		private void MoveBoxPage()
		{
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action(MoveBoxPage));
			}
			else
			{
				try
				{
					List<ThumbnailImageAndPage> list = (List<ThumbnailImageAndPage>)thumbNailListBox.ItemsSource;
					int num = 0;
					foreach (ThumbnailImageAndPage item in list)
					{
						if (item.pageIndex.Equals((curPageIndex + 1).ToString()))
						{
							thumbNailListBox.SelectedIndex = num;
							break;
						}
						num++;
					}
					thumbNailListBox.Focus();
				}
				catch (Exception ex)
				{
					LogTool.Debug(ex);
				}
			}
		}

		private void ShowFilterCount()
		{
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action(ShowFilterCount));
				return;
			}
			int num = 0;
			int num2 = 0;
			foreach (ThumbnailImageAndPage item in (IEnumerable)thumbNailListBox.Items)
			{
				int.Parse(item.pageIndex);
				ListBoxItem listBoxItem = (ListBoxItem)thumbNailListBox.ItemContainerGenerator.ContainerFromIndex(num);
				if (listBoxItem == null)
				{
					num2 = thumbNailListBox.Items.Count;
					break;
				}
				if (listBoxItem.Visibility == Visibility.Visible)
				{
					num2++;
				}
				num++;
			}
			txtFilterCount.Text = string.Format("有 {0} 筆相關資料", num2);
		}

		private void btnThickness_Click(object sender, RoutedEventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			System.Windows.Controls.Image image = (System.Windows.Controls.Image)radioButton.Content;
			System.Windows.Controls.Image image2 = (System.Windows.Controls.Image)btnBold.Content;
			MyAnimation(btnFuncSP, 300.0, "Height", btnFuncSP.ActualHeight, 0.0);
			btnBold.Background = ColorTool.HexColorToBrush("#000000");
			image2.Source = image.Source;
			btnBold.Tag = radioButton.Tag;
			ChangeMainPenColor();
		}

		private void ChangeMainPenColor()
		{
			int result = 1;
			int.TryParse(btnPenColor.Tag.ToString(), out result);
			int result2 = 1;
			int.TryParse(btnBold.Tag.ToString(), out result2);
			PenColorType penColorType = (PenColorType)Enum.Parse(typeof(PenColorType), (result + result2).ToString());
			((System.Windows.Controls.Image)btnPen.Content).Source = PenColorTool.GetButtonImage(penColorType);
			btnPen.Tag = (int)penColorType;
			DrawingAttributes drawingAttributes = new DrawingAttributes();
			switch (result2)
			{
			case 100:
				drawingAttributes.Width = 4.0;
				drawingAttributes.Height = 4.0;
				break;
			case 200:
				drawingAttributes.Width = 8.0;
				drawingAttributes.Height = 8.0;
				break;
			case 300:
				drawingAttributes.Width = 16.0;
				drawingAttributes.Height = 16.0;
				break;
			}
			int num = result % 10;
			int num2 = (result + 1) % 2;
			switch (num)
			{
			case 1:
			case 2:
				drawingAttributes.Color = Colors.Red;
				break;
			case 3:
			case 4:
				drawingAttributes.Color = Colors.Yellow;
				break;
			case 5:
			case 6:
				drawingAttributes.Color = Colors.Green;
				break;
			case 7:
			case 8:
				drawingAttributes.Color = Colors.Blue;
				break;
			case 9:
			case 10:
				drawingAttributes.Color = Colors.Purple;
				break;
			}
			if (num2 == 1)
			{
				drawingAttributes.IsHighlighter = true;
			}
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
			inkCanvas.DefaultDrawingAttributes = drawingAttributes;
		}

		private void btnPenColor_Click(object sender, RoutedEventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			System.Windows.Controls.Image image = (System.Windows.Controls.Image)radioButton.Content;
			System.Windows.Controls.Image image2 = (System.Windows.Controls.Image)btnPenColor.Content;
			MyAnimation(btnPenFuncSP, 300.0, "Height", btnPenFuncSP.ActualHeight, 0.0);
			btnPenColor.Background = ColorTool.HexColorToBrush("#000000");
			image2.Source = image.Source;
			btnPenColor.Tag = radioButton.Tag;
			ChangeMainPenColor();
		}

		private void btnPen_Click(object sender, RoutedEventArgs e)
		{
			StartAnimation(PenSP, PenSlideCtrl);
		}

		private void btnEraser_Click(object sender, RoutedEventArgs e)
		{
			System.Windows.Media.Brush background = btnEraserGD.Background;
			if (background is SolidColorBrush)
			{
				string text = ((SolidColorBrush)background).Color.ToString();
				if (text.Equals("#FFF66F00"))
				{
					btnEraserGD.Background = System.Windows.Media.Brushes.Transparent;
				}
				else
				{
					btnEraserGD.Background = ColorTool.HexColorToBrush("#F66F00");
				}
			}
		}

		private void btnSetting_Click(object sender, RoutedEventArgs e)
		{
			StartAnimation(SettingSP, SettingSlideCtrl);
		}

		private void StartAnimation(StackPanel sp, System.Windows.Controls.Image image)
		{
			Action action = null;
			if (sp.Name.Equals("PenSP"))
			{
				DoubleAnimation doubleAnimation = new DoubleAnimation(0.0, PenSP.Width + 64.0, TimeSpan.FromMilliseconds(500.0));
				doubleAnimation = ((!(PenSP.Width <= 0.0)) ? new DoubleAnimation(PenSP.ActualWidth + 64.0, 64.0, TimeSpan.FromMilliseconds(500.0)) : new DoubleAnimation(64.0, PenSP.ActualWidth + 64.0, TimeSpan.FromMilliseconds(500.0)));
				Border border = FindVisualChildByName<Border>(FR, "mediaListBorder");
				if (border != null)
				{
					Canvas.SetTop(border, double.NaN);
					Canvas.SetRight(border, double.NaN);
					border.BeginAnimation(Canvas.LeftProperty, doubleAnimation);
				}
				if (btnPenFuncSP.Height > 0.0)
				{
					btnPenColor.Background = ColorTool.HexColorToBrush("#000000");
					MyAnimation(btnPenFuncSP, 300.0, "Height", btnPenFuncSP.ActualHeight, 0.0);
				}
				if (btnFuncSP.Height > 0.0)
				{
					btnBold.Background = ColorTool.HexColorToBrush("#000000");
					MyAnimation(btnFuncSP, 300.0, "Height", btnFuncSP.ActualHeight, 0.0);
				}
			}
			else
			{
				if (thumnailCanvas.Visibility == Visibility.Visible)
				{
					ChangeThumbNailListBoxRelativeStatus();
					Canvas sp2 = thumnailCanvas;
					if (action == null)
					{
						action = new Action(_003CStartAnimation_003Eb__27);
					}
					MyAnimation(sp2, 500.0, "Height", 150.0, 0.0, action);
					ViewThumbSP.Background = ColorTool.HexColorToBrush("#000000");
				}
				if (CheckIsNowClick(MemoSP))
				{
					noteButton_Click();
				}
			}
			Storyboard storyboard = new Storyboard();
			DoubleAnimation doubleAnimation2 = new DoubleAnimation();
			Duration duration2 = doubleAnimation2.Duration = new Duration(TimeSpan.FromMilliseconds(500.0));
			storyboard.Children.Add(doubleAnimation2);
			Storyboard.SetTarget(doubleAnimation2, sp);
			Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath("Width"));
			DoubleAnimation doubleAnimation3;
			if (sp.Width > 0.0)
			{
				doubleAnimation2.To = 0.0;
				doubleAnimation3 = new DoubleAnimation(180.0, 0.0, duration2);
			}
			else
			{
				doubleAnimation2.To = sp.ActualWidth;
				doubleAnimation3 = new DoubleAnimation(0.0, 180.0, duration2);
			}
			doubleAnimation2.AccelerationRatio = 0.2;
			doubleAnimation2.DecelerationRatio = 0.7;
			doubleAnimation3.AccelerationRatio = 0.2;
			doubleAnimation3.DecelerationRatio = 0.7;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate2a == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate2a = new EventHandler(_003CStartAnimation_003Eb__28);
			}
			storyboard.Completed += CS_0024_003C_003E9__CachedAnonymousMethodDelegate2a;
			PowerEase powerEase = new PowerEase();
			powerEase.EasingMode = EasingMode.EaseOut;
			PowerEase powerEase2 = (PowerEase)(doubleAnimation3.EasingFunction = powerEase);
			RotateTransform rotateTransform = (RotateTransform)(image.RenderTransform = new RotateTransform());
			image.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
			storyboard.Begin();
			rotateTransform.BeginAnimation(RotateTransform.AngleProperty, doubleAnimation3);
		}

		private void ShowListBoxButton_Click(object sender, RoutedEventArgs e)
		{
			thumbNailListBoxStatus = 1;
			ChangeThumbNailListBoxRelativeStatus();
		}

		private void HideListBoxButton_Checked(object sender, RoutedEventArgs e)
		{
			thumbNailListBoxStatus = 0;
			ChangeThumbNailListBoxRelativeStatus();
		}

		private StackPanel openSettings()
		{
			List<TextBlock> list = new List<TextBlock>();
			TextBlock textBlock = new TextBlock();
			textBlock.Text = langMng.getLangString("showMultimediaSensor");
			textBlock.FontSize = 14.0;
			list.Add(textBlock);
			TextBlock textBlock2 = new TextBlock();
			textBlock2.Text = langMng.getLangString("showPageButton");
			textBlock2.FontSize = 14.0;
			list.Add(textBlock2);
			List<TextBlock> list2 = list;
			List<bool> list3 = new List<bool>();
			list3.Add(true);
			list3.Add(true);
			List<bool> list4 = list3;
			StackPanel stackPanel = new StackPanel();
			stackPanel.Margin = new Thickness(20.0, 10.0, 20.0, 10.0);
			stackPanel.Orientation = Orientation.Vertical;
			for (int i = 0; i < list2.Count; i++)
			{
				Grid grid = new Grid();
				grid.HorizontalAlignment = HorizontalAlignment.Left;
				grid.Margin = new Thickness(0.0, 0.0, 0.0, 10.0);
				CheckBox checkBox = new CheckBox();
				checkBox.Content = list2[i];
				checkBox.IsChecked = list4[i];
				CheckBox checkBox2 = checkBox;
				switch (i)
				{
				case 0:
					checkBox2.Click += new RoutedEventHandler(AreaButtonSettingsButton_Click);
					break;
				case 1:
					checkBox2.Click += new RoutedEventHandler(LeftRightPageButtonSettingsButton_Click);
					break;
				}
				grid.Children.Add(checkBox2);
				stackPanel.Children.Add(grid);
			}
			stackPanel.Orientation = Orientation.Vertical;
			return stackPanel;
		}

		private void LeftRightPageButtonSettingsButton_Click(object sender, RoutedEventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			RadioButton radioButton = FindVisualChildByName<RadioButton>(FR, "leftPageButton");
			RadioButton radioButton2 = FindVisualChildByName<RadioButton>(FR, "rightPageButton");
			if (checkBox.IsChecked.Equals(true))
			{
				radioButton.Visibility = Visibility.Visible;
				radioButton2.Visibility = Visibility.Visible;
			}
			else if (checkBox.IsChecked.Equals(false))
			{
				radioButton.Visibility = Visibility.Collapsed;
				radioButton2.Visibility = Visibility.Collapsed;
			}
			resetFocusBackToReader();
		}

		private void AreaButtonSettingsButton_Click(object sender, RoutedEventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (checkBox.IsChecked.Equals(true))
			{
				canAreaButtonBeSeen = true;
				byte[] curKey = defaultKey;
				Canvas imageCanvas = FindVisualChildByName<Canvas>(FR, "zoomCanvas");
				if (viewStatusIndex.Equals(PageMode.SinglePage))
				{
					CheckAndProduceAreaButton(curPageIndex, -1, curKey, imageCanvas);
				}
				else if (viewStatusIndex.Equals(PageMode.DoublePage))
				{
					int doubleCurPageIndex = curPageIndex;
					if (doubleCurPageIndex.Equals(0) || doubleCurPageIndex.Equals(singleThumbnailImageAndPageList.Count - 1))
					{
						CheckAndProduceAreaButton(curPageIndex, -1, curKey, imageCanvas);
					}
					else
					{
						doubleCurPageIndex = getSingleCurPageIndex(doubleCurPageIndex);
						int leftCurPageIndex = doubleCurPageIndex - 1;
						int rightCurPageIndex = doubleCurPageIndex;
						if (hejMetadata.direction.Equals("right"))
						{
							leftCurPageIndex = doubleCurPageIndex;
							rightCurPageIndex = doubleCurPageIndex - 1;
						}
						CheckAndProduceAreaButton(leftCurPageIndex, rightCurPageIndex, curKey, imageCanvas);
					}
				}
				curKey = null;
			}
			else if (checkBox.IsChecked.Equals(false))
			{
				canAreaButtonBeSeen = false;
				Canvas stageCanvasInReader = GetStageCanvasInReader();
				if (stageCanvasInReader.Children.Count > 0)
				{
					stageCanvasInReader.Children.Clear();
				}
			}
			resetFocusBackToReader();
		}

		private bool setNotesInMem(string text, int targetPageIndex)
		{
			bool result = false;
			DateTime value = new DateTime(1970, 1, 1);
			long num = DateTime.Now.ToUniversalTime().Subtract(value).Ticks / 10000000;
			bool flag = false;
			NoteData noteData = null;
			if (bookNoteDictionary.ContainsKey(curPageIndex))
			{
				noteData = bookNoteDictionary[targetPageIndex];
				if (noteData.text == text)
				{
					return result;
				}
				noteData.text = text;
				noteData.updatetime = num;
				if (noteData.text != "")
				{
					noteData.status = "0";
					flag = true;
				}
				else
				{
					noteData.status = "1";
					flag = false;
				}
			}
			else
			{
				if (text == "")
				{
					return result;
				}
				noteData = new NoteData();
				noteData.objectId = "";
				noteData.createtime = num;
				noteData.updatetime = num;
				noteData.text = text;
				noteData.index = targetPageIndex;
				noteData.status = "0";
				noteData.synctime = 0L;
				bookNoteDictionary.Add(targetPageIndex, noteData);
				flag = false;
			}
			bookManager.saveNoteData(userBookSno, flag, noteData);
			return true;
		}

		private void searchTB_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				StackPanel mediaListPanelInReader = GetMediaListPanelInReader();
				TextBox textBox = FindVisualChildByName<TextBox>(FR, "searchBar");
				string text = textBox.Text;
				double width = mediaListPanelInReader.Width;
				mediaListPanelInReader.Children.Clear();
				StackPanel searchPanelSet = getSearchPanelSet(width, text);
				ListBox element = hyftdSearch(text);
				StackPanel stackPanel = new StackPanel();
				stackPanel.Children.Add(searchPanelSet);
				stackPanel.Children.Add(element);
				RelativePanel[MediaCanvasOpenedBy.SearchButton] = stackPanel;
				mediaListPanelInReader.Children.Add(stackPanel);
			}
		}

		private void searchButton_Click(object sender, RoutedEventArgs e)
		{
			StackPanel mediaListPanelInReader = GetMediaListPanelInReader();
			TextBox textBox = FindVisualChildByName<TextBox>(FR, "searchBar");
			string text = textBox.Text;
			double width = mediaListPanelInReader.Width;
			mediaListPanelInReader.Children.Clear();
			StackPanel searchPanelSet = getSearchPanelSet(width, text);
			ListBox element = hyftdSearch(text);
			StackPanel stackPanel = new StackPanel();
			stackPanel.Children.Add(searchPanelSet);
			stackPanel.Children.Add(element);
			RelativePanel[MediaCanvasOpenedBy.SearchButton] = stackPanel;
			mediaListPanelInReader.Children.Add(stackPanel);
		}

		private void lb_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ListBox listBox = (ListBox)sender;
			if (listBox.SelectedIndex != -1)
			{
				int targetPage = ((SearchRecord)e.AddedItems[0]).targetPage;
				int num = targetPage - 1;
				if (viewStatusIndex.Equals(PageMode.DoublePage))
				{
					num = getDoubleCurPageIndex(num);
				}
				if (!num.Equals(-1))
				{
					bringBlockIntoView(num);
				}
				listBox.SelectedIndex = -1;
			}
		}

		private StackPanel getTocNcx()
		{
			StackPanel mediaListPanelInReader = GetMediaListPanelInReader();
			StackPanel stackPanel = new StackPanel();
			TreeView treeView = new TreeView();
			double height = mediaListPanelInReader.Height;
			stackPanel.Width = mediaListPanelInReader.Width;
			treeView.Height = height;
			foreach (XmlNode childNode in XmlDocNcx.ChildNodes)
			{
				if (childNode.Name == "ncx")
				{
					foreach (XmlNode childNode2 in childNode.ChildNodes)
					{
						if (childNode2.Name == "navMap")
						{
							foreach (XmlNode childNode3 in childNode2.ChildNodes)
							{
								TreeViewItem treeViewItem = new TreeViewItem();
								AddTreeNode(childNode3, treeViewItem);
								treeViewItem.IsExpanded = true;
								treeView.Items.Add(treeViewItem);
							}
						}
					}
				}
			}
			treeView.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(rootTree_SelectedItemChanged);
			treeView.Style = (Style)FindResource("ContentTreeViewStyle");
			treeView.BorderBrush = System.Windows.Media.Brushes.White;
			stackPanel.Children.Clear();
			stackPanel.Children.Add(treeView);
			return stackPanel;
		}

		private void rootTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			string value = (string)((TreeViewItem)e.NewValue).Tag;
			int num = 0;
			for (int i = 0; i < hejMetadata.LImgList.Count; i++)
			{
				if (hejMetadata.LImgList[i].path.Replace("HYWEB\\", "").Equals(value))
				{
					if (viewStatusIndex.Equals(PageMode.SinglePage))
					{
						num = i;
					}
					else if (viewStatusIndex.Equals(PageMode.DoublePage))
					{
						num = getDoubleCurPageIndex(i);
					}
					break;
				}
			}
			if (num != -1)
			{
				bringBlockIntoView(num);
			}
		}

		private void TreeViewItem_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
		{
			e.Handled = true;
		}

		private void AddTreeNode(XmlNode firstNode, TreeViewItem layer1)
		{
			foreach (XmlNode childNode in firstNode.ChildNodes)
			{
				if (childNode.Name == "navLabel")
				{
					layer1.Header = childNode.InnerText;
				}
				else if (childNode.Name == "content")
				{
					layer1.Tag = childNode.Attributes.GetNamedItem("src").Value;
				}
				else if (childNode.HasChildNodes)
				{
					TreeViewItem treeViewItem = new TreeViewItem();
					AddTreeNode(childNode, treeViewItem);
					treeViewItem.IsExpanded = true;
					layer1.Items.Add(treeViewItem);
				}
			}
		}

		private void RepeatButton_Click_1(object sender, RoutedEventArgs e)
		{
			if (zoomStep != 0)
			{
				zoomStep--;
				ZoomImage(zoomStepScale[zoomStep], zoomStepScale[0], false);
			}
		}

		private void RepeatButton_Click_2(object sender, RoutedEventArgs e)
		{
			if (zoomStep != zoomStepScale.Length - 1)
			{
				zoomStep++;
				ZoomImage(zoomStepScale[zoomStep], zoomStepScale[zoomStepScale.Length - 1], true);
			}
		}

		private void SliderInReader_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			try
			{
				if (e.NewValue > e.OldValue)
				{
					if (zoomStep != zoomStepScale.Length - 1)
					{
						zoomStep++;
						ZoomImage(zoomStepScale[zoomStep], zoomStepScale[zoomStepScale.Length - 1], true, false);
					}
				}
				else if (zoomStep != 0)
				{
					zoomStep--;
					ZoomImage(zoomStepScale[zoomStep], zoomStepScale[0], false, false);
				}
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}

		private void ZoomImage(double imageScale, double scaleMaxOrMin, bool Maximum)
		{
			ZoomImage(imageScale, scaleMaxOrMin, Maximum, false);
		}

		private void RepaintPDF(double imageScale)
		{
			lastTimeOfChangingPage = DateTime.Now;
			PDFScale = (float)imageScale;
			if (viewStatusIndex.Equals(PageMode.SinglePage))
			{
				_003C_003Ec__DisplayClass33 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass33();
				_003C_003Ec__DisplayClass.singlePagePair = singleReadPagePair[curPageIndex];
				_003C_003Ec__DisplayClass.imagePath = _003C_003Ec__DisplayClass.singlePagePair.leftImagePath;
				if (File.Exists(_003C_003Ec__DisplayClass.imagePath))
				{
					_003C_003Ec__DisplayClass35 _003C_003Ec__DisplayClass2 = new _003C_003Ec__DisplayClass35();
					_003C_003Ec__DisplayClass2.CS_0024_003C_003E8__locals34 = _003C_003Ec__DisplayClass;
					_003C_003Ec__DisplayClass2._003C_003E4__this = this;
					_003C_003Ec__DisplayClass2.bd = GetBorderInReader();
					Thread thread = new Thread(new ThreadStart(_003C_003Ec__DisplayClass2._003CRepaintPDF_003Eb__30));
					thread.Name = PDFScale.ToString();
					zoomeThread.Add(thread);
				}
			}
			else
			{
				if (!viewStatusIndex.Equals(PageMode.DoublePage))
				{
					return;
				}
				ThreadStart threadStart = null;
				_003C_003Ec__DisplayClass38 _003C_003Ec__DisplayClass3 = new _003C_003Ec__DisplayClass38();
				_003C_003Ec__DisplayClass3._003C_003E4__this = this;
				_003C_003Ec__DisplayClass3.bd = GetBorderInReader();
				_003C_003Ec__DisplayClass3.item = doubleReadPagePair[curPageIndex];
				if (_003C_003Ec__DisplayClass3.item.rightPageIndex == -1)
				{
					if (File.Exists(_003C_003Ec__DisplayClass3.item.leftImagePath))
					{
						if (threadStart == null)
						{
							threadStart = new ThreadStart(_003C_003Ec__DisplayClass3._003CRepaintPDF_003Eb__31);
						}
						Thread thread2 = new Thread(threadStart);
						thread2.Name = PDFScale.ToString();
						zoomeThread.Add(thread2);
					}
					return;
				}
				ThreadStart threadStart2 = null;
				_003C_003Ec__DisplayClass3b _003C_003Ec__DisplayClass3b = new _003C_003Ec__DisplayClass3b();
				_003C_003Ec__DisplayClass3b.CS_0024_003C_003E8__locals39 = _003C_003Ec__DisplayClass3;
				_003C_003Ec__DisplayClass3b.leftImagePath = _003C_003Ec__DisplayClass3.item.leftImagePath;
				_003C_003Ec__DisplayClass3b.rightImagePath = _003C_003Ec__DisplayClass3.item.rightImagePath;
				if (File.Exists(_003C_003Ec__DisplayClass3b.leftImagePath) && File.Exists(_003C_003Ec__DisplayClass3b.rightImagePath))
				{
					if (threadStart2 == null)
					{
						threadStart2 = new ThreadStart(_003C_003Ec__DisplayClass3b._003CRepaintPDF_003Eb__32);
					}
					Thread thread3 = new Thread(threadStart2);
					thread3.Name = PDFScale.ToString();
					zoomeThread.Add(thread3);
				}
			}
		}

		private void ReadWindow_imageSourceRendered(object sender, imageSourceRenderedResultEventArgs e)
		{
			imageSourceRendered -= new EventHandler<imageSourceRenderedResultEventArgs>(ReadWindow_imageSourceRendered);
			isPDFRendering = false;
			if (curPageIndex.Equals(e.renderPageIndex))
			{
				if (PDFScale.Equals(e.sourceScale))
				{
					BitmapImage imgSource = e.imgSource;
					setImgSource(imgSource, e.sourceScale);
				}
				else
				{
					for (int num = zoomeThread.Count - 1; num >= 0; num--)
					{
						if (PDFScale.Equals((float)Convert.ToDouble(zoomeThread[num].Name)))
						{
							try
							{
								zoomeThread[num].Start();
								imageSourceRendered += new EventHandler<imageSourceRenderedResultEventArgs>(ReadWindow_imageSourceRendered);
								isPDFRendering = true;
							}
							catch
							{
								continue;
							}
							break;
						}
					}
				}
			}
			else
			{
				isPDFRendering = false;
				zoomeThread.Clear();
			}
			e.imgSource = null;
		}

		private void setImgSource(BitmapImage imgSource, float pdfScale)
		{
			setImgSourceCallback method = new setImgSourceCallback(setImgSourceDelegate);
			base.Dispatcher.Invoke(method, imgSource, pdfScale);
		}

		private void setImgSourceDelegate(BitmapImage imgSource, float pdfScale)
		{
			useOriginalCanvasOnLockStatus = false;
			SendImageSourceToZoomCanvas(imgSource);
			zoomeThread.Clear();
			isPDFRendering = false;
			imgSource = null;
		}

		private void ZoomImage(double imageScale, double scaleMaxOrMin, bool Maximum, bool isSlide)
		{
			if (bookType.Equals(BookType.PHEJ))
			{
				RepaintPDF(imageScale);
			}
			imageZoom(imageScale, scaleMaxOrMin, Maximum, isSlide);
			hyperlinkZoom(imageScale, scaleMaxOrMin, Maximum, isSlide);
			if (needToSendBroadCast)
			{
				StackPanel stackPanel = (StackPanel)GetImageInReader();
				TranslateTransform translateTransform5 = (TranslateTransform)tfgForImage.Children[1];
				ScaleTransform scaleTransform = (ScaleTransform)tfgForImage.Children[0];
				double num = Math.Abs(stackPanel.ActualWidth * scaleTransform.ScaleX - base.ActualWidth * ratio) / 2.0;
				double num3 = Math.Abs(stackPanel.ActualHeight * scaleTransform.ScaleY - base.ActualHeight * ratio) / 2.0 / num;
				double x = 0.5;
				double num2 = 0.5;
				TranslateTransform translateTransform = (TranslateTransform)tfgForHyperLink.Children[1];
				translateTransform.X = x;
				translateTransform.Y = 0.0;
				TranslateTransform translateTransform2 = (TranslateTransform)tfgForImage.Children[1];
				translateTransform2.X = x;
				translateTransform2.Y = 0.0;
				TranslateTransform translateTransform3 = (TranslateTransform)tfgForHyperLink.Children[1];
				translateTransform3.X = 0.0;
				translateTransform3.Y = 0.0;
				TranslateTransform translateTransform4 = (TranslateTransform)tfgForImage.Children[1];
				translateTransform4.X = 0.0;
				translateTransform4.Y = 0.0;
				imageOrigin = new System.Windows.Point(translateTransform4.X, translateTransform4.Y);
				hyperlinkOrigin = new System.Windows.Point(translateTransform3.X, translateTransform3.Y);
				sendBroadCast("{\"x\":" + x + ",\"y\":" + num2 + ",\"scale\": " + imageScale + ",\"cmd\":\"R.ZC\"}");
			}
			else
			{
				needToSendBroadCast = true;
			}
		}

		private void hyperlinkZoom(double imageScale, double scaleMaxOrMin, bool Maximum, bool isSlide)
		{
			StackPanel stackPanel = (StackPanel)GetImageInReader();
			GetBorderInReader();
			TranslateTransform translateTransform = (TranslateTransform)tfgForHyperLink.Children[1];
			ScaleTransform scaleTransform = (ScaleTransform)tfgForHyperLink.Children[0];
			double scaleX = scaleTransform.ScaleX;
			double scaleY = scaleTransform.ScaleY;
			scaleTransform.ScaleX = imageScale;
			scaleTransform.ScaleY = imageScale;
			if (Maximum)
			{
				scaleTransform.ScaleX = Math.Min(scaleTransform.ScaleX, scaleMaxOrMin);
				scaleTransform.ScaleY = Math.Min(scaleTransform.ScaleY, scaleMaxOrMin);
			}
			else
			{
				scaleTransform.ScaleX = Math.Max(scaleTransform.ScaleX, scaleMaxOrMin);
				scaleTransform.ScaleY = Math.Max(scaleTransform.ScaleY, scaleMaxOrMin);
			}
			translateTransform.X -= translateTransform.X * (scaleX - scaleTransform.ScaleX);
			translateTransform.Y -= translateTransform.Y * (scaleY - scaleTransform.ScaleY);
			translateTransform.X = Math.Min(translateTransform.X, 0.0);
			translateTransform.X = Math.Max(translateTransform.X, 0.0);
			translateTransform.Y = Math.Min(translateTransform.Y, 0.0);
			translateTransform.Y = Math.Max(translateTransform.Y, 0.0);
		}

		private UIElement GetImageInReader()
		{
			int num = curPageIndex;
			Block block = FR.Document.Blocks.FirstBlock;
			UIElement result = new UIElement();
			if (FR.CanGoToPage(num))
			{
				for (int i = 0; i < num; i++)
				{
					block = block.NextBlock;
				}
			}
			if (block != null)
			{
				result = ((BlockUIContainer)block).Child;
			}
			return result;
		}

		private Canvas GetStageCanvasInReader()
		{
			return FindVisualChildByName<Canvas>(FR, "stageCanvas");
		}

		private RadioButton GetMediaListButtonInReader()
		{
			return FindVisualChildByName<RadioButton>(FR, "MediaListButton");
		}

		private Canvas GetMediaTableCanvasInReader()
		{
			return FindVisualChildByName<Canvas>(FR, "MediaTableCanvas");
		}

		private StackPanel GetMediaListPanelInReader()
		{
			return FindVisualChildByName<StackPanel>(FR, "mediaListPanel");
		}

		private Border GetBorderInReader()
		{
			return FindVisualChildByName<Border>(FR, "PART_ContentHost");
		}

		public static T FindVisualChildByName<T>(DependencyObject parent, string name) where T : DependencyObject
		{
			if (parent != null)
			{
				for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
				{
					DependencyObject child = VisualTreeHelper.GetChild(parent, i);
					string a = child.GetValue(FrameworkElement.NameProperty) as string;
					if (a == name)
					{
						return child as T;
					}
					T val = FindVisualChildByName<T>(child, name);
					if (val != null)
					{
						return val;
					}
				}
			}
			return null;
		}

		private void ImageInReader_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Border borderInReader = GetBorderInReader();
			start = e.GetPosition(borderInReader);
			if (e.ClickCount.Equals(2))
			{
				resetTransform();
				return;
			}
			if (sender is StackPanel)
			{
				((StackPanel)sender).MouseMove += new MouseEventHandler(ReadWindow_MouseMove);
				((StackPanel)sender).PreviewMouseLeftButtonUp += new MouseButtonEventHandler(ReadWindow_PreviewMouseLeftButtonUp);
			}
			else if (sender is Canvas)
			{
				((Canvas)sender).MouseMove += new MouseEventHandler(ReadWindow_MouseMove);
				((Canvas)sender).PreviewMouseLeftButtonUp += new MouseButtonEventHandler(ReadWindow_PreviewMouseLeftButtonUp);
			}
			e.Handled = true;
		}

		private void setTransformBetweenSingleAndDoublePage()
		{
			TranslateTransform translateTransform = (TranslateTransform)tfgForImage.Children[1];
			translateTransform.X = 0.0;
			TranslateTransform translateTransform2 = (TranslateTransform)tfgForHyperLink.Children[1];
			translateTransform2.X = 0.0;
		}

		private void resetTransform()
		{
			TranslateTransform translateTransform = (TranslateTransform)tfgForImage.Children[1];
			ScaleTransform scaleTransform = (ScaleTransform)tfgForImage.Children[0];
			translateTransform.X = 0.0;
			translateTransform.Y = 0.0;
			scaleTransform.ScaleX = 1.0;
			scaleTransform.ScaleY = 1.0;
			TranslateTransform translateTransform2 = (TranslateTransform)tfgForHyperLink.Children[1];
			ScaleTransform scaleTransform2 = (ScaleTransform)tfgForHyperLink.Children[0];
			translateTransform2.X = 0.0;
			translateTransform2.Y = 0.0;
			scaleTransform2.ScaleX = 1.0;
			scaleTransform2.ScaleY = 1.0;
			Slider slider = FindVisualChildByName<Slider>(FR, "SliderInReader");
			slider.ValueChanged -= new RoutedPropertyChangedEventHandler<double>(SliderInReader_ValueChanged);
			slider.Value = scaleTransform.ScaleY;
			slider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(SliderInReader_ValueChanged);
			LockButton.Visibility = Visibility.Visible;
			if (zoomStep != 0)
			{
				zoomStep = 0;
				ZoomImage(zoomStepScale[zoomStep], zoomStepScale[0], false, false);
				sendBroadCast("{\"x\":0.500000,\"y\":0.500000,\"scale\":1.000000,\"cmd\":\"R.ZC\"}");
			}
		}

		private void ReadWindow_MouseMove(object sender, MouseEventArgs e)
		{
			StackPanel stackPanel = (StackPanel)GetImageInReader();
			TranslateTransform translateTransform = (TranslateTransform)tfgForImage.Children[1];
			ScaleTransform scaleTransform = (ScaleTransform)tfgForImage.Children[0];
			TranslateTransform translateTransform2 = (TranslateTransform)tfgForHyperLink.Children[1];
			ScaleTransform scaleTransform2 = (ScaleTransform)tfgForHyperLink.Children[0];
			Border borderInReader = GetBorderInReader();
			Vector v = start - e.GetPosition(borderInReader);
			double num = base.ActualHeight / base.ActualWidth;
			double num2 = stackPanel.ActualHeight / stackPanel.ActualWidth;
			if (e.LeftButton != 0)
			{
				moveImage(v);
			}
		}

		private void moveImage(Vector v)
		{
			StackPanel stackPanel = (StackPanel)GetImageInReader();
			TranslateTransform translateTransform = (TranslateTransform)tfgForImage.Children[1];
			ScaleTransform scaleTransform = (ScaleTransform)tfgForImage.Children[0];
			TranslateTransform translateTransform2 = (TranslateTransform)tfgForHyperLink.Children[1];
			ScaleTransform scaleTransform2 = (ScaleTransform)tfgForHyperLink.Children[0];
			if (hejMetadata.direction.Equals("right"))
			{
				v.X = 0.0 - v.X;
			}
			double num = base.ActualHeight / base.ActualWidth;
			double num2 = stackPanel.ActualHeight / stackPanel.ActualWidth;
			if (scaleTransform.ScaleX != 1.0 && scaleTransform.ScaleY != 1.0)
			{
				translateTransform.X = imageOrigin.X - v.X;
				translateTransform.Y = imageOrigin.Y - v.Y;
				if (num < num2)
				{
					if (stackPanel.ActualWidth * scaleTransform.ScaleX < base.ActualWidth * ratio)
					{
						translateTransform.X = 0.0;
						translateTransform.Y = Math.Min(translateTransform.Y, (Math.Abs(stackPanel.ActualHeight * scaleTransform.ScaleY) - base.ActualHeight * ratio) / 2.0);
						translateTransform.Y = Math.Max(translateTransform.Y, 0.0 - (Math.Abs(stackPanel.ActualHeight * scaleTransform.ScaleY) - base.ActualHeight * ratio) / 2.0);
					}
					else
					{
						translateTransform.X = Math.Min(translateTransform.X, (Math.Abs(stackPanel.ActualWidth * scaleTransform.ScaleX) - base.ActualWidth * ratio) / 2.0);
						translateTransform.X = Math.Max(translateTransform.X, 0.0 - (Math.Abs(stackPanel.ActualWidth * scaleTransform.ScaleX) - base.ActualWidth * ratio) / 2.0);
						translateTransform.Y = Math.Min(translateTransform.Y, (Math.Abs(stackPanel.ActualHeight * scaleTransform.ScaleY) - base.ActualHeight * ratio) / 2.0);
						translateTransform.Y = Math.Max(translateTransform.Y, 0.0 - (Math.Abs(stackPanel.ActualHeight * scaleTransform.ScaleY) - base.ActualHeight * ratio) / 2.0);
					}
				}
				else if (stackPanel.ActualHeight * scaleTransform.ScaleY < base.ActualHeight * ratio)
				{
					translateTransform.Y = 0.0;
					translateTransform.X = Math.Min(translateTransform.X, (Math.Abs(stackPanel.ActualWidth * scaleTransform.ScaleX) - base.ActualWidth * ratio) / 2.0);
					translateTransform.X = Math.Max(translateTransform.X, 0.0 - (Math.Abs(stackPanel.ActualWidth * scaleTransform.ScaleX) - base.ActualWidth * ratio) / 2.0);
				}
				else
				{
					translateTransform.Y = Math.Min(translateTransform.Y, (Math.Abs(stackPanel.ActualHeight * scaleTransform.ScaleY) - base.ActualHeight * ratio) / 2.0);
					translateTransform.Y = Math.Max(translateTransform.Y, 0.0 - (Math.Abs(stackPanel.ActualHeight * scaleTransform.ScaleY) - base.ActualHeight * ratio) / 2.0);
					translateTransform.X = Math.Min(translateTransform.X, (Math.Abs(stackPanel.ActualWidth * scaleTransform.ScaleX) - base.ActualWidth * ratio) / 2.0);
					translateTransform.X = Math.Max(translateTransform.X, 0.0 - (Math.Abs(stackPanel.ActualWidth * scaleTransform.ScaleX) - base.ActualWidth * ratio) / 2.0);
				}
			}
			Canvas canvas = FindVisualChildByName<Canvas>(FR, "zoomCanvas");
			double num3 = canvas.Height / stackPanel.ActualHeight;
			translateTransform2.X = translateTransform.X * num3;
			translateTransform2.Y = translateTransform.Y * num3;
			if (hejMetadata.direction.Equals("right"))
			{
				translateTransform2.X = (0.0 - translateTransform.X) * num3;
			}
		}

		private StackPanel toShareBook()
		{
			StackPanel stackPanel = new StackPanel();
			List<ShareButton> list = new List<ShareButton>();
			list.Add(new ShareButton("Assets/ReadWindow/icon_f.png", "Facebook", SharedPlatform.Facebook, true));
			list.Add(new ShareButton("Assets/ReadWindow/icon_p.png", "Plurk", SharedPlatform.Plurk, false));
			list.Add(new ShareButton("Assets/ReadWindow/icon_m.png", "Mail", SharedPlatform.Mail, false));
			list.Add(new ShareButton("Assets/ReadWindow/icon_g.png", "Google+", SharedPlatform.Google, false));
			list.Add(new ShareButton("Assets/ReadWindow/icon_t.png", "Twitter", SharedPlatform.Twitter, false));
			List<ShareButton> itemsSource = list;
			ListBox listBox = new ListBox();
			listBox.Style = (Style)FindResource("ShareListBoxStyle");
			listBox.ItemsSource = itemsSource;
			stackPanel.Children.Add(listBox);
			return stackPanel;
		}

		private void sharePlatformButton_Click(object sender, RoutedEventArgs e)
		{
			SharedPlatform whichPlatform = (SharedPlatform)((RadioButton)sender).Tag;
			StartSharing(whichPlatform);
		}

		private void StartSharing(SharedPlatform whichPlatform)
		{
			string tweetData = getTweetData(whichPlatform);
			if (tweetData.Equals("") || !checkIfSharedTooMuch())
			{
				return;
			}
			BookThumbnail bookThumbnail = (BookThumbnail)selectedBook;
			if (whichPlatform.Equals(SharedPlatform.Facebook))
			{
				string fileName = "http://www.facebook.com/sharer/sharer.php?u=" + Uri.EscapeDataString(tweetData);
				Process.Start(fileName);
			}
			else if (!whichPlatform.Equals(SharedPlatform.Plurk))
			{
				if (whichPlatform.Equals(SharedPlatform.Mail))
				{
					string text = "";
					string text2 = "";
					text = langMng.getLangString("recommend") + "【" + bookThumbnail.title + "】" + langMng.getLangString("thisEBook") + " P." + (curPageIndex + 1) + " " + langMng.getLangString("forYou");
					text2 = langMng.getLangString("imReading") + "【" + bookThumbnail.title + "】" + langMng.getLangString("thisEBook") + " P." + (curPageIndex + 1) + langMng.getLangString("recommend") + langMng.getLangString("forYou") + langMng.getLangString("welcomeToReader");
					if (!bookType.Equals(BookType.EPUB) && !bookType.Equals(BookType.HEJ))
					{
						bookType.Equals(BookType.PHEJ);
					}
					text2 += "%0A";
					text2 += Uri.EscapeDataString(tweetData);
					text2 += "%0A ";
					string text3 = "";
					Process.Start("mailto://" + text3 + "?subject=" + text + "&body=" + text2);
				}
				else if (!whichPlatform.Equals(SharedPlatform.Google))
				{
					whichPlatform.Equals(SharedPlatform.Twitter);
				}
			}
			bookThumbnail = null;
		}

		private string getTweetData(SharedPlatform platform)
		{
			string url = "http://openebook.hyread.com.tw/tweetservice/rest/BookInfo/add";
			XmlDocument xmlDocument = new XmlDocument();
			XMLTool xMLTool = new XMLTool();
			xmlDocument.LoadXml("<body></body>");
			BookThumbnail bookThumbnail = (BookThumbnail)selectedBook;
			try
			{
				if (bookType.Equals(BookType.HEJ) || bookType.Equals(BookType.PHEJ))
				{
					xMLTool.appendChildToXML("unit", bookThumbnail.vendorId, xmlDocument);
					xMLTool.appendChildToXML("type", platform.GetHashCode().ToString(), xmlDocument);
					xMLTool.appendChildToXML("bookid", bookThumbnail.bookID, xmlDocument);
					xMLTool.appendCDATAChildToXML("title", bookThumbnail.title, xmlDocument);
					xMLTool.appendCDATAChildToXML("author", bookThumbnail.author, xmlDocument);
					xMLTool.appendCDATAChildToXML("publisher", bookThumbnail.publisher, xmlDocument);
					xMLTool.appendChildToXML("publishdate", bookThumbnail.publishDate.Replace("/", "-"), xmlDocument);
					xMLTool.appendChildToXML("pages", bookThumbnail.totalPages.ToString(), xmlDocument);
					xMLTool.appendChildToXML("size", "123456", xmlDocument);
					xMLTool.appendChildToXML("direction", hejMetadata.direction, xmlDocument);
					xMLTool.appendChildToXML("comment", "", xmlDocument);
					xMLTool.appendChildToXML("page", (curPageIndex + 1).ToString(), xmlDocument);
					xMLTool.appendChildToXML("userid", bookThumbnail.userId, xmlDocument);
					xMLTool.appendChildToXML("username", bookThumbnail.userId, xmlDocument);
					xMLTool.appendChildToXML("email", "", xmlDocument);
					xMLTool.appendChildToXML("comment", "", xmlDocument);
				}
			}
			catch
			{
			}
			try
			{
				string uriString = bookPath + "\\" + hejMetadata.SImgList[0].path;
				byte[] byteArrayFromImage = getByteArrayFromImage(new BitmapImage(new Uri(uriString)));
				xMLTool.appendCDATAChildToXML("coverpic", Convert.ToBase64String(byteArrayFromImage), xmlDocument);
				Bitmap bitmap = null;
				if (File.Exists(bookPath + "/hyweb/strokes/" + hejMetadata.LImgList[curPageIndex].pageId + ".isf"))
				{
					InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
					InkCanvas inkCanvas2 = new InkCanvas();
					inkCanvas2.Background = System.Windows.Media.Brushes.Transparent;
					FileStream fileStream = new FileStream(bookPath + "/hyweb/strokes/" + hejMetadata.LImgList[curPageIndex].pageId + ".isf", FileMode.Open);
					inkCanvas2.Strokes = new StrokeCollection(fileStream);
					fileStream.Close();
					System.Windows.Size size = new System.Windows.Size(inkCanvas.Width, inkCanvas.Height);
					inkCanvas2.Measure(size);
					inkCanvas2.Arrange(new Rect(size));
					RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)size.Width, (int)size.Height, DpiX, DpiY, PixelFormats.Pbgra32);
					renderTargetBitmap.Render(inkCanvas2);
					using (MemoryStream stream = new MemoryStream())
					{
						PngBitmapEncoder pngBitmapEncoder = new PngBitmapEncoder();
						pngBitmapEncoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
						pngBitmapEncoder.Save(stream);
						bitmap = new Bitmap(stream);
					}
				}
				string text = bookPath + "\\" + hejMetadata.LImgList[curPageIndex].path;
				if (hejMetadata.LImgList[curPageIndex].path.Contains("tryPageEnd"))
				{
					text = hejMetadata.LImgList[curPageIndex].path;
				}
				BitmapImage imageC = null;
				if (bookType.Equals(BookType.HEJ))
				{
					imageC = getHEJSingleBitmapImage(caTool, defaultKey, text, 1f);
				}
				else if (bookType.Equals(BookType.PHEJ))
				{
					imageC = getPHEJSingleBitmapImage(caTool, defaultKey, text, 1f);
				}
				byte[] byteArrayFromImage2 = getByteArrayFromImage(imageC);
				imageC = null;
				Bitmap bitmap2 = null;
				try
				{
					Bitmap bitmap3 = new Bitmap(new MemoryStream(byteArrayFromImage2));
					int width = Convert.ToInt32(bitmap3.Width);
					int height = Convert.ToInt32(bitmap3.Height);
					bitmap2 = new Bitmap(width, height);
					using (Graphics graphics = Graphics.FromImage(bitmap2))
					{
						graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
						graphics.DrawImage(bitmap3, 0, 0, width, height);
						if (bitmap != null)
						{
							graphics.DrawImage(bitmap, 0, 0, width, height);
						}
						graphics.Dispose();
					}
					bitmap = null;
					bitmap3 = null;
					GC.Collect();
				}
				catch
				{
				}
				Bitmap bitmap4 = null;
				try
				{
					int num = Convert.ToInt32(bitmap2.Width);
					int num2 = Convert.ToInt32(bitmap2.Height);
					double num3 = 1024 / num;
					int width2 = 1024;
					int height2 = (int)((double)num2 * num3);
					bitmap4 = new Bitmap(width2, height2);
					using (Graphics graphics2 = Graphics.FromImage(bitmap4))
					{
						graphics2.PixelOffsetMode = PixelOffsetMode.HighQuality;
						graphics2.DrawImage(bitmap2, 0, 0, width2, height2);
						graphics2.Dispose();
					}
					bitmap2 = null;
				}
				catch
				{
				}
				byte[] inArray = (byte[])TypeDescriptor.GetConverter(bitmap4).ConvertTo(bitmap4, typeof(byte[]));
				xMLTool.appendCDATAChildToXML("pagepic", Convert.ToBase64String(inArray), xmlDocument);
				inArray = null;
			}
			catch
			{
			}
			HttpRequest httpRequest = new HttpRequest(configMng.saveProxyMode, configMng.saveProxyHttpPort);
			string result = httpRequest.postXMLAndLoadString(url, xmlDocument);
			bookThumbnail = null;
			return result;
		}

		private bool checkIfSharedTooMuch()
		{
			int postTimes = bookManager.getPostTimes(userBookSno);
			if (!postTimes.Equals(-1))
			{
				if (postTimes < allowedSharedTimes)
				{
					postTimes++;
					bookManager.savePostTimes(userBookSno, postTimes);
					return true;
				}
				MessageBox.Show(langMng.getLangString("overShare") + allowedSharedTimes + langMng.getLangString("page"), langMng.getLangString("warning"));
				return false;
			}
			return false;
		}

		private void PrintButton_Checked(object sender, RoutedEventArgs e)
		{
			byte[] curKey = defaultKey;
			System.Windows.Controls.Image image = null;
			if (viewStatusIndex.Equals(PageMode.SinglePage))
			{
				int index = curPageIndex;
				string text = bookPath + "\\" + hejMetadata.LImgList[index].path;
				if (hejMetadata.LImgList[index].path.Contains("tryPageEnd"))
				{
					text = hejMetadata.LImgList[index].path;
				}
				if (File.Exists(text))
				{
					if (bookType.Equals(BookType.HEJ))
					{
						image = getSingleBigPageToReplace(caTool, curKey, text);
					}
					else if (bookType.Equals(BookType.PHEJ))
					{
						image = getPHEJSingleBigPageToReplace(caTool, curKey, text);
					}
					curKey = null;
				}
			}
			if (image != null)
			{
				FixedDocument fixedDocument = new FixedDocument();
				PrintDialog printDialog = new PrintDialog();
				fixedDocument.DocumentPaginator.PageSize = new System.Windows.Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);
				FixedPage fixedPage = new FixedPage();
				if (viewStatusIndex.Equals(PageMode.SinglePage))
				{
					fixedPage.Width = printDialog.PrintableAreaWidth;
					fixedPage.Height = printDialog.PrintableAreaHeight;
					image.Width = printDialog.PrintableAreaWidth;
					image.Height = printDialog.PrintableAreaHeight;
				}
				else if (viewStatusIndex.Equals(PageMode.DoublePage))
				{
					int num = curPageIndex;
					if (num.Equals(0) || num.Equals(doubleThumbnailImageAndPageList.Count - 1))
					{
						fixedPage.Width = printDialog.PrintableAreaWidth;
						fixedPage.Height = printDialog.PrintableAreaHeight;
						image.Width = printDialog.PrintableAreaWidth;
						image.Height = printDialog.PrintableAreaHeight;
					}
					else
					{
						fixedPage.Width = printDialog.PrintableAreaHeight;
						fixedPage.Height = printDialog.PrintableAreaWidth;
						image.Width = printDialog.PrintableAreaHeight;
						image.Height = printDialog.PrintableAreaWidth;
					}
				}
				fixedPage.Children.Add(image);
				PageContent pageContent = new PageContent();
				((IAddChild)pageContent).AddChild((object)fixedPage);
				fixedDocument.Pages.Add(pageContent);
				DV.Document = fixedDocument;
				DV.Visibility = Visibility.Visible;
			}
			image = null;
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			if (DV.Visibility.Equals(Visibility.Visible))
			{
				DV.Visibility = Visibility.Collapsed;
			}
		}

		private void PrintButton_Click(object sender, RoutedEventArgs e)
		{
		}

		private void FR_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
		{
			e.Handled = true;
			if (isSyncing && !isSyncOwner)
			{
				return;
			}
			if (e.Delta.Equals(120))
			{
				if (zoomStep == zoomStepScale.Length - 1)
				{
					return;
				}
				zoomStep++;
				ZoomImage(zoomStepScale[zoomStep], zoomStepScale[zoomStepScale.Length - 1], true, false);
			}
			else if (e.Delta.Equals(-120))
			{
				if (zoomStep == 0)
				{
					return;
				}
				zoomStep--;
				ZoomImage(zoomStepScale[zoomStep], zoomStepScale[0], false, false);
			}
			stopMovingMouseTime = DateTime.Now;
		}

		private void FR_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			Canvas mediaTableCanvasInReader = GetMediaTableCanvasInReader();
			if (mediaTableCanvasInReader.Visibility == Visibility.Visible)
			{
				return;
			}
			if (e.Key == Key.OemPlus && Keyboard.Modifiers == ModifierKeys.Control)
			{
				if (zoomStep == zoomStepScale.Length - 1)
				{
					e.Handled = true;
					return;
				}
				zoomStep++;
				ZoomImage(zoomStepScale[zoomStep], zoomStepScale[zoomStepScale.Length - 1], true, false);
			}
			TranslateTransform translateTransform = (TranslateTransform)tfgForImage.Children[1];
			ScaleTransform scaleTransform2 = (ScaleTransform)tfgForImage.Children[0];
			TranslateTransform translateTransform2 = (TranslateTransform)tfgForHyperLink.Children[1];
			ScaleTransform scaleTransform = (ScaleTransform)tfgForHyperLink.Children[0];
			if (!(scaleTransform.ScaleX > 1.0) || !(scaleTransform.ScaleY > 0.0))
			{
				return;
			}
			switch (e.Key)
			{
			case Key.Left:
				moveImage(new Vector(keyboardMoveParam * -1, 0.0));
				break;
			case Key.Right:
				moveImage(new Vector(keyboardMoveParam, 0.0));
				break;
			case Key.Up:
				moveImage(new Vector(0.0, keyboardMoveParam * -1));
				break;
			case Key.Down:
				moveImage(new Vector(0.0, keyboardMoveParam));
				break;
			case Key.OemMinus:
				if (Keyboard.Modifiers == ModifierKeys.Control && zoomStep != 0)
				{
					zoomStep--;
					ZoomImage(zoomStepScale[zoomStep], zoomStepScale[0], false, false);
				}
				break;
			case Key.D0:
				if (Keyboard.Modifiers == ModifierKeys.Control)
				{
					resetTransform();
				}
				break;
			}
			if (isSameScale)
			{
				imageOrigin = new System.Windows.Point(translateTransform.X, translateTransform.Y);
				hyperlinkOrigin = new System.Windows.Point(translateTransform2.X, translateTransform2.Y);
			}
			else
			{
				double num3 = translateTransform.X = (translateTransform.Y = 0.0);
				double num6 = translateTransform2.X = (translateTransform2.Y = 0.0);
				isSameScale = true;
			}
			e.Handled = true;
		}

		public void run()
		{
			parseJSonFromMessage(msg);
		}

		private void parseJSonFromMessage(string message)
		{
			try
			{
				Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(message);
				SocketClient.GetCurrentTimeInUnixMillis();
				long num = (long)dictionary["sendTime"];
				string text = dictionary["cmd"].ToString();
				if (text.Equals("broadcast"))
				{
					Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
					using (Dictionary<string, object>.Enumerator enumerator = dictionary.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							string text2 = JsonConvert.SerializeObject(enumerator.Current.Value);
							text2 = text2.Substring(1, text2.Length - 2).Replace("\\\"", "\"");
							dictionary2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(text2);
						}
					}
					if (!dictionary2.Count.Equals(0))
					{
						setMsgToAction(dictionary2);
					}
				}
				else if (text.Equals("R.init"))
				{
					Action action = null;
					_003C_003Ec__DisplayClass41 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass41();
					_003C_003Ec__DisplayClass._003C_003E4__this = this;
					_003C_003Ec__DisplayClass.msgStrings = new Dictionary<string, object>();
					using (Dictionary<string, object>.Enumerator enumerator2 = dictionary.GetEnumerator())
					{
						if (enumerator2.MoveNext())
						{
							string text3 = JsonConvert.SerializeObject(enumerator2.Current.Value);
							text3 = text3.Substring(1, text3.Length - 2).Replace("\\\"", "\"");
							_003C_003Ec__DisplayClass.msgStrings = JsonConvert.DeserializeObject<Dictionary<string, object>>(text3);
						}
					}
					if (!_003C_003Ec__DisplayClass.msgStrings.Count.Equals(0))
					{
						TaskFactory factory = Task.Factory;
						if (action == null)
						{
							action = new Action(_003C_003Ec__DisplayClass._003CparseJSonFromMessage_003Eb__3e);
						}
						factory.StartNew(action);
					}
				}
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}

		private void setMsgToAction(Dictionary<string, object> msgStrings)
		{
			setMsgToActionCallback method = new setMsgToActionCallback(setMsgToActionDelegate);
			base.Dispatcher.Invoke(method, msgStrings);
		}

		private void setMsgToActionDelegate(Dictionary<string, object> msgStrings)
		{
			try
			{
				if (!msgStrings.ContainsKey("cmd"))
				{
					FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
					foreach (KeyValuePair<string, object> msgString in msgStrings)
					{
						if (msgString.Value != null)
						{
							switch (msgString.Key)
							{
							case "bookId":
								closeBook = true;
								break;
							case "pageIndex":
								if (msgStrings["pageIndex"] != null)
								{
									string value = msgStrings["pageIndex"].ToString();
									bringBlockIntoView(Convert.ToInt32(value));
								}
								break;
							case "spline":
								try
								{
									splineString = msgStrings["spline"].ToString();
									if (splineString != null && !splineString.Equals(""))
									{
										drawStrokeFromJson(splineString);
									}
								}
								catch
								{
								}
								break;
							}
						}
					}
				}
				else
				{
					string text = msgStrings["cmd"].ToString();
					string text2 = "";
					RadioButton radioButton = FindVisualChildByName<RadioButton>(FR, "NoteButton");
					FindVisualChildByName<RadioButton>(FR, "BookMarkButton");
					InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
					switch (text)
					{
					case "R.AA":
						break;
					case "syncOwner":
					{
						isSyncing = true;
						string value4 = msgStrings["clientId"].ToString();
						inkCanvas.Strokes.Clear();
						if (clientId.Equals(value4))
						{
							deleteAllLocalPenmemoData();
							alterAccountWhenSyncing(true);
							isSyncOwner = true;
							buttonStatusWhenSyncing(Visibility.Collapsed, Visibility.Collapsed);
							loadCurrentStrokes(hejMetadata.LImgList[curPageIndex].pageId);
						}
						else
						{
							deleteAllLocalPenmemoData();
							alterAccountWhenSyncing(false);
							isSyncOwner = false;
							buttonStatusWhenSyncing(Visibility.Visible, Visibility.Visible);
						}
						break;
					}
					case "R.TP":
						try
						{
							text2 = msgStrings["pageIndex"].ToString();
							bringBlockIntoView(Convert.ToInt32(text2));
						}
						catch
						{
						}
						break;
					case "R.SB":
					{
						text2 = msgStrings["pageIndex"].ToString();
						int num7 = Convert.ToInt32(text2);
						string text6 = msgStrings["bookmark"].ToString();
						BookMarkData bookMarkData = new BookMarkData();
						bookMarkData.index = num7;
						bookMarkData.status = "0";
						BookMarkData bookMarkData2 = bookMarkData;
						text6.Equals("1");
						bookMarkData2.status = (text6.Equals("1") ? "0" : "1");
						if (bookMarkDictionary.ContainsKey(num7))
						{
							bookMarkDictionary[num7] = bookMarkData2;
						}
						break;
					}
					case "R.SA":
					{
						text2 = msgStrings["pageIndex"].ToString();
						string text4 = msgStrings["annotation"].ToString();
						TextBox textBox = FindVisualChildByName<TextBox>(FR, "notePanel");
						text4 = (textBox.Text = text4.Replace("\\n", "\n").Replace("\\t", "\t"));
						int num6 = Convert.ToInt32(text2);
						NoteData noteData = new NoteData();
						noteData.bookid = bookId;
						noteData.text = text4;
						noteData.index = num6;
						noteData.status = "0";
						NoteData value5 = noteData;
						bookNoteDictionary[num6] = value5;
						if (textBox.Text.Equals(""))
						{
							radioButton.IsChecked = false;
							TriggerBookMark_NoteButtonOrElse(radioButton);
						}
						else
						{
							radioButton.IsChecked = true;
							TriggerBookMark_NoteButtonOrElse(radioButton);
						}
						break;
					}
					case "R.DPA":
					{
						Canvas mediaTableCanvasInReader = GetMediaTableCanvasInReader();
						if (mediaTableCanvasInReader.Visibility.Equals(Visibility.Visible))
						{
							doUpperRadioButtonClicked(MediaCanvasOpenedBy.NoteButton, radioButton);
						}
						break;
					}
					case "R.CB":
						Close();
						break;
					case "R.SS":
						inkCanvas.Strokes.Clear();
						text2 = msgStrings["pageIndex"].ToString();
						bringBlockIntoView(Convert.ToInt32(text2));
						try
						{
							drawStrokeFromJson(msgStrings["spline"].ToString());
						}
						catch
						{
						}
						break;
					case "R.ZC":
					{
						string text3 = msgStrings["scale"].ToString();
						string value2 = msgStrings["x"].ToString();
						string value3 = msgStrings["y"].ToString();
						double num = Convert.ToDouble(value2);
						double num2 = Convert.ToDouble(value3);
						double num3 = -1.0;
						if (!text3.Equals("-1"))
						{
							num3 = Convert.ToDouble(text3);
							bool flag = false;
							int num4 = -1;
							for (int i = 0; i < zoomStepScale.Length; i++)
							{
								if (Math.Abs(zoomStepScale[i] - num3) < 0.25)
								{
									num3 = zoomStepScale[i];
									num4 = i;
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								num3 = Math.Min(3.0, num3);
								num3 = Math.Max(1.0, num3);
								if (num3 == 3.0)
								{
									num4 = zoomStepScale.Length - 1;
								}
								else if (num3 == 1.0)
								{
									num4 = 0;
								}
							}
							bool flag2 = true;
							if (num4 != zoomStep)
							{
								if (num4 < zoomStep)
								{
									flag2 = false;
								}
								zoomStep = num4;
								if (flag2)
								{
									ZoomImage(num3, zoomStepScale[zoomStepScale.Length - 1], true, false);
								}
								else
								{
									ZoomImage(num3, zoomStepScale[0], false, false);
									resetTransform();
								}
							}
						}
						if (!PDFScale.Equals(1f))
						{
							StackPanel stackPanel = (StackPanel)GetImageInReader();
							TranslateTransform translateTransform = (TranslateTransform)tfgForImage.Children[1];
							TranslateTransform translateTransform2 = (TranslateTransform)tfgForHyperLink.Children[1];
							if (num3.Equals(-1.0))
							{
								num3 = PDFScale;
							}
							translateTransform.X = (0.5 - num) * stackPanel.ActualWidth * num3;
							translateTransform.Y = (0.5 - num2) * stackPanel.ActualHeight * num3;
							if (stackPanel.ActualWidth * num3 < base.ActualWidth * ratio)
							{
								translateTransform.X = 0.0;
								translateTransform.Y = Math.Min(translateTransform.Y, (Math.Abs(stackPanel.ActualHeight * num3) - base.ActualHeight * ratio) / 2.0);
								translateTransform.Y = Math.Max(translateTransform.Y, 0.0 - (Math.Abs(stackPanel.ActualHeight * num3) - base.ActualHeight * ratio) / 2.0);
							}
							else
							{
								translateTransform.X = Math.Min(translateTransform.X, (Math.Abs(stackPanel.ActualWidth * num3) - base.ActualWidth * ratio) / 2.0);
								translateTransform.X = Math.Max(translateTransform.X, 0.0 - (Math.Abs(stackPanel.ActualWidth * num3) - base.ActualWidth * ratio) / 2.0);
								translateTransform.Y = Math.Min(translateTransform.Y, (Math.Abs(stackPanel.ActualHeight * num3) - base.ActualHeight * ratio) / 2.0);
								translateTransform.Y = Math.Max(translateTransform.Y, 0.0 - (Math.Abs(stackPanel.ActualHeight * num3) - base.ActualHeight * ratio) / 2.0);
							}
							Canvas canvas = FindVisualChildByName<Canvas>(FR, "zoomCanvas");
							double num5 = canvas.Height / stackPanel.ActualHeight;
							translateTransform2.X = translateTransform.X * num5;
							translateTransform2.Y = translateTransform.Y * num5;
							if (hejMetadata.direction.Equals("right"))
							{
								translateTransform2.X = (0.0 - translateTransform.X) * num5;
							}
						}
						break;
					}
					}
				}
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}

		private void alterAccountWhenSyncing(bool isSyncOwner)
		{
			account = account.Replace("_Sync", "");
			if (isSyncOwner)
			{
				account += "_Sync";
			}
			getBookPath();
		}

		private void buttonStatusWhenSyncing(Visibility toolBarVisibility, Visibility syncCanvasVisibility)
		{
			FindVisualChildByName<Canvas>(FR, "toolbarSyncCanvas").Visibility = toolBarVisibility;
			FindVisualChildByName<Canvas>(FR, "syncCanvas").Visibility = syncCanvasVisibility;
			ComboBox comboBox = FindVisualChildByName<ComboBox>(FR, "cbBooks");
			RadioButton radioButton = FindVisualChildByName<RadioButton>(FR, "SearchButton");
			RadioButton radioButton2 = FindVisualChildByName<RadioButton>(FR, "PenMemoButton");
			RadioButton radioButton3 = FindVisualChildByName<RadioButton>(FR, "BookMarkButton");
			RadioButton radioButton4 = FindVisualChildByName<RadioButton>(FR, "NoteButton");
			RadioButton radioButton5 = FindVisualChildByName<RadioButton>(FR, "ShareButton");
			RadioButton radioButton6 = FindVisualChildByName<RadioButton>(FR, "BackToBookShelfButton");
			RadioButton radioButton7 = FindVisualChildByName<RadioButton>(FR, "leftPageButton");
			RadioButton radioButton8 = FindVisualChildByName<RadioButton>(FR, "rightPageButton");
			RadioButton radioButton9 = FindVisualChildByName<RadioButton>(FR, "statusBMK");
			RadioButton radioButton10 = FindVisualChildByName<RadioButton>(FR, "statusMemo");
			if (toolBarVisibility.Equals(Visibility.Visible) && syncCanvasVisibility.Equals(Visibility.Visible))
			{
				comboBox.Opacity = 0.5;
				radioButton.Opacity = 0.5;
				radioButton2.Opacity = 0.5;
				radioButton3.Opacity = 0.5;
				radioButton4.Opacity = 0.5;
				radioButton5.Opacity = 0.5;
				radioButton6.Opacity = 0.5;
				LockButton.Opacity = 0.0;
				radioButton7.Opacity = 0.5;
				radioButton8.Opacity = 0.5;
				ShowListBoxButton.Visibility = Visibility.Collapsed;
				if (Settings.Default.IsFlatUIReader)
				{
					if (CheckIsNowClick(MemoSP))
					{
						noteButton_Click();
					}
					NewUITop.Visibility = Visibility.Collapsed;
					NewUI.Visibility = Visibility.Collapsed;
					if (radioButton9 != null)
					{
						radioButton9.Width = 0.0;
						radioButton9.Height = 0.0;
					}
					if (radioButton10 != null)
					{
						radioButton10.Width = 0.0;
						radioButton10.Height = 0.0;
					}
				}
			}
			else
			{
				comboBox.Opacity = 1.0;
				radioButton.Opacity = 1.0;
				radioButton2.Opacity = 1.0;
				radioButton3.Opacity = 1.0;
				radioButton4.Opacity = 1.0;
				radioButton5.Opacity = 1.0;
				radioButton6.Opacity = 1.0;
				LockButton.Opacity = 1.0;
				radioButton7.Opacity = 1.0;
				radioButton8.Opacity = 1.0;
				ShowListBoxButton.Visibility = Visibility.Visible;
				if (Settings.Default.IsFlatUIReader)
				{
					NewUITop.Visibility = Visibility.Visible;
					NewUI.Visibility = Visibility.Visible;
					if (radioButton9 != null)
					{
						radioButton9.Width = 56.0;
						radioButton9.Height = 56.0;
					}
					if (radioButton10 != null)
					{
						radioButton10.Width = 56.0;
						radioButton10.Height = 56.0;
					}
				}
			}
			System.Windows.Controls.Image image = FindVisualChildByName<System.Windows.Controls.Image>(FR, "screenBroadcasting");
			System.Windows.Controls.Image image2 = FindVisualChildByName<System.Windows.Controls.Image>(FR, "screenReceiving");
			System.Windows.Controls.Image image3 = FindVisualChildByName<System.Windows.Controls.Image>(FR, "StatusOnairOff");
			if (isSyncOwner)
			{
				image3.Visibility = Visibility.Collapsed;
				image.Visibility = Visibility.Visible;
				image2.Visibility = Visibility.Collapsed;
			}
			else if (isSyncing)
			{
				image3.Visibility = Visibility.Collapsed;
				image.Visibility = Visibility.Collapsed;
				image2.Visibility = Visibility.Visible;
			}
			else
			{
				image3.Visibility = Visibility.Visible;
				image.Visibility = Visibility.Collapsed;
				image2.Visibility = Visibility.Collapsed;
			}
		}

		private void switchNoteBookMarkShareButtonStatusWhenSyncing()
		{
			ComboBox comboBox = FindVisualChildByName<ComboBox>(FR, "cbBooks");
			RadioButton radioButton = FindVisualChildByName<RadioButton>(FR, "BookMarkButton");
			RadioButton radioButton2 = FindVisualChildByName<RadioButton>(FR, "ShareButton");
			RadioButton radioButton3 = FindVisualChildByName<RadioButton>(FR, "NoteButton");
			if (isSyncing)
			{
				if (isSyncOwner)
				{
					comboBox.Visibility = Visibility.Visible;
				}
				else
				{
					comboBox.Visibility = Visibility.Collapsed;
				}
				radioButton.Visibility = Visibility.Collapsed;
				radioButton2.Visibility = Visibility.Collapsed;
				radioButton3.Visibility = Visibility.Collapsed;
				BookMarkButtonInListBox.Visibility = Visibility.Collapsed;
				NoteButtonInListBox.Visibility = Visibility.Collapsed;
			}
			else
			{
				comboBox.Visibility = Visibility.Visible;
				radioButton.Visibility = Visibility.Visible;
				radioButton2.Visibility = Visibility.Visible;
				radioButton3.Visibility = Visibility.Visible;
				BookMarkButtonInListBox.Visibility = Visibility.Visible;
				NoteButtonInListBox.Visibility = Visibility.Visible;
			}
		}

		private void syncButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				ToggleButton toggleButton = FindVisualChildByName<ToggleButton>(FR, "syncButton");
				Singleton_Socket.ReaderEvent = this;
				string meetingID = meetingId;
				string userID = account;
				string text = userName;
				bool? isChecked = toggleButton.IsChecked;
				socket = Singleton_Socket.GetInstance(meetingID, userID, text, (isChecked.GetValueOrDefault() && isChecked.HasValue) ? true : false);
				if (socket == null)
				{
					toggleButton.IsChecked = false;
					MessageBox.Show("無法連接廣播同步系統", "連線失敗", MessageBoxButton.OK);
				}
				else
				{
					InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
					bool? isChecked2 = toggleButton.IsChecked;
					if (isChecked2.GetValueOrDefault() && (isChecked2.HasValue ? true : false))
					{
						inkCanvas.Strokes.Clear();
						isSyncing = true;
						Singleton_Socket.ReaderEvent = this;
						string meetingID2 = meetingId;
						string userID2 = account;
						string text2 = userName;
						bool? isChecked3 = toggleButton.IsChecked;
						socket = Singleton_Socket.GetInstance(meetingID2, userID2, text2, (isChecked3.GetValueOrDefault() && isChecked3.HasValue) ? true : false);
						socket.syncSwitch(true);
						clearDataWhenSync();
						loadCurrentStrokes(singleReadPagePair[curPageIndex].leftPageIndex);
						resetTransform();
						buttonStatusWhenSyncing(Visibility.Visible, Visibility.Visible);
					}
					else
					{
						inkCanvas.Strokes.Clear();
						isSyncing = false;
						isSyncOwner = false;
						Singleton_Socket.ReaderEvent = this;
						string meetingID3 = meetingId;
						string userID3 = account;
						string text3 = userName;
						bool? isChecked4 = toggleButton.IsChecked;
						socket = Singleton_Socket.GetInstance(meetingID3, userID3, text3, (isChecked4.GetValueOrDefault() && isChecked4.HasValue) ? true : false);
						socket.syncSwitch(false);
						clearDataWhenSync();
						alterAccountWhenSyncing(false);
						getBookPath();
						initUserDataFromDB();
						loadCurrentStrokes(singleReadPagePair[curPageIndex].leftPageIndex);
						resetTransform();
						buttonStatusWhenSyncing(Visibility.Collapsed, Visibility.Collapsed);
					}
					RadioButton radioButton = FindVisualChildByName<RadioButton>(FR, "BookMarkButton");
					if (bookMarkDictionary.ContainsKey(curPageIndex))
					{
						radioButton.IsChecked = ((bookMarkDictionary[curPageIndex].status == "0") ? true : false);
						TriggerBookMark_NoteButtonOrElse(radioButton);
					}
					else
					{
						radioButton.IsChecked = false;
						TriggerBookMark_NoteButtonOrElse(radioButton);
					}
					RadioButton radioButton2 = FindVisualChildByName<RadioButton>(FR, "NoteButton");
					TextBox textBox = FindVisualChildByName<TextBox>(FR, "notePanel");
					if (textBox != null)
					{
						textBox.Text = bookNoteDictionary[curPageIndex].text;
					}
					if (bookNoteDictionary.ContainsKey(curPageIndex))
					{
						if (bookNoteDictionary[curPageIndex].text.Equals(""))
						{
							radioButton2.IsChecked = false;
							TriggerBookMark_NoteButtonOrElse(radioButton2);
						}
						else
						{
							radioButton2.IsChecked = true;
							TriggerBookMark_NoteButtonOrElse(radioButton2);
						}
					}
					else
					{
						radioButton2.IsChecked = false;
						TriggerBookMark_NoteButtonOrElse(radioButton2);
					}
					switchNoteBookMarkShareButtonStatusWhenSyncing();
				}
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}

		private void clearDataWhenSync()
		{
			bookMarkDictionary = new Dictionary<int, BookMarkData>();
			bookNoteDictionary = new Dictionary<int, NoteData>();
			bookStrokesDictionary = new Dictionary<int, List<StrokesData>>();
		}

		private void deleteAllLocalPenmemoData()
		{
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
			inkCanvas.Strokes.Clear();
			alterAccountWhenSyncing(true);
			string sqlCommand = "delete from bookStrokesDetail Where userbook_sno=" + userBookSno;
			bookManager.sqlCommandNonQuery(sqlCommand);
		}

		private void drawStrokeFromJson(string msgString)
		{
			Canvas canvas = FindVisualChildByName<Canvas>(FR, "zoomCanvas");
			try
			{
				List<PemMemoInfos> list = JsonConvert.DeserializeObject<List<PemMemoInfos>>(msgString);
				for (int i = 0; i < list.Count; i++)
				{
					paintStrokeOnInkCanvas(list[i], canvas.Width, canvas.Height);
				}
			}
			catch
			{
			}
		}

		private Dictionary<string, object> convertSrtokeJosonToDic(string msg)
		{
			char[] trimChars = new char[2]
			{
				'[',
				']'
			};
			string text = msg.TrimEnd(trimChars);
			text = text.TrimStart(trimChars);
			text.Split('{');
			return JsonConvert.DeserializeObject<Dictionary<string, object>>(text);
		}

		private void paintStrokeOnInkCanvas(PemMemoInfos strokeJson, double currentInkcanvasWidth, double currentInkcanvasHeight)
		{
			try
			{
				double strokeWidth = strokeJson.strokeWidth;
				double canvasHeight = strokeJson.canvasHeight;
				double canvasWidth = strokeJson.canvasWidth;
				double strokeAlpha = strokeJson.strokeAlpha;
				string strokeColor = strokeJson.strokeColor;
				double num = currentInkcanvasWidth / canvasWidth;
				double num2 = currentInkcanvasHeight / canvasHeight;
				string[] array = strokeJson.points.Split(';');
				char[] trimChars = new char[2]
				{
					'{',
					'}'
				};
				StylusPointCollection stylusPointCollection = new StylusPointCollection();
				for (int i = 0; i < array.Length; i++)
				{
					try
					{
						System.Windows.Point point = default(System.Windows.Point);
						string text = array[i];
						text = text.TrimEnd(trimChars);
						text = text.TrimStart(trimChars);
						point = System.Windows.Point.Parse(text);
						StylusPoint item = default(StylusPoint);
						item.X = point.X * num;
						item.Y = point.Y * num2;
						stylusPointCollection.Add(item);
					}
					catch (Exception ex)
					{
						LogTool.Debug(ex);
					}
				}
				Stroke stroke = new Stroke(stylusPointCollection);
				stroke.DrawingAttributes.FitToCurve = true;
				if (strokeAlpha != 1.0)
				{
					stroke.DrawingAttributes.IsHighlighter = true;
				}
				else
				{
					stroke.DrawingAttributes.IsHighlighter = false;
				}
				stroke.DrawingAttributes.Width = strokeWidth * 5.0;
				stroke.DrawingAttributes.Height = strokeWidth * 5.0;
				new System.Windows.Media.ColorConverter();
				System.Windows.Media.Color color = ConvertHexStringToColour(strokeColor);
				stroke.DrawingAttributes.Color = color;
				if (isSyncing && stroke != null)
				{
					InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
					inkCanvas.Strokes.Add(stroke.Clone());
					stroke = null;
				}
			}
			catch (Exception ex2)
			{
				LogTool.Debug(ex2);
			}
		}

		private System.Windows.Media.Color ConvertHexStringToColour(string hexString)
		{
			byte b = 0;
			byte b2 = 0;
			byte b3 = 0;
			byte b4 = 0;
			if (hexString.Length == 7)
			{
				hexString = hexString.Insert(1, "FF");
			}
			if (hexString.StartsWith("#"))
			{
				hexString = hexString.Substring(1, 8);
			}
			b = Convert.ToByte(int.Parse(hexString.Substring(0, 2), NumberStyles.AllowHexSpecifier));
			b2 = Convert.ToByte(int.Parse(hexString.Substring(2, 2), NumberStyles.AllowHexSpecifier));
			b3 = Convert.ToByte(int.Parse(hexString.Substring(4, 2), NumberStyles.AllowHexSpecifier));
			b4 = Convert.ToByte(int.Parse(hexString.Substring(6, 2), NumberStyles.AllowHexSpecifier));
			return System.Windows.Media.Color.FromArgb(b, b2, b3, b4);
		}

		private Dictionary<string, object> convertStrokeCommandToJson(string jsonStr)
		{
			return JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonStr);
		}

		private void penMemoCanvas_StrokeErased(object sender, RoutedEventArgs e)
		{
			preparePenMemoAndSend(false);
		}

		private void penMemoCanvasStrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
		{
			Stroke stroke = e.Stroke;
			if (stroke != null && (!isSyncing || isSyncOwner))
			{
				saveStrokeToDB(stroke);
				preparePenMemoAndSend(true);
			}
		}

		private void preparePenMemoAndSend(bool Division3 = true)
		{
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
			if (!isSyncOwner || !isSyncing)
			{
				return;
			}
			int count = inkCanvas.Strokes.Count;
			List<PemMemoInfos> list = new List<PemMemoInfos>();
			for (int i = 0; i < count; i++)
			{
				int count2 = inkCanvas.Strokes[i].StylusPoints.Count;
				DrawingAttributes drawingAttributes = inkCanvas.Strokes[i].DrawingAttributes;
				PemMemoInfos pemMemoInfos = new PemMemoInfos();
				pemMemoInfos.strokeWidth = (int)drawingAttributes.Height;
				if (pemMemoInfos.strokeWidth < 1.0)
				{
					pemMemoInfos.strokeWidth = 1.0;
				}
				if (Division3 && pemMemoInfos.strokeWidth > 1.0 && drawingAttributes.FitToCurve)
				{
					pemMemoInfos.strokeWidth = pemMemoInfos.strokeWidth / 3.0 * 0.6;
				}
				else
				{
					pemMemoInfos.strokeWidth *= 0.75;
				}
				if (pemMemoInfos.strokeWidth < 1.0)
				{
					pemMemoInfos.strokeWidth = 1.0;
				}
				pemMemoInfos.canvasHeight = (int)inkCanvas.Height;
				pemMemoInfos.canvasWidth = (int)inkCanvas.Width;
				pemMemoInfos.strokeAlpha = (drawingAttributes.IsHighlighter ? 0.5 : 1.0);
				string text = drawingAttributes.Color.ToString();
				text = (pemMemoInfos.strokeColor = text.Remove(1, 2));
				string text3 = "";
				for (int j = 0; j < count2; j++)
				{
					StylusPoint stylusPoint = inkCanvas.Strokes[i].StylusPoints[j];
					string text4 = text3;
					text3 = text4 + "{" + stylusPoint.X + ", " + stylusPoint.Y + "};";
				}
				text3 = (pemMemoInfos.points = text3.Substring(0, text3.LastIndexOf(';')));
				list.Add(pemMemoInfos);
			}
			string text6 = JsonConvert.SerializeObject(list);
			text6 = text6.Replace("\r\n", "").Replace("[", "").Replace("]", "");
			string[] value = new string[1]
			{
				text6
			};
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("spline", value);
			dictionary.Add("pageIndex", curPageIndex);
			dictionary.Add("cmd", "R.SS");
			string text7 = JsonConvert.SerializeObject(dictionary);
			text7 = text7.Replace("[\"", "[").Replace("\"]", "]").Replace("\\\"", "\"")
				.Replace(" ", "");
			sendBroadCast(text7);
		}

		private void sendBroadCast(string msg)
		{
			if (!isSyncOwner || !isSyncing)
			{
				return;
			}
			Singleton_Socket.ReaderEvent = this;
			socket = Singleton_Socket.GetInstance(meetingId, account, userName, true);
			if (!string.IsNullOrEmpty(msg) && socket != null)
			{
				Console.WriteLine("ReadWindow Sent: " + msg);
				LogTool.Debug(new Exception(msg));
				Singleton_Socket.ReaderEvent = this;
				socket = Singleton_Socket.GetInstance(meetingId, account, userName, true);
				if (socket != null && socket.GetIsConnected())
				{
					socket.broadcast(msg);
				}
			}
		}

		public void enableSyncButton(SocketClient _socket)
		{
			try
			{
				Singleton_Socket.ReaderEvent = this;
				socket = Singleton_Socket.GetInstance(meetingId, account, userName, true);
				if (_socket != null)
				{
					socket = _socket;
					socket.AddEventManager(this);
				}
				FindVisualChildByName<System.Windows.Controls.Image>(FR, "diableImg").Visibility = Visibility.Collapsed;
				FindVisualChildByName<ToggleButton>(FR, "syncButton").Visibility = Visibility.Visible;
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}

		public int getUserBookSno(string bookId, string account, string meetingId)
		{
			string sqlCommand = "Select sno from bookInfo as bi Where bi.bookId ='" + bookId + "' And bi.account ='" + account + "' And bi.meetingId='" + meetingId + "' ";
			QueryResult queryResult = null;
			try
			{
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

		private void getBookPath()
		{
			userBookSno = getUserBookSno(bookId, account, meetingId);
			if (userBookSno.Equals(-1))
			{
				string text = "Insert into bookInfo( bookId, account, meetingId )";
				string text2 = text;
				text = text2 + " values('" + bookId + "', '" + account + "', '" + meetingId + "')";
				bookManager.sqlCommandNonQuery(text);
				userBookSno = getUserBookSno(bookId, account, meetingId);
			}
		}

		private byte[] getCipherKey()
		{
			return new byte[1];
		}

		private bool loadBookXMLFiles()
		{
			HEJMetadataReader hEJMetadataReader = new HEJMetadataReader(bookPath);
			List<string> list = new List<string>();
			list.Add("book.xml");
			list.Add("thumbs_ok");
			list.Add("infos_ok");
			hejMetadata = hEJMetadataReader.getBookMetadata(bookPath + "\\HYWEB\\content.opf", trialPages, "", "");
			return true;
		}

		private void ReadWindow_Loaded(object sender, RoutedEventArgs e)
		{
			lastPageMode = 1;
			checkViewStatus(PageMode.SinglePage);
			RadioButton radioButton = FindVisualChildByName<RadioButton>(FR, "PageViewButton");
			radioButton.IsChecked = true;
			base.Loaded -= new RoutedEventHandler(ReadWindow_Loaded);
			string sqlCommand = "update userbook_metadata set readtimes = readtimes+1 Where Sno= " + userBookSno;
			bookManager.sqlCommandNonQuery(sqlCommand);
			FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
			tempStrokes = new List<Stroke>();
			base.Closing += new CancelEventHandler(ReadWindow_Closing);
			decodedPDFPages[0] = null;
			decodedPDFPages[1] = null;
			if (trialPages > 0)
			{
				downloadProgBar.Visibility = Visibility.Collapsed;
			}
			else
			{
				downloadProgBar.Maximum = hejMetadata.allFileList.Count;
				downloadProgBar.Minimum = 0.0;
				TextBlock textBlock = FindVisualChildByName<TextBlock>(FR, "watermarkTextBlock");
				textBlock.Text = watermark;
				Singleton_Socket.ReaderEvent = this;
				socket = Singleton_Socket.GetInstance(meetingId, account, userName, isSyncing);
				if (socket != null)
				{
					socket.AddEventManager(this);
				}
				else
				{
					ToggleButton toggleButton = FindVisualChildByName<ToggleButton>(FR, "syncButton");
					toggleButton.Visibility = Visibility.Collapsed;
					System.Windows.Controls.Image image = FindVisualChildByName<System.Windows.Controls.Image>(FR, "diableImg");
					image.Visibility = Visibility.Visible;
				}
				tempStrokes = new List<Stroke>();
				checkThumbnailBorderAndMediaListStatus();
				downloadProgBar.Visibility = Visibility.Collapsed;
			}
			loadOriginalStrokeStatus();
			GC.Collect();
			isReadWindowLoaded = true;
			if (Settings.Default.IsFlatUIReader)
			{
				Canvas canvas = FindVisualChildByName<Canvas>(FR, "ToolBarSensor");
				Grid grid = FindVisualChildByName<Grid>(FR, "PenMemoToolBar");
				Grid grid2 = FindVisualChildByName<Grid>(FR, "ToolBarInReader");
				grid.Height = 0.0;
				grid.Width = 0.0;
				grid2.Height = 0.0;
				grid2.Width = 0.0;
				canvas.Height = 0.0;
				canvas.Width = 0.0;
				AttachKey();
				AttachEvent();
			}
			else
			{
				System.Windows.Controls.Image image2 = FindVisualChildTool.ByName<System.Windows.Controls.Image>(FR, "statusBMK");
				System.Windows.Controls.Image image3 = FindVisualChildTool.ByName<System.Windows.Controls.Image>(FR, "statusMemo");
				System.Windows.Controls.Image image4 = FindVisualChildTool.ByName<System.Windows.Controls.Image>(FR, "StatusOnairOff");
				if (image2 != null)
				{
					image2.Width = 0.0;
					image2.Height = 0.0;
					image2.Source = null;
				}
				if (image2 != null)
				{
					image3.Width = 0.0;
					image3.Height = 0.0;
					image2.Source = null;
				}
				if (image2 != null)
				{
					image4.Width = 0.0;
					image4.Height = 0.0;
					image2.Source = null;
				}
			}
			base.ContentRendered += new EventHandler(_003CReadWindow_Loaded_003Eb__45);
		}

		private void InitPen()
		{
			Task.Factory.StartNew(new Action(_003CInitPen_003Eb__47));
		}

		private void AttachEvent()
		{
			MouseButtonEventHandler mouseButtonEventHandler = null;
			MouseButtonEventHandler mouseButtonEventHandler2 = null;
			MouseButtonEventHandler mouseButtonEventHandler3 = null;
			_003C_003Ec__DisplayClass55 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass55();
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			System.Windows.Controls.Image image = FindVisualChildByName<System.Windows.Controls.Image>(FR, "StatusOnairOff");
			System.Windows.Controls.Image image2 = FindVisualChildByName<System.Windows.Controls.Image>(FR, "screenBroadcasting");
			System.Windows.Controls.Image image3 = FindVisualChildByName<System.Windows.Controls.Image>(FR, "screenReceiving");
			_003C_003Ec__DisplayClass.syncButton = FindVisualChildByName<ToggleButton>(FR, "syncButton");
			if (image == null || image2 == null || image2 == null)
			{
				return;
			}
			if (isSyncing)
			{
				image.Visibility = Visibility.Collapsed;
				if (isSyncOwner)
				{
					image2.Visibility = Visibility.Visible;
					image3.Visibility = Visibility.Collapsed;
				}
				else
				{
					image2.Visibility = Visibility.Collapsed;
					image3.Visibility = Visibility.Visible;
				}
			}
			else
			{
				image.Visibility = Visibility.Visible;
			}
			if (mouseButtonEventHandler == null)
			{
				mouseButtonEventHandler = new MouseButtonEventHandler(_003C_003Ec__DisplayClass._003CAttachEvent_003Eb__49);
			}
			image.MouseLeftButtonDown += mouseButtonEventHandler;
			if (mouseButtonEventHandler2 == null)
			{
				mouseButtonEventHandler2 = new MouseButtonEventHandler(_003C_003Ec__DisplayClass._003CAttachEvent_003Eb__4c);
			}
			image2.MouseLeftButtonDown += mouseButtonEventHandler2;
			if (mouseButtonEventHandler3 == null)
			{
				mouseButtonEventHandler3 = new MouseButtonEventHandler(_003C_003Ec__DisplayClass._003CAttachEvent_003Eb__4f);
			}
			image3.MouseLeftButtonDown += mouseButtonEventHandler3;
		}

		private void ReadWindow_Closing(object sender, EventArgs e)
		{
			TextBlock textBlock = FindVisualChildByName<TextBlock>(FR, "TotalPageInReader");
			int totalPage = (!textBlock.Text.Equals("")) ? int.Parse(textBlock.Text) : 0;
			string text = bookPath;
			double primaryScreenWidth = SystemParameters.PrimaryScreenWidth;
			double primaryScreenHeight = SystemParameters.PrimaryScreenHeight;
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
			float width = (float)inkCanvas.Width;
			float height = (float)inkCanvas.Height;
			noteButton_Click();
			string name = Assembly.GetExecutingAssembly().GetName().Name;
			if (Settings.Default.AssemblyName.Contains("TPI4F"))
			{
				string thumbsPath_Msize = System.IO.Path.Combine(bookPath, "hyweb", "mthumbs");
				string thumbsPath_Lsize = System.IO.Path.Combine(bookPath, "hyweb", "mthumbs\\Larger");
				Singleton_PDFFactory.SavePDF(false, text, totalPage, width, height, account, bookId, dbPath, thumbsPath_Msize, thumbsPath_Lsize);
			}
			InitSyncCenter();
			RecordPage();
		}

		private void SavePDF(string bookPath, int totalPage, float width, float height, string UserAccount)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			if (width > height)
			{
				num3 = width;
				num4 = height;
				num = num4;
				num2 = num3;
			}
			else
			{
				num = width;
				num2 = height;
				num3 = num2;
				num4 = num;
			}
			iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(width, height);
			Document document = new Document(pageSize);
			FileStream fileStream = null;
			string text = System.IO.Path.Combine(bookPath, "PDFFactory/PDF.pdf");
			string directoryName = System.IO.Path.GetDirectoryName(text);
			string destFileName = System.IO.Path.Combine(bookPath, "PDF.pdf");
			try
			{
				Directory.CreateDirectory(directoryName);
				File.Create(text).Dispose();
				fileStream = new FileStream(text, FileMode.Create);
				PdfWriter instance = PdfWriter.GetInstance(document, fileStream);
				string[] files = Directory.GetFiles(directoryName, "*.bmp");
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate58 == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate58 = new Comparison<string>(_003CSavePDF_003Eb__57);
				}
				Array.Sort(files, CS_0024_003C_003E9__CachedAnonymousMethodDelegate58);
				document.Open();
				int num5 = 0;
				string name = Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\..\\Fonts\\kaiu.ttf";
				BaseFont bf = BaseFont.CreateFont(name, "Identity-H", false);
				new iTextSharp.text.Font(bf, 16f, 0);
				string text2 = System.IO.Path.Combine(bookPath, "hyweb");
				string text3 = "";
				string text4 = System.IO.Path.Combine(bookPath, "hyweb", "mthumbs");
				string text5 = System.IO.Path.Combine(bookPath, "hyweb", "mthumbs\\Larger");
				text3 = ((!Directory.Exists(text5)) ? text4 : text5);
				string[] files2 = Directory.GetFiles(text2, "*.pdf");
				string text6 = "";
				if (files2.Length > 0)
				{
					string name2 = new FileInfo(files2[0]).Name;
					text6 = name2.Split('_')[0];
				}
				for (int i = 1; i <= totalPage; i++)
				{
					try
					{
						System.IO.Path.Combine(text2, text6 + "_" + i + ".pdf");
						string text7 = System.IO.Path.Combine(text3, text6 + "_" + i + ".jpg");
						string destFileName2 = System.IO.Path.Combine(directoryName, i + ".bmp");
						Directory.CreateDirectory(System.IO.Path.GetDirectoryName(text7));
						File.Exists(text7);
						File.Copy(text7, destFileName2, true);
					}
					catch (Exception ex)
					{
						LogTool.Debug(ex);
					}
				}
				string[] array = files;
				foreach (string text8 in array)
				{
					num5++;
					FileInfo fileInfo = new FileInfo(text8);
					if (fileInfo.Extension.ToLower().Equals(".bmp"))
					{
						string sqlCommand = string.Format("SELECT page,status,alpha,canvasHeight,canvasWidth,color,points,width\r\n                                                FROM bookStrokesDetail as a inner join bookinfo as b on a.userbook_sno=b.sno \r\n                                                where bookid='{0}' and page={1}  and account='{2}'", bookId, (num5 - 1).ToString(), UserAccount);
						QueryResult queryResult = bookManager.sqlCommandQuery(sqlCommand);
						float num6 = 0f;
						float num7 = 0f;
						if (queryResult.fetchRow())
						{
							num6 = queryResult.getFloat("canvasWidth");
							num7 = queryResult.getFloat("canvasHeight");
							if (num6 > 0f && num7 > 0f)
							{
								if (num6 > num7)
								{
									if (num3 <= 0f || num4 <= 0f)
									{
										num3 = width;
										num4 = height;
										num = num4;
										num2 = num3;
									}
								}
								else if (num <= 0f || num2 <= 0f)
								{
									num = width;
									num2 = height;
									num3 = num2;
									num4 = num;
								}
							}
						}
						iTextSharp.text.Image instance2 = iTextSharp.text.Image.GetInstance(text8);
						float num8 = 1f;
						float num9 = 1f;
						pageSize = ((instance2.Width > instance2.Height) ? ((!(num3 > 0f) || !(num4 > 0f)) ? new iTextSharp.text.Rectangle(num2, num) : new iTextSharp.text.Rectangle(num3, num4)) : ((!(num > 0f) || !(num2 > 0f)) ? new iTextSharp.text.Rectangle(num4, num4) : new iTextSharp.text.Rectangle(num, num2)));
						if (num6 > 0f && num7 > 0f)
						{
							num8 = pageSize.Width / num6;
							num9 = pageSize.Height / num7;
						}
						document.SetPageSize(pageSize);
						document.NewPage();
						instance2.ScaleToFit(pageSize.Width, pageSize.Height);
						instance2.SetAbsolutePosition(0f, 0f);
						document.Add(instance2);
						sqlCommand = string.Format("select notes from booknoteDetail as a inner join bookInfo as b on a.userbook_sno=b.sno   where bookid='{0}' and page='{1}' and account='{2}'", bookId, (num5 - 1).ToString(), UserAccount);
						queryResult = bookManager.sqlCommandQuery(sqlCommand);
						if (queryResult.fetchRow())
						{
							document.Add(new iTextSharp.text.Paragraph("\r\n"));
							document.Add(new Annotation("註解", queryResult.getString("notes")));
						}
						sqlCommand = string.Format("SELECT page,status,alpha,canvasHeight,canvasWidth,color,points,width\r\n                                                FROM bookStrokesDetail as a inner join bookinfo as b on a.userbook_sno=b.sno \r\n                                                where bookid='{0}' and page={1} and status='0' and account='{2}'", bookId, (num5 - 1).ToString(), UserAccount);
						try
						{
							queryResult = bookManager.sqlCommandQuery(sqlCommand);
						}
						catch (Exception ex2)
						{
							LogTool.Debug(ex2);
						}
						if (queryResult != null)
						{
							while (queryResult.fetchRow())
							{
								string @string = queryResult.getString("color");
								float @float = queryResult.getFloat("alpha");
								int red = Convert.ToInt32(@string.Substring(1, 2), 16);
								int green = Convert.ToInt32(@string.Substring(3, 2), 16);
								int blue = Convert.ToInt32(@string.Substring(5, 2), 16);
								float float2 = queryResult.getFloat("width");
								string string2 = queryResult.getString("points");
								string[] array2 = string2.Split(new char[1]
								{
									';'
								}, StringSplitOptions.RemoveEmptyEntries);
								int num10 = 0;
								float num11 = 0f;
								float num12 = 0f;
								List<float[]> list = new List<float[]>();
								List<float> list2 = new List<float>();
								string[] array3 = array2;
								foreach (string text9 in array3)
								{
									num10++;
									string s = text9.Split(new char[3]
									{
										'{',
										',',
										'}'
									}, StringSplitOptions.RemoveEmptyEntries)[0];
									string s2 = text9.Split(new char[3]
									{
										'{',
										',',
										'}'
									}, StringSplitOptions.RemoveEmptyEntries)[1];
									num11 = (float)int.Parse(s) * num8;
									num12 = (float)int.Parse(s2) * num9;
									list2.Add(num11);
									list2.Add(pageSize.Height - num12);
								}
								list.Add(list2.ToArray());
								PdfAnnotation pdfAnnotation = PdfAnnotation.CreateInk(instance, pageSize, "", list.ToArray());
								pdfAnnotation.Color = new BaseColor(red, green, blue, int.Parse(@float.ToString()));
								pdfAnnotation.BorderStyle = new PdfBorderDictionary(float2, 0);
								pdfAnnotation.Flags = 4;
								instance.AddAnnotation(pdfAnnotation);
							}
						}
					}
				}
				document.AddTitle("電子書");
				document.AddAuthor("Hyweb");
			}
			catch (Exception ex3)
			{
				LogTool.Debug(ex3);
			}
			finally
			{
				try
				{
					try
					{
						if (document.IsOpen())
						{
							document.Close();
						}
					}
					catch (Exception ex4)
					{
						LogTool.Debug(ex4);
					}
					try
					{
						if (fileStream != null)
						{
							fileStream.Dispose();
						}
					}
					catch (Exception ex5)
					{
						LogTool.Debug(ex5);
					}
					if (File.Exists(text))
					{
						File.Copy(text, destFileName, true);
					}
				}
				catch (Exception ex6)
				{
					LogTool.Debug(ex6);
				}
			}
		}

		public static void ConvertPDF2Image(string pdfInputPath, string imageOutputPath, int startPageNum, int endPageNum, ImageFormat imageFormat, Definition definition)
		{
			PDFFile pDFFile = PDFFile.Open(pdfInputPath);
			if (startPageNum <= 0)
			{
				startPageNum = 1;
			}
			if (endPageNum > pDFFile.PageCount)
			{
				endPageNum = pDFFile.PageCount;
			}
			if (startPageNum > endPageNum)
			{
				startPageNum = endPageNum;
				endPageNum = startPageNum;
			}
			for (int i = startPageNum; i <= endPageNum; i++)
			{
				Bitmap pageImage = pDFFile.GetPageImage(i - 1, 92.0);
				pageImage.Save(imageOutputPath, ImageFormat.Jpeg);
				pageImage.Dispose();
			}
			pDFFile.Dispose();
		}

		public void RecordPage()
		{
			try
			{
				try
				{
					sendBroadCast("{\"cmd\":\"R.CB\"}");
					Singleton_Socket.ReaderEvent = this;
					socket = Singleton_Socket.GetInstance(meetingId, account, userName, isSyncing);
					if (socket != null)
					{
						socket.RemoveEventManager(this);
					}
				}
				catch (Exception ex)
				{
					LogTool.Debug(ex);
				}
				Canvas canvas = FindVisualChildByName<Canvas>(FR, "zoomCanvas");
				canvas.Background = null;
				try
				{
					base.Closing -= new CancelEventHandler(ReadWindow_Closing);
					imageSourceRendered -= new EventHandler<imageSourceRenderedResultEventArgs>(ReadWindow_imageSourceRendered);
				}
				catch (Exception ex2)
				{
					LogTool.Debug(ex2);
				}
				if (checkImageStatusTimer != null)
				{
					checkImageStatusTimer.Tick -= new EventHandler(checkImageStatus);
					checkImageStatusTimer.Stop();
					checkImageStatusTimer.IsEnabled = false;
					checkImageStatusTimer = null;
				}
				if (trialPages == 0)
				{
					if (fsw != null)
					{
						fsw.EnableRaisingEvents = false;
						fsw.IncludeSubdirectories = false;
						fsw.Changed -= new FileSystemEventHandler(fsw_Changed);
						fsw = null;
					}
					saveLastReadingPage();
					deleteAllLocalPenmemoData();
				}
				clearReadPagePairData(singleReadPagePair);
				clearReadPagePairData(doubleReadPagePair);
				BindingOperations.ClearAllBindings(this);
				BindingOperations.ClearAllBindings(thumbNailListBox);
				List<ThumbnailImageAndPage> list = (List<ThumbnailImageAndPage>)thumbNailListBox.ItemsSource;
				for (int i = 0; i < list.Count; i++)
				{
					list[i].leftImagePath = "";
				}
				if (thumbNailListBox.SelectedIndex > 0)
				{
					list.RemoveAt(thumbNailListBox.SelectedIndex);
				}
				list.Clear();
				BindingOperations.ClearAllBindings(_FlowDocument);
				BindingOperations.ClearAllBindings(_FlowDocumentDouble);
				BindingOperations.ClearAllBindings(FR);
				_FlowDocument.Blocks.Clear();
				_FlowDocumentDouble.Blocks.Clear();
				thumbNailListBox.ItemsSource = null;
				FR.Document = null;
				_FlowDocument = null;
				_FlowDocumentDouble = null;
				singleThumbnailImageAndPageList.Clear();
				doubleThumbnailImageAndPageList.Clear();
				singleThumbnailImageAndPageList = null;
				doubleThumbnailImageAndPageList = null;
				tfgForImage = null;
				caTool = null;
				singleImgStatus = null;
				doubleImgStatus = null;
				selectedBook = null;
				bookPath = null;
				hejMetadata = null;
				bookMarkDictionary = null;
				tfgForHyperLink = null;
				pageInfoManager = null;
				pageInfo = null;
				RelativePanel = null;
				configMng = null;
			}
			catch (Exception ex3)
			{
				LogTool.Debug(ex3);
			}
		}

		private void initUserDataFromDB()
		{
			getBookRightsAsync(bookId);
			string machineName = Environment.MachineName;
			lastViewPage = bookManager.getLastViewPageObj(userBookSno);
			if (lastViewPage.ContainsKey(machineName))
			{
				if (lastViewPage[machineName].index > 0)
				{
					int num = lastPageMode;
					if (isSyncing && !CanSentLine)
					{
						if (isSyncOwner)
						{
							bringBlockIntoView(0);
							loadCurrentStrokes(0);
						}
					}
					else if (num.Equals(1))
					{
						bringBlockIntoView(lastViewPage[machineName].index);
					}
					else if (num.Equals(2))
					{
						int doubleCurPageIndex = getDoubleCurPageIndex(lastViewPage[machineName].index);
						bringBlockIntoView(doubleCurPageIndex);
					}
				}
				else
				{
					Canvas canvas = FindVisualChildByName<Canvas>(FR, "zoomCanvas");
					canvas.Background = null;
				}
			}
			else
			{
				Canvas canvas2 = FindVisualChildByName<Canvas>(FR, "zoomCanvas");
				canvas2.Background = null;
			}
			if (!isSyncing)
			{
				bookStrokesDictionary = bookManager.getStrokesDics(userBookSno);
				bookMarkDictionary = bookManager.getBookMarkDics(userBookSno);
				bookNoteDictionary = bookManager.getBookNoteDics(userBookSno);
			}
		}

		private void iniUpperButtons()
		{
			ComboBox comboBox = FindVisualChildByName<ComboBox>(FR, "cbBooks");
			if (cbBooksData != null)
			{
				comboBox.ItemsSource = cbBooksData;
				comboBox.DisplayMemberPath = "Key";
				comboBox.SelectedValuePath = "Value";
				comboBox.SelectedIndex = 0;
				int num = 0;
				foreach (KeyValuePair<string, BookVM> cbBooksDatum in cbBooksData)
				{
					if (cbBooksDatum.Value.FileID.Equals(bookId))
					{
						comboBox.SelectedIndex = num;
						break;
					}
					num++;
				}
				comboBox.SelectionChanged += new SelectionChangedEventHandler(cbBooks_SelectionChanged);
			}
			else
			{
				comboBox.Width = 0.0;
				comboBox.Visibility = Visibility.Collapsed;
			}
			if (File.Exists(bookPath + "\\HYWEB\\toc.ncx"))
			{
				string path = bookPath + "\\HYWEB\\toc.ncx";
				XmlDocNcx = new XmlDocument();
				using (MemoryStream memoryStream = new MemoryStream())
				{
					FileStream fileStream = new FileStream(path, FileMode.Open);
					fileStream.CopyTo(memoryStream);
					RadioButton radioButton = FindVisualChildByName<RadioButton>(FR, "TocButton");
					try
					{
						XmlDocNcx.Load(memoryStream);
						memoryStream.Close();
						radioButton.Visibility = Visibility.Visible;
					}
					catch
					{
						memoryStream.Close();
						radioButton.Visibility = Visibility.Collapsed;
					}
				}
			}
			if (hejMetadata.tocPageIndex.Equals(0))
			{
				RadioButton radioButton2 = FindVisualChildByName<RadioButton>(FR, "ContentButton");
				radioButton2.Visibility = Visibility.Collapsed;
			}
		}

		private void cbBooks_SelectionChanged(object sender, EventArgs e)
		{
			if (!isSyncing || isSyncOwner)
			{
				ComboBox comboBox = (ComboBox)sender;
				BookVM bookVM = (BookVM)comboBox.SelectedValue;
				if (bookVM != null && this.Home_OpenBookFromReader_Event != null)
				{
					this.Home_OpenBookFromReader_Event(meetingId, bookVM, cbBooksData, watermark);
				}
			}
		}

		public ReadWindow(Dictionary<string, BookVM> cbBooksData, Home_OpenBookFromReader_Function callback, string _bookPath, string _bookId, string _account, string _userName, string _email, string _meetingId, string _watermark, string _dbPath, bool _isSync, bool _isSyncOwner, string _webServiceURL, byte[] defaultKey, string _socketMessage = "", SocketClient _socket = null)
		{
			QueryResult queryResult;
			try
			{
				this.cbBooksData = cbBooksData;
				this.Home_OpenBookFromReader_Event = callback;
				this.defaultKey = defaultKey;
				socket = _socket;
				bookPath = _bookPath;
				bookId = _bookId;
				account = _account;
				userName = _userName;
				email = _email;
				meetingId = _meetingId;
				watermark = _watermark;
				dbPath = _dbPath;
				isSyncing = _isSync;
				webServiceURL = _webServiceURL;
				isSyncOwner = _isSyncOwner;
				socketMessage = _socketMessage;
				bookType = BookType.PHEJ;
				bookManager = new BookManager(dbPath);
				queryResult = null;
				string sqlCommand = "Select objectId from bookMarkDetail";
				queryResult = bookManager.sqlCommandQuery(sqlCommand);
				if (queryResult == null)
				{
					updateDataBase();
				}
			}
			catch (Exception)
			{
			}
			queryResult = null;
			langMng = new MultiLanquageManager("zh-TW");
			base.Initialized += new EventHandler(_InitializedEventHandler);
			lastTimeOfChangingPage = DateTime.Now;
			InitializeComponent();
			setWindowToFitScreen();
			ChangeFlatUI(Settings.Default.IsFlatUIReader);
			base.Loaded += new RoutedEventHandler(ReadWindow_Loaded);
			ClearSyncOwnerPenLine();
		}

		private void ClearSyncOwnerPenLine()
		{
			if (Settings.Default.IsClearSyncOwnerPenLine && isSyncOwner)
			{
				getBookPath();
				int num = userBookSno;
				Exec_Access_Sql(string.Format("DELETE FROM booklastPage WHERE userbook_sno = {0}", num));
				Exec_Access_Sql(string.Format("DELETE FROM bookmarkDetail WHERE userbook_sno = {0}", num));
				Exec_Access_Sql(string.Format("DELETE FROM booknoteDetail WHERE userbook_sno = {0}", num));
				Exec_Access_Sql(string.Format("DELETE FROM bookStrokesDetail WHERE userbook_sno = {0}", num));
			}
		}

		private void Exec_Access_Sql(string SQL)
		{
			try
			{
				bookManager.sqlCommandNonQuery(SQL);
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}

		private void InitSyncCenter()
		{
			Task.Factory.StartNew(new Action(_003CInitSyncCenter_003Eb__5a));
		}

		public ReadWindow(Dictionary<string, BookVM> cbBooksData, Home_OpenBookFromReader_Function callback, string _bookPath, string _bookId, string _account, string _userName, string _email, string _meetingId, string _watermark, string _dbPath, bool _isSync, bool _isSyncOwner, string _webServiceURL, string _socketMessage = "", SocketClient _socket = null)
		{
			QueryResult queryResult;
			try
			{
				this.cbBooksData = cbBooksData;
				this.Home_OpenBookFromReader_Event = callback;
				socket = _socket;
				bookPath = _bookPath;
				bookId = _bookId;
				account = _account;
				userName = _userName;
				email = _email;
				meetingId = _meetingId;
				watermark = _watermark;
				dbPath = _dbPath;
				isSyncing = _isSync;
				webServiceURL = _webServiceURL;
				isSyncOwner = _isSyncOwner;
				socketMessage = _socketMessage;
				bookType = BookType.PHEJ;
				bookManager = new BookManager(dbPath);
				queryResult = null;
				string sqlCommand = "Select objectId from bookMarkDetail";
				queryResult = bookManager.sqlCommandQuery(sqlCommand);
				if (queryResult == null)
				{
					updateDataBase();
				}
			}
			catch (Exception)
			{
			}
			queryResult = null;
			langMng = new MultiLanquageManager("zh-TW");
			base.Initialized += new EventHandler(_InitializedEventHandler);
			lastTimeOfChangingPage = DateTime.Now;
			InitializeComponent();
			setWindowToFitScreen();
			ChangeFlatUI(Settings.Default.IsFlatUIReader);
			base.Loaded += new RoutedEventHandler(ReadWindow_Loaded);
			ClearSyncOwnerPenLine();
		}

		private void Grid_MouseEnterTransparent(object sender, MouseEventArgs e)
		{
			btnThin.Background = System.Windows.Media.Brushes.Transparent;
			btnMedium.Background = System.Windows.Media.Brushes.Transparent;
			btnLarge.Background = System.Windows.Media.Brushes.Transparent;
			Grid grid = (Grid)sender;
			grid.Background = ColorTool.HexColorToBrush("#F66F00");
		}

		private void Grid_MouseLeaveTransparent(object sender, MouseEventArgs e)
		{
			Grid grid = (Grid)sender;
			grid.Background = System.Windows.Media.Brushes.Transparent;
		}

		private void Grid_MouseEnter(object sender, MouseEventArgs e)
		{
			List<DependencyObject> DPs = new List<DependencyObject>();
			FindVisualChildTool.ByType<Grid>(PenColorSP, ref DPs);
			int num = 0;
			foreach (Grid item in DPs)
			{
				num++;
				if (!object.Equals(item.Background, System.Windows.Media.Brushes.Black))
				{
					item.Background = ColorTool.HexColorToBrush("#000000");
				}
			}
			Grid grid2 = (Grid)sender;
			grid2.Background = ColorTool.HexColorToBrush("#F66F00");
		}

		private void Grid_MouseLeave(object sender, MouseEventArgs e)
		{
			Grid grid = (Grid)sender;
			grid.Background = System.Windows.Media.Brushes.Black;
		}

		private void btnBoldSP_MouseLeave(object sender, MouseEventArgs e)
		{
			ShowNowPenBold();
		}

		private void btnPenFuncSP_MouseLeave(object sender, MouseEventArgs e)
		{
			ShowNowPenColor();
		}

		private void txtKeyword_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			Task.Factory.StartNew(new Action(_003CtxtKeyword_PreviewKeyDown_003Eb__5b));
		}

		private void txtKeyword_KeyUp(object sender, KeyEventArgs e)
		{
			string text = txtKeyword.Text.ToLower().Trim();
			int num = 0;
			if (!text.Equals(""))
			{
				ListBox listBox = hyftdSearch(text);
				List<SearchRecord> source = (List<SearchRecord>)listBox.ItemsSource;
				List<ThumbnailImageAndPage> item4 = (List<ThumbnailImageAndPage>)thumbNailListBox.ItemsSource;
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate5e == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate5e = new Func<SearchRecord, int>(_003CtxtKeyword_KeyUp_003Eb__5d);
				}
				List<int> list = Enumerable.ToList(Enumerable.Select(source, CS_0024_003C_003E9__CachedAnonymousMethodDelegate5e));
				int num2 = 0;
				foreach (ThumbnailImageAndPage item2 in (IEnumerable)thumbNailListBox.Items)
				{
					int item = int.Parse(item2.pageIndex);
					ListBoxItem listBoxItem = (ListBoxItem)thumbNailListBox.ItemContainerGenerator.ContainerFromIndex(num2);
					if (!list.Contains(item))
					{
						listBoxItem.Visibility = Visibility.Collapsed;
					}
					else if (listBoxItem.Visibility == Visibility.Visible)
					{
						listBoxItem.Visibility = Visibility.Visible;
						num++;
					}
					num2++;
				}
			}
			else
			{
				int num3 = 0;
				foreach (ThumbnailImageAndPage item3 in (IEnumerable)thumbNailListBox.Items)
				{
					ThumbnailImageAndPage thumbnailImageAndPage = item3;
					ListBoxItem listBoxItem2 = (ListBoxItem)thumbNailListBox.ItemContainerGenerator.ContainerFromIndex(num3);
					if (listBoxItem2 != null)
					{
						listBoxItem2.Visibility = Visibility.Visible;
					}
					num++;
					num3++;
				}
			}
			txtFilterCount.Text = string.Format("有 {0} 筆相關資料", num.ToString());
		}

		private void AttachKey()
		{
			base.PreviewKeyDown += new KeyEventHandler(_003CAttachKey_003Eb__5f);
		}

		private void OpenClosePaint()
		{
			openedby = MediaCanvasOpenedBy.PenMemo;
			Grid grid = FindVisualChildByName<Grid>(FR, "ToolBarInReader");
			Grid grid2 = FindVisualChildByName<Grid>(FR, "PenMemoToolBar");
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
			StrokeToolPanelHorizontal_Reader strokeToolPanelHorizontal_Reader = new StrokeToolPanelHorizontal_Reader();
			strokeToolPanelHorizontal_Reader.langMng = langMng;
			Canvas element = FindVisualChildByName<Canvas>(FR, "zoomCanvas");
			Canvas stageCanvasInReader = GetStageCanvasInReader();
			if (Panel.GetZIndex(inkCanvas) < 900)
			{
				MouseTool.ShowPen();
				Panel.SetZIndex(inkCanvas, 900);
				Panel.SetZIndex(stageCanvasInReader, 2);
				Panel.SetZIndex(element, 850);
				NewUITop.Visibility = Visibility.Collapsed;
				NewUI.Visibility = Visibility.Collapsed;
				NewUI.Visibility = Visibility.Collapsed;
				thumnailCanvas.Visibility = Visibility.Collapsed;
				ShowListBoxButton.Visibility = Visibility.Collapsed;
				inkCanvas.Background = System.Windows.Media.Brushes.Transparent;
				inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
				inkCanvas.Visibility = Visibility.Visible;
				strokeToolPanelHorizontal_Reader.HorizontalAlignment = HorizontalAlignment.Right;
				grid2.Children.Add(strokeToolPanelHorizontal_Reader);
				alterPenmemoAnimation(strokeToolPanelHorizontal_Reader, 0.0, strokeToolPanelHorizontal_Reader.Width);
				strokeToolPanelHorizontal_Reader.strokeChange += new StrokeChangeEvent(strokeChaneEventHandler);
				strokeToolPanelHorizontal_Reader.strokeUndo += new StrokeUndoEvent(strokeUndoEventHandler);
				strokeToolPanelHorizontal_Reader.strokeDelAll += new StrokeDeleteAllEvent(strokeDelAllEventHandler);
				strokeToolPanelHorizontal_Reader.strokeRedo += new StrokeRedoEvent(strokeRedoEventHandler);
				strokeToolPanelHorizontal_Reader.strokeDel += new StrokeDeleteEvent(strokDelEventHandler);
				strokeToolPanelHorizontal_Reader.showPenToolPanel += new showPenToolPanelEvent(showPenToolPanelEventHandler);
				strokeToolPanelHorizontal_Reader.strokeErase += new StrokeEraseEvent(strokeEraseEventHandler);
				strokeToolPanelHorizontal_Reader.strokeCurve += new StrokeCurveEvent(strokeCurveEventHandler);
				strokeToolPanelHorizontal_Reader.strokeLine += new StrokeLineEvent(strokeLineEventHandler);
				inkCanvas.Focus();
				Canvas canvas = FindVisualChildByName<Canvas>(FR, "HiddenControlCanvas");
				if (canvas.Visibility.Equals(Visibility.Collapsed))
				{
					canvas.Visibility = Visibility.Visible;
				}
				Keyboard.ClearFocus();
				ButtonsStatusWhenOpenPenMemo(0.5, false);
				if (isStrokeLine)
				{
					strokeLineEventHandler();
				}
				else
				{
					strokeCurveEventHandler();
				}
				ChangeMainPenColor();
				System.Windows.Media.Brush background = btnEraserGD.Background;
				if (background is SolidColorBrush)
				{
					string text = ((SolidColorBrush)background).Color.ToString();
					if (text.Equals("#FFF66F00"))
					{
						Mouse.OverrideCursor = CursorHelper.CreateCursor(new MyCursor());
						inkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
					}
				}
			}
			else
			{
				MouseTool.ShowArrow();
				Panel.SetZIndex(element, 1);
				Panel.SetZIndex(inkCanvas, 2);
				Panel.SetZIndex(stageCanvasInReader, 3);
				NewUITop.Visibility = Visibility.Visible;
				NewUI.Visibility = Visibility.Visible;
				inkCanvas.EditingMode = InkCanvasEditingMode.None;
				alterPenmemoAnimation(strokeToolPanelHorizontal_Reader, strokeToolPanelHorizontal_Reader.Width, 0.0);
				convertCurrentStrokesToDB(hejMetadata.LImgList[curPageIndex].pageId);
				grid2.Children.Remove(grid2.Children[grid2.Children.Count - 1]);
				Canvas canvas2 = FindVisualChildByName<Canvas>(FR, "PopupControlCanvas");
				if (canvas2.Visibility.Equals(Visibility.Visible))
				{
					canvas2.Visibility = Visibility.Collapsed;
				}
				Canvas canvas3 = FindVisualChildByName<Canvas>(FR, "HiddenControlCanvas");
				if (canvas3.Visibility.Equals(Visibility.Visible))
				{
					canvas3.Visibility = Visibility.Collapsed;
				}
				grid2.Visibility = Visibility.Collapsed;
				grid.Visibility = Visibility.Visible;
				ButtonsStatusWhenOpenPenMemo(1.0, true);
				resetFocusBackToReader();
			}
			inkCanvas.Focus();
		}

		private Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
		{
			try
			{
				using (MemoryStream stream = new MemoryStream())
				{
					BitmapEncoder bitmapEncoder = new BmpBitmapEncoder();
					bitmapEncoder.Frames.Add(BitmapFrame.Create(bitmapImage));
					bitmapEncoder.Save(stream);
					Bitmap original = new Bitmap(stream);
					return new Bitmap(original);
				}
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
			return null;
		}

		private void ChangeFlatUI(bool IsFlatUI)
		{
			MouseButtonEventHandler mouseButtonEventHandler = null;
			MouseEventHandler mouseEventHandler = null;
			RoutedEventHandler routedEventHandler = null;
			RoutedEventHandler routedEventHandler2 = null;
			RoutedEventHandler routedEventHandler3 = null;
			if (IsFlatUI)
			{
				if (mouseButtonEventHandler == null)
				{
					mouseButtonEventHandler = new MouseButtonEventHandler(_003CChangeFlatUI_003Eb__60);
				}
				base.MouseRightButtonDown += mouseButtonEventHandler;
				Canvas mediaTableCanvasInReader = GetMediaTableCanvasInReader();
				if (mediaTableCanvasInReader != null)
				{
					Panel.SetZIndex(mediaTableCanvasInReader, 201);
				}
				NewUITop.Visibility = Visibility.Visible;
				SearchSP.Visibility = Visibility.Visible;
				thumnailCanvas.Margin = new Thickness(0.0, 0.0, 0.0, 100.0);
				AllImageButtonInListBox.Height = 0.0;
				AllImageButtonInListBox.Width = 0.0;
				AllImageButtonInListBox.Margin = new Thickness(0.0);
				AllImageButtonInListBox.Visibility = Visibility.Collapsed;
				BookMarkButtonInListBox.Height = 0.0;
				BookMarkButtonInListBox.Width = 0.0;
				BookMarkButtonInListBox.Margin = new Thickness(0.0);
				BookMarkButtonInListBox.Visibility = Visibility.Collapsed;
				NoteButtonInListBox.Height = 0.0;
				NoteButtonInListBox.Width = 0.0;
				NoteButtonInListBox.Margin = new Thickness(0.0);
				NoteButtonInListBox.Visibility = Visibility.Collapsed;
				HideListBoxButton.Visibility = Visibility.Collapsed;
				NewUI.Visibility = Visibility.Visible;
				AllImageButtonInListBoxSP.Visibility = Visibility.Visible;
				BookMarkButtonInListBoxSP.Visibility = Visibility.Visible;
				NoteButtonInListBoxSP.Visibility = Visibility.Visible;
				Rect1.Visibility = Visibility.Visible;
				Rect2.Visibility = Visibility.Visible;
				thumbNailListBoxGD.Background = ColorTool.HexColorToBrush("#272727");
				thumbNailListBoxGD.VerticalAlignment = VerticalAlignment.Center;
				thumbNailCanvasGrid.Background = ColorTool.HexColorToBrush("#000000");
				TextBox textBox = txtKeyword;
				if (mouseEventHandler == null)
				{
					mouseEventHandler = new MouseEventHandler(_003CChangeFlatUI_003Eb__61);
				}
				textBox.MouseEnter += mouseEventHandler;
				TextBox textBox2 = txtKeyword;
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate68 == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate68 = new MouseEventHandler(_003CChangeFlatUI_003Eb__62);
				}
				textBox2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate68;
				txtKeyword.KeyUp += new KeyEventHandler(txtKeyword_KeyUp);
				txtKeyword.Focus();
				txtKeyword.PreviewKeyDown += new KeyEventHandler(txtKeyword_PreviewKeyDown);
				RadioButton radioButton = btnTxtKeywordClear;
				if (routedEventHandler == null)
				{
					routedEventHandler = new RoutedEventHandler(_003CChangeFlatUI_003Eb__63);
				}
				radioButton.Click += routedEventHandler;
				RadioButton radioButton2 = btnBold;
				if (routedEventHandler2 == null)
				{
					routedEventHandler2 = new RoutedEventHandler(_003CChangeFlatUI_003Eb__64);
				}
				radioButton2.Click += routedEventHandler2;
				RadioButton radioButton3 = btnPenColor;
				if (routedEventHandler3 == null)
				{
					routedEventHandler3 = new RoutedEventHandler(_003CChangeFlatUI_003Eb__65);
				}
				radioButton3.Click += routedEventHandler3;
			}
			else
			{
				thumnailCanvas.Background = ColorTool.HexColorToBrush("#212020");
			}
		}

		private void ShowNowPenColor()
		{
			int result = 1;
			int.TryParse(btnPenColor.Tag.ToString(), out result);
			List<DependencyObject> DPs = new List<DependencyObject>();
			FindVisualChildTool.ByType<Grid>(PenColorSP, ref DPs);
			int num = 0;
			foreach (Grid item in DPs)
			{
				num++;
				if (num == result)
				{
					item.Background = ColorTool.HexColorToBrush("#F66F00");
				}
			}
		}

		private void MyAnimation(DependencyObject sp, double ms, string property, double from, double to, Action act = null)
		{
			_003C_003Ec__DisplayClass6d _003C_003Ec__DisplayClass6d = new _003C_003Ec__DisplayClass6d();
			_003C_003Ec__DisplayClass6d.act = act;
			Storyboard storyboard = new Storyboard();
			DoubleAnimation doubleAnimation = new DoubleAnimation();
			Duration duration2 = doubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(ms));
			storyboard.Children.Add(doubleAnimation);
			Storyboard.SetTarget(doubleAnimation, sp);
			Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(property));
			doubleAnimation.AccelerationRatio = 0.2;
			doubleAnimation.DecelerationRatio = 0.7;
			doubleAnimation.From = from;
			doubleAnimation.To = to;
			storyboard.Completed += new EventHandler(_003C_003Ec__DisplayClass6d._003CMyAnimation_003Eb__6c);
			storyboard.Begin();
		}

		private void ShowNowPenBold()
		{
			int result = 1;
			int.TryParse(btnBold.Tag.ToString(), out result);
			IEnumerable<Grid> enumerable = Enumerable.OfType<Grid>(btnBoldSP.Children);
			int num = 0;
			foreach (Grid item in enumerable)
			{
				num++;
				if (num * 100 == result)
				{
					item.Background = ColorTool.HexColorToBrush("#F66F00");
				}
			}
		}

		public ReadWindow(string _bookPath, string _bookId, string _account, string _userName, string _email, string _meetingId, string _watermark, string _dbPath, bool _isSync, bool _isSyncOwner, string _webServiceURL, string _socketMessage = "", SocketClient _socket = null)
		{
			QueryResult queryResult;
			try
			{
				socket = _socket;
				bookPath = _bookPath;
				bookId = _bookId;
				account = _account;
				userName = _userName;
				email = _email;
				meetingId = _meetingId;
				watermark = _watermark;
				dbPath = _dbPath;
				isSyncing = _isSync;
				webServiceURL = _webServiceURL;
				isSyncOwner = _isSyncOwner;
				socketMessage = _socketMessage;
				bookType = BookType.PHEJ;
				bookManager = new BookManager(dbPath);
				queryResult = null;
				string sqlCommand = "Select objectId from bookMarkDetail";
				queryResult = bookManager.sqlCommandQuery(sqlCommand);
				if (queryResult == null)
				{
					updateDataBase();
				}
			}
			catch (Exception)
			{
			}
			queryResult = null;
			langMng = new MultiLanquageManager("zh-TW");
			base.Initialized += new EventHandler(_InitializedEventHandler);
			lastTimeOfChangingPage = DateTime.Now;
			InitializeComponent();
			setWindowToFitScreen();
			ChangeFlatUI(Settings.Default.IsFlatUIReader);
			base.Loaded += new RoutedEventHandler(ReadWindow_Loaded);
			ClearSyncOwnerPenLine();
		}

		private void updateDataBase()
		{
			List<string> list = new List<string>();
			DateTime value = new DateTime(1970, 1, 1);
			long num = DateTime.Now.ToUniversalTime().Subtract(value).Ticks / 10000000;
			try
			{
				list.Add("CREATE TABLE `userbook_metadata` (\r\n\t                                        `sno` Long NOT NULL IDENTITY, \r\n\t                                        `bookId` VarChar(255) WITH COMP NOT NULL, \r\n\t                                        `account` VarChar(255) WITH COMP NOT NULL, \r\n\t                                        `vendorId` VarChar(255) WITH COMP NOT NULL, \r\n\t                                        `colibId` VarChar(255) WITH COMP NOT NULL, \r\n\t                                        `book_type` VarChar(255) WITH COMP, \r\n\t                                        `globalNo` VarChar(255) WITH COMP, \r\n\t                                        `book_language` VarChar(30) WITH COMP, \r\n\t                                        `orientation` VarChar(20) WITH COMP, \r\n\t                                        `text_direction` VarChar(20) WITH COMP, \r\n\t                                        `owner` VarChar(255) WITH COMP NOT NULL, \r\n\t                                        `hyread_type` Byte, \r\n\t                                        `total_pages` Short, \r\n\t                                        `volume` VarChar(255) WITH COMP, \r\n\t                                        `cover` VarChar(128) WITH COMP, \r\n\t                                        `coverMD5` VarChar(255) WITH COMP, \r\n\t                                        `filesize` Long, \r\n\t                                        `epub_filesize` Long, \r\n\t                                        `hej_filesize` Long, \r\n\t                                        `phej_filesize` Long, \r\n\t                                        `page_direction` VarChar(255) WITH COMP, \r\n\t                                        `lastview_page` Long DEFAULT 0, \r\n\t                                        `canPrint` Bit NOT NULL DEFAULT 0, \r\n\t                                        `canMark` Bit NOT NULL DEFAULT 0, \r\n\t                                        `postTimes` Long DEFAULT 0, \r\n\t                                        `expireDate` VarChar(255), \r\n\t                                        `readTimes` Long DEFAULT 0, \r\n\t                                        `epubLastNode` Long DEFAULT 0, \r\n\t                                        `epubLastPageRate` Single DEFAULT 0, \r\n\t                                        `updateTime` DateTime, \r\n\t                                        `rightsXML` LongText WITH COMP,\r\n\t                                        CONSTRAINT `PrimaryKey` PRIMARY KEY (`sno`, `bookId`, `account`, `vendorId`, `colibId`, `owner`)\r\n                                        )\r\n                                        GO\r\n                                        CREATE INDEX `bookId`\r\n\t                                        ON `userbook_metadata` (\r\n\t                                        `bookId`\r\n                                        )\r\n                                        GO\r\n                                        CREATE INDEX `colibId`\r\n\t                                        ON `userbook_metadata` (\r\n\t                                        `colibId`\r\n                                        )\r\n                                        GO\r\n                                        CREATE INDEX `vendorId`\r\n\t                                        ON `userbook_metadata` (\r\n\t                                        `vendorId`\r\n                                        )\r\n                    GO");
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
			list.Add("CREATE TABLE booklastPage ( [userbook_sno] INTEGER, [page] INTEGER, [objectId] TEXT(50), [createTime] INTEGER, [updateTime] INTEGER, [syncTime] INTEGER, [status] TEXT(50), [device] TEXT(50) )");
			list.Add("ALTER TABLE bookmarkDetail DROP CONSTRAINT PrimaryKey");
			list.Add("ALTER TABLE bookmarkDetail Add COLUMN userbook_sno INTEGER");
			list.Add("UPDATE bookmarkDetail SET sno = userbook_sno");
			list.Add("ALTER TABLE bookmarkDetail Drop COLUMN sno");
			list.Add("ALTER TABLE bookmarkDetail ADD CONSTRAINT PrimaryKey PRIMARY KEY (userbook_sno,page)");
			list.Add("ALTER TABLE bookmarkDetail ADD COLUMN [objectId] TEXT(50), [createTime] INTEGER, [updateTime] INTEGER, [syncTime] INTEGER, [status] TEXT(50) ");
			list.Add("update bookmarkDetail set objectId='', updateTime=" + num + ", createTime=" + num + ", syncTime=0, status='0' Where TRUE");
			list.Add("ALTER TABLE booknoteDetail DROP CONSTRAINT PrimaryKey");
			list.Add("ALTER TABLE booknoteDetail Add COLUMN userbook_sno INTEGER");
			list.Add("UPDATE booknoteDetail SET sno = userbook_sno");
			list.Add("ALTER TABLE booknoteDetail Drop COLUMN sno");
			list.Add("ALTER TABLE booknoteDetail ADD CONSTRAINT PrimaryKey PRIMARY KEY (userbook_sno,page)");
			list.Add("ALTER TABLE booknoteDetail ADD COLUMN [objectId] TEXT(50), [createTime] INTEGER, [updateTime] INTEGER, [syncTime] INTEGER, [status] TEXT(50)");
			list.Add("update booknoteDetail set objectId='', updateTime=" + num + ", createTime=" + num + ", syncTime=0, status='0' Where TRUE");
			list.Add("CREATE TABLE [bookStrokesDetail] ( [userbook_sno] INTEGER, [page] INTEGER, [objectId] TEXT(50), [createTime] INTEGER, [updateTime] INTEGER, [syncTime] INTEGER, [status] TEXT(50), [alpha] FLOAT, [canvasHeight] FLOAT, [canvasWidth] FLOAT, [color] TEXT(50), [points] MEMO, [width] FLOAT )");
			list.Add("CREATE TABLE [cloudSyncTime](   [classKey] TEXT(100),  [lastSyncTime] INTEGER)");
			bookManager.sqlCommandNonQuery(list);
		}

		private Bitmap renPdfToBitmap(string pageFile, byte[] key, int pg, int dpi, float scal, int decodedPageIndex, Border border, bool isSinglePage)
		{
			System.Drawing.Color white = System.Drawing.Color.White;
			Bitmap aBitmap = null;
			if (decodedPDFPages[decodedPageIndex] == null)
			{
				try
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						FileStream fileStream = new FileStream(pageFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
						fileStream.CopyTo(memoryStream);
						decodedPDFPages[decodedPageIndex] = memoryStream.ToArray();
					}
				}
				catch (Exception ex)
				{
					LogTool.Debug(ex);
					return aBitmap;
				}
			}
			else
			{
				try
				{
					using (MemoryStream memoryStream2 = new MemoryStream())
					{
						FileStream fileStream2 = new FileStream(pageFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
						fileStream2.CopyTo(memoryStream2);
						decodedPDFPages[decodedPageIndex] = memoryStream2.ToArray();
					}
				}
				catch (Exception ex2)
				{
					LogTool.Debug(ex2);
					return aBitmap;
				}
			}
			try
			{
				PDFDoc pDFDoc = new PDFDoc();
				pDFDoc.Init("PVD20-M4IRG-QYZK9-MNJ2U-DFTK1-MAJ4L", "PDFX3$Henry$300604_Allnuts#");
				try
				{
					pDFDoc.OpenFromMemory(decodedPDFPages[decodedPageIndex], (uint)decodedPDFPages[decodedPageIndex].Length, 0);
				}
				catch (Exception ex3)
				{
					LogTool.Debug(ex3);
				}
				PXCV_Lib36.PXV_CommonRenderParameters aCommonRenderParams = prepareCommonRenderParameter(pDFDoc, dpi, pg, scal, 0, 0, border, isSinglePage);
				pDFDoc.DrawPageToDIBSection(IntPtr.Zero, pg, white, aCommonRenderParams, out aBitmap);
				pDFDoc.ReleasePageCachedData(pg, 1);
				pDFDoc.Delete();
				return aBitmap;
			}
			catch (Exception ex4)
			{
				LogTool.Debug(ex4);
				return aBitmap;
			}
		}

		private void imageZoom(double imageScale, double scaleMaxOrMin, bool Maximum, bool isSlide)
		{
			StackPanel stackPanel = (StackPanel)GetImageInReader();
			TranslateTransform translateTransform = (TranslateTransform)tfgForImage.Children[1];
			ScaleTransform scaleTransform = (ScaleTransform)tfgForImage.Children[0];
			double scaleX = scaleTransform.ScaleX;
			double scaleY = scaleTransform.ScaleY;
			scaleTransform.ScaleX = imageScale;
			scaleTransform.ScaleY = imageScale;
			if (Maximum)
			{
				scaleTransform.ScaleX = Math.Min(scaleTransform.ScaleX, scaleMaxOrMin);
				scaleTransform.ScaleY = Math.Min(scaleTransform.ScaleY, scaleMaxOrMin);
			}
			else
			{
				scaleTransform.ScaleX = Math.Max(scaleTransform.ScaleX, scaleMaxOrMin);
				scaleTransform.ScaleY = Math.Max(scaleTransform.ScaleY, scaleMaxOrMin);
			}
			double num = base.RestoreBounds.Height / base.ActualWidth;
			double num2 = stackPanel.ActualHeight / stackPanel.ActualWidth;
			translateTransform.X -= translateTransform.X * (scaleX - scaleTransform.ScaleX);
			translateTransform.Y -= translateTransform.Y * (scaleY - scaleTransform.ScaleY);
			translateTransform.X = Math.Min(translateTransform.X, 0.0);
			translateTransform.X = Math.Max(translateTransform.X, 0.0);
			translateTransform.Y = Math.Min(translateTransform.Y, 0.0);
			translateTransform.Y = Math.Max(translateTransform.Y, 0.0);
			if (num < num2)
			{
				ratio = stackPanel.ActualHeight / base.ActualHeight;
			}
			else
			{
				ratio = stackPanel.ActualWidth / base.RestoreBounds.Width;
			}
			if (!isSlide)
			{
				Slider slider = FindVisualChildByName<Slider>(FR, "SliderInReader");
				slider.ValueChanged -= new RoutedPropertyChangedEventHandler<double>(SliderInReader_ValueChanged);
				slider.Value = imageScale;
				slider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(SliderInReader_ValueChanged);
			}
			isSameScale = false;
		}

		private void bringBlockIntoView(int pageIndex)
		{
			if (!IsFirstCapture)
			{
				try
				{
					TextBlock textBlock = FindVisualChildByName<TextBlock>(FR, "CurPageInReader");
					TakeAPicture(int.Parse(textBlock.Text));
				}
				catch (Exception ex)
				{
					LogTool.Debug(ex);
				}
			}
			if (trialPages != 0 && pageIndex > trialPages - 1)
			{
				return;
			}
			Block block = FR.Document.Blocks.FirstBlock;
			if (!pageIndex.Equals(0))
			{
				for (int i = 0; i < pageIndex; i++)
				{
					try
					{
						block = block.NextBlock;
					}
					catch (Exception)
					{
					}
				}
			}
			if (block != null)
			{
				block.BringIntoView();
			}
			if (isReadWindowLoaded)
			{
				sendBroadCast("{\"pageIndex\":" + pageIndex + ",\"cmd\":\"R.TP\"}");
			}
		}

		private void BookMarkButton_Checked(object sender, RoutedEventArgs e)
		{
			RadioButton radioButton = FindVisualChildByName<RadioButton>(FR, "BookMarkButton");
			if (viewStatusIndex.Equals(PageMode.SinglePage))
			{
				bool flag = false;
				int num = 0;
				if (bookMarkDictionary.ContainsKey(curPageIndex) && bookMarkDictionary[curPageIndex].status == "0")
				{
					flag = true;
					num = 1;
				}
				setBookMark(curPageIndex, !flag);
				radioButton.IsChecked = !flag;
				TriggerBookMark_NoteButtonOrElse(radioButton);
				sendBroadCast("{\"bookmark\":" + num + ",\"pageIndex\":" + curPageIndex.ToString() + ",\"cmd\":\"R.SB\"}");
			}
			else if (viewStatusIndex.Equals(PageMode.DoublePage))
			{
				ReadPagePair readPagePair = doubleReadPagePair[curPageIndex];
				if (readPagePair.rightPageIndex == -1 || readPagePair.leftPageIndex == -1)
				{
					bool flag2 = false;
					int num2 = Math.Max(readPagePair.rightPageIndex, readPagePair.leftPageIndex);
					if (bookMarkDictionary.ContainsKey(num2) && bookMarkDictionary[num2].status == "0")
					{
						flag2 = true;
					}
					setBookMark(num2, !flag2);
					radioButton.IsChecked = !flag2;
					TriggerBookMark_NoteButtonOrElse(radioButton);
				}
				else
				{
					bool flag3 = false;
					bool flag4 = false;
					if (bookMarkDictionary.ContainsKey(readPagePair.leftPageIndex) && bookMarkDictionary[readPagePair.leftPageIndex].status == "0")
					{
						flag3 = true;
					}
					if (bookMarkDictionary.ContainsKey(readPagePair.rightPageIndex) && bookMarkDictionary[readPagePair.rightPageIndex].status == "0")
					{
						flag4 = true;
					}
					if (flag3 || flag4)
					{
						if (flag3)
						{
							setBookMark(readPagePair.leftPageIndex, false);
						}
						if (flag4)
						{
							setBookMark(readPagePair.rightPageIndex, false);
						}
						radioButton.IsChecked = false;
						TriggerBookMark_NoteButtonOrElse(radioButton);
					}
					else
					{
						setBookMark(readPagePair.rightPageIndex, true);
						radioButton.IsChecked = true;
						TriggerBookMark_NoteButtonOrElse(radioButton);
					}
				}
			}
			if (CheckIsNowClick(BookMarkButtonInListBoxSP))
			{
				ShowBookMark();
				ShowBookMark();
			}
		}

		private void doUpperRadioButtonClicked(MediaCanvasOpenedBy whichButton, object sender)
		{
			Canvas mediaTableCanvasInReader = GetMediaTableCanvasInReader();
			StackPanel mediaListPanelInReader = GetMediaListPanelInReader();
			if (openedby.Equals(whichButton))
			{
				if (mediaTableCanvasInReader.Visibility.Equals(Visibility.Visible))
				{
					if (!whichButton.Equals(MediaCanvasOpenedBy.NoteButton))
					{
						((RadioButton)sender).IsChecked = false;
					}
					else if (whichButton.Equals(MediaCanvasOpenedBy.NoteButton))
					{
						TextBox textBox = FindVisualChildByName<TextBox>(FR, "notePanel");
						if (textBox != null)
						{
							int targetPageIndex = curPageIndex;
							setNotesInMem(textBox.Text, targetPageIndex);
							RadioButton radioButton = FindVisualChildByName<RadioButton>(FR, "NoteButton");
							if (textBox.Text.Equals(""))
							{
								radioButton.IsChecked = false;
								TriggerBookMark_NoteButtonOrElse(radioButton);
							}
							else
							{
								radioButton.IsChecked = true;
								TriggerBookMark_NoteButtonOrElse(radioButton);
							}
						}
					}
					mediaTableCanvasInReader.Visibility = Visibility.Collapsed;
					sendBroadCast("{\"cmd\":\"R.DPA\"}");
				}
				else
				{
					sendBroadCast("{\"cmd\":\"R.AA\"}");
					mediaTableCanvasInReader.Visibility = Visibility.Visible;
					if (whichButton.Equals(MediaCanvasOpenedBy.NoteButton))
					{
						TextBox textBox2 = FindVisualChildByName<TextBox>(FR, "notePanel");
						if (textBox2 != null)
						{
							int num = curPageIndex;
							setNotesInMem(textBox2.Text, num);
							textBox2.Text = (bookNoteDictionary.ContainsKey(num) ? bookNoteDictionary[num].text : "");
							RadioButton radioButton2 = FindVisualChildByName<RadioButton>(FR, "NoteButton");
							if (textBox2.Text.Equals(""))
							{
								radioButton2.IsChecked = false;
								TriggerBookMark_NoteButtonOrElse(radioButton2);
							}
							else
							{
								radioButton2.IsChecked = true;
								TriggerBookMark_NoteButtonOrElse(radioButton2);
							}
						}
					}
				}
				if (openedby == MediaCanvasOpenedBy.NoteButton)
				{
					TextBox textBox3 = FindVisualChildByName<TextBox>(mediaListPanelInReader, "notePanel");
					textBox3.Select(textBox3.Text.Length, 0);
					textBox3.Focus();
				}
				else
				{
					resetFocusBackToReader();
				}
				return;
			}
			string text = "";
			switch (openedby)
			{
			case MediaCanvasOpenedBy.SearchButton:
				text = "SearchButton";
				break;
			case MediaCanvasOpenedBy.MediaButton:
				text = "MediaListButton";
				break;
			case MediaCanvasOpenedBy.CategoryButton:
				text = "TocButton";
				break;
			case MediaCanvasOpenedBy.NoteButton:
				text = "NoteButton";
				break;
			case MediaCanvasOpenedBy.ShareButton:
				text = "ShareButton";
				break;
			case MediaCanvasOpenedBy.SettingButton:
				text = "SettingsButton";
				break;
			}
			if (!text.Equals("") && !text.Equals("NoteButton"))
			{
				RadioButton radioButton3 = FindVisualChildByName<RadioButton>(FR, text);
				radioButton3.IsChecked = false;
			}
			clickedPage = curPageIndex;
			mediaListPanelInReader.Children.Clear();
			if (RelativePanel.ContainsKey(whichButton) && !whichButton.Equals(MediaCanvasOpenedBy.NoteButton))
			{
				mediaListPanelInReader.Children.Add(RelativePanel[whichButton]);
			}
			else
			{
				StackPanel value = new StackPanel();
				double width = mediaListPanelInReader.Width;
				switch (whichButton)
				{
				case MediaCanvasOpenedBy.SearchButton:
					value = getSearchPanelSet(width, "");
					break;
				case MediaCanvasOpenedBy.MediaButton:
					value = getMediaListFromXML();
					break;
				case MediaCanvasOpenedBy.CategoryButton:
					value = getTocNcx();
					break;
				case MediaCanvasOpenedBy.NoteButton:
					value = getNotesAndMakeNote();
					break;
				case MediaCanvasOpenedBy.ShareButton:
					value = toShareBook();
					break;
				case MediaCanvasOpenedBy.SettingButton:
					value = openSettings();
					break;
				}
				if (RelativePanel.ContainsKey(whichButton))
				{
					RelativePanel[whichButton] = value;
				}
				else
				{
					RelativePanel.Add(whichButton, value);
				}
				mediaListPanelInReader.Children.Clear();
				mediaListPanelInReader.Children.Add(RelativePanel[whichButton]);
			}
			mediaTableCanvasInReader.Visibility = Visibility.Visible;
			openedby = whichButton;
			resetFocusBackToReader();
		}

		private void noteTB_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				TextBox textBox = (TextBox)sender;
				textBox.Text += "\r\n";
				int num = curPageIndex;
				sendBroadCast("{\"annotation\":\"" + textBox.Text + "\",\"pageIndex\":" + num + ",\"cmd\":\"R.SA\"}");
			}
		}

		private void noteButton_Click(object sender, RoutedEventArgs e)
		{
			noteButton_Click();
		}

		private void TriggerBookMark_NoteButtonOrElse(RadioButton rb)
		{
			System.Windows.Controls.Image image = FindVisualChildTool.ByName<System.Windows.Controls.Image>(FR, "statusBMK");
			System.Windows.Controls.Image image2 = FindVisualChildTool.ByName<System.Windows.Controls.Image>(FR, "statusMemo");
			System.Windows.Media.Brush background = ColorTool.HexColorToBrush("#F66F00");
			System.Windows.Media.Brush background2 = ColorTool.HexColorToBrush("#000000");
			switch (rb.Name)
			{
			case "btnBookMark":
			case "BookMarkButton":
			{
				bool? isChecked2 = rb.IsChecked;
				if (isChecked2.GetValueOrDefault() && isChecked2.HasValue)
				{
					BookMarkSP.Background = background;
					image.Visibility = Visibility.Visible;
				}
				else
				{
					BookMarkSP.Background = background2;
					image.Visibility = Visibility.Collapsed;
				}
				break;
			}
			case "NoteButton":
			case "btnNoteButton":
			{
				bool? isChecked = rb.IsChecked;
				if (isChecked.GetValueOrDefault() && isChecked.HasValue)
				{
					MemoSP.Background = background;
					image2.Visibility = Visibility.Visible;
				}
				else
				{
					MemoSP.Background = background2;
					image2.Visibility = Visibility.Collapsed;
				}
				break;
			}
			}
		}

		private void noteButton_Click()
		{
			try
			{
				GetMediaListPanelInReader();
				TextBox textBox = FindVisualChildByName<TextBox>(FR, "notePanel");
				int targetPageIndex = curPageIndex;
				if (textBox != null)
				{
					setNotesInMem(textBox.Text, targetPageIndex);
				}
				bookManager.saveNoteData(userBookSno, targetPageIndex.ToString(), textBox.Text);
				RadioButton radioButton = FindVisualChildByName<RadioButton>(FR, "NoteButton");
				if (textBox != null && textBox.Text.Equals(""))
				{
					radioButton.IsChecked = false;
					TriggerBookMark_NoteButtonOrElse(radioButton);
				}
				else
				{
					radioButton.IsChecked = true;
					TriggerBookMark_NoteButtonOrElse(radioButton);
				}
				Canvas mediaTableCanvasInReader = GetMediaTableCanvasInReader();
				mediaTableCanvasInReader.Visibility = Visibility.Collapsed;
				if (textBox != null)
				{
					sendBroadCast("{\"annotation\":\"" + textBox.Text + "\",\"pageIndex\":" + targetPageIndex + ",\"cmd\":\"R.SA\"}");
					sendBroadCast("{\"cmd\":\"R.DPA\"}");
				}
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}

		private void ReadWindow_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			StackPanel stackPanel = (StackPanel)GetImageInReader();
			TranslateTransform translateTransform = (TranslateTransform)tfgForImage.Children[1];
			ScaleTransform scaleTransform = (ScaleTransform)tfgForImage.Children[0];
			TranslateTransform translateTransform2 = (TranslateTransform)tfgForHyperLink.Children[1];
			ScaleTransform scaleTransform2 = (ScaleTransform)tfgForHyperLink.Children[0];
			double num = base.ActualHeight / base.ActualWidth;
			double num2 = stackPanel.ActualHeight / stackPanel.ActualWidth;
			if (scaleTransform.ScaleX == 1.0 && scaleTransform.ScaleY == 1.0)
			{
				if (sender is StackPanel)
				{
					if (num < num2)
					{
						translateTransform.X = 0.0;
					}
					else
					{
						translateTransform.Y = 0.0;
					}
				}
				else if (sender is Canvas)
				{
					if (num < num2)
					{
						translateTransform2.X = 0.0;
						translateTransform.X = 0.0;
					}
					else
					{
						translateTransform2.Y = 0.0;
						translateTransform.Y = 0.0;
					}
				}
			}
			else
			{
				Border borderInReader = GetBorderInReader();
				Canvas canvas = FindVisualChildByName<Canvas>(FR, "zoomCanvas");
				double num3 = Math.Abs(stackPanel.ActualWidth * scaleTransform.ScaleX - base.ActualWidth * ratio);
				double num4 = Math.Abs(stackPanel.ActualHeight * scaleTransform.ScaleY - base.ActualHeight * ratio);
				double num5 = canvas.Height / stackPanel.ActualHeight;
				double num6 = translateTransform.X.Equals(0.0) ? 0.5 : (0.5 - translateTransform.X * num5 * 2.0 / (canvas.Width * scaleTransform.ScaleX));
				double num7 = (Math.Abs(translateTransform.Y - num4 / 2.0) * num5 + borderInReader.ActualHeight / 2.0) / (canvas.Height * scaleTransform.ScaleY);
				double num8 = num4 / num3;
				sendBroadCast("{\"x\":" + num6 + ",\"y\":" + num7 + ",\"scale\":" + PDFScale + ",\"cmd\":\"R.ZC\"}");
			}
			if (sender is StackPanel)
			{
				imageOrigin = new System.Windows.Point(translateTransform.X, translateTransform.Y);
				((StackPanel)sender).MouseMove -= new MouseEventHandler(ReadWindow_MouseMove);
				((StackPanel)sender).PreviewMouseLeftButtonUp -= new MouseButtonEventHandler(ReadWindow_PreviewMouseLeftButtonUp);
			}
			else if (sender is Canvas)
			{
				imageOrigin = new System.Windows.Point(translateTransform.X, translateTransform.Y);
				hyperlinkOrigin = new System.Windows.Point(translateTransform2.X, translateTransform2.Y);
				((Canvas)sender).MouseMove -= new MouseEventHandler(ReadWindow_MouseMove);
				((Canvas)sender).PreviewMouseLeftButtonUp -= new MouseButtonEventHandler(ReadWindow_PreviewMouseLeftButtonUp);
			}
			e.Handled = true;
		}

		public void TakeAPicture(int PageIndex)
		{
		}

		private void firstTimeLoading()
		{
			if (!isSyncing && !isSyncOwner)
			{
				if (trialPages == 0)
				{
					initUserDataFromDB();
				}
				else
				{
					FindVisualChildByName<Canvas>(FR, "zoomCanvas").Background = null;
				}
			}
			initUserDataFromDB();
			iniUpperButtons();
			TextBlock textBlock = FindVisualChildByName<TextBlock>(FR, "TotalPageInReader");
			textBlock.Text = singleThumbnailImageAndPageList.Count.ToString();
			TextBlock textBlock2 = FindVisualChildByName<TextBlock>(FR, "CurPageInReader");
			textBlock2.Text = (curPageIndex + 1).ToString();
			WrapPanel wrapPanel = FindVisualChildByName<WrapPanel>(thumbNailListBox, "wrapPanel");
			if (hejMetadata.direction.Equals("right"))
			{
				wrapPanel.FlowDirection = FlowDirection.RightToLeft;
				RadioButton radioButton = FindVisualChildByName<RadioButton>(FR, "leftPageButton");
				radioButton.CommandBindings.Clear();
				radioButton.Command = NavigationCommands.NextPage;
				Binding binding = new Binding();
				binding.Source = FR;
				binding.Path = new PropertyPath("CanGoToNextPage");
				BindingOperations.SetBinding(radioButton, UIElement.IsEnabledProperty, binding);
				RadioButton radioButton2 = FindVisualChildByName<RadioButton>(FR, "rightPageButton");
				radioButton2.CommandBindings.Clear();
				radioButton2.Command = NavigationCommands.PreviousPage;
				Binding binding2 = new Binding();
				binding2.Source = FR;
				binding2.Path = new PropertyPath("CanGoToPreviousPage");
				BindingOperations.SetBinding(radioButton2, UIElement.IsEnabledProperty, binding2);
				KeyBinding keyBinding = new KeyBinding();
				KeyBinding keyBinding2 = new KeyBinding();
				base.InputBindings.Clear();
				keyBinding.Command = NavigationCommands.NextPage;
				keyBinding.Key = Key.Left;
				base.InputBindings.Add(keyBinding);
				keyBinding2.Command = NavigationCommands.PreviousPage;
				keyBinding2.Key = Key.Right;
				base.InputBindings.Add(keyBinding2);
			}
			else
			{
				try
				{
					wrapPanel.FlowDirection = FlowDirection.LeftToRight;
				}
				catch (Exception)
				{
				}
			}
			isFirstTimeLoaded = true;
			ToggleButton toggleButton = FindVisualChildByName<ToggleButton>(FR, "syncButton");
			Singleton_Socket.ReaderEvent = this;
			socket = Singleton_Socket.GetInstance(meetingId, account, userName, isSyncing);
			if (isSyncing && socket != null)
			{
				toggleButton.IsChecked = true;
				isSyncing = true;
				clearDataWhenSync();
				resetTransform();
				if (isSyncOwner)
				{
					buttonStatusWhenSyncing(Visibility.Collapsed, Visibility.Collapsed);
				}
				else
				{
					buttonStatusWhenSyncing(Visibility.Visible, Visibility.Visible);
				}
			}
			else
			{
				toggleButton.IsChecked = false;
			}
			LoadFirst_BookNote_BookMark();
			needToSendBroadCast = false;
			FR.PreviewLostKeyboardFocus += new KeyboardFocusChangedEventHandler(FR_PreviewLostKeyboardFocus);
			Keyboard.Focus(FR);
			checkImageStatusTimer = new DispatcherTimer();
			checkImageStatusTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
			checkImageStatusTimer.Tick += new EventHandler(checkImageStatus);
			needToSendBroadCast = false;
		}

		private void LoadFirst_BookNote_BookMark()
		{
			RadioButton radioButton = FindVisualChildByName<RadioButton>(FR, "BookMarkButton");
			if (bookMarkDictionary.ContainsKey(curPageIndex))
			{
				radioButton.IsChecked = ((bookMarkDictionary[curPageIndex].status == "0") ? true : false);
				TriggerBookMark_NoteButtonOrElse(radioButton);
			}
			else
			{
				radioButton.IsChecked = false;
				TriggerBookMark_NoteButtonOrElse(radioButton);
			}
			RadioButton radioButton2 = FindVisualChildByName<RadioButton>(FR, "NoteButton");
			TextBox textBox = FindVisualChildByName<TextBox>(FR, "notePanel");
			if (textBox != null)
			{
				textBox.Text = bookNoteDictionary[curPageIndex].text;
			}
			if (bookNoteDictionary.ContainsKey(curPageIndex))
			{
				if (bookNoteDictionary[curPageIndex].text.Equals(""))
				{
					radioButton2.IsChecked = false;
					TriggerBookMark_NoteButtonOrElse(radioButton2);
				}
				else
				{
					radioButton2.IsChecked = true;
					TriggerBookMark_NoteButtonOrElse(radioButton2);
				}
			}
			else
			{
				radioButton2.IsChecked = false;
				TriggerBookMark_NoteButtonOrElse(radioButton2);
			}
		}

		private void LastNext_Click(object sender, RoutedEventArgs e)
		{
		}

		private StackPanel getNotesAndMakeNote()
		{
			StackPanel mediaListPanelInReader = GetMediaListPanelInReader();
			double width = mediaListPanelInReader.Width;
			double height = mediaListPanelInReader.Height;
			FindVisualChildByName<Border>(FR, "mediaListBorder");
			double width2 = 100.0;
			double num = 20.0;
			string text = bookNoteDictionary.ContainsKey(curPageIndex) ? bookNoteDictionary[curPageIndex].text : "";
			StackPanel stackPanel = new StackPanel();
			TextBox textBox = new TextBox();
			textBox.Name = "notePanel";
			textBox.TextWrapping = TextWrapping.Wrap;
			textBox.AcceptsReturn = true;
			textBox.BorderBrush = System.Windows.Media.Brushes.White;
			textBox.Margin = new Thickness(2.0);
			textBox.Width = width - 4.0;
			textBox.Height = height - num - 8.0;
			textBox.Text = text;
			textBox.FontSize = 16.0;
			TextBox textBox2 = textBox;
			textBox2.KeyDown += new KeyEventHandler(noteTB_KeyDown);
			textBox2.TextChanged += new TextChangedEventHandler(noteTB_TextChanged);
			RadioButton radioButton = new RadioButton();
			radioButton.Name = "notePanelButton";
			TextBlock textBlock = new TextBlock();
			textBlock.VerticalAlignment = VerticalAlignment.Center;
			textBlock.HorizontalAlignment = HorizontalAlignment.Center;
			textBlock.Foreground = System.Windows.Media.Brushes.White;
			textBlock.Text = langMng.getLangString("save");
			radioButton.Content = textBlock;
			radioButton.Background = System.Windows.Media.Brushes.Black;
			radioButton.Margin = new Thickness(2.0);
			radioButton.Width = width2;
			radioButton.Height = num;
			RadioButton radioButton2 = radioButton;
			radioButton2.Click += new RoutedEventHandler(noteButton_Click);
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate73 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate73 = new MouseEventHandler(_003CgetNotesAndMakeNote_003Eb__72);
			}
			textBox2.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate73;
			stackPanel.Children.Add(textBox2);
			stackPanel.Children.Add(radioButton2);
			stackPanel.Orientation = Orientation.Vertical;
			return stackPanel;
		}

		private void noteTB_TextChanged(object sender, TextChangedEventArgs e)
		{
			TextBox textBox = FindVisualChildByName<TextBox>(FR, "notePanel");
			int num = curPageIndex;
			sendBroadCast("{\"annotation\":\"" + textBox.Text + "\",\"pageIndex\":" + num + ",\"cmd\":\"R.SA\"}");
		}

		private StackPanel getSearchPanelSet(double panelWidth, string txtInSearchBar)
		{
			StackPanel stackPanel = new StackPanel();
			RadioButton radioButton = new RadioButton();
			TextBlock textBlock = new TextBlock();
			textBlock.VerticalAlignment = VerticalAlignment.Center;
			textBlock.HorizontalAlignment = HorizontalAlignment.Center;
			textBlock.Foreground = System.Windows.Media.Brushes.White;
			textBlock.Text = langMng.getLangString("search");
			radioButton.Content = textBlock;
			radioButton.Background = System.Windows.Media.Brushes.Black;
			radioButton.Margin = new Thickness(6.0);
			radioButton.Width = 61.0;
			RadioButton radioButton2 = radioButton;
			radioButton2.Click += new RoutedEventHandler(searchButton_Click);
			TextBox textBox = new TextBox();
			textBox.Name = "searchBar";
			textBox.Text = txtInSearchBar;
			textBox.Margin = new Thickness(6.0);
			textBox.Width = panelWidth - 82.0;
			TextBox textBox2 = textBox;
			textBox2.KeyDown += new KeyEventHandler(searchTB_KeyDown);
			stackPanel.Children.Add(textBox2);
			stackPanel.Children.Add(radioButton2);
			stackPanel.Orientation = Orientation.Horizontal;
			stackPanel.Background = System.Windows.Media.Brushes.LightGray;
			return stackPanel;
		}

		private void ShareButton_Checked(object sender, RoutedEventArgs e)
		{
			MouseTool.ShowLoading();
			SentMailSP.Background = ColorTool.HexColorToBrush("#F66F00");
			SendEmail();
			RadioButton radioButton = (RadioButton)sender;
			radioButton.IsChecked = false;
		}

		private void SendEmail()
		{
			string str = bookPath;
			string text = str + "\\imgMail";
			Directory.CreateDirectory(text);
			string text2 = string.Concat(text, "\\", Guid.NewGuid(), ".jpg");
			Border borderInReader = GetBorderInReader();
			StackPanel stackPanel = (StackPanel)GetImageInReader();
			double num = borderInReader.ActualHeight / stackPanel.ActualHeight;
			double num2 = (borderInReader.ActualWidth - stackPanel.ActualWidth * num) / 2.0;
			double num3 = (SystemParameters.PrimaryScreenHeight - borderInReader.ActualHeight) / 2.0;
			string source = string.Format("0,0,{0},{1}", borderInReader.ActualWidth, borderInReader.ActualHeight);
			BitmapSource bmp = CaptureScreenshotTool.Capture(Rect.Parse(source));
			UseBitmapCodecsTool.WriteJpeg(text2, 30, bmp);
			string text3 = "";
			if (bookNoteDictionary.ContainsKey(curPageIndex))
			{
				text3 = bookNoteDictionary[curPageIndex].text;
			}
			GetAnnotationUpload.AsyncPOST(meetingId, bookId, email, text3, text2, new Action<AnnotationUpload>(_003CSendEmail_003Eb__77));
		}

		private void GetAnnotationUpload_DoAction(AnnotationUpload au)
		{
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<AnnotationUpload>(GetAnnotationUpload_DoAction), au);
				return;
			}
			if (au != null)
			{
				AutoClosingMessageBox.Show("資料已送出");
			}
			else
			{
				AutoClosingMessageBox.Show("傳送失敗");
			}
			SentMailSP.Background = ColorTool.HexColorToBrush("#000000");
			MouseTool.ShowArrow();
		}

		private void SentMail()
		{
			try
			{
				string requestUriString = webServiceURL;
				string path = hejMetadata.LImgList[curPageIndex].path;
				string text3 = bookPath + "\\" + path;
				string value = meetingId;
				string value2 = bookId;
				string value3 = email;
				string value4 = "";
				if (bookNoteDictionary.ContainsKey(curPageIndex))
				{
					value4 = bookNoteDictionary[curPageIndex].text;
				}
				string str = "----------------------------" + DateTime.Now.Ticks.ToString("x");
				byte[] bytes = Encoding.ASCII.GetBytes("\r\n--" + str + "\r\n");
				string format = "\r\n--" + str + "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}";
				string format2 = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n Content-Type: application/octet-stream\r\n\r\n";
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>").Append("<UserInfo><MeetingID>").Append(value)
					.Append("</MeetingID><AttachID>")
					.Append(value2)
					.Append("</AttachID><Email>")
					.Append(value3)
					.Append("</Email><Text>")
					.Append(value4)
					.Append("</Text></UserInfo>");
				Stream stream = new MemoryStream();
				string s = string.Format(format, "xmlDoc", stringBuilder.ToString());
				byte[] bytes2 = Encoding.UTF8.GetBytes(s);
				stream.Write(bytes2, 0, bytes2.Length);
				stream.Write(bytes, 0, bytes.Length);
				string s2 = string.Format(format2, "annotationImage", path);
				byte[] bytes3 = Encoding.UTF8.GetBytes(s2);
				stream.Write(bytes3, 0, bytes3.Length);
				Border borderInReader = GetBorderInReader();
				StackPanel stackPanel = (StackPanel)GetImageInReader();
				double num = borderInReader.ActualHeight / stackPanel.ActualHeight;
				double num2 = (borderInReader.ActualWidth - stackPanel.ActualWidth * num) / 2.0;
				double num3 = (int)((SystemParameters.PrimaryScreenHeight - borderInReader.ActualHeight) / 2.0);
				Bitmap bitmap = new Bitmap((int)(stackPanel.ActualWidth * num), (int)borderInReader.ActualHeight);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.CompositingQuality = CompositingQuality.HighQuality;
					graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
					graphics.SmoothingMode = SmoothingMode.HighQuality;
					if (Settings.Default.IsFlatUIReader)
					{
						graphics.CopyFromScreen((int)num2, (int)num3, 0, 0, bitmap.Size, CopyPixelOperation.SourceCopy);
					}
					else
					{
						graphics.CopyFromScreen((int)num2, (int)num3 + 30, 0, 0, bitmap.Size, CopyPixelOperation.SourceCopy);
					}
					graphics.Dispose();
				}
				string str2 = bookPath;
				string text = str2 + "\\imgMail";
				Directory.CreateDirectory(text);
				string text2 = string.Concat(text, "\\", Guid.NewGuid(), ".bmp");
				bitmap.Save(text2);
				FileStream fileStream = new FileStream(text2, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				byte[] array = new byte[1024];
				int num4 = 0;
				while ((num4 = fileStream.Read(array, 0, array.Length)) != 0)
				{
					stream.Write(array, 0, num4);
				}
				try
				{
					if (decodedPDFPages != null)
					{
						stream.Write(decodedPDFPages[0], 0, decodedPDFPages[0].Length);
					}
				}
				catch (Exception ex)
				{
					LogTool.Debug(ex);
				}
				try
				{
					stream.Write(bytes, 0, bytes.Length);
				}
				catch (Exception ex2)
				{
					LogTool.Debug(ex2);
				}
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUriString);
				httpWebRequest.Method = "POST";
				httpWebRequest.ContentType = "multipart/form-data; boundary=" + str;
				httpWebRequest.ContentLength = stream.Length;
				Stream requestStream = httpWebRequest.GetRequestStream();
				stream.Position = 0L;
				byte[] array2 = new byte[stream.Length];
				stream.Read(array2, 0, array2.Length);
				stream.Close();
				requestStream.Write(array2, 0, array2.Length);
				requestStream.Close();
				WebResponse response = httpWebRequest.GetResponse();
				requestStream = response.GetResponseStream();
				StreamReader streamReader = new StreamReader(requestStream);
				streamReader.ReadToEnd();
				streamReader.Close();
				requestStream.Close();
				response.Close();
				httpWebRequest = null;
				response = null;
				AutoClosingMessageBox.Show("資料已送出");
			}
			catch (Exception)
			{
				AutoClosingMessageBox.Show("傳送失敗");
			}
		}

		private ListBox hyftdSearch(string keyWord)
		{
			string text = bookPath + "\\HYWEB\\fulltext";
			string[] files = Directory.GetFiles(text);
			List<SearchRecord> list = new List<SearchRecord>();
			for (int i = 0; i < files.Length; i++)
			{
				string text2 = "";
				using (FileStream stream = new FileStream(files[i], FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
				{
					using (StreamReader streamReader = new StreamReader(stream))
					{
						text2 = streamReader.ReadToEnd();
					}
				}
				string text3 = searchKeyword(keyWord, text2);
				if (text3.Equals(""))
				{
					continue;
				}
				string text4 = files[i].Replace(text + "\\", "");
				text4 = text4.Replace(text4.Substring(text4.LastIndexOf('.')), "");
				for (int j = 0; j < hejMetadata.SImgList.Count; j++)
				{
					if (hejMetadata.SImgList[j].path.Contains(text4))
					{
						SearchRecord searchRecord = new SearchRecord(hejMetadata.SImgList[j].pageNum, text2, j + 1);
						searchRecord.imagePath = bookPath + "\\" + hejMetadata.SImgList[j].path;
						list.Add(searchRecord);
					}
				}
			}
			ListBox listBox = new ListBox();
			listBox.Style = (Style)FindResource("SearchListBoxStyle");
			listBox.ItemsSource = list;
			listBox.SelectionChanged += new SelectionChangedEventHandler(lb_SelectionChanged);
			return listBox;
		}

		private string searchKeyword(string skey, string txtStr)
		{
			string[] array = txtStr.Split(new string[1]
			{
				"\r\n"
			}, StringSplitOptions.RemoveEmptyEntries);
			short num = 30;
			string text = "";
			string text2 = "";
			short num2 = 0;
			short num3 = 0;
			short num4 = 0;
			skey = skey.ToUpper();
			string[] array2 = array;
			foreach (string text3 in array2)
			{
				text = text.ToUpper();
				num2 = 0;
				num3 = 0;
				num4 = 0;
				text = text3;
				string text4 = text;
				foreach (char c in text4)
				{
					if (c == skey[num2])
					{
						if (num2 == 0)
						{
							num3 = num4;
						}
						num2 = (short)(num2 + 1);
					}
					else
					{
						num2 = 0;
					}
					if (num2 == skey.Length)
					{
						num2 = 0;
						text2 = text.Substring(num3);
						if (text2.Length > num)
						{
							text2 = text2.Substring(0, num - 1);
						}
					}
					num4 = (short)(num4 + 1);
				}
			}
			return text2;
		}

		private void loadCurrentStrokes(int curIndex)
		{
			Action action = null;
			if (isSyncing && !isSyncOwner)
			{
				return;
			}
			Canvas canvas = FindVisualChildByName<Canvas>(FR, "zoomCanvas");
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
			if (canvas.Width.Equals(double.NaN) || canvas.Height.Equals(double.NaN))
			{
				return;
			}
			inkCanvas.Width = canvas.Width;
			inkCanvas.Height = canvas.Height;
			inkCanvas.RenderTransform = tfgForHyperLink;
			bookStrokesDictionary = bookManager.getStrokesDics(userBookSno);
			isFirstLoad = true;
			if (bookStrokesDictionary.ContainsKey(curIndex))
			{
				List<StrokesData> list = bookStrokesDictionary[curIndex];
				int count = list.Count;
				for (int i = 0; i < count; i++)
				{
					if (list[i].status == "0")
					{
						paintStrokeOnInkCanvas(list[i], canvas.Width, canvas.Height, 0.0, 0.0);
					}
				}
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003CloadCurrentStrokes_003Eb__78);
				}
				factory.StartNew(action);
			}
			isFirstLoad = false;
		}

		private void TextBlock_TargetUpdated_1(object sender, DataTransferEventArgs e)
		{
			Action action = null;
			if (doubleReadPagePair.Count == 0 || singleReadPagePair.Count == 0)
			{
				return;
			}
			decodedPDFPages[0] = null;
			decodedPDFPages[1] = null;
			Canvas stageCanvasInReader = GetStageCanvasInReader();
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
			openedby = MediaCanvasOpenedBy.None;
			isAreaButtonAndPenMemoRequestSent = false;
			if (stageCanvasInReader.Children.Count > 0)
			{
				stageCanvasInReader.Children.Clear();
				RadioButton radioButton = FindVisualChildByName<RadioButton>(FR, "FullTextButton");
				radioButton.Visibility = Visibility.Collapsed;
			}
			if (inkCanvas.Strokes.Count > 0)
			{
				convertCurrentStrokesToDB(hejMetadata.LImgList[curPageIndex].pageId);
				inkCanvas.Strokes.Clear();
			}
			if (splineString != "")
			{
				try
				{
					drawStrokeFromJson(splineString);
					splineString = "";
				}
				catch
				{
				}
			}
			if (closeBook)
			{
			}
			TextBlock textBlock = (TextBlock)sender;
			if (textBlock == null)
			{
				return;
			}
			if (!isFirstTimeLoaded)
			{
				firstTimeLoading();
				inkCanvas.StrokeCollected += new InkCanvasStrokeCollectedEventHandler(penMemoCanvasStrokeCollected);
				inkCanvas.StrokeErasing += new InkCanvasStrokeErasingEventHandler(penMemoCanvas_StrokeErasing);
				inkCanvas.StrokeErased += new RoutedEventHandler(penMemoCanvas_StrokeErased);
				if (lastViewPage == null)
				{
					showLastReadPageAndStartPreload();
					ifAskedJumpPage = true;
				}
				else if (lastViewPage.ContainsKey(CName))
				{
					if (lastViewPage[CName].index != 0 && !isSyncing)
					{
						Canvas canvas = FindVisualChildByName<Canvas>(FR, "zoomCanvas");
						BrushConverter brushConverter = new BrushConverter();
						canvas.Background = (System.Windows.Media.Brush)brushConverter.ConvertFrom("#FF212020");
						isFirstTimeChangingPage = true;
					}
					else
					{
						showLastReadPageAndStartPreload();
					}
				}
				else
				{
					showLastReadPageAndStartPreload();
				}
				return;
			}
			curPageIndex = Convert.ToInt32(textBlock.Text) - 1;
			if (isFirstTimeChangingPage)
			{
				showLastReadPageAndStartPreload();
				isFirstTimeChangingPage = false;
			}
			Canvas canvas2 = FindVisualChildByName<Canvas>(FR, "zoomCanvas");
			canvas2.Background = null;
			zoomeThread.Clear();
			isPDFRendering = false;
			TaskFactory factory = Task.Factory;
			if (action == null)
			{
				action = new Action(_003CTextBlock_TargetUpdated_1_003Eb__7b);
			}
			factory.StartNew(action);
			lastTimeOfChangingPage = DateTime.Now;
			if (viewStatusIndex.Equals(PageMode.SinglePage))
			{
				if (singleReadPagePair[curPageIndex].leftImageSource != null)
				{
					useOriginalCanvasOnLockStatus = true;
					try
					{
						SendImageSourceToZoomCanvas((BitmapImage)singleReadPagePair[curPageIndex].leftImageSource);
						singleImgStatus[curPageIndex] = ImageStatus.LARGEIMAGE;
					}
					catch (Exception)
					{
						singleReadPagePair[curPageIndex].leftImageSource = null;
					}
				}
			}
			else if (viewStatusIndex.Equals(PageMode.DoublePage) && doubleReadPagePair[curPageIndex].leftImageSource != null)
			{
				useOriginalCanvasOnLockStatus = true;
				try
				{
					SendImageSourceToZoomCanvas((BitmapImage)doubleReadPagePair[curPageIndex].leftImageSource);
					doubleImgStatus[curPageIndex] = ImageStatus.LARGEIMAGE;
				}
				catch (Exception)
				{
					doubleReadPagePair[curPageIndex].leftImageSource = null;
				}
			}
			int num = 0;
			try
			{
				RepaintPDF(zoomStepScale[zoomStep]);
			}
			catch (Exception)
			{
				if (num++ <= 3)
				{
					RepaintPDF(zoomStepScale[zoomStep]);
				}
			}
			if (curPageIndex < hejMetadata.SImgList.Count)
			{
				if (curPageIndex < 0)
				{
					return;
				}
				if (!isSyncing)
				{
					bookMarkDictionary = bookManager.getBookMarkDics(userBookSno);
					bookNoteDictionary = bookManager.getBookNoteDics(userBookSno);
				}
				RadioButton radioButton2 = FindVisualChildByName<RadioButton>(FR, "BookMarkButton");
				RadioButton radioButton3 = FindVisualChildByName<RadioButton>(FR, "NoteButton");
				if (viewStatusIndex.Equals(PageMode.SinglePage))
				{
					if (curPageIndex > singleThumbnailImageAndPageList.Count - 1)
					{
						return;
					}
					thumbNailListBox.SelectedItem = singleThumbnailImageAndPageList[curPageIndex];
					TextBlock textBlock2 = FindVisualChildByName<TextBlock>(FR, "CurPageInReader");
					textBlock2.Text = (curPageIndex + 1).ToString();
					if (bookMarkDictionary.ContainsKey(curPageIndex))
					{
						if (bookMarkDictionary[curPageIndex].status == "0")
						{
							radioButton2.IsChecked = true;
							TriggerBookMark_NoteButtonOrElse(radioButton2);
						}
						else
						{
							radioButton2.IsChecked = false;
							TriggerBookMark_NoteButtonOrElse(radioButton2);
						}
					}
					else
					{
						radioButton2.IsChecked = false;
						TriggerBookMark_NoteButtonOrElse(radioButton2);
					}
					if (bookNoteDictionary.ContainsKey(curPageIndex))
					{
						if (bookNoteDictionary[curPageIndex].status == "0")
						{
							radioButton3.IsChecked = true;
							TriggerBookMark_NoteButtonOrElse(radioButton3);
						}
						else
						{
							radioButton3.IsChecked = false;
							TriggerBookMark_NoteButtonOrElse(radioButton3);
						}
					}
					else
					{
						radioButton3.IsChecked = false;
						TriggerBookMark_NoteButtonOrElse(radioButton3);
					}
				}
				else if (viewStatusIndex.Equals(PageMode.DoublePage))
				{
					ReadPagePair readPagePair = doubleReadPagePair[curPageIndex];
					int num2 = Math.Max(readPagePair.rightPageIndex, readPagePair.leftPageIndex);
					if (num2 < thumbNailListBox.Items.Count)
					{
						thumbNailListBox.SelectedItem = thumbNailListBox.Items[num2];
					}
					if (readPagePair.rightPageIndex == -1 || readPagePair.leftPageIndex == -1)
					{
						bool value = false;
						int num3 = Math.Max(readPagePair.rightPageIndex, readPagePair.leftPageIndex);
						if (bookMarkDictionary.ContainsKey(num3) && bookMarkDictionary[num3].status == "0")
						{
							value = true;
						}
						TextBlock textBlock3 = FindVisualChildByName<TextBlock>(FR, "CurPageInReader");
						textBlock3.Text = (num3 + 1).ToString();
						radioButton2.IsChecked = value;
						TriggerBookMark_NoteButtonOrElse(radioButton2);
					}
					else
					{
						TextBlock textBlock4 = FindVisualChildByName<TextBlock>(FR, "CurPageInReader");
						textBlock4.Text = (readPagePair.leftPageIndex + 1 + "-" + (readPagePair.rightPageIndex + 1)).ToString();
						bool flag = false;
						bool flag2 = false;
						if (bookMarkDictionary.ContainsKey(readPagePair.leftPageIndex) && bookMarkDictionary[readPagePair.leftPageIndex].status == "0")
						{
							flag = true;
						}
						if (bookMarkDictionary.ContainsKey(readPagePair.rightPageIndex) && bookMarkDictionary[readPagePair.rightPageIndex].status == "0")
						{
							flag2 = true;
						}
						if (flag || flag2)
						{
							radioButton2.IsChecked = true;
							TriggerBookMark_NoteButtonOrElse(radioButton2);
						}
						else
						{
							radioButton2.IsChecked = false;
							TriggerBookMark_NoteButtonOrElse(radioButton2);
						}
					}
				}
			}
			checkOtherDevicePage();
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/readwindow.xaml", UriKind.Relative);
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
			case 31:
				mainGrid = (Grid)target;
				break;
			case 32:
				FR = (FlowDocumentReader)target;
				FR.PreviewMouseWheel += new MouseWheelEventHandler(FR_PreviewMouseWheel);
				FR.PreviewKeyDown += new KeyEventHandler(FR_PreviewKeyDown);
				break;
			case 33:
				downloadProgBar = (ProgressBar)target;
				break;
			case 34:
				NewUITop = (Grid)target;
				break;
			case 35:
				btnFuncSP = (StackPanel)target;
				break;
			case 36:
				btnBoldSP = (StackPanel)target;
				btnBoldSP.MouseLeave += new MouseEventHandler(btnBoldSP_MouseLeave);
				break;
			case 37:
				btnThin = (Grid)target;
				btnThin.MouseEnter += new MouseEventHandler(Grid_MouseEnterTransparent);
				btnThin.MouseLeave += new MouseEventHandler(Grid_MouseLeaveTransparent);
				break;
			case 38:
				((RadioButton)target).Click += new RoutedEventHandler(btnThickness_Click);
				break;
			case 39:
				btnMedium = (Grid)target;
				btnMedium.MouseEnter += new MouseEventHandler(Grid_MouseEnterTransparent);
				btnMedium.MouseLeave += new MouseEventHandler(Grid_MouseLeaveTransparent);
				break;
			case 40:
				((RadioButton)target).Click += new RoutedEventHandler(btnThickness_Click);
				break;
			case 41:
				btnLarge = (Grid)target;
				btnLarge.MouseEnter += new MouseEventHandler(Grid_MouseEnterTransparent);
				btnLarge.MouseLeave += new MouseEventHandler(Grid_MouseLeaveTransparent);
				break;
			case 42:
				((RadioButton)target).Click += new RoutedEventHandler(btnThickness_Click);
				break;
			case 43:
				btnPenFuncSP = (StackPanel)target;
				btnPenFuncSP.MouseLeave += new MouseEventHandler(btnPenFuncSP_MouseLeave);
				break;
			case 44:
				PenColorSP = (StackPanel)target;
				break;
			case 45:
				((Grid)target).MouseEnter += new MouseEventHandler(Grid_MouseEnter);
				((Grid)target).MouseLeave += new MouseEventHandler(Grid_MouseLeave);
				break;
			case 46:
				((RadioButton)target).Click += new RoutedEventHandler(btnPenColor_Click);
				break;
			case 47:
				((Grid)target).MouseEnter += new MouseEventHandler(Grid_MouseEnter);
				((Grid)target).MouseLeave += new MouseEventHandler(Grid_MouseLeave);
				break;
			case 48:
				((RadioButton)target).Click += new RoutedEventHandler(btnPenColor_Click);
				break;
			case 49:
				((Grid)target).MouseEnter += new MouseEventHandler(Grid_MouseEnter);
				((Grid)target).MouseLeave += new MouseEventHandler(Grid_MouseLeave);
				break;
			case 50:
				((RadioButton)target).Click += new RoutedEventHandler(btnPenColor_Click);
				break;
			case 51:
				((Grid)target).MouseEnter += new MouseEventHandler(Grid_MouseEnter);
				((Grid)target).MouseLeave += new MouseEventHandler(Grid_MouseLeave);
				break;
			case 52:
				((RadioButton)target).Click += new RoutedEventHandler(btnPenColor_Click);
				break;
			case 53:
				((Grid)target).MouseEnter += new MouseEventHandler(Grid_MouseEnter);
				((Grid)target).MouseLeave += new MouseEventHandler(Grid_MouseLeave);
				break;
			case 54:
				((RadioButton)target).Click += new RoutedEventHandler(btnPenColor_Click);
				break;
			case 55:
				((Grid)target).MouseEnter += new MouseEventHandler(Grid_MouseEnter);
				((Grid)target).MouseLeave += new MouseEventHandler(Grid_MouseLeave);
				break;
			case 56:
				((RadioButton)target).Click += new RoutedEventHandler(btnPenColor_Click);
				break;
			case 57:
				((Grid)target).MouseEnter += new MouseEventHandler(Grid_MouseEnter);
				((Grid)target).MouseLeave += new MouseEventHandler(Grid_MouseLeave);
				break;
			case 58:
				((RadioButton)target).Click += new RoutedEventHandler(btnPenColor_Click);
				break;
			case 59:
				((Grid)target).MouseEnter += new MouseEventHandler(Grid_MouseEnter);
				((Grid)target).MouseLeave += new MouseEventHandler(Grid_MouseLeave);
				break;
			case 60:
				((RadioButton)target).Click += new RoutedEventHandler(btnPenColor_Click);
				break;
			case 61:
				((Grid)target).MouseEnter += new MouseEventHandler(Grid_MouseEnter);
				((Grid)target).MouseLeave += new MouseEventHandler(Grid_MouseLeave);
				break;
			case 62:
				((RadioButton)target).Click += new RoutedEventHandler(btnPenColor_Click);
				break;
			case 63:
				((Grid)target).MouseEnter += new MouseEventHandler(Grid_MouseEnter);
				((Grid)target).MouseLeave += new MouseEventHandler(Grid_MouseLeave);
				break;
			case 64:
				((RadioButton)target).Click += new RoutedEventHandler(btnPenColor_Click);
				break;
			case 65:
				NewUI = (Grid)target;
				break;
			case 66:
				btnPen = (RadioButton)target;
				btnPen.Click += new RoutedEventHandler(btnPen_Click);
				break;
			case 67:
				PenSlideCtrl = (System.Windows.Controls.Image)target;
				break;
			case 68:
				PenSP = (StackPanel)target;
				break;
			case 69:
				btnPenColor = (RadioButton)target;
				break;
			case 70:
				btnBold = (RadioButton)target;
				break;
			case 71:
				btnEraserGD = (Grid)target;
				break;
			case 72:
				btnEraser = (RadioButton)target;
				btnEraser.Click += new RoutedEventHandler(btnEraser_Click);
				break;
			case 73:
				((RadioButton)target).Click += new RoutedEventHandler(btnSetting_Click);
				break;
			case 74:
				SettingSlideCtrl = (System.Windows.Controls.Image)target;
				break;
			case 75:
				SettingSP = (StackPanel)target;
				break;
			case 76:
				BookMarkSP = (StackPanel)target;
				break;
			case 77:
				btnBookMark = (RadioButton)target;
				btnBookMark.Click += new RoutedEventHandler(BookMarkButton_Checked);
				break;
			case 78:
				MemoSP = (StackPanel)target;
				break;
			case 79:
				btnNoteButton = (RadioButton)target;
				btnNoteButton.Click += new RoutedEventHandler(NoteButton_Checked);
				break;
			case 80:
				SentMailSP = (StackPanel)target;
				break;
			case 81:
				((RadioButton)target).Click += new RoutedEventHandler(ShareButton_Checked);
				break;
			case 82:
				ViewThumbSP = (StackPanel)target;
				break;
			case 83:
				btnViewThumb = (RadioButton)target;
				btnViewThumb.Click += new RoutedEventHandler(ShowListBoxButtonNew_Click);
				break;
			case 84:
				((RadioButton)target).Click += new RoutedEventHandler(BackToBookShelfButton_Click);
				break;
			case 85:
				btnClose = (System.Windows.Controls.Image)target;
				break;
			case 86:
				thumnailCanvas = (Canvas)target;
				break;
			case 87:
				SearchSP = (StackPanel)target;
				break;
			case 88:
				txtKeyword = (TextBox)target;
				break;
			case 89:
				btnTxtKeywordClear = (RadioButton)target;
				break;
			case 90:
				txtFilterCount = (TextBlock)target;
				break;
			case 91:
				thumbNailCanvasStackPanel = (StackPanel)target;
				break;
			case 92:
				thumbNailCanvasGrid = (Grid)target;
				break;
			case 93:
				RadioButtonStackPanel = (StackPanel)target;
				break;
			case 94:
				AllImageButtonInListBox = (RadioButton)target;
				AllImageButtonInListBox.Click += new RoutedEventHandler(AllImageButtonInListBox_Checked);
				break;
			case 95:
				AllImageButtonInListBoxSP = (StackPanel)target;
				AllImageButtonInListBoxSP.MouseLeftButtonDown += new MouseButtonEventHandler(AllImageButtonInListBoxSP_MouseLeftButtonDown);
				break;
			case 96:
				AllImageButtonInListBoxNew = (RadioButton)target;
				AllImageButtonInListBoxNew.Click += new RoutedEventHandler(AllImageButtonInListBox_Checked);
				break;
			case 97:
				BookMarkButtonInListBox = (RadioButton)target;
				BookMarkButtonInListBox.Click += new RoutedEventHandler(BookMarkButtonInListBox_Checked);
				break;
			case 98:
				Rect1 = (System.Windows.Shapes.Rectangle)target;
				break;
			case 99:
				BookMarkButtonInListBoxSP = (StackPanel)target;
				BookMarkButtonInListBoxSP.MouseLeftButtonDown += new MouseButtonEventHandler(BookMarkButtonInListBoxSP_MouseLeftButtonDown);
				break;
			case 100:
				BookMarkButtonInListBoxNew = (RadioButton)target;
				BookMarkButtonInListBoxNew.Click += new RoutedEventHandler(BookMarkButtonInListBox_Checked);
				break;
			case 101:
				NoteButtonInListBox = (RadioButton)target;
				NoteButtonInListBox.Click += new RoutedEventHandler(NoteButtonInListBox_Checked);
				break;
			case 102:
				Rect2 = (System.Windows.Shapes.Rectangle)target;
				break;
			case 103:
				NoteButtonInListBoxSP = (StackPanel)target;
				NoteButtonInListBoxSP.MouseLeftButtonDown += new MouseButtonEventHandler(NoteButtonInListBoxSP_MouseLeftButtonDown);
				break;
			case 104:
				NoteButtonInListBoxNew = (RadioButton)target;
				NoteButtonInListBoxNew.Click += new RoutedEventHandler(NoteButtonInListBox_Checked);
				break;
			case 105:
				HideListBoxButton = (RadioButton)target;
				HideListBoxButton.Click += new RoutedEventHandler(HideListBoxButton_Checked);
				break;
			case 106:
				thumbNailListBoxGD = (Grid)target;
				break;
			case 107:
				thumbNailListBox = (ListBox)target;
				thumbNailListBox.SelectionChanged += new SelectionChangedEventHandler(thumbNailListBox_SelectionChanged);
				break;
			case 108:
				ShowListBoxButton = (RadioButton)target;
				ShowListBoxButton.Click += new RoutedEventHandler(ShowListBoxButton_Click);
				break;
			case 109:
				LockButton = (RadioButton)target;
				LockButton.Click += new RoutedEventHandler(LockButton_Checked);
				break;
			case 110:
				DV = (DocumentViewer)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				((Button)target).Click += new RoutedEventHandler(PrintButton_Click);
				break;
			case 2:
				((Button)target).Click += new RoutedEventHandler(CloseButton_Click);
				break;
			case 3:
				((RadioButton)target).Click += new RoutedEventHandler(PenMemoButton_Checked);
				break;
			case 4:
				((RadioButton)target).Click += new RoutedEventHandler(BackToBookShelfButton_Click);
				break;
			case 5:
				((RadioButton)target).Click += new RoutedEventHandler(FullScreenButton_Checked);
				break;
			case 6:
				((RadioButton)target).Click += new RoutedEventHandler(PageViewButton_Checked);
				break;
			case 7:
				((RadioButton)target).Click += new RoutedEventHandler(TwoPageViewButton_Checked);
				break;
			case 8:
				((TextBlock)target).TargetUpdated += new EventHandler<DataTransferEventArgs>(TextBlock_TargetUpdated_1);
				break;
			case 9:
				((RadioButton)target).Click += new RoutedEventHandler(FullTextButton_Checked);
				break;
			case 10:
				((RadioButton)target).Click += new RoutedEventHandler(PrintButton_Checked);
				break;
			case 11:
				((RadioButton)target).Click += new RoutedEventHandler(MediaListButton_Checked);
				break;
			case 12:
				((RadioButton)target).Click += new RoutedEventHandler(ShowAllImageButton_Checked);
				break;
			case 13:
				((RadioButton)target).Click += new RoutedEventHandler(ContentButton_Checked);
				break;
			case 14:
				((RadioButton)target).Click += new RoutedEventHandler(TocButton_Checked);
				break;
			case 15:
				((RadioButton)target).Click += new RoutedEventHandler(SettingsButton_Checked);
				break;
			case 16:
				((RadioButton)target).Click += new RoutedEventHandler(SearchButton_Checked);
				break;
			case 17:
				((RadioButton)target).Click += new RoutedEventHandler(PenMemoButton_Checked);
				break;
			case 18:
				((RadioButton)target).Click += new RoutedEventHandler(BookMarkButton_Checked);
				break;
			case 19:
				((RadioButton)target).Click += new RoutedEventHandler(NoteButton_Checked);
				break;
			case 20:
				((RadioButton)target).Click += new RoutedEventHandler(ShareButton_Checked);
				break;
			case 21:
				((RepeatButton)target).Click += new RoutedEventHandler(RepeatButton_Click_1);
				break;
			case 22:
				((Slider)target).ValueChanged += new RoutedPropertyChangedEventHandler<double>(SliderInReader_ValueChanged);
				break;
			case 23:
				((RepeatButton)target).Click += new RoutedEventHandler(RepeatButton_Click_2);
				break;
			case 24:
				((ToggleButton)target).Click += new RoutedEventHandler(syncButton_Click);
				break;
			case 25:
				((Canvas)target).MouseLeftButtonDown += new MouseButtonEventHandler(MediaTableCanvas_MouseLeftButtonDown);
				break;
			case 26:
				((Canvas)target).MouseLeftButtonDown += new MouseButtonEventHandler(ImageInReader_MouseLeftButtonDown);
				break;
			case 27:
				((Canvas)target).MouseLeftButtonDown += new MouseButtonEventHandler(PopupControlCanvas_MouseLeftButtonDown);
				break;
			case 28:
				((RadioButton)target).Click += new RoutedEventHandler(ButtonInMediaList_Click);
				break;
			case 29:
				((RadioButton)target).Click += new RoutedEventHandler(sharePlatformButton_Click);
				break;
			case 30:
			{
				EventSetter eventSetter = new EventSetter();
				eventSetter.Event = FrameworkElement.RequestBringIntoViewEvent;
				eventSetter.Handler = new RequestBringIntoViewEventHandler(TreeViewItem_RequestBringIntoView);
				((Style)target).Setters.Add(eventSetter);
				break;
			}
			}
		}

		[CompilerGenerated]
		private void _003CprepareReadingPageDataSource_003Eb__4()
		{
			base.Dispatcher.BeginInvoke(new Action(_003CprepareReadingPageDataSource_003Eb__5));
		}

		[CompilerGenerated]
		private void _003CprepareReadingPageDataSource_003Eb__5()
		{
			Panel.SetZIndex(thumnailCanvas, -10);
			thumnailCanvas.Visibility = Visibility.Visible;
			thumbNailListBox.ItemsSource = singleThumbnailImageAndPageList;
			thumnailCanvas.Visibility = Visibility.Collapsed;
			Panel.SetZIndex(thumnailCanvas, 200);
		}

		[CompilerGenerated]
		private void _003CShowNote_003Eb__e()
		{
			base.Dispatcher.BeginInvoke(new Action(_003CShowNote_003Eb__f));
		}

		[CompilerGenerated]
		private static int _003CShowNote_003Eb__10(ThumbnailImageAndPage x)
		{
			return int.Parse(x.pageIndex);
		}

		[CompilerGenerated]
		private void _003CShowNote_003Eb__f()
		{
			thumbNailListBox.ItemsSource = singleThumbnailImageAndPageList;
		}

		[CompilerGenerated]
		private void _003CNoteButton_Checked_003Eb__13()
		{
			thumnailCanvas.Visibility = Visibility.Collapsed;
		}

		[CompilerGenerated]
		private void _003CShowBookMark_003Eb__15()
		{
			base.Dispatcher.BeginInvoke(new Action(_003CShowBookMark_003Eb__16));
		}

		[CompilerGenerated]
		private static int _003CShowBookMark_003Eb__17(ThumbnailImageAndPage x)
		{
			return int.Parse(x.pageIndex);
		}

		[CompilerGenerated]
		private void _003CShowBookMark_003Eb__16()
		{
			thumbNailListBox.ItemsSource = singleThumbnailImageAndPageList;
		}

		[CompilerGenerated]
		private static int _003CShowAll_003Eb__1a(ThumbnailImageAndPage x)
		{
			return int.Parse(x.pageIndex);
		}

		[CompilerGenerated]
		private void _003CChangeThumbNailListBoxRelativeStatus_003Eb__1c()
		{
			base.Dispatcher.BeginInvoke(new Action(_003CChangeThumbNailListBoxRelativeStatus_003Eb__1d));
		}

		[CompilerGenerated]
		private void _003CChangeThumbNailListBoxRelativeStatus_003Eb__1d()
		{
			thumbNailListBox.ItemsSource = singleThumbnailImageAndPageList;
		}

		[CompilerGenerated]
		private void _003CShowListBoxButtonNew_Click_003Eb__1f()
		{
			thumnailCanvas.Visibility = Visibility.Collapsed;
		}

		[CompilerGenerated]
		private void _003CShowListBoxButtonNew_Click_003Eb__20()
		{
			thumnailCanvas.Visibility = Visibility.Visible;
		}

		[CompilerGenerated]
		private void _003CShowListBoxButtonNew_Click_003Eb__21()
		{
			Thread.Sleep(300);
			Dispatcher dispatcher = base.Dispatcher;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate25 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate25 = new Action(_003CShowListBoxButtonNew_Click_003Eb__22);
			}
			dispatcher.BeginInvoke(CS_0024_003C_003E9__CachedAnonymousMethodDelegate25);
		}

		[CompilerGenerated]
		private static void _003CShowListBoxButtonNew_Click_003Eb__22()
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private void _003CStartAnimation_003Eb__27()
		{
			thumnailCanvas.Visibility = Visibility.Collapsed;
		}

		[CompilerGenerated]
		private static void _003CStartAnimation_003Eb__28(object sender2, EventArgs e2)
		{
		}

		[CompilerGenerated]
		private void _003CReadWindow_Loaded_003Eb__45(object sender3, EventArgs e3)
		{
			InitPen();
			Task.Factory.StartNew(new Action(_003CReadWindow_Loaded_003Eb__46));
		}

		[CompilerGenerated]
		private void _003CReadWindow_Loaded_003Eb__46()
		{
			Thread.Sleep(6000);
			CanSentLine = true;
		}

		[CompilerGenerated]
		private void _003CInitPen_003Eb__47()
		{
			Thread.Sleep(5000);
			base.Dispatcher.BeginInvoke(new Action(_003CInitPen_003Eb__48));
		}

		[CompilerGenerated]
		private void _003CInitPen_003Eb__48()
		{
			string text = socketMessage;
			if (!socketMessage.Equals(""))
			{
				parseJSonFromMessage(socketMessage);
				needToSendBroadCast = false;
				socketMessage = "";
			}
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(FR, "penMemoCanvas");
			if (inkCanvas.Strokes.Count <= 0 && !text.Equals(""))
			{
				parseJSonFromMessage(socketMessage);
			}
		}

		[CompilerGenerated]
		private static int _003CSavePDF_003Eb__57(string a, string b)
		{
			int num = int.Parse(new FileInfo(a).Name.Split('.')[0]);
			int value = int.Parse(new FileInfo(b).Name.Split('.')[0]);
			return num.CompareTo(value);
		}

		[CompilerGenerated]
		private void _003CInitSyncCenter_003Eb__5a()
		{
			if (Settings.Default.HasSyncCenterModule)
			{
				try
				{
					SyncCenter syncCenter = new SyncCenter();
					syncCenter.bookManager = new BookManager(dbPath);
					getBookPath();
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
						AbstractSyncManager syncManager = (AbstractSyncManager)Activator.CreateInstance(type, account, "free", bookId, userBookSno, key, 0, "0", WsTool.GetAbstractSyncCenter_BASE_URL());
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
		private void _003CtxtKeyword_PreviewKeyDown_003Eb__5b()
		{
			Thread.Sleep(10);
			base.Dispatcher.BeginInvoke(new Action(_003CtxtKeyword_PreviewKeyDown_003Eb__5c));
		}

		[CompilerGenerated]
		private void _003CtxtKeyword_PreviewKeyDown_003Eb__5c()
		{
			if (txtKeyword.Text.Length > 0)
			{
				btnTxtKeywordClear.Visibility = Visibility.Visible;
			}
			else
			{
				btnTxtKeywordClear.Visibility = Visibility.Collapsed;
			}
		}

		[CompilerGenerated]
		private static int _003CtxtKeyword_KeyUp_003Eb__5d(SearchRecord x)
		{
			return x.targetPage - 1;
		}

		[CompilerGenerated]
		private void _003CAttachKey_003Eb__5f(object sender, KeyEventArgs e)
		{
			if (!isSyncing || isSyncOwner)
			{
				switch (e.Key)
				{
				case Key.ImeConvert:
				case Key.ImeNonConvert:
				case Key.ImeAccept:
				case Key.ImeModeChange:
				case Key.Space:
				case Key.Prior:
				case Key.Next:
				case Key.End:
				case Key.Home:
				case Key.Left:
				case Key.Up:
				case Key.Right:
				case Key.Down:
					break;
				case Key.Escape:
					OpenClosePaint();
					break;
				}
			}
		}

		[CompilerGenerated]
		private void _003CChangeFlatUI_003Eb__60(object sender, MouseButtonEventArgs e)
		{
			OpenClosePaint();
		}

		[CompilerGenerated]
		private void _003CChangeFlatUI_003Eb__61(object sender, MouseEventArgs e)
		{
			MouseTool.ShowIBeam();
			txtKeyword.Focus();
		}

		[CompilerGenerated]
		private static void _003CChangeFlatUI_003Eb__62(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private void _003CChangeFlatUI_003Eb__63(object sender, RoutedEventArgs e)
		{
			txtKeyword.Text = "";
			txtKeyword.Focus();
			btnTxtKeywordClear.Visibility = Visibility.Collapsed;
			int num = 0;
			int num2 = 0;
			foreach (ThumbnailImageAndPage item in (IEnumerable)thumbNailListBox.Items)
			{
				ThumbnailImageAndPage thumbnailImageAndPage = item;
				ListBoxItem listBoxItem = (ListBoxItem)thumbNailListBox.ItemContainerGenerator.ContainerFromIndex(num2);
				if (listBoxItem != null)
				{
					listBoxItem.Visibility = Visibility.Visible;
				}
				num++;
				num2++;
			}
			txtFilterCount.Text = string.Format("有 {0} 筆相關資料", num.ToString());
		}

		[CompilerGenerated]
		private void _003CChangeFlatUI_003Eb__64(object sender, RoutedEventArgs e)
		{
			if (btnPenFuncSP.Height > 0.0)
			{
				btnPenColor.Background = ColorTool.HexColorToBrush("#000000");
				MyAnimation(btnPenFuncSP, 300.0, "Height", btnPenFuncSP.ActualHeight, 0.0);
			}
			if (btnFuncSP.Height > 0.0)
			{
				btnBold.Background = ColorTool.HexColorToBrush("#000000");
				MyAnimation(btnFuncSP, 300.0, "Height", btnFuncSP.ActualHeight, 0.0);
			}
			else
			{
				ShowNowPenBold();
				btnBold.Background = ColorTool.HexColorToBrush("#F66F00");
				MyAnimation(btnFuncSP, 300.0, "Height", 0.0, btnFuncSP.ActualHeight);
			}
		}

		[CompilerGenerated]
		private void _003CChangeFlatUI_003Eb__65(object sender, RoutedEventArgs e)
		{
			if (btnFuncSP.Height > 0.0)
			{
				btnBold.Background = ColorTool.HexColorToBrush("#000000");
				MyAnimation(btnFuncSP, 300.0, "Height", btnFuncSP.ActualHeight, 0.0);
			}
			if (btnPenFuncSP.Height > 0.0)
			{
				btnPenColor.Background = ColorTool.HexColorToBrush("#000000");
				MyAnimation(btnPenFuncSP, 300.0, "Height", btnPenFuncSP.ActualHeight, 0.0);
			}
			else
			{
				ShowNowPenColor();
				btnPenColor.Background = ColorTool.HexColorToBrush("#F66F00");
				MyAnimation(btnPenFuncSP, 300.0, "Height", 0.0, btnPenFuncSP.ActualHeight);
			}
		}

		[CompilerGenerated]
		private static void _003CgetNotesAndMakeNote_003Eb__72(object sender, MouseEventArgs e)
		{
		}

		[CompilerGenerated]
		private void _003CSendEmail_003Eb__77(AnnotationUpload au)
		{
			GetAnnotationUpload_DoAction(au);
		}

		[CompilerGenerated]
		private void _003CloadCurrentStrokes_003Eb__78()
		{
			if (!CanSentLine)
			{
				Thread.Sleep(2000);
			}
			base.Dispatcher.BeginInvoke(new Action(_003CloadCurrentStrokes_003Eb__79));
		}

		[CompilerGenerated]
		private void _003CloadCurrentStrokes_003Eb__79()
		{
			preparePenMemoAndSend(true);
		}

		[CompilerGenerated]
		private void _003CTextBlock_TargetUpdated_1_003Eb__7b()
		{
			Thread.Sleep(1500);
			base.Dispatcher.BeginInvoke(new Action(_003CTextBlock_TargetUpdated_1_003Eb__7c));
		}

		[CompilerGenerated]
		private void _003CTextBlock_TargetUpdated_1_003Eb__7c()
		{
			try
			{
				TakeAPicture(curPageIndex + 1);
				IsFirstCapture = false;
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}
	}
}
