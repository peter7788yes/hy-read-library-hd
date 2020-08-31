using Newtonsoft.Json;
using NLog;
using PaperlessSync.Broadcast.Socket;
using System.Collections.Generic;

namespace PaperlessSync.Broadcast.Service
{
	public class EventManagerSample : IEventManager
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		private string _managerId;

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

		public void run()
		{
			Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(msg);
			long argument = (long)SocketClient.GetCurrentTimeInUnixMillis() - (long)dictionary["sendTime"];
			logger.Debug("EventManagerSample[{0}] recive message->{1}", _managerId, msg);
			logger.Debug("EventManagerSample[{0}] recive msg form {1} time = {2} ms", _managerId, clientId, argument);
		}
	}
}
