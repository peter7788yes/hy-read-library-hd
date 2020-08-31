using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PaperlessSync.Broadcast.Service;
using PaperlessSync.Broadcast.Socket;
using System;
using System.Collections.Generic;

namespace PaperLess_Emeeting.App_Code.Socket
{
	public class MVWindow_OpenIEventManager : IEventManager
	{
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

		public event MVWindow_IsInSync_And_IsSyncOwner_Function MVWindow_IsInSync_And_IsSyncOwner_Event;

		public event MVWindow_MVAction_Function MVWindow_MVAction_Event;

		public MVWindow_OpenIEventManager()
		{
			_managerId = GetType().Name;
		}

		public void run()
		{
			try
			{
				string msg = this.msg;
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				string text = "";
				try
				{
					Console.WriteLine("Home_OpenIEventManager: " + msg);
					dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(msg);
					long num = (long)SocketClient.GetCurrentTimeInUnixMillis() - (long)dictionary["sendTime"];
					LogTool.Debug(string.Format("EventManagerMVWindow[{0}] recive message->{1}", _managerId, msg));
					LogTool.Debug(string.Format("EventManagerMVWindow[{0}] recive msg form {1} time = {2} ms", _managerId, clientId, num));
				}
				catch (Exception)
				{
				}
				try
				{
					text = dictionary["cmd"].ToString();
				}
				catch (Exception)
				{
				}
				switch (text)
				{
				case "userList":
				{
					string value = dictionary["msg"].ToString();
					JArray jArry = JsonConvert.DeserializeObject<JArray>(value);
					if (this.MVWindow_IsInSync_And_IsSyncOwner_Event != null)
					{
						this.MVWindow_IsInSync_And_IsSyncOwner_Event(jArry);
					}
					break;
				}
				case "broadcast":
				{
					string json = dictionary["msg"].ToString();
					JObject jObject = JObject.Parse(json);
					string text2 = jObject["cmd"].ToString().ToUpper();
					string a;
					if ((a = text2) != null && a == "R.SV" && this.MVWindow_MVAction_Event != null)
					{
						this.MVWindow_MVAction_Event(jObject);
					}
					break;
				}
				}
			}
			catch (Exception ex3)
			{
				LogTool.Debug(ex3);
			}
		}
	}
}
