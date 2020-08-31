using PaperlessSync.Broadcast.Service;
using System;

namespace PaperLess_Emeeting.App_Code.Socket
{
	public class Home_CloseIEventManager : IEventManager
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

		public event Home_CloseAllWindow_Function Home_CloseAllWindow_Event;

		public event Home_TurnOffSyncButton_Function Home_TurnOffSyncButton_Event;

		public event Home_SetSocketClientNull_Function Home_SetSocketClientNull_Event;

		public Home_CloseIEventManager()
		{
			_managerId = GetType().Name;
		}

		public void run()
		{
			try
			{
				if (this.Home_CloseAllWindow_Event != null)
				{
					this.Home_CloseAllWindow_Event("", false);
				}
				if (this.Home_TurnOffSyncButton_Event != null)
				{
					this.Home_TurnOffSyncButton_Event();
				}
				if (this.Home_SetSocketClientNull_Event != null)
				{
					this.Home_SetSocketClientNull_Event();
				}
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}
	}
}
