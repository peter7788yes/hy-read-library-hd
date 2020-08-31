using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

public class GetSigninData
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass1
	{
		public Action<SigninData> DoAction;

		public Action ErrorAction;

		public void _003CAsyncPOST_003Eb__0(HttpWebResponse response)
		{
			SigninData obj = null;
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
				if (!text.Equals(""))
				{
					obj = XmlHelper.XmlDeserialize<SigninData>(text, Encoding.UTF8);
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

	public static SigninData POST(string meetingID)
	{
		SigninData result = null;
		try
		{
			string url = WsTool.GetUrl();
			string format = "<?xml version=\"1.0\"?><UserInfo><MeetingID>{0}</MeetingID></UserInfo>";
			format = string.Format(format, meetingID);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["XmlDoc"] = format;
			string text = HttpTool.CreateRequest(url + "/SigninData", "POST", dictionary);
			if (text.Equals(""))
			{
				return result;
			}
			result = XmlHelper.XmlDeserialize<SigninData>(text, Encoding.UTF8);
			return result;
		}
		catch (Exception ex)
		{
			LogTool.Debug(ex);
			return result;
		}
	}

	public static void AsyncPOST(string meetingID, Action<SigninData> DoAction, Action ErrorAction = null)
	{
		_003C_003Ec__DisplayClass1 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass1();
		_003C_003Ec__DisplayClass.DoAction = DoAction;
		_003C_003Ec__DisplayClass.ErrorAction = ErrorAction;
		string url = WsTool.GetUrl();
		string format = "<?xml version=\"1.0\"?><UserInfo><MeetingID>{0}</MeetingID></UserInfo>";
		format = string.Format(format, meetingID);
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary["XmlDoc"] = format;
		HttpWebRequest httpWebRequest = HttpTool.GetHttpWebRequest(url + "/SigninData", "POST", dictionary);
		HttpTool.DoWithResponse(httpWebRequest, new Action<HttpWebResponse>(_003C_003Ec__DisplayClass._003CAsyncPOST_003Eb__0));
	}
}
