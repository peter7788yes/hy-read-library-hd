using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

public class GetNewMeeting
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass2
	{
		public Action<string> DoAction;

		public Action ErrorAction;

		public void _003CAsyncPOST_003Eb__0(HttpWebResponse response)
		{
			string obj = "";
			try
			{
				using (Stream stream = response.GetResponseStream())
				{
					using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
					{
						obj = streamReader.ReadToEnd();
					}
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

	public static MeetingData POST(string UserID, string PinCode)
	{
		MeetingData result = null;
		try
		{
			string url = WsTool.GetUrl();
			string format = "<?xml version=\"1.0\"?><UserInfo><UserID><![CDATA[{0}]]></UserID><PinCode>{1}</PinCode></UserInfo>";
			format = string.Format(format, UserID, PinCode);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["XmlDoc"] = format;
			string text = HttpTool.CreateRequest(url + "/NewMeeting", "POST", dictionary);
			if (text.Equals(""))
			{
				return result;
			}
			result = XmlHelper.XmlDeserialize<MeetingData>(text, Encoding.UTF8);
			return result;
		}
		catch (Exception ex)
		{
			LogTool.Debug(ex);
			return result;
		}
	}

	public static void AsyncPOST(string UserID, string PinCode, Action<string> DoAction, Action ErrorAction = null)
	{
		Action<HttpWebResponse> action = null;
		_003C_003Ec__DisplayClass2 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass2();
		_003C_003Ec__DisplayClass.DoAction = DoAction;
		_003C_003Ec__DisplayClass.ErrorAction = ErrorAction;
		try
		{
			string url = WsTool.GetUrl();
			string format = "<?xml version=\"1.0\"?><UserInfo><UserID><![CDATA[{0}]]></UserID><PinCode>{1}</PinCode></UserInfo>";
			format = string.Format(format, UserID, PinCode);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["XmlDoc"] = format;
			HttpWebRequest httpWebRequest = HttpTool.GetHttpWebRequest(url + "/NewMeeting", "POST", dictionary);
			if (action == null)
			{
				action = new Action<HttpWebResponse>(_003C_003Ec__DisplayClass._003CAsyncPOST_003Eb__0);
			}
			HttpTool.DoWithResponse(httpWebRequest, action);
		}
		catch (Exception ex)
		{
			LogTool.Debug(ex);
		}
	}
}
