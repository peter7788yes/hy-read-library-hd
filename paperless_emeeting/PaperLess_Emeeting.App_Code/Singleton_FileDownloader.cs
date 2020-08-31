using PaperLess_Emeeting.App_Code.WS;
using System.Collections.Generic;

namespace PaperLess_Emeeting.App_Code
{
	public class Singleton_FileDownloader
	{
		private static readonly object thisLock = new object();

		private static Dictionary<string, FileDownloader> dcit_instance = new Dictionary<string, FileDownloader>();

		public static Home_UnZipError_Function Home_UnZipError_Callback;

		private Singleton_FileDownloader()
		{
		}

		public static Dictionary<string, FileDownloader> GetInstance()
		{
			return dcit_instance;
		}

		public static FileDownloader GetInstance(string KeyOfMeetingID_GetFileDownloader)
		{
			FileDownloader value;
			if (!dcit_instance.TryGetValue(KeyOfMeetingID_GetFileDownloader, out value))
			{
				lock (thisLock)
				{
					if (!dcit_instance.TryGetValue(KeyOfMeetingID_GetFileDownloader, out value))
					{
						value = new FileDownloader();
						dcit_instance[KeyOfMeetingID_GetFileDownloader] = value;
					}
					else
					{
						value = dcit_instance[KeyOfMeetingID_GetFileDownloader];
					}
				}
			}
			else
			{
				value = dcit_instance[KeyOfMeetingID_GetFileDownloader];
			}
			value.ClearHomeEvent();
			value.Home_UnZipError_Event += Home_UnZipError_Callback;
			return value;
		}
	}
}
