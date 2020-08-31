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

namespace PaperLess_Emeeting
{
	public class SignListCT : UserControl, IComponentConnector
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass6
		{
			public SignListCT _003C_003E4__this;

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
					_003C_003E4__this.SignRowSP.Children.Add(new SignRow(signinDataUser));
				}
			}
		}

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

		public SignListCT()
		{
			MouseTool.ShowLoading();
			InitializeComponent();
			base.Loaded += new RoutedEventHandler(SignListCT_Loaded);
		}

		private void SignListCT_Loaded(object sender, RoutedEventArgs e)
		{
			Task.Factory.StartNew(new Action(_003CSignListCT_Loaded_003Eb__0));
		}

		private void InitEvent()
		{
		}

		private void InitSelectDB()
		{
			DataTable dataTable = MSCE.GetDataTable("select UserID,UserName,UserPWD,MeetingID from NowLogin");
			if (dataTable.Rows.Count > 0)
			{
				UserID = dataTable.Rows[0]["UserID"].ToString();
				UserName = dataTable.Rows[0]["UserName"].ToString();
				UserPWD = dataTable.Rows[0]["UserPWD"].ToString();
				MeetingID = dataTable.Rows[0]["MeetingID"].ToString();
			}
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

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/signlistct.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				SignRowSP = (StackPanel)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}

		[CompilerGenerated]
		private void _003CSignListCT_Loaded_003Eb__0()
		{
			InitSelectDB();
			base.Dispatcher.BeginInvoke(new Action(_003CSignListCT_Loaded_003Eb__1));
		}

		[CompilerGenerated]
		private void _003CSignListCT_Loaded_003Eb__1()
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
