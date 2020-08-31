using Network;
using Newtonsoft.Json;
using PaperLess_Emeeting.App_Code.MessageBox;
using PaperLess_Emeeting.Properties;
using PaperLess_ViewModel;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace PaperLess_Emeeting
{
	public class MeetingListCT : UserControl, IComponentConnector
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass31
		{
			private sealed class _003C_003Ec__DisplayClass3b
			{
				public _003C_003Ec__DisplayClass31 CS_0024_003C_003E8__locals32;

				public Dictionary<DateTime, List<UserMeeting>> dict;

				private static Func<KeyValuePair<DateTime, List<UserMeeting>>, bool> CS_0024_003C_003E9__CachedAnonymousMethodDelegate3d;

				private static Predicate<KeyValuePair<DateTime, List<UserMeeting>>> CS_0024_003C_003E9__CachedAnonymousMethodDelegate3e;

				private static Predicate<KeyValuePair<DateTime, List<UserMeeting>>> CS_0024_003C_003E9__CachedAnonymousMethodDelegate3f;

				private static Func<KeyValuePair<DateTime, List<UserMeeting>>, int> CS_0024_003C_003E9__CachedAnonymousMethodDelegate40;

				public void _003CInitUI_003Eb__2a()
				{
					RoutedEventHandler routedEventHandler = null;
					MouseEventHandler mouseEventHandler = null;
					try
					{
						CS_0024_003C_003E8__locals32._003C_003E4__this.MeetingDaySP.Children.Clear();
						if (!Settings.Default.UserMeeting_Reverse)
						{
							foreach (KeyValuePair<DateTime, List<UserMeeting>> item in dict)
							{
								MeetingDayList element = new MeetingDayList(CS_0024_003C_003E8__locals32._003C_003E4__this.UserID, CS_0024_003C_003E8__locals32._003C_003E4__this.UserPWD, item.Key, item.Value, CS_0024_003C_003E8__locals32._003C_003E4__this.Home_Change2MeetingDataCT_Event, new MeetingList_Show_HiddenMeetingDayList_Function(CS_0024_003C_003E8__locals32._003C_003E4__this.Show_HiddenMeetingDayList), CS_0024_003C_003E8__locals32._003C_003E4__this.NewAddMeetingID);
								CS_0024_003C_003E8__locals32._003C_003E4__this.MeetingDaySP.Children.Add(element);
							}
						}
						else
						{
							Dictionary<DateTime, List<UserMeeting>> source = dict;
							if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate3d == null)
							{
								CS_0024_003C_003E9__CachedAnonymousMethodDelegate3d = new Func<KeyValuePair<DateTime, List<UserMeeting>>, bool>(_003CInitUI_003Eb__2b);
							}
							bool flag = Enumerable.Any(source, CS_0024_003C_003E9__CachedAnonymousMethodDelegate3d);
							int num = 0;
							if (CS_0024_003C_003E8__locals32._003C_003E4__this.MeetingListDate.Month == DateTime.Now.Month)
							{
								if (flag)
								{
									List<KeyValuePair<DateTime, List<UserMeeting>>> list = Enumerable.ToList(dict);
									if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate3e == null)
									{
										CS_0024_003C_003E9__CachedAnonymousMethodDelegate3e = new Predicate<KeyValuePair<DateTime, List<UserMeeting>>>(_003CInitUI_003Eb__2c);
									}
									num = list.FindIndex(CS_0024_003C_003E9__CachedAnonymousMethodDelegate3e);
								}
								else
								{
									Dictionary<DateTime, List<UserMeeting>> dictionary = new Dictionary<DateTime, List<UserMeeting>>(dict);
									dictionary.Add(DateTime.Now, new List<UserMeeting>());
									if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate40 == null)
									{
										CS_0024_003C_003E9__CachedAnonymousMethodDelegate40 = new Func<KeyValuePair<DateTime, List<UserMeeting>>, int>(_003CInitUI_003Eb__2e);
									}
									List<KeyValuePair<DateTime, List<UserMeeting>>> list2 = Enumerable.ToList(Enumerable.OrderBy(dictionary, CS_0024_003C_003E9__CachedAnonymousMethodDelegate40));
									if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate3f == null)
									{
										CS_0024_003C_003E9__CachedAnonymousMethodDelegate3f = new Predicate<KeyValuePair<DateTime, List<UserMeeting>>>(_003CInitUI_003Eb__2d);
									}
									num = list2.FindIndex(CS_0024_003C_003E9__CachedAnonymousMethodDelegate3f);
								}
							}
							MeetingDayList meetingDayList = null;
							int num2 = 0;
							foreach (KeyValuePair<DateTime, List<UserMeeting>> item2 in dict)
							{
								MeetingDayList meetingDayList2 = new MeetingDayList(CS_0024_003C_003E8__locals32._003C_003E4__this.UserID, CS_0024_003C_003E8__locals32._003C_003E4__this.UserPWD, item2.Key, item2.Value, CS_0024_003C_003E8__locals32._003C_003E4__this.Home_Change2MeetingDataCT_Event, new MeetingList_Show_HiddenMeetingDayList_Function(CS_0024_003C_003E8__locals32._003C_003E4__this.Show_HiddenMeetingDayList), CS_0024_003C_003E8__locals32._003C_003E4__this.NewAddMeetingID);
								CS_0024_003C_003E8__locals32._003C_003E4__this.MeetingDaySP.Children.Add(meetingDayList2);
								if (num2 == num || num == dict.Count - 1 || int.Parse(meetingDayList2.date.ToString("yyyyMMdd")) <= int.Parse(DateTime.Now.ToString("yyyyMMdd")))
								{
									meetingDayList = meetingDayList2;
								}
								else if (meetingDayList == null)
								{
									meetingDayList2.Visibility = Visibility.Collapsed;
									if (CS_0024_003C_003E8__locals32._003C_003E4__this.btnShowMeetingRooms == null)
									{
										MeetingListCT _003C_003E4__this = CS_0024_003C_003E8__locals32._003C_003E4__this;
										Button button = new Button();
										button.Content = "^";
										button.Height = 0.0;
										button.BorderThickness = new Thickness(0.0);
										button.Background = new SolidColorBrush(Colors.Yellow);
										_003C_003E4__this.btnShowMeetingRooms = button;
										CS_0024_003C_003E8__locals32._003C_003E4__this.btnShowMeetingRooms.Margin = new Thickness(12.0, 0.0, 12.0, 0.0);
										Button btnShowMeetingRooms = CS_0024_003C_003E8__locals32._003C_003E4__this.btnShowMeetingRooms;
										if (routedEventHandler == null)
										{
											routedEventHandler = new RoutedEventHandler(_003CInitUI_003Eb__2f);
										}
										btnShowMeetingRooms.Click += routedEventHandler;
										Button btnShowMeetingRooms2 = CS_0024_003C_003E8__locals32._003C_003E4__this.btnShowMeetingRooms;
										if (mouseEventHandler == null)
										{
											mouseEventHandler = new MouseEventHandler(_003CInitUI_003Eb__30);
										}
										btnShowMeetingRooms2.MouseEnter += mouseEventHandler;
										CS_0024_003C_003E8__locals32._003C_003E4__this.MeetingDaySP.Children.Add(CS_0024_003C_003E8__locals32._003C_003E4__this.btnShowMeetingRooms);
									}
								}
								num2++;
							}
							Rectangle rectangle = new Rectangle();
							rectangle.Height = 1080.0;
							rectangle.Margin = new Thickness(12.0, 0.0, 12.0, 0.0);
							rectangle.Fill = new SolidColorBrush(Colors.Transparent);
							CS_0024_003C_003E8__locals32._003C_003E4__this.MeetingDaySP.Children.Add(rectangle);
						}
					}
					catch (Exception ex)
					{
						LogTool.Debug(ex);
					}
				}

				private static bool _003CInitUI_003Eb__2b(KeyValuePair<DateTime, List<UserMeeting>> x)
				{
					return x.Key.Day == DateTime.Now.Day;
				}

				private static bool _003CInitUI_003Eb__2c(KeyValuePair<DateTime, List<UserMeeting>> x)
				{
					return x.Key.Day == DateTime.Now.Day;
				}

				private static bool _003CInitUI_003Eb__2d(KeyValuePair<DateTime, List<UserMeeting>> x)
				{
					return x.Key.Day == DateTime.Now.Day;
				}

				private static int _003CInitUI_003Eb__2e(KeyValuePair<DateTime, List<UserMeeting>> x)
				{
					return x.Key.Day;
				}

				private void _003CInitUI_003Eb__2f(object sender, RoutedEventArgs e)
				{
					CS_0024_003C_003E8__locals32._003C_003E4__this.btnShowMeetingRooms.Visibility = Visibility.Collapsed;
					CS_0024_003C_003E8__locals32._003C_003E4__this.Show_HiddenMeetingDayList();
				}

				private void _003CInitUI_003Eb__30(object sender, MouseEventArgs e)
				{
					CS_0024_003C_003E8__locals32._003C_003E4__this.btnShowMeetingRooms.Visibility = Visibility.Collapsed;
					CS_0024_003C_003E8__locals32._003C_003E4__this.Show_HiddenMeetingDayList();
				}
			}

			public MeetingListCT _003C_003E4__this;

			public UserMeeting[] userMeetingAry;

			private static Func<UserMeeting, DateTime> CS_0024_003C_003E9__CachedAnonymousMethodDelegate33;

			private static Func<UserMeeting, DateTime> CS_0024_003C_003E9__CachedAnonymousMethodDelegate34;

			private static Func<IGrouping<DateTime, UserMeeting>, DateTime> CS_0024_003C_003E9__CachedAnonymousMethodDelegate35;

			private static Func<IGrouping<DateTime, UserMeeting>, List<UserMeeting>> CS_0024_003C_003E9__CachedAnonymousMethodDelegate36;

			private static Func<UserMeeting, DateTime> CS_0024_003C_003E9__CachedAnonymousMethodDelegate37;

			private static Func<UserMeeting, DateTime> CS_0024_003C_003E9__CachedAnonymousMethodDelegate38;

			private static Func<IGrouping<DateTime, UserMeeting>, DateTime> CS_0024_003C_003E9__CachedAnonymousMethodDelegate39;

			private static Func<IGrouping<DateTime, UserMeeting>, List<UserMeeting>> CS_0024_003C_003E9__CachedAnonymousMethodDelegate3a;

			public void _003CInitUI_003Eb__21()
			{
				_003C_003Ec__DisplayClass3b _003C_003Ec__DisplayClass3b = new _003C_003Ec__DisplayClass3b();
				_003C_003Ec__DisplayClass3b.CS_0024_003C_003E8__locals32 = this;
				_003C_003Ec__DisplayClass3b.dict = new Dictionary<DateTime, List<UserMeeting>>();
				if (Settings.Default.UserMeeting_Reverse)
				{
					UserMeeting[] source = userMeetingAry;
					if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate33 == null)
					{
						CS_0024_003C_003E9__CachedAnonymousMethodDelegate33 = new Func<UserMeeting, DateTime>(_003CInitUI_003Eb__22);
					}
					IOrderedEnumerable<UserMeeting> source2 = Enumerable.OrderBy(source, CS_0024_003C_003E9__CachedAnonymousMethodDelegate33);
					if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate34 == null)
					{
						CS_0024_003C_003E9__CachedAnonymousMethodDelegate34 = new Func<UserMeeting, DateTime>(_003CInitUI_003Eb__23);
					}
					IEnumerable<IGrouping<DateTime, UserMeeting>> source3 = Enumerable.Reverse(Enumerable.GroupBy(source2, CS_0024_003C_003E9__CachedAnonymousMethodDelegate34));
					if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate35 == null)
					{
						CS_0024_003C_003E9__CachedAnonymousMethodDelegate35 = new Func<IGrouping<DateTime, UserMeeting>, DateTime>(_003CInitUI_003Eb__24);
					}
					Func<IGrouping<DateTime, UserMeeting>, DateTime> cS_0024_003C_003E9__CachedAnonymousMethodDelegate = CS_0024_003C_003E9__CachedAnonymousMethodDelegate35;
					if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate36 == null)
					{
						CS_0024_003C_003E9__CachedAnonymousMethodDelegate36 = new Func<IGrouping<DateTime, UserMeeting>, List<UserMeeting>>(_003CInitUI_003Eb__25);
					}
					_003C_003Ec__DisplayClass3b.dict = Enumerable.ToDictionary(source3, cS_0024_003C_003E9__CachedAnonymousMethodDelegate, CS_0024_003C_003E9__CachedAnonymousMethodDelegate36);
				}
				else
				{
					UserMeeting[] source4 = userMeetingAry;
					if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate37 == null)
					{
						CS_0024_003C_003E9__CachedAnonymousMethodDelegate37 = new Func<UserMeeting, DateTime>(_003CInitUI_003Eb__26);
					}
					IOrderedEnumerable<UserMeeting> source5 = Enumerable.OrderBy(source4, CS_0024_003C_003E9__CachedAnonymousMethodDelegate37);
					if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate38 == null)
					{
						CS_0024_003C_003E9__CachedAnonymousMethodDelegate38 = new Func<UserMeeting, DateTime>(_003CInitUI_003Eb__27);
					}
					IEnumerable<IGrouping<DateTime, UserMeeting>> source6 = Enumerable.GroupBy(source5, CS_0024_003C_003E9__CachedAnonymousMethodDelegate38);
					if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate39 == null)
					{
						CS_0024_003C_003E9__CachedAnonymousMethodDelegate39 = new Func<IGrouping<DateTime, UserMeeting>, DateTime>(_003CInitUI_003Eb__28);
					}
					Func<IGrouping<DateTime, UserMeeting>, DateTime> cS_0024_003C_003E9__CachedAnonymousMethodDelegate2 = CS_0024_003C_003E9__CachedAnonymousMethodDelegate39;
					if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate3a == null)
					{
						CS_0024_003C_003E9__CachedAnonymousMethodDelegate3a = new Func<IGrouping<DateTime, UserMeeting>, List<UserMeeting>>(_003CInitUI_003Eb__29);
					}
					_003C_003Ec__DisplayClass3b.dict = Enumerable.ToDictionary(source6, cS_0024_003C_003E9__CachedAnonymousMethodDelegate2, CS_0024_003C_003E9__CachedAnonymousMethodDelegate3a);
				}
				_003C_003E4__this.Dispatcher.BeginInvoke(new Action(_003C_003Ec__DisplayClass3b._003CInitUI_003Eb__2a));
			}

			private static DateTime _003CInitUI_003Eb__22(UserMeeting item)
			{
				return DateTool.StringToDate(item.BeginTime);
			}

			private static DateTime _003CInitUI_003Eb__23(UserMeeting item)
			{
				return DateTool.StringToDate(item.BeginTime).Date;
			}

			private static DateTime _003CInitUI_003Eb__24(IGrouping<DateTime, UserMeeting> IGrouping)
			{
				return IGrouping.Key;
			}

			private static List<UserMeeting> _003CInitUI_003Eb__25(IGrouping<DateTime, UserMeeting> IGrouping)
			{
				return Enumerable.ToList(IGrouping);
			}

			private static DateTime _003CInitUI_003Eb__26(UserMeeting item)
			{
				return DateTool.StringToDate(item.BeginTime);
			}

			private static DateTime _003CInitUI_003Eb__27(UserMeeting item)
			{
				return DateTool.StringToDate(item.BeginTime).Date;
			}

			private static DateTime _003CInitUI_003Eb__28(IGrouping<DateTime, UserMeeting> IGrouping)
			{
				return IGrouping.Key;
			}

			private static List<UserMeeting> _003CInitUI_003Eb__29(IGrouping<DateTime, UserMeeting> IGrouping)
			{
				return Enumerable.ToList(IGrouping);
			}
		}

		public string NewAddMeetingID = "";

		public Dictionary<DateTime, User> PreLoadLastNextMonthDict = new Dictionary<DateTime, User>();

		public int CacheMinuteTTL;

		public Thread CacheThread;

		private Button btnShowMeetingRooms;

		internal Grid xGrid;

		internal Image btnLast;

		internal TextBlock txtDate;

		internal Image btnNext;

		internal TextBlock txtCount;

		internal TextBlock txtPinCodeHint;

		internal TextBox txtPinCode;

		internal Border btnAdd;

		internal TextBlock btnAddHint;

		internal ScrollViewer SV;

		internal TextBlock txtNothing;

		internal StackPanel MeetingDaySP;

		private bool _contentLoaded;

		[CompilerGenerated]
		private string _003CUserID_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserName_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserPWD_003Ek__BackingField;

		[CompilerGenerated]
		private DateTime _003CMeetingListDate_003Ek__BackingField;

		[CompilerGenerated]
		private UserMeeting[] _003CUserMeetingAry_003Ek__BackingField;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate12;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate13;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate14;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate15;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate16;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate17;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate18;

		[CompilerGenerated]
		private static Func<UserMeeting, DateTime> CS_0024_003C_003E9__CachedAnonymousMethodDelegate4b;

		[CompilerGenerated]
		private static Func<UserMeeting, DateTime> CS_0024_003C_003E9__CachedAnonymousMethodDelegate4c;

		[CompilerGenerated]
		private static Func<IGrouping<DateTime, UserMeeting>, DateTime> CS_0024_003C_003E9__CachedAnonymousMethodDelegate4d;

		[CompilerGenerated]
		private static Func<IGrouping<DateTime, UserMeeting>, List<UserMeeting>> CS_0024_003C_003E9__CachedAnonymousMethodDelegate4e;

		[CompilerGenerated]
		private static Func<UserMeeting, DateTime> CS_0024_003C_003E9__CachedAnonymousMethodDelegate4f;

		[CompilerGenerated]
		private static Func<UserMeeting, DateTime> CS_0024_003C_003E9__CachedAnonymousMethodDelegate50;

		[CompilerGenerated]
		private static Func<IGrouping<DateTime, UserMeeting>, DateTime> CS_0024_003C_003E9__CachedAnonymousMethodDelegate51;

		[CompilerGenerated]
		private static Func<IGrouping<DateTime, UserMeeting>, List<UserMeeting>> CS_0024_003C_003E9__CachedAnonymousMethodDelegate52;

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

		public DateTime MeetingListDate
		{
			[CompilerGenerated]
			get
			{
				return _003CMeetingListDate_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CMeetingListDate_003Ek__BackingField = value;
			}
		}

		public UserMeeting[] UserMeetingAry
		{
			[CompilerGenerated]
			get
			{
				return _003CUserMeetingAry_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CUserMeetingAry_003Ek__BackingField = value;
			}
		}

		public event Home_Change2MeetingDataCT_Function Home_Change2MeetingDataCT_Event;

		public MeetingListCT(UserMeeting[] UserMeetingAry, DateTime MeetingListDate, Home_Change2MeetingDataCT_Function callback)
		{
			InitializeComponent();
			this.UserMeetingAry = UserMeetingAry;
			this.MeetingListDate = MeetingListDate;
			Home_Change2MeetingDataCT_Event += callback;
			base.Loaded += new RoutedEventHandler(MeetingListCT_Loaded);
			CacheMinuteTTL = Settings.Default.CacheMinuteTTL;
		}

		private void MeetingListCT_Loaded(object sender, RoutedEventArgs e)
		{
			Task.Factory.StartNew(new Action(_003CMeetingListCT_Loaded_003Eb__0));
		}

		private void PreLoadLastNextMonth()
		{
			Action action = null;
			if (CacheMinuteTTL >= 0)
			{
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003CPreLoadLastNextMonth_003Eb__2);
				}
				factory.StartNew(action);
			}
		}

		private void InitSelectDB()
		{
			DataTable dataTable = MSCE.GetDataTable("select UserID,UserName,UserPWD,MeetingListDate from NowLogin");
			if (dataTable.Rows.Count > 0)
			{
				UserID = dataTable.Rows[0]["UserID"].ToString().Trim();
				UserName = dataTable.Rows[0]["UserName"].ToString().Trim();
				UserPWD = dataTable.Rows[0]["UserPWD"].ToString().Trim();
			}
		}

		private void InitEvent()
		{
			Border border = btnAdd;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate12 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate12 = new MouseEventHandler(_003CInitEvent_003Eb__9);
			}
			border.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate12;
			Border border2 = btnAdd;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate13 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate13 = new MouseEventHandler(_003CInitEvent_003Eb__a);
			}
			border2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate13;
			btnAdd.MouseLeftButtonDown += new MouseButtonEventHandler(btnAdd_MouseLeftButtonDown);
			Image image = btnLast;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate14 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate14 = new MouseEventHandler(_003CInitEvent_003Eb__b);
			}
			image.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate14;
			Image image2 = btnLast;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate15 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate15 = new MouseEventHandler(_003CInitEvent_003Eb__c);
			}
			image2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate15;
			btnLast.MouseLeftButtonDown += new MouseButtonEventHandler(btnLastNext_MouseLeftButtonDown);
			Image image3 = btnNext;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate16 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate16 = new MouseEventHandler(_003CInitEvent_003Eb__d);
			}
			image3.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate16;
			Image image4 = btnNext;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate17 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate17 = new MouseEventHandler(_003CInitEvent_003Eb__e);
			}
			image4.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate17;
			btnNext.MouseLeftButtonDown += new MouseButtonEventHandler(btnLastNext_MouseLeftButtonDown);
			txtPinCode.MouseEnter += new MouseEventHandler(_003CInitEvent_003Eb__f);
			TextBox textBox = txtPinCode;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate18 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate18 = new MouseEventHandler(_003CInitEvent_003Eb__10);
			}
			textBox.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate18;
			txtPinCode.KeyDown += new KeyEventHandler(txtPinCode_KeyDown);
			txtPinCode.Focus();
			SV.ScrollToVerticalOffset(1.0);
			SV.ScrollChanged += new ScrollChangedEventHandler(_003CInitEvent_003Eb__11);
		}

		private void Show_HiddenMeetingDayList()
		{
			if (SV.VerticalOffset == 0.0 || (btnShowMeetingRooms != null && btnShowMeetingRooms.Visibility == Visibility.Collapsed))
			{
				IEnumerable<MeetingDayList> enumerable = Enumerable.OfType<MeetingDayList>(MeetingDaySP.Children);
				if (enumerable != null)
				{
					IEnumerable<Button> enumerable2 = Enumerable.OfType<Button>(MeetingDaySP.Children);
					foreach (Button item in enumerable2)
					{
						item.Visibility = Visibility.Collapsed;
					}
					foreach (MeetingDayList item2 in enumerable)
					{
						item2.Visibility = Visibility.Visible;
					}
				}
			}
		}

		private void txtPinCode_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return && txtPinCode.Visibility == Visibility.Visible && !txtPinCode.Text.Equals(""))
			{
				CallAddNewMeeting();
			}
		}

		private void btnAdd_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			CallAddNewMeeting();
		}

		private void CallAddNewMeeting()
		{
			MouseTool.ShowLoading();
			string text = "";
			if (Settings.Default.IsNewMeeting_PopupDialog)
			{
				ConfirmWindow confirmWindow = new ConfirmWindow();
				bool? flag = confirmWindow.ShowDialog();
				if (!flag.GetValueOrDefault() || !flag.HasValue)
				{
					return;
				}
				text = confirmWindow.tbPinCode.Text.Trim();
			}
			else
			{
				text = txtPinCode.Text.Trim();
			}
			if (text.Equals(""))
			{
				AutoClosingMessageBox.Show(string.Format("請先輸入{0}識別碼", Settings.Default.CourseOrMeeting_String));
			}
			else
			{
				GetNewMeeting.AsyncPOST(UserID, text, new Action<string>(_003CCallAddNewMeeting_003Eb__19));
			}
		}

		private void GetNewMeeting_DoAction(string dataString)
		{
			Action<User, DateTime> action = null;
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<string>(GetNewMeeting_DoAction), dataString);
				return;
			}
			try
			{
				string courseOrMeeting_String = Settings.Default.CourseOrMeeting_String;
				XDocument xDocument = null;
				string text = "";
				try
				{
					xDocument = XDocument.Parse(dataString);
					text = xDocument.Element("User").Attribute("State").Value.Trim();
				}
				catch (Exception ex)
				{
					LogTool.Debug(ex);
				}
				switch (text)
				{
				case "0":
				{
					string newAddMeetingID = xDocument.Element("User").Element("MeetingData").Attribute("ID")
						.Value.Trim();
					string s = xDocument.Element("User").Element("MeetingData").Attribute("BeginTime")
						.Value.Trim();
					DateTime result = DateTime.Now;
					if (!DateTime.TryParse(s, out result))
					{
						result = DateTime.Now;
					}
					NewAddMeetingID = newAddMeetingID;
					MouseTool.ShowLoading();
					if (NetworkTool.CheckNetwork() > 0)
					{
						string userID = UserID;
						string userPWD = UserPWD;
						DateTime date = result;
						if (action == null)
						{
							action = new Action<User, DateTime>(_003CGetNewMeeting_DoAction_003Eb__1a);
						}
						GetUserData.AsyncPOST(userID, userPWD, date, action);
					}
					else
					{
						DataTable dataTable = MSCE.GetDataTable("select UserJson from UserData where UserID =@1 and ListDate=@2", UserID, DateTool.MonthFirstDate(MeetingListDate).ToString("yyyyMMdd"));
						User user = new User();
						if (dataTable.Rows.Count > 0)
						{
							user = JsonConvert.DeserializeObject<User>(dataTable.Rows[0]["UserJson"].ToString());
						}
						else
						{
							dataTable = MSCE.GetDataTable("select top 1 UserJson from UserData where UserID =@1", UserID);
							if (dataTable.Rows.Count > 0)
							{
								user = JsonConvert.DeserializeObject<User>(dataTable.Rows[0]["UserJson"].ToString());
							}
							user.MeetingList = new UserMeeting[0];
						}
						GetUserData_DoAction(user, MeetingListDate);
					}
					AutoClosingMessageBox.Show(string.Format("成功加入{0}", courseOrMeeting_String));
					break;
				}
				case "1":
					AutoClosingMessageBox.Show(string.Format("本{0}未邀請貴機關單位參與", courseOrMeeting_String));
					break;
				case "2":
					AutoClosingMessageBox.Show("已加入過");
					break;
				case "3":
					AutoClosingMessageBox.Show(string.Format("{0}不存在", courseOrMeeting_String));
					break;
				case "4":
					AutoClosingMessageBox.Show(string.Format("{0}尚未發佈", courseOrMeeting_String));
					break;
				case "5":
					AutoClosingMessageBox.Show("無此使用者");
					break;
				case "6":
					AutoClosingMessageBox.Show("加入失敗");
					break;
				case "7":
					AutoClosingMessageBox.Show("機密會議");
					break;
				case "8":
					AutoClosingMessageBox.Show("會議已取消");
					break;
				default:
					AutoClosingMessageBox.Show("新增錯誤，請聯絡系統管理人員");
					break;
				}
			}
			catch (Exception ex2)
			{
				AutoClosingMessageBox.Show("新增錯誤，請聯絡系統管理人員");
				LogTool.Debug(ex2);
			}
			txtPinCode.Text = "";
			MouseTool.ShowArrow();
		}

		private void btnLastNext_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Action action = null;
			SV.ScrollToVerticalOffset(1.0);
			Image image = sender as Image;
			if (image.Name.Equals("btnLast"))
			{
				MeetingListDate = MeetingListDate.AddMonths(-1);
			}
			else
			{
				MeetingListDate = MeetingListDate.AddMonths(1);
			}
			MouseTool.ShowLoading();
			new HttpRequest();
			if (NetworkTool.CheckNetwork() > 0)
			{
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003CbtnLastNext_MouseLeftButtonDown_003Eb__1c);
				}
				factory.StartNew(action);
			}
			else
			{
				DataTable dataTable = MSCE.GetDataTable("select UserJson from UserData where UserID =@1 and ListDate=@2", UserID, DateTool.MonthFirstDate(MeetingListDate).ToString("yyyyMMdd"));
				User user = new User();
				if (dataTable.Rows.Count > 0)
				{
					user = JsonConvert.DeserializeObject<User>(dataTable.Rows[0]["UserJson"].ToString());
				}
				else
				{
					dataTable = MSCE.GetDataTable("select top 1 UserJson from UserData where UserID =@1", UserID);
					if (dataTable.Rows.Count > 0)
					{
						user = JsonConvert.DeserializeObject<User>(dataTable.Rows[0]["UserJson"].ToString());
					}
					user.MeetingList = new UserMeeting[0];
				}
				GetUserData_DoAction(user, MeetingListDate);
			}
			string text = "update NowLogin Set MeetingListDate=@1";
			int num = MSCE.ExecuteNonQuery(text, MeetingListDate.ToString("yyyy/MM/dd"));
			if (num < 1)
			{
				LogTool.Debug(new Exception("DB失敗: " + text));
			}
		}

		private void GetUserData_DoAction(User user, DateTime date)
		{
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<User, DateTime>(GetUserData_DoAction), user, date);
				return;
			}
			if (user != null)
			{
				InitUI(user.MeetingList, date);
				DataTable dataTable = MSCE.GetDataTable("select ListDate from UserData where UserID =@1 and ListDate =@2", user.ID, DateTool.MonthFirstDate(date).ToString("yyyyMMdd"));
				if (dataTable.Rows.Count > 0)
				{
					MSCE.ExecuteNonQuery("UPDATE [UserData] SET \r\n                                                 [ListDate] = @1\r\n\t\t                                        ,[UserJson] = @2\r\n\t\t                                         where UserID = @3 and ListDate =@4", DateTool.MonthFirstDate(date).ToString("yyyyMMdd"), JsonConvert.SerializeObject(user), user.ID, DateTool.MonthFirstDate(date).ToString("yyyyMMdd"));
				}
				else
				{
					MSCE.ExecuteNonQuery("INSERT INTO [UserData] ([UserID],[ListDate],UserJson)\r\n                                                            VALUES (@1,@2,@3)", user.ID, DateTool.MonthFirstDate(date).ToString("yyyyMMdd"), JsonConvert.SerializeObject(user));
				}
			}
			else
			{
				AutoClosingMessageBox.Show("無法取得資料，請稍後再試");
			}
			MouseTool.ShowArrow();
		}

		private void InitUI(UserMeeting[] userMeetingAry, DateTime date)
		{
			_003C_003Ec__DisplayClass31 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass31();
			_003C_003Ec__DisplayClass.userMeetingAry = userMeetingAry;
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			string courseOrMeeting_String = Settings.Default.CourseOrMeeting_String;
			txtNothing.Text = string.Format("本月無{0}", courseOrMeeting_String);
			txtPinCodeHint.Text = string.Format("請輸入{0}識別碼", courseOrMeeting_String);
			btnAddHint.Text = string.Format("加入{0}", courseOrMeeting_String);
			btnAddHint.Visibility = Visibility.Visible;
			if (_003C_003Ec__DisplayClass.userMeetingAry.Length > 0)
			{
				txtNothing.Visibility = Visibility.Collapsed;
			}
			else
			{
				txtNothing.Visibility = Visibility.Visible;
			}
			MeetingListDate = date;
			txtCount.Text = string.Format("共 {0} 個{1}", _003C_003Ec__DisplayClass.userMeetingAry.Length.ToString(), courseOrMeeting_String);
			txtDate.Text = date.ToString("yyyy年MM月");
			if (!Settings.Default.IsNewMeeting_PopupDialog)
			{
				txtPinCodeHint.Visibility = Visibility.Visible;
				txtPinCode.Visibility = Visibility.Visible;
			}
			Task.Factory.StartNew(new Action(_003C_003Ec__DisplayClass._003CInitUI_003Eb__21));
		}

		private Dictionary<DateTime, List<UserMeeting>> GetUserMeetingDict_ByOrder(UserMeeting[] userMeetingAry, bool IsReverse)
		{
			Dictionary<DateTime, List<UserMeeting>> dictionary = new Dictionary<DateTime, List<UserMeeting>>();
			if (IsReverse)
			{
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate4b == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate4b = new Func<UserMeeting, DateTime>(_003CGetUserMeetingDict_ByOrder_003Eb__43);
				}
				IOrderedEnumerable<UserMeeting> source = Enumerable.OrderBy(userMeetingAry, CS_0024_003C_003E9__CachedAnonymousMethodDelegate4b);
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate4c == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate4c = new Func<UserMeeting, DateTime>(_003CGetUserMeetingDict_ByOrder_003Eb__44);
				}
				IEnumerable<IGrouping<DateTime, UserMeeting>> source2 = Enumerable.Reverse(Enumerable.GroupBy(source, CS_0024_003C_003E9__CachedAnonymousMethodDelegate4c));
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate4d == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate4d = new Func<IGrouping<DateTime, UserMeeting>, DateTime>(_003CGetUserMeetingDict_ByOrder_003Eb__45);
				}
				Func<IGrouping<DateTime, UserMeeting>, DateTime> cS_0024_003C_003E9__CachedAnonymousMethodDelegate4d = CS_0024_003C_003E9__CachedAnonymousMethodDelegate4d;
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate4e == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate4e = new Func<IGrouping<DateTime, UserMeeting>, List<UserMeeting>>(_003CGetUserMeetingDict_ByOrder_003Eb__46);
				}
				return Enumerable.ToDictionary(source2, cS_0024_003C_003E9__CachedAnonymousMethodDelegate4d, CS_0024_003C_003E9__CachedAnonymousMethodDelegate4e);
			}
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate4f == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate4f = new Func<UserMeeting, DateTime>(_003CGetUserMeetingDict_ByOrder_003Eb__47);
			}
			IOrderedEnumerable<UserMeeting> source3 = Enumerable.OrderBy(userMeetingAry, CS_0024_003C_003E9__CachedAnonymousMethodDelegate4f);
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate50 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate50 = new Func<UserMeeting, DateTime>(_003CGetUserMeetingDict_ByOrder_003Eb__48);
			}
			IEnumerable<IGrouping<DateTime, UserMeeting>> source4 = Enumerable.GroupBy(source3, CS_0024_003C_003E9__CachedAnonymousMethodDelegate50);
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate51 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate51 = new Func<IGrouping<DateTime, UserMeeting>, DateTime>(_003CGetUserMeetingDict_ByOrder_003Eb__49);
			}
			Func<IGrouping<DateTime, UserMeeting>, DateTime> cS_0024_003C_003E9__CachedAnonymousMethodDelegate = CS_0024_003C_003E9__CachedAnonymousMethodDelegate51;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate52 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate52 = new Func<IGrouping<DateTime, UserMeeting>, List<UserMeeting>>(_003CGetUserMeetingDict_ByOrder_003Eb__4a);
			}
			return Enumerable.ToDictionary(source4, cS_0024_003C_003E9__CachedAnonymousMethodDelegate, CS_0024_003C_003E9__CachedAnonymousMethodDelegate52);
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/meetinglistct.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				xGrid = (Grid)target;
				break;
			case 2:
				btnLast = (Image)target;
				break;
			case 3:
				txtDate = (TextBlock)target;
				break;
			case 4:
				btnNext = (Image)target;
				break;
			case 5:
				txtCount = (TextBlock)target;
				break;
			case 6:
				txtPinCodeHint = (TextBlock)target;
				break;
			case 7:
				txtPinCode = (TextBox)target;
				break;
			case 8:
				btnAdd = (Border)target;
				break;
			case 9:
				btnAddHint = (TextBlock)target;
				break;
			case 10:
				SV = (ScrollViewer)target;
				break;
			case 11:
				txtNothing = (TextBlock)target;
				break;
			case 12:
				MeetingDaySP = (StackPanel)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}

		[CompilerGenerated]
		private void _003CMeetingListCT_Loaded_003Eb__0()
		{
			InitSelectDB();
			base.Dispatcher.BeginInvoke(new Action<UserMeeting[], DateTime>(_003CMeetingListCT_Loaded_003Eb__1), UserMeetingAry, MeetingListDate);
			PreLoadLastNextMonth();
		}

		[CompilerGenerated]
		private void _003CMeetingListCT_Loaded_003Eb__1(UserMeeting[] innerUserMetingAry, DateTime innerMeetingListDate)
		{
			try
			{
				InitUI(innerUserMetingAry, innerMeetingListDate);
				InitEvent();
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private void _003CPreLoadLastNextMonth_003Eb__2()
		{
			GetUserData.AsyncPOST(UserID, UserPWD, MeetingListDate.AddMonths(-1), new Action<User, DateTime>(_003CPreLoadLastNextMonth_003Eb__3));
		}

		[CompilerGenerated]
		private void _003CPreLoadLastNextMonth_003Eb__3(User userObj1, DateTime dateTime1)
		{
			Action<User, DateTime> action = null;
			ThreadStart threadStart = null;
			try
			{
				PreLoadLastNextMonthDict[dateTime1] = userObj1;
				string userID = UserID;
				string userPWD = UserPWD;
				DateTime date = MeetingListDate.AddMonths(1);
				if (action == null)
				{
					action = new Action<User, DateTime>(_003CPreLoadLastNextMonth_003Eb__4);
				}
				GetUserData.AsyncPOST(userID, userPWD, date, action);
				if (CacheMinuteTTL > 0)
				{
					if (CacheThread != null)
					{
						CacheThread.Abort();
					}
					if (threadStart == null)
					{
						threadStart = new ThreadStart(_003CPreLoadLastNextMonth_003Eb__5);
					}
					CacheThread = new Thread(threadStart);
					CacheThread.IsBackground = false;
					CacheThread.Start();
				}
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}

		[CompilerGenerated]
		private void _003CPreLoadLastNextMonth_003Eb__4(User userObj2, DateTime dateTime2)
		{
			try
			{
				PreLoadLastNextMonthDict[dateTime2] = userObj2;
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}

		[CompilerGenerated]
		private void _003CPreLoadLastNextMonth_003Eb__5()
		{
			Thread.Sleep(CacheMinuteTTL * 60 * 1000);
			PreLoadLastNextMonthDict.Clear();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__9(object sender, MouseEventArgs e)
		{
			MouseTool.ShowHand();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__a(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__b(object sender, MouseEventArgs e)
		{
			MouseTool.ShowHand();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__c(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__d(object sender, MouseEventArgs e)
		{
			MouseTool.ShowHand();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__e(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__f(object sender, MouseEventArgs e)
		{
			MouseTool.ShowIBeam();
			txtPinCode.Focus();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__10(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__11(object sender, ScrollChangedEventArgs e)
		{
			if (!SV.CanContentScroll)
			{
				Show_HiddenMeetingDayList();
			}
		}

		[CompilerGenerated]
		private void _003CCallAddNewMeeting_003Eb__19(string XmlDataString)
		{
			GetNewMeeting_DoAction(XmlDataString);
		}

		[CompilerGenerated]
		private void _003CGetNewMeeting_DoAction_003Eb__1a(User userObj, DateTime dateTime)
		{
			GetUserData_DoAction(userObj, dateTime);
		}

		[CompilerGenerated]
		private void _003CbtnLastNext_MouseLeftButtonDown_003Eb__1c()
		{
			Action<User, DateTime> action = null;
			if (PreLoadLastNextMonthDict.ContainsKey(MeetingListDate))
			{
				GetUserData_DoAction(PreLoadLastNextMonthDict[MeetingListDate], MeetingListDate);
				PreLoadLastNextMonth();
				return;
			}
			string userID = UserID;
			string userPWD = UserPWD;
			DateTime meetingListDate = MeetingListDate;
			if (action == null)
			{
				action = new Action<User, DateTime>(_003CbtnLastNext_MouseLeftButtonDown_003Eb__1d);
			}
			GetUserData.AsyncPOST(userID, userPWD, meetingListDate, action);
		}

		[CompilerGenerated]
		private void _003CbtnLastNext_MouseLeftButtonDown_003Eb__1d(User userObj, DateTime dateTime)
		{
			GetUserData_DoAction(userObj, dateTime);
			PreLoadLastNextMonth();
		}

		[CompilerGenerated]
		private static DateTime _003CGetUserMeetingDict_ByOrder_003Eb__43(UserMeeting item)
		{
			return DateTool.StringToDate(item.BeginTime);
		}

		[CompilerGenerated]
		private static DateTime _003CGetUserMeetingDict_ByOrder_003Eb__44(UserMeeting item)
		{
			return DateTool.StringToDate(item.BeginTime).Date;
		}

		[CompilerGenerated]
		private static DateTime _003CGetUserMeetingDict_ByOrder_003Eb__45(IGrouping<DateTime, UserMeeting> IGrouping)
		{
			return IGrouping.Key;
		}

		[CompilerGenerated]
		private static List<UserMeeting> _003CGetUserMeetingDict_ByOrder_003Eb__46(IGrouping<DateTime, UserMeeting> IGrouping)
		{
			return Enumerable.ToList(IGrouping);
		}

		[CompilerGenerated]
		private static DateTime _003CGetUserMeetingDict_ByOrder_003Eb__47(UserMeeting item)
		{
			return DateTool.StringToDate(item.BeginTime);
		}

		[CompilerGenerated]
		private static DateTime _003CGetUserMeetingDict_ByOrder_003Eb__48(UserMeeting item)
		{
			return DateTool.StringToDate(item.BeginTime).Date;
		}

		[CompilerGenerated]
		private static DateTime _003CGetUserMeetingDict_ByOrder_003Eb__49(IGrouping<DateTime, UserMeeting> IGrouping)
		{
			return IGrouping.Key;
		}

		[CompilerGenerated]
		private static List<UserMeeting> _003CGetUserMeetingDict_ByOrder_003Eb__4a(IGrouping<DateTime, UserMeeting> IGrouping)
		{
			return Enumerable.ToList(IGrouping);
		}
	}
}
