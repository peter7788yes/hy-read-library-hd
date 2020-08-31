using PaperLess_Emeeting.App_Code.MessageBox;
using PaperLess_Emeeting.Properties;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;

public class HttpTool
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass3
	{
		public HttpWebRequest request;

		public Action<HttpWebResponse> responseAction;

		public Action ErrorAction;

		public void _003CDoWithResponse_003Eb__0()
		{
			request.BeginGetResponse(new AsyncCallback(_003CDoWithResponse_003Eb__1), request);
		}

		public void _003CDoWithResponse_003Eb__2(IAsyncResult iar)
		{
			Action action = (Action)iar.AsyncState;
			try
			{
				action.EndInvoke(iar);
			}
			catch (Exception ex)
			{
				if (ErrorAction != null)
				{
					ErrorAction();
				}
				LogTool.Debug(ex);
				AutoClosingMessageBox.Show(ex.Message);
			}
		}

		public void _003CDoWithResponse_003Eb__1(IAsyncResult iar)
		{
			HttpWebResponse obj = (HttpWebResponse)((HttpWebRequest)iar.AsyncState).EndGetResponse(iar);
			responseAction(obj);
		}
	}

	public static long GetResponseLength(string url)
	{
		long result = 0L;
		try
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			result = httpWebRequest.GetResponse().ContentLength;
			httpWebRequest.Abort();
			return result;
		}
		catch (Exception ex)
		{
			LogTool.Debug(ex);
			return result;
		}
	}

	public static string CallWebService(string wsURL, string wsMethod, string wsData)
	{
		WebRequest webRequest = WebRequest.Create(string.Format("{0}/{1}", wsURL, wsMethod));
		webRequest.Proxy = null;
		webRequest.Method = "POST";
		webRequest.ContentType = "application/x-www-form-urlencoded";
		byte[] bytes = Encoding.UTF8.GetBytes(wsData);
		webRequest.ContentLength = bytes.Length;
		webRequest.GetRequestStream().Write(bytes, 0, bytes.Length);
		HttpWebResponse httpWebResponse = (HttpWebResponse)webRequest.GetResponse();
		Stream responseStream = httpWebResponse.GetResponseStream();
		StreamReader streamReader = new StreamReader(responseStream);
		return streamReader.ReadToEnd();
	}

	public static string CreateRequest(string reqUrl, string method, Dictionary<string, string> dictParams = null)
	{
		string empty = string.Empty;
		string result = string.Empty;
		try
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(reqUrl);
			httpWebRequest.Proxy = null;
			httpWebRequest.Method = method;
			httpWebRequest.KeepAlive = true;
			httpWebRequest.AllowWriteStreamBuffering = false;
			httpWebRequest.Timeout = 1000 * Settings.Default.HttpWebRequest_TimeoutSeconds;
			if (method == "POST")
			{
				string text = "";
				if (dictParams != null)
				{
					foreach (KeyValuePair<string, string> dictParam in dictParams)
					{
						text = text + string.Format("{0}={1}", dictParam.Key.Trim(), HttpUtility.UrlEncode(dictParam.Value.Trim())) + "&";
					}
				}
				byte[] bytes = Encoding.UTF8.GetBytes(text.TrimEnd('&'));
				httpWebRequest.ContentLength = bytes.Length;
				using (Stream stream = httpWebRequest.GetRequestStream())
				{
					stream.Write(bytes, 0, bytes.Length);
				}
			}
			using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
			{
				using (Stream stream2 = httpWebResponse.GetResponseStream())
				{
					using (StreamReader streamReader = new StreamReader(stream2, Encoding.UTF8))
					{
						result = streamReader.ReadToEnd();
						return result;
					}
				}
			}
		}
		catch (Exception ex)
		{
			LogTool.Debug(ex);
			return result;
		}
	}

	public static HttpWebRequest GetHttpWebRequest(string reqUrl, string method, Dictionary<string, string> dictParams = null)
	{
		HttpWebRequest httpWebRequest = null;
		try
		{
			httpWebRequest = (HttpWebRequest)WebRequest.Create(reqUrl);
			httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip;
			httpWebRequest.Proxy = null;
			httpWebRequest.Method = method;
			httpWebRequest.KeepAlive = true;
			httpWebRequest.Timeout = 1000 * Settings.Default.HttpWebRequest_TimeoutSeconds;
			httpWebRequest.AllowWriteStreamBuffering = false;
			httpWebRequest.ContentType = "application/x-www-form-urlencoded";
			if (method == "POST")
			{
				string text = "";
				if (dictParams != null)
				{
					foreach (KeyValuePair<string, string> dictParam in dictParams)
					{
						text = text + string.Format("{0}={1}", dictParam.Key.Trim(), HttpUtility.UrlEncode(dictParam.Value.Trim())) + "&";
					}
				}
				byte[] bytes = Encoding.UTF8.GetBytes(text.TrimEnd('&'));
				httpWebRequest.ContentLength = bytes.Length;
				using (Stream stream = httpWebRequest.GetRequestStream())
				{
					stream.Write(bytes, 0, bytes.Length);
					return httpWebRequest;
				}
			}
			return httpWebRequest;
		}
		catch (Exception ex)
		{
			LogTool.Debug(ex);
			throw;
		}
	}

	public static HttpWebRequest GetHttpWebRequestWithFile(string reqUrl, IEnumerable<UploadFile> files, NameValueCollection values)
	{
		HttpWebRequest httpWebRequest = null;
		try
		{
			httpWebRequest = (HttpWebRequest)WebRequest.Create(reqUrl);
			httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip;
			httpWebRequest.Proxy = null;
			httpWebRequest.Method = "POST";
			httpWebRequest.KeepAlive = true;
			httpWebRequest.Timeout = 1000 * Settings.Default.HttpWebRequest_TimeoutSeconds;
			string str = "---------------------------" + DateTime.Now.Ticks.ToString("x", NumberFormatInfo.InvariantInfo);
			httpWebRequest.ContentType = "multipart/form-data; boundary=" + str;
			str = "--" + str;
			using (Stream stream = httpWebRequest.GetRequestStream())
			{
				foreach (string key in values.Keys)
				{
					byte[] bytes = Encoding.ASCII.GetBytes(str + Environment.NewLine);
					stream.Write(bytes, 0, bytes.Length);
					bytes = Encoding.ASCII.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"{1}{1}", key, Environment.NewLine));
					stream.Write(bytes, 0, bytes.Length);
					bytes = Encoding.UTF8.GetBytes(values[key] + Environment.NewLine);
					stream.Write(bytes, 0, bytes.Length);
				}
				foreach (UploadFile file in files)
				{
					byte[] bytes2 = Encoding.ASCII.GetBytes(str + Environment.NewLine);
					stream.Write(bytes2, 0, bytes2.Length);
					bytes2 = Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"{2}", file.Name, file.Filename, Environment.NewLine));
					stream.Write(bytes2, 0, bytes2.Length);
					bytes2 = Encoding.ASCII.GetBytes(string.Format("Content-Type: {0}{1}{1}", file.ContentType, Environment.NewLine));
					stream.Write(bytes2, 0, bytes2.Length);
					file.Stream.CopyTo(stream);
					file.Stream.Close();
					bytes2 = Encoding.ASCII.GetBytes(Environment.NewLine);
					stream.Write(bytes2, 0, bytes2.Length);
				}
				byte[] bytes3 = Encoding.ASCII.GetBytes(str + "--");
				stream.Write(bytes3, 0, bytes3.Length);
				return httpWebRequest;
			}
		}
		catch (Exception ex)
		{
			LogTool.Debug(ex);
			throw;
		}
	}

	public byte[] UploadFiles(string address, IEnumerable<UploadFile> files, NameValueCollection values)
	{
		WebRequest webRequest = WebRequest.Create(address);
		webRequest.Proxy = null;
		webRequest.Method = "POST";
		string str = "---------------------------" + DateTime.Now.Ticks.ToString("x", NumberFormatInfo.InvariantInfo);
		webRequest.ContentType = "multipart/form-data; boundary=" + str;
		str = "--" + str;
		using (Stream stream = webRequest.GetRequestStream())
		{
			foreach (string key in values.Keys)
			{
				byte[] bytes = Encoding.ASCII.GetBytes(str + Environment.NewLine);
				stream.Write(bytes, 0, bytes.Length);
				bytes = Encoding.ASCII.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"{1}{1}", key, Environment.NewLine));
				stream.Write(bytes, 0, bytes.Length);
				bytes = Encoding.UTF8.GetBytes(values[key] + Environment.NewLine);
				stream.Write(bytes, 0, bytes.Length);
			}
			foreach (UploadFile file in files)
			{
				byte[] bytes2 = Encoding.ASCII.GetBytes(str + Environment.NewLine);
				stream.Write(bytes2, 0, bytes2.Length);
				bytes2 = Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"{2}", file.Name, file.Filename, Environment.NewLine));
				stream.Write(bytes2, 0, bytes2.Length);
				bytes2 = Encoding.ASCII.GetBytes(string.Format("Content-Type: {0}{1}{1}", file.ContentType, Environment.NewLine));
				stream.Write(bytes2, 0, bytes2.Length);
				file.Stream.CopyTo(stream);
				bytes2 = Encoding.ASCII.GetBytes(Environment.NewLine);
				stream.Write(bytes2, 0, bytes2.Length);
			}
			byte[] bytes3 = Encoding.ASCII.GetBytes(str + "--");
			stream.Write(bytes3, 0, bytes3.Length);
		}
		using (WebResponse webResponse = webRequest.GetResponse())
		{
			using (Stream stream2 = webResponse.GetResponseStream())
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					stream2.CopyTo(memoryStream);
					return memoryStream.ToArray();
				}
			}
		}
	}

	public static string CreateRequestWithFile(string reqUrl, string xmlData, string filePath)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("------");
		stringBuilder.AppendLine("Content-Disposition: form-data; name=\"xmlDoc\"");
		stringBuilder.AppendLine();
		stringBuilder.AppendLine(xmlData);
		stringBuilder.AppendLine("------WebKitFormBoundaryxgiRzoRqkAQqQbXU");
		stringBuilder.AppendLine("Content-Disposition: form-data; name=\"signinImage\"; filename=\"test.png\"");
		stringBuilder.AppendLine("Content-Type: image/png");
		stringBuilder.AppendLine();
		MemoryStream memoryStream = new MemoryStream();
		byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
		memoryStream.Write(bytes, 0, bytes.Length);
		FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
		byte[] array = new byte[1024];
		int num = 0;
		while ((num = fileStream.Read(array, 0, array.Length)) != 0)
		{
			memoryStream.Write(array, 0, num);
		}
		fileStream.Close();
		stringBuilder.Clear();
		stringBuilder.AppendLine("------WebKitFormBoundaryxgiRzoRqkAQqQbXU");
		stringBuilder.AppendLine("Content-Disposition: form-data; name=\"submit\"");
		stringBuilder.AppendLine();
		stringBuilder.AppendLine("submit");
		stringBuilder.AppendLine("------WebKitFormBoundaryxgiRzoRqkAQqQbXU--");
		bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
		memoryStream.Write(bytes, 0, bytes.Length);
		string empty = string.Empty;
		string result = string.Empty;
		HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(reqUrl);
		httpWebRequest.Proxy = null;
		httpWebRequest.ContentType = "multipart/form-data; boundary=----WebKitFormBoundaryxgiRzoRqkAQqQbXU";
		httpWebRequest.Method = "POST";
		httpWebRequest.KeepAlive = true;
		httpWebRequest.ContentLength = memoryStream.Length;
		Stream requestStream = httpWebRequest.GetRequestStream();
		memoryStream.Position = 0L;
		byte[] array2 = new byte[memoryStream.Length];
		memoryStream.Read(array2, 0, array2.Length);
		memoryStream.Close();
		requestStream.Write(array2, 0, array2.Length);
		requestStream.Close();
		try
		{
			WebResponse response = httpWebRequest.GetResponse();
			Stream responseStream = response.GetResponseStream();
			StreamReader streamReader = new StreamReader(responseStream);
			result = streamReader.ReadToEnd();
		}
		catch (Exception ex)
		{
			LogTool.Debug(ex);
		}
		httpWebRequest = null;
		return result;
	}

	public static ImageFormat GetImageFormat(Image img)
	{
		if (img.RawFormat.Equals(ImageFormat.Jpeg))
		{
			return ImageFormat.Jpeg;
		}
		if (img.RawFormat.Equals(ImageFormat.Bmp))
		{
			return ImageFormat.Bmp;
		}
		if (img.RawFormat.Equals(ImageFormat.Png))
		{
			return ImageFormat.Png;
		}
		if (img.RawFormat.Equals(ImageFormat.Emf))
		{
			return ImageFormat.Emf;
		}
		if (img.RawFormat.Equals(ImageFormat.Exif))
		{
			return ImageFormat.Exif;
		}
		if (img.RawFormat.Equals(ImageFormat.Gif))
		{
			return ImageFormat.Gif;
		}
		if (img.RawFormat.Equals(ImageFormat.Icon))
		{
			return ImageFormat.Icon;
		}
		if (img.RawFormat.Equals(ImageFormat.MemoryBmp))
		{
			return ImageFormat.MemoryBmp;
		}
		if (img.RawFormat.Equals(ImageFormat.Tiff))
		{
			return ImageFormat.Tiff;
		}
		return ImageFormat.Wmf;
	}

	public static string ImageToBase64(Image image)
	{
		MemoryStream memoryStream = new MemoryStream();
		ImageFormat imageFormat = GetImageFormat(image);
		image.Save(memoryStream, imageFormat);
		byte[] inArray = memoryStream.ToArray();
		return Convert.ToBase64String(inArray);
	}

	public static void DoWithResponse(HttpWebRequest request, Action<HttpWebResponse> responseAction, Action ErrorAction = null)
	{
		_003C_003Ec__DisplayClass3 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass3();
		_003C_003Ec__DisplayClass.request = request;
		_003C_003Ec__DisplayClass.responseAction = responseAction;
		_003C_003Ec__DisplayClass.ErrorAction = ErrorAction;
		Action action = new Action(_003C_003Ec__DisplayClass._003CDoWithResponse_003Eb__0);
		action.BeginInvoke(new AsyncCallback(_003C_003Ec__DisplayClass._003CDoWithResponse_003Eb__2), action);
	}
}
