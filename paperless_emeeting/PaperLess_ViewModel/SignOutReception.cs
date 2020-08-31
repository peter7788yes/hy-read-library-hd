using System.Xml.Serialization;

namespace PaperLess_ViewModel
{
	[XmlType(AnonymousType = true)]
	public class SignOutReception
	{
		private string statusField;

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
	}
}
