using System.Xml.Serialization;

[XmlRoot(Namespace = "", IsNullable = false)]
[XmlType(AnonymousType = true)]
public class SigninData
{
	private SigninDataUser[] userListField;

	private SigninDataButton[] buttonListField;

	[XmlArrayItem("User", IsNullable = false)]
	public SigninDataUser[] UserList
	{
		get
		{
			return userListField;
		}
		set
		{
			userListField = value;
		}
	}

	[XmlArrayItem("Button", IsNullable = false)]
	public SigninDataButton[] ButtonList
	{
		get
		{
			return buttonListField;
		}
		set
		{
			buttonListField = value;
		}
	}
}
