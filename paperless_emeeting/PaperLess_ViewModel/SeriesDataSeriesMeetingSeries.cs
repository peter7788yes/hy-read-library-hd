using System.Xml.Serialization;

namespace PaperLess_ViewModel
{
	[XmlType(AnonymousType = true)]
	public class SeriesDataSeriesMeetingSeries
	{
		private string idField;

		private string nameField;

		[XmlAttribute]
		public string ID
		{
			get
			{
				return idField;
			}
			set
			{
				idField = value;
			}
		}

		[XmlAttribute]
		public string Name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}
	}
}
