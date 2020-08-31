using PaperLess_Emeeting.App_Code.MessageBox;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Threading;

namespace PaperLess_Emeeting
{
	public class SignPictureCT : UserControl, IComponentConnector
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass6
		{
			public SignPictureCT _003C_003E4__this;

			public SigninData sid;

			public void _003CGetSigninData_DoAction_003Eb__3()
			{
				_003C_003E4__this.Dispatcher.BeginInvoke(new Action(_003CGetSigninData_DoAction_003Eb__4));
			}

			public void _003CGetSigninData_DoAction_003Eb__4()
			{
				SigninDataUser[] userList = sid.UserList;
				foreach (SigninDataUser signinDataUser in userList)
				{
					SignRoom element = new SignRoom(signinDataUser, _003C_003E4__this.Home_ChangeTogSignPadCT_Event);
					_003C_003E4__this.SignRoomWP.Children.Add(element);
				}
			}
		}

		internal WrapPanel SignRoomWP;

		private bool _contentLoaded;

		[CompilerGenerated]
		private string _003CMeetingID_003Ek__BackingField;

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

		public event Home_ChangeTogSignPadCT_Function Home_ChangeTogSignPadCT_Event;

		public event Home_GoBackTogSignPictureCT_Function Home_GoBackTogSignPictureCT_Event;

		public SignPictureCT(Home_ChangeTogSignPadCT_Function callback1, Home_GoBackTogSignPictureCT_Function callback2)
		{
			MouseTool.ShowLoading();
			InitializeComponent();
			Home_ChangeTogSignPadCT_Event += callback1;
			Home_GoBackTogSignPictureCT_Event += callback2;
			base.Loaded += new RoutedEventHandler(SignPictureCT_Loaded);
		}

		private void SignPictureCT_Loaded(object sender, RoutedEventArgs e)
		{
			Task.Factory.StartNew(new Action(_003CSignPictureCT_Loaded_003Eb__0));
		}

		private void InitSelectDB()
		{
			DataTable dataTable = MSCE.GetDataTable("select MeetingID from NowLogin");
			if (dataTable.Rows.Count > 0)
			{
				MeetingID = dataTable.Rows[0]["MeetingID"].ToString();
			}
		}

		private void InitEvent()
		{
		}

		private void InitUI()
		{
			MouseTool.ShowLoading();
			GetSigninData.AsyncPOST(MeetingID, new Action<SigninData>(_003CInitUI_003Eb__2));
		}

		private void GetSigninData_DoAction(SigninData sid)
		{
			Action action = null;
			_003C_003Ec__DisplayClass6 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass6();
			_003C_003Ec__DisplayClass.sid = sid;
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<SigninData>(GetSigninData_DoAction), _003C_003Ec__DisplayClass.sid);
				return;
			}
			if (_003C_003Ec__DisplayClass.sid != null)
			{
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003C_003Ec__DisplayClass._003CGetSigninData_DoAction_003Eb__3);
				}
				factory.StartNew(action);
			}
			else
			{
				AutoClosingMessageBox.Show("無法取得資料，請稍後再試");
			}
			MouseTool.ShowArrow();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/signpicturect.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				SignRoomWP = (WrapPanel)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}

		[CompilerGenerated]
		private void _003CSignPictureCT_Loaded_003Eb__0()
		{
			InitSelectDB();
			base.Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(_003CSignPictureCT_Loaded_003Eb__1));
		}

		[CompilerGenerated]
		private void _003CSignPictureCT_Loaded_003Eb__1()
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
		private void _003CInitUI_003Eb__2(SigninData sid)
		{
			GetSigninData_DoAction(sid);
		}
	}
}
