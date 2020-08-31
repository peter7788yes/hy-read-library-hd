using System.Xml.Serialization;

[XmlType(AnonymousType = true)]
public class MeetingDataMeetingsFile
{
	private MeetingDataMeetingsFileFile[] fileListField;

	[XmlArrayItem("File", IsNullable = false)]
	public MeetingDataMeetingsFileFile[] FileList
	{
		get
		{
			return fileListField;
		}
		set
		{
			fileListField = value;
		}
	}
}
