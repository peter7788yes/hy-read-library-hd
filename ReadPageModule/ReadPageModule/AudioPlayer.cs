using BookFormatLoader;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	public class AudioPlayer : Window, IComponentConnector
	{
		private DispatcherTimer m_a;

		private List<Image> m_b;

		private ObservableCollection<MediaList> m_c;

		private bool m_d;

		private bool m_e;

		private string m_f;

		private List<Media> m_g = new List<Media>();

		private bool m_h;

		private System.Timers.Timer m_i;

		private bool m_j;

		private double k;

		internal Grid l;

		internal MediaElement m;

		internal StackPanel n;

		internal StackPanel o;

		internal Slider p;

		internal ToggleButton q;

		internal ToggleButton r;

		internal System.Windows.Controls.Button s;

		internal Image t;

		internal System.Windows.Controls.Button u;

		internal System.Windows.Controls.Button v;

		internal System.Windows.Controls.Button w;

		internal Slider x;

		private bool y;

		public AudioPlayer(string FilePath, ObservableCollection<MediaList> ObservableMediaList, bool IsMovie)
		{
			System.Timers.Timer timer = new System.Timers.Timer((int)GetDoubleClickTime());
			timer.AutoReset = false;
			this.m_i = timer;
			base._002Ector();
			InitializeComponent();
			this.m_c = ObservableMediaList;
			this.m_f = FilePath;
			a(ObservableMediaList);
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
			m.Source = new Uri(FilePath);
			try
			{
				base.Title = this.m_f.Substring(this.m_f.LastIndexOf('\\') + 1);
			}
			catch
			{
				base.Title = "Audio Player";
			}
			if (!IsMovie)
			{
				m.Visibility = Visibility.Collapsed;
			}
			m.MediaEnded += new RoutedEventHandler(c);
			m.Play();
			a(true);
		}

		private void a(ObservableCollection<MediaList> A_0)
		{
			for (int i = 0; i < A_0.Count; i++)
			{
				for (int j = 0; j < A_0[i].mediaList.Count; j++)
				{
					if (A_0[i].mediaList[j].mediaType == "audio/mpeg")
					{
						this.m_g.Add(A_0[i].mediaList[j]);
					}
				}
			}
		}

		private void a(bool A_0)
		{
			u.IsEnabled = A_0;
			v.IsEnabled = A_0;
			w.IsEnabled = A_0;
			s.IsEnabled = A_0;
			p.IsEnabled = A_0;
		}

		private void j(object A_0, RoutedEventArgs A_1)
		{
			a(true);
			string name = ((Image)s.Content).Name;
			if (name.Equals("play"))
			{
				m.Play();
				s.Content = this.m_b[0];
			}
			else if (name.Equals("pause"))
			{
				m.Pause();
				s.Content = this.m_b[1];
			}
		}

		private void i(object A_0, RoutedEventArgs A_1)
		{
			m.Stop();
			s.Content = this.m_b[1];
			a(false);
			s.IsEnabled = true;
		}

		private void h(object A_0, RoutedEventArgs A_1)
		{
			m.Position += TimeSpan.FromSeconds(10.0);
		}

		private void g(object A_0, RoutedEventArgs A_1)
		{
			m.Position -= TimeSpan.FromSeconds(10.0);
		}

		private void f(object A_0, RoutedEventArgs A_1)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Video Files (*.mp4)|*.mp4";
			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				m.Source = new Uri(openFileDialog.FileName);
				s.IsEnabled = true;
			}
		}

		private void e(object A_0, RoutedEventArgs A_1)
		{
		}

		private void d(object A_0, RoutedEventArgs A_1)
		{
			if (m.NaturalDuration.HasTimeSpan)
			{
				TimeSpan timeSpan = m.NaturalDuration.TimeSpan;
				p.Maximum = timeSpan.TotalSeconds;
				p.SmallChange = 1.0;
				p.LargeChange = Math.Min(10, timeSpan.Seconds / 10);
			}
			this.m_a.Start();
		}

		private void a(object A_0, EventArgs A_1)
		{
			if (!this.m_h)
			{
				p.Value = m.Position.TotalSeconds;
				k = p.Value;
			}
		}

		private void a(object A_0, DragStartedEventArgs A_1)
		{
			this.m_h = true;
		}

		private void a(object A_0, DragCompletedEventArgs A_1)
		{
			this.m_h = false;
			m.Position = TimeSpan.FromSeconds(p.Value);
		}

		[DllImport("user32.dll")]
		private static extern uint GetDoubleClickTime();

		private void a(object A_0, MouseButtonEventArgs A_1)
		{
			if (!this.m_i.Enabled)
			{
				this.m_i.Enabled = true;
			}
			else if (this.m_i.Enabled)
			{
				if (!this.m_j)
				{
					l.Children.Remove(m);
					base.Background = new SolidColorBrush(Colors.Black);
					base.Content = m;
					base.WindowStyle = WindowStyle.None;
					base.WindowState = WindowState.Maximized;
					m.Position = TimeSpan.FromSeconds(k);
				}
				else
				{
					base.Content = l;
					l.Children.Add(m);
					base.Background = new SolidColorBrush(Colors.White);
					base.WindowStyle = WindowStyle.SingleBorderWindow;
					base.WindowState = WindowState.Normal;
					m.Position = TimeSpan.FromSeconds(k);
				}
				this.m_j = !this.m_j;
			}
		}

		private void c(object A_0, RoutedEventArgs A_1)
		{
			(A_0 as MediaElement).Stop();
			if (this.m_d)
			{
				(A_0 as MediaElement).Play();
			}
			else if (this.m_e)
			{
				int num = 0;
				for (int i = 0; i < this.m_g.Count; i++)
				{
					if (this.m_g[i].mediaSourcePath == this.m_f && this.m_g[i].downloadStatus)
					{
						num = i + 1;
						break;
					}
				}
				this.m_f = ((num == this.m_g.Count) ? this.m_g[0].mediaSourcePath : this.m_g[num].mediaSourcePath);
				m.Source = new Uri(this.m_f);
				try
				{
					base.Title = this.m_f.Substring(this.m_f.LastIndexOf('\\') + 1);
				}
				catch
				{
					base.Title = "Audio Player";
				}
				(A_0 as MediaElement).Play();
			}
			else
			{
				this.i(A_0, A_1);
			}
		}

		private void b(object A_0, RoutedEventArgs A_1)
		{
			this.m_d = !this.m_d;
			bool? isChecked = q.IsChecked;
			bool flag = true;
			if ((isChecked.GetValueOrDefault() == flag) & isChecked.HasValue)
			{
				r.IsChecked = false;
				this.m_e = false;
			}
		}

		private void a(object A_0, RoutedEventArgs A_1)
		{
			this.m_e = !this.m_e;
			bool? isChecked = r.IsChecked;
			bool flag = true;
			if ((isChecked.GetValueOrDefault() == flag) & isChecked.HasValue)
			{
				q.IsChecked = false;
				this.m_d = false;
			}
		}

		private void a()
		{
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!y)
			{
				y = true;
				Uri resourceLocator = new Uri("/ReadPageModule;component/audioplayer.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
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
				l = (Grid)target;
				break;
			case 2:
				m = (MediaElement)target;
				m.MediaOpened += new RoutedEventHandler(d);
				m.MouseLeftButtonUp += new MouseButtonEventHandler(a);
				break;
			case 3:
				n = (StackPanel)target;
				break;
			case 4:
				o = (StackPanel)target;
				break;
			case 5:
				p = (Slider)target;
				p.AddHandler(Thumb.DragStartedEvent, new DragStartedEventHandler(a));
				p.AddHandler(Thumb.DragCompletedEvent, new DragCompletedEventHandler(a));
				break;
			case 6:
				q = (ToggleButton)target;
				q.Click += new RoutedEventHandler(b);
				break;
			case 7:
				r = (ToggleButton)target;
				r.Click += new RoutedEventHandler(a);
				break;
			case 8:
				s = (System.Windows.Controls.Button)target;
				s.Click += new RoutedEventHandler(j);
				break;
			case 9:
				t = (Image)target;
				break;
			case 10:
				u = (System.Windows.Controls.Button)target;
				u.Click += new RoutedEventHandler(i);
				break;
			case 11:
				v = (System.Windows.Controls.Button)target;
				v.Click += new RoutedEventHandler(g);
				break;
			case 12:
				w = (System.Windows.Controls.Button)target;
				w.Click += new RoutedEventHandler(h);
				break;
			case 13:
				x = (Slider)target;
				break;
			default:
				y = true;
				break;
			}
		}
	}
}
