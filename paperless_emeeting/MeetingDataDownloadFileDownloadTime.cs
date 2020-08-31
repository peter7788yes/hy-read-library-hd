using System.Xml.Serialization;

[XmlType(AnonymousType = true)]
public class MeetingDataDownloadFileDownloadTime
{
	private string beginTimeField;

	private string endTimeField;

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
}
