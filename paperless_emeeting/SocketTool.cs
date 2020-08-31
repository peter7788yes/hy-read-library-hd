using PaperLess_Emeeting.Properties;

public class SocketTool
{
	public static string GetUrl()
	{
		if (!Settings.Default.IsDebugMode)
		{
			return Settings.Default.SyncServerUrl;
		}
		return Settings.Default.SyncServerUrl_Debug;
	}

	public static string GetUrl_Imp()
	{
		if (!Settings.Default.IsDebugMode)
		{
			return Settings.Default.SyncServerUrl_Imp;
		}
		return Settings.Default.SyncServerUrl_Debug;
	}
}
