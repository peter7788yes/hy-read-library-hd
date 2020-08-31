using System.Xml.Serialization;

namespace PaperLess_ViewModel
{
	[XmlType(AnonymousType = true)]
	[XmlRoot(Namespace = "", IsNullable = false)]
	public class AnnotationUpload
	{
		private object errorMsgField;

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
