using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

namespace ReadPageModule
{
	public class MoviePlayer : Window, IComponentConnector
	{
		private DispatcherTimer m_a;

		private List<Image> m_b;

		private bool m_c;

		private System.Timers.Timer m_d;

		private bool m_e;

		private double m_f;

		internal Grid g;

		internal MediaElement h;

		internal StackPanel i;

		internal StackPanel j;

		internal Slider k;

		internal System.Windows.Controls.Button l;

		internal Image m;

		internal System.Windows.Controls.Button n;

		internal System.Windows.Controls.Button o;

		internal System.Windows.Controls.Button p;

		internal Slider q;

		private bool r;

		public MoviePlayer(string FilePath, bool IsMovie)
		{
			System.Timers.Timer timer = new System.Timers.Timer((int)GetDoubleClickTime());
			timer.AutoReset = false;
			this.m_d = timer;
			base._002Ector();
			InitializeComponent();
			List<Image> list = new List<Image>();
			Image image = new Image();
			image.Name = "pause";
			image.Style = (Style)FindResource("PauseImageStyle");
			list.Add(image);
			Image image2 = new Image();
			image2.Name = "play";
			image2.Style = (Style)FindResource("PlayImageStyle");
			list.Add(image2);
			this.m_b = list;
			a(false);
			this.m_a = new DispatcherTimer();
			this.m_a.Interval = TimeSpan.FromMilliseconds(200.0);
			this.m_a.Tick += new EventHandler(a);
			h.Source = new Uri(FilePath);
			if (!IsMovie)
			{
				h.Visibility = Visibility.Collapsed;
			}
			h.Play();
			a(true);
		}

		private void a(bool A_0)
		{
			n.IsEnabled = A_0;
			o.IsEnabled = A_0;
			p.IsEnabled = A_0;
			l.IsEnabled = A_0;
			k.IsEnabled = A_0;
		}

		private void g(object A_0, RoutedEventArgs A_1)
		{
			a(true);
			string name = ((Image)l.Content).Name;
			if (name.Equals("play"))
			{
				h.Play();
				l.Content = this.m_b[0];
			}
			else if (name.Equals("pause"))
			{
				h.Pause();
				l.Content = this.m_b[1];
			}
		}

		private void f(object A_0, RoutedEventArgs A_1)
		{
			h.Stop();
			l.Content = this.m_b[1];
			a(false);
			l.IsEnabled = true;
		}

		private void e(object A_0, RoutedEventArgs A_1)
		{
			h.Position += TimeSpan.FromSeconds(10.0);
		}

		private void d(object A_0, RoutedEventArgs A_1)
		{
			h.Position -= TimeSpan.FromSeconds(10.0);
		}

		private void c(object A_0, RoutedEventArgs A_1)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Video Files (*.mp4)|*.mp4";
			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				h.Source = new Uri(openFileDialog.FileName);
				l.IsEnabled = true;
			}
		}

		private void b(object A_0, RoutedEventArgs A_1)
		{
		}

		private void a(object A_0, RoutedEventArgs A_1)
		{
			if (h.NaturalDuration.HasTimeSpan)
			{
				TimeSpan timeSpan = h.NaturalDuration.TimeSpan;
				k.Maximum = timeSpan.TotalSeconds;
				k.SmallChange = 1.0;
				k.LargeChange = Math.Min(10, timeSpan.Seconds / 10);
			}
			this.m_a.Start();
		}

		private void a(object A_0, EventArgs A_1)
		{
			if (!this.m_c)
			{
				k.Value = h.Position.TotalSeconds;
				this.m_f = k.Value;
			}
		}

		private void a(object A_0, DragStartedEventArgs A_1)
		{
			this.m_c = true;
		}

		private void a(object A_0, DragCompletedEventArgs A_1)
		{
			this.m_c = false;
			h.Position = TimeSpan.FromSeconds(k.Value);
		}

		[DllImport("user32.dll")]
		private static extern uint GetDoubleClickTime();

		private void a(object A_0, MouseButtonEventArgs A_1)
		{
			if (!this.m_d.Enabled)
			{
				this.m_d.Enabled = true;
			}
			else if (this.m_d.Enabled)
			{
				if (!this.m_e)
				{
					this.g.Children.Remove(h);
					base.Background = new SolidColorBrush(Colors.Black);
					base.Content = h;
					base.WindowStyle = WindowStyle.None;
					base.WindowState = WindowState.Maximized;
					h.Position = TimeSpan.FromSeconds(this.m_f);
				}
				else
				{
					base.Content = this.g;
					this.g.Children.Add(h);
					base.Background = new SolidColorBrush(Colors.White);
					base.WindowStyle = WindowStyle.SingleBorderWindow;
					base.WindowState = WindowState.Normal;
					h.Position = TimeSpan.FromSeconds(this.m_f);
				}
				this.m_e = !this.m_e;
			}
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!r)
			{
				r = true;
				Uri resourceLocator = new Uri("/ReadPageModule;component/movieplayer.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
			}
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[DebuggerNonUserCode]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.g = (Grid)target;
				break;
			case 2:
				h = (MediaElement)target;
				h.MediaOpened += new RoutedEventHandler(a);
				h.MouseLeftButtonUp += new MouseButtonEventHandler(a);
				break;
			case 3:
				i = (StackPanel)target;
				break;
			case 4:
				j = (StackPanel)target;
				break;
			case 5:
				k = (Slider)target;
				k.AddHandler(Thumb.DragStartedEvent, new DragStartedEventHandler(a));
				k.AddHandler(Thumb.DragCompletedEvent, new DragCompletedEventHandler(a));
				break;
			case 6:
				l = (System.Windows.Controls.Button)target;
				l.Click += new RoutedEventHandler(g);
				break;
			case 7:
				m = (Image)target;
				break;
			case 8:
				n = (System.Windows.Controls.Button)target;
				n.Click += new RoutedEventHandler(f);
				break;
			case 9:
				o = (System.Windows.Controls.Button)target;
				o.Click += new RoutedEventHandler(d);
				break;
			case 10:
				p = (System.Windows.Controls.Button)target;
				p.Click += new RoutedEventHandler(e);
				break;
			case 11:
				q = (Slider)target;
				break;
			default:
				r = true;
				break;
			}
		}
	}
}
