using System.Xml.Serialization;

namespace PaperLess_ViewModel
{
	[XmlType(AnonymousType = true)]
	[XmlRoot(Namespace = "", IsNullable = false)]
	public class SigninDataUpload
	{
		private SigninDataUploadFile fileField;

		private object errorMsgField;

		public SigninDataUploadFile File
		{
			get
			{
				return fileField;
			}
			set
			{
				fileField = value;
			}
		}

		public object ErrorMsg
		{
			get
			{
				return errorMsgField;
			}
			set
			{
				errorMsgField = value;
			}
		}
	}
}
