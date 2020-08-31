using System.Xml.Serialization;

[XmlType(AnonymousType = true)]
public class MeetingDataAgenda
{
	private string idField;

	private string parentIDField;

	private string agendaField;

	private string captionField;

	private string proposalUnitField;

	private string progressField;

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
	public string ParentID
	{
		get
		{
			return parentIDField;
		}
		set
		{
			parentIDField = value;
		}
	}

	[XmlAttribute]
	public string Agenda
	{
		get
		{
			return agendaField;
		}
		set
		{
			agendaField = value;
		}
	}

	[XmlAttribute]
	public string Caption
	{
		get
		{
			return captionField;
		}
		set
		{
			captionField = value;
		}
	}

	[XmlAttribute]
	public string ProposalUnit
	{
		get
		{
			return proposalUnitField;
		}
		set
		{
			proposalUnitField = value;
		}
	}

	[XmlAttribute]
	public string Progress
	{
		get
		{
			return progressField;
		}
		set
		{
			progressField = value;
		}
	}
}
