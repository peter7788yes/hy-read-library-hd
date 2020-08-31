using Newtonsoft.Json;
using PaperLess_Emeeting.App_Code.DownloadItem;
using PaperLess_Emeeting.App_Code.MessageBox;
using PaperLess_Emeeting.Properties;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.Linq;

namespace PaperLess_Emeeting
{
	public class PDFFactoryCT : UserControl, IComponentConnector
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass3
		{
			public List<MeetingDataDownloadFileFile> FileList;

			public PDFFactoryCT _003C_003E4__this;

			public void _003CInitSelectDB_003Eb__2()
			{
				int num = 0;
				foreach (MeetingDataDownloadFileFile file in FileList)
				{
					num++;
					File_DownloadItemViewModel fileItem = FileItemTool.Gen(file, _003C_003E4__this.UserID, _003C_003E4__this.MeetingID);
					_003C_003E4__this.PDFFactoryRowSP.Children.Add(new PDFFactoryRow(num, fileItem));
				}
			}
		}

		public bool CanDetectServerState = true;

		public MeetingData md;

		public bool HasRecordFile;

		private int All_FileCount;

		internal Grid btnServerCtrl;

		internal Image btnStatus;

		internal TextBlock txtStatus;

		internal TextBox txtKeyword;

		internal StackPanel PDFFactoryRowSP;

		private bool _contentLoaded;

		[CompilerGenerated]
		private string _003CMeetingID_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserID_003Ek__BackingField;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate7;

		[CompilerGenerated]
		private static Func<XElement, _003C_003Ef__AnonymousType0<string>> CS_0024_003C_003E9__CachedAnonymousMethodDelegate9;

		public string MeetingID
		{
			[CompilerGenerated]
			get
			{
				return _003CMeetingID_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CMeetingID_003Ek__BackingField = value;
			}
		}

		public string UserID
		{
			[CompilerGenerated]
			get
			{
				return _003CUserID_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CUserID_003Ek__BackingField = value;
			}
		}

		public PDFFactoryCT()
		{
			MouseTool.ShowLoading();
			InitializeComponent();
			base.Loaded += new RoutedEventHandler(PDFFactoryCT_Loaded);
			base.Unloaded += new RoutedEventHandler(PDFFactoryCT_Unloaded);
		}

		private void PDFFactoryCT_Unloaded(object sender, RoutedEventArgs e)
		{
			CanDetectServerState = false;
		}

		private void PDFFactoryCT_Loaded(object sender, RoutedEventArgs e)
		{
			Task.Factory.StartNew(new Action(_003CPDFFactoryCT_Loaded_003Eb__0));
		}

		private void InitSelectDB()
		{
			DataTable dataTable = MSCE.GetDataTable("select MeetingID,UserID from NowLogin");
			if (dataTable.Rows.Count > 0)
			{
				MeetingID = dataTable.Rows[0]["MeetingID"].ToString().Trim();
				UserID = dataTable.Rows[0]["UserID"].ToString().Trim();
			}
			dataTable = MSCE.GetDataTable("select MeetingJson from MeetingData where MeetingID=@1 and UserID =@2", MeetingID, UserID);
			if (dataTable.Rows.Count > 0)
			{
				_003C_003Ec__DisplayClass3 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass3();
				_003C_003Ec__DisplayClass._003C_003E4__this = this;
				md = JsonConvert.DeserializeObject<MeetingData>(dataTable.Rows[0]["MeetingJson"].ToString());
				_003C_003Ec__DisplayClass.FileList = new List<MeetingDataDownloadFileFile>();
				try
				{
					MeetingDataMeetingsFileFile[] fileList = md.MeetingsFile.FileList;
					foreach (MeetingDataMeetingsFileFile meetingDataMeetingsFileFile in fileList)
					{
						MeetingDataDownloadFileFile meetingDataDownloadFileFile = new MeetingDataDownloadFileFile();
						meetingDataDownloadFileFile.AgendaID = "record";
						meetingDataDownloadFileFile.FileName = meetingDataMeetingsFileFile.FileName;
						meetingDataDownloadFileFile.ID = meetingDataMeetingsFileFile.ID;
						meetingDataDownloadFileFile.Url = meetingDataMeetingsFileFile.Url;
						meetingDataDownloadFileFile.version = meetingDataMeetingsFileFile.version;
						_003C_003Ec__DisplayClass.FileList.Add(meetingDataDownloadFileFile);
						HasRecordFile = true;
					}
				}
				catch (Exception)
				{
				}
				_003C_003Ec__DisplayClass.FileList.AddRange(Enumerable.ToList(md.DownloadFile.DownloadFileList));
				All_FileCount = _003C_003Ec__DisplayClass.FileList.Count;
				base.Dispatcher.BeginInvoke(new Action(_003C_003Ec__DisplayClass._003CInitSelectDB_003Eb__2));
			}
			else
			{
				AutoClosingMessageBox.Show("無法取得資料，請稍後再試");
				MouseTool.ShowArrow();
			}
		}

		private void InitEvent()
		{
			txtKeyword.MouseEnter += new MouseEventHandler(_003CInitEvent_003Eb__5);
			TextBox textBox = txtKeyword;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate7 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate7 = new MouseEventHandler(_003CInitEvent_003Eb__6);
			}
			textBox.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate7;
			txtKeyword.KeyUp += new KeyEventHandler(txtKeyword_KeyUp);
			txtKeyword.Focus();
		}

		private void txtKeyword_KeyUp(object sender, KeyEventArgs e)
		{
			string text = txtKeyword.Text.ToLower().Trim();
			if (!text.Equals(""))
			{
				foreach (PDFFactoryRow item in Enumerable.OfType<PDFFactoryRow>(PDFFactoryRowSP.Children))
				{
					if (item.txtIndex.Text.Contains(text) || item.txtFileName.Text.Contains(text) || item.txtStatus.Text.Contains(text))
					{
						item.Visibility = Visibility.Visible;
					}
					else
					{
						item.Visibility = Visibility.Collapsed;
					}
				}
			}
			else
			{
				foreach (PDFFactoryRow item2 in Enumerable.OfType<PDFFactoryRow>(PDFFactoryRowSP.Children))
				{
					item2.Visibility = Visibility.Visible;
				}
			}
		}

		private void ClearList()
		{
			if (!base.Dispatcher.CheckAccess())
			{
				base.Dispatcher.BeginInvoke(new Action(ClearList));
			}
		}

		private void ChangeServerCtrl(bool Online)
		{
			if (Online)
			{
				txtStatus.Text = "連線中";
				txtStatus.Foreground = ColorTool.HexColorToBrush("#E2F540");
				txtStatus.HorizontalAlignment = HorizontalAlignment.Left;
				btnStatus.Source = new BitmapImage(new Uri("images/btn_broadcast_connected.png", UriKind.Relative));
			}
			else
			{
				txtStatus.Text = "未啟動";
				txtStatus.Foreground = ColorTool.HexColorToBrush("#707A82");
				txtStatus.HorizontalAlignment = HorizontalAlignment.Center;
				btnStatus.Source = new BitmapImage(new Uri("images/btn_broadcast_broken.png", UriKind.Relative));
			}
		}

		private void InitUI()
		{
		}

		public bool StopSyncServer(string meetingID)
		{
			bool result = false;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("<?xml version=\"1.0\" encoding=\"UTF-8\"?>").AppendFormat("<Sync>").AppendFormat("<Stop ID=\"{0}\" />", MeetingID)
					.AppendFormat("</Sync>");
				if (PostToSyncServer("/StopSyncServer", stringBuilder.ToString()).Contains("成功"))
				{
					return true;
				}
				return result;
			}
			catch (Exception ex)
			{
				result = false;
				LogTool.Debug(ex);
				return result;
			}
		}

		public bool SyncServerAlreadyStarted(string meetingID)
		{
			bool result = false;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("<?xml version=\"1.0\" encoding=\"UTF-8\"?>").AppendFormat("<MeetingList date=\"{0}\" >", DateTime.Now.ToString("yyyyMMddHHmmss")).AppendFormat("</MeetingList>");
				XDocument xDocument = XDocument.Parse(PostToSyncServer("/GetMeetingList", stringBuilder.ToString()));
				IEnumerable<XElement> source = xDocument.Element("MeetingList").Elements("Meeting");
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate9 == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate9 = new Func<XElement, _003C_003Ef__AnonymousType0<string>>(_003CSyncServerAlreadyStarted_003Eb__8);
				}
				IEnumerable<_003C_003Ef__AnonymousType0<string>> enumerable = Enumerable.Select(source, CS_0024_003C_003E9__CachedAnonymousMethodDelegate9);
				foreach (_003C_003Ef__AnonymousType0<string> item in enumerable)
				{
					if (item.ID.Equals(meetingID))
					{
						return true;
					}
				}
				return result;
			}
			catch (Exception ex)
			{
				result = false;
				LogTool.Debug(ex);
				return result;
			}
		}

		private string PostToSyncServer(string subUrl, string sentXml)
		{
			string result = "";
			try
			{
				string str = SocketTool.GetUrl();
				string url_Imp = SocketTool.GetUrl_Imp();
				if (MeetingID.ToLower().StartsWith("i"))
				{
					str = url_Imp;
				}
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(str + subUrl);
				byte[] bytes = Encoding.UTF8.GetBytes(sentXml);
				httpWebRequest.Method = "POST";
				httpWebRequest.ContentType = "text/xml; encoding='utf-8'";
				httpWebRequest.ContentLength = bytes.Length;
				Stream requestStream = httpWebRequest.GetRequestStream();
				requestStream.Write(bytes, 0, bytes.Length);
				requestStream.Close();
				WebResponse response = httpWebRequest.GetResponse();
				requestStream = response.GetResponseStream();
				StreamReader streamReader = new StreamReader(requestStream);
				result = streamReader.ReadToEnd();
				streamReader.Close();
				requestStream.Close();
				response.Close();
				return result;
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
				return result;
			}
		}

		public bool StartSyncServer(string meetingID)
		{
			bool result = false;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>").Append("<Sync>").AppendFormat("<Start ID=\"{0}\" MaxClient=\"{1}\" />", meetingID, 100)
					.AppendFormat("<Init>{0}</Init>", Settings.Default.InitConfig)
					.Append("</Sync>");
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(PostToSyncServer("/StartSyncServer", stringBuilder.ToString()));
				XmlNode documentElement = xmlDocument.DocumentElement;
				string value = documentElement.SelectSingleNode("/Sync/Start/@IP").Value;
				int num = int.Parse(documentElement.SelectSingleNode("/Sync/Start/@Port").Value);
				if (value.Equals(""))
				{
					return result;
				}
				if (num < 1)
				{
					return result;
				}
				if (num <= 65535)
				{
					return true;
				}
				return result;
			}
			catch (Exception ex)
			{
				result = false;
				LogTool.Debug(ex);
				return result;
			}
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/pdffactoryct.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				btnServerCtrl = (Grid)target;
				break;
			case 2:
				btnStatus = (Image)target;
				break;
			case 3:
				txtStatus = (TextBlock)target;
				break;
			case 4:
				txtKeyword = (TextBox)target;
				break;
			case 5:
				PDFFactoryRowSP = (StackPanel)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}

		[CompilerGenerated]
		private void _003CPDFFactoryCT_Loaded_003Eb__0()
		{
			InitSelectDB();
			base.Dispatcher.BeginInvoke(new Action(_003CPDFFactoryCT_Loaded_003Eb__1));
		}

		[CompilerGenerated]
		private void _003CPDFFactoryCT_Loaded_003Eb__1()
		{
			try
			{
				InitEvent();
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__5(object sender, MouseEventArgs e)
		{
			MouseTool.ShowIBeam();
			txtKeyword.Focus();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__6(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private static _003C_003Ef__AnonymousType0<string> _003CSyncServerAlreadyStarted_003Eb__8(XElement x)
		{
			return new _003C_003Ef__AnonymousType0<string>(x.Attribute("ID").Value.Trim());
		}
	}
}
