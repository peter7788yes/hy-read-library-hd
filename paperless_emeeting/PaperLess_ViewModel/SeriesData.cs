using System.Xml.Serialization;

namespace PaperLess_ViewModel
{
	[XmlRoot(Namespace = "", IsNullable = false)]
	[XmlType(AnonymousType = true)]
	public class SeriesData
	{
		private SeriesDataSeriesMeeting[] seriesMeetingField;

		[XmlElement("SeriesMeeting")]
		public SeriesDataSeriesMeeting[] SeriesMeeting
		{
			get
			{
				return seriesMeetingField;
			}
			set
			{
				seriesMeetingField = value;
			}
		}
	}
}
