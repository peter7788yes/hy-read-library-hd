using PaperLess_Emeeting.App_Code.MessageBox;
using PaperLess_Emeeting.Properties;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;

namespace PaperLess_Emeeting
{
	public class ConfirmWindow : Window, IComponentConnector
	{
		private const int GWL_STYLE = -16;

		private const int WS_SYSMENU = 524288;

		internal TextBlock txtQuest;

		internal TextBox tbPinCode;

		internal Button btnPinCodeClear;

		internal Image btnNO;

		internal Image btnYes;

		private bool _contentLoaded;

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

		[DllImport("user32.dll", SetLastError = true)]
		private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

		[DllImport("user32.dll")]
		private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

		public ConfirmWindow()
		{
			MouseTool.ShowLoading();
			InitializeComponent();
			base.Loaded += new RoutedEventHandler(ConfirmWindow_Loaded);
			base.KeyDown += new KeyEventHandler(ConfirmWindow_KeyDown);
			Window window = Enumerable.FirstOrDefault(Enumerable.OfType<Home>(Application.Current.Windows));
			if (window != null)
			{
				base.Owner = window;
			}
			base.WindowStartupLocation = WindowStartupLocation.CenterScreen;
		}

		private void ConfirmWindow_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				base.DialogResult = true;
				Close();
			}
		}

		private void ConfirmWindow_Loaded(object sender, RoutedEventArgs e)
		{
			IntPtr handle = new WindowInteropHelper(this).Handle;
			SetWindowLong(handle, -16, GetWindowLong(handle, -16) & -524289);
			Task.Factory.StartNew(new Action(_003CConfirmWindow_Loaded_003Eb__0));
		}

		private void InitSelectDB()
		{
		}

		private void InitEvent()
		{
			Image image = btnNO;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegated == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegated = new MouseEventHandler(_003CInitEvent_003Eb__2);
			}
			image.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegated;
			Image image2 = btnNO;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegatee == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegatee = new MouseEventHandler(_003CInitEvent_003Eb__3);
			}
			image2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegatee;
			btnNO.MouseLeftButtonDown += new MouseButtonEventHandler(_003CInitEvent_003Eb__4);
			Image image3 = btnYes;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegatef == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegatef = new MouseEventHandler(_003CInitEvent_003Eb__5);
			}
			image3.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegatef;
			Image image4 = btnYes;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate10 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate10 = new MouseEventHandler(_003CInitEvent_003Eb__6);
			}
			image4.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate10;
			btnYes.MouseLeftButtonDown += new MouseButtonEventHandler(_003CInitEvent_003Eb__7);
			TextBox textBox = tbPinCode;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate11 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate11 = new MouseEventHandler(_003CInitEvent_003Eb__8);
			}
			textBox.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate11;
			TextBox textBox2 = tbPinCode;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate12 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate12 = new MouseEventHandler(_003CInitEvent_003Eb__9);
			}
			textBox2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate12;
			tbPinCode.PreviewKeyDown += new KeyEventHandler(tbPinCode_PreviewKeyDown);
			tbPinCode.Focus();
			Button button = btnPinCodeClear;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate13 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate13 = new MouseEventHandler(_003CInitEvent_003Eb__a);
			}
			button.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate13;
			Button button2 = btnPinCodeClear;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate14 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate14 = new MouseEventHandler(_003CInitEvent_003Eb__b);
			}
			button2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate14;
			btnPinCodeClear.Click += new RoutedEventHandler(_003CInitEvent_003Eb__c);
		}

		private void tbPinCode_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			Task.Factory.StartNew(new Action(_003CtbPinCode_PreviewKeyDown_003Eb__15));
		}

		private void InitUI()
		{
			txtQuest.Text = string.Format("請輸入{0}識別碼", Settings.Default.CourseOrMeeting_String);
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/confirmwindow.xaml", UriKind.Relative);
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
				txtQuest = (TextBlock)target;
				break;
			case 2:
				tbPinCode = (TextBox)target;
				break;
			case 3:
				btnPinCodeClear = (Button)target;
				break;
			case 4:
				btnNO = (Image)target;
				break;
			case 5:
				btnYes = (Image)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}

		[CompilerGenerated]
		private void _003CConfirmWindow_Loaded_003Eb__0()
		{
			InitSelectDB();
			base.Dispatcher.BeginInvoke(new Action(_003CConfirmWindow_Loaded_003Eb__1));
		}

		[CompilerGenerated]
		private void _003CConfirmWindow_Loaded_003Eb__1()
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
		private void _003CInitEvent_003Eb__4(object sender, MouseButtonEventArgs e)
		{
			base.DialogResult = false;
			tbPinCode.Text = "";
			Close();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__5(object sender, MouseEventArgs e)
		{
			MouseTool.ShowHand();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__6(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__7(object sender, MouseButtonEventArgs e)
		{
			if (tbPinCode.Text.Trim().Equals(""))
			{
				AutoClosingMessageBox.Show(string.Format("請先輸入{0}識別碼", Settings.Default.CourseOrMeeting_String));
				return;
			}
			base.DialogResult = true;
			Close();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__8(object sender, MouseEventArgs e)
		{
			MouseTool.ShowIBeam();
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
		private void _003CInitEvent_003Eb__c(object sender, RoutedEventArgs e)
		{
			tbPinCode.Text = "";
			btnPinCodeClear.Visibility = Visibility.Collapsed;
		}

		[CompilerGenerated]
		private void _003CtbPinCode_PreviewKeyDown_003Eb__15()
		{
			Thread.Sleep(10);
			base.Dispatcher.BeginInvoke(new Action(_003CtbPinCode_PreviewKeyDown_003Eb__16));
		}

		[CompilerGenerated]
		private void _003CtbPinCode_PreviewKeyDown_003Eb__16()
		{
			if (tbPinCode.Text.Length > 0)
			{
				btnPinCodeClear.Visibility = Visibility.Visible;
			}
			else
			{
				btnPinCodeClear.Visibility = Visibility.Collapsed;
			}
		}
	}
}
