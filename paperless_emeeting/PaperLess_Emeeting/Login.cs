using Newtonsoft.Json;
using PaperLess_Emeeting.App_Code.MessageBox;
using PaperLess_Emeeting.Properties;
using PaperLess_ViewModel;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace PaperLess_Emeeting
{
	public class Login : Window, IComponentConnector
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass18
		{
			public Login _003C_003E4__this;

			public HttpWebRequest request;

			public Action<HttpWebResponse> responseAction;

			public void _003CDoWithResponse_003Eb__14()
			{
				request.BeginGetResponse(new AsyncCallback(_003CDoWithResponse_003Eb__15), request);
			}

			public void _003CDoWithResponse_003Eb__15(IAsyncResult iar)
			{
				HttpWebResponse obj = (HttpWebResponse)((HttpWebRequest)iar.AsyncState).EndGetResponse(iar);
				responseAction(obj);
			}
		}

		private bool canLogin = true;

		private bool RemeberLogin;

		internal Image imgBeta;

		internal Image imgHeader;

		internal TextBlock txtVersion;

		internal TextBox tbUserID;

		internal Button btnUserIDClear;

		internal PasswordBox tbUserPWD;

		internal Button btnUserPWDClear;

		internal DockPanel RemeberLoginDP;

		internal CheckBox cbRemeberLogin;

		internal Image btnSubmit;

		internal TextBlock UserHint;

		private bool _contentLoaded;

		[CompilerGenerated]
		private string _003CUserID_003Ek__BackingField;

		[CompilerGenerated]
		private static Action CS_0024_003C_003E9__CachedAnonymousMethodDelegate13;

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

		public Login()
		{
			MouseTool.ShowLoading();
			base.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			InitializeComponent();
			base.Loaded += new RoutedEventHandler(Login_Loaded);
		}

		private void Login_Loaded(object sender, RoutedEventArgs e)
		{
			Task.Factory.StartNew(new Action(_003CLogin_Loaded_003Eb__0));
		}

		private void InitSelectDB()
		{
			DataTable dataTable = MSCE.GetDataTable("select UserID,RemeberLogin from NowLogin");
			if (dataTable.Rows.Count > 0)
			{
				UserID = dataTable.Rows[0]["UserID"].ToString().Trim();
				bool.TryParse(dataTable.Rows[0]["RemeberLogin"].ToString().Trim(), out RemeberLogin);
			}
		}

		private void InitUI()
		{
			base.Title = Settings.Default.AppName;
			imgHeader.Source = new BitmapImage(new Uri(Settings.Default.Login_Header_Image, UriKind.Relative));
			if (ApplicationDeployment.IsNetworkDeployed)
			{
				txtVersion.Text = "版本: " + ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
			}
			if (RemeberLogin)
			{
				tbUserID.Text = UserID;
				tbUserPWD.Password = "";
				cbRemeberLogin.IsChecked = true;
				if (tbUserID.Text.Length > 0)
				{
					btnUserIDClear.Visibility = Visibility.Visible;
				}
				else
				{
					btnUserIDClear.Visibility = Visibility.Collapsed;
				}
				if (tbUserPWD.Password.Length > 0)
				{
					btnUserPWDClear.Visibility = Visibility.Visible;
				}
				else
				{
					btnUserPWDClear.Visibility = Visibility.Collapsed;
				}
			}
			else
			{
				tbUserID.Text = "";
				tbUserPWD.Password = "";
				cbRemeberLogin.IsChecked = false;
			}
			if (Settings.Default.HasRemeberLogin)
			{
				RemeberLoginDP.Visibility = Visibility.Visible;
			}
			if (Settings.Default.IsDebugMode)
			{
				imgBeta.Visibility = Visibility.Visible;
			}
			if (Settings.Default.AssemblyName.Contains("PaperLess_Emeeting_EDU"))
			{
				UserHint.Visibility = Visibility.Visible;
			}
		}

		private void InitEvent()
		{
			tbUserID.MouseEnter += new MouseEventHandler(_003CInitEvent_003Eb__2);
			tbUserID.MouseLeave += new MouseEventHandler(_003CInitEvent_003Eb__3);
			tbUserID.PreviewKeyDown += new KeyEventHandler(tbUserID_PreviewKeyDown);
			tbUserID.KeyDown += new KeyEventHandler(tbUserID_KeyDown);
			btnUserIDClear.MouseEnter += new MouseEventHandler(_003CInitEvent_003Eb__4);
			btnUserIDClear.MouseLeave += new MouseEventHandler(_003CInitEvent_003Eb__5);
			btnUserIDClear.Click += new RoutedEventHandler(_003CInitEvent_003Eb__6);
			tbUserPWD.MouseEnter += new MouseEventHandler(_003CInitEvent_003Eb__7);
			tbUserPWD.MouseLeave += new MouseEventHandler(_003CInitEvent_003Eb__8);
			tbUserPWD.PreviewKeyDown += new KeyEventHandler(tbUserID_PreviewKeyDown);
			tbUserPWD.KeyDown += new KeyEventHandler(tbUserID_KeyDown);
			btnUserPWDClear.MouseEnter += new MouseEventHandler(_003CInitEvent_003Eb__9);
			btnUserPWDClear.MouseLeave += new MouseEventHandler(_003CInitEvent_003Eb__a);
			btnUserPWDClear.Click += new RoutedEventHandler(_003CInitEvent_003Eb__b);
			btnSubmit.MouseEnter += new MouseEventHandler(_003CInitEvent_003Eb__c);
			btnSubmit.MouseLeave += new MouseEventHandler(_003CInitEvent_003Eb__d);
			btnSubmit.MouseLeftButtonDown += new MouseButtonEventHandler(btnSubmit_MouseLeftButtonDown);
			base.KeyDown += new KeyEventHandler(tbUserID_KeyDown);
			tbUserID.Focus();
		}

		private void tbUserID_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			Task.Factory.StartNew(new Action(_003CtbUserID_PreviewKeyDown_003Eb__e));
		}

		private void tbUserID_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				if (tbUserID.Text.Trim().Equals(""))
				{
					AutoClosingMessageBox.Show("請輸入帳號");
				}
				else if (tbUserPWD.Password.Trim().Equals(""))
				{
					AutoClosingMessageBox.Show("請輸入密碼");
				}
				else if (canLogin)
				{
					CallLigon();
				}
			}
		}

		private void btnSubmit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (canLogin)
			{
				CallLigon();
			}
		}

		private void CallLigon()
		{
			Action<HttpWebResponse> action = null;
			canLogin = false;
			MouseTool.ShowLoading();
			try
			{
				string text = tbUserID.Text.Trim();
				string text2 = tbUserPWD.Password.Trim();
				string text3 = DateTool.MonthFirstDate(DateTime.Today).ToString("yyyyMMdd");
				string text4 = DateTool.MonthFirstDate(DateTime.Today.AddMonths(1)).ToString("yyyyMMdd");
				if (NetworkTool.CheckNetwork() > 0)
				{
					string url = WsTool.GetUrl();
					string format = "<?xml version=\"1.0\"?><UserInfo><UserID><![CDATA[{0}]]></UserID><UserPW><![CDATA[{1}]]></UserPW><UserDevice>1</UserDevice><UserDateBegin>{2}</UserDateBegin><UserDateEnd>{3}</UserDateEnd></UserInfo>";
					format = string.Format(format, text, text2, text3, text4);
					Dictionary<string, string> dictionary = new Dictionary<string, string>();
					dictionary["XmlDoc"] = format;
					HttpWebRequest httpWebRequest = HttpTool.GetHttpWebRequest(url + "/UserData", "POST", dictionary);
					if (action == null)
					{
						action = new Action<HttpWebResponse>(_003CCallLigon_003Eb__10);
					}
					DoWithResponse(httpWebRequest, action);
				}
				else
				{
					DataTable dataTable = MSCE.GetDataTable("select UserID from LoginInfo where UserID =@1", text);
					if (dataTable.Rows.Count > 0)
					{
						dataTable = MSCE.GetDataTable("select UserJson from LoginInfo where UserID =@1 and UserPWD=@2", text, text2);
						if (dataTable.Rows.Count > 0)
						{
							User user = JsonConvert.DeserializeObject<User>(dataTable.Rows[0]["UserJson"].ToString());
							string text5 = JsonConvert.SerializeObject(user.EnableButtonList);
							string s = user.UTC.ToString();
							long num = 0L;
							try
							{
								num = DateTool.GetCurrentTimeInUnixMillis() - long.Parse(s);
							}
							catch (Exception ex)
							{
								LogTool.Debug(ex);
							}
							string text6 = "Update NowLogin set UserID=@1,UserName=@2,UserPWD=@3,MeetingListDate=getdate(),HomeUserButtonAryJSON=@4,UserEmail=@5,UTC=@6,DeltaUTC=@7,RemeberLogin=@8";
							string[] array = new string[8]
							{
								user.ID,
								user.Name,
								tbUserPWD.Password.Trim(),
								text5,
								user.Email,
								DateTool.GetCurrentTimeInUnixMillis().ToString(),
								num.ToString(),
								null
							};
							bool? isChecked = cbRemeberLogin.IsChecked;
							array[7] = ((isChecked.GetValueOrDefault() && isChecked.HasValue) ? "true" : "false");
							int num2 = MSCE.ExecuteNonQuery(text6, array);
							if (num2 < 1)
							{
								LogTool.Debug(new Exception("DB失敗: " + text6));
							}
							else
							{
								base.Dispatcher.BeginInvoke(new Action<User>(CheckLogin), user);
							}
						}
						else
						{
							MouseTool.ShowArrow();
							AutoClosingMessageBox.Show("您的密碼錯誤");
							canLogin = true;
						}
					}
					else
					{
						MouseTool.ShowArrow();
						AutoClosingMessageBox.Show("無此使用者帳號，請重新輸入");
						canLogin = true;
					}
				}
			}
			catch (Exception)
			{
				MouseTool.ShowArrow();
				AutoClosingMessageBox.Show("登入失敗");
				canLogin = true;
			}
		}

		private void CheckLogin(User user)
		{
			try
			{
				if (user != null)
				{
					switch (user.State)
					{
					case "0":
					{
						string text = JsonConvert.SerializeObject(user.EnableButtonList);
						string text2 = (user.UTC == null) ? DateTool.GetCurrentTimeInUnixMillis().ToString() : user.UTC.ToString();
						long num = 0L;
						try
						{
							num = DateTool.GetCurrentTimeInUnixMillis() - long.Parse(text2);
						}
						catch (Exception ex)
						{
							LogTool.Debug(ex);
						}
						string text3 = "Update NowLogin set UserID=@1,UserName=@2,UserPWD=@3,MeetingListDate=getdate(),HomeUserButtonAryJSON=@4,UserEmail=@5,UTC=@6,DeltaUTC=@7,RemeberLogin=@8";
						string[] array = new string[8]
						{
							user.ID,
							user.Name,
							tbUserPWD.Password.Trim(),
							text,
							user.Email,
							text2,
							num.ToString(),
							null
						};
						bool? isChecked = cbRemeberLogin.IsChecked;
						array[7] = ((isChecked.GetValueOrDefault() && isChecked.HasValue) ? "true" : "false");
						int num2 = MSCE.ExecuteNonQuery(text3, array);
						if (num2 < 1)
						{
							LogTool.Debug(new Exception("DB失敗: " + text3));
							return;
						}
						try
						{
							DataTable dataTable = MSCE.GetDataTable("select UserID from LoginInfo where UserID =@1", user.ID);
							if (dataTable.Rows.Count > 0)
							{
								MSCE.ExecuteNonQuery("UPDATE [LoginInfo] SET \r\n                                                 [UserID] = @1\r\n\t\t                                        ,[UserPWD] = @2\r\n                                                ,UserJson = @3\r\n\t\t                                         where UserID=@4", user.ID, tbUserPWD.Password.Trim(), JsonConvert.SerializeObject(user), user.ID);
							}
							else
							{
								MSCE.ExecuteNonQuery("INSERT INTO [LoginInfo] ([UserID],[UserPWD],UserJson)\r\n                                                            VALUES (@1,@2,@3)", user.ID, tbUserPWD.Password.Trim(), JsonConvert.SerializeObject(user));
							}
							dataTable = MSCE.GetDataTable("select ListDate from UserData where UserID =@1 and ListDate =@2", user.ID, DateTool.MonthFirstDate(DateTime.Now).ToString("yyyyMMdd"));
							if (dataTable.Rows.Count > 0)
							{
								MSCE.ExecuteNonQuery("UPDATE [UserData] SET \r\n                                                             [ListDate] = @1\r\n\t\t                                                    ,[UserJson] = @2\r\n\t\t                                                     where UserID = @3 and ListDate =@4", DateTool.MonthFirstDate(DateTime.Now).ToString("yyyyMMdd"), JsonConvert.SerializeObject(user), user.ID, DateTool.MonthFirstDate(DateTime.Now).ToString("yyyyMMdd"));
							}
							else
							{
								MSCE.ExecuteNonQuery("INSERT INTO [UserData] ([UserID],[ListDate],UserJson)\r\n                                                                        VALUES (@1,@2,@3)", user.ID, DateTool.MonthFirstDate(DateTime.Now).ToString("yyyyMMdd"), JsonConvert.SerializeObject(user));
							}
						}
						catch (Exception ex2)
						{
							LogTool.Debug(ex2);
						}
						Hide();
						Home home = new Home(user, tbUserPWD.Password.Trim());
						home.Show();
						Close();
						break;
					}
					case "1":
						AutoClosingMessageBox.Show("無此使用者帳號，請重新輸入");
						break;
					case "2":
						AutoClosingMessageBox.Show("帳號密碼錯誤或帳號已被鎖定");
						break;
					}
				}
			}
			catch (Exception ex3)
			{
				LogTool.Debug(ex3);
			}
			canLogin = true;
			MouseTool.ShowArrow();
		}

		private void DoWithResponse(HttpWebRequest request, Action<HttpWebResponse> responseAction)
		{
			_003C_003Ec__DisplayClass18 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass18();
			_003C_003Ec__DisplayClass.request = request;
			_003C_003Ec__DisplayClass.responseAction = responseAction;
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			Action action = new Action(_003C_003Ec__DisplayClass._003CDoWithResponse_003Eb__14);
			action.BeginInvoke(new AsyncCallback(_003CDoWithResponse_003Eb__16), action);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/login.xaml", UriKind.Relative);
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
				imgBeta = (Image)target;
				break;
			case 2:
				imgHeader = (Image)target;
				break;
			case 3:
				txtVersion = (TextBlock)target;
				break;
			case 4:
				tbUserID = (TextBox)target;
				break;
			case 5:
				btnUserIDClear = (Button)target;
				break;
			case 6:
				tbUserPWD = (PasswordBox)target;
				break;
			case 7:
				btnUserPWDClear = (Button)target;
				break;
			case 8:
				RemeberLoginDP = (DockPanel)target;
				break;
			case 9:
				cbRemeberLogin = (CheckBox)target;
				break;
			case 10:
				btnSubmit = (Image)target;
				break;
			case 11:
				UserHint = (TextBlock)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}

		[CompilerGenerated]
		private void _003CLogin_Loaded_003Eb__0()
		{
			InitSelectDB();
			base.Dispatcher.BeginInvoke(new Action(_003CLogin_Loaded_003Eb__1));
			HttpTool.GetResponseLength(WsTool.GetUrl() + "/UserData?XmlDoc=");
		}

		[CompilerGenerated]
		private void _003CLogin_Loaded_003Eb__1()
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
			if (canLogin)
			{
				MouseTool.ShowIBeam();
			}
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__3(object sender, MouseEventArgs e)
		{
			if (canLogin)
			{
				MouseTool.ShowArrow();
			}
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__4(object sender, MouseEventArgs e)
		{
			if (canLogin)
			{
				MouseTool.ShowHand();
			}
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__5(object sender, MouseEventArgs e)
		{
			if (canLogin)
			{
				MouseTool.ShowArrow();
			}
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__6(object sender, RoutedEventArgs e)
		{
			tbUserID.Text = "";
			btnUserIDClear.Visibility = Visibility.Collapsed;
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__7(object sender, MouseEventArgs e)
		{
			if (canLogin)
			{
				MouseTool.ShowIBeam();
			}
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__8(object sender, MouseEventArgs e)
		{
			if (canLogin)
			{
				MouseTool.ShowArrow();
			}
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__9(object sender, MouseEventArgs e)
		{
			if (canLogin)
			{
				MouseTool.ShowHand();
			}
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__a(object sender, MouseEventArgs e)
		{
			if (canLogin)
			{
				MouseTool.ShowArrow();
			}
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__b(object sender, RoutedEventArgs e)
		{
			tbUserPWD.Password = "";
			btnUserPWDClear.Visibility = Visibility.Collapsed;
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__c(object sender, MouseEventArgs e)
		{
			if (canLogin)
			{
				MouseTool.ShowHand();
			}
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__d(object sender, MouseEventArgs e)
		{
			if (canLogin)
			{
				MouseTool.ShowArrow();
			}
		}

		[CompilerGenerated]
		private void _003CtbUserID_PreviewKeyDown_003Eb__e()
		{
			Thread.Sleep(10);
			base.Dispatcher.BeginInvoke(new Action(_003CtbUserID_PreviewKeyDown_003Eb__f));
		}

		[CompilerGenerated]
		private void _003CtbUserID_PreviewKeyDown_003Eb__f()
		{
			if (tbUserID.Text.Length > 0)
			{
				btnUserIDClear.Visibility = Visibility.Visible;
			}
			else
			{
				btnUserIDClear.Visibility = Visibility.Collapsed;
			}
			if (tbUserPWD.Password.Length > 0)
			{
				btnUserPWDClear.Visibility = Visibility.Visible;
			}
			else
			{
				btnUserPWDClear.Visibility = Visibility.Collapsed;
			}
		}

		[CompilerGenerated]
		private void _003CCallLigon_003Eb__10(HttpWebResponse response)
		{
			User user = null;
			try
			{
				string s = new StreamReader(response.GetResponseStream()).ReadToEnd();
				user = XmlHelper.XmlDeserialize<User>(s, Encoding.UTF8);
			}
			catch (Exception ex)
			{
				Dispatcher dispatcher = base.Dispatcher;
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate13 == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate13 = new Action(_003CCallLigon_003Eb__11);
				}
				dispatcher.BeginInvoke(CS_0024_003C_003E9__CachedAnonymousMethodDelegate13);
				LogTool.Debug(ex);
			}
			base.Dispatcher.BeginInvoke(new Action<User>(CheckLogin), user);
		}

		[CompilerGenerated]
		private static void _003CCallLigon_003Eb__11()
		{
			AutoClosingMessageBox.Show("登入失敗");
		}

		[CompilerGenerated]
		private void _003CDoWithResponse_003Eb__16(IAsyncResult iar)
		{
			Action action = null;
			Action action2 = (Action)iar.AsyncState;
			try
			{
				action2.EndInvoke(iar);
			}
			catch (Exception ex)
			{
				AutoClosingMessageBox.Show(ex.Message);
				LogTool.Debug(ex);
				Dispatcher dispatcher = base.Dispatcher;
				if (action == null)
				{
					action = new Action(_003CDoWithResponse_003Eb__17);
				}
				dispatcher.Invoke(action, new object[0]);
			}
		}

		[CompilerGenerated]
		private void _003CDoWithResponse_003Eb__17()
		{
			canLogin = true;
			MouseTool.ShowArrow();
		}
	}
}
