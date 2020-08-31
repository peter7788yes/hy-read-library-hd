using System.Xml.Serialization;

[XmlType(AnonymousType = true)]
public class MeetingDataDownloadFile
{
	private MeetingDataDownloadFileDownloadTime downloadTimeField;

	private MeetingDataDownloadFileBrowseTime browseTimeField;

	private MeetingDataDownloadFileFile[] downloadFileListField;

	public MeetingDataDownloadFileDownloadTime DownloadTime
	{
		get
		{
			return downloadTimeField;
		}
		set
		{
			downloadTimeField = value;
		}
	}

	public MeetingDataDownloadFileBrowseTime BrowseTime
	{
		get
		{
			return browseTimeField;
		}
		set
		{
			browseTimeField = value;
		}
	}

	[XmlArrayItem("File", IsNullable = false)]
	public MeetingDataDownloadFileFile[] DownloadFileList
	{
		get
		{
			return downloadFileListField;
		}
		set
		{
			downloadFileListField = value;
		}
	}
}
