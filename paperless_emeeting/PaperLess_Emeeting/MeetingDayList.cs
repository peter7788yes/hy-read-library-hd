using PaperLess_ViewModel;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;

namespace PaperLess_Emeeting
{
	public class MeetingDayList : UserControl, IComponentConnector
	{
		public string NewAddMeetingID = "";

		internal StackPanel DateSP;

		internal TextBlock txtMonth;

		internal TextBlock txtDay;

		internal TextBlock txtWeek;

		internal Grid RoomGrid;

		internal WrapPanel MeetingRoomWP;

		private bool _contentLoaded;

		[CompilerGenerated]
		private string _003CUserID_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserPWD_003Ek__BackingField;

		[CompilerGenerated]
		private DateTime _003Cdate_003Ek__BackingField;

		[CompilerGenerated]
		private List<UserMeeting> _003CmeetingList_003Ek__BackingField;

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

		public DateTime date
		{
			[CompilerGenerated]
			get
			{
				return _003Cdate_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003Cdate_003Ek__BackingField = value;
			}
		}

		public List<UserMeeting> meetingList
		{
			[CompilerGenerated]
			get
			{
				return _003CmeetingList_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CmeetingList_003Ek__BackingField = value;
			}
		}

		public event Home_Change2MeetingDataCT_Function Home_Change2MeetingDataCT_Event;

		public event MeetingList_Show_HiddenMeetingDayList_Function MeetingList_Show_HiddenMeetingEvent;

		public MeetingDayList(string UserID, string UserPWD, DateTime date, List<UserMeeting> meetingList, Home_Change2MeetingDataCT_Function callback, MeetingList_Show_HiddenMeetingDayList_Function callback2, string NewAddMeetingID)
		{
			InitializeComponent();
			this.UserID = UserID;
			this.UserPWD = UserPWD;
			this.date = date;
			this.meetingList = meetingList;
			Home_Change2MeetingDataCT_Event += callback;
			MeetingList_Show_HiddenMeetingEvent += callback2;
			this.NewAddMeetingID = NewAddMeetingID;
			base.Loaded += new RoutedEventHandler(MeetingDayList_Loaded);
		}

		private void MeetingDayList_Loaded(object sender, RoutedEventArgs e)
		{
			Task.Factory.StartNew(new Action(_003CMeetingDayList_Loaded_003Eb__0));
		}

		private void InitSelectDB()
		{
		}

		private void InitEvent()
		{
			base.MouseWheel += new MouseWheelEventHandler(_003CInitEvent_003Eb__2);
		}

		private void InitUI()
		{
			txtMonth.Text = date.Month + "月";
			txtDay.Text = date.Day.ToString();
			txtWeek.Text = DateTool.DayOfWeek(date);
			if (date.Date == DateTime.Today)
			{
				DateSP.Background = ColorTool.HexColorToBrush("#c5f3ff");
				RoomGrid.Background = ColorTool.HexColorToBrush("#53a9ba");
			}
			Task.Factory.StartNew(new Action(_003CInitUI_003Eb__3));
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/meetingdaylist.xaml", UriKind.Relative);
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
				DateSP = (StackPanel)target;
				break;
			case 2:
				txtMonth = (TextBlock)target;
				break;
			case 3:
				txtDay = (TextBlock)target;
				break;
			case 4:
				txtWeek = (TextBlock)target;
				break;
			case 5:
				RoomGrid = (Grid)target;
				break;
			case 6:
				MeetingRoomWP = (WrapPanel)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}

		[CompilerGenerated]
		private void _003CMeetingDayList_Loaded_003Eb__0()
		{
			InitSelectDB();
			base.Dispatcher.BeginInvoke(new Action(_003CMeetingDayList_Loaded_003Eb__1));
		}

		[CompilerGenerated]
		private void _003CMeetingDayList_Loaded_003Eb__1()
		{
			InitUI();
			InitEvent();
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__2(object sender, MouseWheelEventArgs e)
		{
			if (e.Delta > 0 && this.MeetingList_Show_HiddenMeetingEvent != null)
			{
				this.MeetingList_Show_HiddenMeetingEvent();
			}
		}

		[CompilerGenerated]
		private void _003CInitUI_003Eb__3()
		{
			base.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(_003CInitUI_003Eb__4));
		}

		[CompilerGenerated]
		private void _003CInitUI_003Eb__4()
		{
			foreach (UserMeeting meeting in meetingList)
			{
				MeetingRoomWP.Children.Add(new MeetingRoom(UserID, UserPWD, meeting, this.Home_Change2MeetingDataCT_Event, NewAddMeetingID));
			}
		}
	}
}
