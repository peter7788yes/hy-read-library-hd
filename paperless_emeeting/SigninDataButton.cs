using System.Xml.Serialization;

[XmlType(AnonymousType = true)]
public class SigninDataButton
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
