using PaperLess_Emeeting.Properties;

public class WsTool
{
	public static string GetUrl()
	{
		if (!Settings.Default.IsDebugMode)
		{
			return Settings.Default.PaperLess_Emeeting_WS_MeetingService;
		}
		return Settings.Default.PaperLess_Emeeting_WS_MeetingService_Debug;
	}

	public static string GetAbstractSyncCenter_BASE_URL()
	{
		if (Settings.Default.IsDebugMode)
		{
			return Settings.Default.AbstractSyncCenter_BASE_URL_Debug;
		}
		return Settings.Default.AbstractSyncCenter_BASE_URL;
	}

	public static string GetSyncServer_URL()
	{
		if (Settings.Default.IsDebugMode)
		{
			return Settings.Default.SyncServerUrl_Debug;
		}
		return Settings.Default.SyncServerUrl;
	}

	public static string GetSyncServerImp_URL()
	{
		if (Settings.Default.IsDebugMode)
		{
			return Settings.Default.SyncServerUrl_Debug;
		}
		return Settings.Default.SyncServerUrl_Imp;
	}
}
