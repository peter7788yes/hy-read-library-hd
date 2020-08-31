using System.Xml.Serialization;

[XmlType(AnonymousType = true)]
public class SigninDataUser
{
	private string idField;

	private string nameField;

	private string attendField;

	private string agentIDField;

	private string deptIDField;

	private string deptField;

	private string titleField;

	private string rankField;

	private string agentNameField;

	private string isSignedField;

	private string signedPicField;

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
	public string Attend
	{
		get
		{
			return attendField;
		}
		set
		{
			attendField = value;
		}
	}

	[XmlAttribute]
	public string AgentID
	{
		get
		{
			return agentIDField;
		}
		set
		{
			agentIDField = value;
		}
	}

	[XmlAttribute]
	public string DeptID
	{
		get
		{
			return deptIDField;
		}
		set
		{
			deptIDField = value;
		}
	}

	[XmlAttribute]
	public string Dept
	{
		get
		{
			return deptField;
		}
		set
		{
			deptField = value;
		}
	}

	[XmlAttribute]
	public string Title
	{
		get
		{
			return titleField;
		}
		set
		{
			titleField = value;
		}
	}

	[XmlAttribute]
	public string Rank
	{
		get
		{
			return rankField;
		}
		set
		{
			rankField = value;
		}
	}

	[XmlAttribute]
	public string AgentName
	{
		get
		{
			return agentNameField;
		}
		set
		{
			agentNameField = value;
		}
	}

	[XmlAttribute]
	public string IsSigned
	{
		get
		{
			return isSignedField;
		}
		set
		{
			isSignedField = value;
		}
	}

	[XmlAttribute]
	public string SignedPic
	{
		get
		{
			return signedPicField;
		}
		set
		{
			signedPicField = value;
		}
	}
}
