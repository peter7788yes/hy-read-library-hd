using PaperLess_Emeeting.App_Code.MessageBox;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

namespace PaperLess_Emeeting
{
	public class SignListCT_Mix : UserControl, IComponentConnector
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass1c
		{
			public Home Home_Window;

			public void _003CInitEvent_003Eb__10(string x, string y)
			{
				Home_Window.CC.Content = new SignListCT_Mix();
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass27
		{
			public List<SignRow_Mix> SignRowS;

			public SignListCT_Mix _003C_003E4__this;

			public void _003CChangeSignRow_ByOrder_003Eb__21()
			{
				_003C_003E4__this.Dispatcher.BeginInvoke(new Action(_003CChangeSignRow_ByOrder_003Eb__22));
			}

			public void _003CChangeSignRow_ByOrder_003Eb__22()
			{
				foreach (SignRow_Mix signRow in SignRowS)
				{
					_003C_003E4__this.SignRowSP.Children.Add(signRow);
				}
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass2d
		{
			public SignListCT_Mix _003C_003E4__this;

			public SigninData sid;

			public void _003CGetSigninData_DoAction_003Eb__2a()
			{
				_003C_003E4__this.Dispatcher.BeginInvoke(new Action(_003CGetSigninData_DoAction_003Eb__2b));
			}

			public void _003CGetSigninData_DoAction_003Eb__2b()
			{
				SigninDataUser[] userList = sid.UserList;
				foreach (SigninDataUser signinDataUser in userList)
				{
					bool enableTxtPLSSigned = false;
					if (_003C_003E4__this.BeginTime.AddHours(-1.0) <= DateTime.Now && DateTime.Now < _003C_003E4__this.EndTime.AddHours(1.0))
					{
						enableTxtPLSSigned = true;
					}
					_003C_003E4__this.SignRowSP.Children.Add(new SignRow_Mix(signinDataUser, _003C_003E4__this.meetingUserType, enableTxtPLSSigned));
				}
			}
		}

		public DateTime BeginTime = new DateTime(2010, 1, 1);

		public DateTime EndTime = new DateTime(2050, 1, 1);

		public MeetingUserType meetingUserType = MeetingUserType.與會人員;

		internal StackPanel CateBtnS;

		internal Border btnIndex;

		internal TextBlock txtIndex;

		internal Border btnDept;

		internal TextBlock txtDept;

		internal Border btnIsSigned;

		internal TextBlock txtIsSigned;

		internal Canvas SearchInput;

		internal TextBox txtKeyword;

		internal Image btnAddUser;

		internal StackPanel SignRowSP;

		private bool _contentLoaded;

		[CompilerGenerated]
		private string _003CUserID_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserName_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserPWD_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CMeetingID_003Ek__BackingField;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate11;

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
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate19;

		[CompilerGenerated]
		private static MouseButtonEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate1a;

		[CompilerGenerated]
		private static Func<SignRow_Mix, int> CS_0024_003C_003E9__CachedAnonymousMethodDelegate23;

		[CompilerGenerated]
		private static Func<SignRow_Mix, string> CS_0024_003C_003E9__CachedAnonymousMethodDelegate24;

		[CompilerGenerated]
		private static Func<SignRow_Mix, string> CS_0024_003C_003E9__CachedAnonymousMethodDelegate25;

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

		public SignListCT_Mix()
		{
			MouseTool.ShowLoading();
			InitializeComponent();
			base.Loaded += new RoutedEventHandler(SignListCT_Mix_Loaded);
		}

		private void SignListCT_Mix_Loaded(object sender, RoutedEventArgs e)
		{
			Task.Factory.StartNew(new Action(_003CSignListCT_Mix_Loaded_003Eb__0));
		}

		private void InitEvent()
		{
			Border border = btnIndex;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate11 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate11 = new MouseEventHandler(_003CInitEvent_003Eb__2);
			}
			border.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate11;
			Border border2 = btnIndex;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate12 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate12 = new MouseEventHandler(_003CInitEvent_003Eb__3);
			}
			border2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate12;
			btnIndex.MouseLeftButtonDown += new MouseButtonEventHandler(_003CInitEvent_003Eb__4);
			Border border3 = btnDept;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate13 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate13 = new MouseEventHandler(_003CInitEvent_003Eb__5);
			}
			border3.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate13;
			Border border4 = btnDept;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate14 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate14 = new MouseEventHandler(_003CInitEvent_003Eb__6);
			}
			border4.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate14;
			btnDept.MouseLeftButtonDown += new MouseButtonEventHandler(_003CInitEvent_003Eb__7);
			Border border5 = btnIsSigned;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate15 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate15 = new MouseEventHandler(_003CInitEvent_003Eb__8);
			}
			border5.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate15;
			Border border6 = btnIsSigned;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate16 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate16 = new MouseEventHandler(_003CInitEvent_003Eb__9);
			}
			border6.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate16;
			btnIsSigned.MouseLeftButtonDown += new MouseButtonEventHandler(_003CInitEvent_003Eb__a);
			txtKeyword.MouseEnter += new MouseEventHandler(_003CInitEvent_003Eb__b);
			TextBox textBox = txtKeyword;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate17 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate17 = new MouseEventHandler(_003CInitEvent_003Eb__c);
			}
			textBox.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate17;
			txtKeyword.KeyUp += new KeyEventHandler(txtKeyword_KeyUp);
			txtKeyword.Focus();
			Image image = btnAddUser;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate18 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate18 = new MouseEventHandler(_003CInitEvent_003Eb__d);
			}
			image.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate18;
			Image image2 = btnAddUser;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate19 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate19 = new MouseEventHandler(_003CInitEvent_003Eb__e);
			}
			image2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate19;
			Image image3 = btnAddUser;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate1a == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate1a = new MouseButtonEventHandler(_003CInitEvent_003Eb__f);
			}
			image3.MouseLeftButtonDown += CS_0024_003C_003E9__CachedAnonymousMethodDelegate1a;
		}

		private void txtKeyword_KeyUp(object sender, KeyEventArgs e)
		{
			CallSearch();
		}

		private void CallSearch()
		{
			string text = txtKeyword.Text.ToLower().Trim();
			if (!text.Equals(""))
			{
				foreach (SignRow_Mix item in Enumerable.OfType<SignRow_Mix>(SignRowSP.Children))
				{
					if (item.txtIndex.Text.ToLower().Contains(text) || item.txtAgent.Text.ToLower().Contains(text) || item.txtDept.Text.ToLower().Contains(text) || item.txtTitle.Text.ToLower().Contains(text) || item.txtUserName.Text.ToLower().Contains(text))
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
				foreach (SignRow_Mix item2 in Enumerable.OfType<SignRow_Mix>(SignRowSP.Children))
				{
					item2.Visibility = Visibility.Visible;
				}
			}
		}

		private void ChangeSignRow_ByOrder(SignListCT_Order signListCT_Order)
		{
			Action action = null;
			_003C_003Ec__DisplayClass27 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass27();
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			_003C_003Ec__DisplayClass.SignRowS = Enumerable.ToList(Enumerable.OfType<SignRow_Mix>(SignRowSP.Children));
			SignRowSP.Children.Clear();
			switch (signListCT_Order)
			{
			case SignListCT_Order.序號:
			{
				List<SignRow_Mix> signRowS2 = _003C_003Ec__DisplayClass.SignRowS;
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate23 == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate23 = new Func<SignRow_Mix, int>(_003CChangeSignRow_ByOrder_003Eb__1e);
				}
				_003C_003Ec__DisplayClass.SignRowS = Enumerable.ToList(Enumerable.OrderBy(signRowS2, CS_0024_003C_003E9__CachedAnonymousMethodDelegate23));
				break;
			}
			case SignListCT_Order.機關單位:
			{
				List<SignRow_Mix> signRowS3 = _003C_003Ec__DisplayClass.SignRowS;
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate24 == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate24 = new Func<SignRow_Mix, string>(_003CChangeSignRow_ByOrder_003Eb__1f);
				}
				_003C_003Ec__DisplayClass.SignRowS = Enumerable.ToList(Enumerable.OrderBy(signRowS3, CS_0024_003C_003E9__CachedAnonymousMethodDelegate24));
				break;
			}
			case SignListCT_Order.是否簽到:
			{
				List<SignRow_Mix> signRowS = _003C_003Ec__DisplayClass.SignRowS;
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate25 == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate25 = new Func<SignRow_Mix, string>(_003CChangeSignRow_ByOrder_003Eb__20);
				}
				_003C_003Ec__DisplayClass.SignRowS = Enumerable.ToList(Enumerable.OrderBy(signRowS, CS_0024_003C_003E9__CachedAnonymousMethodDelegate25));
				break;
			}
			}
			if (_003C_003Ec__DisplayClass.SignRowS != null)
			{
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003C_003Ec__DisplayClass._003CChangeSignRow_ByOrder_003Eb__21);
				}
				factory.StartNew(action);
			}
			txtKeyword.Text = "";
			txtKeyword.Focus();
		}

		private void ClearButtonColor()
		{
			btnIndex.Background = ColorTool.HexColorToBrush("#D3Dce0");
			btnDept.Background = ColorTool.HexColorToBrush("#D3Dce0");
			btnIsSigned.Background = ColorTool.HexColorToBrush("#D3Dce0");
			txtIndex.Foreground = Brushes.Black;
			txtDept.Foreground = Brushes.Black;
			txtIsSigned.Foreground = Brushes.Black;
		}

		private void ClearBorderColor()
		{
			btnIndex.BorderBrush = ColorTool.HexColorToBrush("#5F879B");
			btnDept.BorderBrush = ColorTool.HexColorToBrush("#5F879B");
			btnIsSigned.BorderBrush = ColorTool.HexColorToBrush("#5F879B");
		}

		private void InitSelectDB()
		{
			DataTable dataTable = MSCE.GetDataTable("select UserID,UserName,UserPWD,MeetingID,MeetingBeginTime,MeetingEndTime,MeetingUserType from NowLogin");
			if (dataTable.Rows.Count > 0)
			{
				UserID = dataTable.Rows[0]["UserID"].ToString();
				UserName = dataTable.Rows[0]["UserName"].ToString();
				UserPWD = dataTable.Rows[0]["UserPWD"].ToString();
				MeetingID = dataTable.Rows[0]["MeetingID"].ToString();
				DateTime.TryParse(dataTable.Rows[0]["MeetingBeginTime"].ToString(), out BeginTime);
				DateTime.TryParse(dataTable.Rows[0]["MeetingEndTime"].ToString(), out EndTime);
				Enum.TryParse(dataTable.Rows[0]["MeetingUserType"].ToString(), out meetingUserType);
			}
		}

		private void InitUI()
		{
			MouseTool.ShowLoading();
			switch (meetingUserType)
			{
			case MeetingUserType.議事管理人員:
				CateBtnS.Visibility = Visibility.Visible;
				SearchInput.Visibility = Visibility.Visible;
				btnAddUser.Visibility = Visibility.Visible;
				break;
			}
			GetSigninData.AsyncPOST(MeetingID, new Action<SigninData>(_003CInitUI_003Eb__29));
		}

		private void GetSigninData_DoAction(SigninData sid)
		{
			Action action = null;
			_003C_003Ec__DisplayClass2d _003C_003Ec__DisplayClass2d = new _003C_003Ec__DisplayClass2d();
			_003C_003Ec__DisplayClass2d.sid = sid;
			_003C_003Ec__DisplayClass2d._003C_003E4__this = this;
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<SigninData>(GetSigninData_DoAction), _003C_003Ec__DisplayClass2d.sid);
				return;
			}
			if (_003C_003Ec__DisplayClass2d.sid != null)
			{
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003C_003Ec__DisplayClass2d._003CGetSigninData_DoAction_003Eb__2a);
				}
				factory.StartNew(action);
			}
			else
			{
				AutoClosingMessageBox.Show("無法取得資料，請稍後再試");
			}
			MouseTool.ShowArrow();
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/signlistct_mix.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				CateBtnS = (StackPanel)target;
				break;
			case 2:
				btnIndex = (Border)target;
				break;
			case 3:
				txtIndex = (TextBlock)target;
				break;
			case 4:
				btnDept = (Border)target;
				break;
			case 5:
				txtDept = (TextBlock)target;
				break;
			case 6:
				btnIsSigned = (Border)target;
				break;
			case 7:
				txtIsSigned = (TextBlock)target;
				break;
			case 8:
				SearchInput = (Canvas)target;
				break;
			case 9:
				txtKeyword = (TextBox)target;
				break;
			case 10:
				btnAddUser = (Image)target;
				break;
			case 11:
				SignRowSP = (StackPanel)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}

		[CompilerGenerated]
		private void _003CSignListCT_Mix_Loaded_003Eb__0()
		{
			InitSelectDB();
			base.Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(_003CSignListCT_Mix_Loaded_003Eb__1));
		}

		[CompilerGenerated]
		private void _003CSignListCT_Mix_Loaded_003Eb__1()
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
		private static void _003CInitEvent_003Eb__2(object sender, MouseEventArgs e)
		{
			MouseTool.ShowHand();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__3(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__4(object sender, MouseButtonEventArgs e)
		{
			ClearButtonColor();
			btnIndex.Background = ColorTool.HexColorToBrush("#019fde");
			txtIndex.Foreground = Brushes.White;
			ChangeSignRow_ByOrder(SignListCT_Order.序號);
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__5(object sender, MouseEventArgs e)
		{
			MouseTool.ShowHand();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__6(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__7(object sender, MouseButtonEventArgs e)
		{
			ClearButtonColor();
			btnDept.Background = ColorTool.HexColorToBrush("#019fde");
			txtDept.Foreground = Brushes.White;
			ChangeSignRow_ByOrder(SignListCT_Order.機關單位);
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__8(object sender, MouseEventArgs e)
		{
			MouseTool.ShowHand();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__9(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__a(object sender, MouseButtonEventArgs e)
		{
			ClearButtonColor();
			btnIsSigned.Background = ColorTool.HexColorToBrush("#019fde");
			txtIsSigned.Foreground = Brushes.White;
			ChangeSignRow_ByOrder(SignListCT_Order.是否簽到);
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__b(object sender, MouseEventArgs e)
		{
			MouseTool.ShowIBeam();
			txtKeyword.Focus();
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
		private static void _003CInitEvent_003Eb__f(object sender, MouseButtonEventArgs e)
		{
			Home_GoBackTogSignPictureCT_Function home_GoBackTogSignPictureCT_Function = null;
			_003C_003Ec__DisplayClass1c _003C_003Ec__DisplayClass1c = new _003C_003Ec__DisplayClass1c();
			_003C_003Ec__DisplayClass1c.Home_Window = Enumerable.FirstOrDefault(Enumerable.OfType<Home>(Application.Current.Windows));
			if (_003C_003Ec__DisplayClass1c.Home_Window != null)
			{
				ContentControl cC = _003C_003Ec__DisplayClass1c.Home_Window.CC;
				if (home_GoBackTogSignPictureCT_Function == null)
				{
					home_GoBackTogSignPictureCT_Function = new Home_GoBackTogSignPictureCT_Function(_003C_003Ec__DisplayClass1c._003CInitEvent_003Eb__10);
				}
				cC.Content = new SignPadCT("guest", "來賓", "", "", home_GoBackTogSignPictureCT_Function);
			}
		}

		[CompilerGenerated]
		private static int _003CChangeSignRow_ByOrder_003Eb__1e(SignRow_Mix x)
		{
			return int.Parse(x.signinDataUser.Rank);
		}

		[CompilerGenerated]
		private static string _003CChangeSignRow_ByOrder_003Eb__1f(SignRow_Mix x)
		{
			return x.signinDataUser.Dept;
		}

		[CompilerGenerated]
		private static string _003CChangeSignRow_ByOrder_003Eb__20(SignRow_Mix x)
		{
			return x.signinDataUser.IsSigned;
		}

		[CompilerGenerated]
		private void _003CInitUI_003Eb__29(SigninData sid)
		{
			GetSigninData_DoAction(sid);
		}
	}
}
