using PaperLess_Emeeting.App_Code.MessageBox;
using PaperLess_ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

public class GetUserData
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass4
	{
		public DateTime date;

		public Action<User, DateTime> DoAction;

		public Action ErrorAction;

		public void _003CAsyncPOST_003Eb__0(HttpWebResponse response)
		{
			User arg = null;
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
					arg = XmlHelper.XmlDeserialize<User>(text, Encoding.UTF8);
				}
				DoAction(arg, date);
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

	public static User POST(string UserID, string UserPWD, string UserDateBegin, string UserDateEnd)
	{
		User result = null;
		try
		{
			string url = WsTool.GetUrl();
			string format = "<?xml version=\"1.0\"?><UserInfo><UserID><![CDATA[{0}]]></UserID><UserPW><![CDATA[{1}]]></UserPW><UserDevice>1</UserDevice><UserDateBegin>{2}</UserDateBegin><UserDateEnd>{3}</UserDateEnd></UserInfo>";
			format = string.Format(format, UserID, UserPWD, UserDateBegin, UserDateEnd);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["XmlDoc"] = format;
			string text = HttpTool.CreateRequest(url + "/UserData", "POST", dictionary);
			if (text.Equals(""))
			{
				return result;
			}
			result = XmlHelper.XmlDeserialize<User>(text, Encoding.UTF8);
			return result;
		}
		catch (Exception ex)
		{
			LogTool.Debug(ex);
			return result;
		}
	}

	public static void AsyncPOST(string UserID, string UserPWD, DateTime date, Action<User, DateTime> DoAction, Action ErrorAction = null)
	{
		Action<HttpWebResponse> action = null;
		Action action2 = null;
		_003C_003Ec__DisplayClass4 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass4();
		_003C_003Ec__DisplayClass.date = date;
		_003C_003Ec__DisplayClass.DoAction = DoAction;
		_003C_003Ec__DisplayClass.ErrorAction = ErrorAction;
		try
		{
			string url = WsTool.GetUrl();
			string format = "<?xml version=\"1.0\"?><UserInfo><UserID><![CDATA[{0}]]></UserID><UserPW><![CDATA[{1}]]></UserPW><UserDevice>1</UserDevice><UserDateBegin>{2}</UserDateBegin><UserDateEnd>{3}</UserDateEnd></UserInfo>";
			format = string.Format(format, UserID, UserPWD, DateTool.MonthFirstDate(_003C_003Ec__DisplayClass.date).ToString("yyyyMMdd"), DateTool.MonthLastDate(_003C_003Ec__DisplayClass.date).ToString("yyyyMMdd"));
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["XmlDoc"] = format;
			HttpWebRequest httpWebRequest = HttpTool.GetHttpWebRequest(url + "/UserData", "POST", dictionary);
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
