using PaperLess_ViewModel;
using System;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

public class GetLawData
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass1
	{
		public Action<LawData> DoAction;

		public Action ErrorAction;

		public void _003CAsyncPOST_003Eb__0(HttpWebResponse response)
		{
			LawData obj = null;
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
					obj = XmlHelper.XmlDeserialize<LawData>(text, Encoding.UTF8);
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

	public static LawData POST()
	{
		LawData result = null;
		try
		{
			string url = WsTool.GetUrl();
			string text = HttpTool.CreateRequest(url + "/LawData", "POST");
			if (text.Equals(""))
			{
				return result;
			}
			result = XmlHelper.XmlDeserialize<LawData>(text, Encoding.UTF8);
			return result;
		}
		catch (Exception ex)
		{
			LogTool.Debug(ex);
			return result;
		}
	}

	public static void AsyncPOST(Action<LawData> DoAction, Action ErrorAction = null)
	{
		_003C_003Ec__DisplayClass1 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass1();
		_003C_003Ec__DisplayClass.DoAction = DoAction;
		_003C_003Ec__DisplayClass.ErrorAction = ErrorAction;
		string url = WsTool.GetUrl();
		HttpWebRequest httpWebRequest = HttpTool.GetHttpWebRequest(url + "/LawData", "POST");
		HttpTool.DoWithResponse(httpWebRequest, new Action<HttpWebResponse>(_003C_003Ec__DisplayClass._003CAsyncPOST_003Eb__0));
	}
}
