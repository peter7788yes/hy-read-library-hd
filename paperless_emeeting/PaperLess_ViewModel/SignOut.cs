using System.Xml.Serialization;

namespace PaperLess_ViewModel
{
	[XmlRoot(Namespace = "", IsNullable = false)]
	[XmlType(AnonymousType = true)]
	public class SignOut
	{
		private SignOutReception receptionField;

		private object errorMsgField;

		public SignOutReception Reception
		{
			get
			{
				return receptionField;
			}
			set
			{
				receptionField = value;
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
