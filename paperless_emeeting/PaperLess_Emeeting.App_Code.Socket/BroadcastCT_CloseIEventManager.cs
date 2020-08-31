using PaperlessSync.Broadcast.Service;
using System;

namespace PaperLess_Emeeting.App_Code.Socket
{
	public class BroadcastCT_CloseIEventManager : IEventManager
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

		public event BroadcastCT_ClearList_Function BroadcastCT_ClearList_Event;

		public BroadcastCT_CloseIEventManager()
		{
			_managerId = GetType().Name;
		}

		public void run()
		{
			try
			{
				if (this.BroadcastCT_ClearList_Event != null)
				{
					this.BroadcastCT_ClearList_Event();
				}
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}
	}
}
