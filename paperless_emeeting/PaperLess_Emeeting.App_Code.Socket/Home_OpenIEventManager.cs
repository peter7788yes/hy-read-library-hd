using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PaperLess_Emeeting.Properties;
using PaperlessSync.Broadcast.Service;
using PaperlessSync.Broadcast.Socket;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace PaperLess_Emeeting.App_Code.Socket
{
	public class Home_OpenIEventManager : IEventManager
	{
		private Dictionary<string, string> dictCache = new Dictionary<string, string>();

		private string _managerId = typeof(BroadcastCT_OpenIEventManager).Name;

		private string _msg;

		private string _clientId;

		public string managerId
		{
			get
			{
				return _managerId;
			}
			set
			{
				_managerId = value;
			}
		}

		public string msg
		{
			get
			{
				return _msg;
			}
			set
			{
				_msg = value;
			}
		}

		public string clientId
		{
			get
			{
				return _clientId;
			}
			set
			{
				_clientId = value;
			}
		}

		public event Home_OpenBook_Function Home_OpenBook_Event;

		public event Home_IsInSync_And_IsSyncOwner_Function Home_IsInSync_And_IsSyncOwner_Event;

		public event Home_CloseAllWindow_Function Home_CloseAllWindow_Event;

		public Home_OpenIEventManager()
		{
			_managerId = GetType().Name;
		}

		public void run()
		{
			Action action = null;
			try
			{
				string msg = this.msg;
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				string text = "";
				if (this.msg != null)
				{
					try
					{
						Console.WriteLine("Home_OpenIEventManager: " + msg);
						dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(msg);
						long num = (long)SocketClient.GetCurrentTimeInUnixMillis() - (long)dictionary["sendTime"];
						LogTool.Debug(string.Format("EventManagerHome[{0}] recive message->{1}", _managerId, msg));
						LogTool.Debug(string.Format("EventManagerHome[{0}] recive msg form {1} time = {2} ms", _managerId, clientId, num));
					}
					catch (Exception ex)
					{
						LogTool.Debug(ex);
					}
					try
					{
						text = dictionary["cmd"].ToString();
					}
					catch (Exception ex2)
					{
						LogTool.Debug(ex2);
					}
					switch (text)
					{
					case "userList":
					{
						string value = dictionary["msg"].ToString();
						JArray jArry = JsonConvert.DeserializeObject<JArray>(value);
						if (this.Home_IsInSync_And_IsSyncOwner_Event != null)
						{
							this.Home_IsInSync_And_IsSyncOwner_Event(jArry);
						}
						break;
					}
					case "broadcast":
					{
						string json = dictionary["msg"].ToString();
						JObject jObject2 = JObject.Parse(json);
						switch (jObject2["cmd"].ToString().ToUpper())
						{
						case "R.OB":
						{
							string text4 = jObject2["bookId"].ToString();
							if (text4 != null && !text4.Equals(""))
							{
								if (this.Home_CloseAllWindow_Event != null)
								{
									this.Home_CloseAllWindow_Event("主控者開啟檔案", false);
								}
								if (this.Home_OpenBook_Event != null)
								{
									this.Home_OpenBook_Event(text4, "");
								}
							}
							break;
						}
						case "R.CB":
							if (this.Home_CloseAllWindow_Event != null)
							{
								this.Home_CloseAllWindow_Event("主控者點選離開", false);
							}
							break;
						}
						break;
					}
					case "R.init":
					{
						string text2 = dictionary["msg"].ToString();
						if (text2.Contains("bookId"))
						{
							JObject jObject = JObject.Parse(text2);
							string text3 = jObject["bookId"].ToString();
							lock (this)
							{
								if (!dictCache.ContainsKey(text3))
								{
									dictCache[text3] = msg;
									TaskFactory factory = Task.Factory;
									if (action == null)
									{
										action = new Action(_003Crun_003Eb__1);
									}
									factory.StartNew(action);
									if (this.Home_CloseAllWindow_Event != null)
									{
										this.Home_CloseAllWindow_Event(string.Format("加入進行中{0}", Settings.Default.CourseOrMeeting_String), true);
									}
									if (this.Home_OpenBook_Event != null)
									{
										this.Home_OpenBook_Event(text3, msg);
									}
								}
							}
						}
						break;
					}
					}
				}
			}
			catch (Exception ex3)
			{
				LogTool.Debug(ex3);
			}
		}

		[CompilerGenerated]
		private void _003Crun_003Eb__1()
		{
			Thread.Sleep(5000);
			dictCache.Clear();
		}
	}
}
