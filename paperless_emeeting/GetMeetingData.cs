using PaperLess_Emeeting.App_Code.MessageBox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

public class GetMeetingData
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass4
	{
		public Action<MeetingData> DoAction;

		public Action ErrorAction;

		public void _003CAsyncPOST_003Eb__0(HttpWebResponse response)
		{
			MeetingData obj = null;
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
					obj = XmlHelper.XmlDeserialize<MeetingData>(text, Encoding.UTF8);
				}
				DoAction(obj);
			}
			catch (Exception ex)
			{
				if (ErrorAction != null)
				{
					ErrorAction();
				}
				LogTool.Debug(ex);
			}
		}

		public void _003CAsyncPOST_003Eb__1()
		{
			ErrorAction();
		}
	}

	public static MeetingData POST(string MeetingID, string UserID, string UserPWD)
	{
		MeetingData result = null;
		try
		{
			string url = WsTool.GetUrl();
			string format = "<?xml version=\"1.0\"?><UserInfo><MeetingID>{0}</MeetingID><UserID><![CDATA[{1}]]></UserID><UserPW><![CDATA[{2}]]></UserPW></UserInfo>";
			format = string.Format(format, MeetingID, UserID, UserPWD);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["XmlDoc"] = format;
			string text = HttpTool.CreateRequest(url + "/MeetingData", "POST", dictionary);
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

	public static void AsyncPOST(string MeetingID, string UserID, string UserPWD, Action<MeetingData> DoAction, Action ErrorAction = null)
	{
		Action<HttpWebResponse> action = null;
		Action action2 = null;
		_003C_003Ec__DisplayClass4 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass4();
		_003C_003Ec__DisplayClass.DoAction = DoAction;
		_003C_003Ec__DisplayClass.ErrorAction = ErrorAction;
		try
		{
			string url = WsTool.GetUrl();
			string format = "<?xml version=\"1.0\"?><UserInfo><MeetingID>{0}</MeetingID><UserID><![CDATA[{1}]]></UserID><UserPW><![CDATA[{2}]]></UserPW></UserInfo>";
			format = string.Format(format, MeetingID, UserID, UserPWD);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["XmlDoc"] = format;
			HttpWebRequest httpWebRequest = HttpTool.GetHttpWebRequest(url + "/MeetingData", "POST", dictionary);
			if (action == null)
			{
				action = new Action<HttpWebResponse>(_003C_003Ec__DisplayClass._003CAsyncPOST_003Eb__0);
			}
			Action<HttpWebResponse> responseAction = action;
			if (action2 == null)
			{
				action2 = new Action(_003C_003Ec__DisplayClass._003CAsyncPOST_003Eb__1);
			}
			HttpTool.DoWithResponse(httpWebRequest, responseAction, action2);
		}
		catch (Exception ex)
		{
			if (_003C_003Ec__DisplayClass.ErrorAction != null)
			{
				_003C_003Ec__DisplayClass.ErrorAction();
			}
			AutoClosingMessageBox.Show(ex.Message);
			LogTool.Debug(ex);
		}
	}
}
