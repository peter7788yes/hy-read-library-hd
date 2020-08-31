using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

public class GetProgressUpload
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass1
	{
		public Action<bool> DoAction;

		public Action ErrorAction;

		public void _003CAsyncPOST_003Eb__0(HttpWebResponse response)
		{
			bool obj = false;
			try
			{
				string text = "";
				using (Stream stream = response.GetResponseStream())
				{
					using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
					{
						text = streamReader.ReadToEnd();
					}
				}
				if (text.Contains("</succes>"))
				{
					obj = true;
				}
			}
			catch (Exception ex)
			{
				if (ErrorAction != null)
				{
					ErrorAction();
				}
				LogTool.Debug(ex);
			}
			DoAction(obj);
		}
	}

	public static bool POST(string MeetingID, string UserID, string AgendaID, string AgendaStatus)
	{
		bool result = false;
		try
		{
			string url = WsTool.GetUrl();
			string format = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><UserInfo><MeetingID>{0}</MeetingID><UserID><![CDATA[{1}]]></UserID><UpdateList><UpdateAgenda><AgendaID>{2}</AgendaID><AgendaStatus>{3}</AgendaStatus></UpdateAgenda></UpdateList></UserInfo>";
			format = string.Format(format, MeetingID, UserID, AgendaID, AgendaStatus);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["XmlDoc"] = format;
			string text = HttpTool.CreateRequest(url + "/ProgressUpload", "POST", dictionary);
			if (!text.Contains("</succes>"))
			{
				return result;
			}
			result = true;
			return result;
		}
		catch (Exception ex)
		{
			LogTool.Debug(ex);
			return result;
		}
	}

	public static void AsyncPOST(string MeetingID, string UserID, string AgendaID, string AgendaStatus, Action<bool> DoAction, Action ErrorAction = null)
	{
		_003C_003Ec__DisplayClass1 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass1();
		_003C_003Ec__DisplayClass.DoAction = DoAction;
		_003C_003Ec__DisplayClass.ErrorAction = ErrorAction;
		string url = WsTool.GetUrl();
		string format = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><UserInfo><MeetingID>{0}</MeetingID><UserID><![CDATA[{1}]]></UserID><UpdateList><UpdateAgenda><AgendaID>{2}</AgendaID><AgendaStatus>{3}</AgendaStatus></UpdateAgenda></UpdateList></UserInfo>";
		format = string.Format(format, MeetingID, UserID, AgendaID, AgendaStatus);
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary["XmlDoc"] = format;
		HttpWebRequest httpWebRequest = HttpTool.GetHttpWebRequest(url + "/ProgressUpload", "POST", dictionary);
		HttpTool.DoWithResponse(httpWebRequest, new Action<HttpWebResponse>(_003C_003Ec__DisplayClass._003CAsyncPOST_003Eb__0));
	}
}
