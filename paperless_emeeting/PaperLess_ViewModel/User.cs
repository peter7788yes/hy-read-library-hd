using System.Xml.Serialization;

namespace PaperLess_ViewModel
{
	[XmlRoot(Namespace = "", IsNullable = false)]
	[XmlType(AnonymousType = true)]
	public class User
	{
		private UserButton[] enableButtonListField;

		private UserMeeting[] meetingListField;

		private string idField;

		private string nameField;

		private string emailField;

		private string stateField;

		private string uTCField;

		[XmlArrayItem("Button", IsNullable = false)]
		public UserButton[] EnableButtonList
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

		[XmlArrayItem("Meeting", IsNullable = false)]
		public UserMeeting[] MeetingList
		{
			get
			{
				return meetingListField;
			}
			set
			{
				meetingListField = value;
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
		public string UTC
		{
			get
			{
				return uTCField;
			}
			set
			{
				uTCField = value;
			}
		}
	}
}
