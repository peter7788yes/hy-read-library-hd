using System;
using System.Windows.Forms;
using System.Windows.Interop;

namespace AutoLogOffInWPF
{
	internal class AutoLogOffHelper
	{
		public delegate void MakeAutoLogOff();

		private static Timer _timer = null;

		private static int _logOffTime;

		public static int LogOffTime
		{
			get
			{
				return _logOffTime;
			}
			set
			{
				_logOffTime = value;
			}
		}

		public static event MakeAutoLogOff MakeAutoLogOffEvent;

		public static void StartAutoLogoffOption()
		{
			ComponentDispatcher.ThreadIdle += new EventHandler(DispatcherQueueEmptyHandler);
		}

		public static void StopAutoLogoffOption()
		{
			if (_timer != null)
			{
				ComponentDispatcher.ThreadIdle -= new EventHandler(DispatcherQueueEmptyHandler);
				_timer.Stop();
				_timer = null;
			}
		}

		private static void _timer_Tick(object sender, EventArgs e)
		{
			if (_timer != null)
			{
				ComponentDispatcher.ThreadIdle -= new EventHandler(DispatcherQueueEmptyHandler);
				_timer.Stop();
				_timer = null;
				if (AutoLogOffHelper.MakeAutoLogOffEvent != null)
				{
					AutoLogOffHelper.MakeAutoLogOffEvent();
				}
			}
		}

		private static void DispatcherQueueEmptyHandler(object sender, EventArgs e)
		{
			if (_timer == null)
			{
				_timer = new Timer();
				_timer.Interval = LogOffTime * 60 * 1000;
				_timer.Tick += new EventHandler(_timer_Tick);
				_timer.Enabled = true;
			}
			else if (!_timer.Enabled)
			{
				_timer.Enabled = true;
			}
		}

		public static void ResetLogoffTimer()
		{
			if (_timer != null)
			{
				_timer.Enabled = false;
				_timer.Enabled = true;
			}
		}
	}
}
