using System.Xml.Serialization;

[XmlType(AnonymousType = true)]
public class MeetingDataLoginResultLoginState
{
	private string idField;

	private string nameField;

	private string emailField;

	private string typeField;

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
	public string Email
	{
		get
		{
			return emailField;
		}
		set
		{
			emailField = value;
		}
	}

	[XmlAttribute]
	public string Type
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
}
