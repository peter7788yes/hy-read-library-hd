using BookFormatLoader;
using BookManagerModule;
using CACodec;
using ConfigureManagerModule;
using DataAccessObject;
using DownloadManagerModule;
using HyftdMoudule;
using LocalFilesManagerModule;
using MultiLanquageModule;
using Network;
using PXCView36;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using System.Xml;
using Utility;

namespace ReadPageModule
{
	public class ReadWindow : Window, IDisposable, IComponentConnector, IStyleConnector
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

		internal delegate void a(int A_0);

		private delegate void b(BitmapImage A_0, float A_1);

		[CompilerGenerated]
		private sealed class c
		{
			public int a;

			public int b;

			public ReadWindow c;

			internal void d()
			{
				if (!a.Equals(b))
				{
					if (c.ch.Value < (double)b)
					{
						c.ch.Value = b;
					}
				}
				else
				{
					c.ch.Visibility = Visibility.Collapsed;
					c.a1.EnableRaisingEvents = false;
					c.a1.IncludeSubdirectories = false;
					c.a1.Changed -= new FileSystemEventHandler(c.a);
					c.a1 = null;
				}
			}
		}

		[CompilerGenerated]
		private sealed class d
		{
			public int a;

			public ReadWindow b;

			internal void c()
			{
				try
				{
					FindVisualChildByName<Canvas>(b.cg, "zoomCanvas").Background = null;
					int aw = b.aw;
					if (aw.Equals(1))
					{
						b.c(a);
					}
					else if (aw.Equals(2))
					{
						int a_ = b.d(a);
						b.c(a_);
					}
				}
				catch (Exception)
				{
				}
			}
		}

		[CompilerGenerated]
		private sealed class e
		{
			public IAsyncResult a;

			public d b;

			internal void c(IAsyncResult A_0)
			{
				try
				{
					bo.EndInvoke(a);
				}
				catch (Exception)
				{
				}
				b.b.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new m(b.c));
			}
		}

		[CompilerGenerated]
		private sealed class f
		{
			public string a;

			public ReadPagePair b;

			public ReadWindow c;
		}

		[CompilerGenerated]
		private sealed class g
		{
			public Border a;

			public f b;

			internal void c()
			{
				b.c.a(b.c.m_d, am, b.a, b.c.m_ad, b.b.leftPageIndex, a);
			}
		}

		[CompilerGenerated]
		private sealed class h
		{
			public ReadPagePair a;

			public Border b;

			public ReadWindow c;

			internal void d()
			{
				c.a(c.m_d, am, a.leftImagePath, c.m_ad, c.m_p, b);
			}
		}

		[CompilerGenerated]
		private sealed class i
		{
			public string a;

			public string b;

			public h c;

			internal void d()
			{
				c.c.a(c.c.m_d, am, a, b, c.c.m_ad, c.c.m_p, c.b);
			}
		}

		[CompilerGenerated]
		private EventHandler<DataRequestEventArgs> m_a;

		private FlowDocument m_b;

		private FlowDocument m_c;

		private CACodecTools m_d;

		private List<ThumbnailImageAndPage> m_e;

		private List<ThumbnailImageAndPage> m_f;

		private List<ImageStatus> m_g;

		private List<ImageStatus> m_h;

		private Dictionary<int, ReadPagePair> m_i;

		private Dictionary<int, ReadPagePair> m_j;

		private TransformGroup m_k;

		private TransformGroup m_l;

		private System.Windows.Point m_m;

		private System.Windows.Point m_n;

		private System.Windows.Point m_o;

		private int m_p;

		private int m_q;

		private int m_r;

		private object m_s;

		private BookType m_t;

		private string m_u;

		private string m_v;

		private string m_w;

		private ConfigurationManager m_x;

		private static DispatcherTimer m_y;

		private string m_z;

		private bool m_aa;

		private int m_ab;

		private int m_ac = 96;

		public double DpiX;

		public double DpiY;

		private float m_ad = 1f;

		private double m_ae = 1.0;

		private int m_af;

		private double[] m_ag = new double[9]
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

		private bool m_ah;

		private byte[][] m_ai = new byte[2][];

		private HEJMetadata m_aj;

		private PageInfoManager m_ak;

		private PageInfoMetadata al;

		private static byte[] am;

		private Dictionary<int, BookMarkData> an;

		private Dictionary<int, NoteData> ao;

		private Dictionary<int, List<StrokesData>> ap;

		private Dictionary<string, LastPageData> aq;

		private LocalFilesManager ar;

		private DateTime @as;

		private List<Stroke> at;

		private StylusPointCollection au;

		private Stroke av;

		private int aw = 2;

		private bool ax;

		private string ay = "";

		private bool az;

		private long a0;

		private FileSystemWatcher a1;

		private bool a2;

		private bool a3;

		private TimeSpan a4 = new TimeSpan(0, 0, 0, 0, 200);

		private string a5 = Environment.MachineName;

		private BookManager a6;

		private MultiLanquageManager a7;

		private bool a8;

		private string a9;

		private TocButtonController ba;

		private string bb = "";

		private bool bc;

		private ObservableCollection<MediaList> bd;

		private bool be = true;

		private bool bf;

		private bool bg;

		private bool bh;

		private bool bi;

		private bool bj;

		private int bk;

		private int bl = 5;

		private bool bm;

		private bool bn;

		internal static a bo;

		private IInputElement bp;

		private double bq = 1.0;

		private double br = 1.0;

		private double bs = 1.0;

		private double bt = 1.0;

		private double bu;

		private double bv;

		private string bw = "originalPenmemoStatus.xml";

		private bool bx;

		private bool by;

		private bool bz;

		private PageMode b0 = PageMode.DoublePage;

		private MediaCanvasOpenedBy b1;

		private int b2;

		private Dictionary<MediaCanvasOpenedBy, StackPanel> b3 = new Dictionary<MediaCanvasOpenedBy, StackPanel>();

		private int b4;

		private bool b5;

		private double b6 = 150.0;

		private bool b7 = true;

		[CompilerGenerated]
		private EventHandler<imageSourceRenderedResultEventArgs> b8;

		private List<Thread> b9 = new List<Thread>();

		private double ca;

		private bool cb;

		private int cc = 10;

		private DateTime cd;

		private int ce = 5;

		internal Grid cf;

		internal FlowDocumentReader cg;

		internal ProgressBar ch;

		internal Canvas ci;

		internal StackPanel cj;

		internal Grid ck;

		internal StackPanel cl;

		internal RadioButton cm;

		internal RadioButton cn;

		internal RadioButton co;

		internal RadioButton cp;

		internal ListBox cq;

		internal RadioButton cr;

		internal RadioButton cs;

		internal DocumentViewer ct;

		private bool cu;

		public event EventHandler<DataRequestEventArgs> ReadWindowDataRequest
		{
			[CompilerGenerated]
			add
			{
				EventHandler<DataRequestEventArgs> eventHandler = this.m_a;
				EventHandler<DataRequestEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<DataRequestEventArgs> value2 = (EventHandler<DataRequestEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange(ref this.m_a, value2, eventHandler2);
				}
				while ((object)eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<DataRequestEventArgs> eventHandler = this.m_a;
				EventHandler<DataRequestEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<DataRequestEventArgs> value2 = (EventHandler<DataRequestEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange(ref this.m_a, value2, eventHandler2);
				}
				while ((object)eventHandler != eventHandler2);
			}
		}

		public event EventHandler<imageSourceRenderedResultEventArgs> imageSourceRendered
		{
			[CompilerGenerated]
			add
			{
				EventHandler<imageSourceRenderedResultEventArgs> eventHandler = b8;
				EventHandler<imageSourceRenderedResultEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<imageSourceRenderedResultEventArgs> value2 = (EventHandler<imageSourceRenderedResultEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange(ref b8, value2, eventHandler2);
				}
				while ((object)eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<imageSourceRenderedResultEventArgs> eventHandler = b8;
				EventHandler<imageSourceRenderedResultEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<imageSourceRenderedResultEventArgs> value2 = (EventHandler<imageSourceRenderedResultEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange(ref b8, value2, eventHandler2);
				}
				while ((object)eventHandler != eventHandler2);
			}
		}

		public void Dispose()
		{
			GC.Collect();
		}

		public ReadWindow(object selectedBook, string bookId, string userId, int trialPages, BookType bookType, BookManager bookManager, MultiLanquageManager langMng, bool shareMode, string appName)
		{
			a9 = appName;
			this.m_s = selectedBook;
			this.m_u = bookId;
			this.m_v = userId;
			this.m_r = trialPages;
			this.m_t = bookType;
			BookThumbnail bookThumbnail = (BookThumbnail)selectedBook;
			this.m_w = bookThumbnail.vendorId;
			a6 = bookManager;
			a7 = langMng;
			a8 = shareMode;
			bookThumbnail = null;
			this.m_x = new ConfigurationManager(bookManager);
			ak();
			base.Initialized += new EventHandler(b);
			@as = DateTime.Now;
			InitializeComponent();
			q();
			base.Loaded += new RoutedEventHandler(aj);
		}

		private void ak()
		{
			OperatingSystem oSVersion = Environment.OSVersion;
			int major = oSVersion.Version.Major;
			if (major == 5 && oSVersion.Version.Minor != 0)
			{
				a2 = true;
				Console.WriteLine("Windows XP");
			}
		}

		private void aj(object A_0, RoutedEventArgs A_1)
		{
			base.Loaded -= new RoutedEventHandler(aj);
			string sqlCommand = "select pdfPageMode, bookRightsDRM, canPrint from userbook_metadata Where Sno= " + this.m_ab;
			QueryResult queryResult = a6.sqlCommandQuery(sqlCommand);
			if (queryResult.fetchRow())
			{
				aw = queryResult.getInt("pdfPageMode");
				ay = queryResult.getString("bookRightsDRM");
				bb = queryResult.getString("canPrint");
				if (aw == 1)
				{
					a(PageMode.SinglePage);
					FindVisualChildByName<RadioButton>(cg, "PageViewButton").IsChecked = true;
				}
				RadioButton radioButton = FindVisualChildByName<RadioButton>(cg, "ExportButton");
				if (a9.Equals("NTPCReader") || a9.Equals("HyReadCN"))
				{
					radioButton.Visibility = Visibility.Collapsed;
				}
			}
			if (a9.Equals("HyReadCN"))
			{
				FindVisualChildByName<RadioButton>(cg, "ShareButton").Visibility = Visibility.Collapsed;
			}
			sqlCommand = "update userbook_metadata set readtimes = readtimes+1 Where Sno= " + this.m_ab;
			a6.sqlCommandNonQuery(sqlCommand);
			FindVisualChildByName<InkCanvas>(cg, "penMemoCanvas").DefaultDrawingAttributes = this.m_x.loadStrokeSetting();
			ax = this.m_x.isStrokeLine;
			at = new List<Stroke>();
			base.Closing += new CancelEventHandler(c);
			this.m_ai[0] = null;
			this.m_ai[1] = null;
			if (this.m_r > 0)
			{
				ch.Visibility = Visibility.Collapsed;
			}
			else
			{
				ch.Maximum = this.m_aj.allFileList.Count;
				ch.Minimum = 0.0;
				if (!ag())
				{
					a1 = new FileSystemWatcher(this.m_z + "\\HYWEB\\");
					a1.EnableRaisingEvents = true;
					a1.IncludeSubdirectories = true;
					a1.Changed += new FileSystemEventHandler(a);
				}
			}
			s();
			if (this.m_aj.bookType.ToLower().Equals("video"))
			{
				aj();
			}
			GC.Collect();
			DispatcherTimer dispatcherTimer = new DispatcherTimer();
			dispatcherTimer.Tick += new EventHandler(a);
			dispatcherTimer.Start();
		}

		private void a(object A_0, object A_1)
		{
			DispatcherTimer obj = (DispatcherTimer)A_0;
			obj.Tick -= new EventHandler(a);
			obj.Stop();
			string text = this.m_z + "\\HYWEB\\index.zip";
			string text2 = this.m_z + "\\HYWEB\\index";
			try
			{
				if (File.Exists(text) && new FileInfo(text).Length > 0)
				{
					bool flag = false;
					if (Directory.Exists(text2))
					{
						string[] files = Directory.GetFiles(text2);
						for (int i = 0; i < files.Length; i++)
						{
							if (files[i].EndsWith("lck"))
							{
								flag = true;
								break;
							}
						}
					}
					else
					{
						Directory.CreateDirectory(text2);
					}
					if (!flag)
					{
						new ZipTool().unzip(text, text2);
					}
				}
			}
			catch
			{
			}
		}

		private void aj()
		{
			string text = "";
			foreach (ManifestItem manifestItem in this.m_aj.manifestItemList)
			{
				if (manifestItem.mediaType.Equals("application/x-mpegURL"))
				{
					text = manifestItem.href;
					break;
				}
			}
			if (!text.Equals(""))
			{
				HlsPlayer hlsPlayer = new HlsPlayer(base.Title, this.m_u, this.m_v, this.m_z, am, text, this.m_ab, a0, a6);
				hlsPlayer.HlsPlayerDataRequest += new EventHandler<DataRequestEventArgs>(a);
				hlsPlayer.ShowDialog();
				hlsPlayer.HlsPlayerDataRequest -= new EventHandler<DataRequestEventArgs>(a);
				hlsPlayer.Dispose();
				Close();
			}
		}

		private void c(object A_0, EventArgs A_1)
		{
			FindVisualChildByName<Canvas>(cg, "zoomCanvas").Background = null;
			base.Closing -= new CancelEventHandler(c);
			imageSourceRendered -= new EventHandler<imageSourceRenderedResultEventArgs>(a);
			ReadWindow.m_y.Tick -= new EventHandler(a);
			ReadWindow.m_y.Stop();
			ReadWindow.m_y.IsEnabled = false;
			ReadWindow.m_y = null;
			if (this.m_r == 0)
			{
				InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(cg, "penMemoCanvas");
				this.m_x.saveStrokeSetting(inkCanvas.DefaultDrawingAttributes, ax);
				if (a1 != null)
				{
					a1.EnableRaisingEvents = false;
					a1.IncludeSubdirectories = false;
					a1.Changed -= new FileSystemEventHandler(a);
					a1 = null;
				}
				ai();
				ah();
			}
			a(this.m_i);
			a(this.m_j);
			BindingOperations.ClearAllBindings(this);
			BindingOperations.ClearAllBindings(cq);
			List<ThumbnailImageAndPage> list = (List<ThumbnailImageAndPage>)cq.ItemsSource;
			for (int i = 0; i < list.Count; i++)
			{
				list[i].leftImagePath = "";
			}
			if (cq.SelectedIndex > 0)
			{
				list.RemoveAt(cq.SelectedIndex);
			}
			list.Clear();
			BindingOperations.ClearAllBindings(this.m_b);
			BindingOperations.ClearAllBindings(this.m_c);
			BindingOperations.ClearAllBindings(cg);
			this.m_b.Blocks.Clear();
			this.m_c.Blocks.Clear();
			cq.ItemsSource = null;
			cg.Document = null;
			this.m_b = null;
			this.m_c = null;
			this.m_e.Clear();
			this.m_f.Clear();
			this.m_e = null;
			this.m_f = null;
			this.m_k = null;
			this.m_d = null;
			this.m_g = null;
			this.m_h = null;
			this.m_s = null;
			this.m_z = null;
			this.m_aj = null;
			an = null;
			this.m_l = null;
			this.m_ak = null;
			al = null;
			b3 = null;
			this.m_x = null;
		}

		private void ai()
		{
			string sqlCommand = "";
			if (b0.Equals(PageMode.SinglePage))
			{
				sqlCommand = "update userbook_metadata set pdfPageMode = 1 Where Sno= " + this.m_ab;
			}
			else if (b0.Equals(PageMode.DoublePage))
			{
				sqlCommand = "update userbook_metadata set pdfPageMode = 2 Where Sno= " + this.m_ab;
			}
			a6.sqlCommandNonQuery(sqlCommand);
		}

		private void ah()
		{
			int num = 0;
			if (b0.Equals(PageMode.SinglePage))
			{
				num = this.m_p;
			}
			else if (b0.Equals(PageMode.DoublePage))
			{
				ReadPagePair readPagePair = this.m_j[this.m_p];
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
			if (aq.ContainsKey(machineName))
			{
				lastPageData = aq[machineName];
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
			a6.saveLastviewPage(this.m_ab, flag, lastPageData);
		}

		private void a(Dictionary<int, ReadPagePair> A_0)
		{
			int count = A_0.Count;
			for (int i = 0; i < count; i++)
			{
				ReadPagePair readPagePair = A_0[i];
				if (readPagePair.leftImageSource != null)
				{
					readPagePair.leftImageSource = null;
					readPagePair.decodedPDFPages = new byte[2][];
				}
			}
			A_0.Clear();
			A_0 = null;
		}

		private bool ag()
		{
			int count = this.m_aj.allFileList.Count;
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				if (File.Exists(this.m_z + "\\HYWEB\\" + this.m_aj.allFileList[i]))
				{
					num++;
				}
			}
			string[] files = Directory.GetFiles(this.m_z + "\\HYWEB\\", "*.pdf");
			if (this.m_t.Equals(BookType.HEJ))
			{
				files = Directory.GetFiles(this.m_z + "\\HYWEB\\", "*.jpg");
			}
			for (int j = 0; j < files.Length; j++)
			{
				for (int k = 0; k < this.m_aj.LImgList.Count; k++)
				{
					if (files[j].Substring(files[j].LastIndexOf("\\") + 1).Equals(this.m_aj.LImgList[k].path.Replace("HYWEB\\", "")) && !this.m_e[k].isDownloaded)
					{
						this.m_e[k].isDownloaded = true;
					}
				}
			}
			if (files.Length.Equals(this.m_aj.LImgList.Count))
			{
				bc = true;
			}
			for (int l = 0; l < bd.Count; l++)
			{
				for (int m = 0; m < bd[l].mediaList.Count; m++)
				{
					if (File.Exists(bd[l].mediaList[m].mediaSourcePath) && !bd[l].mediaList[m].downloadStatus)
					{
						bd[l].mediaList[m].downloadStatus = true;
					}
				}
			}
			files = null;
			if (!count.Equals(num))
			{
				ch.Value = num;
				return false;
			}
			ch.Visibility = Visibility.Collapsed;
			return true;
		}

		private void a(object A_0, FileSystemEventArgs A_1)
		{
			c c = new c();
			c.c = this;
			if (this.m_aj == null)
			{
				return;
			}
			string fileName = Path.GetFileName(A_1.Name.Replace(".tmp", ""));
			c.b = 0;
			int count = this.m_aj.LImgList.Count;
			for (int i = 0; i < count; i++)
			{
				if (!this.m_e[i].isDownloaded)
				{
					if (this.m_aj.LImgList[i].path.Contains(fileName))
					{
						this.m_e[i].isDownloaded = true;
						c.b++;
						break;
					}
				}
				else
				{
					c.b++;
				}
			}
			int num = 0;
			int count2 = bd.Count;
			for (int j = 0; j < count2; j++)
			{
				int count3 = bd[j].mediaList.Count;
				num += count3;
				for (int k = 0; k < count3; k++)
				{
					if (!bd[j].mediaList[k].downloadStatus)
					{
						if (Path.GetFileName(bd[j].mediaList[k].mediaSourcePath) == fileName)
						{
							bd[j].mediaList[k].downloadStatus = true;
							c.b++;
							break;
						}
					}
					else
					{
						c.b++;
					}
				}
			}
			c.a = count + num;
			try
			{
				base.Dispatcher.Invoke(new Action(c.d), new object[0]);
			}
			catch
			{
			}
		}

		private void b(object A_0, EventArgs A_1)
		{
			ac();
			am = z();
			byte[] key = am;
			if (aa())
			{
				List<MediaList> mediaList = this.m_ak.getMediaList(key);
				bd = new ObservableCollection<MediaList>(mediaList);
			}
			key = null;
			ab();
			a(out DpiX, out DpiY);
			this.m_ac = Convert.ToInt32(Math.Max(DpiX, DpiY));
			y();
			af();
			base.Initialized -= new EventHandler(b);
		}

		private void a(out double A_0, out double A_1)
		{
			System.Windows.Media.Matrix transformToDevice = PresentationSource.FromVisual(Application.Current.MainWindow).CompositionTarget.TransformToDevice;
			A_0 = transformToDevice.M11 * 96.0;
			A_1 = transformToDevice.M22 * 96.0;
		}

		private void af()
		{
			if (this.m_aj.direction.Equals("right"))
			{
				this.m_b.FlowDirection = FlowDirection.RightToLeft;
				this.m_c.FlowDirection = FlowDirection.RightToLeft;
			}
			else
			{
				this.m_b.FlowDirection = FlowDirection.LeftToRight;
				this.m_c.FlowDirection = FlowDirection.LeftToRight;
			}
		}

		private void ae()
		{
			getBookRightsAsync(this.m_u);
			string machineName = Environment.MachineName;
			aq = a6.getLastViewPageObj(this.m_ab);
			if (aq.ContainsKey(machineName))
			{
				if (aq[machineName].index > 0)
				{
					int num = aw;
					if (num.Equals(1))
					{
						c(aq[machineName].index);
					}
					else if (num.Equals(2))
					{
						int a_ = d(aq[machineName].index);
						c(a_);
					}
				}
				else
				{
					FindVisualChildByName<Canvas>(cg, "zoomCanvas").Background = null;
				}
			}
			else
			{
				FindVisualChildByName<Canvas>(cg, "zoomCanvas").Background = null;
			}
			ap = a6.getStrokesDics(this.m_ab);
			an = a6.getBookMarkDics(this.m_ab);
			ao = a6.getBookNoteDics(this.m_ab);
			if (!((BookThumbnail)this.m_s).vendorId.Equals("free"))
			{
				az = true;
			}
		}

		private void ad()
		{
			if (File.Exists(this.m_z + "\\HYWEB\\index.zip"))
			{
				FindVisualChildByName<RadioButton>(cg, "SearchButton").Visibility = Visibility.Visible;
			}
			if (File.Exists(this.m_z + "\\HYWEB\\toc.ncx"))
			{
				byte[] aeskey = am;
				string sourcePath = this.m_z + "\\HYWEB\\toc.ncx";
				ba = new TocButtonController(this.m_aj.LImgList);
				XmlDocument xmlDocument = new XmlDocument();
				using (MemoryStream memoryStream = this.m_d.fileAESDecode(sourcePath, aeskey, false))
				{
					RadioButton radioButton = FindVisualChildByName<RadioButton>(cg, "TocButton");
					try
					{
						xmlDocument.Load(memoryStream);
						memoryStream.Close();
						radioButton.Visibility = Visibility.Visible;
						ba.SetTocXmlDocument(xmlDocument);
					}
					catch
					{
						memoryStream.Close();
						radioButton.Visibility = Visibility.Collapsed;
					}
				}
			}
			else
			{
				FindVisualChildByName<RadioButton>(cg, "TocButton").Visibility = Visibility.Collapsed;
			}
			if (bd[0].mediaList.Count > 0 || bd[1].mediaList.Count > 0 || bd[2].mediaList.Count > 0 || bd[3].mediaList.Count > 0)
			{
				FindVisualChildByName<RadioButton>(cg, "MediaListButton").Visibility = Visibility.Visible;
			}
			if (this.m_aj.tocPageIndex.Equals(0))
			{
				FindVisualChildByName<RadioButton>(cg, "ContentButton").Visibility = Visibility.Collapsed;
			}
			if (this.m_r > 0)
			{
				FindVisualChildByName<RadioButton>(cg, "PenMemoButton").Visibility = Visibility.Collapsed;
				FindVisualChildByName<StackPanel>(cg, "MediasStackPanel").Visibility = Visibility.Collapsed;
				cr.Visibility = Visibility.Collapsed;
			}
			if (this.m_ah)
			{
				RadioButton radioButton2 = FindVisualChildByName<RadioButton>(cg, "PrintButton");
				if (radioButton2.Visibility.Equals(Visibility.Collapsed))
				{
					radioButton2.Visibility = Visibility.Visible;
				}
			}
			BookThumbnail bookThumbnail = (BookThumbnail)this.m_s;
			base.Title = bookThumbnail.title;
			bookThumbnail = null;
		}

		public void getBookRightsAsync(string bookId)
		{
			bool flag = false;
			if (File.Exists(this.m_z + "\\HYWEB\\rights.xml"))
			{
				string path = this.m_z + "\\HYWEB\\rights.xml";
				XmlDocument xmlDocument = new XmlDocument();
				string xml = File.ReadAllText(path);
				try
				{
					xmlDocument.LoadXml(xml);
					string innerText = xmlDocument.InnerText;
					innerText = this.m_d.stringDecode(innerText, am, true);
					xmlDocument.LoadXml(innerText);
					foreach (XmlNode item in xmlDocument.SelectNodes("/drm/functions"))
					{
						if (item.InnerText.Contains("canPrint"))
						{
							this.m_ah = true;
							break;
						}
					}
				}
				catch
				{
					flag = true;
				}
			}
			if (flag && ay != null && ay != "")
			{
				flag = false;
				try
				{
					XmlDocument xmlDocument2 = new XmlDocument();
					string xml2 = this.m_d.stringDecode(ay, true);
					xmlDocument2.LoadXml(xml2);
					foreach (XmlNode item2 in xmlDocument2.SelectNodes("/drm/functions"))
					{
						if (item2.InnerText.Contains("canPrint"))
						{
							this.m_ah = true;
							break;
						}
					}
				}
				catch
				{
					flag = true;
				}
			}
			if (flag)
			{
				this.m_ah = (bb.ToUpper().Equals("TRUE") ? true : false);
			}
		}

		private void ac()
		{
			BookThumbnail bookThumbnail = (BookThumbnail)this.m_s;
			this.m_d = new CACodecTools();
			if (this.m_r > 0)
			{
				ar = new LocalFilesManager(a9, "tryread", "tryread", "tryread");
				this.m_z = ar.getUserBookPath(bookThumbnail.bookID, this.m_t.GetHashCode(), bookThumbnail.owner);
				this.m_w = bookThumbnail.vendorId;
				this.m_ab = -1;
			}
			else
			{
				ar = new LocalFilesManager(a9, bookThumbnail.vendorId, bookThumbnail.colibId, bookThumbnail.userId);
				this.m_z = ar.getUserBookPath(bookThumbnail.bookID, this.m_t.GetHashCode(), bookThumbnail.owner);
				this.m_ab = a6.getUserBookSno(bookThumbnail.vendorId, bookThumbnail.colibId, bookThumbnail.userId, bookThumbnail.bookID);
			}
			bookThumbnail = null;
			ar = null;
		}

		private void ab()
		{
			this.m_k = new TransformGroup();
			ScaleTransform value = new ScaleTransform();
			this.m_k.Children.Add(value);
			TranslateTransform value2 = new TranslateTransform();
			this.m_k.Children.Add(value2);
			this.m_l = new TransformGroup();
			ScaleTransform value3 = new ScaleTransform();
			this.m_l.Children.Add(value3);
			TranslateTransform value4 = new TranslateTransform();
			this.m_l.Children.Add(value4);
			value = null;
			value2 = null;
			value3 = null;
			value4 = null;
		}

		private bool aa()
		{
			HEJMetadataReader hEJMetadataReader = new HEJMetadataReader(this.m_z);
			this.m_aj = hEJMetadataReader.getBookMetadata(this.m_z + "\\book.xml", this.m_r, a9, this.m_w);
			this.m_ak = new PageInfoManager(this.m_z, this.m_aj);
			return true;
		}

		private byte[] z()
		{
			byte[] array = new byte[1];
			string encryptionFile = this.m_z + "\\HYWEB\\encryption.xml";
			string str = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\" + a9;
			BookThumbnail bookThumbnail = (BookThumbnail)this.m_s;
			string text = a6.bookProviders[bookThumbnail.vendorId].loginUserPassword;
			if (bookThumbnail.vendorId.Equals("free") || this.m_r > 0)
			{
				text = "free";
			}
			if (a9.Equals("HyReadCN") && (this.m_r > 0 || text == "free"))
			{
				text = "MMF2wqY8fNDXT";
			}
			str = ((this.m_r <= 0) ? (str + "\\" + this.m_d.CreateMD5Hash(bookThumbnail.vendorId + bookThumbnail.colibId + bookThumbnail.userId).ToUpper()) : (str + "\\" + this.m_d.CreateMD5Hash("tryreadtryreadtryread").ToUpper()));
			string cipherValue = getCipherValue(encryptionFile);
			string p12Path = str + "\\HyHDWL.ps2";
			if (text == null || text == "")
			{
				try
				{
					text = a6.bookProviders[bookThumbnail.colibId].loginUserPassword;
				}
				catch
				{
					text = "";
				}
			}
			text = this.m_d.CreateMD5Hash(text);
			text += ":";
			return this.m_d.encryptStringDecode2ByteArray(cipherValue, p12Path, text, true);
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
				result = xmlDocument.GetElementsByTagName("enc:CipherValue")[0].InnerText;
				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine("getCipherValue error=" + ex.ToString());
				return result;
			}
		}

		private Bitmap a(string A_0, byte[] A_1, int A_2, int A_3, float A_4, int A_5, Border A_6, bool A_7)
		{
			System.Drawing.Color white = System.Drawing.Color.White;
			Bitmap aBitmap = null;
			try
			{
				if (this.m_ai[A_5] == null)
				{
					this.m_ai[A_5] = this.m_d.fileAESDecode(A_0, A_1);
				}
			}
			catch (Exception ex)
			{
				this.m_ai[A_5] = null;
				throw ex;
			}
			try
			{
				PDFDoc pDFDoc = new PDFDoc();
				pDFDoc.Init("PVD20-M4IRG-QYZK9-MNJ2U-DFTK1-MAJ4L", "PDFX3$Henry$300604_Allnuts#");
				pDFDoc.OpenFromMemory(this.m_ai[A_5], (uint)this.m_ai[A_5].Length, 0);
				PXCV_Lib36.PXV_CommonRenderParameters aCommonRenderParams = a(pDFDoc, A_3, A_2, A_4, 0, 0, A_6, A_7);
				pDFDoc.DrawPageToDIBSection(IntPtr.Zero, A_2, white, aCommonRenderParams, out aBitmap);
				pDFDoc.ReleasePageCachedData(A_2, 1);
				pDFDoc.Delete();
				return aBitmap;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		private Bitmap a(string A_0, byte[] A_1, int A_2, int A_3, float A_4, int A_5, bool A_6)
		{
			System.Drawing.Color white = System.Drawing.Color.White;
			Bitmap aBitmap = null;
			try
			{
				if (this.m_ai[A_5] == null)
				{
					this.m_ai[A_5] = this.m_d.fileAESDecode(A_0, A_1);
				}
			}
			catch (Exception ex)
			{
				this.m_ai[A_5] = null;
				throw ex;
			}
			try
			{
				PDFDoc pDFDoc = new PDFDoc();
				pDFDoc.Init("PVD20-M4IRG-QYZK9-MNJ2U-DFTK1-MAJ4L", "PDFX3$Henry$300604_Allnuts#");
				pDFDoc.OpenFromMemory(this.m_ai[A_5], (uint)this.m_ai[A_5].Length, 0);
				PXCV_Lib36.PXV_CommonRenderParameters aCommonRenderParams = a(pDFDoc, A_3, A_2, A_4, 0, 0, A_6);
				pDFDoc.DrawPageToDIBSection(IntPtr.Zero, A_2, white, aCommonRenderParams, out aBitmap);
				pDFDoc.ReleasePageCachedData(A_2, 1);
				pDFDoc.Delete();
				return aBitmap;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		private PXCV_Lib36.PXV_CommonRenderParameters a(PDFDoc A_0, int A_1, int A_2, float A_3, int A_4, int A_5, Border A_6, bool A_7)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(PXCV_Helper.RECT)));
			IntPtr intPtr2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(PXCV_Helper.RECT)));
			System.Drawing.Point point = new System.Drawing.Point(A_4, A_5);
			System.Drawing.Size empty = System.Drawing.Size.Empty;
			PXCV_Helper.RECT rECT = default(PXCV_Helper.RECT);
			PXCV_Helper.RECT rECT2 = default(PXCV_Helper.RECT);
			PXCV_Lib36.PXV_CommonRenderParameters result = default(PXCV_Lib36.PXV_CommonRenderParameters);
			PageDimension pageDimension = default(PageDimension);
			A_0.GetPageDimensions(A_2, out pageDimension.w, out pageDimension.h);
			double num = A_6.ActualHeight / 96.0 * (double)A_1;
			double num2 = A_6.ActualWidth / 96.0 * (double)A_1;
			if (this.m_af == 0)
			{
				empty.Width = (int)(pageDimension.w / 72.0 * (double)A_1 * (double)A_3);
				empty.Height = (int)(pageDimension.h / 72.0 * (double)A_1 * (double)A_3);
				double num3 = num2 / num;
				double num4 = 0.0;
				num4 = ((!A_7) ? ((double)(empty.Width * 2) / (double)empty.Height) : ((double)empty.Width / (double)empty.Height));
				if ((double)empty.Width < num2 && (double)empty.Height < num)
				{
					double num5;
					double num6;
					if (num4 > num3)
					{
						num5 = num2 / 2.0;
						this.m_ae = num5 / (double)empty.Width;
						num6 = Math.Round(this.m_ae * (double)empty.Height, 2);
					}
					else
					{
						num6 = num;
						this.m_ae = num6 / (double)empty.Height;
						num5 = Math.Round(this.m_ae * (double)empty.Width, 2);
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
						this.m_ae = num7 / (double)empty.Width;
						num8 = Math.Round(this.m_ae * (double)empty.Height, 2);
					}
					else
					{
						num8 = num;
						this.m_ae = num8 / (double)empty.Height;
						num7 = Math.Round(this.m_ae * (double)empty.Width, 2);
					}
					empty.Width = (int)num7;
					empty.Height = (int)num8;
				}
			}
			else
			{
				empty.Width = (int)(pageDimension.w / 72.0 * (double)A_1 * (double)A_3 * this.m_ae);
				empty.Height = (int)(pageDimension.h / 72.0 * (double)A_1 * (double)A_3 * this.m_ae);
			}
			rECT.left = -point.X;
			rECT.top = -point.Y;
			rECT.right = rECT.left + empty.Width;
			rECT.bottom = rECT.top + empty.Height;
			rECT2.left = 0;
			rECT2.top = 0;
			if (this.m_af == 0)
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

		private PXCV_Lib36.PXV_CommonRenderParameters a(PDFDoc A_0, int A_1, int A_2, float A_3, int A_4, int A_5, bool A_6)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(PXCV_Helper.RECT)));
			IntPtr intPtr2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(PXCV_Helper.RECT)));
			System.Drawing.Point point = new System.Drawing.Point(A_4, A_5);
			System.Drawing.Size empty = System.Drawing.Size.Empty;
			PXCV_Helper.RECT rECT = default(PXCV_Helper.RECT);
			PXCV_Helper.RECT rECT2 = default(PXCV_Helper.RECT);
			PXCV_Lib36.PXV_CommonRenderParameters result = default(PXCV_Lib36.PXV_CommonRenderParameters);
			PageDimension pageDimension = default(PageDimension);
			A_0.GetPageDimensions(A_2, out pageDimension.w, out pageDimension.h);
			Border border = e();
			double num = border.ActualHeight / 96.0 * (double)A_1;
			double num2 = border.ActualWidth / 96.0 * (double)A_1;
			if (this.m_af == 0)
			{
				empty.Width = (int)(pageDimension.w / 72.0 * (double)A_1 * (double)A_3);
				empty.Height = (int)(pageDimension.h / 72.0 * (double)A_1 * (double)A_3);
				double num3 = num2 / num;
				double num4 = 0.0;
				num4 = ((!A_6) ? ((double)(empty.Width * 2) / (double)empty.Height) : ((double)empty.Width / (double)empty.Height));
				if ((double)empty.Width < num2 && (double)empty.Height < num)
				{
					double num5;
					double num6;
					if (num4 > num3)
					{
						num5 = num2 / 2.0;
						this.m_ae = num5 / (double)empty.Width;
						num6 = Math.Round(this.m_ae * (double)empty.Height, 2);
					}
					else
					{
						num6 = num;
						this.m_ae = num6 / (double)empty.Height;
						num5 = Math.Round(this.m_ae * (double)empty.Width, 2);
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
						this.m_ae = num7 / (double)empty.Width;
						num8 = Math.Round(this.m_ae * (double)empty.Height, 2);
					}
					else
					{
						num8 = num;
						this.m_ae = num8 / (double)empty.Height;
						num7 = Math.Round(this.m_ae * (double)empty.Width, 2);
					}
					empty.Width = (int)num7;
					empty.Height = (int)num8;
				}
			}
			else
			{
				empty.Width = (int)(pageDimension.w / 72.0 * (double)A_1 * (double)A_3 * this.m_ae);
				empty.Height = (int)(pageDimension.h / 72.0 * (double)A_1 * (double)A_3 * this.m_ae);
			}
			rECT.left = -point.X;
			rECT.top = -point.Y;
			rECT.right = rECT.left + empty.Width;
			rECT.bottom = rECT.top + empty.Height;
			rECT2.left = 0;
			rECT2.top = 0;
			if (this.m_af == 0)
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

		private BitmapSource a(Bitmap A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("bitmap");
			}
			return Imaging.CreateBitmapSourceFromHBitmap(A_0.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
		}

		private MemoryStream a(string A_0, byte[] A_1, int A_2, int A_3, float A_4)
		{
			Mutex mutex = new Mutex(be, "LoadMutex", out bf);
			if (!(be & bf))
			{
				mutex.WaitOne();
			}
			MemoryStream memoryStream = new MemoryStream();
			MemoryStream memoryStream2 = this.m_d.fileAESDecode(A_0, A_1, false);
			int num = new byte[memoryStream2.Length].Length;
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
				NativeMethods.pdfPageSize(A_3, A_4, intPtr, num, intPtr5, intPtr6, A_2);
				int x = Marshal.ReadInt32(intPtr5);
				int y = Marshal.ReadInt32(intPtr6);
				Marshal.FreeHGlobal(intPtr5);
				Marshal.FreeHGlobal(intPtr6);
				NativeMethods.pdfLoadFromMemoryPartial(A_3, A_4, intPtr, num, intPtr3, intPtr2, 0, 0, x, y, A_2);
				intPtr4 = (IntPtr)Marshal.ReadInt32(intPtr3);
				num2 = Marshal.ReadInt32(intPtr2);
				byte[] array = new byte[num2];
				Marshal.Copy(intPtr4, array, 0, num2);
				memoryStream.Write(array, 0, array.Length);
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

		private System.Windows.Controls.Image c(CACodecTools A_0, byte[] A_1, string A_2, float A_3)
		{
			System.Windows.Controls.Image image = new System.Windows.Controls.Image();
			image.Source = b(A_0, A_1, A_2, A_3);
			image.Stretch = Stretch.Uniform;
			image.Margin = new Thickness(this.m_q);
			image.Name = "imageInReader";
			image.RenderTransform = this.m_k;
			image.MouseLeftButtonDown += new MouseButtonEventHandler(b);
			return image;
		}

		private BitmapImage b(CACodecTools A_0, byte[] A_1, string A_2, float A_3)
		{
			BitmapImage bitmapImage = new BitmapImage();
			Bitmap bitmap = a(A_2, A_1, 0, this.m_ac, A_3, 0, true);
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

		private void a(CACodecTools A_0, byte[] A_1, string A_2, float A_3, int A_4, Border A_5)
		{
			BitmapImage bitmapImage = null;
			Bitmap bitmap = null;
			try
			{
				bitmapImage = new BitmapImage();
				bitmap = a(A_2, A_1, 0, this.m_ac, A_3, 0, A_5, true);
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
				}
			}
			catch
			{
				bitmap = null;
				bitmapImage = null;
			}
			EventHandler<imageSourceRenderedResultEventArgs> eventHandler = b8;
			if (eventHandler != null)
			{
				eventHandler(this, new imageSourceRenderedResultEventArgs(bitmapImage, A_4, A_3));
			}
		}

		private System.Windows.Controls.Image b(CACodecTools A_0, byte[] A_1, string A_2)
		{
			return c(A_0, A_1, A_2, this.m_ad);
		}

		private BitmapImage b(CACodecTools A_0, byte[] A_1, string A_2, string A_3, float A_4)
		{
			BitmapImage bitmapImage = new BitmapImage();
			try
			{
				Bitmap bitmap = a(A_2, A_1, 0, this.m_ac, A_4, 0, false);
				Bitmap bitmap2 = a(A_3, A_1, 0, this.m_ac, A_4, 1, false);
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

		private void a(CACodecTools A_0, byte[] A_1, string A_2, string A_3, float A_4, int A_5, Border A_6)
		{
			BitmapImage bitmapImage = new BitmapImage();
			Bitmap bitmap = null;
			Bitmap bitmap2 = null;
			Bitmap bitmap3 = null;
			bool a_ = true;
			if (b0.Equals(PageMode.DoublePage))
			{
				a_ = false;
			}
			bool flag = false;
			EventHandler<imageSourceRenderedResultEventArgs> eventHandler = b8;
			try
			{
				if (A_2.Contains("tryPageEndLarge"))
				{
					if (A_2.Contains("tryPageEndLarge.pdf"))
					{
						A_2 = A_2.Replace("tryPageEndLarge.pdf", "tryPageEnd.jpg");
					}
					else if (A_2.Contains("tryPageEndLargeNTPC.pdf"))
					{
						A_2 = A_2.Replace("tryPageEndLargeNTPC.pdf", "tryPageEndNTPC.jpg");
					}
					flag = true;
					BitmapImage source = new BitmapImage(new Uri(A_2, UriKind.RelativeOrAbsolute));
					using (MemoryStream stream = new MemoryStream())
					{
						BmpBitmapEncoder bmpBitmapEncoder = new BmpBitmapEncoder();
						bmpBitmapEncoder.Frames.Add(BitmapFrame.Create(source));
						bmpBitmapEncoder.Save(stream);
						bitmap = new Bitmap(stream);
					}
				}
				else
				{
					bitmap = a(A_2, A_1, 0, this.m_ac, A_4, 0, A_6, a_);
				}
			}
			catch (Exception)
			{
				bitmap = null;
				bitmap2 = null;
				bitmap3 = null;
				bitmapImage = null;
				if (eventHandler != null)
				{
					eventHandler(this, new imageSourceRenderedResultEventArgs(bitmapImage, A_5, A_4));
				}
				return;
			}
			try
			{
				if (A_3.Contains("tryPageEndLarge"))
				{
					if (A_3.Contains("tryPageEndLarge.pdf"))
					{
						A_3 = A_3.Replace("tryPageEndLarge.pdf", "tryPageEnd.jpg");
					}
					else if (A_3.Contains("tryPageEndLargeNTPC.pdf"))
					{
						A_3 = A_3.Replace("tryPageEndLargeNTPC.pdf", "tryPageEndNTPC.jpg");
					}
					flag = true;
					BitmapImage source2 = new BitmapImage(new Uri(A_3, UriKind.RelativeOrAbsolute));
					using (MemoryStream stream2 = new MemoryStream())
					{
						BmpBitmapEncoder bmpBitmapEncoder2 = new BmpBitmapEncoder();
						bmpBitmapEncoder2.Frames.Add(BitmapFrame.Create(source2));
						bmpBitmapEncoder2.Save(stream2);
						bitmap2 = new Bitmap(stream2);
					}
				}
				else
				{
					bitmap2 = a(A_3, A_1, 0, this.m_ac, A_4, 1, A_6, a_);
				}
			}
			catch (Exception)
			{
				bitmap = null;
				bitmap2 = null;
				bitmap3 = null;
				bitmapImage = null;
				if (eventHandler != null)
				{
					eventHandler(this, new imageSourceRenderedResultEventArgs(bitmapImage, A_5, A_4));
				}
				return;
			}
			try
			{
				int width = Convert.ToInt32(bitmap.Width + bitmap2.Width);
				int height = Convert.ToInt32(Math.Max(bitmap.Height, bitmap2.Height));
				if (flag)
				{
					width = Math.Min(bitmap.Width, bitmap2.Width) * 2;
					height = Math.Min(bitmap.Height, bitmap2.Height);
					if (bitmap.Width > bitmap2.Width)
					{
						bitmap = ResizeImage(bitmap, new System.Drawing.Size(bitmap2.Width, bitmap2.Height));
						width = bitmap2.Width * 2;
					}
					else
					{
						bitmap2 = ResizeImage(bitmap2, new System.Drawing.Size(bitmap.Width, bitmap.Height));
						width = bitmap.Width * 2;
					}
				}
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
			}
			catch
			{
				bitmap = null;
				bitmap2 = null;
				bitmap3 = null;
				bitmapImage = null;
			}
			if (eventHandler != null)
			{
				eventHandler(this, new imageSourceRenderedResultEventArgs(bitmapImage, A_5, A_4));
			}
		}

		public static Bitmap ResizeImage(Bitmap imgToResize, System.Drawing.Size size)
		{
			try
			{
				Bitmap bitmap = new Bitmap(size.Width, size.Height);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
					graphics.DrawImage(imgToResize, 0, 0, size.Width, size.Height);
				}
				return bitmap;
			}
			catch
			{
			}
			return null;
		}

		private BitmapImage a(CACodecTools A_0, byte[] A_1, string A_2, float A_3)
		{
			BitmapImage bitmapImage = new BitmapImage();
			try
			{
				using (MemoryStream memoryStream = A_0.fileAESDecode(A_2, A_1, false))
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
				throw ex;
			}
		}

		private System.Windows.Controls.Image a(CACodecTools A_0, byte[] A_1, string A_2)
		{
			System.Windows.Controls.Image image = new System.Windows.Controls.Image();
			try
			{
				image.Source = a(A_0, A_1, A_2, this.m_ad);
				image.Stretch = Stretch.Uniform;
				image.Margin = new Thickness(this.m_q);
				image.Name = "imageInReader";
				image.RenderTransform = this.m_k;
				image.MouseLeftButtonDown += new MouseButtonEventHandler(b);
				return image;
			}
			catch
			{
				return image;
			}
		}

		private BitmapImage a(CACodecTools A_0, byte[] A_1, string A_2, string A_3, float A_4)
		{
			BitmapImage bitmapImage = new BitmapImage();
			try
			{
				using (MemoryStream memoryStream = A_0.fileAESDecode(A_2, A_1, false))
				{
					using (MemoryStream memoryStream2 = A_0.fileAESDecode(A_3, A_1, false))
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
				throw ex;
			}
		}

		private System.Windows.Controls.Image a(CACodecTools A_0, byte[] A_1, string A_2, string A_3)
		{
			System.Windows.Controls.Image image = new System.Windows.Controls.Image();
			try
			{
				image.Source = a(A_0, A_1, A_2, A_3, this.m_ad);
				image.Stretch = Stretch.Uniform;
				image.Margin = new Thickness(this.m_q);
				image.Name = "imageInReader";
				image.RenderTransform = this.m_k;
				image.MouseLeftButtonDown += new MouseButtonEventHandler(b);
				return image;
			}
			catch
			{
				return image;
			}
		}

		private int e(int A_0)
		{
			A_0 = ((A_0 != 0) ? ((A_0 != this.m_c.Blocks.Count - 1) ? (A_0 * 2) : (this.m_b.Blocks.Count - 1)) : 0);
			return A_0;
		}

		private int d(int A_0)
		{
			A_0 = ((A_0 != 0) ? ((A_0 == this.m_b.Blocks.Count - 1) ? (this.m_c.Blocks.Count - 1) : ((A_0 % 2 != 1) ? (A_0 / 2) : ((A_0 + 1) / 2))) : 0);
			return A_0;
		}

		private bool y()
		{
			if (this.m_aj != null)
			{
				this.m_c = new FlowDocument();
				this.m_b = new FlowDocument();
				this.m_e = new List<ThumbnailImageAndPage>();
				this.m_g = new List<ImageStatus>();
				this.m_i = new Dictionary<int, ReadPagePair>();
				for (int i = 0; i < this.m_aj.SImgList.Count; i++)
				{
					try
					{
						string a_ = this.m_z + "\\" + this.m_aj.SImgList[i].path;
						if (this.m_aj.SImgList[i].path.Contains("tryPageEnd"))
						{
							a_ = this.m_aj.SImgList[i].path;
						}
						a(this.m_aj.LImgList[i].pageNum, a_, "", this.m_e, this.m_g, this.m_b);
						string leftImgPath = this.m_z + "\\" + this.m_aj.LImgList[i].path;
						if (this.m_aj.LImgList[i].path.Contains("tryPageEnd"))
						{
							leftImgPath = this.m_aj.LImgList[i].path;
						}
						ReadPagePair value = new ReadPagePair(i, -1, leftImgPath, "", this.m_aj.LImgList[i].pageId, "", this.m_ac);
						if (!this.m_i.ContainsKey(i))
						{
							this.m_i.Add(i, value);
						}
					}
					catch (Exception arg)
					{
						Console.WriteLine("Exception: {0}, From getHEJThumbnailAndPage, Single", arg);
					}
				}
				this.m_f = new List<ThumbnailImageAndPage>();
				this.m_h = new List<ImageStatus>();
				this.m_j = new Dictionary<int, ReadPagePair>();
				string text = "";
				string text2 = "";
				for (int j = 0; j < this.m_aj.manifestItemList.Count; j++)
				{
					if (this.m_aj.manifestItemList[j].id.Equals("cover") || this.m_aj.manifestItemList[j].id.Equals("backcover"))
					{
						try
						{
							if (this.m_aj.manifestItemList[j].href.StartsWith("thumbs/"))
							{
								this.m_aj.manifestItemList[j].href = this.m_aj.manifestItemList[j].href.Replace("thumbs/", "");
							}
							string text3 = this.m_z + "\\HYWEB\\thumbs\\" + this.m_aj.manifestItemList[j].href;
							if (this.m_aj.manifestItemList[j].id.Equals("cover"))
							{
								text = text3;
							}
							else if (this.m_aj.manifestItemList[j].id.Equals("backcover"))
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
				for (int k = 0; k < this.m_aj.SImgList.Count; k++)
				{
					try
					{
						if ((this.m_z + "\\" + this.m_aj.SImgList[k].path).Equals(text))
						{
							a(this.m_aj.LImgList[k].pageNum, text, "", this.m_f, this.m_h, this.m_c);
							string leftImgPath2 = this.m_z + "\\" + this.m_aj.LImgList[k].path;
							if (this.m_aj.LImgList[k].path.Contains("tryPageEnd"))
							{
								leftImgPath2 = this.m_aj.LImgList[k].path;
							}
							ReadPagePair value2 = new ReadPagePair(0, -1, leftImgPath2, "", this.m_aj.LImgList[k].pageId, "", this.m_ac);
							if (!this.m_j.ContainsKey(0))
							{
								this.m_j.Add(0, value2);
							}
						}
						else if ((this.m_z + "\\" + this.m_aj.SImgList[k].path).Equals(text2))
						{
							a(this.m_aj.LImgList[k].pageNum, text2, "", this.m_f, this.m_h, this.m_c);
							int key = (k + 1) / 2;
							string leftImgPath3 = this.m_z + "\\" + this.m_aj.LImgList[k].path;
							if (this.m_aj.LImgList[k].path.Contains("tryPageEnd"))
							{
								leftImgPath3 = this.m_aj.LImgList[k].path;
							}
							ReadPagePair value3 = new ReadPagePair(k, -1, leftImgPath3, "", this.m_aj.LImgList[k].pageId, "", this.m_ac);
							if (!this.m_j.ContainsKey(key))
							{
								this.m_j.Add(key, value3);
							}
						}
						else if (k % 2 == 1)
						{
							if (k + 1 == this.m_aj.SImgList.Count)
							{
								string a_2 = this.m_z + "\\" + this.m_aj.SImgList[k].path;
								if (this.m_aj.SImgList[k].path.Contains("tryPageEnd"))
								{
									a_2 = this.m_aj.SImgList[k].path;
								}
								a(this.m_aj.LImgList[k].pageNum, a_2, "", this.m_f, this.m_h, this.m_c);
								int key2 = (k + 1) / 2;
								string leftImgPath4 = this.m_z + "\\" + this.m_aj.LImgList[k].path;
								if (this.m_aj.LImgList[k].path.Contains("tryPageEnd"))
								{
									leftImgPath4 = this.m_aj.LImgList[k].path;
								}
								ReadPagePair value4 = new ReadPagePair(k, -1, leftImgPath4, "", this.m_aj.LImgList[k].pageId, "", this.m_ac);
								if (!this.m_j.ContainsKey(key2))
								{
									this.m_j.Add(key2, value4);
								}
								break;
							}
							string a_3 = this.m_z + "\\" + this.m_aj.SImgList[k].path;
							string a_4 = this.m_z + "\\" + this.m_aj.SImgList[k + 1].path;
							if (this.m_aj.SImgList[k].path.Contains("tryPageEnd"))
							{
								a_3 = this.m_aj.SImgList[k].path;
							}
							if (this.m_aj.SImgList[k + 1].path.Contains("tryPageEnd"))
							{
								a_4 = this.m_aj.SImgList[k + 1].path;
							}
							string a_5 = this.m_aj.LImgList[k].pageNum + "-" + this.m_aj.LImgList[k + 1].pageNum;
							int num = k;
							int num2 = k + 1;
							int key3 = num2 / 2;
							if (this.m_aj.direction.Equals("right"))
							{
								a_3 = this.m_z + "\\" + this.m_aj.SImgList[k + 1].path;
								a_4 = this.m_z + "\\" + this.m_aj.SImgList[k].path;
								if (this.m_aj.SImgList[k + 1].path.Contains("tryPageEnd"))
								{
									a_3 = this.m_aj.SImgList[k + 1].path;
								}
								if (this.m_aj.SImgList[k].path.Contains("tryPageEnd"))
								{
									a_4 = this.m_aj.SImgList[k].path;
								}
								a_5 = this.m_aj.LImgList[k + 1].pageNum + "-" + this.m_aj.LImgList[k].pageNum;
								num = k + 1;
								num2 = k;
								key3 = num / 2;
							}
							a(a_5, a_3, a_4, this.m_f, this.m_h, this.m_c);
							string leftImgPath5 = this.m_z + "\\" + this.m_aj.LImgList[num].path;
							if (this.m_aj.LImgList[num].path.Contains("tryPageEnd"))
							{
								leftImgPath5 = this.m_aj.LImgList[num].path;
							}
							string rightImgPath = this.m_z + "\\" + this.m_aj.LImgList[num2].path;
							if (this.m_aj.LImgList[num2].path.Contains("tryPageEnd"))
							{
								rightImgPath = this.m_aj.LImgList[num2].path;
							}
							ReadPagePair value5 = new ReadPagePair(num, num2, leftImgPath5, rightImgPath, this.m_aj.LImgList[num].pageId, this.m_aj.LImgList[num2].pageId, this.m_ac);
							if (!this.m_j.ContainsKey(key3))
							{
								this.m_j.Add(key3, value5);
							}
						}
					}
					catch (Exception arg3)
					{
						Console.WriteLine("Exception: {0}, From getHEJThumbnailAndPage, Double", arg3);
					}
				}
				cq.ItemsSource = this.m_e;
				this.m_c.PagePadding = new Thickness(0.0);
				this.m_b.PagePadding = new Thickness(0.0);
				cg.FontSize = 12.0;
				FlowDocumentReader flowDocumentReader = cg;
				FlowDocumentReader flowDocumentReader2 = cg;
				double num4 = cg.MinZoom = 500.0;
				double num7 = flowDocumentReader.Zoom = (flowDocumentReader2.MaxZoom = num4);
				int num8 = aw;
				if (num8.Equals(1))
				{
					cg.Document = this.m_b;
				}
				else if (num8.Equals(2))
				{
					cg.Document = this.m_c;
				}
				GC.Collect();
			}
			return true;
		}

		private void a(string A_0, string A_1, string A_2, List<ThumbnailImageAndPage> A_3, List<ImageStatus> A_4, FlowDocument A_5)
		{
			if (!File.Exists(A_1))
			{
				if (!A_1.Contains("tryPageEnd"))
				{
					A_1 = "pack://application:,,,/Assets/ReadWindow/NotYet.jpg";
				}
			}
			else
			{
				ThumbnailImageAndPage item = new ThumbnailImageAndPage(A_0, A_2, A_1, false);
				A_3.Add(item);
				item = null;
			}
			A_4.Add(ImageStatus.SMALLIMAGE);
			System.Windows.Controls.Image image = a(A_1, new Thickness(this.m_q));
			System.Windows.Controls.Image image2 = null;
			if (!A_2.Equals(""))
			{
				if (!File.Exists(A_2) && !A_2.Contains("tryPageEnd"))
				{
					A_2 = "pack://application:,,,/Assets/ReadWindow/NotYet.jpg";
				}
				image2 = a(A_2, new Thickness(this.m_q));
				if (image2.Source.Width > image2.Source.Height)
				{
					double num = (SystemParameters.PrimaryScreenWidth - 16.0) / 2.0 / image2.Source.Width;
					double num2 = image2.Source.Height * num;
					double num3 = SystemParameters.PrimaryScreenHeight - 110.0;
					double num4 = Math.Abs(num2 - num3) / 2.0 / num;
					image2.Margin = new Thickness(0.0, num4, 0.0, num4);
				}
				if (image.Source.Width > image.Source.Height)
				{
					double num5 = (SystemParameters.PrimaryScreenWidth - 16.0) / 2.0 / image.Source.Width;
					double num6 = image.Source.Height * num5;
					double num7 = SystemParameters.PrimaryScreenHeight - 110.0;
					double num8 = Math.Abs(num6 - num7) / 2.0 / num5;
					image.Margin = new Thickness(0.0, num8, 0.0, num8);
				}
			}
			BlockUIContainer item2 = new BlockUIContainer(a(image, image2));
			A_5.Blocks.Add(item2);
			item2 = null;
			image = null;
			image2 = null;
		}

		private StackPanel a(System.Windows.Controls.Image A_0, System.Windows.Controls.Image A_1)
		{
			StackPanel stackPanel = new StackPanel();
			stackPanel.Children.Add(A_0);
			if (A_1 != null)
			{
				stackPanel.Children.Add(A_1);
				if (this.m_aj.direction.Equals("right"))
				{
					stackPanel.FlowDirection = FlowDirection.LeftToRight;
				}
			}
			stackPanel.Orientation = Orientation.Horizontal;
			stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
			stackPanel.VerticalAlignment = VerticalAlignment.Center;
			stackPanel.RenderTransform = this.m_k;
			stackPanel.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
			stackPanel.MouseLeftButtonDown += new MouseButtonEventHandler(b);
			return stackPanel;
		}

		private void b(BitmapImage A_0)
		{
			Canvas canvas = FindVisualChildByName<Canvas>(cg, "zoomCanvas");
			canvas.RenderTransform = this.m_l;
			if (this.m_t.Equals(BookType.HEJ))
			{
				double height = 0.0;
				double width = 0.0;
				if (A_0.Width / 2.0 < A_0.Height)
				{
					Border border = e();
					height = border.ActualHeight;
					width = 0.0;
					width = (double)A_0.PixelWidth * border.ActualHeight / (double)A_0.PixelHeight;
				}
				else if (A_0.Width / 2.0 > A_0.Height)
				{
					Border border2 = e();
					height = 0.0;
					width = border2.ActualWidth;
					height = (double)A_0.PixelHeight * border2.ActualWidth / (double)A_0.PixelWidth;
				}
				canvas.Height = height;
				canvas.Width = width;
			}
			else if (this.m_t.Equals(BookType.PHEJ))
			{
				if (this.m_af == 0)
				{
					canvas.Height = (double)A_0.PixelHeight / this.m_ag[this.m_af] * 96.0 / DpiY;
					canvas.Width = (double)A_0.PixelWidth / this.m_ag[this.m_af] * 96.0 / DpiX;
				}
				else if (bg)
				{
					canvas.Height = (double)(A_0.PixelHeight * 96) / DpiY;
					canvas.Width = (double)(A_0.PixelWidth * 96) / DpiX;
				}
				else
				{
					canvas.Height = (double)A_0.PixelHeight / this.m_ag[this.m_af] * 96.0 / DpiY;
					canvas.Width = (double)A_0.PixelWidth / this.m_ag[this.m_af] * 96.0 / DpiX;
				}
			}
			ImageBrush imageBrush = new ImageBrush();
			imageBrush.ImageSource = A_0;
			imageBrush.AlignmentX = AlignmentX.Left;
			imageBrush.AlignmentY = AlignmentY.Top;
			imageBrush.Stretch = Stretch.Uniform;
			imageBrush.Freeze();
			canvas.Background = imageBrush;
		}

		private System.Windows.Controls.Image a(string A_0, Thickness A_1)
		{
			System.Windows.Controls.Image image = new System.Windows.Controls.Image();
			BitmapImage bitmapImage = (BitmapImage)(image.Source = new BitmapImage(new Uri(A_0, UriKind.RelativeOrAbsolute)));
			image.Stretch = Stretch.Uniform;
			image.Margin = A_1;
			bitmapImage = null;
			return image;
		}

		private byte[] a(BitmapImage A_0)
		{
			JpegBitmapEncoder jpegBitmapEncoder = new JpegBitmapEncoder();
			if (A_0.UriSource != null)
			{
				jpegBitmapEncoder.Frames.Add(BitmapFrame.Create(A_0.UriSource));
			}
			else
			{
				jpegBitmapEncoder.Frames.Add(BitmapFrame.Create(A_0));
			}
			using (MemoryStream memoryStream = new MemoryStream())
			{
				jpegBitmapEncoder.Save(memoryStream);
				byte[] result = memoryStream.ToArray();
				memoryStream.Close();
				memoryStream.Dispose();
				jpegBitmapEncoder = null;
				A_0 = null;
				GC.Collect();
				return result;
			}
		}

		private void c(int A_0)
		{
			if (this.m_r != 0 && A_0 > this.m_r)
			{
				return;
			}
			Block block = cg.Document.Blocks.FirstBlock;
			if (!A_0.Equals(0))
			{
				for (int i = 0; i < A_0; i++)
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
		}

		private void c(object A_0, SelectionChangedEventArgs A_1)
		{
			if (cq.SelectedIndex.Equals(-1))
			{
				return;
			}
			int num = 0;
			if (by || bx)
			{
				cq.Focus();
				object selectedItem = cq.SelectedItem;
				num = this.m_e.IndexOf((ThumbnailImageAndPage)cq.SelectedItem);
			}
			else
			{
				num = cq.SelectedIndex;
			}
			if (b0.Equals(PageMode.SinglePage))
			{
				c(num);
			}
			else if (b0.Equals(PageMode.DoublePage))
			{
				int num2 = num;
				if (num2 % 2 == 1)
				{
					num2++;
				}
				this.m_aj.direction.Equals("right");
				c(num2 / 2);
			}
			if (this.m_aa)
			{
				if (!bh)
				{
					this.m_af = 0;
					this.m_ad = (float)this.m_ag[0];
					c();
					cs.Visibility = Visibility.Collapsed;
				}
				else
				{
					if (num.Equals(0) || num.Equals(cq.Items.Count - 1))
					{
						d();
					}
					cs.Visibility = Visibility.Visible;
				}
			}
			if (b5)
			{
				ci.Visibility = Visibility.Hidden;
				BindingOperations.ClearBinding(ci, FrameworkElement.HeightProperty);
				BindingOperations.ClearBinding(cq, FrameworkElement.HeightProperty);
				FindVisualChildByName<RadioButton>(cg, "ShowAllImageButton").IsChecked = false;
				cr.Visibility = Visibility.Visible;
			}
			ListBoxItem listBoxItem = (ListBoxItem)cq.ItemContainerGenerator.ContainerFromItem(cq.SelectedItem);
			if (listBoxItem == null)
			{
				return;
			}
			listBoxItem.Focus();
			if (!b5)
			{
				if (this.m_aj.direction.Equals("right"))
				{
					ScrollViewer scrollViewer = FindVisualChildByName<ScrollViewer>(cq, "SVInLV");
					scrollViewer.ScrollToRightEnd();
					if ((double)(num + 1) * listBoxItem.ActualWidth > base.ActualWidth / 2.0)
					{
						double offset = scrollViewer.ScrollableWidth - (double)(num + 1) * listBoxItem.ActualWidth + base.ActualWidth / 2.0;
						scrollViewer.ScrollToHorizontalOffset(offset);
					}
				}
				else if ((double)(num + 1) * listBoxItem.ActualWidth > base.ActualWidth / 2.0)
				{
					ScrollViewer scrollViewer2 = FindVisualChildByName<ScrollViewer>(cq, "SVInLV");
					double offset2 = (double)(num + 1) * listBoxItem.ActualWidth - base.ActualWidth / 2.0;
					scrollViewer2.ScrollToHorizontalOffset(offset2);
				}
			}
			cq.SelectedIndex = -1;
			o();
		}

		private void a(object A_0, EventArgs A_1)
		{
			bool flag = false;
			bool flag2 = false;
			if (b0.Equals(PageMode.SinglePage))
			{
				flag = true;
			}
			else
			{
				if (!b0.Equals(PageMode.DoublePage))
				{
					return;
				}
				flag2 = true;
			}
			Thread.Sleep(500);
			try
			{
				if (DateTime.Now.Subtract(@as).TotalMilliseconds >= 300.0)
				{
					if (ReadWindow.m_y.Interval != a4)
					{
						ReadWindow.m_y.Interval = a4;
					}
					if (!b9.Count.Equals(0) && !bj)
					{
						for (int num = b9.Count - 1; num >= 0; num--)
						{
							if (this.m_ad.Equals((float)Convert.ToDouble(b9[num].Name)))
							{
								try
								{
									if (flag)
									{
										this.m_g[this.m_p] = ImageStatus.LARGEIMAGE;
									}
									else if (flag2)
									{
										this.m_h[this.m_p] = ImageStatus.LARGEIMAGE;
									}
									b9[num].Start();
									imageSourceRendered += new EventHandler<imageSourceRenderedResultEventArgs>(a);
									bj = true;
									return;
								}
								catch
								{
								}
							}
						}
					}
					v();
					byte[] am2 = am;
					if (flag2)
					{
						try
						{
							if (this.m_h[this.m_p] != ImageStatus.LARGEIMAGE || !bi)
							{
								int p2 = this.m_p;
								ReadPagePair readPagePair = this.m_j[this.m_p];
								if (readPagePair.rightPageIndex == -1)
								{
									if (File.Exists(readPagePair.leftImagePath))
									{
										w();
										bi = true;
									}
								}
								else if (File.Exists(readPagePair.leftImagePath) && File.Exists(readPagePair.rightImagePath))
								{
									w();
									bi = true;
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
							if (this.m_g[this.m_p] == ImageStatus.LARGEIMAGE && bi)
							{
								bk++;
							}
							else if (File.Exists(this.m_i[this.m_p].leftImagePath))
							{
								x();
								bi = true;
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

		private void x()
		{
			int num = this.m_p;
			List<int> list = new List<int>();
			if (this.m_g[num] != ImageStatus.LARGEIMAGE && this.m_g[num] != ImageStatus.GENERATING)
			{
				list.Add(num);
			}
			if (a3)
			{
				if (num + 1 < this.m_i.Count && this.m_g[num + 1] != ImageStatus.LARGEIMAGE && this.m_g[num + 1] != ImageStatus.GENERATING)
				{
					list.Add(num + 1);
				}
				if (num - 1 > 0 && this.m_g[num - 1] != ImageStatus.LARGEIMAGE && this.m_g[num - 1] != ImageStatus.GENERATING)
				{
					list.Add(num - 1);
				}
			}
			for (int i = 0; i < list.Count; i++)
			{
				if (num != this.m_p)
				{
					return;
				}
				ReadPagePair readPagePair = this.m_i[list[i]];
				if (readPagePair.leftImageSource == null && readPagePair.leftImagePath != "")
				{
					if (num != this.m_p)
					{
						return;
					}
					try
					{
						if (File.Exists(readPagePair.leftImagePath))
						{
							if (!readPagePair.isRendering)
							{
								if (this.m_t.Equals(BookType.PHEJ))
								{
									readPagePair.createLargePHEJBitmapImage(this.m_d, am, e(), true);
								}
								else if (this.m_t.Equals(BookType.HEJ))
								{
									readPagePair.createLargeHEJBitmapImage(this.m_d, am);
								}
								this.m_g[list[i]] = ImageStatus.GENERATING;
							}
						}
						else if ((readPagePair.leftImagePath.Contains("tryPageEnd") || readPagePair.rightImagePath.Contains("tryPageEnd")) && !readPagePair.isRendering)
						{
							if (this.m_t.Equals(BookType.PHEJ))
							{
								readPagePair.createLargePHEJBitmapImage(this.m_d, am, e(), true);
							}
							else if (this.m_t.Equals(BookType.HEJ))
							{
								readPagePair.createLargeHEJBitmapImage(this.m_d, am);
							}
							this.m_g[list[i]] = ImageStatus.GENERATING;
						}
					}
					catch (Exception)
					{
						return;
					}
				}
			}
			int count = this.m_i.Count;
			for (int j = 0; j < count; j++)
			{
				if (num != this.m_p)
				{
					return;
				}
				if (a3)
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
				ReadPagePair readPagePair2 = this.m_i[j];
				if ((this.m_g[j] == ImageStatus.GENERATING || this.m_g[j] == ImageStatus.LARGEIMAGE) && readPagePair2.leftImageSource != null)
				{
					readPagePair2.leftImageSource = null;
					readPagePair2.decodedPDFPages = new byte[2][];
					this.m_g[j] = ImageStatus.SMALLIMAGE;
				}
			}
			if (num != this.m_p)
			{
				return;
			}
			ReadPagePair readPagePair3 = this.m_i[this.m_p];
			Canvas canvas = FindVisualChildByName<Canvas>(cg, "zoomCanvas");
			if (num != this.m_p || readPagePair3.leftImageSource == null || readPagePair3.isRendering)
			{
				return;
			}
			try
			{
				this.m_ae = readPagePair3.baseScale;
				if (canvas.Background == null)
				{
					b((BitmapImage)readPagePair3.leftImageSource);
					this.m_g[this.m_p] = ImageStatus.LARGEIMAGE;
				}
			}
			catch (Exception)
			{
				readPagePair3.leftImageSource = null;
				return;
			}
			if (canvas.Background != null)
			{
				if (b7)
				{
					a(readPagePair3.leftPageIndex, -1, am, canvas);
				}
				f(this.m_aj.LImgList[readPagePair3.leftPageIndex].pageId);
				a(this.m_i[this.m_p].leftPageIndex);
				GC.Collect();
			}
		}

		private void w()
		{
			int num = this.m_p;
			List<int> list = new List<int>();
			if (this.m_h[num] != ImageStatus.LARGEIMAGE && this.m_h[num] != ImageStatus.GENERATING)
			{
				list.Add(num);
			}
			if (a3)
			{
				if (num + 1 < this.m_j.Count && this.m_h[num + 1] != ImageStatus.LARGEIMAGE && this.m_h[num + 1] != ImageStatus.GENERATING)
				{
					list.Add(num + 1);
				}
				if (num - 1 > 0 && this.m_h[num - 1] != ImageStatus.LARGEIMAGE && this.m_h[num - 1] != ImageStatus.GENERATING)
				{
					list.Add(num - 1);
				}
			}
			for (int i = 0; i < list.Count; i++)
			{
				if (num != this.m_p)
				{
					return;
				}
				ReadPagePair readPagePair = this.m_j[list[i]];
				if (readPagePair.leftImageSource == null && readPagePair.leftImagePath != "")
				{
					if (num != this.m_p)
					{
						return;
					}
					try
					{
						if (File.Exists(readPagePair.leftImagePath))
						{
							if (!readPagePair.isRendering)
							{
								if (this.m_t.Equals(BookType.PHEJ))
								{
									if (readPagePair.rightPageIndex.Equals(-1))
									{
										readPagePair.createLargePHEJBitmapImage(this.m_d, am, e(), true);
									}
									else
									{
										readPagePair.createLargePHEJBitmapImage(this.m_d, am, e(), false);
									}
								}
								else if (this.m_t.Equals(BookType.HEJ))
								{
									readPagePair.createLargeHEJBitmapImage(this.m_d, am);
								}
								this.m_h[list[i]] = ImageStatus.GENERATING;
							}
						}
						else if ((readPagePair.leftImagePath.Contains("tryPageEnd") || readPagePair.rightImagePath.Contains("tryPageEnd")) && !readPagePair.isRendering)
						{
							if (this.m_t.Equals(BookType.PHEJ))
							{
								if (readPagePair.rightPageIndex.Equals(-1))
								{
									readPagePair.createLargePHEJBitmapImage(this.m_d, am, e(), true);
								}
								else
								{
									readPagePair.createLargePHEJBitmapImage(this.m_d, am, e(), false);
								}
							}
							else if (this.m_t.Equals(BookType.HEJ))
							{
								readPagePair.createLargeHEJBitmapImage(this.m_d, am);
							}
							this.m_h[list[i]] = ImageStatus.GENERATING;
						}
					}
					catch (Exception)
					{
						return;
					}
				}
			}
			int count = this.m_j.Count;
			for (int j = 0; j < count; j++)
			{
				if (num != this.m_p)
				{
					return;
				}
				if (a3)
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
				ReadPagePair readPagePair2 = this.m_j[j];
				if (this.m_h[j] == ImageStatus.GENERATING || this.m_h[j] == ImageStatus.LARGEIMAGE)
				{
					readPagePair2.leftImageSource = null;
					readPagePair2.decodedPDFPages = new byte[2][];
					this.m_h[j] = ImageStatus.SMALLIMAGE;
				}
			}
			if (num != this.m_p)
			{
				return;
			}
			ReadPagePair readPagePair3 = this.m_j[this.m_p];
			Canvas canvas = FindVisualChildByName<Canvas>(cg, "zoomCanvas");
			if (num != this.m_p || readPagePair3.leftImageSource == null || readPagePair3.isRendering)
			{
				return;
			}
			try
			{
				this.m_ae = readPagePair3.baseScale;
				if (canvas.Background == null)
				{
					b((BitmapImage)readPagePair3.leftImageSource);
					this.m_h[this.m_p] = ImageStatus.LARGEIMAGE;
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
				if (b7)
				{
					a(readPagePair3.leftPageIndex, -1, am, canvas);
				}
				f(this.m_aj.LImgList[readPagePair3.leftPageIndex].pageId);
				a(this.m_i[this.m_p].leftPageIndex);
			}
			else
			{
				if (b7)
				{
					a(readPagePair3.leftPageIndex, readPagePair3.rightPageIndex, am, canvas);
				}
				a(this.m_aj.LImgList[readPagePair3.leftPageIndex].pageId, this.m_aj.LImgList[readPagePair3.rightPageIndex].pageId);
				a(readPagePair3.leftPageIndex, readPagePair3.rightPageIndex);
			}
			GC.Collect();
		}

		private void a(object A_0, DataTransferEventArgs A_1)
		{
			if (this.m_j.Count == 0 || this.m_i.Count == 0)
			{
				return;
			}
			this.m_ai[0] = null;
			this.m_ai[1] = null;
			Canvas canvas = i();
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(cg, "penMemoCanvas");
			if (b1 != MediaCanvasOpenedBy.CategoryButton)
			{
				b1 = MediaCanvasOpenedBy.None;
			}
			bi = false;
			if (canvas.Children.Count > 0)
			{
				canvas.Children.Clear();
				FindVisualChildByName<RadioButton>(cg, "FullTextButton").Visibility = Visibility.Collapsed;
			}
			if (inkCanvas.Strokes.Count > 0)
			{
				d(this.m_aj.LImgList[this.m_p].pageId);
				inkCanvas.Strokes.Clear();
			}
			TextBlock textBlock = (TextBlock)A_0;
			if (textBlock == null)
			{
				return;
			}
			if (!this.m_aa)
			{
				t();
				inkCanvas.StrokeCollected += new InkCanvasStrokeCollectedEventHandler(a);
				inkCanvas.StrokeErasing += new InkCanvasStrokeErasingEventHandler(a);
				if (aq == null)
				{
					u();
					bm = true;
				}
				else if (aq.ContainsKey(a5))
				{
					if (aq[a5].index != 0)
					{
						Canvas canvas2 = FindVisualChildByName<Canvas>(cg, "zoomCanvas");
						BrushConverter brushConverter = new BrushConverter();
						canvas2.Background = (System.Windows.Media.Brush)brushConverter.ConvertFrom("#FF212020");
						bn = true;
					}
					else
					{
						u();
					}
				}
				else
				{
					u();
				}
				return;
			}
			this.m_p = Convert.ToInt32(textBlock.Text) - 1;
			if (bn)
			{
				u();
				bn = false;
			}
			FindVisualChildByName<Canvas>(cg, "zoomCanvas").Background = null;
			b9.Clear();
			bj = false;
			@as = DateTime.Now;
			if (b0.Equals(PageMode.SinglePage))
			{
				if (this.m_i[this.m_p].leftImageSource != null)
				{
					bg = true;
					try
					{
						b((BitmapImage)this.m_i[this.m_p].leftImageSource);
						this.m_g[this.m_p] = ImageStatus.LARGEIMAGE;
					}
					catch (Exception)
					{
						this.m_i[this.m_p].leftImageSource = null;
					}
				}
			}
			else if (b0.Equals(PageMode.DoublePage) && this.m_j.Count > this.m_p && this.m_j[this.m_p].leftImageSource != null)
			{
				bg = true;
				try
				{
					b((BitmapImage)this.m_j[this.m_p].leftImageSource);
					this.m_h[this.m_p] = ImageStatus.LARGEIMAGE;
				}
				catch (Exception)
				{
					this.m_j[this.m_p].leftImageSource = null;
				}
			}
			if (bh && this.m_t.Equals(BookType.PHEJ))
			{
				a(this.m_ag[this.m_af]);
			}
			else if (this.m_r > 0)
			{
				c();
			}
			if (this.m_p >= this.m_aj.SImgList.Count || this.m_p < 0)
			{
				return;
			}
			an = a6.getBookMarkDics(this.m_ab);
			ao = a6.getBookNoteDics(this.m_ab);
			RadioButton radioButton = FindVisualChildByName<RadioButton>(cg, "BookMarkButton");
			RadioButton radioButton2 = FindVisualChildByName<RadioButton>(cg, "NoteButton");
			if (b0.Equals(PageMode.SinglePage))
			{
				if (this.m_r.Equals(0))
				{
					if (this.m_p > this.m_e.Count - 1)
					{
						return;
					}
					cq.SelectedItem = this.m_e[this.m_p];
				}
				FindVisualChildByName<TextBlock>(cg, "CurPageInReader").Text = (this.m_p + 1).ToString();
				if (an.ContainsKey(this.m_p))
				{
					if (an[this.m_p].status == "0")
					{
						radioButton.IsChecked = true;
					}
					else
					{
						radioButton.IsChecked = false;
					}
				}
				else
				{
					radioButton.IsChecked = false;
				}
				if (ao.ContainsKey(this.m_p))
				{
					if (ao[this.m_p].status == "0")
					{
						radioButton2.IsChecked = true;
					}
					else
					{
						radioButton2.IsChecked = false;
					}
				}
				else
				{
					radioButton2.IsChecked = false;
				}
			}
			else
			{
				if (!b0.Equals(PageMode.DoublePage) || this.m_j.Count <= this.m_p)
				{
					return;
				}
				ReadPagePair readPagePair = this.m_j[this.m_p];
				if (this.m_r.Equals(0))
				{
					int num = Math.Max(readPagePair.rightPageIndex, readPagePair.leftPageIndex);
					if (num < cq.Items.Count)
					{
						cq.SelectedItem = cq.Items[num];
					}
				}
				if (readPagePair.rightPageIndex == -1 || readPagePair.leftPageIndex == -1)
				{
					bool value = false;
					int num2 = Math.Max(readPagePair.rightPageIndex, readPagePair.leftPageIndex);
					if (an.ContainsKey(num2) && an[num2].status == "0")
					{
						value = true;
					}
					FindVisualChildByName<TextBlock>(cg, "CurPageInReader").Text = (num2 + 1).ToString();
					radioButton.IsChecked = value;
					return;
				}
				FindVisualChildByName<TextBlock>(cg, "CurPageInReader").Text = (readPagePair.leftPageIndex + 1 + "-" + (readPagePair.rightPageIndex + 1)).ToString();
				bool flag = false;
				bool flag2 = false;
				if (an.ContainsKey(readPagePair.leftPageIndex) && an[readPagePair.leftPageIndex].status == "0")
				{
					flag = true;
				}
				if (an.ContainsKey(readPagePair.rightPageIndex) && an[readPagePair.rightPageIndex].status == "0")
				{
					flag2 = true;
				}
				if (flag || flag2)
				{
					radioButton.IsChecked = true;
				}
				else
				{
					radioButton.IsChecked = false;
				}
			}
		}

		private void v()
		{
			if (bm)
			{
				return;
			}
			if (aq.Count > 0)
			{
				LastPageData lastPageData = null;
				if (aq.ContainsKey(a5))
				{
					lastPageData = aq[a5];
				}
				foreach (KeyValuePair<string, LastPageData> item in aq)
				{
					if (item.Key != a5)
					{
						LastPageData lastPageData2 = aq[item.Key];
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
							if (lastPageData2.index == this.m_p)
							{
								bm = true;
								return;
							}
							if (MessageBox.Show(messageBoxText, "", MessageBoxButton.YesNo).Equals(MessageBoxResult.Yes))
							{
								d d = new d();
								d.b = this;
								d.a = -1;
								if (lastPageData2.index > 0)
								{
									e e = new e();
									e.b = d;
									e.b.a = lastPageData2.index;
									bo = new a(b);
									e.a = null;
									AsyncCallback a_ = new AsyncCallback(e.c);
									e.a = bo.BeginInvoke(e.b.a, a_, null);
								}
								break;
							}
						}
					}
				}
			}
			bm = true;
		}

		private void b(int A_0)
		{
		}

		private void u()
		{
			if (b0.Equals(PageMode.SinglePage))
			{
				x();
			}
			else if (b0.Equals(PageMode.DoublePage))
			{
				w();
			}
			if (!a2)
			{
				a3 = true;
			}
			if (!ReadWindow.m_y.IsEnabled)
			{
				ReadWindow.m_y.IsEnabled = true;
				ReadWindow.m_y.Start();
			}
		}

		private void t()
		{
			if (this.m_r == 0)
			{
				ae();
			}
			else
			{
				FindVisualChildByName<Canvas>(cg, "zoomCanvas").Background = null;
			}
			ad();
			FindVisualChildByName<TextBlock>(cg, "TotalPageInReader").Text = this.m_aj.SImgList.Count.ToString();
			FindVisualChildByName<TextBlock>(cg, "CurPageInReader").Text = (this.m_p + 1).ToString();
			WrapPanel wrapPanel = FindVisualChildByName<WrapPanel>(cq, "wrapPanel");
			if (this.m_aj.direction.Equals("right"))
			{
				wrapPanel.FlowDirection = FlowDirection.RightToLeft;
				RadioButton radioButton = FindVisualChildByName<RadioButton>(cg, "leftPageButton");
				radioButton.CommandBindings.Clear();
				radioButton.Command = NavigationCommands.NextPage;
				Binding binding = new Binding();
				binding.Source = cg;
				binding.Path = new PropertyPath("CanGoToNextPage");
				BindingOperations.SetBinding(radioButton, UIElement.IsEnabledProperty, binding);
				RadioButton radioButton2 = FindVisualChildByName<RadioButton>(cg, "rightPageButton");
				radioButton2.CommandBindings.Clear();
				radioButton2.Command = NavigationCommands.PreviousPage;
				Binding binding2 = new Binding();
				binding2.Source = cg;
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
				wrapPanel.FlowDirection = FlowDirection.LeftToRight;
			}
			this.m_aa = true;
			cg.PreviewLostKeyboardFocus += new KeyboardFocusChangedEventHandler(a);
			Keyboard.Focus(cg);
			ReadWindow.m_y = new DispatcherTimer();
			ReadWindow.m_y.Interval = new TimeSpan(0, 0, 0, 0, 500);
			ReadWindow.m_y.Tick += new EventHandler(a);
		}

		private void a(object A_0, KeyboardFocusChangedEventArgs A_1)
		{
			if (A_1.OldFocus is FlowDocumentReader)
			{
				bp = A_1.NewFocus;
				cg.PreviewLostKeyboardFocus -= new KeyboardFocusChangedEventHandler(a);
				A_1.Handled = true;
			}
		}

		private void a(double A_0, double A_1)
		{
			try
			{
				if (!File.Exists(this.m_z + "\\hyweb\\strokes\\" + bw))
				{
					FileStream fileStream = new FileStream(this.m_z + "\\hyweb\\strokes\\" + bw, FileMode.Create);
					XmlWriter xmlWriter = XmlWriter.Create(fileStream);
					xmlWriter.WriteStartDocument();
					xmlWriter.WriteStartElement("status");
					xmlWriter.WriteElementString("originalCanvasWidth", A_0.ToString());
					xmlWriter.WriteElementString("originalCanvasHeight", A_1.ToString());
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

		private void s()
		{
			try
			{
				if (File.Exists(this.m_z + "\\hyweb\\strokes\\" + bw))
				{
					XmlReader reader = XmlReader.Create(new FileStream(this.m_z + "\\hyweb\\strokes\\" + bw, FileMode.Open));
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
									bu = Convert.ToDouble(childNode2.InnerText);
								}
								else if (childNode2.Name.Equals("originalCanvasHeight"))
								{
									bv = Convert.ToDouble(childNode2.InnerText);
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

		private void a(string A_0, string A_1)
		{
			Canvas canvas = FindVisualChildByName<Canvas>(cg, "zoomCanvas");
			if (canvas.Width.Equals(double.NaN) || canvas.Height.Equals(double.NaN))
			{
				return;
			}
			double offsetX = canvas.Width / 2.0;
			StrokeCollection strokeCollection = new StrokeCollection();
			StrokeCollection strokeCollection2 = new StrokeCollection();
			StrokeCollection strokeCollection3 = new StrokeCollection();
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(cg, "penMemoCanvas");
			if (!bz)
			{
				bq = canvas.Width;
				br = canvas.Height;
			}
			else
			{
				bs = canvas.Width;
				bt = canvas.Height;
			}
			inkCanvas.Width = canvas.Width;
			inkCanvas.Height = canvas.Height;
			inkCanvas.RenderTransform = this.m_l;
			if (File.Exists(this.m_z + "/hyweb/strokes/" + A_0 + ".isf"))
			{
				FileStream fileStream = new FileStream(this.m_z + "/hyweb/strokes/" + A_0 + ".isf", FileMode.Open);
				if (fileStream.Length > 0)
				{
					strokeCollection2 = new StrokeCollection(fileStream);
				}
				fileStream.Close();
			}
			if (File.Exists(this.m_z + "/hyweb/strokes/" + A_1 + ".isf"))
			{
				FileStream fileStream2 = new FileStream(this.m_z + "/hyweb/strokes/" + A_1 + ".isf", FileMode.Open);
				strokeCollection3 = new StrokeCollection(fileStream2);
				fileStream2.Close();
			}
			if (strokeCollection2.Count > 0)
			{
				System.Windows.Media.Matrix transformMatrix = new System.Windows.Media.Matrix(1.0, 0.0, 0.0, 1.0, 0.0, 0.0);
				if (!bv.Equals(0.0) && !bu.Equals(0.0) && (br != bv || bq / 2.0 != bu))
				{
					double scaleX = bq / 2.0 / bu;
					double scaleY = br / bv;
					transformMatrix.Scale(scaleX, scaleY);
				}
				if (bz)
				{
					double scaleX2 = bs / 2.0 / (bq / 2.0);
					double scaleY2 = bt / br;
					transformMatrix.Scale(scaleX2, scaleY2);
				}
				strokeCollection2.Transform(transformMatrix, false);
				strokeCollection.Add(strokeCollection2);
			}
			if (strokeCollection3.Count > 0)
			{
				System.Windows.Media.Matrix transformMatrix2 = new System.Windows.Media.Matrix(1.0, 0.0, 0.0, 1.0, offsetX, 0.0);
				if (!bv.Equals(0.0) && !bu.Equals(0.0) && (br != bv || bq / 2.0 != bu))
				{
					double num = bq / 2.0 / bu;
					double scaleY3 = br / bv;
					transformMatrix2.OffsetX /= num;
					transformMatrix2.Scale(num, scaleY3);
				}
				if (bz)
				{
					double num2 = bs / 2.0 / (bq / 2.0);
					double scaleY4 = bt / br;
					transformMatrix2.OffsetX /= num2;
					transformMatrix2.Scale(num2, scaleY4);
				}
				strokeCollection3.Transform(transformMatrix2, false);
				strokeCollection.Add(strokeCollection3);
				inkCanvas.Strokes = strokeCollection;
			}
		}

		private void f(string A_0)
		{
			Canvas canvas = FindVisualChildByName<Canvas>(cg, "zoomCanvas");
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(cg, "penMemoCanvas");
			if (canvas.Width.Equals(double.NaN) || canvas.Height.Equals(double.NaN))
			{
				return;
			}
			if (!bz)
			{
				bq = canvas.Width;
				br = canvas.Height;
			}
			else
			{
				bs = canvas.Width;
				bt = canvas.Height;
			}
			inkCanvas.Width = canvas.Width;
			inkCanvas.Height = canvas.Height;
			inkCanvas.RenderTransform = this.m_l;
			StrokeCollection strokeCollection = new StrokeCollection();
			if (File.Exists(this.m_z + "\\hyweb\\strokes\\" + A_0 + ".isf"))
			{
				FileStream fileStream = new FileStream(this.m_z + "\\hyweb\\strokes\\" + A_0 + ".isf", FileMode.Open);
				if (fileStream.Length > 0)
				{
					strokeCollection = new StrokeCollection(fileStream);
				}
				fileStream.Close();
			}
			if (strokeCollection.Count > 0)
			{
				System.Windows.Media.Matrix transformMatrix = new System.Windows.Media.Matrix(1.0, 0.0, 0.0, 1.0, 0.0, 0.0);
				if (!bv.Equals(0.0) && !bu.Equals(0.0) && (br != bv || bq != bu))
				{
					double scaleX = bq / bu;
					double scaleY = br / bv;
					transformMatrix.Scale(scaleX, scaleY);
				}
				if (bz)
				{
					double scaleX2 = bs / bq;
					double scaleY2 = bt / br;
					transformMatrix.Scale(scaleX2, scaleY2);
				}
				strokeCollection.Transform(transformMatrix, false);
			}
			inkCanvas.Strokes = strokeCollection;
		}

		private void a(int A_0, int A_1)
		{
			Canvas canvas = FindVisualChildByName<Canvas>(cg, "zoomCanvas");
			if (canvas.Width.Equals(double.NaN) || canvas.Height.Equals(double.NaN))
			{
				return;
			}
			double a_ = canvas.Width / 2.0;
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(cg, "penMemoCanvas");
			inkCanvas.Width = canvas.Width;
			inkCanvas.Height = canvas.Height;
			inkCanvas.RenderTransform = this.m_l;
			ap = a6.getStrokesDics(this.m_ab);
			if (ap.ContainsKey(A_0))
			{
				List<StrokesData> list = ap[A_0];
				int count = list.Count;
				for (int i = 0; i < count; i++)
				{
					if (list[i].status == "0")
					{
						a(list[i], canvas.Width / 2.0, canvas.Height, 0.0, 0.0);
					}
				}
			}
			if (!ap.ContainsKey(A_1))
			{
				return;
			}
			List<StrokesData> list2 = ap[A_1];
			int count2 = list2.Count;
			for (int j = 0; j < count2; j++)
			{
				if (list2[j].status == "0")
				{
					a(list2[j], canvas.Width / 2.0, canvas.Height, a_, 0.0);
				}
			}
		}

		private void a(int A_0)
		{
			Canvas canvas = FindVisualChildByName<Canvas>(cg, "zoomCanvas");
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(cg, "penMemoCanvas");
			if (canvas.Width.Equals(double.NaN) || canvas.Height.Equals(double.NaN))
			{
				return;
			}
			inkCanvas.Width = canvas.Width;
			inkCanvas.Height = canvas.Height;
			inkCanvas.RenderTransform = this.m_l;
			ap = a6.getStrokesDics(this.m_ab);
			if (!ap.ContainsKey(A_0))
			{
				return;
			}
			List<StrokesData> list = ap[A_0];
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				if (list[i].status == "0")
				{
					a(list[i], canvas.Width, canvas.Height, 0.0, 0.0);
				}
			}
		}

		private void a(StrokesData A_0, double A_1, double A_2, double A_3, double A_4)
		{
			double num = A_0.width;
			double num2 = A_0.canvasheight;
			double num3 = A_0.canvaswidth;
			double num4 = A_0.alpha;
			string color = A_0.color;
			double num5 = A_1 / num3;
			double num6 = A_2 / num2;
			string[] array = A_0.points.Split(';');
			char[] trimChars = new char[2]
			{
				'{',
				'}'
			};
			StylusPointCollection stylusPointCollection = new StylusPointCollection();
			for (int i = 0; i < array.Length; i++)
			{
				System.Windows.Point point = default(System.Windows.Point);
				point = System.Windows.Point.Parse(array[i].TrimEnd(trimChars).TrimStart(trimChars));
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
			new System.Windows.Media.ColorConverter();
			System.Windows.Media.Color color2 = e(color);
			stroke.DrawingAttributes.Color = color2;
			System.Windows.Media.Matrix transformMatrix = new System.Windows.Media.Matrix(1.0, 0.0, 0.0, 1.0, A_3, 0.0);
			if (stroke != null)
			{
				InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(cg, "penMemoCanvas");
				stroke.Transform(transformMatrix, false);
				inkCanvas.Strokes.Add(stroke.Clone());
				stroke = null;
			}
		}

		private System.Windows.Media.Color e(string A_0)
		{
			byte b = 0;
			byte b2 = 0;
			byte b3 = 0;
			if (A_0.Length == 7)
			{
				A_0 = A_0.Insert(1, "FF");
			}
			if (A_0.StartsWith("#"))
			{
				A_0 = A_0.Substring(1, 8);
			}
			byte num = Convert.ToByte(int.Parse(A_0.Substring(0, 2), NumberStyles.AllowHexSpecifier));
			b = Convert.ToByte(int.Parse(A_0.Substring(2, 2), NumberStyles.AllowHexSpecifier));
			b2 = Convert.ToByte(int.Parse(A_0.Substring(4, 2), NumberStyles.AllowHexSpecifier));
			b3 = Convert.ToByte(int.Parse(A_0.Substring(6, 2), NumberStyles.AllowHexSpecifier));
			return System.Windows.Media.Color.FromArgb(num, b, b2, b3);
		}

		private void d(string A_0)
		{
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(cg, "penMemoCanvas");
			if (!File.Exists(this.m_z + "\\hyweb\\strokes\\" + A_0 + ".isf"))
			{
				return;
			}
			if (inkCanvas.Strokes.Count > 0)
			{
				DateTime value = new DateTime(1970, 1, 1);
				long num = DateTime.Now.ToUniversalTime().Subtract(value).Ticks / 10000000;
				StrokeCollection strokes = inkCanvas.Strokes;
				System.Windows.Media.Matrix transformMatrix = new System.Windows.Media.Matrix(1.0, 0.0, 0.0, 1.0, 0.0, 0.0);
				if (!bv.Equals(0.0) && !bu.Equals(0.0) && (br != bv || bq != bu))
				{
					double scaleX = bu / bq;
					double scaleY = bv / br;
					transformMatrix.Scale(scaleX, scaleY);
				}
				if (bz)
				{
					double scaleX2 = bq / bs;
					double scaleY2 = br / bt;
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
						text2 = text2 + "{" + stylusPoint.X + ", " + stylusPoint.Y + "};";
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
					strokesData.index = this.m_p;
					strokesData.points = text2;
					strokesData.status = "0";
					strokesData.synctime = 0L;
					strokesData.updatetime = num;
					strokesData.userid = "";
					strokesData.vendor = "";
					strokesData.width = (float)drawingAttributes.Height;
					string item = a6.insertStrokeCmdString(this.m_ab, strokesData);
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
				if (list.Count > 0)
				{
					a6.saveBatchData(list);
				}
			}
			try
			{
				File.Delete(this.m_z + "\\hyweb\\strokes\\" + A_0 + ".isf");
			}
			catch (IOException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		private void a(object A_0, InkCanvasStrokeErasingEventArgs A_1)
		{
			Stroke stroke = A_1.Stroke;
			if (stroke == null)
			{
				return;
			}
			InkCanvas inkCanvas = (InkCanvas)A_0;
			List<StrokesData> curPageStrokes = a6.getCurPageStrokes(this.m_ab, this.m_p);
			int count = curPageStrokes.Count;
			int num = 0;
			while (true)
			{
				if (num < count)
				{
					if (a(stroke, curPageStrokes[num], inkCanvas.Width, inkCanvas.Height))
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
			string item = a6.deleteStrokeCmdString(this.m_ab, curPageStrokes[num]);
			if (!list.Contains(item))
			{
				list.Add(item);
			}
			if (list.Count > 0)
			{
				a6.saveBatchData(list);
			}
		}

		private bool a(Stroke A_0, StrokesData A_1, double A_2, double A_3)
		{
			double num = A_1.canvasheight;
			double num2 = (double)A_1.canvaswidth / A_2;
			double num3 = num / A_3;
			int count = A_0.StylusPoints.Count;
			int num4 = 0;
			string text = A_1.points.Replace(" ", "");
			for (int i = 0; i < count; i++)
			{
				StylusPoint stylusPoint = A_0.StylusPoints[i];
				string value = "{" + stylusPoint.X * num2 + "," + stylusPoint.Y * num3 + "}";
				if (text.Contains(value))
				{
					num4++;
				}
			}
			if ((double)(num4 / count * 100) == 100.0)
			{
				return true;
			}
			return false;
		}

		private void a(object A_0, InkCanvasStrokeCollectedEventArgs A_1)
		{
			Stroke stroke = A_1.Stroke;
			if (stroke != null)
			{
				a(stroke);
			}
		}

		private void a(Stroke A_0)
		{
			DateTime value = new DateTime(1970, 1, 1);
			long num = DateTime.Now.ToUniversalTime().Subtract(value).Ticks / 10000000;
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(cg, "penMemoCanvas");
			int count = A_0.StylusPoints.Count;
			DrawingAttributes drawingAttributes = A_0.DrawingAttributes;
			string text = drawingAttributes.Color.ToString();
			text = text.Remove(1, 2);
			string text2 = "";
			for (int i = 0; i < count; i++)
			{
				StylusPoint stylusPoint = A_0.StylusPoints[i];
				text2 = text2 + "{" + stylusPoint.X + ", " + stylusPoint.Y + "};";
			}
			text2 = text2.Substring(0, text2.LastIndexOf(';'));
			StrokesData strokesData = new StrokesData();
			strokesData.objectId = "";
			strokesData.alpha = (float)(drawingAttributes.IsHighlighter ? 0.5 : 1.0);
			strokesData.bookid = this.m_u;
			strokesData.canvasheight = (float)inkCanvas.Height;
			strokesData.canvaswidth = (float)inkCanvas.Width;
			strokesData.color = text;
			strokesData.createtime = num;
			strokesData.index = this.m_p;
			strokesData.points = text2;
			strokesData.status = "0";
			strokesData.synctime = 0L;
			strokesData.updatetime = num;
			strokesData.userid = this.m_v;
			strokesData.vendor = this.m_w;
			strokesData.width = (float)drawingAttributes.Height;
			a6.saveStrokesData(this.m_ab, false, strokesData);
		}

		public void strokeChaneEventHandler(DrawingAttributes d)
		{
			FindVisualChildByName<InkCanvas>(cg, "penMemoCanvas").DefaultDrawingAttributes = d;
		}

		public void strokeUndoEventHandler()
		{
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(cg, "penMemoCanvas");
			if (inkCanvas.Strokes.Count > 0)
			{
				at.Add(inkCanvas.Strokes[inkCanvas.Strokes.Count - 1]);
				inkCanvas.Strokes.RemoveAt(inkCanvas.Strokes.Count - 1);
			}
		}

		public void strokeRedoEventHandler()
		{
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(cg, "penMemoCanvas");
			while (at.Count > 0)
			{
				inkCanvas.Strokes.Add(at[at.Count - 1]);
				at.RemoveAt(at.Count - 1);
			}
		}

		public void strokeEraseEventHandler()
		{
			FindVisualChildByName<InkCanvas>(cg, "penMemoCanvas").EditingMode = InkCanvasEditingMode.EraseByStroke;
		}

		public void strokeLineEventHandler()
		{
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(cg, "penMemoCanvas");
			inkCanvas.EditingMode = InkCanvasEditingMode.None;
			inkCanvas.MouseLeftButtonDown += new MouseButtonEventHandler(g);
			inkCanvas.MouseUp += new MouseButtonEventHandler(f);
			inkCanvas.MouseMove += new MouseEventHandler(b);
			ax = true;
		}

		public void strokeCurveEventHandler()
		{
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(cg, "penMemoCanvas");
			inkCanvas.MouseDown -= new MouseButtonEventHandler(g);
			inkCanvas.MouseUp -= new MouseButtonEventHandler(f);
			inkCanvas.MouseMove -= new MouseEventHandler(b);
			inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
			ax = false;
		}

		private void g(object A_0, MouseButtonEventArgs A_1)
		{
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(cg, "penMemoCanvas");
			if (inkCanvas.EditingMode == InkCanvasEditingMode.None)
			{
				au = new StylusPointCollection();
				System.Windows.Point position = A_1.GetPosition(inkCanvas);
				au.Add(new StylusPoint(position.X, position.Y));
			}
		}

		private void b(object A_0, MouseEventArgs A_1)
		{
			FindVisualChildByName<InkCanvas>(cg, "penMemoCanvas");
		}

		private void f(object A_0, MouseButtonEventArgs A_1)
		{
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(cg, "penMemoCanvas");
			if (inkCanvas.EditingMode == InkCanvasEditingMode.None && au != null)
			{
				System.Windows.Point position = A_1.GetPosition(inkCanvas);
				au.Add(new StylusPoint(position.X, position.Y));
				av = new Stroke(au, inkCanvas.DefaultDrawingAttributes);
				inkCanvas.Strokes.Add(av.Clone());
				a(av.Clone());
				au = null;
				av = null;
			}
		}

		public void strokDelEventHandler()
		{
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(cg, "penMemoCanvas");
			Button button = FindVisualChildByName<Button>(f(), "delClickButton");
			if (inkCanvas.EditingMode != InkCanvasEditingMode.EraseByStroke)
			{
				inkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
				button.Content = a7.getLangString("stroke");
				inkCanvas.MouseDown += new MouseButtonEventHandler(d);
			}
			else
			{
				inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
				inkCanvas.MouseDown -= new MouseButtonEventHandler(d);
				button.Content = a7.getLangString("delete");
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
			Canvas canvas = FindVisualChildByName<Canvas>(cg, "PopupControlCanvas");
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

		private void e(object A_0, MouseButtonEventArgs A_1)
		{
			Canvas canvas = FindVisualChildByName<Canvas>(cg, "PopupControlCanvas");
			Panel.SetZIndex(canvas, 899);
			if (canvas.Visibility.Equals(Visibility.Visible))
			{
				canvas.Visibility = Visibility.Collapsed;
			}
			Grid grid = FindVisualChildByName<Grid>(cg, "PenMemoToolBar");
			((StrokeToolPanelHorizontal)grid.Children[grid.Children.Count - 1]).closePopup();
		}

		private void d(object A_0, MouseButtonEventArgs A_1)
		{
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(cg, "penMemoCanvas");
			StrokeCollection selectedStrokes = inkCanvas.GetSelectedStrokes();
			if (selectedStrokes.Count > 0)
			{
				inkCanvas.Strokes.Remove(selectedStrokes);
			}
		}

		public void strokeDelAllEventHandler()
		{
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(cg, "penMemoCanvas");
			for (int i = 0; i < inkCanvas.Strokes.Count; i++)
			{
				at.Add(inkCanvas.Strokes[i]);
			}
			inkCanvas.Strokes.Clear();
			List<StrokesData> curPageStrokes = a6.getCurPageStrokes(this.m_ab, this.m_p);
			int count = curPageStrokes.Count;
			List<string> list = new List<string>();
			for (int j = 0; j < count; j++)
			{
				DateTime value = new DateTime(1970, 1, 1);
				long updatetime = DateTime.Now.ToUniversalTime().Subtract(value).Ticks / 10000000;
				curPageStrokes[j].updatetime = updatetime;
				curPageStrokes[j].status = "1";
				string item = a6.deleteStrokeCmdString(this.m_ab, curPageStrokes[j]);
				if (!list.Contains(item))
				{
					list.Add(item);
				}
			}
			if (list.Count > 0)
			{
				a6.saveBatchData(list);
			}
		}

		private void a(int A_0, int A_1, byte[] A_2, UIElement A_3)
		{
			if (this.m_ak == null)
			{
				b7 = false;
				return;
			}
			e();
			Canvas canvas = (Canvas)A_3;
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
			a(A_0, A_1, A_2, width, height, num2, num);
		}

		private void a(int A_0, int A_1, byte[] A_2, double A_3, double A_4, double A_5, double A_6)
		{
			double num = A_4 / A_6;
			if (!A_1.Equals(-1))
			{
				A_5 /= 2.0;
				num = A_4 / A_6;
				double a_ = A_5 * num;
				double a_2 = 0.0;
				a(A_1, A_2, A_3, A_4, num, a_, a_2, A_6, A_5);
			}
			a(A_0, A_2, A_3, A_4, num, 0.0, 0.0, A_6, A_5);
		}

		private void a(int A_0, byte[] A_1, double A_2, double A_3, double A_4, double A_5, double A_6, double A_7, double A_8)
		{
			if (!this.m_ak.HyperLinkAreaDictionary.ContainsKey(this.m_aj.LImgList[A_0].pageId))
			{
				return;
			}
			Canvas canvas = this.i();
			al = this.m_ak.getHyperLinkAreasByPageId(this.m_aj.LImgList[A_0].pageId, A_1);
			canvas.RenderTransform = this.m_l;
			canvas.Height = A_3;
			canvas.Width = A_2;
			double num = A_7;
			double num2 = A_8;
			List<HyperLinkArea> hyperLinkAreas = al.hyperLinkAreas;
			if (al.refHeight != 0 && al.refWidth != 0)
			{
				num = al.refHeight;
				num2 = al.refWidth;
				A_4 = A_3 / num;
				if (!A_5.Equals(0.0))
				{
					A_5 = num2 * A_4;
				}
			}
			for (int i = 0; i < hyperLinkAreas.Count; i++)
			{
				if (!hyperLinkAreas[i].itemRef.Count.Equals(0) || !hyperLinkAreas[i].items.Count.Equals(0))
				{
					a(hyperLinkAreas[i], this.m_aj.LImgList[A_0].pageId, canvas, al, A_4, A_5, A_6);
				}
			}
		}

		private void a(HyperLinkArea A_0, string A_1, Canvas A_2, PageInfoMetadata A_3, double A_4, double A_5, double A_6)
		{
			string areaId = A_0.areaId;
			if (areaId.StartsWith("FullText"))
			{
				RadioButton radioButton = FindVisualChildByName<RadioButton>(cg, "FullTextButton");
				radioButton.Visibility = Visibility.Visible;
				radioButton.Uid = areaId;
				radioButton.Tag = A_3;
				return;
			}
			float startX = A_0.startX;
			float startY = A_0.startY;
			float endX = A_0.endX;
			float endY = A_0.endY;
			Button button = new Button();
			button.Style = (Style)FindResource("AreaButtonStyle");
			A_2.Children.Add(button);
			double width = Math.Ceiling((double)(endX - startX) * A_4);
			double height = Math.Ceiling((double)(endY - startY) * A_4);
			double num = Math.Floor((double)startX * A_4);
			double num2 = Math.Floor((double)startY * A_4);
			button.Width = width;
			button.Height = height;
			button.Uid = areaId;
			button.Tag = A_1;
			button.Click += new RoutedEventHandler(ai);
			Canvas.SetTop(button, num2 + A_6);
			Canvas.SetLeft(button, num + A_5);
			if (A_0.shape.Equals("icon"))
			{
				string uriString = this.m_z + "\\HYWEB\\" + A_0.imagePath.Replace("/", "\\");
				try
				{
					button.Background = new ImageBrush(new BitmapImage(new Uri(uriString)));
				}
				catch
				{
				}
			}
		}

		private void ai(object A_0, RoutedEventArgs A_1)
		{
			string uid = ((Button)A_0).Uid;
			string pageId = (string)((Button)A_0).Tag;
			HyperLinkArea hyperLinkArea = this.m_ak.getHyperLinkArea(pageId, uid);
			int a_ = 0;
			if (hyperLinkArea != null)
			{
				if (hyperLinkArea.items.Count > 0)
				{
					a_ = b(hyperLinkArea.items[0].id);
				}
			}
			else
			{
				hyperLinkArea = this.m_ak.getHyperLinkAreaForFullText(pageId, uid);
				a_ = b(hyperLinkArea.items[0].id);
			}
			string a_2 = this.m_z + "\\HYWEB\\" + hyperLinkArea.items[0].href.Replace("/", "\\");
			a(hyperLinkArea.items[0].mediaType, a_2, a_);
		}

		private void a(string A_0, string A_1, int A_2)
		{
			if (!File.Exists(A_1) && !A_0.Equals("application/x-url"))
			{
				MessageBox.Show(a7.getLangString("fileNotDownloadedPls"), a7.getLangString("yetDownloadComplete"), MessageBoxButton.OK);
				return;
			}
			switch (global::n.a(A_0))
			{
			case 3901389917u:
				if (A_0 == "image/jpeg" && A_2 != -1)
				{
					c(A_2);
				}
				break;
			case 2953494330u:
				if (A_0 == "image/png")
				{
					Window window = new Window();
					BitmapImage bitmapImage = a(this.m_d, am, A_1, this.m_ad);
					window.Width = bitmapImage.PixelWidth;
					window.Height = bitmapImage.PixelHeight;
					window.Background = new ImageBrush(bitmapImage);
					window.Show();
				}
				break;
			case 2899107204u:
				if (A_0 == "application/pdf" && A_2 != -1)
				{
					c(A_2);
				}
				break;
			case 1639502448u:
				if (A_0 == "video/mp4")
				{
					new MoviePlayer(A_1, true).ShowDialog();
				}
				break;
			case 167271941u:
				if (A_0 == "application/hsd")
				{
					slideShow slideShow = new slideShow(this.m_x, am);
					slideShow.hsdFile = A_1;
					slideShow.ShowDialog();
				}
				break;
			case 1342579590u:
				if (A_0 == "application/x-url")
				{
					A_1 = A_1.Replace(this.m_z + "\\HYWEB\\", "");
					A_1 = A_1.Replace("\\", "/");
					Process.Start(new ProcessStartInfo(A_1));
				}
				break;
			case 3109025507u:
				if (A_0 == "audio/mpeg")
				{
					AudioPlayer audioPlayer = new AudioPlayer(A_1, bd, false);
					audioPlayer.Show();
					audioPlayer.Topmost = true;
				}
				break;
			case 1977116014u:
				if (A_0 == "text/html")
				{
					c(A_1);
				}
				break;
			case 3527136901u:
				if (A_0 == "text/plain")
				{
					c(A_1);
				}
				break;
			case 369145033u:
				if (A_0 == "application/x-mpegURL")
				{
					HlsPlayer hlsPlayer = new HlsPlayer(base.Title, this.m_u, this.m_v, this.m_z, am, A_1, this.m_ab, a0, a6);
					hlsPlayer.HlsPlayerDataRequest += new EventHandler<DataRequestEventArgs>(a);
					hlsPlayer.ShowDialog();
					hlsPlayer.HlsPlayerDataRequest -= new EventHandler<DataRequestEventArgs>(a);
					hlsPlayer.Dispose();
				}
				break;
			}
		}

		private void a(object A_0, DataRequestEventArgs A_1)
		{
			if (this.m_a != null)
			{
				this.m_a(this, A_1);
			}
		}

		private void c(string A_0)
		{
			Stream stream = this.m_d.fileAESDecode(A_0, false);
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				stream.CopyTo(memoryStream);
				array = memoryStream.ToArray();
			}
			Encoding encoding = Encoding.GetEncoding(950);
			Encoding encoding2 = (array.Length == encoding.GetByteCount(encoding.GetString(array))) ? Encoding.Default : Encoding.UTF8;
			stream.Position = 0L;
			StreamReader streamReader = new StreamReader(stream, encoding2);
			fullTextView fullTextView = new fullTextView(this.m_x);
			fullTextView.htmlString = streamReader.ReadToEnd();
			streamReader.Close();
			fullTextView.ShowDialog();
		}

		private int b(string A_0)
		{
			if (b0.Equals(PageMode.SinglePage))
			{
				for (int i = 0; i < this.m_aj.SImgList.Count; i++)
				{
					if (this.m_aj.SImgList[i].pageId == A_0)
					{
						return i;
					}
				}
			}
			else if (b0.Equals(PageMode.DoublePage))
			{
				for (int j = 0; j < this.m_aj.SImgList.Count; j++)
				{
					if (this.m_aj.SImgList[j].pageId == A_0)
					{
						return d(j);
					}
				}
			}
			return -1;
		}

		private StackPanel r()
		{
			g();
			StackPanel stackPanel = new StackPanel();
			TabControl tabControl = new TabControl();
			for (int i = 0; i < bd.Count; i++)
			{
				TabItem tabItem = new TabItem();
				tabItem.Header = bd[i].categoryName;
				tabItem.HeaderTemplate = (DataTemplate)FindResource("MediaListBoxHeaderTemplateStyle");
				if (!bd[i].mediaList.Count.Equals(0))
				{
					ListBox listBox = new ListBox();
					listBox.ItemsSource = bd[i].mediaList;
					listBox.Style = (Style)FindResource("MediaListBoxStyle");
					listBox.SelectionChanged += new SelectionChangedEventHandler(b);
					tabItem.Content = listBox;
					tabControl.Items.Add(tabItem);
					listBox = null;
				}
			}
			stackPanel.Children.Add(tabControl);
			tabControl = null;
			return stackPanel;
		}

		private void b(object A_0, SelectionChangedEventArgs A_1)
		{
			if (!((ListBox)A_0).SelectedIndex.Equals(-1))
			{
				g();
				string mediaSourcePath = ((Media)A_1.AddedItems[0]).mediaSourcePath;
				string mediaType = ((Media)A_1.AddedItems[0]).mediaType;
				a(mediaType, mediaSourcePath, -1);
				((ListBox)A_0).SelectedIndex = -1;
				A_1.Handled = true;
			}
		}

		private void ah(object A_0, RoutedEventArgs A_1)
		{
			try
			{
				g();
				int num = Convert.ToInt32(((RadioButton)A_0).Uid) - 1;
				if (b0.Equals(PageMode.DoublePage))
				{
					num = d(num);
				}
				if (num > -1)
				{
					c(num);
				}
			}
			catch
			{
			}
		}

		private void ag(object A_0, RoutedEventArgs A_1)
		{
			if (bx)
			{
				co.IsChecked = false;
				bx = false;
				cm.IsChecked = true;
				cq.ItemsSource = this.m_e;
				return;
			}
			if (by)
			{
				cn.IsChecked = false;
				by = false;
			}
			List<ThumbnailImageAndPage> list = new List<ThumbnailImageAndPage>();
			foreach (KeyValuePair<int, BookMarkData> item in an)
			{
				if (item.Value.status == "0")
				{
					list.Add(this.m_e[item.Key]);
				}
			}
			cq.ItemsSource = list;
			bx = true;
			co.IsChecked = true;
		}

		private void af(object A_0, RoutedEventArgs A_1)
		{
			if (by)
			{
				cn.IsChecked = false;
				by = false;
				cm.IsChecked = true;
				cq.ItemsSource = this.m_e;
				return;
			}
			if (bx)
			{
				co.IsChecked = false;
				bx = false;
			}
			List<ThumbnailImageAndPage> list = new List<ThumbnailImageAndPage>();
			foreach (KeyValuePair<int, NoteData> item in ao)
			{
				if (!item.Value.status.Equals("1"))
				{
					list.Add(this.m_e[item.Key]);
				}
			}
			foreach (KeyValuePair<int, List<StrokesData>> item2 in ap)
			{
				List<StrokesData> value = item2.Value;
				for (int i = 0; i < value.Count; i++)
				{
					if (value[i].status == "0")
					{
						list.Add(this.m_e[item2.Key]);
						break;
					}
				}
			}
			cq.ItemsSource = list;
			by = true;
			cn.IsChecked = true;
		}

		private void ae(object A_0, RoutedEventArgs A_1)
		{
			bx = false;
			by = false;
			cm.IsChecked = true;
			cq.ItemsSource = this.m_e;
		}

		private void ad(object A_0, RoutedEventArgs A_1)
		{
			RoutedCommand routedCommand = new RoutedCommand();
			routedCommand.InputGestures.Add(new KeyGesture(Key.Escape));
			Grid grid = FindVisualChildByName<Grid>(cg, "ToolBarInReader");
			RadioButton radioButton = FindVisualChildByName<RadioButton>(cg, "FullScreenButton");
			cs.IsChecked = false;
			bh = false;
			c();
			cs.Visibility = Visibility.Collapsed;
			base.Visibility = Visibility.Collapsed;
			base.WindowState = WindowState.Maximized;
			if (!bz)
			{
				base.CommandBindings.Add(new CommandBinding(routedCommand, new ExecutedRoutedEventHandler(ad)));
				base.WindowStyle = WindowStyle.None;
				base.Visibility = Visibility.Visible;
				grid.Visibility = Visibility.Collapsed;
				Canvas canvas = FindVisualChildByName<Canvas>(cg, "ToolBarSensor");
				canvas.Visibility = Visibility.Visible;
				canvas.IsMouseDirectlyOverChanged += new DependencyPropertyChangedEventHandler(a);
				radioButton.IsChecked = true;
				bz = true;
			}
			else
			{
				base.CommandBindings.Remove(base.CommandBindings[base.CommandBindings.Count - 1]);
				q();
				base.WindowStyle = WindowStyle.SingleBorderWindow;
				base.Visibility = Visibility.Visible;
				grid.Visibility = Visibility.Visible;
				Canvas canvas2 = FindVisualChildByName<Canvas>(cg, "ToolBarSensor");
				canvas2.Visibility = Visibility.Collapsed;
				canvas2.IsMouseDirectlyOverChanged -= new DependencyPropertyChangedEventHandler(a);
				grid.IsMouseDirectlyOverChanged -= new DependencyPropertyChangedEventHandler(a);
				radioButton.IsChecked = false;
				bz = false;
			}
			p();
			for (int i = 0; i < this.m_h.Count; i++)
			{
				if (this.m_h[i] == ImageStatus.GENERATING || this.m_h[i] == ImageStatus.LARGEIMAGE)
				{
					ReadPagePair readPagePair = this.m_j[i];
					if (readPagePair.leftImageSource != null)
					{
						readPagePair.leftImageSource = null;
						readPagePair.decodedPDFPages = new byte[2][];
						this.m_h[i] = ImageStatus.SMALLIMAGE;
					}
				}
			}
			for (int j = 0; j < this.m_g.Count; j++)
			{
				if (this.m_g[j] == ImageStatus.GENERATING || this.m_g[j] == ImageStatus.LARGEIMAGE)
				{
					ReadPagePair readPagePair2 = this.m_i[j];
					if (readPagePair2.leftImageSource != null)
					{
						readPagePair2.leftImageSource = null;
						readPagePair2.decodedPDFPages = new byte[2][];
						this.m_g[j] = ImageStatus.SMALLIMAGE;
					}
				}
			}
		}

		private void a(object A_0, DependencyPropertyChangedEventArgs A_1)
		{
			if ((bool)A_1.NewValue)
			{
				if (A_0 is Canvas)
				{
					Grid grid = FindVisualChildByName<Grid>(cg, "ToolBarInReader");
					grid.Visibility = Visibility.Visible;
					grid.IsMouseDirectlyOverChanged += new DependencyPropertyChangedEventHandler(a);
					Canvas canvas = FindVisualChildByName<Canvas>(cg, "ToolBarSensor");
					canvas.Visibility = Visibility.Collapsed;
					canvas.IsMouseDirectlyOverChanged -= new DependencyPropertyChangedEventHandler(a);
				}
			}
			else if (A_0 is Grid)
			{
				Grid grid2 = FindVisualChildByName<Grid>(cg, "ToolBarInReader");
				grid2.Visibility = Visibility.Collapsed;
				grid2.IsMouseDirectlyOverChanged -= new DependencyPropertyChangedEventHandler(a);
				Canvas canvas2 = FindVisualChildByName<Canvas>(cg, "ToolBarSensor");
				canvas2.Visibility = Visibility.Visible;
				canvas2.IsMouseDirectlyOverChanged += new DependencyPropertyChangedEventHandler(a);
			}
		}

		private void ac(object A_0, RoutedEventArgs A_1)
		{
			FindVisualChildByName<RadioButton>(cg, "PageViewButton").IsChecked = true;
			a(PageMode.SinglePage);
		}

		private void ab(object A_0, RoutedEventArgs A_1)
		{
			g().Visibility = Visibility.Collapsed;
			a(PageMode.DoublePage);
		}

		private void q()
		{
			base.Width = SystemParameters.PrimaryScreenWidth;
			base.Height = SystemParameters.PrimaryScreenHeight - 40.0;
			base.Left = 0.0;
			base.Top = 0.0;
			base.WindowState = WindowState.Normal;
		}

		private void a(PageMode A_0)
		{
			if (b0.Equals(A_0))
			{
				return;
			}
			c();
			b0 = A_0;
			p();
			cs.IsChecked = false;
			bh = false;
			cs.Visibility = Visibility.Collapsed;
			bool flag = false;
			if (this.m_r > 0)
			{
				flag = true;
			}
			RadioButton radioButton = FindVisualChildByName<RadioButton>(cg, "NoteButton");
			RadioButton radioButton2 = FindVisualChildByName<RadioButton>(cg, "ShareButton");
			FindVisualChildByName<RadioButton>(cg, "PenMemoButton");
			FindVisualChildByName<InkCanvas>(cg, "penMemoCanvas");
			Canvas canvas = FindVisualChildByName<Canvas>(cg, "zoomCanvas");
			BrushConverter brushConverter = new BrushConverter();
			int num = 0;
			switch (b0)
			{
			case PageMode.SinglePage:
			{
				num = e(this.m_p);
				int num2 = num;
				if (num != 0 && num != this.m_b.Blocks.Count - 1)
				{
					num--;
				}
				int num3 = 0;
				for (int i = 0; i < this.m_g.Count; i++)
				{
					if (this.m_g[i] == ImageStatus.LARGEIMAGE)
					{
						num3 += i;
					}
				}
				int num4 = num3 / 3;
				if (num4 == num || num4 == num2)
				{
					num = num4;
				}
				cg.Document = this.m_b;
				if (num.Equals(0))
				{
					if (this.m_i[this.m_p].leftImageSource != null)
					{
						bg = true;
						try
						{
							b((BitmapImage)this.m_i[this.m_p].leftImageSource);
							if (b7)
							{
								a(this.m_i[this.m_p].leftPageIndex, -1, am, canvas);
							}
							f(this.m_aj.LImgList[this.m_i[this.m_p].leftPageIndex].pageId);
							a(this.m_i[this.m_p].leftPageIndex);
						}
						catch (Exception)
						{
							this.m_i[this.m_p].leftImageSource = null;
						}
					}
				}
				else
				{
					canvas.Background = (System.Windows.Media.Brush)brushConverter.ConvertFrom("#FF212020");
				}
				c(num);
				this.m_p = num;
				if (radioButton.Visibility.Equals(Visibility.Collapsed) && !flag)
				{
					radioButton.Visibility = Visibility.Visible;
				}
				if (radioButton2.Visibility.Equals(Visibility.Collapsed))
				{
					if (az)
					{
						radioButton2.Visibility = Visibility.Visible;
					}
				}
				else if (!az)
				{
					radioButton2.Visibility = Visibility.Collapsed;
				}
				break;
			}
			case PageMode.DoublePage:
				num = d(this.m_p);
				cg.Document = this.m_c;
				if (num.Equals(0))
				{
					if (this.m_j[this.m_p].leftImageSource != null)
					{
						bg = true;
						try
						{
							b((BitmapImage)this.m_j[this.m_p].leftImageSource);
							if (this.m_j[this.m_p].rightPageIndex == -1)
							{
								if (b7)
								{
									a(this.m_j[this.m_p].leftPageIndex, -1, am, canvas);
								}
								f(this.m_aj.LImgList[this.m_j[this.m_p].leftPageIndex].pageId);
								a(this.m_i[this.m_p].leftPageIndex);
							}
							else
							{
								if (b7)
								{
									a(this.m_j[this.m_p].leftPageIndex, this.m_j[this.m_p].rightPageIndex, am, canvas);
								}
								a(this.m_aj.LImgList[this.m_j[this.m_p].leftPageIndex].pageId, this.m_aj.LImgList[this.m_j[this.m_p].rightPageIndex].pageId);
								a(this.m_j[this.m_p].leftPageIndex, this.m_j[this.m_p].rightPageIndex);
							}
						}
						catch (Exception)
						{
							this.m_j[this.m_p].leftImageSource = null;
						}
					}
				}
				else
				{
					canvas.Background = (System.Windows.Media.Brush)brushConverter.ConvertFrom("#FF212020");
				}
				c(num);
				this.m_p = num;
				if (radioButton.Visibility.Equals(Visibility.Collapsed) && !flag)
				{
					radioButton.Visibility = Visibility.Visible;
				}
				if (radioButton2.Visibility.Equals(Visibility.Visible) && !az)
				{
					radioButton2.Visibility = Visibility.Collapsed;
				}
				break;
			}
			if (bz)
			{
				Grid grid = FindVisualChildByName<Grid>(cg, "ToolBarInReader");
				grid.Visibility = Visibility.Collapsed;
				grid.IsMouseDirectlyOverChanged -= new DependencyPropertyChangedEventHandler(a);
				Canvas canvas2 = FindVisualChildByName<Canvas>(cg, "ToolBarSensor");
				canvas2.Visibility = Visibility.Visible;
				canvas2.IsMouseDirectlyOverChanged += new DependencyPropertyChangedEventHandler(a);
				p();
			}
			if (a9.Equals("NCLReader") || a9.Equals("HyReadCN"))
			{
				radioButton2.Visibility = Visibility.Collapsed;
			}
		}

		private void p()
		{
			Canvas canvas = i();
			bi = false;
			if (canvas.Children.Count > 0)
			{
				canvas.Children.Clear();
				FindVisualChildByName<RadioButton>(cg, "FullTextButton").Visibility = Visibility.Collapsed;
			}
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(cg, "penMemoCanvas");
			if (inkCanvas.Strokes.Count > 0)
			{
				inkCanvas.Strokes.Clear();
			}
			FindVisualChildByName<Canvas>(cg, "zoomCanvas").Background = null;
		}

		private void aa(object A_0, RoutedEventArgs A_1)
		{
			HyperLinkArea hyperLinkArea = ((PageInfoMetadata)((RadioButton)A_0).Tag).hyperLinkAreas[0];
			string a_ = this.m_z + "\\HYWEB\\" + hyperLinkArea.items[0].href.Replace("/", "\\");
			a(hyperLinkArea.items[0].mediaType, a_, -1);
		}

		private void z(object A_0, RoutedEventArgs A_1)
		{
			if (bh.Equals(false))
			{
				cs.IsChecked = true;
				bh = true;
				return;
			}
			cs.IsChecked = false;
			bh = false;
			c();
			cs.Visibility = Visibility.Collapsed;
		}

		private void y(object A_0, RoutedEventArgs A_1)
		{
			RadioButton radioButton = (RadioButton)A_0;
			if (b0.Equals(PageMode.SinglePage))
			{
				bool flag = false;
				if (an.ContainsKey(this.m_p) && an[this.m_p].status == "0")
				{
					flag = true;
				}
				a(this.m_p, !flag);
				radioButton.IsChecked = !flag;
			}
			else
			{
				if (!b0.Equals(PageMode.DoublePage))
				{
					return;
				}
				ReadPagePair readPagePair = this.m_j[this.m_p];
				if (readPagePair.rightPageIndex == -1 || readPagePair.leftPageIndex == -1)
				{
					bool flag2 = false;
					int num = Math.Max(readPagePair.rightPageIndex, readPagePair.leftPageIndex);
					if (an.ContainsKey(num) && an[num].status == "0")
					{
						flag2 = true;
					}
					a(num, !flag2);
					radioButton.IsChecked = !flag2;
					return;
				}
				bool flag3 = false;
				bool flag4 = false;
				if (an.ContainsKey(readPagePair.leftPageIndex) && an[readPagePair.leftPageIndex].status == "0")
				{
					flag3 = true;
				}
				if (an.ContainsKey(readPagePair.rightPageIndex) && an[readPagePair.rightPageIndex].status == "0")
				{
					flag4 = true;
				}
				if (flag3 || flag4)
				{
					if (flag3)
					{
						a(readPagePair.leftPageIndex, false);
					}
					if (flag4)
					{
						a(readPagePair.rightPageIndex, false);
					}
					radioButton.IsChecked = false;
				}
				else
				{
					a(readPagePair.rightPageIndex, true);
					radioButton.IsChecked = true;
				}
			}
		}

		private void a(int A_0, bool A_1)
		{
			DateTime value = new DateTime(1970, 1, 1);
			long num = DateTime.Now.ToUniversalTime().Subtract(value).Ticks / 10000000;
			BookMarkData bookMarkData = null;
			if (an.ContainsKey(A_0))
			{
				bookMarkData = an[A_0];
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
				bookMarkData.index = A_0;
				bookMarkData.status = "0";
				bookMarkData.synctime = 0L;
				bookMarkData.objectId = "";
				an.Add(A_0, bookMarkData);
			}
			a6.saveBookMarkData(this.m_ab, A_1, bookMarkData);
		}

		private void x(object A_0, RoutedEventArgs A_1)
		{
			a(MediaCanvasOpenedBy.MediaButton, A_0);
		}

		private void w(object A_0, RoutedEventArgs A_1)
		{
			a(MediaCanvasOpenedBy.SearchButton, A_0);
		}

		private void v(object A_0, RoutedEventArgs A_1)
		{
			a(MediaCanvasOpenedBy.CategoryButton, A_0);
		}

		private void u(object A_0, RoutedEventArgs A_1)
		{
			if (b0.Equals(PageMode.DoublePage))
			{
				RadioButton radioButton = (RadioButton)A_0;
				if (!MessageBox.Show(a7.getLangString("doublePageNoteModeAlert"), a7.getLangString("note"), MessageBoxButton.YesNo).Equals(MessageBoxResult.Yes))
				{
					radioButton.IsChecked = false;
					return;
				}
				ac(A_0, A_1);
			}
			a(MediaCanvasOpenedBy.NoteButton, A_0);
		}

		private void t(object A_0, RoutedEventArgs A_1)
		{
			if (b0.Equals(PageMode.SinglePage))
			{
				a(MediaCanvasOpenedBy.ShareButton, A_0);
			}
			else if (b0.Equals(PageMode.DoublePage))
			{
				RadioButton radioButton = (RadioButton)A_0;
				if (MessageBox.Show(a7.getLangString("doublePageShareModeAlert"), a7.getLangString("share"), MessageBoxButton.YesNo).Equals(MessageBoxResult.Yes))
				{
					ac(A_0, A_1);
					a(MediaCanvasOpenedBy.ShareButton, A_0);
				}
				else
				{
					radioButton.IsChecked = false;
				}
			}
		}

		private void s(object A_0, RoutedEventArgs A_1)
		{
			a(MediaCanvasOpenedBy.SettingButton, A_0);
		}

		private void a(MediaCanvasOpenedBy A_0, object A_1)
		{
			Canvas canvas = g();
			StackPanel stackPanel = f();
			if (b1.Equals(A_0))
			{
				if (canvas.Visibility.Equals(Visibility.Visible))
				{
					if (!A_0.Equals(MediaCanvasOpenedBy.NoteButton))
					{
						((RadioButton)A_1).IsChecked = false;
					}
					else if (A_0.Equals(MediaCanvasOpenedBy.NoteButton))
					{
						TextBox textBox = FindVisualChildByName<TextBox>(cg, "notePanel");
						if (textBox != null)
						{
							int a_ = this.m_p;
							a(textBox.Text, a_);
							RadioButton radioButton = FindVisualChildByName<RadioButton>(cg, "NoteButton");
							if (textBox.Text.Equals(""))
							{
								radioButton.IsChecked = false;
							}
							else
							{
								radioButton.IsChecked = true;
							}
						}
					}
					canvas.Visibility = Visibility.Collapsed;
				}
				else
				{
					if (A_0.Equals(MediaCanvasOpenedBy.NoteButton))
					{
						TextBox textBox2 = FindVisualChildByName<TextBox>(cg, "notePanel");
						if (textBox2 != null)
						{
							int num = this.m_p;
							a(textBox2.Text, num);
							textBox2.Text = (ao.ContainsKey(num) ? ao[num].text : "");
							RadioButton radioButton2 = FindVisualChildByName<RadioButton>(cg, "NoteButton");
							if (textBox2.Text.Equals(""))
							{
								radioButton2.IsChecked = false;
							}
							else
							{
								radioButton2.IsChecked = true;
							}
						}
					}
					canvas.Visibility = Visibility.Visible;
				}
				o();
				return;
			}
			string text = "";
			switch (b1)
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
				FindVisualChildByName<RadioButton>(cg, text).IsChecked = false;
			}
			b2 = this.m_p;
			stackPanel.Children.Clear();
			if (b3.ContainsKey(A_0) && !A_0.Equals(MediaCanvasOpenedBy.NoteButton))
			{
				stackPanel.Children.Add(b3[A_0]);
			}
			else
			{
				StackPanel value = new StackPanel();
				double width = stackPanel.Width;
				switch (A_0)
				{
				case MediaCanvasOpenedBy.SearchButton:
					value = a(width, "");
					break;
				case MediaCanvasOpenedBy.MediaButton:
					value = r();
					break;
				case MediaCanvasOpenedBy.CategoryButton:
					value = k();
					break;
				case MediaCanvasOpenedBy.NoteButton:
					value = l();
					break;
				case MediaCanvasOpenedBy.ShareButton:
					value = b();
					break;
				case MediaCanvasOpenedBy.SettingButton:
					value = m();
					break;
				}
				if (b3.ContainsKey(A_0))
				{
					b3[A_0] = value;
				}
				else
				{
					b3.Add(A_0, value);
				}
				stackPanel.Children.Clear();
				stackPanel.Children.Add(b3[A_0]);
			}
			canvas.Visibility = Visibility.Visible;
			b1 = A_0;
			o();
		}

		private void r(object A_0, RoutedEventArgs A_1)
		{
			if (b5)
			{
				ci.Visibility = Visibility.Hidden;
			}
			Canvas canvas = g();
			if (canvas.Visibility.Equals(Visibility.Visible))
			{
				canvas.Visibility = Visibility.Collapsed;
			}
			int num = this.m_aj.tocPageIndex - 1;
			if (b0.Equals(PageMode.DoublePage))
			{
				num = d(num);
			}
			if (num != -1)
			{
				c(num);
			}
		}

		private void c(object A_0, MouseButtonEventArgs A_1)
		{
			((Canvas)A_0).Visibility = Visibility.Collapsed;
			string text = "";
			switch (b1)
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
				FindVisualChildByName<RadioButton>(cg, text).IsChecked = false;
			}
			else if (text.Equals("NoteButton"))
			{
				TextBox textBox = FindVisualChildByName<TextBox>(cg, "notePanel");
				int a_ = this.m_p;
				a(textBox.Text, a_);
				RadioButton radioButton = FindVisualChildByName<RadioButton>(cg, "NoteButton");
				if (textBox.Text.Equals(""))
				{
					radioButton.IsChecked = false;
				}
				else
				{
					radioButton.IsChecked = true;
				}
			}
			o();
		}

		private void o()
		{
			if (bp != null && bp.Focusable && bp.IsEnabled && !bp.IsKeyboardFocused)
			{
				Keyboard.Focus(bp);
			}
		}

		private void q(object A_0, RoutedEventArgs A_1)
		{
			RadioButton radioButton = (RadioButton)A_0;
			if (b0.Equals(PageMode.DoublePage))
			{
				if (!MessageBox.Show(a7.getLangString("doublePageStrokeModeAlert"), a7.getLangString("strokeMode"), MessageBoxButton.YesNo).Equals(MessageBoxResult.Yes))
				{
					radioButton.IsChecked = false;
					return;
				}
				ac(A_0, A_1);
			}
			b1 = MediaCanvasOpenedBy.PenMemo;
			Grid grid = FindVisualChildByName<Grid>(cg, "ToolBarInReader");
			Grid grid2 = FindVisualChildByName<Grid>(cg, "PenMemoToolBar");
			InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(cg, "penMemoCanvas");
			StrokeToolPanelHorizontal strokeToolPanelHorizontal = new StrokeToolPanelHorizontal();
			strokeToolPanelHorizontal.langMng = a7;
			Canvas element = FindVisualChildByName<Canvas>(cg, "zoomCanvas");
			Canvas element2 = i();
			if (grid2.Visibility.Equals(Visibility.Collapsed))
			{
				grid.Visibility = Visibility.Collapsed;
				grid2.Visibility = Visibility.Visible;
				radioButton.IsChecked = false;
				strokeToolPanelHorizontal.determineDrawAtt(inkCanvas.DefaultDrawingAttributes, ax);
				Panel.SetZIndex(inkCanvas, 900);
				Panel.SetZIndex(element2, 2);
				Panel.SetZIndex(element, 850);
				inkCanvas.Background = System.Windows.Media.Brushes.Transparent;
				inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
				inkCanvas.Visibility = Visibility.Visible;
				strokeToolPanelHorizontal.HorizontalAlignment = HorizontalAlignment.Right;
				grid2.Children.Add(strokeToolPanelHorizontal);
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
				inkCanvas.Focus();
				Canvas canvas = FindVisualChildByName<Canvas>(cg, "HiddenControlCanvas");
				if (canvas.Visibility.Equals(Visibility.Collapsed))
				{
					canvas.Visibility = Visibility.Visible;
				}
				Keyboard.ClearFocus();
				a(0.5, false);
				if (ax)
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
				Panel.SetZIndex(element2, 3);
				((RadioButton)A_0).IsChecked = false;
				inkCanvas.EditingMode = InkCanvasEditingMode.None;
				alterPenmemoAnimation(strokeToolPanelHorizontal, strokeToolPanelHorizontal.Width, 0.0);
				d(this.m_aj.LImgList[this.m_p].pageId);
				grid2.Children.Remove(grid2.Children[grid2.Children.Count - 1]);
				Canvas canvas2 = FindVisualChildByName<Canvas>(cg, "PopupControlCanvas");
				if (canvas2.Visibility.Equals(Visibility.Visible))
				{
					canvas2.Visibility = Visibility.Collapsed;
				}
				Canvas canvas3 = FindVisualChildByName<Canvas>(cg, "HiddenControlCanvas");
				if (canvas3.Visibility.Equals(Visibility.Visible))
				{
					canvas3.Visibility = Visibility.Collapsed;
				}
				grid2.Visibility = Visibility.Collapsed;
				grid.Visibility = Visibility.Visible;
				a(1.0, true);
				o();
			}
		}

		private void a(double A_0, bool A_1)
		{
			RadioButton radioButton = FindVisualChildByName<RadioButton>(cg, "leftPageButton");
			RadioButton radioButton2 = FindVisualChildByName<RadioButton>(cg, "rightPageButton");
			radioButton.Opacity = A_0;
			radioButton2.Opacity = A_0;
			cs.IsEnabled = A_1;
			cr.IsEnabled = base.IsEnabled;
		}

		private void p(object A_0, RoutedEventArgs A_1)
		{
			Close();
		}

		private void n()
		{
			Canvas canvas = g();
			if (canvas.Visibility.Equals(Visibility.Visible))
			{
				canvas.Visibility = Visibility.Collapsed;
			}
			RadioButton radioButton = FindVisualChildByName<RadioButton>(cg, "ShowAllImageButton");
			ScrollViewer scrollViewer = FindVisualChildByName<ScrollViewer>(cq, "SVInLV");
			FindVisualChildByName<WrapPanel>(cg, "wrapPanel");
			bx = false;
			by = false;
			cm.IsChecked = true;
			cq.ItemsSource = this.m_e;
			switch (b4)
			{
			case 0:
				b5 = false;
				ci.Visibility = Visibility.Hidden;
				cr.Visibility = Visibility.Visible;
				radioButton.IsChecked = false;
				cm.IsChecked = true;
				if (!ch.Visibility.Equals(Visibility.Collapsed))
				{
					ch.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
				}
				cs.Margin = new Thickness(0.0, 0.0, 15.0, 15.0);
				break;
			case 1:
			{
				b5 = false;
				Binding binding4 = new Binding();
				binding4.Source = cg;
				binding4.Path = new PropertyPath("ActualWidth");
				binding4.Converter = new thumbNailListBoxWidthHeightConverter();
				binding4.ConverterParameter = 30;
				cq.SetBinding(FrameworkElement.WidthProperty, binding4);
				cq.Height = b6;
				ci.Height = b6;
				scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
				scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
				cp.ToolTip = a7.getLangString("hideThumbnails");
				cj.Orientation = Orientation.Horizontal;
				cl.Orientation = Orientation.Vertical;
				ck.HorizontalAlignment = HorizontalAlignment.Center;
				ci.Visibility = Visibility.Visible;
				radioButton.IsChecked = false;
				cr.Visibility = Visibility.Hidden;
				if (!ch.Visibility.Equals(Visibility.Collapsed))
				{
					ch.Margin = new Thickness(0.0, 0.0, 0.0, b6);
				}
				cs.Margin = new Thickness(0.0, 0.0, 15.0, 15.0 + b6);
				break;
			}
			case 2:
			{
				b5 = true;
				Binding binding = new Binding();
				binding.Source = cg;
				binding.Path = new PropertyPath("ActualHeight");
				ci.SetBinding(FrameworkElement.HeightProperty, binding);
				Binding binding2 = new Binding();
				binding2.Source = cg;
				binding2.Path = new PropertyPath("ActualWidth");
				Binding binding3 = new Binding();
				binding3.Source = cg;
				binding3.Path = new PropertyPath("ActualHeight");
				binding3.Converter = new thumbNailListBoxWidthHeightConverter();
				binding3.ConverterParameter = 30;
				cq.SetBinding(FrameworkElement.HeightProperty, binding3);
				cq.SetBinding(FrameworkElement.WidthProperty, binding2);
				scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
				scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
				cj.Orientation = Orientation.Vertical;
				cl.Orientation = Orientation.Horizontal;
				ck.HorizontalAlignment = HorizontalAlignment.Right;
				ci.Visibility = Visibility.Visible;
				radioButton.IsChecked = true;
				cr.Visibility = Visibility.Hidden;
				cp.ToolTip = a7.getLangString("closeThumbnail");
				if (!ch.Visibility.Equals(Visibility.Collapsed))
				{
					ch.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
				}
				cs.Margin = new Thickness(0.0, 0.0, 15.0, 15.0);
				break;
			}
			}
		}

		private void o(object A_0, RoutedEventArgs A_1)
		{
			b4 = 2;
			n();
		}

		private void n(object A_0, RoutedEventArgs A_1)
		{
			b4 = 1;
			n();
		}

		private void m(object A_0, RoutedEventArgs A_1)
		{
			b4 = 0;
			n();
		}

		private StackPanel m()
		{
			List<TextBlock> list = new List<TextBlock>();
			if (!a9.Equals("NCLReader"))
			{
				TextBlock textBlock = new TextBlock();
				textBlock.Text = a7.getLangString("showMultimediaSensor");
				textBlock.FontSize = 14.0;
				list.Add(textBlock);
			}
			TextBlock textBlock2 = new TextBlock();
			textBlock2.Text = a7.getLangString("showPageButton");
			textBlock2.FontSize = 14.0;
			list.Add(textBlock2);
			List<bool> list2 = new List<bool>();
			list2.Add(true);
			list2.Add(true);
			List<bool> list3 = list2;
			StackPanel stackPanel = new StackPanel();
			stackPanel.Margin = new Thickness(20.0, 10.0, 20.0, 10.0);
			stackPanel.Orientation = Orientation.Vertical;
			for (int i = 0; i < list.Count; i++)
			{
				Grid grid = new Grid();
				grid.HorizontalAlignment = HorizontalAlignment.Left;
				grid.Margin = new Thickness(0.0, 0.0, 0.0, 10.0);
				CheckBox checkBox = new CheckBox();
				checkBox.Content = list[i];
				checkBox.IsChecked = list3[i];
				CheckBox checkBox2 = checkBox;
				switch (i)
				{
				case 0:
					checkBox2.Click += new RoutedEventHandler(k);
					break;
				case 1:
					checkBox2.Click += new RoutedEventHandler(l);
					break;
				}
				grid.Children.Add(checkBox2);
				stackPanel.Children.Add(grid);
			}
			stackPanel.Orientation = Orientation.Vertical;
			return stackPanel;
		}

		private void l(object A_0, RoutedEventArgs A_1)
		{
			CheckBox checkBox = (CheckBox)A_0;
			RadioButton radioButton = FindVisualChildByName<RadioButton>(cg, "leftPageButton");
			RadioButton radioButton2 = FindVisualChildByName<RadioButton>(cg, "rightPageButton");
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
			o();
		}

		private void k(object A_0, RoutedEventArgs A_1)
		{
			CheckBox checkBox = (CheckBox)A_0;
			if (checkBox.IsChecked.Equals(true))
			{
				b7 = true;
				byte[] a_ = am;
				Canvas a_2 = FindVisualChildByName<Canvas>(cg, "zoomCanvas");
				if (b0.Equals(PageMode.SinglePage))
				{
					a(this.m_p, -1, a_, a_2);
				}
				else if (b0.Equals(PageMode.DoublePage))
				{
					int a_3 = this.m_p;
					if (a_3.Equals(0) || a_3.Equals(this.m_e.Count - 1))
					{
						a(this.m_p, -1, a_, a_2);
					}
					else
					{
						a_3 = e(a_3);
						int a_4 = a_3 - 1;
						int a_5 = a_3;
						if (this.m_aj.direction.Equals("right"))
						{
							a_4 = a_3;
							a_5 = a_3 - 1;
						}
						a(a_4, a_5, a_, a_2);
					}
				}
				a_ = null;
			}
			else if (checkBox.IsChecked.Equals(false))
			{
				b7 = false;
				Canvas canvas = i();
				if (canvas.Children.Count > 0)
				{
					canvas.Children.Clear();
				}
			}
			o();
		}

		private StackPanel l()
		{
			StackPanel stackPanel = f();
			double width = stackPanel.Width;
			double height = stackPanel.Height;
			FindVisualChildByName<Border>(cg, "mediaListBorder");
			double width2 = 100.0;
			double num = 20.0;
			string text = ao.ContainsKey(this.m_p) ? ao[this.m_p].text : "";
			StackPanel stackPanel2 = new StackPanel();
			TextBox textBox = new TextBox();
			textBox.Name = "notePanel";
			textBox.TextWrapping = TextWrapping.WrapWithOverflow;
			textBox.AcceptsReturn = true;
			textBox.BorderBrush = System.Windows.Media.Brushes.White;
			textBox.Margin = new Thickness(2.0);
			textBox.Width = width - 4.0;
			textBox.Height = height - num - 8.0;
			textBox.Text = text;
			textBox.FontSize = 14.0;
			TextBox textBox2 = textBox;
			textBox2.KeyDown += new KeyEventHandler(c);
			RadioButton radioButton = new RadioButton();
			TextBlock textBlock = new TextBlock();
			textBlock.VerticalAlignment = VerticalAlignment.Center;
			textBlock.HorizontalAlignment = HorizontalAlignment.Center;
			textBlock.Foreground = System.Windows.Media.Brushes.White;
			textBlock.Text = a7.getLangString("save");
			radioButton.Content = textBlock;
			radioButton.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/mainWindow/header_bg.png", UriKind.RelativeOrAbsolute)));
			radioButton.Margin = new Thickness(2.0);
			radioButton.Width = width2;
			radioButton.Height = num;
			RadioButton radioButton2 = radioButton;
			radioButton2.Click += new RoutedEventHandler(j);
			stackPanel2.Children.Add(textBox2);
			stackPanel2.Children.Add(radioButton2);
			stackPanel2.Orientation = Orientation.Vertical;
			return stackPanel2;
		}

		private void c(object A_0, KeyEventArgs A_1)
		{
			if (A_1.Key == Key.Return)
			{
				((TextBox)A_0).Text += "\r\n";
			}
		}

		private void j(object A_0, RoutedEventArgs A_1)
		{
			f();
			TextBox textBox = FindVisualChildByName<TextBox>(cg, "notePanel");
			int a_ = this.m_p;
			a(textBox.Text, a_);
			RadioButton radioButton = FindVisualChildByName<RadioButton>(cg, "NoteButton");
			if (textBox.Text.Equals(""))
			{
				radioButton.IsChecked = false;
			}
			else
			{
				radioButton.IsChecked = true;
			}
			g().Visibility = Visibility.Collapsed;
		}

		private bool a(string A_0, int A_1)
		{
			bool result = false;
			DateTime value = new DateTime(1970, 1, 1);
			long num = DateTime.Now.ToUniversalTime().Subtract(value).Ticks / 10000000;
			bool flag = false;
			NoteData noteData = null;
			if (ao.ContainsKey(this.m_p))
			{
				noteData = ao[A_1];
				if (noteData.text == A_0)
				{
					return result;
				}
				noteData.text = A_0;
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
				if (A_0 == "")
				{
					return result;
				}
				noteData = new NoteData();
				noteData.objectId = "";
				noteData.createtime = num;
				noteData.updatetime = num;
				noteData.text = A_0;
				noteData.index = A_1;
				noteData.status = "0";
				noteData.synctime = 0L;
				ao.Add(A_1, noteData);
				flag = false;
			}
			a6.saveNoteData(this.m_ab, flag, noteData);
			return true;
		}

		private StackPanel a(double A_0, string A_1)
		{
			StackPanel stackPanel = new StackPanel();
			RadioButton radioButton = new RadioButton();
			TextBlock textBlock = new TextBlock();
			textBlock.VerticalAlignment = VerticalAlignment.Center;
			textBlock.HorizontalAlignment = HorizontalAlignment.Center;
			textBlock.Foreground = System.Windows.Media.Brushes.White;
			textBlock.Text = a7.getLangString("search");
			radioButton.Content = textBlock;
			radioButton.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/mainWindow/header_bg.png", UriKind.RelativeOrAbsolute)));
			radioButton.Margin = new Thickness(6.0);
			radioButton.Width = 61.0;
			RadioButton radioButton2 = radioButton;
			radioButton2.Click += new RoutedEventHandler(i);
			TextBox textBox = new TextBox();
			textBox.Name = "searchBar";
			textBox.Text = A_1;
			textBox.Margin = new Thickness(6.0);
			textBox.Width = A_0 - 82.0;
			TextBox textBox2 = textBox;
			textBox2.KeyDown += new KeyEventHandler(b);
			stackPanel.Children.Add(textBox2);
			stackPanel.Children.Add(radioButton2);
			stackPanel.Orientation = Orientation.Horizontal;
			stackPanel.Background = System.Windows.Media.Brushes.LightGray;
			return stackPanel;
		}

		private void b(object A_0, KeyEventArgs A_1)
		{
			if (A_1.Key == Key.Return)
			{
				StackPanel stackPanel = f();
				string text = FindVisualChildByName<TextBox>(cg, "searchBar").Text;
				double width = stackPanel.Width;
				stackPanel.Children.Clear();
				StackPanel element = a(width, text);
				ListBox element2 = a(text);
				StackPanel stackPanel2 = new StackPanel();
				stackPanel2.Children.Add(element);
				stackPanel2.Children.Add(element2);
				b3[MediaCanvasOpenedBy.SearchButton] = stackPanel2;
				stackPanel.Children.Add(stackPanel2);
			}
		}

		private void i(object A_0, RoutedEventArgs A_1)
		{
			StackPanel stackPanel = f();
			string text = FindVisualChildByName<TextBox>(cg, "searchBar").Text;
			double width = stackPanel.Width;
			stackPanel.Children.Clear();
			StackPanel element = a(width, text);
			ListBox element2 = a(text);
			StackPanel stackPanel2 = new StackPanel();
			stackPanel2.Children.Add(element);
			stackPanel2.Children.Add(element2);
			b3[MediaCanvasOpenedBy.SearchButton] = stackPanel2;
			stackPanel.Children.Add(stackPanel2);
		}

		private ListBox a(string A_0)
		{
			HyftdTools hyftdTools = new HyftdTools();
			string text = this.m_z + "\\HYWEB";
			text = text.Replace(Directory.GetCurrentDirectory(), ".");
			string[] directories = Directory.GetDirectories(text);
			string text2 = "";
			for (int i = 0; i < directories.Length; i++)
			{
				if (directories[i].Replace(text + "\\", "").StartsWith("ebook") && !directories[i].Replace(text + "\\", "").EndsWith(".work"))
				{
					text2 = directories[i].Replace(text + "\\", "");
				}
			}
			if (text2.Equals(""))
			{
				text = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\" + a9 + "\\HyftdIndex\\" + this.m_z.Substring(this.m_z.IndexOf(this.m_u), this.m_z.Length - this.m_z.IndexOf(this.m_u));
				directories = Directory.GetDirectories(text);
				for (int j = 0; j < directories.Length; j++)
				{
					if (directories[j].Replace(text + "\\", "").StartsWith("ebook") && !directories[j].Replace(text + "\\", "").EndsWith(".work"))
					{
						text2 = directories[j].Replace(text + "\\", "");
					}
				}
			}
			string hyftdLibPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\" + a9 + "\\HyftdLib";
			hyftdTools.register(hyftdLibPath, text, text2);
			hyftdTools.addQuery(A_0);
			new List<hyftdResultRecord>();
			List<hyftdResultRecord> resultRecord = hyftdTools.getResultRecord();
			ListBox listBox = new ListBox();
			listBox.Style = (Style)FindResource("SearchListBoxStyle");
			List<SearchRecord> list = new List<SearchRecord>();
			foreach (hyftdResultRecord item in resultRecord)
			{
				list.Add(new SearchRecord(item.pagelabel, item.content, item.page));
			}
			for (int k = 0; k < list.Count; k++)
			{
				for (int l = 0; l < this.m_aj.SImgList.Count; l++)
				{
					if (list[k].showedPage.Equals(this.m_aj.SImgList[l].pageNum))
					{
						list[k].imagePath = this.m_z + "\\" + this.m_aj.SImgList[l].path;
						if (this.m_aj.SImgList[l].path.Contains("tryPageEnd"))
						{
							list[k].imagePath = this.m_aj.SImgList[l].path;
						}
					}
				}
			}
			listBox.ItemsSource = list;
			listBox.SelectionChanged += new SelectionChangedEventHandler(a);
			return listBox;
		}

		private void a(object A_0, SelectionChangedEventArgs A_1)
		{
			ListBox listBox = (ListBox)A_0;
			if (listBox.SelectedIndex != -1)
			{
				int a_ = ((SearchRecord)A_1.AddedItems[0]).targetPage - 1;
				if (b0.Equals(PageMode.DoublePage))
				{
					a_ = d(a_);
				}
				if (!a_.Equals(-1))
				{
					c(a_);
				}
				listBox.SelectedIndex = -1;
			}
		}

		private StackPanel k()
		{
			StackPanel stackPanel = f();
			StackPanel stackPanel2 = new StackPanel();
			TreeView treeView = new TreeView();
			double height = stackPanel.Height;
			double num = stackPanel2.Width = stackPanel.Width;
			treeView.ItemsSource = ba.TocContent;
			treeView.Height = height;
			treeView.Style = (Style)FindResource("ContentTreeViewStyle");
			treeView.BorderBrush = System.Windows.Media.Brushes.White;
			stackPanel2.Children.Clear();
			stackPanel2.Children.Add(treeView);
			return stackPanel2;
		}

		private void h(object A_0, RoutedEventArgs A_1)
		{
			navPoint navPoint = (navPoint)((Button)A_0).Tag;
			int num = -1;
			if (b0.Equals(PageMode.SinglePage))
			{
				num = navPoint.targetIndex;
			}
			else if (b0.Equals(PageMode.DoublePage))
			{
				num = d(navPoint.targetIndex);
			}
			if (num != -1)
			{
				c(num);
			}
		}

		private void a(object A_0, RequestBringIntoViewEventArgs A_1)
		{
			A_1.Handled = true;
		}

		private void g(object A_0, RoutedEventArgs A_1)
		{
			if (this.m_af != 0)
			{
				this.m_af--;
				a(this.m_ag[this.m_af], this.m_ag[0], false);
			}
		}

		private void f(object A_0, RoutedEventArgs A_1)
		{
			if (this.m_af != this.m_ag.Length - 1)
			{
				this.m_af++;
				a(this.m_ag[this.m_af], this.m_ag[this.m_ag.Length - 1], true);
			}
		}

		private void a(object A_0, RoutedPropertyChangedEventArgs<double> A_1)
		{
			if (A_1.NewValue > A_1.OldValue)
			{
				if (this.m_af != this.m_ag.Length - 1)
				{
					this.m_af++;
					c(this.m_ag[this.m_af], this.m_ag[this.m_ag.Length - 1], true, false);
				}
			}
			else if (this.m_af != 0)
			{
				this.m_af--;
				c(this.m_ag[this.m_af], this.m_ag[0], false, false);
			}
		}

		private void a(double A_0, double A_1, bool A_2)
		{
			c(A_0, A_1, A_2, false);
		}

		private void a(double A_0)
		{
			@as = DateTime.Now;
			this.m_ad = (float)A_0;
			if (b0.Equals(PageMode.SinglePage))
			{
				f f = new f();
				f.c = this;
				f.b = this.m_i[this.m_p];
				f.a = f.b.leftImagePath;
				if (File.Exists(f.a))
				{
					g obj = new g();
					obj.b = f;
					obj.a = e();
					Thread thread = new Thread(new ThreadStart(obj.c));
					thread.Name = this.m_ad.ToString();
					b9.Add(thread);
				}
			}
			else
			{
				if (!b0.Equals(PageMode.DoublePage))
				{
					return;
				}
				h h = new h();
				h.c = this;
				h.b = e();
				h.a = this.m_j[this.m_p];
				if (h.a.rightPageIndex == -1)
				{
					if (File.Exists(h.a.leftImagePath))
					{
						Thread thread2 = new Thread(new ThreadStart(h.d));
						thread2.Name = this.m_ad.ToString();
						b9.Add(thread2);
					}
					return;
				}
				i i = new i();
				i.c = h;
				i.a = i.c.a.leftImagePath;
				i.b = i.c.a.rightImagePath;
				if (File.Exists(i.a) && File.Exists(i.b))
				{
					Thread thread3 = new Thread(new ThreadStart(i.d));
					thread3.Name = this.m_ad.ToString();
					b9.Add(thread3);
				}
			}
		}

		private void a(object A_0, imageSourceRenderedResultEventArgs A_1)
		{
			imageSourceRendered -= new EventHandler<imageSourceRenderedResultEventArgs>(a);
			bj = false;
			if (this.m_p.Equals(A_1.renderPageIndex))
			{
				if (this.m_ad.Equals(A_1.sourceScale))
				{
					BitmapImage imgSource = A_1.imgSource;
					b(imgSource, A_1.sourceScale);
				}
				else
				{
					for (int num = b9.Count - 1; num >= 0; num--)
					{
						if (this.m_ad.Equals((float)Convert.ToDouble(b9[num].Name)))
						{
							try
							{
								b9[num].Start();
								imageSourceRendered += new EventHandler<imageSourceRenderedResultEventArgs>(a);
								bj = true;
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
				bj = false;
				b9.Clear();
			}
			A_1.imgSource = null;
		}

		private void b(BitmapImage A_0, float A_1)
		{
			b method = new b(a);
			base.Dispatcher.Invoke(method, A_0, A_1);
		}

		private void a(BitmapImage A_0, float A_1)
		{
			if (A_0 != null)
			{
				bg = false;
				b(A_0);
				b9.Clear();
				bj = false;
				A_0 = null;
			}
		}

		private void c(double A_0, double A_1, bool A_2, bool A_3)
		{
			if (this.m_t.Equals(BookType.PHEJ))
			{
				a(A_0);
			}
			a(A_0, A_1, A_2, A_3);
			b(A_0, A_1, A_2, A_3);
			TranslateTransform translateTransform = (TranslateTransform)this.m_l.Children[1];
			translateTransform.X = 0.0;
			translateTransform.Y = 0.0;
			TranslateTransform translateTransform2 = (TranslateTransform)this.m_k.Children[1];
			translateTransform2.X = 0.0;
			translateTransform2.Y = 0.0;
			this.m_n = new System.Windows.Point(translateTransform2.X, translateTransform2.Y);
			this.m_o = new System.Windows.Point(translateTransform.X, translateTransform.Y);
		}

		private void b(double A_0, double A_1, bool A_2, bool A_3)
		{
			StackPanel stackPanel = (StackPanel)j();
			e();
			TranslateTransform translateTransform = (TranslateTransform)this.m_l.Children[1];
			ScaleTransform scaleTransform = (ScaleTransform)this.m_l.Children[0];
			double scaleX = scaleTransform.ScaleX;
			double scaleY = scaleTransform.ScaleY;
			scaleTransform.ScaleX = A_0;
			scaleTransform.ScaleY = A_0;
			if (A_2)
			{
				scaleTransform.ScaleX = Math.Min(scaleTransform.ScaleX, A_1);
				scaleTransform.ScaleY = Math.Min(scaleTransform.ScaleY, A_1);
			}
			else
			{
				scaleTransform.ScaleX = Math.Max(scaleTransform.ScaleX, A_1);
				scaleTransform.ScaleY = Math.Max(scaleTransform.ScaleY, A_1);
			}
			translateTransform.X -= translateTransform.X * (scaleX - scaleTransform.ScaleX);
			translateTransform.Y -= translateTransform.Y * (scaleY - scaleTransform.ScaleY);
			translateTransform.X = Math.Min(translateTransform.X, 0.0);
			translateTransform.X = Math.Max(translateTransform.X, 0.0);
			translateTransform.Y = Math.Min(translateTransform.Y, 0.0);
			translateTransform.Y = Math.Max(translateTransform.Y, 0.0);
		}

		private void a(double A_0, double A_1, bool A_2, bool A_3)
		{
			StackPanel stackPanel = (StackPanel)j();
			TranslateTransform translateTransform = (TranslateTransform)this.m_k.Children[1];
			ScaleTransform scaleTransform = (ScaleTransform)this.m_k.Children[0];
			double scaleX = scaleTransform.ScaleX;
			double scaleY = scaleTransform.ScaleY;
			scaleTransform.ScaleX = A_0;
			scaleTransform.ScaleY = A_0;
			if (A_2)
			{
				scaleTransform.ScaleX = Math.Min(scaleTransform.ScaleX, A_1);
				scaleTransform.ScaleY = Math.Min(scaleTransform.ScaleY, A_1);
			}
			else
			{
				scaleTransform.ScaleX = Math.Max(scaleTransform.ScaleX, A_1);
				scaleTransform.ScaleY = Math.Max(scaleTransform.ScaleY, A_1);
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
				ca = stackPanel.ActualHeight / base.ActualHeight;
			}
			else
			{
				ca = stackPanel.ActualWidth / base.RestoreBounds.Width;
			}
			if (!A_3)
			{
				Slider slider = FindVisualChildByName<Slider>(cg, "SliderInReader");
				slider.ValueChanged -= new RoutedPropertyChangedEventHandler<double>(a);
				slider.Value = A_0;
				slider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(a);
			}
			cb = false;
			if (scaleTransform.ScaleX != 1.0 || scaleTransform.ScaleY != 1.0)
			{
				cs.Visibility = Visibility.Visible;
			}
			else
			{
				cs.Visibility = Visibility.Collapsed;
			}
		}

		private UIElement j()
		{
			int num = this.m_p;
			Block block = cg.Document.Blocks.FirstBlock;
			UIElement result = new UIElement();
			if (cg.CanGoToPage(num))
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

		private Canvas i()
		{
			return FindVisualChildByName<Canvas>(cg, "stageCanvas");
		}

		private RadioButton h()
		{
			return FindVisualChildByName<RadioButton>(cg, "MediaListButton");
		}

		private Canvas g()
		{
			return FindVisualChildByName<Canvas>(cg, "MediaTableCanvas");
		}

		private StackPanel f()
		{
			return FindVisualChildByName<StackPanel>(cg, "mediaListPanel");
		}

		private Border e()
		{
			return FindVisualChildByName<Border>(cg, "PART_ContentHost");
		}

		public static T FindVisualChildByName<T>(DependencyObject parent, string name) where T : DependencyObject
		{
			if (parent != null)
			{
				for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
				{
					DependencyObject child = VisualTreeHelper.GetChild(parent, i);
					if (child.GetValue(FrameworkElement.NameProperty) as string== name)
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

		private void b(object A_0, MouseButtonEventArgs A_1)
		{
			Border relativeTo = e();
			this.m_m = A_1.GetPosition(relativeTo);
			if (A_1.ClickCount.Equals(2))
			{
				c();
				return;
			}
			if (A_0 is StackPanel)
			{
				((StackPanel)A_0).MouseMove += new MouseEventHandler(a);
				((StackPanel)A_0).PreviewMouseLeftButtonUp += new MouseButtonEventHandler(a);
			}
			else if (A_0 is Canvas)
			{
				((Canvas)A_0).MouseMove += new MouseEventHandler(a);
				((Canvas)A_0).PreviewMouseLeftButtonUp += new MouseButtonEventHandler(a);
			}
			A_1.Handled = true;
		}

		private void a(object A_0, MouseButtonEventArgs A_1)
		{
			StackPanel stackPanel = (StackPanel)j();
			TranslateTransform translateTransform = (TranslateTransform)this.m_k.Children[1];
			ScaleTransform scaleTransform = (ScaleTransform)this.m_k.Children[0];
			TranslateTransform translateTransform2 = (TranslateTransform)this.m_l.Children[1];
			ScaleTransform scaleTransform2 = (ScaleTransform)this.m_l.Children[0];
			double num = base.ActualHeight / base.ActualWidth;
			double num2 = stackPanel.ActualHeight / stackPanel.ActualWidth;
			if (scaleTransform.ScaleX == 1.0 && scaleTransform.ScaleY == 1.0)
			{
				if (A_0 is StackPanel)
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
				else if (A_0 is Canvas)
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
			if (A_0 is StackPanel)
			{
				this.m_n = new System.Windows.Point(translateTransform.X, translateTransform.Y);
				((StackPanel)A_0).MouseMove -= new MouseEventHandler(a);
				((StackPanel)A_0).PreviewMouseLeftButtonUp -= new MouseButtonEventHandler(a);
			}
			else if (A_0 is Canvas)
			{
				this.m_n = new System.Windows.Point(translateTransform.X, translateTransform.Y);
				this.m_o = new System.Windows.Point(translateTransform2.X, translateTransform2.Y);
				((Canvas)A_0).MouseMove -= new MouseEventHandler(a);
				((Canvas)A_0).PreviewMouseLeftButtonUp -= new MouseButtonEventHandler(a);
			}
			A_1.Handled = true;
		}

		private void d()
		{
			((TranslateTransform)this.m_k.Children[1]).X = 0.0;
			((TranslateTransform)this.m_l.Children[1]).X = 0.0;
		}

		private void c()
		{
			TranslateTransform obj = (TranslateTransform)this.m_k.Children[1];
			ScaleTransform scaleTransform = (ScaleTransform)this.m_k.Children[0];
			obj.X = 0.0;
			obj.Y = 0.0;
			scaleTransform.ScaleX = 1.0;
			scaleTransform.ScaleY = 1.0;
			TranslateTransform obj2 = (TranslateTransform)this.m_l.Children[1];
			ScaleTransform scaleTransform2 = (ScaleTransform)this.m_l.Children[0];
			obj2.X = 0.0;
			obj2.Y = 0.0;
			scaleTransform2.ScaleX = 1.0;
			scaleTransform2.ScaleY = 1.0;
			Slider slider = FindVisualChildByName<Slider>(cg, "SliderInReader");
			slider.ValueChanged -= new RoutedPropertyChangedEventHandler<double>(a);
			slider.Value = scaleTransform.ScaleY;
			slider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(a);
			cs.Visibility = Visibility.Collapsed;
			if (this.m_af != 0)
			{
				this.m_af = 0;
				c(this.m_ag[this.m_af], this.m_ag[0], false, false);
			}
		}

		private void a(object A_0, MouseEventArgs A_1)
		{
			StackPanel stackPanel = (StackPanel)j();
			TranslateTransform translateTransform = (TranslateTransform)this.m_k.Children[1];
			ScaleTransform scaleTransform = (ScaleTransform)this.m_k.Children[0];
			TranslateTransform translateTransform2 = (TranslateTransform)this.m_l.Children[1];
			ScaleTransform scaleTransform2 = (ScaleTransform)this.m_l.Children[0];
			Border relativeTo = e();
			Vector a_ = this.m_m - A_1.GetPosition(relativeTo);
			double num = base.ActualHeight / base.ActualWidth;
			double num2 = stackPanel.ActualHeight / stackPanel.ActualWidth;
			if (A_1.LeftButton != 0)
			{
				a(a_);
			}
		}

		private void a(Vector A_0)
		{
			StackPanel stackPanel = (StackPanel)j();
			TranslateTransform translateTransform = (TranslateTransform)this.m_k.Children[1];
			ScaleTransform scaleTransform = (ScaleTransform)this.m_k.Children[0];
			TranslateTransform translateTransform2 = (TranslateTransform)this.m_l.Children[1];
			ScaleTransform scaleTransform2 = (ScaleTransform)this.m_l.Children[0];
			if (this.m_aj.direction.Equals("right"))
			{
				A_0.X = 0.0 - A_0.X;
			}
			double num = base.ActualHeight / base.ActualWidth;
			double num2 = stackPanel.ActualHeight / stackPanel.ActualWidth;
			if (scaleTransform.ScaleX != 1.0 && scaleTransform.ScaleY != 1.0)
			{
				translateTransform.X = this.m_n.X - A_0.X;
				translateTransform.Y = this.m_n.Y - A_0.Y;
				if (num < num2)
				{
					if (stackPanel.ActualWidth * scaleTransform.ScaleX < base.ActualWidth * ca)
					{
						translateTransform.X = 0.0;
						translateTransform.Y = Math.Min(translateTransform.Y, (Math.Abs(stackPanel.ActualHeight * scaleTransform.ScaleY) - base.ActualHeight * ca) / 2.0);
						translateTransform.Y = Math.Max(translateTransform.Y, 0.0 - (Math.Abs(stackPanel.ActualHeight * scaleTransform.ScaleY) - base.ActualHeight * ca) / 2.0);
					}
					else
					{
						translateTransform.X = Math.Min(translateTransform.X, (Math.Abs(stackPanel.ActualWidth * scaleTransform.ScaleX) - base.ActualWidth * ca) / 2.0);
						translateTransform.X = Math.Max(translateTransform.X, 0.0 - (Math.Abs(stackPanel.ActualWidth * scaleTransform.ScaleX) - base.ActualWidth * ca) / 2.0);
						translateTransform.Y = Math.Min(translateTransform.Y, (Math.Abs(stackPanel.ActualHeight * scaleTransform.ScaleY) - base.ActualHeight * ca) / 2.0);
						translateTransform.Y = Math.Max(translateTransform.Y, 0.0 - (Math.Abs(stackPanel.ActualHeight * scaleTransform.ScaleY) - base.ActualHeight * ca) / 2.0);
					}
				}
				else if (stackPanel.ActualHeight * scaleTransform.ScaleY < base.ActualHeight * ca)
				{
					translateTransform.Y = 0.0;
					translateTransform.X = Math.Min(translateTransform.X, (Math.Abs(stackPanel.ActualWidth * scaleTransform.ScaleX) - base.ActualWidth * ca) / 2.0);
					translateTransform.X = Math.Max(translateTransform.X, 0.0 - (Math.Abs(stackPanel.ActualWidth * scaleTransform.ScaleX) - base.ActualWidth * ca) / 2.0);
				}
				else
				{
					translateTransform.Y = Math.Min(translateTransform.Y, (Math.Abs(stackPanel.ActualHeight * scaleTransform.ScaleY) - base.ActualHeight * ca) / 2.0);
					translateTransform.Y = Math.Max(translateTransform.Y, 0.0 - (Math.Abs(stackPanel.ActualHeight * scaleTransform.ScaleY) - base.ActualHeight * ca) / 2.0);
					translateTransform.X = Math.Min(translateTransform.X, (Math.Abs(stackPanel.ActualWidth * scaleTransform.ScaleX) - base.ActualWidth * ca) / 2.0);
					translateTransform.X = Math.Max(translateTransform.X, 0.0 - (Math.Abs(stackPanel.ActualWidth * scaleTransform.ScaleX) - base.ActualWidth * ca) / 2.0);
				}
			}
			double num3 = FindVisualChildByName<Canvas>(cg, "zoomCanvas").Height / stackPanel.ActualHeight;
			translateTransform2.X = translateTransform.X * num3;
			translateTransform2.Y = translateTransform.Y * num3;
			if (this.m_aj.direction.Equals("right"))
			{
				translateTransform2.X = (0.0 - translateTransform.X) * num3;
			}
		}

		private StackPanel b()
		{
			StackPanel stackPanel = new StackPanel();
			List<ShareButton> list = new List<ShareButton>();
			list.Add(new ShareButton("Assets/ReadWindow/icon_m.png", "Mail", SharedPlatform.Mail, false));
			List<ShareButton> list2 = list;
			if (a8 && !a9.Equals("HyReadCN"))
			{
				list2.Add(new ShareButton("Assets/ReadWindow/icon_f.png", "Facebook", SharedPlatform.Facebook, true));
				list2.Add(new ShareButton("Assets/ReadWindow/icon_p.png", "Plurk", SharedPlatform.Plurk, false));
				list2.Add(new ShareButton("Assets/ReadWindow/icon_t.png", "Twitter", SharedPlatform.Twitter, false));
			}
			ListBox listBox = new ListBox();
			listBox.Style = (Style)FindResource("ShareListBoxStyle");
			listBox.ItemsSource = list2;
			stackPanel.Children.Add(listBox);
			return stackPanel;
		}

		private void e(object A_0, RoutedEventArgs A_1)
		{
			SharedPlatform a_ = (SharedPlatform)((RadioButton)A_0).Tag;
			a(a_);
		}

		private void a(SharedPlatform A_0)
		{
			string pageIndex = this.m_e[this.m_p].pageIndex;
			string text = a(A_0, pageIndex);
			if (text.Equals("") || !a())
			{
				return;
			}
			BookThumbnail bookThumbnail = (BookThumbnail)this.m_s;
			if (A_0.Equals(SharedPlatform.Facebook))
			{
				Process.Start("http://www.facebook.com/sharer/sharer.php?u=" + Uri.EscapeDataString(text));
			}
			else if (A_0.Equals(SharedPlatform.Plurk))
			{
				string stringToEscape = a7.getLangString("imReading") + "【" + bookThumbnail.title + "】" + a7.getLangString("thisEBook") + "( P." + pageIndex + " ) " + a7.getLangString("recommend") + a7.getLangString("forYou") + a7.getLangString("welcomeToReader");
				Process.Start("http://www.plurk.com/?qualifier=shares&status=" + Uri.EscapeDataString(stringToEscape));
			}
			else if (A_0.Equals(SharedPlatform.Mail))
			{
				string text2 = "";
				string text3 = "";
				text2 = a7.getLangString("recommend") + "【" + bookThumbnail.title + "】" + a7.getLangString("thisEBook") + "( P." + pageIndex + " ) " + a7.getLangString("forYou");
				text3 = a7.getLangString("imReading") + "【" + bookThumbnail.title + "】" + a7.getLangString("thisEBook") + "( P." + pageIndex + " ) " + a7.getLangString("recommend") + a7.getLangString("forYou") + a7.getLangString("welcomeToReader");
				if (!this.m_t.Equals(BookType.EPUB) && !this.m_t.Equals(BookType.HEJ))
				{
					this.m_t.Equals(BookType.PHEJ);
				}
				text3 += "%0A";
				text3 += Uri.EscapeDataString(text);
				text3 += "%0A ";
				if (MessageBox.Show(a7.getLangString("mailtoMessage"), "", MessageBoxButton.YesNo).Equals(MessageBoxResult.Yes))
				{
					string text4 = "";
					Process.Start("mailto://" + text4 + "?subject=" + text2 + "&body=" + text3);
				}
			}
			else if (!A_0.Equals(SharedPlatform.Google) && A_0.Equals(SharedPlatform.Twitter))
			{
				string stringToEscape2 = a7.getLangString("imReading") + "【" + bookThumbnail.title + "】" + a7.getLangString("thisEBook") + "( P." + pageIndex + " ) " + a7.getLangString("recommend") + a7.getLangString("forYou") + a7.getLangString("welcomeToReader");
				Process.Start("http://twitter.com/home/?status=" + Uri.EscapeDataString(stringToEscape2));
			}
			bookThumbnail = null;
		}

		private string a(SharedPlatform A_0, string A_1)
		{
			string url = "http://openebook.hyread.com.tw/tweetservice/rest/BookInfo/add";
			XmlDocument xmlDocument = new XmlDocument();
			XMLTool xMLTool = new XMLTool();
			xmlDocument.LoadXml("<body></body>");
			BookThumbnail bookThumbnail = (BookThumbnail)this.m_s;
			try
			{
				if (this.m_t.Equals(BookType.HEJ) || this.m_t.Equals(BookType.PHEJ))
				{
					xMLTool.appendChildToXML("unit", bookThumbnail.vendorId, xmlDocument);
					xMLTool.appendChildToXML("type", A_0.GetHashCode().ToString(), xmlDocument);
					xMLTool.appendChildToXML("bookid", bookThumbnail.bookID, xmlDocument);
					xMLTool.appendCDATAChildToXML("title", bookThumbnail.title, xmlDocument);
					xMLTool.appendCDATAChildToXML("author", bookThumbnail.author, xmlDocument);
					xMLTool.appendCDATAChildToXML("publisher", bookThumbnail.publisher, xmlDocument);
					xMLTool.appendChildToXML("publishdate", bookThumbnail.publishDate.Replace("/", "-"), xmlDocument);
					xMLTool.appendChildToXML("pages", bookThumbnail.totalPages.ToString(), xmlDocument);
					xMLTool.appendChildToXML("size", "123456", xmlDocument);
					xMLTool.appendChildToXML("direction", this.m_aj.direction, xmlDocument);
					xMLTool.appendChildToXML("comment", "", xmlDocument);
					xMLTool.appendChildToXML("page", A_1, xmlDocument);
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
				string uriString = this.m_z + "\\" + this.m_aj.SImgList[0].path;
				byte[] inArray = a(new BitmapImage(new Uri(uriString)));
				xMLTool.appendCDATAChildToXML("coverpic", Convert.ToBase64String(inArray), xmlDocument);
				Bitmap bitmap = null;
				if (File.Exists(this.m_z + "/hyweb/strokes/" + this.m_aj.LImgList[this.m_p].pageId + ".isf"))
				{
					InkCanvas inkCanvas = FindVisualChildByName<InkCanvas>(cg, "penMemoCanvas");
					InkCanvas inkCanvas2 = new InkCanvas();
					inkCanvas2.Background = System.Windows.Media.Brushes.Transparent;
					FileStream fileStream = new FileStream(this.m_z + "/hyweb/strokes/" + this.m_aj.LImgList[this.m_p].pageId + ".isf", FileMode.Open);
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
				string a_ = this.m_z + "\\" + this.m_aj.LImgList[this.m_p].path;
				if (this.m_aj.LImgList[this.m_p].path.Contains("tryPageEnd"))
				{
					a_ = this.m_aj.LImgList[this.m_p].path;
				}
				BitmapImage a_2 = null;
				if (this.m_t.Equals(BookType.HEJ))
				{
					a_2 = a(this.m_d, am, a_, 1f);
				}
				else if (this.m_t.Equals(BookType.PHEJ))
				{
					a_2 = b(this.m_d, am, a_, 1f);
				}
				byte[] buffer = a(a_2);
				a_2 = null;
				Bitmap bitmap2 = null;
				try
				{
					Bitmap bitmap3 = new Bitmap(new MemoryStream(buffer));
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
					double num3 = 1024.0 / (double)num;
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
				byte[] inArray2 = (byte[])TypeDescriptor.GetConverter(bitmap4).ConvertTo(bitmap4, typeof(byte[]));
				xMLTool.appendCDATAChildToXML("pagepic", Convert.ToBase64String(inArray2), xmlDocument);
				inArray2 = null;
			}
			catch
			{
			}
			string result = new HttpRequest(this.m_x.saveProxyMode, this.m_x.saveProxyHttpPort).postXMLAndLoadString(url, xmlDocument);
			bookThumbnail = null;
			return result;
		}

		private bool a()
		{
			int postTimes = a6.getPostTimes(this.m_ab);
			if (!postTimes.Equals(-1))
			{
				if (postTimes < cc)
				{
					postTimes++;
					a6.savePostTimes(this.m_ab, postTimes);
					return true;
				}
				MessageBox.Show(a7.getLangString("overShare") + cc + a7.getLangString("page"), a7.getLangString("warning"));
				return false;
			}
			return false;
		}

		private void d(object A_0, RoutedEventArgs A_1)
		{
			byte[] a_ = am;
			System.Windows.Controls.Image image = null;
			if (b0.Equals(PageMode.DoublePage))
			{
				RadioButton radioButton = (RadioButton)A_0;
				if (!MessageBox.Show(a7.getLangString("doublePagePrintModeAlert"), a7.getLangString("printMode"), MessageBoxButton.YesNo).Equals(MessageBoxResult.Yes))
				{
					radioButton.IsChecked = false;
					return;
				}
				ac(A_0, A_1);
			}
			int index = this.m_p;
			string text = this.m_z + "\\" + this.m_aj.LImgList[index].path;
			if (this.m_aj.LImgList[index].path.Contains("tryPageEnd"))
			{
				text = this.m_aj.LImgList[index].path;
			}
			if (File.Exists(text))
			{
				if (this.m_t.Equals(BookType.HEJ))
				{
					image = a(this.m_d, a_, text);
				}
				else if (this.m_t.Equals(BookType.PHEJ))
				{
					image = b(this.m_d, a_, text);
				}
				a_ = null;
			}
			if (image != null)
			{
				FixedDocument fixedDocument = new FixedDocument();
				PrintDialog printDialog = new PrintDialog();
				fixedDocument.DocumentPaginator.PageSize = new System.Windows.Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);
				FixedPage fixedPage = new FixedPage();
				if (b0.Equals(PageMode.SinglePage))
				{
					fixedPage.Width = printDialog.PrintableAreaWidth;
					fixedPage.Height = printDialog.PrintableAreaHeight;
					image.Width = printDialog.PrintableAreaWidth;
					image.Height = printDialog.PrintableAreaHeight;
				}
				else if (b0.Equals(PageMode.DoublePage))
				{
					int num = this.m_p;
					if (num.Equals(0) || num.Equals(this.m_f.Count - 1))
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
				ct.Document = fixedDocument;
				ct.Visibility = Visibility.Visible;
			}
			image = null;
		}

		private void c(object A_0, RoutedEventArgs A_1)
		{
			if (ct.Visibility.Equals(Visibility.Visible))
			{
				ct.Visibility = Visibility.Collapsed;
			}
		}

		private void b(object A_0, RoutedEventArgs A_1)
		{
			PrintDialog printDialog = new PrintDialog();
			if (b0.Equals(PageMode.DoublePage))
			{
				int num = this.m_p;
				if (num.Equals(0) || num.Equals(this.m_f.Count - 1))
				{
					printDialog.PrintTicket.PageOrientation = PageOrientation.Portrait;
				}
				else
				{
					printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;
				}
			}
			printDialog.PrintDocument(ct.Document.DocumentPaginator, "");
		}

		private void a(object A_0, MouseWheelEventArgs A_1)
		{
			A_1.Handled = true;
			if (A_1.Delta.Equals(120))
			{
				if (this.m_af == this.m_ag.Length - 1)
				{
					return;
				}
				this.m_af++;
				c(this.m_ag[this.m_af], this.m_ag[this.m_ag.Length - 1], true, false);
			}
			else if (A_1.Delta.Equals(-120))
			{
				if (this.m_af == 0)
				{
					return;
				}
				this.m_af--;
				c(this.m_ag[this.m_af], this.m_ag[0], false, false);
			}
			cd = DateTime.Now;
		}

		private void a(object A_0, KeyEventArgs A_1)
		{
			if (A_1.Key == Key.OemPlus && Keyboard.Modifiers == ModifierKeys.Control)
			{
				if (this.m_af == this.m_ag.Length - 1)
				{
					A_1.Handled = true;
					return;
				}
				this.m_af++;
				c(this.m_ag[this.m_af], this.m_ag[this.m_ag.Length - 1], true, false);
			}
			TranslateTransform translateTransform = (TranslateTransform)this.m_k.Children[1];
			ScaleTransform scaleTransform2 = (ScaleTransform)this.m_k.Children[0];
			TranslateTransform translateTransform2 = (TranslateTransform)this.m_l.Children[1];
			ScaleTransform scaleTransform = (ScaleTransform)this.m_l.Children[0];
			if (!(scaleTransform.ScaleX > 1.0) || !(scaleTransform.ScaleY > 0.0))
			{
				return;
			}
			switch (A_1.Key)
			{
			case Key.Left:
				a(new Vector(ce * -1, 0.0));
				break;
			case Key.Right:
				a(new Vector(ce, 0.0));
				break;
			case Key.Up:
				a(new Vector(0.0, ce * -1));
				break;
			case Key.Down:
				a(new Vector(0.0, ce));
				break;
			case Key.OemMinus:
				if (Keyboard.Modifiers == ModifierKeys.Control && this.m_af != 0)
				{
					this.m_af--;
					c(this.m_ag[this.m_af], this.m_ag[0], false, false);
				}
				break;
			case Key.D0:
				if (Keyboard.Modifiers == ModifierKeys.Control)
				{
					c();
				}
				break;
			}
			if (cb)
			{
				this.m_n = new System.Windows.Point(translateTransform.X, translateTransform.Y);
				this.m_o = new System.Windows.Point(translateTransform2.X, translateTransform2.Y);
			}
			else
			{
				double num3 = translateTransform.X = (translateTransform.Y = 0.0);
				num3 = (translateTransform2.X = (translateTransform2.Y = 0.0));
				cb = true;
			}
			if (!A_1.Key.Equals(Key.Next) && !A_1.Key.Equals(Key.Prior))
			{
				A_1.Handled = true;
			}
		}

		private void a(object A_0, RoutedEventArgs A_1)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string sqlCommand = "select page, notes, createTime from booknoteDetail  where userbook_sno=" + this.m_ab + " and status='0' order by page";
			QueryResult queryResult = a6.sqlCommandQuery(sqlCommand);
			string text = "";
			while (queryResult.fetchRow())
			{
				string @string = queryResult.getString("page");
				if (text.Equals(""))
				{
					stringBuilder.AppendLine("<< " + base.Title + " >>%0d%0a");
				}
				stringBuilder.AppendLine("%0d%0a");
				text = @string;
				string pageIndex = this.m_e[Convert.ToInt32(@string)].pageIndex;
				long num = queryResult.getLong("createTime") + 28800;
				long ticks = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
				DateTime dateTime = new DateTime(ticks + num * 10000000);
				if (ba != null)
				{
					stringBuilder.AppendLine(ba.getNavLabelByIndex(Convert.ToInt32(@string)));
				}
				stringBuilder.AppendLine("  (P." + pageIndex + ")   " + dateTime.ToString() + "%0d%0a");
				stringBuilder.AppendLine(queryResult.getString("notes") + "%0d%0a");
			}
			if (MessageBox.Show(a7.getLangString("mailtoMessage"), "", MessageBoxButton.YesNo).Equals(MessageBoxResult.Yes))
			{
				new OpenProcess().mailToProcess("", "Notes of " + base.Title, stringBuilder.ToString(), "");
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!cu)
			{
				cu = true;
				Uri resourceLocator = new Uri("/ReadPageModule;component/readwindow.xaml", UriKind.Relative);
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
			case 33:
				cf = (Grid)target;
				break;
			case 34:
				cg = (FlowDocumentReader)target;
				cg.PreviewMouseWheel += new MouseWheelEventHandler(a);
				cg.PreviewKeyDown += new KeyEventHandler(a);
				break;
			case 35:
				ch = (ProgressBar)target;
				break;
			case 36:
				ci = (Canvas)target;
				break;
			case 37:
				cj = (StackPanel)target;
				break;
			case 38:
				ck = (Grid)target;
				break;
			case 39:
				cl = (StackPanel)target;
				break;
			case 40:
				cm = (RadioButton)target;
				cm.Click += new RoutedEventHandler(ae);
				break;
			case 41:
				cn = (RadioButton)target;
				cn.Click += new RoutedEventHandler(af);
				break;
			case 42:
				co = (RadioButton)target;
				co.Click += new RoutedEventHandler(ag);
				break;
			case 43:
				cp = (RadioButton)target;
				cp.Click += new RoutedEventHandler(m);
				break;
			case 44:
				cq = (ListBox)target;
				cq.SelectionChanged += new SelectionChangedEventHandler(c);
				break;
			case 45:
				cr = (RadioButton)target;
				cr.Click += new RoutedEventHandler(n);
				break;
			case 46:
				cs = (RadioButton)target;
				cs.Click += new RoutedEventHandler(z);
				break;
			case 47:
				ct = (DocumentViewer)target;
				break;
			default:
				cu = true;
				break;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				((Button)target).Click += new RoutedEventHandler(b);
				break;
			case 2:
				((Button)target).Click += new RoutedEventHandler(c);
				break;
			case 3:
				((RadioButton)target).Click += new RoutedEventHandler(q);
				break;
			case 4:
				((RadioButton)target).Click += new RoutedEventHandler(p);
				break;
			case 5:
				((RadioButton)target).Click += new RoutedEventHandler(ad);
				break;
			case 6:
				((RadioButton)target).Click += new RoutedEventHandler(ac);
				break;
			case 7:
				((RadioButton)target).Click += new RoutedEventHandler(ab);
				break;
			case 8:
				((RadioButton)target).Click += new RoutedEventHandler(q);
				break;
			case 9:
				((TextBlock)target).TargetUpdated += new EventHandler<DataTransferEventArgs>(a);
				break;
			case 10:
				((RadioButton)target).Click += new RoutedEventHandler(aa);
				break;
			case 11:
				((RadioButton)target).Click += new RoutedEventHandler(w);
				break;
			case 12:
				((RadioButton)target).Click += new RoutedEventHandler(d);
				break;
			case 13:
				((RadioButton)target).Click += new RoutedEventHandler(a);
				break;
			case 14:
				((RadioButton)target).Click += new RoutedEventHandler(t);
				break;
			case 15:
				((RadioButton)target).Click += new RoutedEventHandler(x);
				break;
			case 16:
				((RadioButton)target).Click += new RoutedEventHandler(o);
				break;
			case 17:
				((RadioButton)target).Click += new RoutedEventHandler(r);
				break;
			case 18:
				((RadioButton)target).Click += new RoutedEventHandler(v);
				break;
			case 19:
				((RadioButton)target).Click += new RoutedEventHandler(s);
				break;
			case 20:
				((RadioButton)target).Click += new RoutedEventHandler(u);
				break;
			case 21:
				((RadioButton)target).Click += new RoutedEventHandler(y);
				break;
			case 22:
				((RepeatButton)target).Click += new RoutedEventHandler(g);
				break;
			case 23:
				((Slider)target).ValueChanged += new RoutedPropertyChangedEventHandler<double>(a);
				break;
			case 24:
				((RepeatButton)target).Click += new RoutedEventHandler(f);
				break;
			case 25:
				((Canvas)target).MouseLeftButtonDown += new MouseButtonEventHandler(c);
				break;
			case 26:
				((Canvas)target).MouseLeftButtonDown += new MouseButtonEventHandler(b);
				break;
			case 27:
				((Canvas)target).MouseLeftButtonDown += new MouseButtonEventHandler(e);
				break;
			case 28:
				((RadioButton)target).Click += new RoutedEventHandler(ah);
				break;
			case 29:
				((RadioButton)target).Click += new RoutedEventHandler(e);
				break;
			case 30:
				((Button)target).Click += new RoutedEventHandler(h);
				break;
			case 31:
			{
				EventSetter eventSetter = new EventSetter();
				eventSetter.Event = FrameworkElement.RequestBringIntoViewEvent;
				eventSetter.Handler = new RequestBringIntoViewEventHandler(a);
				((Style)target).Setters.Add(eventSetter);
				break;
			}
			case 32:
				((Button)target).Click += new RoutedEventHandler(h);
				break;
			}
		}
	}
}
