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
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

namespace PaperLess_Emeeting
{
	public class MoviePlayer : Window, IComponentConnector
	{
		private DispatcherTimer timer;

		private List<Image> playButtonImageList;

		public string filePath;

		private bool isDragging;

		private Timer timeClick;

		private bool fullScreen;

		private double currentposition;

		internal Grid LayoutRoot;

		internal MediaElement MediaEL;

		internal StackPanel controlPanel;

		internal StackPanel SPSeekBar;

		internal Slider seekBar;

		internal Button btnPlay;

		internal Image pause;

		internal Button btnStop;

		internal Button btnMoveBackward;

		internal Button btnMoveForward;

		internal Slider volumeSlider;

		private bool _contentLoaded;

		public MoviePlayer(string FilePath, bool IsMovie, bool isToolBarEnabled)
		{
			Timer timer = new Timer((int)GetDoubleClickTime());
			timer.AutoReset = false;
			timeClick = timer;
			base._002Ector();
			filePath = FilePath;
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
			playButtonImageList = list;
			IsPlaying(false);
			this.timer = new DispatcherTimer();
			this.timer.Interval = TimeSpan.FromMilliseconds(200.0);
			this.timer.Tick += new EventHandler(timer_Tick);
			MediaEL.Source = new Uri(FilePath);
			if (!IsMovie)
			{
				MediaEL.Visibility = Visibility.Collapsed;
			}
			if (isToolBarEnabled)
			{
				controlPanel.IsEnabled = true;
			}
			else
			{
				controlPanel.IsEnabled = false;
			}
			MediaEL.Play();
			IsPlaying(true);
		}

		private void IsPlaying(bool bValue)
		{
			btnStop.IsEnabled = bValue;
			btnMoveBackward.IsEnabled = bValue;
			btnMoveForward.IsEnabled = bValue;
			btnPlay.IsEnabled = bValue;
			seekBar.IsEnabled = bValue;
		}

		private void btnPlay_Click(object sender, RoutedEventArgs e)
		{
			IsPlaying(true);
			string name = ((Image)btnPlay.Content).Name;
			if (name.Equals("play"))
			{
				MediaEL.Play();
				btnPlay.Content = playButtonImageList[0];
			}
			else if (name.Equals("pause"))
			{
				MediaEL.Pause();
				btnPlay.Content = playButtonImageList[1];
			}
		}

		private void btnStop_Click(object sender, RoutedEventArgs e)
		{
			MediaEL.Stop();
			btnPlay.Content = playButtonImageList[1];
			IsPlaying(false);
			btnPlay.IsEnabled = true;
			Close();
		}

		private void btnMoveForward_Click(object sender, RoutedEventArgs e)
		{
			MediaEL.Position += TimeSpan.FromSeconds(10.0);
		}

		private void btnMoveBackward_Click(object sender, RoutedEventArgs e)
		{
			MediaEL.Position -= TimeSpan.FromSeconds(10.0);
		}

		private void btnScreenShot_Click(object sender, RoutedEventArgs e)
		{
		}

		private void MediaEL_MediaOpened(object sender, RoutedEventArgs e)
		{
			if (MediaEL.NaturalDuration.HasTimeSpan)
			{
				TimeSpan timeSpan = MediaEL.NaturalDuration.TimeSpan;
				seekBar.Maximum = timeSpan.TotalSeconds;
				seekBar.SmallChange = 1.0;
				seekBar.LargeChange = Math.Min(10, timeSpan.Seconds / 10);
			}
			timer.Start();
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			if (!isDragging)
			{
				seekBar.Value = MediaEL.Position.TotalSeconds;
				currentposition = seekBar.Value;
			}
		}

		private void seekBar_DragStarted(object sender, DragStartedEventArgs e)
		{
			isDragging = true;
		}

		private void seekBar_DragCompleted(object sender, DragCompletedEventArgs e)
		{
			isDragging = false;
			MediaEL.Position = TimeSpan.FromSeconds(seekBar.Value);
		}

		[DllImport("user32.dll")]
		private static extern uint GetDoubleClickTime();

		private void MediaEL_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (!timeClick.Enabled)
			{
				timeClick.Enabled = true;
			}
			else if (timeClick.Enabled)
			{
				if (!fullScreen)
				{
					LayoutRoot.Children.Remove(MediaEL);
					base.Background = new SolidColorBrush(Colors.Black);
					base.Content = MediaEL;
					base.WindowStyle = WindowStyle.None;
					base.WindowState = WindowState.Maximized;
					MediaEL.Position = TimeSpan.FromSeconds(currentposition);
				}
				else
				{
					base.Content = LayoutRoot;
					LayoutRoot.Children.Add(MediaEL);
					base.Background = new SolidColorBrush(Colors.White);
					base.WindowStyle = WindowStyle.SingleBorderWindow;
					base.WindowState = WindowState.Normal;
					MediaEL.Position = TimeSpan.FromSeconds(currentposition);
				}
				fullScreen = !fullScreen;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/movieplayer.xaml", UriKind.Relative);
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
				LayoutRoot = (Grid)target;
				break;
			case 2:
				MediaEL = (MediaElement)target;
				MediaEL.MediaOpened += new RoutedEventHandler(MediaEL_MediaOpened);
				MediaEL.MouseLeftButtonUp += new MouseButtonEventHandler(MediaEL_MouseLeftButtonUp);
				break;
			case 3:
				controlPanel = (StackPanel)target;
				break;
			case 4:
				SPSeekBar = (StackPanel)target;
				break;
			case 5:
				seekBar = (Slider)target;
				seekBar.AddHandler(Thumb.DragStartedEvent, new DragStartedEventHandler(seekBar_DragStarted));
				seekBar.AddHandler(Thumb.DragCompletedEvent, new DragCompletedEventHandler(seekBar_DragCompleted));
				break;
			case 6:
				btnPlay = (Button)target;
				btnPlay.Click += new RoutedEventHandler(btnPlay_Click);
				break;
			case 7:
				pause = (Image)target;
				break;
			case 8:
				btnStop = (Button)target;
				btnStop.Click += new RoutedEventHandler(btnStop_Click);
				break;
			case 9:
				btnMoveBackward = (Button)target;
				btnMoveBackward.Click += new RoutedEventHandler(btnMoveBackward_Click);
				break;
			case 10:
				btnMoveForward = (Button)target;
				btnMoveForward.Click += new RoutedEventHandler(btnMoveForward_Click);
				break;
			case 11:
				volumeSlider = (Slider)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
}
