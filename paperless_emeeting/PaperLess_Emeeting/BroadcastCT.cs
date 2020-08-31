using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PaperLess_Emeeting.App_Code.MessageBox;
using PaperLess_Emeeting.App_Code.Socket;
using PaperLess_Emeeting.Properties;
using PaperlessSync.Broadcast.Socket;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
using System.Xml;
using System.Xml.Linq;

namespace PaperLess_Emeeting
{
	public class BroadcastCT : UserControl, IComponentConnector
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClassf
		{
			public bool inList;

			public BroadcastCT _003C_003E4__this;

			public void _003CInitEvent_003Eb__7()
			{
				_003C_003E4__this.ChangeServerCtrl(inList);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass12
		{
			public Task<bool> task;

			public BroadcastCT _003C_003E4__this;

			public void _003CInitEvent_003Eb__9()
			{
				_003C_003E4__this.ChangeServerCtrl(task.Result);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass14
		{
			public _003C_003Ec__DisplayClass12 CS_0024_003C_003E8__locals13;

			public SocketClient socketClient;

			public void _003CInitEvent_003Eb__a()
			{
				if (socketClient != null && socketClient.GetIsConnected())
				{
					socketClient.getUserList();
				}
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass1e
		{
			public bool success;

			public BroadcastCT _003C_003E4__this;

			public void _003CbtnServerCtrl_MouseLeftButtonDown_003Eb__17()
			{
				_003C_003E4__this.ChangeServerCtrl(success);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass20
		{
			public _003C_003Ec__DisplayClass1e CS_0024_003C_003E8__locals1f;

			public SocketClient socketClient;

			public void _003CbtnServerCtrl_MouseLeftButtonDown_003Eb__18()
			{
				if (socketClient != null && socketClient.GetIsConnected())
				{
					socketClient.getUserList();
				}
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass23
		{
			public bool success;

			public BroadcastCT _003C_003E4__this;

			public void _003CbtnServerCtrl_MouseLeftButtonDown_003Eb__1a()
			{
				_003C_003E4__this.ChangeServerCtrl(!success);
			}
		}

		public BroadcastCT_ChangeList_Function BroadcastCT_ChangeList_Callback;

		public BroadcastCT_ClearList_Function BroadcastCT_ClearList_Callback;

		public bool CanDetectServerState = true;

		internal Grid btnServerCtrl;

		internal Image btnStatus;

		internal TextBlock txtStatus;

		internal TextBox txtKeyword;

		internal StackPanel BroadcastRowSP;

		private bool _contentLoaded;

		[CompilerGenerated]
		private string _003CUserID_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserName_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CMeetingID_003Ek__BackingField;

		[CompilerGenerated]
		private CancellationTokenSource _003CtokenSource_003Ek__BackingField;

		[CompilerGenerated]
		private DispatcherTimer _003CdTimer_003Ek__BackingField;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegatec;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegated;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegatee;

		[CompilerGenerated]
		private static Func<XElement, _003C_003Ef__AnonymousType0<string>> CS_0024_003C_003E9__CachedAnonymousMethodDelegate26;

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

		public CancellationTokenSource tokenSource
		{
			[CompilerGenerated]
			get
			{
				return _003CtokenSource_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CtokenSource_003Ek__BackingField = value;
			}
		}

		public DispatcherTimer dTimer
		{
			[CompilerGenerated]
			get
			{
				return _003CdTimer_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CdTimer_003Ek__BackingField = value;
			}
		}

		public BroadcastCT()
		{
			MouseTool.ShowLoading();
			InitializeComponent();
			tokenSource = new CancellationTokenSource();
			base.Loaded += new RoutedEventHandler(BroadcastCT_Loaded);
			base.Unloaded += new RoutedEventHandler(BroadcastCT_Unloaded);
		}

		private void BroadcastCT_Unloaded(object sender, RoutedEventArgs e)
		{
			Singleton_Socket.broadcastCT_OpenIEventManager.BroadcastCT_ChangeList_Event -= BroadcastCT_ChangeList_Callback;
			Singleton_Socket.broadcastCT_CloseIEventManager.BroadcastCT_ClearList_Event -= BroadcastCT_ClearList_Callback;
			CanDetectServerState = false;
		}

		private void BroadcastCT_Loaded(object sender, RoutedEventArgs e)
		{
			Task.Factory.StartNew(new Action(_003CBroadcastCT_Loaded_003Eb__0));
		}

		private void InitSelectDB()
		{
			DataTable dataTable = MSCE.GetDataTable("select UserID,UserName,UserPWD,MeetingID from NowLogin");
			if (dataTable.Rows.Count > 0)
			{
				UserID = dataTable.Rows[0]["UserID"].ToString();
				UserName = dataTable.Rows[0]["UserName"].ToString();
				MeetingID = dataTable.Rows[0]["MeetingID"].ToString();
			}
		}

		private void InitEvent()
		{
			txtKeyword.MouseEnter += new MouseEventHandler(_003CInitEvent_003Eb__2);
			TextBox textBox = txtKeyword;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegatec == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegatec = new MouseEventHandler(_003CInitEvent_003Eb__3);
			}
			textBox.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegatec;
			txtKeyword.KeyUp += new KeyEventHandler(txtKeyword_KeyUp);
			txtKeyword.Focus();
			Grid grid = btnServerCtrl;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegated == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegated = new MouseEventHandler(_003CInitEvent_003Eb__4);
			}
			grid.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegated;
			Grid grid2 = btnServerCtrl;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegatee == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegatee = new MouseEventHandler(_003CInitEvent_003Eb__5);
			}
			grid2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegatee;
			btnServerCtrl.MouseLeftButtonDown += new MouseButtonEventHandler(btnServerCtrl_MouseLeftButtonDown);
			BroadcastCT_ChangeList_Callback = new BroadcastCT_ChangeList_Function(ChangeList);
			BroadcastCT_ClearList_Callback = new BroadcastCT_ClearList_Function(ClearList);
			Singleton_Socket.broadcastCT_OpenIEventManager.BroadcastCT_ChangeList_Event += BroadcastCT_ChangeList_Callback;
			Singleton_Socket.broadcastCT_CloseIEventManager.BroadcastCT_ClearList_Event += BroadcastCT_ClearList_Callback;
			Task.Factory.StartNew(new Func<bool>(_003CInitEvent_003Eb__b)).ContinueWith(new Action<Task<bool>>(_003CInitEvent_003Eb__8)).ContinueWith(new Action<Task>(_003CInitEvent_003Eb__6), tokenSource.Token);
		}

		private void txtKeyword_KeyUp(object sender, KeyEventArgs e)
		{
			string text = txtKeyword.Text.ToLower().Trim();
			if (!text.Equals(""))
			{
				foreach (BroadcastRow item in Enumerable.OfType<BroadcastRow>(BroadcastRowSP.Children))
				{
					if (item.clientId.Contains(text) || item.clientName.Contains(text) || item.txtLight.Text.Contains(text) || item.txtUserDevice.Text.Contains(text))
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
				foreach (BroadcastRow item2 in Enumerable.OfType<BroadcastRow>(BroadcastRowSP.Children))
				{
					item2.Visibility = Visibility.Visible;
				}
			}
		}

		private void ClearList()
		{
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action(ClearList));
			}
			else
			{
				BroadcastRowSP.Children.Clear();
			}
		}

		private void ChangeServerCtrl(bool Online)
		{
			if (Online)
			{
				txtStatus.Text = "連線中";
				txtStatus.Foreground = ColorTool.HexColorToBrush("#E2F540");
				txtStatus.HorizontalAlignment = HorizontalAlignment.Left;
				btnStatus.Source = new BitmapImage(new Uri("images/btn_broadcast_connected.png", UriKind.Relative));
			}
			else
			{
				txtStatus.Text = "未啟動";
				txtStatus.Foreground = ColorTool.HexColorToBrush("#707A82");
				txtStatus.HorizontalAlignment = HorizontalAlignment.Center;
				btnStatus.Source = new BitmapImage(new Uri("images/btn_broadcast_broken.png", UriKind.Relative));
			}
		}

		private void btnServerCtrl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Action action = null;
			Action action2 = null;
			if (txtStatus.Text.Equals("未啟動"))
			{
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003CbtnServerCtrl_MouseLeftButtonDown_003Eb__16);
				}
				factory.StartNew(action);
			}
			else
			{
				TaskFactory factory2 = Task.Factory;
				if (action2 == null)
				{
					action2 = new Action(_003CbtnServerCtrl_MouseLeftButtonDown_003Eb__19);
				}
				factory2.StartNew(action2);
			}
		}

		public void ChangeList(JArray jArry)
		{
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action<JArray>(ChangeList), jArry);
				return;
			}
			BroadcastRowSP.Children.Clear();
			foreach (JToken item in jArry)
			{
				Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(item.ToString());
				string userID = dictionary["clientId"].ToString();
				string clientName = dictionary["clientName"].ToString();
				string clientType = dictionary["clientType"].ToString();
				string text = dictionary["status"].ToString();
				if (!text.Equals("1"))
				{
					BroadcastRowSP.Children.Add(new BroadcastRow(Socket_FixEmailUserID.FromSocket(userID), clientName, clientType, text, UserID, UserName, MeetingID));
				}
			}
		}

		private void InitUI()
		{
		}

		public bool StopSyncServer(string meetingID)
		{
			bool result = false;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("<?xml version=\"1.0\" encoding=\"UTF-8\"?>").AppendFormat("<Sync>").AppendFormat("<Stop ID=\"{0}\" />", MeetingID)
					.AppendFormat("</Sync>");
				if (PostToSyncServer("/StopSyncServer", stringBuilder.ToString()).Contains("成功"))
				{
					return true;
				}
				return result;
			}
			catch (Exception ex)
			{
				result = false;
				LogTool.Debug(ex);
				return result;
			}
		}

		public bool SyncServerAlreadyStarted(string meetingID)
		{
			bool result = false;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("<?xml version=\"1.0\" encoding=\"UTF-8\"?>").AppendFormat("<MeetingList date=\"{0}\" >", DateTime.Now.ToString("yyyyMMddHHmmss")).AppendFormat("</MeetingList>");
				XDocument xDocument = XDocument.Parse(PostToSyncServer("/GetMeetingList", stringBuilder.ToString()));
				IEnumerable<XElement> source = xDocument.Element("MeetingList").Elements("Meeting");
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate26 == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate26 = new Func<XElement, _003C_003Ef__AnonymousType0<string>>(_003CSyncServerAlreadyStarted_003Eb__25);
				}
				IEnumerable<_003C_003Ef__AnonymousType0<string>> enumerable = Enumerable.Select(source, CS_0024_003C_003E9__CachedAnonymousMethodDelegate26);
				foreach (_003C_003Ef__AnonymousType0<string> item in enumerable)
				{
					if (item.ID.Equals(meetingID))
					{
						return true;
					}
				}
				return result;
			}
			catch (Exception ex)
			{
				result = false;
				LogTool.Debug(ex);
				return result;
			}
		}

		private string PostToSyncServer(string subUrl, string sentXml)
		{
			string result = "";
			try
			{
				string str = SocketTool.GetUrl();
				string url_Imp = SocketTool.GetUrl_Imp();
				if (MeetingID.ToLower().StartsWith("i"))
				{
					str = url_Imp;
				}
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(str + subUrl);
				byte[] bytes = Encoding.UTF8.GetBytes(sentXml);
				httpWebRequest.Method = "POST";
				httpWebRequest.ContentType = "text/xml; encoding='utf-8'";
				httpWebRequest.ContentLength = bytes.Length;
				Stream requestStream = httpWebRequest.GetRequestStream();
				requestStream.Write(bytes, 0, bytes.Length);
				requestStream.Close();
				WebResponse response = httpWebRequest.GetResponse();
				requestStream = response.GetResponseStream();
				StreamReader streamReader = new StreamReader(requestStream);
				result = streamReader.ReadToEnd();
				streamReader.Close();
				requestStream.Close();
				response.Close();
				return result;
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
				return result;
			}
		}

		public bool StartSyncServer(string meetingID)
		{
			bool result = false;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>").Append("<Sync>").AppendFormat("<Start ID=\"{0}\" MaxClient=\"{1}\" />", meetingID, 100)
					.AppendFormat("<Init>{0}</Init>", Settings.Default.InitConfig)
					.Append("</Sync>");
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(PostToSyncServer("/StartSyncServer", stringBuilder.ToString()));
				XmlNode documentElement = xmlDocument.DocumentElement;
				string value = documentElement.SelectSingleNode("/Sync/Start/@IP").Value;
				int num = int.Parse(documentElement.SelectSingleNode("/Sync/Start/@Port").Value);
				if (value.Equals(""))
				{
					return result;
				}
				if (num < 1)
				{
					return result;
				}
				if (num <= 65535)
				{
					return true;
				}
				return result;
			}
			catch (Exception ex)
			{
				result = false;
				LogTool.Debug(ex);
				return result;
			}
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/broadcastct.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
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
				btnServerCtrl = (Grid)target;
				break;
			case 2:
				btnStatus = (Image)target;
				break;
			case 3:
				txtStatus = (TextBlock)target;
				break;
			case 4:
				txtKeyword = (TextBox)target;
				break;
			case 5:
				BroadcastRowSP = (StackPanel)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}

		[CompilerGenerated]
		private void _003CBroadcastCT_Loaded_003Eb__0()
		{
			InitSelectDB();
			base.Dispatcher.BeginInvoke(new Action(_003CBroadcastCT_Loaded_003Eb__1));
		}

		[CompilerGenerated]
		private void _003CBroadcastCT_Loaded_003Eb__1()
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

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__4(object sender, MouseEventArgs e)
		{
			MouseTool.ShowHand();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__5(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__6(Task task)
		{
			while (CanDetectServerState)
			{
				_003C_003Ec__DisplayClassf _003C_003Ec__DisplayClassf = new _003C_003Ec__DisplayClassf();
				_003C_003Ec__DisplayClassf._003C_003E4__this = this;
				_003C_003Ec__DisplayClassf.inList = SyncServerAlreadyStarted(MeetingID);
				base.Dispatcher.BeginInvoke(new Action(_003C_003Ec__DisplayClassf._003CInitEvent_003Eb__7));
				SocketClient instance = Singleton_Socket.GetInstance(MeetingID, UserID, UserName, false);
				instance.getUserList();
				Console.WriteLine("Detect Server Alive => IsCompleted: {0} IsCanceled: {1} IsFaulted: {2}", task.IsCompleted, task.IsCanceled, task.IsFaulted);
				Thread.Sleep(1000 * Settings.Default.DetectSyncServerSeconds);
			}
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__8(Task<bool> task)
		{
			Action action = null;
			_003C_003Ec__DisplayClass12 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass12();
			_003C_003Ec__DisplayClass.task = task;
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			try
			{
				Dispatcher dispatcher = base.Dispatcher;
				if (action == null)
				{
					action = new Action(_003C_003Ec__DisplayClass._003CInitEvent_003Eb__9);
				}
				dispatcher.BeginInvoke(action);
				if (_003C_003Ec__DisplayClass.task.Result)
				{
					try
					{
						_003C_003Ec__DisplayClass14 _003C_003Ec__DisplayClass2 = new _003C_003Ec__DisplayClass14();
						_003C_003Ec__DisplayClass2.CS_0024_003C_003E8__locals13 = _003C_003Ec__DisplayClass;
						_003C_003Ec__DisplayClass2.socketClient = Singleton_Socket.GetInstance(MeetingID, UserID, UserName, false);
						Task.Factory.StartNew(new Action(_003C_003Ec__DisplayClass2._003CInitEvent_003Eb__a));
					}
					catch (Exception ex)
					{
						LogTool.Debug(ex);
					}
				}
			}
			catch (Exception ex2)
			{
				LogTool.Debug(ex2);
			}
		}

		[CompilerGenerated]
		private bool _003CInitEvent_003Eb__b()
		{
			return SyncServerAlreadyStarted(MeetingID);
		}

		[CompilerGenerated]
		private void _003CbtnServerCtrl_MouseLeftButtonDown_003Eb__16()
		{
			try
			{
				_003C_003Ec__DisplayClass1e _003C_003Ec__DisplayClass1e = new _003C_003Ec__DisplayClass1e();
				_003C_003Ec__DisplayClass1e._003C_003E4__this = this;
				_003C_003Ec__DisplayClass1e.success = StartSyncServer(MeetingID);
				base.Dispatcher.BeginInvoke(new Action(_003C_003Ec__DisplayClass1e._003CbtnServerCtrl_MouseLeftButtonDown_003Eb__17));
				if (_003C_003Ec__DisplayClass1e.success)
				{
					AutoClosingMessageBox.Show("啟動成功");
					try
					{
						_003C_003Ec__DisplayClass20 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass20();
						_003C_003Ec__DisplayClass.CS_0024_003C_003E8__locals1f = _003C_003Ec__DisplayClass1e;
						_003C_003Ec__DisplayClass.socketClient = Singleton_Socket.GetInstance(MeetingID, UserID, UserName, false);
						Task.Factory.StartNew(new Action(_003C_003Ec__DisplayClass._003CbtnServerCtrl_MouseLeftButtonDown_003Eb__18));
					}
					catch (Exception ex)
					{
						LogTool.Debug(ex);
					}
				}
				else
				{
					AutoClosingMessageBox.Show("啟動失敗");
				}
			}
			catch (Exception ex2)
			{
				LogTool.Debug(ex2);
			}
		}

		[CompilerGenerated]
		private void _003CbtnServerCtrl_MouseLeftButtonDown_003Eb__19()
		{
			Action action = null;
			try
			{
				_003C_003Ec__DisplayClass23 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass23();
				_003C_003Ec__DisplayClass._003C_003E4__this = this;
				_003C_003Ec__DisplayClass.success = StopSyncServer(MeetingID);
				base.Dispatcher.BeginInvoke(new Action(_003C_003Ec__DisplayClass._003CbtnServerCtrl_MouseLeftButtonDown_003Eb__1a));
				if (_003C_003Ec__DisplayClass.success)
				{
					AutoClosingMessageBox.Show("停止成功");
					Dispatcher dispatcher = base.Dispatcher;
					if (action == null)
					{
						action = new Action(_003CbtnServerCtrl_MouseLeftButtonDown_003Eb__1b);
					}
					dispatcher.BeginInvoke(action);
				}
				else
				{
					AutoClosingMessageBox.Show("停止失敗");
				}
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}

		[CompilerGenerated]
		private void _003CbtnServerCtrl_MouseLeftButtonDown_003Eb__1b()
		{
			BroadcastRowSP.Children.Clear();
		}

		[CompilerGenerated]
		private static _003C_003Ef__AnonymousType0<string> _003CSyncServerAlreadyStarted_003Eb__25(XElement x)
		{
			return new _003C_003Ef__AnonymousType0<string>(x.Attribute("ID").Value.Trim());
		}
	}
}
