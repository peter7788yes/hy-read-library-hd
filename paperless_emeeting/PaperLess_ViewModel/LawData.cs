using System.Xml.Serialization;

namespace PaperLess_ViewModel
{
	[XmlType(AnonymousType = true)]
	[XmlRoot(Namespace = "", IsNullable = false)]
	public class LawData
	{
		private LawDataLaw[] lawListField;

		private LawDataButton[] buttonListField;

		[XmlArrayItem("Law", IsNullable = false)]
		public LawDataLaw[] LawList
		{
			get
			{
				return lawListField;
			}
			set
			{
				lawListField = value;
			}
		}

		[XmlArrayItem("Button", IsNullable = false)]
		public LawDataButton[] ButtonList
		{
			get
			{
				return buttonListField;
			}
			set
			{
				buttonListField = value;
			}
		}
	}
}
