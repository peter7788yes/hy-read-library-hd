using PaperLess_ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

public class GetSeriesData
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass1
	{
		public Action<SeriesData> DoAction;

		public Action ErrorAction;

		public void _003CAsyncPOST_003Eb__0(HttpWebResponse response)
		{
			SeriesData obj = null;
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
					obj = XmlHelper.XmlDeserialize<SeriesData>(text, Encoding.UTF8);
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

	public static SeriesData POST(string UserID)
	{
		SeriesData result = null;
		try
		{
			string url = WsTool.GetUrl();
			string format = "<?xml version=\"1.0\"?><UserInfo><UserID>{0}</UserID></UserInfo>";
			format = string.Format(format, UserID);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["XmlDoc"] = format;
			string text = HttpTool.CreateRequest(url + "/SeriesData", "POST", dictionary);
			if (text.Equals(""))
			{
				return result;
			}
			result = XmlHelper.XmlDeserialize<SeriesData>(text, Encoding.UTF8);
			return result;
		}
		catch (Exception ex)
		{
			LogTool.Debug(ex);
			return result;
		}
	}

	public static void AsyncPOST(string UserID, Action<SeriesData> DoAction, Action ErrorAction = null)
	{
		_003C_003Ec__DisplayClass1 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass1();
		_003C_003Ec__DisplayClass.DoAction = DoAction;
		_003C_003Ec__DisplayClass.ErrorAction = ErrorAction;
		string url = WsTool.GetUrl();
		string format = "<?xml version=\"1.0\"?><UserInfo><UserID>{0}</UserID></UserInfo>";
		format = string.Format(format, UserID);
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary["XmlDoc"] = format;
		HttpWebRequest httpWebRequest = HttpTool.GetHttpWebRequest(url + "/SeriesData", "POST", dictionary);
		HttpTool.DoWithResponse(httpWebRequest, new Action<HttpWebResponse>(_003C_003Ec__DisplayClass._003CAsyncPOST_003Eb__0));
	}
}
