using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows;
using System.Windows.Threading;

namespace PaperLess_Emeeting
{
	public class MousePosition : DependencyObject
	{
		internal struct NativePoint
		{
			public int X;

			public int Y;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass1
		{
			public MousePosition _003C_003E4__this;

			public Point value;

			public void _003Cset_CurrentPosition_003Eb__0()
			{
				_003C_003E4__this.SetValue(CurrentPositionProperty, value);
			}
		}

		private Dispatcher dispatcher;

		private Timer timer = new Timer(100.0);

		public static readonly DependencyProperty CurrentPositionProperty = DependencyProperty.Register("CurrentPosition", typeof(Point), typeof(MousePosition));

		public Point CurrentPosition
		{
			get
			{
				return (Point)GetValue(CurrentPositionProperty);
			}
			set
			{
				_003C_003Ec__DisplayClass1 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass1();
				_003C_003Ec__DisplayClass.value = value;
				_003C_003Ec__DisplayClass._003C_003E4__this = this;
				dispatcher.Invoke(new Action(_003C_003Ec__DisplayClass._003Cset_CurrentPosition_003Eb__0), new object[0]);
			}
		}

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetCursorPos(ref NativePoint pt);

		public static Point GetCurrentMousePosition()
		{
			NativePoint pt = default(NativePoint);
			GetCursorPos(ref pt);
			return new Point(pt.X, pt.Y);
		}

		public MousePosition()
		{
			dispatcher = Application.Current.MainWindow.Dispatcher;
			timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
			timer.Start();
		}

		private void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			Point point = CurrentPosition = GetCurrentMousePosition();
		}
	}
}
