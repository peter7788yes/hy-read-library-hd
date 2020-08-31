using System.Xml.Serialization;

namespace PaperLess_ViewModel
{
	[XmlType(AnonymousType = true)]
	public class SeriesDataSeriesMeeting
	{
		private SeriesDataSeriesMeetingSeries seriesField;

		private SeriesDataSeriesMeetingMeeting[] meetingListField;

		public SeriesDataSeriesMeetingSeries Series
		{
			get
			{
				return seriesField;
			}
			set
			{
				seriesField = value;
			}
		}

		[XmlArrayItem("Meeting", IsNullable = false)]
		public SeriesDataSeriesMeetingMeeting[] MeetingList
		{
			get
			{
				return meetingListField;
			}
			set
			{
				meetingListField = value;
			}
		}
	}
}
