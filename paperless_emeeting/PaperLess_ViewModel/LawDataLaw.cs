using System.Xml.Serialization;

namespace PaperLess_ViewModel
{
	[XmlType(AnonymousType = true)]
	public class LawDataLaw
	{
		private string idField;

		private string upDateField;

		private string nameField;

		private string linkField;

		private string statusField;

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
		public string UpDate
		{
			get
			{
				return upDateField;
			}
			set
			{
				upDateField = value;
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

		[XmlAttribute]
		public string Link
		{
			get
			{
				return linkField;
			}
			set
			{
				linkField = value;
			}
		}

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
