using Microsoft.Win32;
using Newtonsoft.Json;
using PaperLess_Emeeting.App_Code;
using PaperLess_Emeeting.App_Code.DownloadItem;
using PaperLess_Emeeting.App_Code.Tools;
using System;
using System.CodeDom.Compiler;
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
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace PaperLess_Emeeting
{
	public class PDFFactoryRow : UserControl, IComponentConnector
	{
		public PDFStatus pdfStatus;

		public DispatcherTimer timer;

		private Storyboard sb;

		internal TextBlock txtIndex;

		internal TextBlock txtFileName;

		internal TextBlock txtStatus;

		internal Image btnExport;

		internal Image btnDownload;

		private bool _contentLoaded;

		[CompilerGenerated]
		private int _003Cindex_003Ek__BackingField;

		[CompilerGenerated]
		private File_DownloadItemViewModel _003CfileItem_003Ek__BackingField;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegatee;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegatef;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate10;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate11;

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

		public PDFFactoryRow(int index, File_DownloadItemViewModel fileItem)
		{
			InitializeComponent();
			this.index = index;
			sb = (Storyboard)TryFindResource("sb");
			this.fileItem = fileItem;
			base.Loaded += new RoutedEventHandler(PDFFactoryRow_Loaded);
		}

		private void PDFFactoryRow_Loaded(object sender, RoutedEventArgs e)
		{
			txtIndex.Text = index.ToString();
			txtFileName.Text = fileItem.FileName;
			Task.Factory.StartNew(new Action(_003CPDFFactoryRow_Loaded_003Eb__0));
		}

		private void InitEvent()
		{
			Image image = btnExport;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegatee == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegatee = new MouseEventHandler(_003CInitEvent_003Eb__2);
			}
			image.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegatee;
			Image image2 = btnExport;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegatef == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegatef = new MouseEventHandler(_003CInitEvent_003Eb__3);
			}
			image2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegatef;
			btnExport.MouseLeftButtonDown += new MouseButtonEventHandler(_003CInitEvent_003Eb__4);
			Image image3 = btnDownload;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate10 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate10 = new MouseEventHandler(_003CInitEvent_003Eb__7);
			}
			image3.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate10;
			Image image4 = btnDownload;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate11 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate11 = new MouseEventHandler(_003CInitEvent_003Eb__8);
			}
			image4.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate11;
			btnDownload.MouseLeftButtonDown += new MouseButtonEventHandler(_003CInitEvent_003Eb__9);
			timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromMilliseconds(1100.0);
			timer.Tick += new EventHandler(_003CInitEvent_003Eb__a);
			timer.Start();
		}

		private void InitUI()
		{
			sb.Begin();
			if (Singleton_PDFFactory.IsPDFInWork(fileItem.ID))
			{
				sb.Begin();
			}
			else if (File.Exists(Path.Combine(fileItem.UnZipFilePath, "PDF.pdf")) && fileItem.FileType == MeetingFileType.已下載完成)
			{
				sb.Stop();
				pdfStatus = PDFStatus.匯出成功;
				txtStatus.Text = pdfStatus.ToString();
			}
			else
			{
				sb.Stop();
				pdfStatus = PDFStatus.尚未匯出;
				txtStatus.Text = pdfStatus.ToString();
			}
			PDFStatus pDFStatus = pdfStatus;
			if (pDFStatus == PDFStatus.匯出成功)
			{
				btnExport.Visibility = Visibility.Visible;
				btnDownload.Visibility = Visibility.Visible;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/pdffactoryrow.xaml", UriKind.Relative);
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
				txtIndex = (TextBlock)target;
				break;
			case 2:
				txtFileName = (TextBlock)target;
				break;
			case 3:
				txtStatus = (TextBlock)target;
				break;
			case 4:
				btnExport = (Image)target;
				break;
			case 5:
				btnDownload = (Image)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}

		[CompilerGenerated]
		private void _003CPDFFactoryRow_Loaded_003Eb__0()
		{
			base.Dispatcher.BeginInvoke(new Action(_003CPDFFactoryRow_Loaded_003Eb__1));
		}

		[CompilerGenerated]
		private void _003CPDFFactoryRow_Loaded_003Eb__1()
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
		private void _003CInitEvent_003Eb__4(object sender, MouseButtonEventArgs e)
		{
			Task.Factory.StartNew(new Action(_003CInitEvent_003Eb__5));
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
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			saveFileDialog.FileName = FileNameTool.PureFileName(fileItem.FileName);
			saveFileDialog.DefaultExt = ".pdf";
			saveFileDialog.Filter = "PDF documents (.pdf)|*.pdf";
			bool? flag = saveFileDialog.ShowDialog();
			bool? flag2 = flag;
			if (flag2.GetValueOrDefault() && flag2.HasValue)
			{
				string text = Path.Combine(fileItem.UnZipFilePath, "PDf.pdf");
				string fileName = saveFileDialog.FileName;
				if (File.Exists(text))
				{
					File.Copy(text, fileName, true);
				}
			}
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__a(object sender, EventArgs e)
		{
			Action action = null;
			Action action2 = null;
			Action action3 = null;
			if (Singleton_PDFFactory.IsPDFInWork(fileItem.ID))
			{
				Dispatcher dispatcher = base.Dispatcher;
				if (action == null)
				{
					action = new Action(_003CInitEvent_003Eb__b);
				}
				dispatcher.BeginInvoke(action);
			}
			else if (File.Exists(Path.Combine(fileItem.UnZipFilePath, "PDF.pdf")) && fileItem.FileType == MeetingFileType.已下載完成)
			{
				Dispatcher dispatcher2 = base.Dispatcher;
				if (action2 == null)
				{
					action2 = new Action(_003CInitEvent_003Eb__c);
				}
				dispatcher2.BeginInvoke(action2);
			}
			else
			{
				Dispatcher dispatcher3 = base.Dispatcher;
				if (action3 == null)
				{
					action3 = new Action(_003CInitEvent_003Eb__d);
				}
				dispatcher3.BeginInvoke(action3);
			}
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__5()
		{
			Singleton_PDFFactory.AddBookInPDFWork(fileItem.ID);
			base.Dispatcher.BeginInvoke(new Action(_003CInitEvent_003Eb__6));
			Singleton_PDFFactory.RemoveBookInPDFWork(fileItem.ID);
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__6()
		{
			btnExport.Visibility = Visibility.Collapsed;
			btnDownload.Visibility = Visibility.Collapsed;
			sb.Begin();
			DataTable dataTable = MSCE.GetDataTable("select PDFFactoryParameterJson from FileRow where userid=@1 and id=@2", fileItem.UserID, fileItem.ID);
			if (dataTable.Rows.Count > 0)
			{
				PDFFactoryParameter pDFFactoryParameter = JsonConvert.DeserializeObject<PDFFactoryParameter>(dataTable.Rows[0][0].ToString());
				Home home = Enumerable.First(Enumerable.OfType<Home>(Application.Current.Windows));
				if (home.IsInSync)
				{
					pDFFactoryParameter.UserAccount += "_Sync";
				}
				Singleton_PDFFactory.SavePDF(pDFFactoryParameter);
			}
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__b()
		{
			pdfStatus = PDFStatus.匯出中;
			btnExport.Visibility = Visibility.Collapsed;
			btnDownload.Visibility = Visibility.Collapsed;
			txtStatus.Text = pdfStatus.ToString();
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__c()
		{
			sb.Stop();
			pdfStatus = PDFStatus.匯出成功;
			btnExport.Visibility = Visibility.Visible;
			btnDownload.Visibility = Visibility.Visible;
			txtStatus.Text = pdfStatus.ToString();
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__d()
		{
			sb.Stop();
			pdfStatus = PDFStatus.尚未匯出;
			btnExport.Visibility = Visibility.Collapsed;
			btnDownload.Visibility = Visibility.Collapsed;
			txtStatus.Text = pdfStatus.ToString();
		}
	}
}
