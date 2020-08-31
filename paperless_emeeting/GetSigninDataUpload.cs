using PaperLess_ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

public class GetSigninDataUpload
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass2
	{
		public Action<SigninDataUpload> DoAction;

		public Action ErrorAction;

		public void _003CAsyncPOST_003Eb__1(HttpWebResponse response)
		{
			SigninDataUpload obj = null;
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
					obj = XmlHelper.XmlDeserialize<SigninDataUpload>(text, Encoding.UTF8);
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

	public static SigninDataUpload POST(string MeetingID, string UserID, string DeptId, string filePath)
	{
		SigninDataUpload result = null;
		try
		{
			string url = WsTool.GetUrl();
			string format = "<?xml version=\"1.0\"?><UserInfo><MeetingID>{0}</MeetingID><UserID><![CDATA[{1}]]></UserID><DeptId><![CDATA[{2}]]></DeptId></UserInfo>";
			format = string.Format(format, MeetingID, UserID, DeptId);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["XmlDoc"] = format;
			string text = HttpTool.CreateRequestWithFile(url + "/SigninDataUpload", "", filePath);
			if (text.Equals(""))
			{
				return result;
			}
			result = XmlHelper.XmlDeserialize<SigninDataUpload>(text, Encoding.UTF8);
			return result;
		}
		catch (Exception ex)
		{
			LogTool.Debug(ex);
			return result;
		}
	}

	public static void AsyncPOST(string MeetingID, string UserID, string DeptId, string FilePath, Action<SigninDataUpload> DoAction, Action ErrorAction = null)
	{
		_003C_003Ec__DisplayClass2 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass2();
		_003C_003Ec__DisplayClass.DoAction = DoAction;
		_003C_003Ec__DisplayClass.ErrorAction = ErrorAction;
		string url = WsTool.GetUrl();
		string format = "<?xml version=\"1.0\"?><UserInfo><MeetingID>{0}</MeetingID><UserID><![CDATA[{1}]]></UserID><DeptId><![CDATA[{2}]]></DeptId></UserInfo>";
		format = string.Format(format, MeetingID, UserID, DeptId);
		UploadFile uploadFile = new UploadFile();
		FileInfo fileInfo = new FileInfo(FilePath);
		uploadFile.Name = fileInfo.Name.Split('.')[0];
		uploadFile.Filename = fileInfo.Name;
		uploadFile.ContentType = "image/png";
		uploadFile.Stream = File.Open(FilePath, FileMode.Open);
		NameValueCollection nameValueCollection = new NameValueCollection();
		nameValueCollection.Add("XmlDoc", format);
		NameValueCollection values = nameValueCollection;
		UploadFile[] files = new UploadFile[1]
		{
			uploadFile
		};
		HttpWebRequest httpWebRequestWithFile = HttpTool.GetHttpWebRequestWithFile(url + "/SigninDataUpload", files, values);
		HttpTool.DoWithResponse(httpWebRequestWithFile, new Action<HttpWebResponse>(_003C_003Ec__DisplayClass._003CAsyncPOST_003Eb__1));
	}
}
