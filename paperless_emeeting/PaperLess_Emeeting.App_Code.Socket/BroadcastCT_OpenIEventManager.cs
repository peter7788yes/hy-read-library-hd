using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PaperlessSync.Broadcast.Service;
using PaperlessSync.Broadcast.Socket;
using System;
using System.Collections.Generic;

namespace PaperLess_Emeeting.App_Code.Socket
{
	public class BroadcastCT_OpenIEventManager : IEventManager
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

		public event BroadcastCT_ChangeList_Function BroadcastCT_ChangeList_Event;

		public BroadcastCT_OpenIEventManager()
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
					LogTool.Debug(string.Format("EventManagerBroadcastCT[{0}] recive message->{1}", _managerId, msg));
					LogTool.Debug(string.Format("EventManagerBroadcastCT[{0}] recive msg form {1} time = {2} ms", _managerId, clientId, num));
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
				string a;
				if ((a = text) != null && a == "userList")
				{
					string value = dictionary["msg"].ToString();
					JArray jArry = JsonConvert.DeserializeObject<JArray>(value);
					if (this.BroadcastCT_ChangeList_Event != null)
					{
						this.BroadcastCT_ChangeList_Event(jArry);
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
