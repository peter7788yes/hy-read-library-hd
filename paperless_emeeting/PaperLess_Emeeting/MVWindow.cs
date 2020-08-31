using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PaperLess_Emeeting.App_Code.MessageBox;
using PaperLess_Emeeting.App_Code.Socket;
using PaperLess_Emeeting.App_Code.ViewModel;
using PaperlessSync.Broadcast.Socket;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace PaperLess_Emeeting
{
	public class MVWindow : Window, IDisposable, IComponentConnector
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass1a
		{
			public SocketClient socketClient;

			public void _003CbtnClose_MouseLeftButtonDown_003Eb__19()
			{
				if (socketClient != null && socketClient.GetIsConnected())
				{
					socketClient.broadcast("{\"cmd\":\"R.CB\"}");
				}
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass1e
		{
			public bool syncSwitch;

			public SocketClient socketClient;

			public MVWindow _003C_003E4__this;

			public void _003CbtnSync_MouseLeftButtonDown_003Eb__1c()
			{
				Action action = null;
				if (socketClient != null && socketClient.GetIsConnected())
				{
					socketClient.syncSwitch(syncSwitch);
					return;
				}
				_003C_003E4__this.IsInSync = false;
				_003C_003E4__this.IsSyncOwner = false;
				Dispatcher dispatcher = _003C_003E4__this.Dispatcher;
				if (action == null)
				{
					action = new Action(_003CbtnSync_MouseLeftButtonDown_003Eb__1d);
				}
				dispatcher.BeginInvoke(action);
			}

			private void _003CbtnSync_MouseLeftButtonDown_003Eb__1d()
			{
				AutoClosingMessageBox.Show("同步伺服器尚未啟動，請聯絡議事管理員開啟同步");
				_003C_003E4__this.btnSync.Source = ButtonTool.GetSyncButtonImage(_003C_003E4__this.IsInSync, _003C_003E4__this.IsSyncOwner);
				_003C_003E4__this.cbBooks.Visibility = Visibility.Visible;
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass22
		{
			public long ecTime;

			public MVWindow _003C_003E4__this;

			public string function;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass24
		{
			public _003C_003Ec__DisplayClass22 CS_0024_003C_003E8__locals23;

			public DateTime mydate;

			public void _003CSentToOther_003Eb__21()
			{
				SocketClient instance = Singleton_Socket.GetInstance(CS_0024_003C_003E8__locals23._003C_003E4__this.MeetingID, CS_0024_003C_003E8__locals23._003C_003E4__this.UserID, CS_0024_003C_003E8__locals23._003C_003E4__this.UserName, true);
				if (instance != null && instance.GetIsConnected())
				{
					instance.broadcast("{\"execTime\":" + CS_0024_003C_003E8__locals23.ecTime + ",\"action\":\"" + CS_0024_003C_003E8__locals23.function + "\",\"actionTime\":\"" + mydate.ToString("HH:mm:ss.fff") + "\",\"cmd\":\"R.SV\"}");
				}
			}
		}

		public DispatcherTimer MediaPlayerTimer;

		public DispatcherTimer MouseTimer;

		public Point lastMousePoint = new Point(0.0, 0.0);

		public bool IsSeekBarDragging;

		public bool IsAlwaysShowHeaderFooter;

		public double moiveTotalMilliseconds;

		public MVWindow_IsInSync_And_IsSyncOwner_Function MVWindow_IsInSync_And_IsSyncOwner_Callback;

		public MVWindow_MVAction_Function MVWindow_MVAction_Callback;

		private bool IsInSync;

		private bool IsSyncOwner;

		private long DeltaUTC;

		public string pageJson = "";

		public Dictionary<string, BookVM> cbBooksData = new Dictionary<string, BookVM>();

		internal MediaElement mediaPlayer;

		internal Image img_animation;

		internal DockPanel HeaderDP;

		internal Image btnClose;

		internal Image btnSync;

		internal System.Windows.Controls.ComboBox cbBooks;

		internal Image btnLight;

		internal DockPanel FooterDP;

		internal Image btnFunction;

		internal TextBlock txtCurrentTime;

		internal TextBlock txtEndTime;

		internal System.Windows.Controls.ProgressBar pb;

		internal Slider SeekBar;

		private bool _contentLoaded;

		[CompilerGenerated]
		private string _003CMeetingID_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserID_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserName_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CFilePath_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CFileID_003Ek__BackingField;

		[CompilerGenerated]
		private static System.Windows.Input.MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegatee;

		[CompilerGenerated]
		private static System.Windows.Input.MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegatef;

		[CompilerGenerated]
		private static System.Windows.Input.MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate10;

		[CompilerGenerated]
		private static System.Windows.Input.MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate11;

		[CompilerGenerated]
		private static System.Windows.Input.MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate12;

		[CompilerGenerated]
		private static System.Windows.Input.MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate13;

		[CompilerGenerated]
		private static System.Windows.Input.MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate14;

		[CompilerGenerated]
		private static System.Windows.Input.MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate15;

		public string MeetingID
		{
			[CompilerGenerated]
			get
			{
				return _003CMeetingID_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CMeetingID_003Ek__BackingField = value;
			}
		}

		public string UserID
		{
			[CompilerGenerated]
			get
			{
				return _003CUserID_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CUserID_003Ek__BackingField = value;
			}
		}

		public string UserName
		{
			[CompilerGenerated]
			get
			{
				return _003CUserName_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CUserName_003Ek__BackingField = value;
			}
		}

		public string FilePath
		{
			[CompilerGenerated]
			get
			{
				return _003CFilePath_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CFilePath_003Ek__BackingField = value;
			}
		}

		public string FileID
		{
			[CompilerGenerated]
			get
			{
				return _003CFileID_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CFileID_003Ek__BackingField = value;
			}
		}

		public event Home_OpenBookFromReader_Function Home_OpenBookFromReader_Event;

		public MVWindow(Dictionary<string, BookVM> cbBooksData, Home_OpenBookFromReader_Function callback, string FilePath, string FileID, string pageJson = "")
		{
			MouseTool.ShowLoading();
			InitializeComponent();
			this.cbBooksData = cbBooksData;
			this.Home_OpenBookFromReader_Event = callback;
			this.FilePath = FilePath;
			this.FileID = FileID;
			this.pageJson = pageJson;
			base.Loaded += new RoutedEventHandler(MVWindow_Loaded);
			base.Unloaded += new RoutedEventHandler(MVWindow_Unloaded);
		}

		private void MVWindow_Closed(object sender, EventArgs e)
		{
		}

		private void MVWindow_Closing(object sender, CancelEventArgs e)
		{
		}

		public MVWindow(string FilePath, string pageJson = "")
		{
			MouseTool.ShowLoading();
			InitializeComponent();
			this.FilePath = FilePath;
			FileID = FileID;
			this.pageJson = pageJson;
			base.Loaded += new RoutedEventHandler(MVWindow_Loaded);
			base.Unloaded += new RoutedEventHandler(MVWindow_Unloaded);
		}

		private void MVWindow_Unloaded(object sender, RoutedEventArgs e)
		{
			Singleton_Socket.mvWindow_OpenIEventManager.MVWindow_IsInSync_And_IsSyncOwner_Event -= MVWindow_IsInSync_And_IsSyncOwner_Callback;
			Singleton_Socket.mvWindow_OpenIEventManager.MVWindow_MVAction_Event -= MVWindow_MVAction_Callback;
		}

		private void MVWindow_Loaded(object sender, RoutedEventArgs e)
		{
			Task.Factory.StartNew(new Action(_003CMVWindow_Loaded_003Eb__0));
		}

		private void InitEvent()
		{
			MVWindow_IsInSync_And_IsSyncOwner_Callback = new MVWindow_IsInSync_And_IsSyncOwner_Function(IsInSync_And_IsSyncOwner);
			MVWindow_MVAction_Callback = new MVWindow_MVAction_Function(MVAction);
			Singleton_Socket.mvWindow_OpenIEventManager.MVWindow_IsInSync_And_IsSyncOwner_Event += MVWindow_IsInSync_And_IsSyncOwner_Callback;
			Singleton_Socket.mvWindow_OpenIEventManager.MVWindow_MVAction_Event += MVWindow_MVAction_Callback;
			MouseTimer = new DispatcherTimer();
			MouseTimer.Interval = TimeSpan.FromMilliseconds(1100.0);
			MouseTimer.Tick += new EventHandler(MouseTimer_Tick);
			MouseTimer.Start();
			MediaPlayerTimer = new DispatcherTimer();
			MediaPlayerTimer.Interval = TimeSpan.FromMilliseconds(200.0);
			MediaPlayerTimer.Tick += new EventHandler(MediaPlayerTimer_Tick);
			Image image = btnClose;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegatee == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegatee = new System.Windows.Input.MouseEventHandler(_003CInitEvent_003Eb__2);
			}
			image.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegatee;
			Image image2 = btnClose;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegatef == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegatef = new System.Windows.Input.MouseEventHandler(_003CInitEvent_003Eb__3);
			}
			image2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegatef;
			btnClose.MouseLeftButtonDown += new MouseButtonEventHandler(btnClose_MouseLeftButtonDown);
			Image image3 = btnSync;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate10 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate10 = new System.Windows.Input.MouseEventHandler(_003CInitEvent_003Eb__4);
			}
			image3.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate10;
			Image image4 = btnSync;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate11 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate11 = new System.Windows.Input.MouseEventHandler(_003CInitEvent_003Eb__5);
			}
			image4.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate11;
			btnSync.MouseLeftButtonDown += new MouseButtonEventHandler(btnSync_MouseLeftButtonDown);
			Image image5 = btnLight;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate12 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate12 = new System.Windows.Input.MouseEventHandler(_003CInitEvent_003Eb__6);
			}
			image5.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate12;
			Image image6 = btnLight;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate13 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate13 = new System.Windows.Input.MouseEventHandler(_003CInitEvent_003Eb__7);
			}
			image6.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate13;
			btnLight.MouseLeftButtonDown += new MouseButtonEventHandler(btnSync_MouseLeftButtonDown);
			Image image7 = btnFunction;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate14 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate14 = new System.Windows.Input.MouseEventHandler(_003CInitEvent_003Eb__8);
			}
			image7.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate14;
			Image image8 = btnFunction;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate15 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate15 = new System.Windows.Input.MouseEventHandler(_003CInitEvent_003Eb__9);
			}
			image8.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate15;
			HeaderDP.MouseEnter += new System.Windows.Input.MouseEventHandler(_003CInitEvent_003Eb__a);
			HeaderDP.MouseLeave += new System.Windows.Input.MouseEventHandler(_003CInitEvent_003Eb__b);
			FooterDP.MouseEnter += new System.Windows.Input.MouseEventHandler(_003CInitEvent_003Eb__c);
			FooterDP.MouseLeave += new System.Windows.Input.MouseEventHandler(_003CInitEvent_003Eb__d);
			SeekBar.ValueChanged += new RoutedPropertyChangedEventHandler<double>(SeekBar_ValueChanged);
			mediaPlayer.MediaOpened += new RoutedEventHandler(MediaPlayer_MediaOpened);
			mediaPlayer.Play();
			btnFunction.Source = new BitmapImage(new Uri("images/mv_pause.png", UriKind.Relative));
			img_animation.MouseLeftButtonDown += new MouseButtonEventHandler(AnimationController_MouseLeftButtonDown);
			btnFunction.MouseLeftButtonDown += new MouseButtonEventHandler(AnimationController_MouseLeftButtonDown);
			mediaPlayer.MouseLeftButtonDown += new MouseButtonEventHandler(AnimationController_MouseLeftButtonDown);
			cbBooks.SelectionChanged += new SelectionChangedEventHandler(cbBooks_SelectionChanged);
			ParsePageJson();
		}

		private void SeekBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			pb.Value = SeekBar.Value;
		}

		private void cbBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (!IsInSync || IsSyncOwner)
			{
				System.Windows.Controls.ComboBox comboBox = (System.Windows.Controls.ComboBox)sender;
				BookVM bookVM = (BookVM)comboBox.SelectedValue;
				if (bookVM != null && this.Home_OpenBookFromReader_Event != null)
				{
					this.Home_OpenBookFromReader_Event(MeetingID, bookVM, cbBooksData);
				}
			}
		}

		private void ParsePageJson()
		{
			Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(pageJson);
			Dictionary<string, object> dictionary2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(dictionary["msg"].ToString());
			string text = dictionary2["action"].ToString();
			string s = dictionary2["execTime"].ToString();
			string s2 = dictionary2["actionTime"].ToString();
			switch (text)
			{
			case "play":
			{
				long num = long.Parse(s);
				long num2 = 0L;
				num2 = DateTool.GetCurrentTimeInUnixMillis() - DeltaUTC;
				long num3 = num2 - num;
				mediaPlayer.Position = TimeSpan.Parse(s2) + new TimeSpan(0, 0, 0, 0, (int)num3);
				mediaPlayer.Play();
				IsAlwaysShowHeaderFooter = false;
				break;
			}
			case "pause":
				mediaPlayer.Play();
				mediaPlayer.Position = TimeSpan.Parse(s2);
				mediaPlayer.Pause();
				IsAlwaysShowHeaderFooter = true;
				break;
			}
		}

		private void AnimationController_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (!IsInSync || IsSyncOwner)
			{
				if (GetMediaState(mediaPlayer) == MediaState.Play)
				{
					btnFunction.Source = new BitmapImage(new Uri("images/mv_right.png", UriKind.Relative));
					mediaPlayer.Pause();
					SentToOther("pause");
					img_animation.Source = new BitmapImage(new Uri("images/MVWindow_Animation_Pause.png", UriKind.Relative));
					ChangeShowHeaderFooterDP(true);
					IsAlwaysShowHeaderFooter = true;
				}
				else
				{
					btnFunction.Source = new BitmapImage(new Uri("images/mv_pause.png", UriKind.Relative));
					mediaPlayer.Play();
					SentToOther("play");
					img_animation.Source = new BitmapImage(new Uri("images/MVWindow_Animation_Play.png", UriKind.Relative));
					IsAlwaysShowHeaderFooter = false;
				}
				ShowAnimation();
			}
		}

		private void ShowAnimation()
		{
			img_animation.Opacity = 1.0;
			img_animation.Width = 128.0;
			img_animation.Height = 128.0;
			img_animation.Visibility = Visibility.Visible;
			DoubleAnimation doubleAnimation = new DoubleAnimation();
			doubleAnimation.From = 128.0;
			doubleAnimation.To = 256.0;
			doubleAnimation.Duration = TimeSpan.FromSeconds(0.5);
			DoubleAnimation doubleAnimation2 = doubleAnimation;
			DoubleAnimation doubleAnimation3 = new DoubleAnimation();
			doubleAnimation3.From = 128.0;
			doubleAnimation3.To = 256.0;
			doubleAnimation3.Duration = TimeSpan.FromSeconds(0.5);
			DoubleAnimation doubleAnimation4 = doubleAnimation3;
			DoubleAnimation doubleAnimation5 = new DoubleAnimation();
			doubleAnimation5.From = 1.0;
			doubleAnimation5.To = 0.0;
			doubleAnimation5.Duration = TimeSpan.FromSeconds(0.5);
			DoubleAnimation doubleAnimation6 = doubleAnimation5;
			Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath(FrameworkElement.WidthProperty));
			Storyboard.SetTarget(doubleAnimation2, img_animation);
			Storyboard.SetTargetProperty(doubleAnimation4, new PropertyPath(FrameworkElement.HeightProperty));
			Storyboard.SetTarget(doubleAnimation4, img_animation);
			Storyboard.SetTargetProperty(doubleAnimation6, new PropertyPath(UIElement.OpacityProperty));
			Storyboard.SetTarget(doubleAnimation6, img_animation);
			Storyboard storyboard = new Storyboard();
			storyboard.Children.Add(doubleAnimation2);
			storyboard.Children.Add(doubleAnimation4);
			storyboard.Children.Add(doubleAnimation6);
			storyboard.Begin();
		}

		private void sb_Completed(object sender, EventArgs e)
		{
			img_animation.Width = 0.0;
			img_animation.Height = 0.0;
			img_animation.Visibility = Visibility.Collapsed;
			Storyboard storyboard = (Storyboard)sender;
			storyboard.Completed -= new EventHandler(sb_Completed);
		}

		private void btnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (!IsInSync || IsSyncOwner)
			{
				mediaPlayer.Stop();
				mediaPlayer.Close();
				Close();
				if (IsInSync && IsSyncOwner)
				{
					_003C_003Ec__DisplayClass1a _003C_003Ec__DisplayClass1a = new _003C_003Ec__DisplayClass1a();
					_003C_003Ec__DisplayClass1a.socketClient = Singleton_Socket.GetInstance(MeetingID, UserID, UserName, true);
					Task.Factory.StartNew(new Action(_003C_003Ec__DisplayClass1a._003CbtnClose_MouseLeftButtonDown_003Eb__19));
				}
			}
		}

		private void MVAction(JObject jObject)
		{
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<JObject>(MVAction), jObject);
				return;
			}
			string text = (string)jObject["action"];
			string text2 = (string)jObject["execTime"];
			string s = (string)jObject["actionTime"];
			switch (text)
			{
			case "play":
				mediaPlayer.Position = TimeSpan.Parse(s);
				mediaPlayer.Pause();
				Thread.Sleep(1);
				mediaPlayer.Play();
				IsAlwaysShowHeaderFooter = false;
				ChangeShowHeaderFooterDP(false);
				btnFunction.Source = new BitmapImage(new Uri("images/mv_pause.png", UriKind.Relative));
				img_animation.Source = new BitmapImage(new Uri("images/MVWindow_Animation_Play.png", UriKind.Relative));
				break;
			case "pause":
				mediaPlayer.Position = TimeSpan.Parse(s);
				mediaPlayer.Play();
				Thread.Sleep(1);
				mediaPlayer.Pause();
				IsAlwaysShowHeaderFooter = true;
				ChangeShowHeaderFooterDP(true);
				btnFunction.Source = new BitmapImage(new Uri("images/mv_right.png", UriKind.Relative));
				img_animation.Source = new BitmapImage(new Uri("images/MVWindow_Animation_Pause.png", UriKind.Relative));
				break;
			case "stop":
				mediaPlayer.Stop();
				break;
			}
			ShowAnimation();
		}

		private void IsInSync_And_IsSyncOwner(JArray jArry)
		{
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<JArray>(IsInSync_And_IsSyncOwner), jArry);
				return;
			}
			foreach (JToken item in jArry)
			{
				Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(item.ToString());
				string userID = dictionary["clientId"].ToString();
				userID = Socket_FixEmailUserID.FromSocket(userID);
				dictionary["clientName"].ToString();
				dictionary["clientType"].ToString();
				string text = dictionary["status"].ToString();
				if (userID.Equals(UserID))
				{
					switch (text)
					{
					case "-1":
						IsInSync = true;
						IsSyncOwner = true;
						IsAlwaysShowHeaderFooter = false;
						break;
					case "0":
						IsInSync = true;
						IsSyncOwner = false;
						if (GetMediaState(mediaPlayer) == MediaState.Pause)
						{
							IsAlwaysShowHeaderFooter = true;
							ChangeShowHeaderFooterDP(true);
						}
						else if (GetMediaState(mediaPlayer) == MediaState.Pause)
						{
							IsAlwaysShowHeaderFooter = false;
							ChangeShowHeaderFooterDP(false);
						}
						break;
					case "1":
						IsInSync = false;
						IsSyncOwner = false;
						IsAlwaysShowHeaderFooter = false;
						break;
					default:
						IsInSync = false;
						IsSyncOwner = false;
						IsAlwaysShowHeaderFooter = false;
						break;
					}
					break;
				}
			}
			ChangeSyncButtonLight(IsInSync, IsSyncOwner);
		}

		private void ChangeSyncButtonLight(bool IsInSync, bool IsSyncOwner)
		{
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<bool, bool>(ChangeSyncButtonLight), IsInSync, IsSyncOwner);
				return;
			}
			btnSync.Source = ButtonTool.GetSyncButtonImage(IsInSync, IsSyncOwner);
			btnLight.Source = ButtonTool.GetSyncButtonImage(IsInSync, IsSyncOwner);
		}

		private void btnSync_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			_003C_003Ec__DisplayClass1e _003C_003Ec__DisplayClass1e = new _003C_003Ec__DisplayClass1e();
			_003C_003Ec__DisplayClass1e._003C_003E4__this = this;
			_003C_003Ec__DisplayClass1e.syncSwitch = false;
			if (!IsInSync)
			{
				IsInSync = true;
				_003C_003Ec__DisplayClass1e.syncSwitch = true;
			}
			else
			{
				IsInSync = false;
				_003C_003Ec__DisplayClass1e.syncSwitch = false;
			}
			btnSync.Source = ButtonTool.GetSyncButtonImage(IsInSync, IsSyncOwner);
			_003C_003Ec__DisplayClass1e.socketClient = Singleton_Socket.GetInstance(MeetingID, UserID, UserName, false);
			Task.Factory.StartNew(new Action(_003C_003Ec__DisplayClass1e._003CbtnSync_MouseLeftButtonDown_003Eb__1c));
		}

		private void SeekBar_DragStarted(object sender, DragStartedEventArgs e)
		{
			IsSeekBarDragging = true;
		}

		private void SeekBar_DragCompleted(object sender, DragCompletedEventArgs e)
		{
			mediaPlayer.Position = TimeSpan.FromMilliseconds(SeekBar.Value);
			if (GetMediaState(mediaPlayer) == MediaState.Play)
			{
				SentToOther("play");
			}
			else
			{
				mediaPlayer.Play();
				Thread.Sleep(1);
				mediaPlayer.Pause();
				SentToOther("pause");
			}
			IsSeekBarDragging = false;
		}

		private void SentToOther(string function)
		{
			_003C_003Ec__DisplayClass22 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass22();
			_003C_003Ec__DisplayClass.function = function;
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			_003C_003Ec__DisplayClass.ecTime = DateTool.GetCurrentTimeInUnixMillis() - DeltaUTC;
			if (IsInSync && IsSyncOwner)
			{
				_003C_003Ec__DisplayClass24 _003C_003Ec__DisplayClass2 = new _003C_003Ec__DisplayClass24();
				_003C_003Ec__DisplayClass2.CS_0024_003C_003E8__locals23 = _003C_003Ec__DisplayClass;
				_003C_003Ec__DisplayClass2.mydate = new DateTime(mediaPlayer.Position.Ticks);
				Task.Factory.StartNew(new Action(_003C_003Ec__DisplayClass2._003CSentToOther_003Eb__21));
			}
		}

		private void ChangeShowHeaderFooterDP(bool toShowing)
		{
			if (toShowing)
			{
				HeaderDP.Visibility = Visibility.Visible;
				FooterDP.Visibility = Visibility.Visible;
				Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
				btnLight.Visibility = Visibility.Collapsed;
			}
			else if (!IsAlwaysShowHeaderFooter)
			{
				HeaderDP.Visibility = Visibility.Collapsed;
				FooterDP.Visibility = Visibility.Collapsed;
				btnLight.Visibility = Visibility.Visible;
				Mouse.OverrideCursor = System.Windows.Input.Cursors.None;
			}
		}

		private MediaState GetMediaState(MediaElement myMedia)
		{
			FieldInfo field = typeof(MediaElement).GetField("_helper", BindingFlags.Instance | BindingFlags.NonPublic);
			object value = field.GetValue(myMedia);
			FieldInfo field2 = value.GetType().GetField("_currentState", BindingFlags.Instance | BindingFlags.NonPublic);
			return (MediaState)field2.GetValue(value);
		}

		private void InitUI()
		{
			base.Width = Screen.PrimaryScreen.Bounds.Width;
			base.Height = Screen.PrimaryScreen.Bounds.Height;
			base.Top = 0.0;
			base.Left = 0.0;
			base.WindowState = WindowState.Normal;
			base.WindowStyle = WindowStyle.None;
			base.ResizeMode = ResizeMode.NoResize;
			mediaPlayer.LoadedBehavior = MediaState.Manual;
			mediaPlayer.UnloadedBehavior = MediaState.Manual;
			mediaPlayer.Source = new Uri(FilePath, UriKind.Absolute);
			Home home = Enumerable.FirstOrDefault(Enumerable.OfType<Home>(System.Windows.Application.Current.Windows));
			if (home != null)
			{
				IsInSync = home.IsInSync;
				IsSyncOwner = home.IsSyncOwner;
				if (!pageJson.Equals(""))
				{
					IsInSync = true;
					IsSyncOwner = false;
				}
				if (IsInSync && !IsSyncOwner)
				{
					cbBooks.Visibility = Visibility.Collapsed;
				}
			}
			btnSync.Source = ButtonTool.GetSyncButtonImage(IsInSync, IsSyncOwner);
			btnLight.Source = ButtonTool.GetSyncButtonImage(IsInSync, IsSyncOwner);
			cbBooks.ItemsSource = cbBooksData;
			cbBooks.DisplayMemberPath = "Key";
			cbBooks.SelectedValuePath = "Value";
			cbBooks.SelectedIndex = 0;
			int num = 0;
			foreach (KeyValuePair<string, BookVM> cbBooksDatum in cbBooksData)
			{
				if (cbBooksDatum.Value.FileID.Equals(FileID))
				{
					cbBooks.SelectedIndex = num;
					break;
				}
				num++;
			}
		}

		private void InitSelectDB()
		{
			DataTable dataTable = MSCE.GetDataTable("select MeetingID,UserID,UserName,DeltaUTC from NowLogin");
			if (dataTable.Rows.Count > 0)
			{
				MeetingID = dataTable.Rows[0]["MeetingID"].ToString().Trim();
				UserID = dataTable.Rows[0]["UserID"].ToString().Trim();
				UserName = dataTable.Rows[0]["UserName"].ToString().Trim();
				long.TryParse(dataTable.Rows[0]["DeltaUTC"].ToString(), out DeltaUTC);
			}
		}

		private void MouseTimer_Tick(object sender, EventArgs e)
		{
			if (Point.Equals(lastMousePoint, MousePosition.GetCurrentMousePosition()) && !IsAlwaysShowHeaderFooter)
			{
				ChangeShowHeaderFooterDP(false);
				return;
			}
			lastMousePoint = MousePosition.GetCurrentMousePosition();
			if (!IsInSync || IsSyncOwner)
			{
				ChangeShowHeaderFooterDP(true);
			}
		}

		private void MediaPlayerTimer_Tick(object sender, EventArgs e)
		{
			if (!IsSeekBarDragging)
			{
				SeekBar.Value = mediaPlayer.Position.TotalMilliseconds;
				pb.Value = mediaPlayer.Position.TotalMilliseconds;
			}
			DateTime dateTime = new DateTime(mediaPlayer.Position.Ticks);
			txtCurrentTime.Text = dateTime.ToString("HH:mm:ss");
		}

		private void MediaPlayer_MediaOpened(object sender, RoutedEventArgs e)
		{
			if (mediaPlayer.NaturalDuration.HasTimeSpan)
			{
				TimeSpan timeSpan = mediaPlayer.NaturalDuration.TimeSpan;
				SeekBar.Maximum = timeSpan.TotalMilliseconds;
				pb.Maximum = timeSpan.TotalMilliseconds;
				moiveTotalMilliseconds = timeSpan.TotalMilliseconds;
			}
			DateTime dateTime = new DateTime((long)moiveTotalMilliseconds * 10000);
			txtEndTime.Text = dateTime.ToString("HH:mm:ss");
			MediaPlayerTimer.Start();
		}

		public void Dispose()
		{
			GC.Collect();
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/mvwindow.xaml", UriKind.Relative);
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
				mediaPlayer = (MediaElement)target;
				break;
			case 2:
				img_animation = (Image)target;
				break;
			case 3:
				HeaderDP = (DockPanel)target;
				break;
			case 4:
				btnClose = (Image)target;
				break;
			case 5:
				btnSync = (Image)target;
				break;
			case 6:
				cbBooks = (System.Windows.Controls.ComboBox)target;
				break;
			case 7:
				btnLight = (Image)target;
				break;
			case 8:
				FooterDP = (DockPanel)target;
				break;
			case 9:
				btnFunction = (Image)target;
				break;
			case 10:
				txtCurrentTime = (TextBlock)target;
				break;
			case 11:
				txtEndTime = (TextBlock)target;
				break;
			case 12:
				pb = (System.Windows.Controls.ProgressBar)target;
				break;
			case 13:
				SeekBar = (Slider)target;
				SeekBar.AddHandler(Thumb.DragStartedEvent, new DragStartedEventHandler(SeekBar_DragStarted));
				SeekBar.AddHandler(Thumb.DragCompletedEvent, new DragCompletedEventHandler(SeekBar_DragCompleted));
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}

		[CompilerGenerated]
		private void _003CMVWindow_Loaded_003Eb__0()
		{
			InitSelectDB();
			base.Dispatcher.BeginInvoke(new Action(_003CMVWindow_Loaded_003Eb__1));
		}

		[CompilerGenerated]
		private void _003CMVWindow_Loaded_003Eb__1()
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
		private static void _003CInitEvent_003Eb__2(object sender, System.Windows.Input.MouseEventArgs e)
		{
			MouseTool.ShowHand();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__3(object sender, System.Windows.Input.MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__4(object sender, System.Windows.Input.MouseEventArgs e)
		{
			MouseTool.ShowHand();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__5(object sender, System.Windows.Input.MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__6(object sender, System.Windows.Input.MouseEventArgs e)
		{
			MouseTool.ShowHand();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__7(object sender, System.Windows.Input.MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__8(object sender, System.Windows.Input.MouseEventArgs e)
		{
			MouseTool.ShowHand();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__9(object sender, System.Windows.Input.MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__a(object sender, System.Windows.Input.MouseEventArgs e)
		{
			IsAlwaysShowHeaderFooter = true;
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__b(object sender, System.Windows.Input.MouseEventArgs e)
		{
			IsAlwaysShowHeaderFooter = false;
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__c(object sender, System.Windows.Input.MouseEventArgs e)
		{
			IsAlwaysShowHeaderFooter = true;
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__d(object sender, System.Windows.Input.MouseEventArgs e)
		{
			IsAlwaysShowHeaderFooter = false;
		}
	}
}
