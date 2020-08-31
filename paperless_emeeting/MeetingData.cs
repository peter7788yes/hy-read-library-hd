using System.Xml.Serialization;

[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "", IsNullable = false)]
public class MeetingData
{
	private MeetingDataLoginResult loginResultField;

	private string subjectField;

	private string meetingsTitleField;

	private MeetingDataAgenda[] agendaListField;

	private MeetingDataMeetingsFile meetingsFileField;

	private MeetingDataDownloadFile downloadFileField;

	private string stateField;

	private string idField;

	private string nameField;

	private string beginTimeField;

	private string endTimeField;

	private string pinCodeField;

	private string typeField;

	private string locationField;

	private string capacityField;

	private string ipField;

	private string syncIPField;

	private string syncPortField;

	private string seriesMeetingIDField;

	private string statusField;

	private string watermarkField;

	public MeetingDataLoginResult LoginResult
	{
		get
		{
			return loginResultField;
		}
		set
		{
			loginResultField = value;
		}
	}

	public string Subject
	{
		get
		{
			return subjectField;
		}
		set
		{
			subjectField = value;
		}
	}

	public string MeetingsTitle
	{
		get
		{
			return meetingsTitleField;
		}
		set
		{
			meetingsTitleField = value;
		}
	}

	[XmlArrayItem("Agenda", IsNullable = false)]
	public MeetingDataAgenda[] AgendaList
	{
		get
		{
			return agendaListField;
		}
		set
		{
			agendaListField = value;
		}
	}

	public MeetingDataMeetingsFile MeetingsFile
	{
		get
		{
			return meetingsFileField;
		}
		set
		{
			meetingsFileField = value;
		}
	}

	public MeetingDataDownloadFile DownloadFile
	{
		get
		{
			return downloadFileField;
		}
		set
		{
			downloadFileField = value;
		}
	}

	[XmlAttribute]
	public string State
	{
		get
		{
			return stateField;
		}
		set
		{
			stateField = value;
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

	[XmlAttribute]
	public string PinCode
	{
		get
		{
			return pinCodeField;
		}
		set
		{
			pinCodeField = value;
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

	[XmlAttribute]
	public string Location
	{
		get
		{
			return locationField;
		}
		set
		{
			locationField = value;
		}
	}

	[XmlAttribute]
	public string Capacity
	{
		get
		{
			return capacityField;
		}
		set
		{
			capacityField = value;
		}
	}

	[XmlAttribute]
	public string IP
	{
		get
		{
			return ipField;
		}
		set
		{
			ipField = value;
		}
	}

	[XmlAttribute]
	public string syncIP
	{
		get
		{
			return syncIPField;
		}
		set
		{
			syncIPField = value;
		}
	}

	[XmlAttribute]
	public string syncPort
	{
		get
		{
			return syncPortField;
		}
		set
		{
			syncPortField = value;
		}
	}

	[XmlAttribute]
	public string SeriesMeetingID
	{
		get
		{
			return seriesMeetingIDField;
		}
		set
		{
			seriesMeetingIDField = value;
		}
	}

	[XmlAttribute]
	public string status
	{
		get
		{
			return statusField;
		}
		set
		{
			statusField = value;
		}
	}

	[XmlAttribute]
	public string watermark
	{
		get
		{
			return watermarkField;
		}
		set
		{
			watermarkField = value;
		}
	}
}
