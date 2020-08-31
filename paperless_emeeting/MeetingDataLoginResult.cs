using System.Xml.Serialization;

[XmlType(AnonymousType = true)]
public class MeetingDataLoginResult
{
	private MeetingDataLoginResultLoginState loginStateField;

	private MeetingDataLoginResultButton[] enableButtonListField;

	public MeetingDataLoginResultLoginState LoginState
	{
		get
		{
			return loginStateField;
		}
		set
		{
			loginStateField = value;
		}
	}

	[XmlArrayItem("Button", IsNullable = false)]
	public MeetingDataLoginResultButton[] EnableButtonList
	{
		get
		{
			return enableButtonListField;
		}
		set
		{
			enableButtonListField = value;
		}
	}
}
