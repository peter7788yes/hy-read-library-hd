using PaperLess_ViewModel;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

public class GetAnnotationUpload
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass2
	{
		public Action<AnnotationUpload> DoAction;

		public Action ErrorAction;

		public void _003CAsyncPOST_003Eb__1(HttpWebResponse response)
		{
			AnnotationUpload obj = null;
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
					obj = XmlHelper.XmlDeserialize<AnnotationUpload>(text, Encoding.UTF8);
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

	public static void AsyncPOST(string MeetingID, string AttachID, string Email, string Text, string FilePath, Action<AnnotationUpload> DoAction, Action ErrorAction = null)
	{
		_003C_003Ec__DisplayClass2 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass2();
		_003C_003Ec__DisplayClass.DoAction = DoAction;
		_003C_003Ec__DisplayClass.ErrorAction = ErrorAction;
		string url = WsTool.GetUrl();
		string format = "<?xml version=\"1.0\"?><UserInfo><MeetingID>{0}</MeetingID><AttachID>{1}</AttachID><Email>{2}</Email><Text>{3}</Text></UserInfo>";
		format = string.Format(format, MeetingID, AttachID, Email, Text);
		UploadFile uploadFile = new UploadFile();
		FileInfo fileInfo = new FileInfo(FilePath);
		uploadFile.Name = fileInfo.Name.Split('.')[0];
		uploadFile.Filename = fileInfo.Name;
		uploadFile.ContentType = "image/jpg";
		uploadFile.Stream = File.Open(FilePath, FileMode.Open);
		NameValueCollection nameValueCollection = new NameValueCollection();
		nameValueCollection.Add("XmlDoc", format);
		NameValueCollection values = nameValueCollection;
		UploadFile[] files = new UploadFile[1]
		{
			uploadFile
		};
		HttpWebRequest httpWebRequestWithFile = HttpTool.GetHttpWebRequestWithFile(url + "/AnnotationUpload", files, values);
		HttpTool.DoWithResponse(httpWebRequestWithFile, new Action<HttpWebResponse>(_003C_003Ec__DisplayClass._003CAsyncPOST_003Eb__1));
	}
}
