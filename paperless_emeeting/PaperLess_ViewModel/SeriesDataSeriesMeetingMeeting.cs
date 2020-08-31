using System.Xml.Serialization;

namespace PaperLess_ViewModel
{
	[XmlType(AnonymousType = true)]
	public class SeriesDataSeriesMeetingMeeting
	{
		private string idField;

		private string nameField;

		private string beginTimeField;

		private string endTimeField;

		private string locationField;

		private string pincodeField;

		private string typeField;

		private string seriesMeetingIDField;

		private string isBrowserdField;

		private string isDownloadField;

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

		[XmlAttribute]
		public string BeginTime
		{
			get
			{
				return beginTimeField;
			}
			set
			{
				beginTimeField = value;
			}
		}

		[XmlAttribute]
		public string EndTime
		{
			get
			{
				return endTimeField;
			}
			set
			{
				endTimeField = value;
			}
		}

		[XmlAttribute]
		public string Location
		{
			get
			{
				return locationField;
			}
			set
			{
				locationField = value;
			}
		}

		[XmlAttribute]
		public string pincode
		{
			get
			{
				return pincodeField;
			}
			set
			{
				pincodeField = value;
			}
		}

		[XmlAttribute]
		public string type
		{
			get
			{
				return typeField;
			}
			set
			{
				typeField = value;
			}
		}

		[XmlAttribute]
		public string SeriesMeetingID
		{
			get
			{
				return seriesMeetingIDField;
			}
			set
			{
				seriesMeetingIDField = value;
			}
		}

		[XmlAttribute]
		public string isBrowserd
		{
			get
			{
				return isBrowserdField;
			}
			set
			{
				isBrowserdField = value;
			}
		}

		[XmlAttribute]
		public string isDownload
		{
			get
			{
				return isDownloadField;
			}
			set
			{
				isDownloadField = value;
			}
		}
	}
}
