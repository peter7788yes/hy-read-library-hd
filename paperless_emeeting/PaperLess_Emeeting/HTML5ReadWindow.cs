using BookFormatLoader;
using BookManagerModule;
using CefSharp;
using CefSharp.Wpf;
using DataAccessObject;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MultiLanquageModule;
using Newtonsoft.Json;
using PaperLess_Emeeting.App_Code;
using PaperLess_Emeeting.App_Code.MessageBox;
using PaperLess_Emeeting.App_Code.Socket;
using PaperLess_Emeeting.App_Code.Tools;
using PaperLess_Emeeting.App_Code.ViewModel;
using PaperLess_Emeeting.Properties;
using PaperLess_ViewModel;
using PaperlessSync.Broadcast.Service;
using PaperlessSync.Broadcast.Socket;
using SyncCenterModule;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Wpf_CustomCursor;

namespace PaperLess_Emeeting
{
	public class HTML5ReadWindow : Window, IEventManager, IRequestHandler, IMenuHandler, IComponentConnector
	{
		private delegate void setImgSourceCallback(string page, string animation);

		private delegate void setMsgToActionCallback(Dictionary<string, object> msgStrings);

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass1a
		{
			public Action act;

			public void _003CMyAnimation_003Eb__19(object sender2, EventArgs e2)
			{
				if (act != null)
				{
					act();
				}
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass2f
		{
			public Dictionary<string, object> msgStrings;

			public HTML5ReadWindow _003C_003E4__this;

			public void _003CparseJSonFromMessage_003Eb__2c()
			{
				_003C_003E4__this.Dispatcher.BeginInvoke(new Action(_003CparseJSonFromMessage_003Eb__2d));
			}

			public void _003CparseJSonFromMessage_003Eb__2d()
			{
				_003C_003E4__this.web_view.ExecuteScript("goToStep(0, 0)");
				string msgString = msgStrings["spline"].ToString();
				_003C_003E4__this.setMsgToAction(msgStrings);
				_003C_003E4__this.penMemoCanvas.Strokes.Clear();
				_003C_003E4__this.drawStrokeFromJson(msgString);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass38
		{
			public string pageIndex;

			public HTML5ReadWindow _003C_003E4__this;

			public void _003CsetMsgToActionDelegate_003Eb__33()
			{
				_003C_003E4__this.txtPage.Text = string.Format("{0} / {1}", pageIndex, _003C_003E4__this.totalPage.ToString());
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass3c
		{
			public Stroke targetStroke;

			public HTML5ReadWindow _003C_003E4__this;

			public void _003CpaintStrokeOnInkCanvas_003Eb__3a()
			{
				Thread.Sleep(5);
				_003C_003E4__this.Dispatcher.BeginInvoke(new Action(_003CpaintStrokeOnInkCanvas_003Eb__3b));
			}

			public void _003CpaintStrokeOnInkCanvas_003Eb__3b()
			{
				if (_003C_003E4__this.isSyncing && targetStroke != null)
				{
					_003C_003E4__this.penMemoCanvas.Strokes.Add(targetStroke.Clone());
					targetStroke = null;
				}
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass73
		{
			public int tempIndex;

			public HTML5ReadWindow _003C_003E4__this;

			public void _003CthumbNailListBox_SelectionChanged_003Eb__71()
			{
				Thread.Sleep(700);
				_003C_003E4__this.Dispatcher.BeginInvoke(new Action(_003CthumbNailListBox_SelectionChanged_003Eb__72));
			}

			public void _003CthumbNailListBox_SelectionChanged_003Eb__72()
			{
				_003C_003E4__this.txtPage.Text = string.Format("{0} / {1}", (tempIndex + 1).ToString(), _003C_003E4__this.totalPage.ToString());
			}
		}

		private const int PADDING_TOP = 0;

		private const int PADDING_BOTTOM = 0;

		private const int PADDING_LEFT = 0;

		private const int PADDING_RIGHT = 0;

		private const bool VERTICAL_WRITING_MODE = false;

		private const bool LEFT_TO_RIGHT = false;

		private HEJMetadata hejMetadata;

		private string pptPath;

		private SocketClient socket;

		private bool isSyncing = true;

		private bool isSyncOwner = true;

		private string appPath = Directory.GetCurrentDirectory();

		public int actual_webkit_column_width;

		public int actual_webkit_column_height;

		private string curHtmlDoc;

		private string jsquery;

		private string jsrangy_core;

		private string jsrangy_cssclassapplier;

		private string jsrangy_selectionsaverestore;

		private string jsrangy_serializer;

		private string jsEPubAddition;

		private string jsCustomSearch;

		private string jsBackCanvas;

		private string tail_JS;

		public static int curLeft;

		public int curPage;

		public int totalPage;

		public int fontSize = 16;

		public int perFontFize = 100;

		public bool scaleFlag;

		public string scaleRangyRange = "";

		private string basePath = "";

		private List<string> HTMLCode = new List<string>();

		private List<string> resultXMLList = new List<string>();

		private List<int> totalPagesInNodes = new List<int>();

		private bool processing;

		private bool bookOpened;

		private double thumbnailWidth;

		private double thumbnailHeight;

		private double thumbnailRatio;

		public DispatcherTimer initTimer;

		private Dictionary<int, BookMarkData> bookMarkDictionary;

		private Dictionary<int, NoteData> bookNoteDictionary;

		private Dictionary<int, List<StrokesData>> bookStrokesDictionary;

		private Dictionary<string, LastPageData> lastViewPage;

		private MoviePlayer mp;

		public Dictionary<string, BookVM> cbBooksData = new Dictionary<string, BookVM>();

		private bool isHTML5ReaderLoaded;

		private bool CanSentLine;

		private bool ifAskedJumpPage;

		private string CName = Environment.MachineName;

		private bool isFirstTimeLoaded;

		private string _managerId;

		private string _msg;

		private string _clientId;

		private Dictionary<int, string> dictCache = new Dictionary<int, string>();

		private MediaCanvasOpenedBy openedby;

		private int clickedPage;

		private Dictionary<MediaCanvasOpenedBy, StackPanel> RelativePanel = new Dictionary<MediaCanvasOpenedBy, StackPanel>();

		private PageMode viewStatusIndex = PageMode.SinglePage;

		private double originalCanvasWidth = 1.0;

		private double originalCanvasHeight = 1.0;

		private double fullScreenCanvasWidth = 1.0;

		private double fullScreenCanvasHeight = 1.0;

		private double baseStrokesCanvasWidth;

		private double baseStrokesCanvasHeight;

		private int i;

		private StylusPointCollection stylusPC;

		private Stroke strokeLine;

		private List<ThumbnailImageAndPage> singleThumbnailImageAndPageList;

		private bool BookMarkInLBIsClicked;

		private bool NoteButtonInLBIsClicked;

		private int thumbNailListBoxStatus;

		private bool thumbNailListBoxOpenedFullScreen;

		private double thumbnailListBoxHeight = 150.0;

		private bool isLockButtonLocked;

		private int firstIndex;

		internal Grid mainGrid;

		internal FlowDocumentReader FR;

		internal Canvas ToolBarSensor;

		internal Grid PenMemoToolBar;

		internal RadioButton BackToOriToolBar;

		internal Grid ToolBarInReader;

		internal TextBlock txtPage;

		internal RadioButton BackToBookShelfButton;

		internal StackPanel MediasStackPanel;

		internal ComboBox cbBooks;

		internal RadioButton SearchButton;

		internal RadioButton PenMemoButton;

		internal RadioButton BookMarkButton;

		internal RadioButton NoteButton;

		internal RadioButton ShareButton;

		internal ToggleButton syncButton;

		internal System.Windows.Controls.Image diableImg;

		internal Canvas toolbarSyncCanvas;

		internal Grid Grid_33;

		internal Canvas MediaTableCanvas;

		internal Border mediaListBorder;

		internal StackPanel mediaListPanel;

		internal Canvas stageCanvas;

		internal InkCanvas penMemoCanvas;

		internal Canvas PopupControlCanvas;

		internal Canvas HiddenControlCanvas;

		internal Canvas watermarkCanvas;

		internal TextBlock watermarkTextBlock;

		internal WebView web_view;

		internal RadioButton leftPageButton;

		internal RadioButton rightPageButton;

		internal System.Windows.Controls.Image statusBMK;

		internal System.Windows.Controls.Image statusMemo;

		internal System.Windows.Controls.Image StatusOnairOff;

		internal System.Windows.Controls.Image screenBroadcasting;

		internal System.Windows.Controls.Image screenReceiving;

		internal Canvas syncCanvas;

		internal RadioButton ShowListBoxButton;

		internal RadioButton ShowListBoxButtonNew;

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

		internal Grid webViewGrid;

		private bool _contentLoaded;

		[CompilerGenerated]
		private string _003CjsAnimation_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CbookId_003Ek__BackingField;

		[CompilerGenerated]
		private string _003Caccount_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CuserName_003Ek__BackingField;

		[CompilerGenerated]
		private string _003Cemail_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CmeetingId_003Ek__BackingField;

		[CompilerGenerated]
		private string _003Cwatermark_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CdbPath_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CwebServiceURL_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CsocketMessage_003Ek__BackingField;

		[CompilerGenerated]
		private BookManager _003CbookManager_003Ek__BackingField;

		[CompilerGenerated]
		private MultiLanquageManager _003ClangMng_003Ek__BackingField;

		[CompilerGenerated]
		private int _003CuserBookSno_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CsplineString_003Ek__BackingField;

		[CompilerGenerated]
		private int _003CcurPageIndex_003Ek__BackingField;

		[CompilerGenerated]
		private bool _003CisStrokeLine_003Ek__BackingField;

		[CompilerGenerated]
		private List<Stroke> _003CtempStrokes_003Ek__BackingField;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate15;

		[CompilerGenerated]
		private static Func<SearchRecord, int> CS_0024_003C_003E9__CachedAnonymousMethodDelegate1f;

		[CompilerGenerated]
		private static ConsoleMessageEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate27;

		[CompilerGenerated]
		private static Comparison<string> CS_0024_003C_003E9__CachedAnonymousMethodDelegate2b;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate42;

		[CompilerGenerated]
		private static Func<FileInfo, int> CS_0024_003C_003E9__CachedAnonymousMethodDelegate57;

		[CompilerGenerated]
		private static Func<ThumbnailImageAndPage, int> CS_0024_003C_003E9__CachedAnonymousMethodDelegate5d;

		[CompilerGenerated]
		private static Func<ThumbnailImageAndPage, int> CS_0024_003C_003E9__CachedAnonymousMethodDelegate62;

		[CompilerGenerated]
		private static Func<ThumbnailImageAndPage, int> CS_0024_003C_003E9__CachedAnonymousMethodDelegate64;

		[CompilerGenerated]
		private static EventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate70;

		public string jsAnimation
		{
			[CompilerGenerated]
			get
			{
				return _003CjsAnimation_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CjsAnimation_003Ek__BackingField = value;
			}
		}

		public string bookId
		{
			[CompilerGenerated]
			get
			{
				return _003CbookId_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CbookId_003Ek__BackingField = value;
			}
		}

		public string account
		{
			[CompilerGenerated]
			get
			{
				return _003Caccount_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003Caccount_003Ek__BackingField = value;
			}
		}

		public string userName
		{
			[CompilerGenerated]
			get
			{
				return _003CuserName_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CuserName_003Ek__BackingField = value;
			}
		}

		public string email
		{
			[CompilerGenerated]
			get
			{
				return _003Cemail_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003Cemail_003Ek__BackingField = value;
			}
		}

		public string meetingId
		{
			[CompilerGenerated]
			get
			{
				return _003CmeetingId_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CmeetingId_003Ek__BackingField = value;
			}
		}

		public string watermark
		{
			[CompilerGenerated]
			get
			{
				return _003Cwatermark_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003Cwatermark_003Ek__BackingField = value;
			}
		}

		public string dbPath
		{
			[CompilerGenerated]
			get
			{
				return _003CdbPath_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CdbPath_003Ek__BackingField = value;
			}
		}

		public string webServiceURL
		{
			[CompilerGenerated]
			get
			{
				return _003CwebServiceURL_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CwebServiceURL_003Ek__BackingField = value;
			}
		}

		public string socketMessage
		{
			[CompilerGenerated]
			get
			{
				return _003CsocketMessage_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CsocketMessage_003Ek__BackingField = value;
			}
		}

		public BookManager bookManager
		{
			[CompilerGenerated]
			get
			{
				return _003CbookManager_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CbookManager_003Ek__BackingField = value;
			}
		}

		public MultiLanquageManager langMng
		{
			[CompilerGenerated]
			get
			{
				return _003ClangMng_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003ClangMng_003Ek__BackingField = value;
			}
		}

		public int userBookSno
		{
			[CompilerGenerated]
			get
			{
				return _003CuserBookSno_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CuserBookSno_003Ek__BackingField = value;
			}
		}

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

		public string splineString
		{
			[CompilerGenerated]
			get
			{
				return _003CsplineString_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CsplineString_003Ek__BackingField = value;
			}
		}

		public int curPageIndex
		{
			[CompilerGenerated]
			get
			{
				return _003CcurPageIndex_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CcurPageIndex_003Ek__BackingField = value;
			}
		}

		public bool isStrokeLine
		{
			[CompilerGenerated]
			get
			{
				return _003CisStrokeLine_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CisStrokeLine_003Ek__BackingField = value;
			}
		}

		public List<Stroke> tempStrokes
		{
			[CompilerGenerated]
			get
			{
				return _003CtempStrokes_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CtempStrokes_003Ek__BackingField = value;
			}
		}

		public event Home_OpenBookFromReader_Function Home_OpenBookFromReader_Event;

		protected override void OnContentRendered(EventArgs e)
		{
			base.Topmost = true;
			base.Topmost = false;
			base.OnContentRendered(e);
		}

		public HTML5ReadWindow(Dictionary<string, BookVM> cbBooksData, Home_OpenBookFromReader_Function callback, string _pptPath, string _bookId, string _account, string _userName, string _email, string _meetingId, string _watermark, string _dbPath, bool _isSync, bool _isSyncOwner, string _webServiceURL, string _socketMessage = "", SocketClient _socket = null)
		{
			InitializeComponent();
			this.cbBooksData = cbBooksData;
			this.Home_OpenBookFromReader_Event = callback;
			socket = _socket;
			pptPath = _pptPath;
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
			bookManager = new BookManager(dbPath);
			QueryResult queryResult = null;
			try
			{
				string sqlCommand = "Select objectId from bookMarkDetail";
				queryResult = bookManager.sqlCommandQuery(sqlCommand);
				if (queryResult == null)
				{
					updateDataBase();
				}
				InitSyncCenter();
			}
			catch (Exception)
			{
			}
			queryResult = null;
			langMng = new MultiLanquageManager("zh-TW");
			alterAccountWhenSyncing(isSyncOwner);
			getBookPath();
			InitializeComponent();
			setWindowToFitScreen();
			base.Loaded += new RoutedEventHandler(HTML5Reader_Loaded);
			if (cbBooksData != null)
			{
				cbBooks.ItemsSource = cbBooksData;
				cbBooks.DisplayMemberPath = "Key";
				cbBooks.SelectedValuePath = "Value";
				cbBooks.SelectedIndex = 0;
				int num = 0;
				foreach (KeyValuePair<string, BookVM> cbBooksDatum in cbBooksData)
				{
					if (cbBooksDatum.Value.FileID.Equals(bookId))
					{
						cbBooks.SelectedIndex = num;
						break;
					}
					num++;
				}
				cbBooks.SelectionChanged += new SelectionChangedEventHandler(cbBooks_SelectionChanged);
			}
			else
			{
				cbBooks.Width = 0.0;
				cbBooks.Visibility = Visibility.Collapsed;
			}
			ChangeFlatUI(PaperLess_Emeeting.Properties.Settings.Default.IsFlatUIReader);
			AttachKey();
			ClearSyncOwnerPenLine();
		}

		private void AttachKey()
		{
			base.PreviewKeyDown += new KeyEventHandler(_003CAttachKey_003Eb__0);
		}

		private void ChangeFlatUI(bool IsFlatUI)
		{
			MouseButtonEventHandler mouseButtonEventHandler = null;
			MouseButtonEventHandler mouseButtonEventHandler2 = null;
			MouseButtonEventHandler mouseButtonEventHandler3 = null;
			MouseButtonEventHandler mouseButtonEventHandler4 = null;
			MouseEventHandler mouseEventHandler = null;
			RoutedEventHandler routedEventHandler = null;
			RoutedEventHandler routedEventHandler2 = null;
			RoutedEventHandler routedEventHandler3 = null;
			if (IsFlatUI)
			{
				ToolBarInReader.Visibility = Visibility.Collapsed;
				if (isSyncing)
				{
					StatusOnairOff.Visibility = Visibility.Collapsed;
					if (isSyncOwner)
					{
						screenBroadcasting.Visibility = Visibility.Visible;
						screenReceiving.Visibility = Visibility.Collapsed;
					}
					else
					{
						screenBroadcasting.Visibility = Visibility.Collapsed;
						screenReceiving.Visibility = Visibility.Visible;
					}
				}
				else
				{
					StatusOnairOff.Visibility = Visibility.Visible;
				}
				System.Windows.Controls.Image statusOnairOff = StatusOnairOff;
				if (mouseButtonEventHandler == null)
				{
					mouseButtonEventHandler = new MouseButtonEventHandler(_003CChangeFlatUI_003Eb__1);
				}
				statusOnairOff.MouseLeftButtonDown += mouseButtonEventHandler;
				System.Windows.Controls.Image image = screenBroadcasting;
				if (mouseButtonEventHandler2 == null)
				{
					mouseButtonEventHandler2 = new MouseButtonEventHandler(_003CChangeFlatUI_003Eb__4);
				}
				image.MouseLeftButtonDown += mouseButtonEventHandler2;
				System.Windows.Controls.Image image2 = screenReceiving;
				if (mouseButtonEventHandler3 == null)
				{
					mouseButtonEventHandler3 = new MouseButtonEventHandler(_003CChangeFlatUI_003Eb__7);
				}
				image2.MouseLeftButtonDown += mouseButtonEventHandler3;
				if (mouseButtonEventHandler4 == null)
				{
					mouseButtonEventHandler4 = new MouseButtonEventHandler(_003CChangeFlatUI_003Eb__a);
				}
				base.MouseRightButtonDown += mouseButtonEventHandler4;
				Panel.SetZIndex(MediaTableCanvas, 201);
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
					mouseEventHandler = new MouseEventHandler(_003CChangeFlatUI_003Eb__b);
				}
				textBox.MouseEnter += mouseEventHandler;
				TextBox textBox2 = txtKeyword;
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate15 == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate15 = new MouseEventHandler(_003CChangeFlatUI_003Eb__c);
				}
				textBox2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate15;
				txtKeyword.KeyUp += new KeyEventHandler(txtKeyword_KeyUp);
				txtKeyword.Focus();
				txtKeyword.PreviewKeyDown += new KeyEventHandler(txtKeyword_PreviewKeyDown);
				RadioButton radioButton = btnTxtKeywordClear;
				if (routedEventHandler == null)
				{
					routedEventHandler = new RoutedEventHandler(_003CChangeFlatUI_003Eb__d);
				}
				radioButton.Click += routedEventHandler;
				RadioButton radioButton2 = btnBold;
				if (routedEventHandler2 == null)
				{
					routedEventHandler2 = new RoutedEventHandler(_003CChangeFlatUI_003Eb__e);
				}
				radioButton2.Click += routedEventHandler2;
				RadioButton radioButton3 = btnPenColor;
				if (routedEventHandler3 == null)
				{
					routedEventHandler3 = new RoutedEventHandler(_003CChangeFlatUI_003Eb__f);
				}
				radioButton3.Click += routedEventHandler3;
			}
			else
			{
				statusBMK.Width = 0.0;
				statusBMK.Height = 0.0;
				statusMemo.Width = 0.0;
				statusMemo.Height = 0.0;
				StatusOnairOff.Width = 0.0;
				StatusOnairOff.Height = 0.0;
				thumnailCanvas.Background = ColorTool.HexColorToBrush("#212020");
			}
		}

		private void OpenClosePaint()
		{
			if (Panel.GetZIndex(penMemoCanvas) < 900)
			{
				MouseTool.ShowPen();
				Panel.SetZIndex(penMemoCanvas, 900);
				Panel.SetZIndex(stageCanvas, 2);
				Panel.SetZIndex(web_view, 850);
				web_view.IsHitTestVisible = false;
				penMemoCanvas.IsHitTestVisible = true;
				stageCanvas.IsHitTestVisible = false;
				penMemoCanvas.Background = Brushes.Transparent;
				penMemoCanvas.EditingMode = InkCanvasEditingMode.Ink;
				ChangeMainPenColor();
				Brush background = btnEraserGD.Background;
				if (background is SolidColorBrush)
				{
					string text = ((SolidColorBrush)background).Color.ToString();
					if (text.Equals("#FFF66F00"))
					{
						Mouse.OverrideCursor = CursorHelper.CreateCursor(new MyCursor());
						penMemoCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
					}
				}
				penMemoCanvas.Visibility = Visibility.Visible;
			}
			else
			{
				MouseTool.ShowArrow();
				Panel.SetZIndex(web_view, 1);
				Panel.SetZIndex(penMemoCanvas, 2);
				Panel.SetZIndex(stageCanvas, 3);
				web_view.IsHitTestVisible = true;
				penMemoCanvas.IsHitTestVisible = false;
				stageCanvas.IsHitTestVisible = false;
				penMemoCanvas.EditingMode = InkCanvasEditingMode.None;
				if (PopupControlCanvas.Visibility.Equals(Visibility.Visible))
				{
					PopupControlCanvas.Visibility = Visibility.Collapsed;
				}
				if (HiddenControlCanvas.Visibility.Equals(Visibility.Visible))
				{
					HiddenControlCanvas.Visibility = Visibility.Collapsed;
				}
			}
			penMemoCanvas.Focus();
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

		private void MyAnimation(DependencyObject sp, double ms, string property, double from, double to, Action act = null)
		{
			_003C_003Ec__DisplayClass1a _003C_003Ec__DisplayClass1a = new _003C_003Ec__DisplayClass1a();
			_003C_003Ec__DisplayClass1a.act = act;
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
			storyboard.Completed += new EventHandler(_003C_003Ec__DisplayClass1a._003CMyAnimation_003Eb__19);
			storyboard.Begin();
		}

		private void Grid_MouseEnterTransparent(object sender, MouseEventArgs e)
		{
			btnThin.Background = Brushes.Transparent;
			btnMedium.Background = Brushes.Transparent;
			btnLarge.Background = Brushes.Transparent;
			Grid grid = (Grid)sender;
			grid.Background = ColorTool.HexColorToBrush("#F66F00");
		}

		private void Grid_MouseLeaveTransparent(object sender, MouseEventArgs e)
		{
			Grid grid = (Grid)sender;
			grid.Background = Brushes.Transparent;
		}

		private void Grid_MouseEnter(object sender, MouseEventArgs e)
		{
			List<DependencyObject> DPs = new List<DependencyObject>();
			FindVisualChildTool.ByType<Grid>(PenColorSP, ref DPs);
			int num = 0;
			foreach (Grid item in DPs)
			{
				num++;
				if (!object.Equals(item.Background, Brushes.Black))
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
			grid.Background = Brushes.Black;
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
			Task.Factory.StartNew(new Action(_003CtxtKeyword_PreviewKeyDown_003Eb__1c));
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
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate1f == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate1f = new Func<SearchRecord, int>(_003CtxtKeyword_KeyUp_003Eb__1e);
				}
				List<int> list = Enumerable.ToList(Enumerable.Select(source, CS_0024_003C_003E9__CachedAnonymousMethodDelegate1f));
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

		private void InitSyncCenter()
		{
			Task.Factory.StartNew(new Action(_003CInitSyncCenter_003Eb__21));
		}

		private void cbBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

		public HTML5ReadWindow(string _pptPath, string _bookId, string _account, string _userName, string _email, string _meetingId, string _watermark, string _dbPath, bool _isSync, bool _isSyncOwner, string _webServiceURL, string _socketMessage = "", SocketClient _socket = null)
		{
			InitializeComponent();
			socket = _socket;
			pptPath = _pptPath;
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
			bookManager = new BookManager(dbPath);
			QueryResult queryResult = null;
			try
			{
				string sqlCommand = "Select objectId from bookMarkDetail";
				queryResult = bookManager.sqlCommandQuery(sqlCommand);
				if (queryResult == null)
				{
					updateDataBase();
				}
				InitSyncCenter();
			}
			catch (Exception)
			{
			}
			queryResult = null;
			langMng = new MultiLanquageManager("zh-TW");
			alterAccountWhenSyncing(isSyncOwner);
			getBookPath();
			InitializeComponent();
			setWindowToFitScreen();
			base.Loaded += new RoutedEventHandler(HTML5Reader_Loaded);
			ClearSyncOwnerPenLine();
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

		private void ClearSyncOwnerPenLine()
		{
			if (PaperLess_Emeeting.Properties.Settings.Default.IsClearSyncOwnerPenLine && isSyncOwner)
			{
				getBookPath();
				int userBookSno = this.userBookSno;
				Exec_Access_Sql(string.Format("DELETE FROM booklastPage WHERE userbook_sno = {0}", userBookSno));
				Exec_Access_Sql(string.Format("DELETE FROM bookmarkDetail WHERE userbook_sno = {0}", userBookSno));
				Exec_Access_Sql(string.Format("DELETE FROM booknoteDetail WHERE userbook_sno = {0}", userBookSno));
				Exec_Access_Sql(string.Format("DELETE FROM bookStrokesDetail WHERE userbook_sno = {0}", userBookSno));
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

		private void setWindowToFitScreen()
		{
			base.Width = SystemParameters.PrimaryScreenWidth;
			base.Height = SystemParameters.PrimaryScreenHeight - 40.0;
			base.Left = 0.0;
			base.Top = 0.0;
			base.WindowState = WindowState.Normal;
		}

		private void updateDataBase()
		{
			List<string> list = new List<string>();
			DateTime value = new DateTime(1970, 1, 1);
			long num = DateTime.Now.ToUniversalTime().Subtract(value).Ticks / 10000000;
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

		private void HTML5Reader_Loaded(object sender, RoutedEventArgs e)
		{
			base.Loaded -= new RoutedEventHandler(HTML5Reader_Loaded);
			web_view.MenuHandler = this;
			web_view.PropertyChanged += new PropertyChangedEventHandler(model_PropertyChanged);
			web_view.LoadCompleted += new LoadCompletedEventHandler(loadCompleted);
			web_view.RequestHandler = this;
			WebView webView = web_view;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate27 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate27 = new ConsoleMessageEventHandler(_003CHTML5Reader_Loaded_003Eb__22);
			}
			webView.ConsoleMessage += CS_0024_003C_003E9__CachedAnonymousMethodDelegate27;
			initJavaScript();
			tempStrokes = new List<Stroke>();
			initTimer = new DispatcherTimer();
			initTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
			initTimer.IsEnabled = true;
			initTimer.Tick += new EventHandler(loadEpubFromPath);
			initTimer.Start();
			watermarkTextBlock.Text = watermark;
			Grid_33.MouseLeftButtonDown += new MouseButtonEventHandler(_003CHTML5Reader_Loaded_003Eb__23);
			if (socket != null)
			{
				socket.AddEventManager(this);
			}
			else
			{
				syncButton.Visibility = Visibility.Collapsed;
				diableImg.Visibility = Visibility.Visible;
			}
			base.Closing += new CancelEventHandler(HTML5ReadWindow_Closing);
			isHTML5ReaderLoaded = true;
			base.ContentRendered += new EventHandler(_003CHTML5Reader_Loaded_003Eb__26);
		}

		private void InitPen()
		{
			Task.Factory.StartNew(new Action(_003CInitPen_003Eb__28));
		}

		private void HTML5ReadWindow_Closing(object sender, CancelEventArgs e)
		{
			string fileName = pptPath;
			double primaryScreenWidth = SystemParameters.PrimaryScreenWidth;
			double primaryScreenHeight = SystemParameters.PrimaryScreenHeight;
			float width = (float)penMemoCanvas.Width;
			float height = (float)penMemoCanvas.Height;
			noteButton_Click();
			string name = Assembly.GetExecutingAssembly().GetName().Name;
			if (PaperLess_Emeeting.Properties.Settings.Default.AssemblyName.Contains("TPI4F"))
			{
				string fullName = new FileInfo(fileName).Directory.FullName;
				string thumbsPath_Msize = System.IO.Path.Combine(fullName, "data", "Thumbnails");
				string thumbsPath_Lsize = System.IO.Path.Combine(fullName, "data", "Thumbnails\\Larger");
				Singleton_PDFFactory.SavePDF(true, fullName, totalPage, width, height, account, bookId, dbPath, thumbsPath_Msize, thumbsPath_Lsize);
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
			try
			{
				FileStream os = new FileStream(System.IO.Path.Combine(bookPath, "PDF.pdf"), FileMode.Create);
				PdfWriter instance = PdfWriter.GetInstance(document, os);
				string[] files = Directory.GetFiles(bookPath, "*.bmp");
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate2b == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate2b = new Comparison<string>(_003CSavePDF_003Eb__2a);
				}
				Array.Sort(files, CS_0024_003C_003E9__CachedAnonymousMethodDelegate2b);
				document.Open();
				int num5 = 0;
				string name = Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\..\\Fonts\\kaiu.ttf";
				BaseFont bf = BaseFont.CreateFont(name, "Identity-H", false);
				new Font(bf, 16f, 0);
				string text = System.IO.Path.Combine(bookPath, "data");
				string text2 = "";
				string text3 = System.IO.Path.Combine(bookPath, "data", "Thumbnails");
				string text4 = System.IO.Path.Combine(bookPath, "data", "Thumbnails\\Larger");
				text2 = ((!Directory.Exists(text4)) ? text3 : text4);
				string[] files2 = Directory.GetFiles(text, "*.png");
				string text5 = "";
				if (files2.Length > 0)
				{
					string name2 = new FileInfo(files2[0]).Name;
					text5 = name2.Split('_')[0];
				}
				for (int i = 1; i <= totalPage; i++)
				{
					try
					{
						System.IO.Path.Combine(text, text5 + "_" + i + ".pdf");
						string text6 = System.IO.Path.Combine(text2, text5 + "_" + i + ".png");
						string destFileName = System.IO.Path.Combine(bookPath, i + ".bmp");
						Directory.CreateDirectory(System.IO.Path.GetDirectoryName(text6));
						File.Exists(text6);
						File.Copy(text6, destFileName, true);
					}
					catch (Exception ex)
					{
						LogTool.Debug(ex);
					}
				}
				string[] array = files;
				foreach (string text7 in array)
				{
					num5++;
					FileInfo fileInfo = new FileInfo(text7);
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
						iTextSharp.text.Image instance2 = iTextSharp.text.Image.GetInstance(text7);
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
						queryResult = bookManager.sqlCommandQuery(sqlCommand);
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
							foreach (string text8 in array3)
							{
								num10++;
								string s = text8.Split(new char[3]
								{
									'{',
									',',
									'}'
								}, StringSplitOptions.RemoveEmptyEntries)[0];
								string s2 = text8.Split(new char[3]
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
				document.AddTitle("電子書");
				document.AddAuthor("Hyweb");
			}
			catch (Exception ex2)
			{
				LogTool.Debug(ex2);
			}
			finally
			{
				try
				{
					if (document.IsOpen())
					{
						document.Close();
					}
				}
				catch (Exception ex3)
				{
					LogTool.Debug(ex3);
				}
			}
		}

		public void RecordPage()
		{
			try
			{
				try
				{
					sendBroadCast("{\"cmd\":\"R.CB\"}");
					if (socket != null)
					{
						socket.RemoveEventManager(this);
					}
				}
				catch (Exception ex)
				{
					LogTool.Debug(ex);
				}
				base.Closing -= new CancelEventHandler(HTML5ReadWindow_Closing);
				saveLastReadingPage();
				deleteAllLocalPenmemoData();
			}
			catch (Exception ex2)
			{
				LogTool.Debug(ex2);
			}
		}

		private void saveLastReadingPage()
		{
			string machineName = Environment.MachineName;
			DateTime value = new DateTime(1970, 1, 1);
			long num = DateTime.Now.ToUniversalTime().Subtract(value).Ticks / 10000000;
			bool flag = false;
			LastPageData lastPageData = null;
			if (lastViewPage == null)
			{
				lastPageData = new LastPageData();
				lastPageData.index = curPageIndex + 1;
				lastPageData.updatetime = num;
				lastPageData.objectId = "";
				lastPageData.createtime = num;
				lastPageData.synctime = 0L;
				lastPageData.status = "0";
				lastPageData.device = machineName;
				flag = false;
			}
			else if (lastViewPage.ContainsKey(machineName))
			{
				lastPageData = lastViewPage[machineName];
				lastPageData.index = curPageIndex + 1;
				lastPageData.updatetime = num;
				flag = true;
			}
			else
			{
				lastPageData = new LastPageData();
				lastPageData.index = curPageIndex + 1;
				lastPageData.updatetime = num;
				lastPageData.objectId = "";
				lastPageData.createtime = num;
				lastPageData.synctime = 0L;
				lastPageData.status = "0";
				lastPageData.device = machineName;
				flag = false;
			}
			bookManager.saveLastviewPage(userBookSno, flag, lastPageData);
		}

		private void deleteAllLocalPenmemoData()
		{
			bookStrokesDictionary = new Dictionary<int, List<StrokesData>>();
		}

		private void loadCompleted(object sender, EventArgs e)
		{
			WebView webView = (WebView)sender;
			string address = webView.Address;
			if (!address.StartsWith("broadcast") && !processing && !web_view.IsLoading)
			{
				web_view.ExecuteScript("$(window).scroll(function(){android.selection.scrollTop(0); android.selection.scrollLeft(" + curLeft + "); return false;});");
				web_view.ExecuteScript("$('a').click(function() { return false; });");
			}
		}

		protected void model_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
		}

		private void initJavaScript()
		{
			try
			{
				fontSize = 20;
				perFontFize = 100;
				jsquery = File.ReadAllText(appPath + "\\jquery-2.0.3.js");
				jsrangy_core = File.ReadAllText(appPath + "\\rangy-1.3alpha.772\\rangy-1.3alpha.772\\rangy-core.js");
				jsrangy_cssclassapplier = File.ReadAllText(appPath + "\\rangy-1.3alpha.772\\rangy-1.3alpha.772\\rangy-cssclassapplier.js");
				jsrangy_selectionsaverestore = File.ReadAllText(appPath + "\\rangy-1.3alpha.772\\rangy-1.3alpha.772\\rangy-selectionsaverestore.js");
				jsrangy_serializer = File.ReadAllText(appPath + "\\rangy-1.3alpha.772\\rangy-1.3alpha.772\\rangy-serializer.js");
				jsEPubAddition = File.ReadAllText(appPath + "\\epubJS\\epubJS\\android.selection.js");
				jsCustomSearch = File.ReadAllText(appPath + "\\epubJS\\epubJS\\CustomSearch.js");
				jsBackCanvas = File.ReadAllText(appPath + "\\epubJS\\epubJS\\backcanvas.js");
				string path = pptPath;
				path = System.IO.Path.GetDirectoryName(path);
				string path2 = path + "\\data\\Thumbnails";
				if (Directory.Exists(path2))
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(path2);
					totalPage = Enumerable.Count(directoryInfo.GetFiles());
				}
				string text = path + "\\data\\Thumbnails\\Slide1.png";
				if (File.Exists(text))
				{
					new System.Windows.Controls.Image();
					BitmapImage bitmapImage = new BitmapImage(new Uri(text));
					thumbnailWidth = bitmapImage.PixelWidth;
					thumbnailHeight = bitmapImage.PixelHeight;
					thumbnailRatio = thumbnailWidth / thumbnailHeight;
					InitSmallImage();
				}
				basePath = path.Replace("\\", "/");
				basePath += "/";
				string[] files = Directory.GetFiles(path, "*.js", SearchOption.AllDirectories);
				for (int i = 0; i < files.Length; i++)
				{
					jsAnimation += File.ReadAllText(files[i]);
				}
				string[] files2 = Directory.GetFiles(path, "*.css", SearchOption.AllDirectories);
				for (int j = 0; j < files2.Length; j++)
				{
					jsAnimation += File.ReadAllText(files2[j]);
				}
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}

		private void setLayout()
		{
		}

		private void refreshDocument()
		{
			tail_JS = "$(document).ready(function(){$(document).mousedown(function(e){ if(e.which==1) { android.selection.startTouch(e.pageX, e.pageY);} });\r\n                $(document).keyup(function(e){ window.FORM.keyup(e.keyCode);   });\r\n                $(document).mouseup(function(e){ if(e.which==1) { android.selection.longTouch(e); } }); })";
			string html = "<script>" + jsquery + jsrangy_core + jsrangy_cssclassapplier + jsrangy_selectionsaverestore + jsrangy_serializer + jsEPubAddition + tail_JS + jsCustomSearch + jsBackCanvas + jsAnimation + "</script>" + curHtmlDoc + "<span id=\"mymarker\"></span>";
			processing = false;
			web_view.LoadHtml(html, "file:///");
		}

		private string fixHtmlDocument(string showHTML)
		{
			string text = "";
			string[] array = showHTML.Split(new string[1]
			{
				"\r\n"
			}, StringSplitOptions.RemoveEmptyEntries);
			string[] array2 = array;
			foreach (string text2 in array2)
			{
				string text3 = text2;
				if (text3.Contains("src="))
				{
					text3 = text3.Replace("../", "");
					text3 = text3.Replace("src=\"", "src=\"" + basePath);
				}
				text += text3;
			}
			string str = "<base href='" + basePath + "'/>";
			return text.Replace("<head>", "<head>" + str);
		}

		private void TextBlock_TargetUpdated_1(string page, string animation)
		{
			if (!socketMessage.Equals(""))
			{
				InitPen();
			}
			int num = 0;
			try
			{
				num = Convert.ToInt32(page);
				Convert.ToInt32(animation);
			}
			catch (Exception)
			{
				return;
			}
			if (!isFirstTimeLoaded)
			{
				initUserDataFromDB();
				if (isSyncing && socket != null)
				{
					syncButton.IsChecked = true;
					isSyncing = true;
					clearDataWhenSync();
					if (bookMarkDictionary.ContainsKey(curPageIndex))
					{
						BookMarkButton.IsChecked = ((bookMarkDictionary[curPageIndex].status == "0") ? true : false);
					}
					else
					{
						BookMarkButton.IsChecked = false;
					}
					TextBox textBox = FindVisualChildByName<TextBox>(MediaTableCanvas, "notePanel");
					if (textBox != null)
					{
						textBox.Text = bookNoteDictionary[curPageIndex].text;
					}
					if (bookNoteDictionary.ContainsKey(curPageIndex))
					{
						if (bookNoteDictionary[curPageIndex].text.Equals(""))
						{
							NoteButton.IsChecked = false;
						}
						else
						{
							NoteButton.IsChecked = true;
						}
					}
					else
					{
						NoteButton.IsChecked = false;
					}
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
					syncButton.IsChecked = false;
				}
				isFirstTimeLoaded = true;
				penMemoCanvas.StrokeCollected += new InkCanvasStrokeCollectedEventHandler(penMemoCanvasStrokeCollected);
				penMemoCanvas.StrokeErasing += new InkCanvasStrokeErasingEventHandler(penMemoCanvas_StrokeErasing);
				penMemoCanvas.StrokeErased += new RoutedEventHandler(penMemoCanvas_StrokeErased);
			}
			else
			{
				int num2 = num - 1;
				if (curPageIndex.Equals(num2))
				{
					return;
				}
				curPageIndex = num2;
				txtPage.Text = string.Format("{0} / {1}", (curPageIndex + 1).ToString(), totalPage.ToString());
			}
			double num3 = web_view.ContentsHeight;
			penMemoCanvas.Height = num3;
			penMemoCanvas.Width = num3 * thumbnailRatio;
			openedby = MediaCanvasOpenedBy.None;
			if (curPageIndex < 0 || (isSyncing && !isSyncOwner))
			{
				return;
			}
			if (stageCanvas.Children.Count > 0)
			{
				stageCanvas.Children.Clear();
			}
			if (penMemoCanvas.Strokes.Count > 0)
			{
				penMemoCanvas.Strokes.Clear();
			}
			if (splineString != "")
			{
				try
				{
					drawStrokeFromJson(splineString);
					splineString = "";
				}
				catch (Exception)
				{
					return;
				}
			}
			bookMarkDictionary = bookManager.getBookMarkDics(userBookSno);
			bookNoteDictionary = bookManager.getBookNoteDics(userBookSno);
			bookStrokesDictionary = bookManager.getStrokesDics(userBookSno);
			if (bookMarkDictionary.ContainsKey(curPageIndex))
			{
				if (bookMarkDictionary[curPageIndex].status == "0")
				{
					BookMarkButton.IsChecked = true;
				}
				else
				{
					BookMarkButton.IsChecked = false;
				}
			}
			else
			{
				BookMarkButton.IsChecked = false;
			}
			if (bookNoteDictionary.ContainsKey(curPageIndex))
			{
				if (bookNoteDictionary[curPageIndex].status == "0")
				{
					NoteButton.IsChecked = true;
				}
				else
				{
					NoteButton.IsChecked = false;
				}
			}
			else
			{
				NoteButton.IsChecked = false;
			}
			if (isSyncing && !CanSentLine)
			{
				loadCurrentStrokes(0);
				CanSentLine = true;
			}
			else
			{
				loadCurrentStrokes(curPageIndex);
			}
		}

		bool IRequestHandler.OnBeforeBrowse(IWebBrowser browser, IRequest request, NavigationType naigationvType, bool isRedirect)
		{
			if (request.Url.StartsWith("broadcast"))
			{
				string value = request.Url.Replace("broadcast://", "");
				try
				{
					Dictionary<string, string> dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(value);
					if (dictionary.ContainsKey("f"))
					{
						string text = dictionary["f"];
						if (text.Equals("currentStep"))
						{
							string msg = "{\"cmd\":\"R.PP\", \"page\":" + dictionary["page"] + ", \"animations\":" + dictionary["animations"] + "}";
							sendBroadCast(msg);
							if (isSyncOwner)
							{
								Thread.Sleep(500);
							}
							setImgSourceCallback method = new setImgSourceCallback(TextBlock_TargetUpdated_1);
							base.Dispatcher.Invoke(method, dictionary["page"], dictionary["animations"]);
						}
						else if (text.Equals("videoAction"))
						{
							string text2 = dictionary["source"];
							string text3 = basePath + text2;
							setImgSourceCallback method2 = new setImgSourceCallback(prepareVideoCmd);
							base.Dispatcher.Invoke(method2, text2, text3);
						}
					}
				}
				catch (Exception)
				{
				}
				return true;
			}
			return false;
		}

		bool IRequestHandler.OnBeforeResourceLoad(IWebBrowser browser, IRequestResponse requestResponse)
		{
			IRequest request = requestResponse.Request;
			return false;
		}

		void IRequestHandler.OnResourceResponse(IWebBrowser browser, string url, int status, string statusText, string mimeType, WebHeaderCollection headers)
		{
		}

		bool IRequestHandler.GetDownloadHandler(IWebBrowser browser, string mimeType, string fileName, long contentLength, ref IDownloadHandler handler)
		{
			return true;
		}

		bool IRequestHandler.GetAuthCredentials(IWebBrowser browser, bool isProxy, string host, int port, string realm, string scheme, ref string username, ref string password)
		{
			return false;
		}

		public void run()
		{
			parseJSonFromMessage(msg);
		}

		private void parseJSonFromMessage(string message)
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
			else
			{
				if (!text.Equals("R.init"))
				{
					return;
				}
				Action action = null;
				_003C_003Ec__DisplayClass2f _003C_003Ec__DisplayClass2f = new _003C_003Ec__DisplayClass2f();
				_003C_003Ec__DisplayClass2f._003C_003E4__this = this;
				_003C_003Ec__DisplayClass2f.msgStrings = new Dictionary<string, object>();
				using (Dictionary<string, object>.Enumerator enumerator2 = dictionary.GetEnumerator())
				{
					if (enumerator2.MoveNext())
					{
						string text3 = JsonConvert.SerializeObject(enumerator2.Current.Value);
						text3 = text3.Substring(1, text3.Length - 2).Replace("\\\"", "\"");
						_003C_003Ec__DisplayClass2f.msgStrings = JsonConvert.DeserializeObject<Dictionary<string, object>>(text3);
					}
				}
				if (!_003C_003Ec__DisplayClass2f.msgStrings.Count.Equals(0))
				{
					TaskFactory factory = Task.Factory;
					if (action == null)
					{
						action = new Action(_003C_003Ec__DisplayClass2f._003CparseJSonFromMessage_003Eb__2c);
					}
					factory.StartNew(action);
				}
			}
		}

		private void setMsgToAction(Dictionary<string, object> msgStrings)
		{
			setMsgToActionCallback method = new setMsgToActionCallback(setMsgToActionDelegate);
			base.Dispatcher.Invoke(method, msgStrings);
		}

		private void setMsgToActionDelegate(Dictionary<string, object> msgStrings)
		{
			Action action = null;
			if (!msgStrings.ContainsKey("cmd"))
			{
				string text = "";
				string text2 = "";
				foreach (KeyValuePair<string, object> msgString in msgStrings)
				{
					if (msgString.Value != null)
					{
						switch (msgString.Key)
						{
						case "pageIndex":
							if (msgStrings["pageIndex"] != null)
							{
								_003C_003Ec__DisplayClass38 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass38();
								_003C_003Ec__DisplayClass._003C_003E4__this = this;
								_003C_003Ec__DisplayClass.pageIndex = msgStrings["pageIndex"].ToString();
								base.Dispatcher.BeginInvoke(new Action(_003C_003Ec__DisplayClass._003CsetMsgToActionDelegate_003Eb__33));
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
						case "page":
							try
							{
								text = msgStrings["page"].ToString();
							}
							catch
							{
							}
							break;
						case "animations":
							try
							{
								text2 = msgStrings["animations"].ToString();
							}
							catch
							{
							}
							break;
						}
					}
				}
				if (text != "" && text2 != "")
				{
					try
					{
						web_view.ExecuteScript("goToStep(" + text + ", " + text2 + ")");
					}
					catch (Exception)
					{
					}
				}
			}
			else
			{
				string text3 = msgStrings["cmd"].ToString();
				string text4 = "";
				switch (text3)
				{
				case "R.PP.V":
				{
					msgStrings["bookId"].ToString();
					string text9 = msgStrings["path"].ToString();
					string text10 = msgStrings["action"].ToString();
					if (text10.Equals("start"))
					{
						string filePath = basePath + text9.Replace("test2", "");
						if (dictCache.Count <= 0)
						{
							dictCache[1] = "";
							TaskFactory factory = Task.Factory;
							if (action == null)
							{
								action = new Action(_003CsetMsgToActionDelegate_003Eb__34);
							}
							factory.StartNew(action);
							mp = new MoviePlayer(filePath, true, false);
							mp.ShowDialog();
						}
					}
					else if (text10.Equals("stop") && mp != null)
					{
						mp.Close();
						mp = null;
					}
					break;
				}
				case "R.PP":
				{
					string text7 = msgStrings["page"].ToString();
					string text8 = msgStrings["animations"].ToString();
					if (!text7.Equals((curPageIndex + 1).ToString()))
					{
						penMemoCanvas.Strokes.Clear();
					}
					try
					{
						web_view.ExecuteScript("goToStep(" + text7 + ", " + text8 + ")");
					}
					catch (Exception)
					{
					}
					break;
				}
				case "syncOwner":
				{
					isSyncing = true;
					string value = msgStrings["clientId"].ToString();
					penMemoCanvas.Strokes.Clear();
					if (clientId.Equals(value))
					{
						deleteAllLocalPenmemoData();
						alterAccountWhenSyncing(true);
						isSyncOwner = true;
						buttonStatusWhenSyncing(Visibility.Collapsed, Visibility.Collapsed);
						loadCurrentStrokes(curPageIndex);
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
				case "R.SB":
				{
					text4 = msgStrings["pageIndex"].ToString();
					int num2 = Convert.ToInt32(text4);
					string text11 = msgStrings["bookmark"].ToString();
					BookMarkData bookMarkData = new BookMarkData();
					bookMarkData.index = num2;
					bookMarkData.status = "0";
					BookMarkData bookMarkData2 = bookMarkData;
					text11.Equals("1");
					bookMarkData2.status = (text11.Equals("1") ? "0" : "1");
					if (bookMarkDictionary.ContainsKey(num2))
					{
						bookMarkDictionary[num2] = bookMarkData2;
					}
					break;
				}
				case "R.SA":
				{
					text4 = msgStrings["pageIndex"].ToString();
					string text5 = msgStrings["annotation"].ToString();
					TextBox textBox = FindVisualChildByName<TextBox>(MediaTableCanvas, "notePanel");
					text5 = (textBox.Text = text5.Replace("\\n", "\n").Replace("\\t", "\t"));
					int num = Convert.ToInt32(text4);
					NoteData noteData = new NoteData();
					noteData.bookid = bookId;
					noteData.text = text5;
					noteData.index = num;
					noteData.status = "0";
					NoteData value2 = noteData;
					bookNoteDictionary[num] = value2;
					if (textBox.Text.Equals(""))
					{
						NoteButton.IsChecked = false;
						TriggerBookMark_NoteButtonOrElse(NoteButton);
					}
					else
					{
						NoteButton.IsChecked = true;
						TriggerBookMark_NoteButtonOrElse(NoteButton);
					}
					break;
				}
				case "R.DPA":
					if (MediaTableCanvas.Visibility.Equals(Visibility.Visible))
					{
						doUpperRadioButtonClicked(MediaCanvasOpenedBy.NoteButton, NoteButton);
					}
					break;
				case "R.CB":
					Close();
					break;
				case "R.SS":
					penMemoCanvas.Strokes.Clear();
					text4 = msgStrings["pageIndex"].ToString();
					try
					{
						drawStrokeFromJson(msgStrings["spline"].ToString());
					}
					catch
					{
					}
					break;
				}
			}
			Task.Factory.StartNew(new Action(_003CsetMsgToActionDelegate_003Eb__35));
		}

		private void drawStrokeFromJson(string msgString)
		{
			try
			{
				double num = web_view.ContentsHeight;
				double currentInkcanvasWidth = num * thumbnailRatio;
				List<PemMemoInfos> list = JsonConvert.DeserializeObject<List<PemMemoInfos>>(msgString);
				for (int i = 0; i < list.Count; i++)
				{
					paintStrokeOnInkCanvas(list[i], currentInkcanvasWidth, num);
				}
			}
			catch
			{
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

		private void paintStrokeOnInkCanvas(PemMemoInfos strokeJson, double currentInkcanvasWidth, double currentInkcanvasHeight)
		{
			try
			{
				_003C_003Ec__DisplayClass3c _003C_003Ec__DisplayClass3c = new _003C_003Ec__DisplayClass3c();
				_003C_003Ec__DisplayClass3c._003C_003E4__this = this;
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
						Point point = default(Point);
						string text = array[i];
						text = text.TrimEnd(trimChars);
						text = text.TrimStart(trimChars);
						point = Point.Parse(text);
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
				if (stylusPointCollection.Count < 1)
				{
					StylusPoint item2 = default(StylusPoint);
					item2.X = 0.0;
					item2.Y = 0.0;
					stylusPointCollection.Add(item2);
				}
				_003C_003Ec__DisplayClass3c.targetStroke = new Stroke(stylusPointCollection);
				_003C_003Ec__DisplayClass3c.targetStroke.DrawingAttributes.FitToCurve = true;
				if (strokeAlpha != 1.0)
				{
					_003C_003Ec__DisplayClass3c.targetStroke.DrawingAttributes.IsHighlighter = true;
				}
				else
				{
					_003C_003Ec__DisplayClass3c.targetStroke.DrawingAttributes.IsHighlighter = false;
				}
				_003C_003Ec__DisplayClass3c.targetStroke.DrawingAttributes.Width = strokeWidth * 5.0;
				_003C_003Ec__DisplayClass3c.targetStroke.DrawingAttributes.Height = strokeWidth * 5.0;
				new ColorConverter();
				Color color = ConvertHexStringToColour(strokeColor);
				_003C_003Ec__DisplayClass3c.targetStroke.DrawingAttributes.Color = color;
				Task.Factory.StartNew(new Action(_003C_003Ec__DisplayClass3c._003CpaintStrokeOnInkCanvas_003Eb__3a));
			}
			catch (Exception ex2)
			{
				LogTool.Debug(ex2);
			}
		}

		private void sendBroadCast(string msg)
		{
			if (isSyncOwner && isSyncing && !string.IsNullOrEmpty(msg) && socket != null && socket != null && socket.GetIsConnected())
			{
				socket.broadcast(msg);
			}
		}

		public bool OnBeforeMenu(IWebBrowser browser)
		{
			return true;
		}

		private void loadEpubFromPath(object sender, EventArgs e)
		{
			initTimer.Tick -= new EventHandler(loadEpubFromPath);
			initTimer.Stop();
			initTimer.IsEnabled = false;
			initTimer = null;
			tail_JS = "$(document).ready(function(){$(document).mousedown(function(e){ if(e.which==1) { android.selection.startTouch(e.pageX, e.pageY);} });\r\n                //$(document).mouseup(function(e){ if(e.which==1) {android.selection.up(e.pageX, e.pageY); window.FORM.showMsg('shit'); return false;} });\r\n                $(document).mouseup(function(e){ if(e.which==1) { android.selection.longTouch(e); } }); })";
			processing = true;
			string html = "<script>" + jsquery + jsrangy_core + jsrangy_cssclassapplier + jsrangy_selectionsaverestore + jsrangy_serializer + jsEPubAddition + tail_JS + jsCustomSearch + jsBackCanvas + "</script>" + curHtmlDoc + "<span id=\"mymarker\"></span>";
			web_view.LoadHtml(html, "file:///");
			Thread.Sleep(100);
			string showHTML = "";
			using (FileStream fileStream = new FileStream(pptPath, FileMode.Open))
			{
				StreamReader streamReader = new StreamReader(fileStream);
				showHTML = streamReader.ReadToEnd();
				streamReader.Close();
				streamReader = null;
				fileStream.Close();
			}
			try
			{
				curHtmlDoc = fixHtmlDocument(showHTML);
				refreshDocument();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ex:" + ex.Message);
			}
		}

		private void syncButton_Click(object sender, RoutedEventArgs e)
		{
			if (socket == null)
			{
				Singleton_Socket.ReaderEvent = this;
				string meetingId = this.meetingId;
				string account = this.account;
				string userName = this.userName;
				bool? isChecked = syncButton.IsChecked;
				socket = Singleton_Socket.GetInstance(meetingId, account, userName, (isChecked.GetValueOrDefault() && isChecked.HasValue) ? true : false);
				if (socket == null)
				{
					syncButton.IsChecked = false;
					MessageBox.Show("無法連接廣播同步系統", "連線失敗", MessageBoxButton.OK);
					return;
				}
			}
			bool? isChecked2 = syncButton.IsChecked;
			if (isChecked2.GetValueOrDefault() && (isChecked2.HasValue ? true : false))
			{
				penMemoCanvas.Strokes.Clear();
				isSyncing = true;
				socket.syncSwitch(true);
				clearDataWhenSync();
				loadCurrentStrokes(curPageIndex);
				buttonStatusWhenSyncing(Visibility.Visible, Visibility.Visible);
			}
			else
			{
				penMemoCanvas.Strokes.Clear();
				isSyncing = false;
				isSyncOwner = false;
				socket.syncSwitch(false);
				clearDataWhenSync();
				alterAccountWhenSyncing(false);
				initUserDataFromDB();
				loadCurrentStrokes(curPageIndex);
				buttonStatusWhenSyncing(Visibility.Collapsed, Visibility.Collapsed);
			}
			if (bookMarkDictionary.ContainsKey(curPageIndex))
			{
				BookMarkButton.IsChecked = ((bookMarkDictionary[curPageIndex].status == "0") ? true : false);
				TriggerBookMark_NoteButtonOrElse(BookMarkButton);
			}
			else
			{
				BookMarkButton.IsChecked = false;
				TriggerBookMark_NoteButtonOrElse(BookMarkButton);
			}
			TextBox textBox = FindVisualChildByName<TextBox>(MediaTableCanvas, "notePanel");
			if (textBox != null)
			{
				textBox.Text = bookNoteDictionary[curPageIndex].text;
			}
			if (bookNoteDictionary.ContainsKey(curPageIndex))
			{
				if (bookNoteDictionary[curPageIndex].text.Equals(""))
				{
					NoteButton.IsChecked = false;
					TriggerBookMark_NoteButtonOrElse(NoteButton);
				}
				else
				{
					NoteButton.IsChecked = true;
					TriggerBookMark_NoteButtonOrElse(NoteButton);
				}
			}
			else
			{
				NoteButton.IsChecked = false;
				TriggerBookMark_NoteButtonOrElse(NoteButton);
			}
			switchNoteBookMarkShareButtonStatusWhenSyncing();
		}

		private void switchNoteBookMarkShareButtonStatusWhenSyncing()
		{
			if (isSyncing)
			{
				if (isSyncOwner)
				{
					cbBooks.Visibility = Visibility.Visible;
				}
				else
				{
					cbBooks.Visibility = Visibility.Collapsed;
				}
				BookMarkButton.Visibility = Visibility.Collapsed;
				NoteButton.Visibility = Visibility.Collapsed;
				ShareButton.Visibility = Visibility.Collapsed;
				BookMarkButtonInListBox.Visibility = Visibility.Collapsed;
				NoteButtonInListBox.Visibility = Visibility.Collapsed;
			}
			else
			{
				cbBooks.Visibility = Visibility.Visible;
				BookMarkButton.Visibility = Visibility.Visible;
				NoteButton.Visibility = Visibility.Visible;
				ShareButton.Visibility = Visibility.Visible;
				BookMarkButtonInListBox.Visibility = Visibility.Visible;
				NoteButtonInListBox.Visibility = Visibility.Visible;
			}
		}

		private void buttonStatusWhenSyncing(Visibility toolBarVisibility, Visibility syncCanvasVisibility)
		{
			toolbarSyncCanvas.Visibility = toolBarVisibility;
			syncCanvas.Visibility = syncCanvasVisibility;
			if (toolBarVisibility.Equals(Visibility.Visible) && syncCanvasVisibility.Equals(Visibility.Visible))
			{
				cbBooks.Opacity = 0.5;
				PenMemoButton.Opacity = 0.5;
				BookMarkButton.Opacity = 0.5;
				NoteButton.Opacity = 0.5;
				ShareButton.Opacity = 0.5;
				BackToBookShelfButton.Opacity = 0.5;
				ShowListBoxButton.Visibility = Visibility.Collapsed;
				if (PaperLess_Emeeting.Properties.Settings.Default.IsFlatUIReader)
				{
					if (CheckIsNowClick(MemoSP))
					{
						noteButton_Click();
					}
					NewUITop.Visibility = Visibility.Collapsed;
					NewUI.Visibility = Visibility.Collapsed;
					statusBMK.Width = 0.0;
					statusBMK.Height = 0.0;
					statusMemo.Width = 0.0;
					statusMemo.Height = 0.0;
				}
			}
			else
			{
				cbBooks.Opacity = 1.0;
				PenMemoButton.Opacity = 1.0;
				BookMarkButton.Opacity = 1.0;
				NoteButton.Opacity = 1.0;
				ShareButton.Opacity = 1.0;
				BackToBookShelfButton.Opacity = 1.0;
				ShowListBoxButton.Visibility = Visibility.Visible;
				if (PaperLess_Emeeting.Properties.Settings.Default.IsFlatUIReader)
				{
					NewUITop.Visibility = Visibility.Visible;
					NewUI.Visibility = Visibility.Visible;
					statusBMK.Width = 56.0;
					statusBMK.Height = 56.0;
					statusMemo.Width = 56.0;
					statusMemo.Height = 56.0;
				}
			}
			if (isSyncOwner)
			{
				StatusOnairOff.Visibility = Visibility.Collapsed;
				screenBroadcasting.Visibility = Visibility.Visible;
				screenReceiving.Visibility = Visibility.Collapsed;
			}
			else if (isSyncing)
			{
				StatusOnairOff.Visibility = Visibility.Collapsed;
				screenBroadcasting.Visibility = Visibility.Collapsed;
				screenReceiving.Visibility = Visibility.Visible;
			}
			else
			{
				StatusOnairOff.Visibility = Visibility.Visible;
				screenBroadcasting.Visibility = Visibility.Collapsed;
				screenReceiving.Visibility = Visibility.Collapsed;
			}
		}

		private void initUserDataFromDB()
		{
			string machineName = Environment.MachineName;
			lastViewPage = bookManager.getLastViewPageObj(userBookSno);
			if (lastViewPage.ContainsKey(machineName) && lastViewPage[machineName].index > 0)
			{
				if (!lastViewPage[machineName].index.Equals((curPageIndex + 1).ToString()))
				{
					penMemoCanvas.Strokes.Clear();
				}
				try
				{
					string text = "0";
					if (isSyncing && !CanSentLine)
					{
						txtPage.Text = string.Format("{0} / {1}", "1", totalPage.ToString());
					}
					else
					{
						web_view.ExecuteScript("goToStep(" + lastViewPage[machineName].index + ", " + text + ")");
						txtPage.Text = string.Format("{0} / {1}", lastViewPage[machineName].index.ToString(), totalPage.ToString());
					}
				}
				catch (Exception)
				{
				}
			}
			if (!isSyncing)
			{
				bookStrokesDictionary = bookManager.getStrokesDics(userBookSno);
				bookMarkDictionary = bookManager.getBookMarkDics(userBookSno);
				bookNoteDictionary = bookManager.getBookNoteDics(userBookSno);
			}
		}

		private void clearDataWhenSync()
		{
			bookMarkDictionary = new Dictionary<int, BookMarkData>();
			bookNoteDictionary = new Dictionary<int, NoteData>();
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

		private void loadCurrentStrokes(int curIndex)
		{
			if (isSyncing && !isSyncOwner)
			{
				return;
			}
			bookStrokesDictionary = bookManager.getStrokesDics(userBookSno);
			if (bookStrokesDictionary.ContainsKey(curIndex))
			{
				List<StrokesData> list = bookStrokesDictionary[curIndex];
				int count = list.Count;
				for (int i = 0; i < count; i++)
				{
					if (list[i].status == "0")
					{
						paintStrokeOnInkCanvas(list[i], penMemoCanvas.Width, penMemoCanvas.Height, 0.0, 0.0);
					}
				}
			}
			preparePenMemoAndSend(true);
		}

		private void preparePenMemoAndSend(bool Division3 = true)
		{
			if (!isSyncOwner || !isSyncing)
			{
				return;
			}
			int count = penMemoCanvas.Strokes.Count;
			List<PemMemoInfos> list = new List<PemMemoInfos>();
			for (int i = 0; i < count; i++)
			{
				int count2 = penMemoCanvas.Strokes[i].StylusPoints.Count;
				DrawingAttributes drawingAttributes = penMemoCanvas.Strokes[i].DrawingAttributes;
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
				pemMemoInfos.canvasHeight = (int)penMemoCanvas.Height;
				pemMemoInfos.canvasWidth = (int)penMemoCanvas.Width;
				pemMemoInfos.strokeAlpha = (drawingAttributes.IsHighlighter ? 0.5 : 1.0);
				string text = drawingAttributes.Color.ToString();
				text = (pemMemoInfos.strokeColor = text.Remove(1, 2));
				string text3 = "";
				for (int j = 0; j < count2; j++)
				{
					StylusPoint stylusPoint = penMemoCanvas.Strokes[i].StylusPoints[j];
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

		private void doUpperRadioButtonClicked(MediaCanvasOpenedBy whichButton, object sender)
		{
			if (openedby.Equals(whichButton))
			{
				if (MediaTableCanvas.Visibility.Equals(Visibility.Visible))
				{
					if (!whichButton.Equals(MediaCanvasOpenedBy.NoteButton))
					{
						((RadioButton)sender).IsChecked = false;
					}
					else if (whichButton.Equals(MediaCanvasOpenedBy.NoteButton))
					{
						TextBox textBox = FindVisualChildByName<TextBox>(MediaTableCanvas, "notePanel");
						if (textBox != null)
						{
							int curPageIndex = this.curPageIndex;
							setNotesInMem(textBox.Text, curPageIndex);
							if (textBox.Text.Equals(""))
							{
								NoteButton.IsChecked = false;
								TriggerBookMark_NoteButtonOrElse(NoteButton);
							}
							else
							{
								NoteButton.IsChecked = true;
								TriggerBookMark_NoteButtonOrElse(NoteButton);
							}
						}
					}
					MediaTableCanvas.Visibility = Visibility.Collapsed;
					sendBroadCast("{\"cmd\":\"R.DPA\"}");
				}
				else
				{
					sendBroadCast("{\"cmd\":\"R.AA\"}");
					MediaTableCanvas.Visibility = Visibility.Visible;
					if (whichButton.Equals(MediaCanvasOpenedBy.NoteButton))
					{
						TextBox textBox2 = FindVisualChildByName<TextBox>(MediaTableCanvas, "notePanel");
						if (textBox2 != null)
						{
							int curPageIndex2 = this.curPageIndex;
							setNotesInMem(textBox2.Text, curPageIndex2);
							textBox2.Text = (bookNoteDictionary.ContainsKey(curPageIndex2) ? bookNoteDictionary[curPageIndex2].text : "");
							if (textBox2.Text.Equals(""))
							{
								NoteButton.IsChecked = false;
								TriggerBookMark_NoteButtonOrElse(NoteButton);
							}
							else
							{
								NoteButton.IsChecked = true;
								TriggerBookMark_NoteButtonOrElse(NoteButton);
								sendBroadCast("{\"annotation\":\"" + textBox2.Text + "\",\"pageIndex\":" + curPageIndex2 + ",\"cmd\":\"R.SA\"}");
							}
						}
					}
				}
				if (openedby == MediaCanvasOpenedBy.NoteButton)
				{
					TextBox textBox3 = FindVisualChildByName<TextBox>(mediaListPanel, "notePanel");
					textBox3.Select(textBox3.Text.Length, 0);
					textBox3.Focus();
					return;
				}
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
				NoteButton.IsChecked = false;
				TriggerBookMark_NoteButtonOrElse(NoteButton);
			}
			clickedPage = this.curPageIndex;
			mediaListPanel.Children.Clear();
			if (RelativePanel.ContainsKey(whichButton) && !whichButton.Equals(MediaCanvasOpenedBy.NoteButton))
			{
				mediaListPanel.Children.Add(RelativePanel[whichButton]);
			}
			else
			{
				StackPanel value = new StackPanel();
				double width = mediaListPanel.Width;
				switch (whichButton)
				{
				case MediaCanvasOpenedBy.SearchButton:
					value = getSearchPanelSet(width, "");
					break;
				case MediaCanvasOpenedBy.NoteButton:
					value = getNotesAndMakeNote();
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
				mediaListPanel.Children.Clear();
				mediaListPanel.Children.Add(RelativePanel[whichButton]);
			}
			MediaTableCanvas.Visibility = Visibility.Visible;
			openedby = whichButton;
			resetFocusBackToReader();
			if (openedby == MediaCanvasOpenedBy.NoteButton)
			{
				TextBox textBox4 = FindVisualChildByName<TextBox>(mediaListPanel, "notePanel");
				textBox4.Select(textBox4.Text.Length, 0);
				textBox4.Focus();
			}
		}

		private StackPanel getNotesAndMakeNote()
		{
			double width = mediaListPanel.Width;
			double height = mediaListPanel.Height;
			double width2 = 100.0;
			double num = 20.0;
			string text = bookNoteDictionary.ContainsKey(curPageIndex) ? bookNoteDictionary[curPageIndex].text : "";
			StackPanel stackPanel = new StackPanel();
			TextBox textBox = new TextBox();
			textBox.Name = "notePanel";
			textBox.TextWrapping = TextWrapping.Wrap;
			textBox.AcceptsReturn = true;
			textBox.BorderBrush = Brushes.White;
			textBox.Margin = new Thickness(2.0);
			textBox.Width = width - 4.0;
			textBox.Height = height - num - 8.0;
			textBox.Text = text;
			textBox.FontSize = 16.0;
			TextBox textBox2 = textBox;
			textBox2.KeyDown += new KeyEventHandler(noteTB_KeyDown);
			textBox2.TextChanged += new TextChangedEventHandler(noteTB_TextChanged);
			RadioButton radioButton = new RadioButton();
			TextBlock textBlock = new TextBlock();
			textBlock.VerticalAlignment = VerticalAlignment.Center;
			textBlock.HorizontalAlignment = HorizontalAlignment.Center;
			textBlock.Foreground = Brushes.White;
			textBlock.Text = langMng.getLangString("save");
			radioButton.Content = textBlock;
			radioButton.Background = Brushes.Black;
			radioButton.Margin = new Thickness(2.0);
			radioButton.Width = width2;
			radioButton.Height = num;
			RadioButton radioButton2 = radioButton;
			radioButton2.Click += new RoutedEventHandler(noteButton_Click);
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate42 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate42 = new MouseEventHandler(_003CgetNotesAndMakeNote_003Eb__41);
			}
			textBox2.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate42;
			stackPanel.Children.Add(textBox2);
			stackPanel.Children.Add(radioButton2);
			stackPanel.Orientation = Orientation.Vertical;
			return stackPanel;
		}

		private StackPanel getSearchPanelSet(double panelWidth, string txtInSearchBar)
		{
			StackPanel stackPanel = new StackPanel();
			stackPanel.Name = "spParent";
			RadioButton radioButton = new RadioButton();
			TextBlock textBlock = new TextBlock();
			textBlock.VerticalAlignment = VerticalAlignment.Center;
			textBlock.HorizontalAlignment = HorizontalAlignment.Center;
			textBlock.Foreground = Brushes.White;
			textBlock.Text = langMng.getLangString("search");
			radioButton.Content = textBlock;
			radioButton.Background = Brushes.Black;
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
			stackPanel.Background = Brushes.LightGray;
			return stackPanel;
		}

		private StackPanel GetMediaListPanelInReader()
		{
			return FindVisualChildByName<StackPanel>(FR, "mediaListPanel");
		}

		private ListBox hyftdSearch(string keyWord)
		{
			string directoryName = System.IO.Path.GetDirectoryName(pptPath);
			string text = directoryName + "\\data\\fulltext";
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
				if (!text3.Equals(""))
				{
					string text4 = files[i].Replace(text + "\\", "");
					text4 = text4.Replace(text4.Substring(text4.LastIndexOf('.')), "");
					int num = 1;
					try
					{
						num = int.Parse(text4.Split('_')[1]);
					}
					catch (Exception ex)
					{
						LogTool.Debug(ex);
					}
					text4 = string.Format("Slide{0}.png", num);
					SearchRecord searchRecord = new SearchRecord(num.ToString(), text2, num + 1);
					string str = directoryName;
					searchRecord.imagePath = str + "\\data\\Thumbnails\\" + text4;
					list.Add(searchRecord);
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

		private void searchTB_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				TextBox textBox = (TextBox)sender;
				string text = textBox.Text;
				double width = mediaListPanel.Width;
				mediaListPanel.Children.Clear();
				StackPanel searchPanelSet = getSearchPanelSet(width, text);
				ListBox element = hyftdSearch(text);
				StackPanel stackPanel = new StackPanel();
				stackPanel.Children.Add(searchPanelSet);
				stackPanel.Children.Add(element);
				RelativePanel[MediaCanvasOpenedBy.SearchButton] = stackPanel;
				mediaListPanel.Children.Add(stackPanel);
			}
		}

		private void searchButton_Click(object sender, RoutedEventArgs e)
		{
			TextBox textBox = FindVisualChildByName<TextBox>(mediaListPanel, "searchBar");
			string text = textBox.Text;
			double width = mediaListPanel.Width;
			mediaListPanel.Children.Clear();
			StackPanel searchPanelSet = getSearchPanelSet(width, text);
			ListBox element = hyftdSearch(text);
			StackPanel stackPanel = new StackPanel();
			stackPanel.Children.Add(searchPanelSet);
			stackPanel.Children.Add(element);
			RelativePanel[MediaCanvasOpenedBy.SearchButton] = stackPanel;
			mediaListPanel.Children.Add(stackPanel);
		}

		private void lb_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Action action = null;
			ListBox listBox = (ListBox)sender;
			if (listBox.SelectedIndex != -1)
			{
				int targetPage = ((SearchRecord)e.AddedItems[0]).targetPage;
				int num = targetPage - 1;
				if (!num.Equals(-1))
				{
					string text = num.ToString();
					string text2 = "0";
					web_view.ExecuteScript("goToStep(" + text + ", " + text2 + ")");
				}
				listBox.SelectedIndex = -1;
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003Clb_SelectionChanged_003Eb__46);
				}
				factory.StartNew(action);
			}
		}

		private void noteTB_TextChanged(object sender, TextChangedEventArgs e)
		{
			TextBox textBox = FindVisualChildByName<TextBox>(MediaTableCanvas, "notePanel");
			int curPageIndex = this.curPageIndex;
			sendBroadCast("{\"annotation\":\"" + textBox.Text + "\",\"pageIndex\":" + curPageIndex + ",\"cmd\":\"R.SA\"}");
		}

		private void noteTB_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				TextBox textBox = (TextBox)sender;
				textBox.Text += "\r\n";
				int curPageIndex = this.curPageIndex;
				sendBroadCast("{\"annotation\":\"" + textBox.Text + "\",\"pageIndex\":" + curPageIndex + ",\"cmd\":\"R.SA\"}");
			}
		}

		private void btnPen_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
			Panel.SetZIndex(penMemoCanvas, 900);
			Panel.SetZIndex(stageCanvas, 2);
			Panel.SetZIndex(web_view, 850);
			web_view.IsHitTestVisible = false;
			penMemoCanvas.IsHitTestVisible = true;
			stageCanvas.IsHitTestVisible = false;
			penMemoCanvas.Background = Brushes.Transparent;
			penMemoCanvas.EditingMode = InkCanvasEditingMode.Ink;
			penMemoCanvas.Visibility = Visibility.Visible;
			penMemoCanvas.Focus();
			if (HiddenControlCanvas.Visibility.Equals(Visibility.Collapsed))
			{
				HiddenControlCanvas.Visibility = Visibility.Visible;
			}
			Keyboard.ClearFocus();
			if (isStrokeLine)
			{
				strokeLineEventHandler();
			}
			else
			{
				strokeCurveEventHandler();
			}
		}

		private void noteButton_Click(object sender, RoutedEventArgs e)
		{
			noteButton_Click();
		}

		private void noteButton_Click()
		{
			try
			{
				TextBox textBox = FindVisualChildByName<TextBox>(MediaTableCanvas, "notePanel");
				int curPageIndex = this.curPageIndex;
				setNotesInMem(textBox.Text, curPageIndex);
				bookManager.saveNoteData(userBookSno, curPageIndex.ToString(), textBox.Text);
				if (textBox.Text.Equals(""))
				{
					NoteButton.IsChecked = false;
					TriggerBookMark_NoteButtonOrElse(NoteButton);
				}
				else
				{
					NoteButton.IsChecked = true;
					TriggerBookMark_NoteButtonOrElse(NoteButton);
				}
				MediaTableCanvas.Visibility = Visibility.Collapsed;
				sendBroadCast("{\"annotation\":\"" + textBox.Text + "\",\"pageIndex\":" + curPageIndex + ",\"cmd\":\"R.SA\"}");
				sendBroadCast("{\"cmd\":\"R.DPA\"}");
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
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

		private void PenMemoButton_Checked(object sender, RoutedEventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			openedby = MediaCanvasOpenedBy.PenMemo;
			StrokeToolPanelHorizontal strokeToolPanelHorizontal = new StrokeToolPanelHorizontal();
			strokeToolPanelHorizontal.langMng = langMng;
			if (PenMemoToolBar.Visibility.Equals(Visibility.Collapsed))
			{
				ToolBarInReader.Visibility = Visibility.Collapsed;
				PenMemoToolBar.Visibility = Visibility.Visible;
				radioButton.IsChecked = false;
				strokeToolPanelHorizontal.determineDrawAtt(penMemoCanvas.DefaultDrawingAttributes, isStrokeLine);
				Panel.SetZIndex(penMemoCanvas, 900);
				Panel.SetZIndex(stageCanvas, 2);
				Panel.SetZIndex(web_view, 850);
				web_view.IsHitTestVisible = false;
				penMemoCanvas.IsHitTestVisible = true;
				stageCanvas.IsHitTestVisible = false;
				penMemoCanvas.Background = Brushes.Transparent;
				penMemoCanvas.EditingMode = InkCanvasEditingMode.Ink;
				penMemoCanvas.Visibility = Visibility.Visible;
				strokeToolPanelHorizontal.HorizontalAlignment = HorizontalAlignment.Right;
				PenMemoToolBar.Children.Add(strokeToolPanelHorizontal);
				alterPenmemoAnimation(strokeToolPanelHorizontal, 0.0, strokeToolPanelHorizontal.Width);
				strokeToolPanelHorizontal.strokeChange += new StrokeChangeEvent(strokeChaneEventHandler);
				strokeToolPanelHorizontal.strokeUndo += new StrokeUndoEvent(strokeUndoEventHandler);
				strokeToolPanelHorizontal.strokeDelAll += new StrokeDeleteAllEvent(strokeDelAllEventHandler);
				strokeToolPanelHorizontal.strokeRedo += new StrokeRedoEvent(strokeRedoEventHandler);
				strokeToolPanelHorizontal.strokeDel += new StrokeDeleteEvent(strokDelEventHandler);
				strokeToolPanelHorizontal.showPenToolPanel += new showPenToolPanelEvent(showPenToolPanelEventHandler);
				strokeToolPanelHorizontal.strokeErase += new StrokeEraseEvent(strokeEraseEventHandler);
				strokeToolPanelHorizontal.strokeCurve += new StrokeCurveEvent(strokeCurveEventHandler);
				strokeToolPanelHorizontal.strokeLine += new StrokeLineEvent(strokeLineEventHandler);
				penMemoCanvas.Focus();
				if (HiddenControlCanvas.Visibility.Equals(Visibility.Collapsed))
				{
					HiddenControlCanvas.Visibility = Visibility.Visible;
				}
				Keyboard.ClearFocus();
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
				Panel.SetZIndex(web_view, 1);
				Panel.SetZIndex(penMemoCanvas, 2);
				Panel.SetZIndex(stageCanvas, 3);
				web_view.IsHitTestVisible = true;
				penMemoCanvas.IsHitTestVisible = false;
				stageCanvas.IsHitTestVisible = false;
				((RadioButton)sender).IsChecked = false;
				penMemoCanvas.EditingMode = InkCanvasEditingMode.None;
				alterPenmemoAnimation(strokeToolPanelHorizontal, strokeToolPanelHorizontal.Width, 0.0);
				PenMemoToolBar.Children.Remove(PenMemoToolBar.Children[PenMemoToolBar.Children.Count - 1]);
				if (PopupControlCanvas.Visibility.Equals(Visibility.Visible))
				{
					PopupControlCanvas.Visibility = Visibility.Collapsed;
				}
				if (HiddenControlCanvas.Visibility.Equals(Visibility.Visible))
				{
					HiddenControlCanvas.Visibility = Visibility.Collapsed;
				}
				PenMemoToolBar.Visibility = Visibility.Collapsed;
				ToolBarInReader.Visibility = Visibility.Visible;
			}
		}

		private void paintStrokeOnInkCanvas(StrokesData strokeJson, double currentInkcanvasWidth, double currentInkcanvasHeight, double offsetX, double offsetY)
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
				Point point = default(Point);
				string text = array[i];
				text = text.TrimEnd(trimChars);
				text = text.TrimStart(trimChars);
				point = Point.Parse(text);
				StylusPoint item = default(StylusPoint);
				item.X = point.X * num5;
				item.Y = point.Y * num6;
				stylusPointCollection.Add(item);
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
			new ColorConverter();
			Color color2 = ConvertHexStringToColour(color);
			stroke.DrawingAttributes.Color = color2;
			Matrix transformMatrix = new Matrix(1.0, 0.0, 0.0, 1.0, offsetX, 0.0);
			if (stroke != null)
			{
				stroke.Transform(transformMatrix, false);
				penMemoCanvas.Strokes.Add(stroke.Clone());
				stroke = null;
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
					if (compareStrokeInDB(stroke, curPageStrokes[num], inkCanvas.ActualWidth, inkCanvas.ActualHeight))
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
				strokesData.canvasheight = (float)penMemoCanvas.ActualHeight;
				strokesData.canvaswidth = (float)penMemoCanvas.ActualWidth;
				strokesData.color = text;
				strokesData.createtime = num + this.i;
				strokesData.index = curPageIndex;
				strokesData.points = text2;
				strokesData.status = "0";
				strokesData.synctime = 0L;
				strokesData.updatetime = num + this.i;
				strokesData.userid = account;
				strokesData.width = (float)drawingAttributes.Height;
				bookManager.saveStrokesData(userBookSno, false, strokesData);
			}
		}

		public void strokeChaneEventHandler(DrawingAttributes d)
		{
			penMemoCanvas.DefaultDrawingAttributes = d;
		}

		public void strokeUndoEventHandler()
		{
			if (penMemoCanvas.Strokes.Count > 0)
			{
				tempStrokes.Add(penMemoCanvas.Strokes[penMemoCanvas.Strokes.Count - 1]);
				penMemoCanvas.Strokes.RemoveAt(penMemoCanvas.Strokes.Count - 1);
			}
		}

		public void strokeRedoEventHandler()
		{
			while (tempStrokes.Count > 0)
			{
				penMemoCanvas.Strokes.Add(tempStrokes[tempStrokes.Count - 1]);
				tempStrokes.RemoveAt(tempStrokes.Count - 1);
			}
		}

		public void strokeEraseEventHandler()
		{
			penMemoCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
		}

		public void strokeLineEventHandler()
		{
			penMemoCanvas.EditingMode = InkCanvasEditingMode.None;
			penMemoCanvas.MouseLeftButtonDown += new MouseButtonEventHandler(inkCanvas1_MouseDown);
			penMemoCanvas.MouseUp += new MouseButtonEventHandler(inkCanvas1_MouseUp);
			penMemoCanvas.MouseMove += new MouseEventHandler(inkCanvas1_MouseMove);
			isStrokeLine = true;
		}

		public void strokeCurveEventHandler()
		{
			penMemoCanvas.MouseDown -= new MouseButtonEventHandler(inkCanvas1_MouseDown);
			penMemoCanvas.MouseUp -= new MouseButtonEventHandler(inkCanvas1_MouseUp);
			penMemoCanvas.MouseMove -= new MouseEventHandler(inkCanvas1_MouseMove);
			penMemoCanvas.EditingMode = InkCanvasEditingMode.Ink;
			isStrokeLine = false;
		}

		private void inkCanvas1_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (penMemoCanvas.EditingMode == InkCanvasEditingMode.None)
			{
				stylusPC = new StylusPointCollection();
				Point position = e.GetPosition(penMemoCanvas);
				stylusPC.Add(new StylusPoint(position.X, position.Y));
			}
		}

		private void inkCanvas1_MouseMove(object sender, MouseEventArgs e)
		{
		}

		private void inkCanvas1_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (penMemoCanvas.EditingMode == InkCanvasEditingMode.None && stylusPC != null)
			{
				Point position = e.GetPosition(penMemoCanvas);
				stylusPC.Add(new StylusPoint(position.X, position.Y));
				strokeLine = new Stroke(stylusPC, penMemoCanvas.DefaultDrawingAttributes);
				penMemoCanvas.Strokes.Add(strokeLine.Clone());
				saveStrokeToDB(strokeLine.Clone());
				stylusPC = null;
				strokeLine = null;
			}
		}

		public void strokDelEventHandler()
		{
			Button button = FindVisualChildByName<Button>(mediaListPanel, "delClickButton");
			if (penMemoCanvas.EditingMode != InkCanvasEditingMode.EraseByStroke)
			{
				penMemoCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
				button.Content = langMng.getLangString("stroke");
				penMemoCanvas.MouseDown += new MouseButtonEventHandler(penMemoCanvas_MouseDown);
			}
			else
			{
				penMemoCanvas.EditingMode = InkCanvasEditingMode.Ink;
				penMemoCanvas.MouseDown -= new MouseButtonEventHandler(penMemoCanvas_MouseDown);
				button.Content = langMng.getLangString("delete");
			}
		}

		public void alterPenmemoAnimation(StrokeToolPanelHorizontal toolPanel, double f, double t)
		{
			DoubleAnimation doubleAnimation = new DoubleAnimation();
			doubleAnimation.From = f;
			doubleAnimation.To = t;
			doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.3));
			toolPanel.BeginAnimation(FrameworkElement.WidthProperty, doubleAnimation);
		}

		public void showPenToolPanelEventHandler(bool isCanvasShowed)
		{
			if (isCanvasShowed)
			{
				Panel.SetZIndex(PopupControlCanvas, 901);
				if (PopupControlCanvas.Visibility.Equals(Visibility.Collapsed))
				{
					PopupControlCanvas.Visibility = Visibility.Visible;
				}
			}
			else
			{
				Panel.SetZIndex(PopupControlCanvas, 899);
				if (PopupControlCanvas.Visibility.Equals(Visibility.Visible))
				{
					PopupControlCanvas.Visibility = Visibility.Collapsed;
				}
			}
		}

		private void PopupControlCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Panel.SetZIndex(PopupControlCanvas, 899);
			if (PopupControlCanvas.Visibility.Equals(Visibility.Visible))
			{
				PopupControlCanvas.Visibility = Visibility.Collapsed;
			}
			StrokeToolPanelHorizontal strokeToolPanelHorizontal = (StrokeToolPanelHorizontal)PenMemoToolBar.Children[PenMemoToolBar.Children.Count - 1];
			strokeToolPanelHorizontal.closePopup();
		}

		private void penMemoCanvas_MouseDown(object sender, MouseButtonEventArgs e)
		{
			StrokeCollection selectedStrokes = penMemoCanvas.GetSelectedStrokes();
			if (selectedStrokes.Count > 0)
			{
				penMemoCanvas.Strokes.Remove(selectedStrokes);
			}
		}

		public void strokeDelAllEventHandler()
		{
			for (int i = 0; i < penMemoCanvas.Strokes.Count; i++)
			{
				tempStrokes.Add(penMemoCanvas.Strokes[i]);
			}
			penMemoCanvas.Strokes.Clear();
			List<string> list = new List<string>();
			List<StrokesData> curPageStrokes = bookManager.getCurPageStrokes(userBookSno, curPageIndex);
			for (int j = 0; j < curPageStrokes.Count; j++)
			{
				list.Add(bookManager.deleteStrokeCmdString(userBookSno, curPageStrokes[j]));
			}
			bookManager.saveBatchData(list);
		}

		private Color ConvertHexStringToColour(string hexString)
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
			return Color.FromArgb(b, b2, b3, b4);
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
				NoteButton.IsChecked = false;
				TriggerBookMark_NoteButtonOrElse(NoteButton);
			}
			else if (text.Equals("NoteButton"))
			{
				TextBox textBox = FindVisualChildByName<TextBox>(MediaTableCanvas, "notePanel");
				int curPageIndex = this.curPageIndex;
				setNotesInMem(textBox.Text, curPageIndex);
				if (textBox.Text.Equals(""))
				{
					NoteButton.IsChecked = false;
					TriggerBookMark_NoteButtonOrElse(NoteButton);
				}
				else
				{
					NoteButton.IsChecked = true;
					TriggerBookMark_NoteButtonOrElse(NoteButton);
				}
				if (PaperLess_Emeeting.Properties.Settings.Default.IsFlatUIReader)
				{
					NoteButtonInLBIsClicked = false;
				}
				ShowNote();
				ShowAddition(true);
			}
		}

		private void SearchButton_Checked(object sender, RoutedEventArgs e)
		{
			doUpperRadioButtonClicked(MediaCanvasOpenedBy.SearchButton, sender);
		}

		private void NoteButton_Checked(object sender, RoutedEventArgs e)
		{
			if (PaperLess_Emeeting.Properties.Settings.Default.IsFlatUIReader)
			{
				Canvas.SetTop(mediaListBorder, double.NaN);
				Canvas.SetRight(mediaListBorder, double.NaN);
				Canvas.SetBottom(mediaListBorder, 64.0);
				Canvas.SetLeft(mediaListBorder, PenSP.Width + 64.0);
			}
			doUpperRadioButtonClicked(MediaCanvasOpenedBy.NoteButton, sender);
			if (MediaTableCanvas.Visibility == Visibility.Visible)
			{
				MemoSP.Background = ColorTool.HexColorToBrush("#F66F00");
			}
		}

		private void BackToBookShelfButton_Click(object sender, RoutedEventArgs e)
		{
			if (!isSyncing || isSyncOwner)
			{
				Close();
			}
		}

		private void BookMarkButton_Checked(object sender, RoutedEventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			bool flag = false;
			int num = 0;
			if (bookMarkDictionary.ContainsKey(curPageIndex) && bookMarkDictionary[curPageIndex].status == "0")
			{
				flag = true;
				num = 1;
			}
			setBookMark(curPageIndex, !flag);
			radioButton.IsChecked = !flag;
			BookMarkButton.IsChecked = !flag;
			btnBookMark.IsChecked = !flag;
			TriggerBookMark_NoteButtonOrElse(radioButton);
			bool? isChecked = radioButton.IsChecked;
			if (isChecked.GetValueOrDefault() && isChecked.HasValue)
			{
				BookMarkSP.Background = ColorTool.HexColorToBrush("#F66F00");
			}
			else
			{
				BookMarkSP.Background = ColorTool.HexColorToBrush("#000000");
			}
			if (CheckIsNowClick(BookMarkButtonInListBoxSP))
			{
				ShowBookMark();
				ShowBookMark();
			}
			sendBroadCast("{\"bookmark\":" + num + ",\"pageIndex\":" + curPageIndex.ToString() + ",\"cmd\":\"R.SB\"}");
		}

		private void TriggerBookMark_NoteButtonOrElse(RadioButton rb)
		{
			Brush background = ColorTool.HexColorToBrush("#F66F00");
			Brush background2 = ColorTool.HexColorToBrush("#000000");
			switch (rb.Name)
			{
			case "btnBookMark":
			case "BookMarkButton":
			{
				bool? isChecked2 = rb.IsChecked;
				if (isChecked2.GetValueOrDefault() && isChecked2.HasValue)
				{
					BookMarkSP.Background = background;
					statusBMK.Visibility = Visibility.Visible;
				}
				else
				{
					BookMarkSP.Background = background2;
					statusBMK.Visibility = Visibility.Collapsed;
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
					statusMemo.Visibility = Visibility.Visible;
				}
				else
				{
					MemoSP.Background = background2;
					statusMemo.Visibility = Visibility.Collapsed;
				}
				break;
			}
			}
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

		private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
		{
		}

		private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
		{
			e.Handled = true;
			if (!isSyncing || isSyncOwner)
			{
				if (e.Delta < 0)
				{
					MovePage(MovePageType.下一頁);
				}
				else
				{
					MovePage(MovePageType.上一頁);
				}
			}
		}

		private void MovePage(MovePageType mpt)
		{
			Action action = null;
			try
			{
				int num = 0;
				int num2 = 0;
				if (MediaTableCanvas.Visibility == Visibility.Visible)
				{
					noteButton_Click();
				}
				switch (mpt)
				{
				case MovePageType.下一頁:
					if (curPageIndex + 1 != totalPage)
					{
						penMemoCanvas.Strokes.Clear();
					}
					web_view.ExecuteScript("goNext();");
					break;
				case MovePageType.上一頁:
					if (curPageIndex != 0)
					{
						penMemoCanvas.Strokes.Clear();
					}
					web_view.ExecuteScript("goPrevious();");
					break;
				case MovePageType.第一頁:
					if (curPageIndex != 0)
					{
						penMemoCanvas.Strokes.Clear();
					}
					num = 1;
					num2 = 0;
					web_view.ExecuteScript("goToStep(" + num + ", " + num2 + ")");
					break;
				case MovePageType.最後一頁:
					if (curPageIndex + 1 != totalPage)
					{
						penMemoCanvas.Strokes.Clear();
					}
					num = totalPage;
					num2 = 0;
					web_view.ExecuteScript("goToStep(" + num + ", " + num2 + ")");
					break;
				}
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003CMovePage_003Eb__4a);
				}
				factory.StartNew(action);
			}
			catch (Exception)
			{
			}
		}

		private void MoveBoxPage()
		{
		}

		private void ShowFilterCount()
		{
			if (PaperLess_Emeeting.Properties.Settings.Default.IsFlatUIReader)
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
		}

		private void leftPageButton_Click(object sender, RoutedEventArgs e)
		{
			Action action = null;
			try
			{
				if (curPageIndex != 0)
				{
					penMemoCanvas.Strokes.Clear();
				}
				web_view.ExecuteScript("goPrevious();");
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003CleftPageButton_Click_003Eb__4d);
				}
				factory.StartNew(action);
			}
			catch (Exception)
			{
			}
		}

		private void rightPageButton_Click(object sender, RoutedEventArgs e)
		{
			Action action = null;
			try
			{
				if (curPageIndex + 1 != totalPage)
				{
					penMemoCanvas.Strokes.Clear();
				}
				web_view.ExecuteScript("goNext();");
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003CrightPageButton_Click_003Eb__50);
				}
				factory.StartNew(action);
			}
			catch (Exception)
			{
			}
		}

		private void testButton_Click(object sender, RoutedEventArgs e)
		{
			string text = "data/pres/vs9s8.mp4";
			string videoPath = basePath + text;
			prepareVideoCmd(text, videoPath);
		}

		private void prepareVideoCmd(string relativePath, string videoPath)
		{
			sendBroadCast("{\"cmd\" : \"R.PP.V\", \"bookId\" : \"" + bookId + "\", \"path\" : \"test2/" + relativePath + "\", \"action\" : \"start\"}");
			mp = new MoviePlayer(videoPath, true, true);
			mp.Closing += new CancelEventHandler(mp_Closing);
			mp.ShowDialog();
		}

		private void mp_Closing(object sender, CancelEventArgs e)
		{
			MoviePlayer moviePlayer = (MoviePlayer)sender;
			string text = moviePlayer.filePath.Replace(basePath, "");
			sendBroadCast("{\"cmd\" : \"R.PP.V\", \"bookId\" : \"" + bookId + "\", \"path\" : \"" + bookId + "/" + text + "\", \"action\" : \"stop\"}");
		}

		private void InitSmallImage()
		{
			Action action = null;
			try
			{
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003CInitSmallImage_003Eb__53);
				}
				factory.StartNew(action);
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}

		private void BookMarkButtonInListBox_Checked(object sender, RoutedEventArgs e)
		{
			if (!CheckIsNowClick(BookMarkButtonInListBoxSP))
			{
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
					action = new Action(_003CShowBookMark_003Eb__59);
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
				if (!PaperLess_Emeeting.Properties.Settings.Default.IsFlatUIReader)
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
					if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate5d == null)
					{
						CS_0024_003C_003E9__CachedAnonymousMethodDelegate5d = new Func<ThumbnailImageAndPage, int>(_003CShowBookMark_003Eb__5b);
					}
					listBox.ItemsSource = Enumerable.ToList(Enumerable.OrderBy(list, CS_0024_003C_003E9__CachedAnonymousMethodDelegate5d));
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
					action = new Action(_003CShowNote_003Eb__5e);
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
				if (!PaperLess_Emeeting.Properties.Settings.Default.IsFlatUIReader)
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
					if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate62 == null)
					{
						CS_0024_003C_003E9__CachedAnonymousMethodDelegate62 = new Func<ThumbnailImageAndPage, int>(_003CShowNote_003Eb__60);
					}
					listBox.ItemsSource = Enumerable.ToList(Enumerable.OrderBy(list, CS_0024_003C_003E9__CachedAnonymousMethodDelegate62));
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
			Brush background = SP.Background;
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

		private void ShowAll()
		{
			BookMarkInLBIsClicked = false;
			NoteButtonInLBIsClicked = false;
			AllImageButtonInListBox.IsChecked = true;
			if (!PaperLess_Emeeting.Properties.Settings.Default.IsFlatUIReader)
			{
				ListBox listBox = thumbNailListBox;
				List<ThumbnailImageAndPage> source = singleThumbnailImageAndPageList;
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate64 == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate64 = new Func<ThumbnailImageAndPage, int>(_003CShowAll_003Eb__63);
				}
				listBox.ItemsSource = Enumerable.ToList(Enumerable.OrderBy(source, CS_0024_003C_003E9__CachedAnonymousMethodDelegate64));
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

		private void ShowAddition(bool ShowFilter = true)
		{
			MoveBoxPage();
			if (ShowFilter)
			{
				ShowFilterCount();
			}
			ShowImageCenter();
		}

		private void ChangeThumbNailListBoxRelativeStatus()
		{
			Action action = null;
			if (MediaTableCanvas.Visibility.Equals(Visibility.Visible))
			{
				TextBox textBox = FindVisualChildByName<TextBox>(mediaListPanel, "notePanel");
				if (textBox != null && textBox.Text.Equals(""))
				{
					MemoSP.Background = ColorTool.HexColorToBrush("#000000");
				}
				MediaTableCanvas.Visibility = Visibility.Collapsed;
			}
			ScrollViewer scrollViewer = FindVisualChildByName<ScrollViewer>(thumbNailListBox, "SVInLV");
			BookMarkInLBIsClicked = false;
			NoteButtonInLBIsClicked = false;
			AllImageButtonInListBox.IsChecked = true;
			if (PaperLess_Emeeting.Properties.Settings.Default.IsFlatUIReader)
			{
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003CChangeThumbNailListBoxRelativeStatus_003Eb__65);
				}
				factory.StartNew(action);
			}
			switch (thumbNailListBoxStatus)
			{
			case 2:
				break;
			case 0:
				thumbNailListBoxOpenedFullScreen = false;
				thumnailCanvas.Visibility = Visibility.Hidden;
				ShowListBoxButton.Visibility = Visibility.Visible;
				AllImageButtonInListBox.IsChecked = true;
				break;
			case 1:
			{
				thumbNailListBoxOpenedFullScreen = false;
				Binding binding = new Binding();
				binding.Source = FR;
				binding.Path = new PropertyPath("ActualWidth");
				binding.Converter = new thumbNailListBoxWidthHeightConverter();
				binding.ConverterParameter = 30;
				if (PaperLess_Emeeting.Properties.Settings.Default.IsFlatUIReader)
				{
					binding.ConverterParameter = 105;
				}
				thumbNailListBox.SetBinding(FrameworkElement.WidthProperty, binding);
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
				ShowListBoxButton.Visibility = Visibility.Hidden;
				break;
			}
			}
		}

		private void ShowListBoxButton_Click(object sender, RoutedEventArgs e)
		{
			ShowListBoxButton.Visibility = Visibility.Collapsed;
			thumnailCanvas.Visibility = Visibility.Visible;
			thumbNailListBoxStatus = 1;
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
					action = new Action(_003CShowListBoxButtonNew_Click_003Eb__68);
				}
				MyAnimation(sp, 500.0, "Height", 150.0, 0.0, action);
				ViewThumbSP.Background = ColorTool.HexColorToBrush("#000000");
			}
			else
			{
				Canvas sp2 = thumnailCanvas;
				if (action2 == null)
				{
					action2 = new Action(_003CShowListBoxButtonNew_Click_003Eb__69);
				}
				MyAnimation(sp2, 500.0, "Height", 0.0, 150.0, action2);
				ViewThumbSP.Background = ColorTool.HexColorToBrush("#F66F00");
				ShowAll();
				thumbNailListBoxStatus = 1;
				ChangeThumbNailListBoxRelativeStatus();
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
			MouseTool.ShowArrow();
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
			penMemoCanvas.DefaultDrawingAttributes = drawingAttributes;
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
			Brush background = btnEraserGD.Background;
			if (background is SolidColorBrush)
			{
				string text = ((SolidColorBrush)background).Color.ToString();
				if (text.Equals("#FFF66F00"))
				{
					btnEraserGD.Background = Brushes.Transparent;
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
				Canvas.SetTop(mediaListBorder, double.NaN);
				Canvas.SetRight(mediaListBorder, double.NaN);
				mediaListBorder.BeginAnimation(Canvas.LeftProperty, doubleAnimation);
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
						action = new Action(_003CStartAnimation_003Eb__6d);
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
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate70 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate70 = new EventHandler(_003CStartAnimation_003Eb__6e);
			}
			storyboard.Completed += CS_0024_003C_003E9__CachedAnonymousMethodDelegate70;
			PowerEase powerEase = new PowerEase();
			powerEase.EasingMode = EasingMode.EaseOut;
			PowerEase powerEase2 = (PowerEase)(doubleAnimation3.EasingFunction = powerEase);
			RotateTransform rotateTransform = (RotateTransform)(image.RenderTransform = new RotateTransform());
			image.RenderTransformOrigin = new Point(0.5, 0.5);
			storyboard.Begin();
			rotateTransform.BeginAnimation(RotateTransform.AngleProperty, doubleAnimation3);
		}

		private void HideListBoxButton_Click(object sender, RoutedEventArgs e)
		{
			ShowListBoxButton.Visibility = Visibility.Visible;
			thumnailCanvas.Visibility = Visibility.Collapsed;
			thumbNailListBoxStatus = 0;
			ChangeThumbNailListBoxRelativeStatus();
		}

		private void ShowAllImageButton_Checked(object sender, RoutedEventArgs e)
		{
			thumbNailListBoxStatus = 2;
			ChangeThumbNailListBoxRelativeStatus();
		}

		private void thumbNailListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			_003C_003Ec__DisplayClass73 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass73();
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			try
			{
				firstIndex++;
				if (firstIndex > 1 && (!isSyncing || isSyncOwner) && NoteButton != null)
				{
					doUpperRadioButtonClicked(MediaCanvasOpenedBy.NoteButton, NoteButton);
					MediaTableCanvas.Visibility = Visibility.Collapsed;
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
			_003C_003Ec__DisplayClass.tempIndex = 0;
			if (NoteButtonInLBIsClicked || BookMarkInLBIsClicked)
			{
				object selectedItem = thumbNailListBox.SelectedItem;
				_003C_003Ec__DisplayClass.tempIndex = singleThumbnailImageAndPageList.IndexOf((ThumbnailImageAndPage)thumbNailListBox.SelectedItem);
			}
			else
			{
				_003C_003Ec__DisplayClass.tempIndex = thumbNailListBox.SelectedIndex;
			}
			int num = 0;
			web_view.ExecuteScript("goToStep(" + (_003C_003Ec__DisplayClass.tempIndex + 1).ToString() + ", " + num + ")");
			if (isHTML5ReaderLoaded)
			{
				sendBroadCast("{\"pageIndex\":" + _003C_003Ec__DisplayClass.tempIndex + ",\"cmd\":\"R.TP\"}");
			}
			Task.Factory.StartNew(new Action(_003C_003Ec__DisplayClass._003CthumbNailListBox_SelectionChanged_003Eb__71));
			if (isFirstTimeLoaded)
			{
				isLockButtonLocked = true;
				if (!_003C_003Ec__DisplayClass.tempIndex.Equals(0))
				{
					_003C_003Ec__DisplayClass.tempIndex.Equals(thumbNailListBox.Items.Count - 1);
				}
			}
			if (thumbNailListBoxOpenedFullScreen)
			{
				thumnailCanvas.Visibility = Visibility.Hidden;
				BindingOperations.ClearBinding(thumnailCanvas, FrameworkElement.HeightProperty);
				BindingOperations.ClearBinding(thumbNailListBox, FrameworkElement.HeightProperty);
				RadioButton radioButton = FindVisualChildByName<RadioButton>(FR, "ShowAllImageButton");
				radioButton.IsChecked = false;
				ShowListBoxButton.Visibility = Visibility.Visible;
			}
			ListBoxItem listBoxItem = (ListBoxItem)thumbNailListBox.ItemContainerGenerator.ContainerFromItem(thumbNailListBox.SelectedItem);
			if (listBoxItem != null)
			{
				listBoxItem.Focus();
				if (!thumbNailListBoxOpenedFullScreen && (double)(_003C_003Ec__DisplayClass.tempIndex + 1) * listBoxItem.ActualWidth > base.ActualWidth / 2.0)
				{
					ScrollViewer scrollViewer = FindVisualChildByName<ScrollViewer>(thumbNailListBox, "SVInLV");
					double offset = (double)(_003C_003Ec__DisplayClass.tempIndex + 1) * listBoxItem.ActualWidth - base.ActualWidth / 2.0;
					scrollViewer.ScrollToHorizontalOffset(offset);
				}
			}
			ShowFilterCount();
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

		private void bringBlockIntoView(int pageIndex)
		{
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
			if (isHTML5ReaderLoaded)
			{
				sendBroadCast("{\"pageIndex\":" + pageIndex + ",\"cmd\":\"R.TP\"}");
			}
			ShowAddition(true);
		}

		private void resetFocusBackToReader()
		{
		}

		private void ShareButton_Checked(object sender, RoutedEventArgs e)
		{
			MouseTool.ShowLoading();
			SentMailSP.Background = ColorTool.HexColorToBrush("#F66F00");
			SendEmail();
		}

		private void SendEmail()
		{
			string webServiceURL = webServiceURL;
			string directoryName = System.IO.Path.GetDirectoryName(pptPath);
			string text = directoryName + "\\imgMail";
			Directory.CreateDirectory(text);
			string text2 = string.Concat(text, "\\", Guid.NewGuid(), ".jpg");
			string arg = web_view.ActualWidth.ToString();
			string arg2 = web_view.ActualHeight.ToString();
			string source = string.Format("0,0,{0},{1}", arg, arg2);
			BitmapSource bmp = CaptureScreenshotTool.Capture(Rect.Parse(source));
			UseBitmapCodecsTool.WriteJpeg(text2, 30, bmp);
			string text3 = "";
			if (bookNoteDictionary.ContainsKey(curPageIndex))
			{
				text3 = bookNoteDictionary[curPageIndex].text;
			}
			GetAnnotationUpload.AsyncPOST(meetingId, bookId, email, text3, text2, new Action<AnnotationUpload>(_003CSendEmail_003Eb__75));
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
			ShareButton.IsChecked = false;
			SentMailSP.Background = ColorTool.HexColorToBrush("#000000");
			MouseTool.ShowArrow();
		}

		private Border GetBorderInReader()
		{
			return FindVisualChildByName<Border>(FR, "PART_ContentHost");
		}

		private UIElement GetImageInReader()
		{
			int curPageIndex = this.curPageIndex;
			Block block = FR.Document.Blocks.FirstBlock;
			UIElement result = new UIElement();
			if (FR.CanGoToPage(curPageIndex))
			{
				for (int i = 0; i < curPageIndex; i++)
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

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/html5readwindow.xaml", UriKind.Relative);
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
				((HTML5ReadWindow)target).SizeChanged += new SizeChangedEventHandler(Window_SizeChanged);
				((HTML5ReadWindow)target).MouseWheel += new MouseWheelEventHandler(Window_MouseWheel);
				break;
			case 2:
				mainGrid = (Grid)target;
				break;
			case 3:
				FR = (FlowDocumentReader)target;
				break;
			case 4:
				ToolBarSensor = (Canvas)target;
				break;
			case 5:
				PenMemoToolBar = (Grid)target;
				break;
			case 6:
				BackToOriToolBar = (RadioButton)target;
				BackToOriToolBar.Click += new RoutedEventHandler(PenMemoButton_Checked);
				break;
			case 7:
				ToolBarInReader = (Grid)target;
				break;
			case 8:
				txtPage = (TextBlock)target;
				break;
			case 9:
				BackToBookShelfButton = (RadioButton)target;
				BackToBookShelfButton.Click += new RoutedEventHandler(BackToBookShelfButton_Click);
				break;
			case 10:
				MediasStackPanel = (StackPanel)target;
				break;
			case 11:
				cbBooks = (ComboBox)target;
				break;
			case 12:
				SearchButton = (RadioButton)target;
				SearchButton.Click += new RoutedEventHandler(SearchButton_Checked);
				break;
			case 13:
				PenMemoButton = (RadioButton)target;
				PenMemoButton.Click += new RoutedEventHandler(PenMemoButton_Checked);
				break;
			case 14:
				BookMarkButton = (RadioButton)target;
				BookMarkButton.Click += new RoutedEventHandler(BookMarkButton_Checked);
				break;
			case 15:
				NoteButton = (RadioButton)target;
				NoteButton.Click += new RoutedEventHandler(NoteButton_Checked);
				break;
			case 16:
				ShareButton = (RadioButton)target;
				ShareButton.Click += new RoutedEventHandler(ShareButton_Checked);
				break;
			case 17:
				syncButton = (ToggleButton)target;
				syncButton.Click += new RoutedEventHandler(syncButton_Click);
				break;
			case 18:
				diableImg = (System.Windows.Controls.Image)target;
				break;
			case 19:
				toolbarSyncCanvas = (Canvas)target;
				break;
			case 20:
				Grid_33 = (Grid)target;
				break;
			case 21:
				MediaTableCanvas = (Canvas)target;
				MediaTableCanvas.MouseLeftButtonDown += new MouseButtonEventHandler(MediaTableCanvas_MouseLeftButtonDown);
				break;
			case 22:
				mediaListBorder = (Border)target;
				break;
			case 23:
				mediaListPanel = (StackPanel)target;
				break;
			case 24:
				stageCanvas = (Canvas)target;
				break;
			case 25:
				penMemoCanvas = (InkCanvas)target;
				break;
			case 26:
				PopupControlCanvas = (Canvas)target;
				PopupControlCanvas.MouseLeftButtonDown += new MouseButtonEventHandler(PopupControlCanvas_MouseLeftButtonDown);
				break;
			case 27:
				HiddenControlCanvas = (Canvas)target;
				break;
			case 28:
				watermarkCanvas = (Canvas)target;
				break;
			case 29:
				watermarkTextBlock = (TextBlock)target;
				break;
			case 30:
				web_view = (WebView)target;
				break;
			case 31:
				leftPageButton = (RadioButton)target;
				leftPageButton.Click += new RoutedEventHandler(leftPageButton_Click);
				break;
			case 32:
				rightPageButton = (RadioButton)target;
				rightPageButton.Click += new RoutedEventHandler(rightPageButton_Click);
				break;
			case 33:
				statusBMK = (System.Windows.Controls.Image)target;
				break;
			case 34:
				statusMemo = (System.Windows.Controls.Image)target;
				break;
			case 35:
				StatusOnairOff = (System.Windows.Controls.Image)target;
				break;
			case 36:
				screenBroadcasting = (System.Windows.Controls.Image)target;
				break;
			case 37:
				screenReceiving = (System.Windows.Controls.Image)target;
				break;
			case 38:
				syncCanvas = (Canvas)target;
				break;
			case 39:
				ShowListBoxButton = (RadioButton)target;
				ShowListBoxButton.Click += new RoutedEventHandler(ShowListBoxButton_Click);
				break;
			case 40:
				ShowListBoxButtonNew = (RadioButton)target;
				break;
			case 41:
				NewUITop = (Grid)target;
				break;
			case 42:
				btnFuncSP = (StackPanel)target;
				break;
			case 43:
				btnBoldSP = (StackPanel)target;
				btnBoldSP.MouseLeave += new MouseEventHandler(btnBoldSP_MouseLeave);
				break;
			case 44:
				btnThin = (Grid)target;
				btnThin.MouseEnter += new MouseEventHandler(Grid_MouseEnterTransparent);
				btnThin.MouseLeave += new MouseEventHandler(Grid_MouseLeaveTransparent);
				break;
			case 45:
				((RadioButton)target).Click += new RoutedEventHandler(btnThickness_Click);
				break;
			case 46:
				btnMedium = (Grid)target;
				btnMedium.MouseEnter += new MouseEventHandler(Grid_MouseEnterTransparent);
				btnMedium.MouseLeave += new MouseEventHandler(Grid_MouseLeaveTransparent);
				break;
			case 47:
				((RadioButton)target).Click += new RoutedEventHandler(btnThickness_Click);
				break;
			case 48:
				btnLarge = (Grid)target;
				btnLarge.MouseEnter += new MouseEventHandler(Grid_MouseEnterTransparent);
				btnLarge.MouseLeave += new MouseEventHandler(Grid_MouseLeaveTransparent);
				break;
			case 49:
				((RadioButton)target).Click += new RoutedEventHandler(btnThickness_Click);
				break;
			case 50:
				btnPenFuncSP = (StackPanel)target;
				btnPenFuncSP.MouseLeave += new MouseEventHandler(btnPenFuncSP_MouseLeave);
				break;
			case 51:
				PenColorSP = (StackPanel)target;
				break;
			case 52:
				((Grid)target).MouseEnter += new MouseEventHandler(Grid_MouseEnter);
				((Grid)target).MouseLeave += new MouseEventHandler(Grid_MouseLeave);
				break;
			case 53:
				((RadioButton)target).Click += new RoutedEventHandler(btnPenColor_Click);
				break;
			case 54:
				((Grid)target).MouseEnter += new MouseEventHandler(Grid_MouseEnter);
				((Grid)target).MouseLeave += new MouseEventHandler(Grid_MouseLeave);
				break;
			case 55:
				((RadioButton)target).Click += new RoutedEventHandler(btnPenColor_Click);
				break;
			case 56:
				((Grid)target).MouseEnter += new MouseEventHandler(Grid_MouseEnter);
				((Grid)target).MouseLeave += new MouseEventHandler(Grid_MouseLeave);
				break;
			case 57:
				((RadioButton)target).Click += new RoutedEventHandler(btnPenColor_Click);
				break;
			case 58:
				((Grid)target).MouseEnter += new MouseEventHandler(Grid_MouseEnter);
				((Grid)target).MouseLeave += new MouseEventHandler(Grid_MouseLeave);
				break;
			case 59:
				((RadioButton)target).Click += new RoutedEventHandler(btnPenColor_Click);
				break;
			case 60:
				((Grid)target).MouseEnter += new MouseEventHandler(Grid_MouseEnter);
				((Grid)target).MouseLeave += new MouseEventHandler(Grid_MouseLeave);
				break;
			case 61:
				((RadioButton)target).Click += new RoutedEventHandler(btnPenColor_Click);
				break;
			case 62:
				((Grid)target).MouseEnter += new MouseEventHandler(Grid_MouseEnter);
				((Grid)target).MouseLeave += new MouseEventHandler(Grid_MouseLeave);
				break;
			case 63:
				((RadioButton)target).Click += new RoutedEventHandler(btnPenColor_Click);
				break;
			case 64:
				((Grid)target).MouseEnter += new MouseEventHandler(Grid_MouseEnter);
				((Grid)target).MouseLeave += new MouseEventHandler(Grid_MouseLeave);
				break;
			case 65:
				((RadioButton)target).Click += new RoutedEventHandler(btnPenColor_Click);
				break;
			case 66:
				((Grid)target).MouseEnter += new MouseEventHandler(Grid_MouseEnter);
				((Grid)target).MouseLeave += new MouseEventHandler(Grid_MouseLeave);
				break;
			case 67:
				((RadioButton)target).Click += new RoutedEventHandler(btnPenColor_Click);
				break;
			case 68:
				((Grid)target).MouseEnter += new MouseEventHandler(Grid_MouseEnter);
				((Grid)target).MouseLeave += new MouseEventHandler(Grid_MouseLeave);
				break;
			case 69:
				((RadioButton)target).Click += new RoutedEventHandler(btnPenColor_Click);
				break;
			case 70:
				((Grid)target).MouseEnter += new MouseEventHandler(Grid_MouseEnter);
				((Grid)target).MouseLeave += new MouseEventHandler(Grid_MouseLeave);
				break;
			case 71:
				((RadioButton)target).Click += new RoutedEventHandler(btnPenColor_Click);
				break;
			case 72:
				NewUI = (Grid)target;
				break;
			case 73:
				btnPen = (RadioButton)target;
				btnPen.Click += new RoutedEventHandler(btnPen_Click);
				break;
			case 74:
				PenSlideCtrl = (System.Windows.Controls.Image)target;
				break;
			case 75:
				PenSP = (StackPanel)target;
				break;
			case 76:
				btnPenColor = (RadioButton)target;
				break;
			case 77:
				btnBold = (RadioButton)target;
				break;
			case 78:
				btnEraserGD = (Grid)target;
				break;
			case 79:
				btnEraser = (RadioButton)target;
				btnEraser.Click += new RoutedEventHandler(btnEraser_Click);
				break;
			case 80:
				((RadioButton)target).Click += new RoutedEventHandler(btnSetting_Click);
				break;
			case 81:
				SettingSlideCtrl = (System.Windows.Controls.Image)target;
				break;
			case 82:
				SettingSP = (StackPanel)target;
				break;
			case 83:
				BookMarkSP = (StackPanel)target;
				break;
			case 84:
				btnBookMark = (RadioButton)target;
				btnBookMark.Click += new RoutedEventHandler(BookMarkButton_Checked);
				break;
			case 85:
				MemoSP = (StackPanel)target;
				break;
			case 86:
				btnNoteButton = (RadioButton)target;
				btnNoteButton.Click += new RoutedEventHandler(NoteButton_Checked);
				break;
			case 87:
				SentMailSP = (StackPanel)target;
				break;
			case 88:
				((RadioButton)target).Click += new RoutedEventHandler(ShareButton_Checked);
				break;
			case 89:
				ViewThumbSP = (StackPanel)target;
				break;
			case 90:
				btnViewThumb = (RadioButton)target;
				btnViewThumb.Click += new RoutedEventHandler(ShowListBoxButtonNew_Click);
				break;
			case 91:
				((RadioButton)target).Click += new RoutedEventHandler(BackToBookShelfButton_Click);
				break;
			case 92:
				btnClose = (System.Windows.Controls.Image)target;
				break;
			case 93:
				thumnailCanvas = (Canvas)target;
				break;
			case 94:
				SearchSP = (StackPanel)target;
				break;
			case 95:
				txtKeyword = (TextBox)target;
				break;
			case 96:
				btnTxtKeywordClear = (RadioButton)target;
				break;
			case 97:
				txtFilterCount = (TextBlock)target;
				break;
			case 98:
				thumbNailCanvasStackPanel = (StackPanel)target;
				break;
			case 99:
				thumbNailCanvasGrid = (Grid)target;
				break;
			case 100:
				RadioButtonStackPanel = (StackPanel)target;
				break;
			case 101:
				AllImageButtonInListBox = (RadioButton)target;
				AllImageButtonInListBox.Click += new RoutedEventHandler(AllImageButtonInListBox_Checked);
				break;
			case 102:
				AllImageButtonInListBoxSP = (StackPanel)target;
				AllImageButtonInListBoxSP.MouseLeftButtonDown += new MouseButtonEventHandler(AllImageButtonInListBoxSP_MouseLeftButtonDown);
				break;
			case 103:
				AllImageButtonInListBoxNew = (RadioButton)target;
				AllImageButtonInListBoxNew.Click += new RoutedEventHandler(AllImageButtonInListBox_Checked);
				break;
			case 104:
				BookMarkButtonInListBox = (RadioButton)target;
				BookMarkButtonInListBox.Click += new RoutedEventHandler(BookMarkButtonInListBox_Checked);
				break;
			case 105:
				Rect1 = (System.Windows.Shapes.Rectangle)target;
				break;
			case 106:
				BookMarkButtonInListBoxSP = (StackPanel)target;
				BookMarkButtonInListBoxSP.MouseLeftButtonDown += new MouseButtonEventHandler(BookMarkButtonInListBoxSP_MouseLeftButtonDown);
				break;
			case 107:
				BookMarkButtonInListBoxNew = (RadioButton)target;
				BookMarkButtonInListBoxNew.Click += new RoutedEventHandler(BookMarkButtonInListBox_Checked);
				break;
			case 108:
				NoteButtonInListBox = (RadioButton)target;
				NoteButtonInListBox.Click += new RoutedEventHandler(NoteButtonInListBox_Checked);
				break;
			case 109:
				Rect2 = (System.Windows.Shapes.Rectangle)target;
				break;
			case 110:
				NoteButtonInListBoxSP = (StackPanel)target;
				NoteButtonInListBoxSP.MouseLeftButtonDown += new MouseButtonEventHandler(NoteButtonInListBoxSP_MouseLeftButtonDown);
				break;
			case 111:
				NoteButtonInListBoxNew = (RadioButton)target;
				NoteButtonInListBoxNew.Click += new RoutedEventHandler(NoteButtonInListBox_Checked);
				break;
			case 112:
				HideListBoxButton = (RadioButton)target;
				HideListBoxButton.Click += new RoutedEventHandler(HideListBoxButton_Click);
				break;
			case 113:
				thumbNailListBoxGD = (Grid)target;
				break;
			case 114:
				thumbNailListBox = (ListBox)target;
				thumbNailListBox.SelectionChanged += new SelectionChangedEventHandler(thumbNailListBox_SelectionChanged);
				break;
			case 115:
				webViewGrid = (Grid)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}

		[CompilerGenerated]
		private void _003CAttachKey_003Eb__0(object sender, KeyEventArgs e)
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
					break;
				case Key.Left:
					MovePage(MovePageType.上一頁);
					break;
				case Key.Right:
					MovePage(MovePageType.下一頁);
					break;
				case Key.Up:
					MovePage(MovePageType.上一頁);
					break;
				case Key.Down:
					MovePage(MovePageType.下一頁);
					break;
				case Key.Next:
					MovePage(MovePageType.下一頁);
					break;
				case Key.Prior:
					MovePage(MovePageType.上一頁);
					break;
				case Key.Home:
					MovePage(MovePageType.第一頁);
					break;
				case Key.End:
					MovePage(MovePageType.最後一頁);
					break;
				case Key.Escape:
					OpenClosePaint();
					break;
				}
			}
		}

		[CompilerGenerated]
		private void _003CChangeFlatUI_003Eb__1(object sender, MouseButtonEventArgs e)
		{
			Task.Factory.StartNew(new Action(_003CChangeFlatUI_003Eb__2));
		}

		[CompilerGenerated]
		private void _003CChangeFlatUI_003Eb__4(object sender, MouseButtonEventArgs e)
		{
			Task.Factory.StartNew(new Action(_003CChangeFlatUI_003Eb__5));
		}

		[CompilerGenerated]
		private void _003CChangeFlatUI_003Eb__7(object sender, MouseButtonEventArgs e)
		{
			Task.Factory.StartNew(new Action(_003CChangeFlatUI_003Eb__8));
		}

		[CompilerGenerated]
		private void _003CChangeFlatUI_003Eb__a(object sender, MouseButtonEventArgs e)
		{
			OpenClosePaint();
		}

		[CompilerGenerated]
		private void _003CChangeFlatUI_003Eb__b(object sender, MouseEventArgs e)
		{
			MouseTool.ShowIBeam();
			txtKeyword.Focus();
		}

		[CompilerGenerated]
		private static void _003CChangeFlatUI_003Eb__c(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private void _003CChangeFlatUI_003Eb__d(object sender, RoutedEventArgs e)
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
		private void _003CChangeFlatUI_003Eb__e(object sender, RoutedEventArgs e)
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
		private void _003CChangeFlatUI_003Eb__f(object sender, RoutedEventArgs e)
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
		private void _003CChangeFlatUI_003Eb__2()
		{
			if (socket == null)
			{
				Singleton_Socket.ReaderEvent = this;
				socket = Singleton_Socket.GetInstance(meetingId, account, userName, true);
			}
			base.Dispatcher.BeginInvoke(new Action(_003CChangeFlatUI_003Eb__3));
		}

		[CompilerGenerated]
		private void _003CChangeFlatUI_003Eb__3()
		{
			AutoClosingMessageBox.Show("連線中");
			syncButton.IsChecked = true;
			syncButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
		}

		[CompilerGenerated]
		private void _003CChangeFlatUI_003Eb__5()
		{
			if (socket == null)
			{
				Singleton_Socket.ReaderEvent = this;
				socket = Singleton_Socket.GetInstance(meetingId, account, userName, false);
			}
			base.Dispatcher.BeginInvoke(new Action(_003CChangeFlatUI_003Eb__6));
		}

		[CompilerGenerated]
		private void _003CChangeFlatUI_003Eb__6()
		{
			AutoClosingMessageBox.Show("連線中");
			syncButton.IsChecked = false;
			syncButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
		}

		[CompilerGenerated]
		private void _003CChangeFlatUI_003Eb__8()
		{
			if (socket == null)
			{
				Singleton_Socket.ReaderEvent = this;
				socket = Singleton_Socket.GetInstance(meetingId, account, userName, false);
			}
			base.Dispatcher.BeginInvoke(new Action(_003CChangeFlatUI_003Eb__9));
		}

		[CompilerGenerated]
		private void _003CChangeFlatUI_003Eb__9()
		{
			AutoClosingMessageBox.Show("連線中");
			syncButton.IsChecked = false;
			syncButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
		}

		[CompilerGenerated]
		private void _003CtxtKeyword_PreviewKeyDown_003Eb__1c()
		{
			Thread.Sleep(10);
			base.Dispatcher.BeginInvoke(new Action(_003CtxtKeyword_PreviewKeyDown_003Eb__1d));
		}

		[CompilerGenerated]
		private void _003CtxtKeyword_PreviewKeyDown_003Eb__1d()
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
		private static int _003CtxtKeyword_KeyUp_003Eb__1e(SearchRecord x)
		{
			return x.targetPage - 1;
		}

		[CompilerGenerated]
		private void _003CInitSyncCenter_003Eb__21()
		{
			if (PaperLess_Emeeting.Properties.Settings.Default.HasSyncCenterModule)
			{
				try
				{
					SyncCenter syncCenter = new SyncCenter();
					syncCenter.bookManager = new BookManager(dbPath);
					alterAccountWhenSyncing(isSyncOwner);
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
		private static void _003CHTML5Reader_Loaded_003Eb__22(object sender2, ConsoleMessageEventArgs args2)
		{
			string text = string.Format("Webview {0}({1}): {2}", args2.Source, args2.Line, args2.Message);
			Console.WriteLine(text);
			LogTool.Debug(text);
		}

		[CompilerGenerated]
		private void _003CHTML5Reader_Loaded_003Eb__23(object sender2, MouseButtonEventArgs e2)
		{
			Task.Factory.StartNew(new Action(_003CHTML5Reader_Loaded_003Eb__24));
		}

		[CompilerGenerated]
		private void _003CHTML5Reader_Loaded_003Eb__26(object sender3, EventArgs e3)
		{
			InitPen();
		}

		[CompilerGenerated]
		private void _003CHTML5Reader_Loaded_003Eb__24()
		{
			if (isSyncing && (!isSyncing || isSyncOwner))
			{
				Thread.Sleep(700);
				base.Dispatcher.BeginInvoke(new Action(_003CHTML5Reader_Loaded_003Eb__25));
				ShowAddition(false);
			}
		}

		[CompilerGenerated]
		private void _003CHTML5Reader_Loaded_003Eb__25()
		{
			txtPage.Text = string.Format("{0} / {1}", (curPageIndex + 1).ToString(), totalPage.ToString());
		}

		[CompilerGenerated]
		private void _003CInitPen_003Eb__28()
		{
			Thread.Sleep(5000);
			base.Dispatcher.BeginInvoke(new Action(_003CInitPen_003Eb__29));
		}

		[CompilerGenerated]
		private void _003CInitPen_003Eb__29()
		{
			string socketMessage = this.socketMessage;
			if (!this.socketMessage.Equals(""))
			{
				parseJSonFromMessage(this.socketMessage);
				this.socketMessage = "";
			}
			if (penMemoCanvas.Strokes.Count <= 0 && !socketMessage.Equals(""))
			{
				parseJSonFromMessage(this.socketMessage);
			}
			CanSentLine = true;
		}

		[CompilerGenerated]
		private static int _003CSavePDF_003Eb__2a(string a, string b)
		{
			int num = int.Parse(new FileInfo(a).Name.Split('.')[0]);
			int value = int.Parse(new FileInfo(b).Name.Split('.')[0]);
			return num.CompareTo(value);
		}

		[CompilerGenerated]
		private void _003CsetMsgToActionDelegate_003Eb__34()
		{
			Thread.Sleep(6000);
			dictCache.Clear();
		}

		[CompilerGenerated]
		private void _003CsetMsgToActionDelegate_003Eb__35()
		{
			Thread.Sleep(700);
			base.Dispatcher.BeginInvoke(new Action(_003CsetMsgToActionDelegate_003Eb__36));
			ShowAddition(false);
		}

		[CompilerGenerated]
		private void _003CsetMsgToActionDelegate_003Eb__36()
		{
			txtPage.Text = string.Format("{0} / {1}", (curPageIndex + 1).ToString(), totalPage.ToString());
		}

		[CompilerGenerated]
		private static void _003CgetNotesAndMakeNote_003Eb__41(object sender, MouseEventArgs e)
		{
		}

		[CompilerGenerated]
		private void _003Clb_SelectionChanged_003Eb__46()
		{
			Thread.Sleep(700);
			base.Dispatcher.BeginInvoke(new Action(_003Clb_SelectionChanged_003Eb__47));
		}

		[CompilerGenerated]
		private void _003Clb_SelectionChanged_003Eb__47()
		{
			txtPage.Text = string.Format("{0} / {1}", (curPageIndex + 1).ToString(), totalPage.ToString());
		}

		[CompilerGenerated]
		private void _003CMovePage_003Eb__4a()
		{
			Thread.Sleep(700);
			base.Dispatcher.BeginInvoke(new Action(_003CMovePage_003Eb__4b));
			ShowAddition(false);
		}

		[CompilerGenerated]
		private void _003CMovePage_003Eb__4b()
		{
			txtPage.Text = string.Format("{0} / {1}", (curPageIndex + 1).ToString(), totalPage.ToString());
		}

		[CompilerGenerated]
		private void _003CleftPageButton_Click_003Eb__4d()
		{
			Thread.Sleep(700);
			base.Dispatcher.BeginInvoke(new Action(_003CleftPageButton_Click_003Eb__4e));
			ShowAddition(false);
		}

		[CompilerGenerated]
		private void _003CleftPageButton_Click_003Eb__4e()
		{
			txtPage.Text = string.Format("{0} / {1}", (curPageIndex + 1).ToString(), totalPage.ToString());
		}

		[CompilerGenerated]
		private void _003CrightPageButton_Click_003Eb__50()
		{
			Thread.Sleep(700);
			base.Dispatcher.BeginInvoke(new Action(_003CrightPageButton_Click_003Eb__51));
			ShowAddition(false);
		}

		[CompilerGenerated]
		private void _003CrightPageButton_Click_003Eb__51()
		{
			txtPage.Text = string.Format("{0} / {1}", (curPageIndex + 1).ToString(), totalPage.ToString());
		}

		[CompilerGenerated]
		private void _003CInitSmallImage_003Eb__53()
		{
			Action action = null;
			singleThumbnailImageAndPageList = new List<ThumbnailImageAndPage>();
			string path = pptPath;
			path = System.IO.Path.GetDirectoryName(path);
			string path2 = path + "\\data\\Thumbnails";
			if (Directory.Exists(path2))
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(path2);
				totalPage = Enumerable.Count(directoryInfo.GetFiles());
				int num = 0;
				string text = "";
				string text2 = "";
				FileInfo[] files = directoryInfo.GetFiles();
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate57 == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate57 = new Func<FileInfo, int>(_003CInitSmallImage_003Eb__54);
				}
				foreach (FileInfo item2 in Enumerable.OrderBy(files, CS_0024_003C_003E9__CachedAnonymousMethodDelegate57))
				{
					num++;
					text2 = item2.FullName;
					text = "";
					ThumbnailImageAndPage item = new ThumbnailImageAndPage(num.ToString(), text, text2, true);
					singleThumbnailImageAndPageList.Add(item);
				}
				Dispatcher dispatcher = base.Dispatcher;
				if (action == null)
				{
					action = new Action(_003CInitSmallImage_003Eb__55);
				}
				dispatcher.BeginInvoke(action);
			}
		}

		[CompilerGenerated]
		private static int _003CInitSmallImage_003Eb__54(FileInfo f)
		{
			return int.Parse(f.Name.ToLower().Replace("slide", "").Replace(".png", ""));
		}

		[CompilerGenerated]
		private void _003CInitSmallImage_003Eb__55()
		{
			if (PaperLess_Emeeting.Properties.Settings.Default.IsFlatUIReader)
			{
				thumbNailListBox.ItemsSource = singleThumbnailImageAndPageList;
				return;
			}
			Panel.SetZIndex(thumnailCanvas, -10);
			thumnailCanvas.Visibility = Visibility.Visible;
			thumbNailListBox.ItemsSource = singleThumbnailImageAndPageList;
			thumnailCanvas.Visibility = Visibility.Collapsed;
			Panel.SetZIndex(thumnailCanvas, 200);
		}

		[CompilerGenerated]
		private void _003CShowBookMark_003Eb__59()
		{
			base.Dispatcher.BeginInvoke(new Action(_003CShowBookMark_003Eb__5a));
		}

		[CompilerGenerated]
		private static int _003CShowBookMark_003Eb__5b(ThumbnailImageAndPage x)
		{
			return int.Parse(x.pageIndex);
		}

		[CompilerGenerated]
		private void _003CShowBookMark_003Eb__5a()
		{
			thumbNailListBox.ItemsSource = singleThumbnailImageAndPageList;
		}

		[CompilerGenerated]
		private void _003CShowNote_003Eb__5e()
		{
			base.Dispatcher.BeginInvoke(new Action(_003CShowNote_003Eb__5f));
		}

		[CompilerGenerated]
		private static int _003CShowNote_003Eb__60(ThumbnailImageAndPage x)
		{
			return int.Parse(x.pageIndex);
		}

		[CompilerGenerated]
		private void _003CShowNote_003Eb__5f()
		{
			thumbNailListBox.ItemsSource = singleThumbnailImageAndPageList;
		}

		[CompilerGenerated]
		private static int _003CShowAll_003Eb__63(ThumbnailImageAndPage x)
		{
			return int.Parse(x.pageIndex);
		}

		[CompilerGenerated]
		private void _003CChangeThumbNailListBoxRelativeStatus_003Eb__65()
		{
			base.Dispatcher.BeginInvoke(new Action(_003CChangeThumbNailListBoxRelativeStatus_003Eb__66));
		}

		[CompilerGenerated]
		private void _003CChangeThumbNailListBoxRelativeStatus_003Eb__66()
		{
			thumbNailListBox.ItemsSource = singleThumbnailImageAndPageList;
		}

		[CompilerGenerated]
		private void _003CShowListBoxButtonNew_Click_003Eb__68()
		{
			thumnailCanvas.Visibility = Visibility.Collapsed;
		}

		[CompilerGenerated]
		private void _003CShowListBoxButtonNew_Click_003Eb__69()
		{
			thumnailCanvas.Visibility = Visibility.Visible;
		}

		[CompilerGenerated]
		private void _003CStartAnimation_003Eb__6d()
		{
			thumnailCanvas.Visibility = Visibility.Collapsed;
		}

		[CompilerGenerated]
		private static void _003CStartAnimation_003Eb__6e(object sender2, EventArgs e2)
		{
		}

		[CompilerGenerated]
		private void _003CSendEmail_003Eb__75(AnnotationUpload au)
		{
			GetAnnotationUpload_DoAction(au);
		}
	}
}
