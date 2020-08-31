using System.Xml.Serialization;

[XmlType(AnonymousType = true)]
public class MeetingDataMeetingsFileFile
{
	private string idField;

	private string urlField;

	private string fileNameField;

	private string versionField;

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
	public string Url
	{
		get
		{
			return urlField;
		}
		set
		{
			urlField = value;
		}
	}

	[XmlAttribute]
	public string FileName
	{
		get
		{
			return fileNameField;
		}
		set
		{
			fileNameField = value;
		}
	}

	[XmlAttribute]
	public string version
	{
		get
		{
			return versionField;
		}
		set
		{
			versionField = value;
		}
	}
}
