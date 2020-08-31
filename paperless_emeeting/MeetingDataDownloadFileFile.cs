using System.Xml.Serialization;

[XmlType(AnonymousType = true)]
public class MeetingDataDownloadFileFile
{
	private string fileNameField;

	private string urlField;

	private string idField;

	private string agendaIDField;

	private string versionField;

	private string encryptionKeyField;

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
	public string AgendaID
	{
		get
		{
			return agendaIDField;
		}
		set
		{
			agendaIDField = value;
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

	[XmlAttribute]
	public string EncryptionKey
	{
		get
		{
			return encryptionKeyField;
		}
		set
		{
			encryptionKeyField = value;
		}
	}
}
