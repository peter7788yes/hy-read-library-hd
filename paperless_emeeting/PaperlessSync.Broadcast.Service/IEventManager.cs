namespace PaperlessSync.Broadcast.Service
{
	public interface IEventManager
	{
		string managerId
		{
			get;
			set;
		}

		string msg
		{
			get;
			set;
		}

		string clientId
		{
			get;
			set;
		}

		void run();
	}
}
