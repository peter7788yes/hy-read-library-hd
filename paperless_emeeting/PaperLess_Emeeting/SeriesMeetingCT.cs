using PaperLess_Emeeting.Properties;
using PaperLess_ViewModel;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace PaperLess_Emeeting
{
	public class SeriesMeetingCT : UserControl, IComponentConnector
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass8
		{
			public List<SeriesDataSeriesMeetingSeries> SeriesList;

			public List<SeriesDataSeriesMeetingMeeting> MeetingList;

			public SeriesMeetingCT _003C_003E4__this;

			public void _003CInitUI_003Eb__5()
			{
				_003C_003E4__this.Dispatcher.BeginInvoke(new Action(_003CInitUI_003Eb__6));
				_003C_003E4__this.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(_003CInitUI_003Eb__7));
			}

			public void _003CInitUI_003Eb__6()
			{
				foreach (SeriesDataSeriesMeetingSeries series in SeriesList)
				{
					SeriesMenu seriesMenu = new SeriesMenu(series, _003C_003E4__this.SeriesMeetingCT_ChangeMeetingRoomWP_Callback);
					_003C_003E4__this.SeriesMenuSP.Children.Add(seriesMenu);
					if (seriesMenu.seriesDataSeriesMeetingSeries.ID.Equals(_003C_003E4__this.NowSeriesID))
					{
						seriesMenu.btnImg.Source = new BitmapImage(new Uri("images/icon_arrow_active.png", UriKind.Relative));
						seriesMenu.Background = ColorTool.HexColorToBrush("#019fde");
						seriesMenu.txtSeriesName.Foreground = Brushes.White;
					}
				}
			}

			public void _003CInitUI_003Eb__7()
			{
				int num = 0;
				int num2 = 0;
				foreach (SeriesDataSeriesMeetingMeeting meeting in MeetingList)
				{
					num++;
					UserMeeting userMeeting = new UserMeeting();
					userMeeting.BeginTime = meeting.BeginTime;
					userMeeting.EndTime = meeting.EndTime;
					userMeeting.ID = meeting.ID;
					userMeeting.isBrowserd = meeting.isBrowserd;
					userMeeting.isDownload = meeting.isDownload;
					userMeeting.Location = meeting.Location;
					userMeeting.Name = meeting.Name;
					userMeeting.pincode = meeting.pincode;
					userMeeting.SeriesMeetingID = meeting.SeriesMeetingID;
					userMeeting.type = meeting.type;
					Home home = Enumerable.FirstOrDefault(Enumerable.OfType<Home>(Application.Current.Windows));
					if (home != null)
					{
						bool invisible = false;
						if (!_003C_003E4__this.NowSeriesID.Equals("") && !userMeeting.SeriesMeetingID.Equals(_003C_003E4__this.NowSeriesID))
						{
							invisible = true;
						}
						else
						{
							num2++;
						}
						_003C_003E4__this.MeetingRoomWP.Children.Add(new MeetingRoom(_003C_003E4__this.UserID, _003C_003E4__this.UserPWD, userMeeting, home.Home_Change2MeetingDataCT_Callback, "", invisible));
					}
				}
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClassc
		{
			public SeriesMeetingCT _003C_003E4__this;

			public string SeriesID;

			public void _003CChangeMeetingRoomWP_003Eb__a()
			{
				_003C_003E4__this.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(_003CChangeMeetingRoomWP_003Eb__b));
			}

			public void _003CChangeMeetingRoomWP_003Eb__b()
			{
				IEnumerable<MeetingRoom> enumerable = Enumerable.OfType<MeetingRoom>(_003C_003E4__this.MeetingRoomWP.Children);
				foreach (MeetingRoom item in enumerable)
				{
					if (item.userMeeting.SeriesMeetingID.Equals(SeriesID))
					{
						item.Visibility = Visibility.Visible;
					}
					else
					{
						item.Visibility = Visibility.Collapsed;
					}
				}
				MouseTool.ShowArrow();
			}
		}

		public string NowSeriesID = "";

		public SeriesMeetingCT_ChangeMeetingRoomWP_Function SeriesMeetingCT_ChangeMeetingRoomWP_Callback;

		internal TextBlock txtCount;

		internal TextBox txtKeyword;

		internal StackPanel SeriesMenuSP;

		internal ScrollViewer scrollBar2;

		internal WrapPanel MeetingRoomWP;

		private bool _contentLoaded;

		[CompilerGenerated]
		private string _003CUserID_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserPWD_003Ek__BackingField;

		[CompilerGenerated]
		private SeriesData _003CseriesData_003Ek__BackingField;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate4;

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

		public string UserPWD
		{
			[CompilerGenerated]
			get
			{
				return _003CUserPWD_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CUserPWD_003Ek__BackingField = value;
			}
		}

		public SeriesData seriesData
		{
			[CompilerGenerated]
			get
			{
				return _003CseriesData_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CseriesData_003Ek__BackingField = value;
			}
		}

		public SeriesMeetingCT(string UserID, string UserPWD, SeriesData seriesData, string NowSeriesID = "")
		{
			MouseTool.ShowLoading();
			InitializeComponent();
			this.UserID = UserID;
			this.UserPWD = UserPWD;
			this.seriesData = seriesData;
			this.NowSeriesID = NowSeriesID;
			base.Loaded += new RoutedEventHandler(SeriesMeetingCT_Loaded);
			base.Unloaded += new RoutedEventHandler(SeriesMeetingCT_Unloaded);
		}

		private void SeriesMeetingCT_Loaded(object sender, RoutedEventArgs e)
		{
			Task.Factory.StartNew(new Action(_003CSeriesMeetingCT_Loaded_003Eb__0));
		}

		private void InitEvent()
		{
			txtKeyword.MouseEnter += new MouseEventHandler(_003CInitEvent_003Eb__2);
			TextBox textBox = txtKeyword;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate4 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate4 = new MouseEventHandler(_003CInitEvent_003Eb__3);
			}
			textBox.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate4;
			txtKeyword.KeyUp += new KeyEventHandler(txtKeyword_KeyUp);
			txtKeyword.Focus();
		}

		private void txtKeyword_KeyUp(object sender, KeyEventArgs e)
		{
			string text = txtKeyword.Text.ToLower().Trim();
			if (!text.Equals(""))
			{
				foreach (MeetingRoom item in Enumerable.OfType<MeetingRoom>(MeetingRoomWP.Children))
				{
					if (!NowSeriesID.Equals("") && !item.userMeeting.SeriesMeetingID.Equals(NowSeriesID))
					{
						item.Visibility = Visibility.Collapsed;
					}
					else if (item.userMeeting.Name.Contains(text) || item.userMeeting.Location.Contains(text))
					{
						item.Visibility = Visibility.Visible;
					}
					else
					{
						item.Visibility = Visibility.Collapsed;
					}
				}
			}
			else
			{
				foreach (MeetingRoom item2 in Enumerable.OfType<MeetingRoom>(MeetingRoomWP.Children))
				{
					if (!NowSeriesID.Equals("") && !item2.userMeeting.SeriesMeetingID.Equals(NowSeriesID))
					{
						item2.Visibility = Visibility.Collapsed;
					}
					else
					{
						item2.Visibility = Visibility.Visible;
					}
				}
			}
		}

		private void InitUI()
		{
			_003C_003Ec__DisplayClass8 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass8();
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			SeriesMeetingCT_ChangeMeetingRoomWP_Callback = new SeriesMeetingCT_ChangeMeetingRoomWP_Function(ChangeMeetingRoomWP);
			_003C_003Ec__DisplayClass.SeriesList = new List<SeriesDataSeriesMeetingSeries>();
			_003C_003Ec__DisplayClass.MeetingList = new List<SeriesDataSeriesMeetingMeeting>();
			SeriesDataSeriesMeeting[] seriesMeeting = seriesData.SeriesMeeting;
			foreach (SeriesDataSeriesMeeting seriesDataSeriesMeeting in seriesMeeting)
			{
				_003C_003Ec__DisplayClass.SeriesList.Add(seriesDataSeriesMeeting.Series);
				_003C_003Ec__DisplayClass.MeetingList.AddRange(Enumerable.ToList(seriesDataSeriesMeeting.MeetingList));
			}
			string courseOrMeeting_String = Settings.Default.CourseOrMeeting_String;
			txtCount.Text = string.Format("共 {0} 組系列{1}", _003C_003Ec__DisplayClass.SeriesList.Count, courseOrMeeting_String);
			Task.Factory.StartNew(new Action(_003C_003Ec__DisplayClass._003CInitUI_003Eb__5));
		}

		private void ChangeMeetingRoomWP(string SeriesID)
		{
			_003C_003Ec__DisplayClassc _003C_003Ec__DisplayClassc = new _003C_003Ec__DisplayClassc();
			_003C_003Ec__DisplayClassc.SeriesID = SeriesID;
			_003C_003Ec__DisplayClassc._003C_003E4__this = this;
			NowSeriesID = _003C_003Ec__DisplayClassc.SeriesID;
			IEnumerable<SeriesMenu> enumerable = Enumerable.OfType<SeriesMenu>(SeriesMenuSP.Children);
			foreach (SeriesMenu item in enumerable)
			{
				if (!item.seriesDataSeriesMeetingSeries.ID.Equals(_003C_003Ec__DisplayClassc.SeriesID))
				{
					item.btnImg.Source = new BitmapImage(new Uri("images/icon_arrow.png", UriKind.Relative));
					item.Background = Brushes.Transparent;
					item.txtSeriesName.Foreground = Brushes.Black;
				}
			}
			Task.Factory.StartNew(new Action(_003C_003Ec__DisplayClassc._003CChangeMeetingRoomWP_003Eb__a));
		}

		private void InitSelectDB()
		{
		}

		private void SeriesMeetingCT_Unloaded(object sender, RoutedEventArgs e)
		{
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/seriesmeetingct.xaml", UriKind.Relative);
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
				txtCount = (TextBlock)target;
				break;
			case 2:
				txtKeyword = (TextBox)target;
				break;
			case 3:
				SeriesMenuSP = (StackPanel)target;
				break;
			case 4:
				scrollBar2 = (ScrollViewer)target;
				break;
			case 5:
				MeetingRoomWP = (WrapPanel)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}

		[CompilerGenerated]
		private void _003CSeriesMeetingCT_Loaded_003Eb__0()
		{
			InitSelectDB();
			base.Dispatcher.BeginInvoke(new Action(_003CSeriesMeetingCT_Loaded_003Eb__1));
		}

		[CompilerGenerated]
		private void _003CSeriesMeetingCT_Loaded_003Eb__1()
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
		private void _003CInitEvent_003Eb__2(object sender, MouseEventArgs e)
		{
			MouseTool.ShowIBeam();
			txtKeyword.Focus();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__3(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}
	}
}
