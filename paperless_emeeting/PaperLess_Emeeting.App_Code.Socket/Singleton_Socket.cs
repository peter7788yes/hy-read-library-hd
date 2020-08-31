using PaperlessSync.Broadcast.Service;
using PaperlessSync.Broadcast.Socket;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace PaperLess_Emeeting.App_Code.Socket
{
	public class Singleton_Socket
	{
		private static readonly object thisLock = new object();

		private static volatile SocketClient instance;

		public int test = 1;

		public static IEventManager ReaderEvent = null;

		public static List<IEventManager> OpenEventList = new List<IEventManager>();

		public static List<IEventManager> CloseEventList = new List<IEventManager>();

		public static BroadcastCT_OpenIEventManager broadcastCT_OpenIEventManager = new BroadcastCT_OpenIEventManager();

		public static BroadcastCT_CloseIEventManager broadcastCT_CloseIEventManager = new BroadcastCT_CloseIEventManager();

		public static Home_OpenIEventManager home_OpenIEventManager = new Home_OpenIEventManager();

		public static Home_CloseIEventManager home_CloseIEventManager = new Home_CloseIEventManager();

		public static MVWindow_OpenIEventManager mvWindow_OpenIEventManager = new MVWindow_OpenIEventManager();

		[CompilerGenerated]
		private static string _003CMeetingID_003Ek__BackingField;

		[CompilerGenerated]
		private static string _003CUserID_003Ek__BackingField;

		[CompilerGenerated]
		private static string _003CUserName_003Ek__BackingField;

		[CompilerGenerated]
		private static Thread _003Cthread_003Ek__BackingField;

		[CompilerGenerated]
		private static ThreadStart CS_0024_003C_003E9__CachedAnonymousMethodDelegate3;

		public static string MeetingID
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

		public static string UserID
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

		public static string UserName
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

		public static Thread thread
		{
			[CompilerGenerated]
			get
			{
				return _003Cthread_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003Cthread_003Ek__BackingField = value;
			}
		}

		private Singleton_Socket()
		{
		}

		public static void Init()
		{
			OpenEventList.Add(broadcastCT_OpenIEventManager);
			CloseEventList.Add(broadcastCT_CloseIEventManager);
			OpenEventList.Add(home_OpenIEventManager);
			CloseEventList.Add(home_CloseIEventManager);
			OpenEventList.Add(mvWindow_OpenIEventManager);
		}

		public static SocketClient GetInstance()
		{
			return instance;
		}

		public static SocketClient GetInstance(string _MeetingID, string _UserID, string _UserName, bool InitToSync)
		{
			if (NetworkTool.GetDomainNameIP(WsTool.GetSyncServer_URL(), 1000).Equals("") && NetworkTool.GetDomainNameIP(WsTool.GetSyncServerImp_URL(), 1000).Equals(""))
			{
				return null;
			}
			if (instance != null)
			{
				lock (thisLock)
				{
					if (instance == null)
					{
						Init_Instance(_MeetingID, _UserID, _UserName, InitToSync);
					}
					else if (!MeetingID.Equals(_MeetingID))
					{
						Init_Instance(_MeetingID, _UserID, _UserName, InitToSync);
					}
					else
					{
						for (int i = 1; i <= 10; i++)
						{
							bool isConnected = instance.GetIsConnected();
							if (instance != null && isConnected)
							{
								break;
							}
							if (i == 10)
							{
								Init_Instance(_MeetingID, _UserID, _UserName, InitToSync);
							}
							Thread.Sleep(10);
						}
					}
				}
			}
			else
			{
				lock (thisLock)
				{
					if (instance == null)
					{
						Init_Instance(_MeetingID, _UserID, _UserName, InitToSync);
					}
					else if (!MeetingID.Equals(_MeetingID))
					{
						Init_Instance(_MeetingID, _UserID, _UserName, InitToSync);
					}
					else
					{
						for (int j = 1; j <= 10; j++)
						{
							bool isConnected2 = instance.GetIsConnected();
							if (instance != null && isConnected2)
							{
								break;
							}
							if (j == 10)
							{
								Init_Instance(_MeetingID, _UserID, _UserName, InitToSync);
							}
							Thread.Sleep(10);
						}
					}
				}
			}
			return instance;
		}

		private static void Init_Instance(string _MeetingID, string _UserID, string _UserName, bool InitToSync)
		{
			UserName = _UserName;
			MeetingID = _MeetingID;
			_UserID = Socket_FixEmailUserID.ToSocket(_UserID);
			UserID = _UserID;
			ClearInstance();
			string str = SocketTool.GetUrl();
			string url_Imp = SocketTool.GetUrl_Imp();
			if (_MeetingID.ToLower().StartsWith("i"))
			{
				str = url_Imp;
			}
			instance = new SocketClient(str + "/JoinSyncServer", _MeetingID, 100, _UserID, _UserName, InitToSync, 2, "12");
			if (ReaderEvent != null)
			{
				instance.AddEventManager(ReaderEvent);
			}
			instance.AddEventManager(OpenEventList);
			instance.AddCloseEventManager(CloseEventList);
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate3 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate3 = new ThreadStart(_003CInit_Instance_003Eb__2);
			}
			thread = new Thread(CS_0024_003C_003E9__CachedAnonymousMethodDelegate3);
			thread.IsBackground = false;
			thread.Start();
			for (int i = 0; i < 10; i++)
			{
				if (instance != null && instance.GetIsConnected())
				{
					return;
				}
				Thread.Sleep(10);
			}
			if (instance == null || !instance.GetIsConnected())
			{
				instance = null;
			}
		}

		public static void ClearInstance()
		{
			try
			{
				if (thread != null)
				{
					thread.Abort();
				}
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
			Thread.Sleep(10);
			thread = null;
			instance = null;
		}

		[CompilerGenerated]
		private static void _003CInit_Instance_003Eb__2()
		{
			try
			{
				if (instance != null)
				{
					instance.run();
				}
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}
	}
}
