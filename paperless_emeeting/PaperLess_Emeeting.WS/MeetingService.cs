using PaperLess_Emeeting.Properties;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace PaperLess_Emeeting.WS
{
	[WebServiceBinding(Name = "MeetingServiceSoap", Namespace = "http://com-meeting.ntpc.gov.tw/")]
	[DesignerCategory("code")]
	[GeneratedCode("System.Web.Services", "4.0.30319.18408")]
	[DebuggerStepThrough]
	public class MeetingService : SoapHttpClientProtocol
	{
		private SendOrPostCallback UserDataOperationCompleted;

		private SendOrPostCallback NewMeetingOperationCompleted;

		private SendOrPostCallback MeetingDataOperationCompleted;

		private SendOrPostCallback SeriesDataOperationCompleted;

		private SendOrPostCallback LawDataOperationCompleted;

		private SendOrPostCallback SigninDataOperationCompleted;

		private SendOrPostCallback SigninDataUploadOperationCompleted;

		private SendOrPostCallback SigninDataUploadNetOperationCompleted;

		private SendOrPostCallback AnnotationUploadOperationCompleted;

		private SendOrPostCallback AnnotationUploadNetOperationCompleted;

		private SendOrPostCallback ProgressUploadOperationCompleted;

		private bool useDefaultCredentialsSetExplicitly;

		public new string Url
		{
			get
			{
				return base.Url;
			}
			set
			{
				if (IsLocalFileSystemWebService(base.Url) && !useDefaultCredentialsSetExplicitly && !IsLocalFileSystemWebService(value))
				{
					base.UseDefaultCredentials = false;
				}
				base.Url = value;
			}
		}

		public new bool UseDefaultCredentials
		{
			get
			{
				return base.UseDefaultCredentials;
			}
			set
			{
				base.UseDefaultCredentials = value;
				useDefaultCredentialsSetExplicitly = true;
			}
		}

		public event UserDataCompletedEventHandler UserDataCompleted;

		public event NewMeetingCompletedEventHandler NewMeetingCompleted;

		public event MeetingDataCompletedEventHandler MeetingDataCompleted;

		public event SeriesDataCompletedEventHandler SeriesDataCompleted;

		public event LawDataCompletedEventHandler LawDataCompleted;

		public event SigninDataCompletedEventHandler SigninDataCompleted;

		public event SigninDataUploadCompletedEventHandler SigninDataUploadCompleted;

		public event SigninDataUploadNetCompletedEventHandler SigninDataUploadNetCompleted;

		public event AnnotationUploadCompletedEventHandler AnnotationUploadCompleted;

		public event AnnotationUploadNetCompletedEventHandler AnnotationUploadNetCompleted;

		public event ProgressUploadCompletedEventHandler ProgressUploadCompleted;

		public MeetingService()
		{
			Url = Settings.Default.PaperLess_Emeeting_WS_MeetingService;
			if (IsLocalFileSystemWebService(Url))
			{
				UseDefaultCredentials = true;
				useDefaultCredentialsSetExplicitly = false;
			}
			else
			{
				useDefaultCredentialsSetExplicitly = true;
			}
		}

		[SoapDocumentMethod("http://com-meeting.ntpc.gov.tw/UserData", RequestNamespace = "http://com-meeting.ntpc.gov.tw/", ResponseNamespace = "http://com-meeting.ntpc.gov.tw/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode UserData(string xmlDoc)
		{
			object[] array = Invoke("UserData", new object[1]
			{
				xmlDoc
			});
			return (XmlNode)array[0];
		}

		public void UserDataAsync(string xmlDoc)
		{
			UserDataAsync(xmlDoc, null);
		}

		public void UserDataAsync(string xmlDoc, object userState)
		{
			if (UserDataOperationCompleted == null)
			{
				UserDataOperationCompleted = new SendOrPostCallback(OnUserDataOperationCompleted);
			}
			InvokeAsync("UserData", new object[1]
			{
				xmlDoc
			}, UserDataOperationCompleted, userState);
		}

		private void OnUserDataOperationCompleted(object arg)
		{
			if (this.UserDataCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UserDataCompleted(this, new UserDataCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://com-meeting.ntpc.gov.tw/NewMeeting", RequestNamespace = "http://com-meeting.ntpc.gov.tw/", ResponseNamespace = "http://com-meeting.ntpc.gov.tw/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode NewMeeting(string xmlDoc)
		{
			object[] array = Invoke("NewMeeting", new object[1]
			{
				xmlDoc
			});
			return (XmlNode)array[0];
		}

		public void NewMeetingAsync(string xmlDoc)
		{
			NewMeetingAsync(xmlDoc, null);
		}

		public void NewMeetingAsync(string xmlDoc, object userState)
		{
			if (NewMeetingOperationCompleted == null)
			{
				NewMeetingOperationCompleted = new SendOrPostCallback(OnNewMeetingOperationCompleted);
			}
			InvokeAsync("NewMeeting", new object[1]
			{
				xmlDoc
			}, NewMeetingOperationCompleted, userState);
		}

		private void OnNewMeetingOperationCompleted(object arg)
		{
			if (this.NewMeetingCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.NewMeetingCompleted(this, new NewMeetingCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://com-meeting.ntpc.gov.tw/MeetingData", RequestNamespace = "http://com-meeting.ntpc.gov.tw/", ResponseNamespace = "http://com-meeting.ntpc.gov.tw/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode MeetingData(string xmlDoc)
		{
			object[] array = Invoke("MeetingData", new object[1]
			{
				xmlDoc
			});
			return (XmlNode)array[0];
		}

		public void MeetingDataAsync(string xmlDoc)
		{
			MeetingDataAsync(xmlDoc, null);
		}

		public void MeetingDataAsync(string xmlDoc, object userState)
		{
			if (MeetingDataOperationCompleted == null)
			{
				MeetingDataOperationCompleted = new SendOrPostCallback(OnMeetingDataOperationCompleted);
			}
			InvokeAsync("MeetingData", new object[1]
			{
				xmlDoc
			}, MeetingDataOperationCompleted, userState);
		}

		private void OnMeetingDataOperationCompleted(object arg)
		{
			if (this.MeetingDataCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.MeetingDataCompleted(this, new MeetingDataCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://com-meeting.ntpc.gov.tw/SeriesData", RequestNamespace = "http://com-meeting.ntpc.gov.tw/", ResponseNamespace = "http://com-meeting.ntpc.gov.tw/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode SeriesData(string xmlDoc)
		{
			object[] array = Invoke("SeriesData", new object[1]
			{
				xmlDoc
			});
			return (XmlNode)array[0];
		}

		public void SeriesDataAsync(string xmlDoc)
		{
			SeriesDataAsync(xmlDoc, null);
		}

		public void SeriesDataAsync(string xmlDoc, object userState)
		{
			if (SeriesDataOperationCompleted == null)
			{
				SeriesDataOperationCompleted = new SendOrPostCallback(OnSeriesDataOperationCompleted);
			}
			InvokeAsync("SeriesData", new object[1]
			{
				xmlDoc
			}, SeriesDataOperationCompleted, userState);
		}

		private void OnSeriesDataOperationCompleted(object arg)
		{
			if (this.SeriesDataCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SeriesDataCompleted(this, new SeriesDataCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://com-meeting.ntpc.gov.tw/LawData", RequestNamespace = "http://com-meeting.ntpc.gov.tw/", ResponseNamespace = "http://com-meeting.ntpc.gov.tw/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode LawData()
		{
			object[] array = Invoke("LawData", new object[0]);
			return (XmlNode)array[0];
		}

		public void LawDataAsync()
		{
			LawDataAsync(null);
		}

		public void LawDataAsync(object userState)
		{
			if (LawDataOperationCompleted == null)
			{
				LawDataOperationCompleted = new SendOrPostCallback(OnLawDataOperationCompleted);
			}
			InvokeAsync("LawData", new object[0], LawDataOperationCompleted, userState);
		}

		private void OnLawDataOperationCompleted(object arg)
		{
			if (this.LawDataCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.LawDataCompleted(this, new LawDataCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://com-meeting.ntpc.gov.tw/SigninData", RequestNamespace = "http://com-meeting.ntpc.gov.tw/", ResponseNamespace = "http://com-meeting.ntpc.gov.tw/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode SigninData(string xmlDoc)
		{
			object[] array = Invoke("SigninData", new object[1]
			{
				xmlDoc
			});
			return (XmlNode)array[0];
		}

		public void SigninDataAsync(string xmlDoc)
		{
			SigninDataAsync(xmlDoc, null);
		}

		public void SigninDataAsync(string xmlDoc, object userState)
		{
			if (SigninDataOperationCompleted == null)
			{
				SigninDataOperationCompleted = new SendOrPostCallback(OnSigninDataOperationCompleted);
			}
			InvokeAsync("SigninData", new object[1]
			{
				xmlDoc
			}, SigninDataOperationCompleted, userState);
		}

		private void OnSigninDataOperationCompleted(object arg)
		{
			if (this.SigninDataCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SigninDataCompleted(this, new SigninDataCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://com-meeting.ntpc.gov.tw/SigninDataUpload", RequestNamespace = "http://com-meeting.ntpc.gov.tw/", ResponseNamespace = "http://com-meeting.ntpc.gov.tw/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode SigninDataUpload()
		{
			object[] array = Invoke("SigninDataUpload", new object[0]);
			return (XmlNode)array[0];
		}

		public void SigninDataUploadAsync()
		{
			SigninDataUploadAsync(null);
		}

		public void SigninDataUploadAsync(object userState)
		{
			if (SigninDataUploadOperationCompleted == null)
			{
				SigninDataUploadOperationCompleted = new SendOrPostCallback(OnSigninDataUploadOperationCompleted);
			}
			InvokeAsync("SigninDataUpload", new object[0], SigninDataUploadOperationCompleted, userState);
		}

		private void OnSigninDataUploadOperationCompleted(object arg)
		{
			if (this.SigninDataUploadCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SigninDataUploadCompleted(this, new SigninDataUploadCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://com-meeting.ntpc.gov.tw/SigninDataUploadNet", RequestNamespace = "http://com-meeting.ntpc.gov.tw/", ResponseNamespace = "http://com-meeting.ntpc.gov.tw/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode SigninDataUploadNet(string xmlDoc, [XmlElement(DataType = "base64Binary")] byte[] signinImage)
		{
			object[] array = Invoke("SigninDataUploadNet", new object[2]
			{
				xmlDoc,
				signinImage
			});
			return (XmlNode)array[0];
		}

		public void SigninDataUploadNetAsync(string xmlDoc, byte[] signinImage)
		{
			SigninDataUploadNetAsync(xmlDoc, signinImage, null);
		}

		public void SigninDataUploadNetAsync(string xmlDoc, byte[] signinImage, object userState)
		{
			if (SigninDataUploadNetOperationCompleted == null)
			{
				SigninDataUploadNetOperationCompleted = new SendOrPostCallback(OnSigninDataUploadNetOperationCompleted);
			}
			InvokeAsync("SigninDataUploadNet", new object[2]
			{
				xmlDoc,
				signinImage
			}, SigninDataUploadNetOperationCompleted, userState);
		}

		private void OnSigninDataUploadNetOperationCompleted(object arg)
		{
			if (this.SigninDataUploadNetCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SigninDataUploadNetCompleted(this, new SigninDataUploadNetCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://com-meeting.ntpc.gov.tw/AnnotationUpload", RequestNamespace = "http://com-meeting.ntpc.gov.tw/", ResponseNamespace = "http://com-meeting.ntpc.gov.tw/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode AnnotationUpload()
		{
			object[] array = Invoke("AnnotationUpload", new object[0]);
			return (XmlNode)array[0];
		}

		public void AnnotationUploadAsync()
		{
			AnnotationUploadAsync(null);
		}

		public void AnnotationUploadAsync(object userState)
		{
			if (AnnotationUploadOperationCompleted == null)
			{
				AnnotationUploadOperationCompleted = new SendOrPostCallback(OnAnnotationUploadOperationCompleted);
			}
			InvokeAsync("AnnotationUpload", new object[0], AnnotationUploadOperationCompleted, userState);
		}

		private void OnAnnotationUploadOperationCompleted(object arg)
		{
			if (this.AnnotationUploadCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AnnotationUploadCompleted(this, new AnnotationUploadCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://com-meeting.ntpc.gov.tw/AnnotationUploadNet", RequestNamespace = "http://com-meeting.ntpc.gov.tw/", ResponseNamespace = "http://com-meeting.ntpc.gov.tw/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode AnnotationUploadNet(string xmlDoc, [XmlElement(DataType = "base64Binary")] byte[] annotationImage)
		{
			object[] array = Invoke("AnnotationUploadNet", new object[2]
			{
				xmlDoc,
				annotationImage
			});
			return (XmlNode)array[0];
		}

		public void AnnotationUploadNetAsync(string xmlDoc, byte[] annotationImage)
		{
			AnnotationUploadNetAsync(xmlDoc, annotationImage, null);
		}

		public void AnnotationUploadNetAsync(string xmlDoc, byte[] annotationImage, object userState)
		{
			if (AnnotationUploadNetOperationCompleted == null)
			{
				AnnotationUploadNetOperationCompleted = new SendOrPostCallback(OnAnnotationUploadNetOperationCompleted);
			}
			InvokeAsync("AnnotationUploadNet", new object[2]
			{
				xmlDoc,
				annotationImage
			}, AnnotationUploadNetOperationCompleted, userState);
		}

		private void OnAnnotationUploadNetOperationCompleted(object arg)
		{
			if (this.AnnotationUploadNetCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AnnotationUploadNetCompleted(this, new AnnotationUploadNetCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://com-meeting.ntpc.gov.tw/ProgressUpload", RequestNamespace = "http://com-meeting.ntpc.gov.tw/", ResponseNamespace = "http://com-meeting.ntpc.gov.tw/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public XmlNode ProgressUpload(string xmlDoc)
		{
			object[] array = Invoke("ProgressUpload", new object[1]
			{
				xmlDoc
			});
			return (XmlNode)array[0];
		}

		public void ProgressUploadAsync(string xmlDoc)
		{
			ProgressUploadAsync(xmlDoc, null);
		}

		public void ProgressUploadAsync(string xmlDoc, object userState)
		{
			if (ProgressUploadOperationCompleted == null)
			{
				ProgressUploadOperationCompleted = new SendOrPostCallback(OnProgressUploadOperationCompleted);
			}
			InvokeAsync("ProgressUpload", new object[1]
			{
				xmlDoc
			}, ProgressUploadOperationCompleted, userState);
		}

		private void OnProgressUploadOperationCompleted(object arg)
		{
			if (this.ProgressUploadCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ProgressUploadCompleted(this, new ProgressUploadCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		private bool IsLocalFileSystemWebService(string url)
		{
			if (url == null || url == string.Empty)
			{
				return false;
			}
			Uri uri = new Uri(url);
			if (uri.Port >= 1024 && string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			return false;
		}
	}
}
