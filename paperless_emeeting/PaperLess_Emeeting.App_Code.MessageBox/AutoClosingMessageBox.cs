using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;

namespace PaperLess_Emeeting.App_Code.MessageBox
{
	public class AutoClosingMessageBox
	{
		private const int WM_CLOSE = 16;

		private Timer _timeoutTimer;

		private string _caption;

		private AutoClosingMessageBox(string text, string caption, int timeout)
		{
			_caption = caption;
			_timeoutTimer = new Timer(new TimerCallback(OnTimerElapsed), null, timeout, -1);
			System.Windows.MessageBox.Show(text, caption);
		}

		public static void Show(string text, int timeout = 1000)
		{
			new AutoClosingMessageBox(text, "系統訊息", timeout);
		}

		private void OnTimerElapsed(object state)
		{
			try
			{
				IntPtr intPtr = FindWindow(null, _caption);
				if (intPtr != IntPtr.Zero)
				{
					SendMessage(intPtr, 16u, IntPtr.Zero, IntPtr.Zero);
				}
				_timeoutTimer.Dispose();
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}

		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
	}
}
