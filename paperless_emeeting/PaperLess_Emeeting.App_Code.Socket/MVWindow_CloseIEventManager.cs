using PaperlessSync.Broadcast.Service;

namespace PaperLess_Emeeting.App_Code.Socket
{
	public class MVWindow_CloseIEventManager : IEventManager
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

		public MVWindow_CloseIEventManager()
		{
			_managerId = GetType().Name;
		}

		public void run()
		{
		}
	}
}
