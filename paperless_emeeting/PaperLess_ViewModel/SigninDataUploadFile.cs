using System.Xml.Serialization;

namespace PaperLess_ViewModel
{
	[XmlType(AnonymousType = true)]
	public class SigninDataUploadFile
	{
		private string statusField;

		private string signedPicField;

		[XmlAttribute]
		public string Status
		{
			get
			{
				return statusField;
			}
			set
			{
				statusField = value;
			}
		}

		[XmlAttribute]
		public string SignedPic
		{
			get
			{
				return signedPicField;
			}
			set
			{
				signedPicField = value;
			}
		}
	}
}
