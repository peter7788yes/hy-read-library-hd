using System.Linq;
using System.Runtime.CompilerServices;

namespace PaperLess_Emeeting.App_Code.DownloadItem
{
	public class File_DownloadItemViewModel
	{
		[CompilerGenerated]
		private string _003CMeetingID_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CID_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CFileName_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUrl_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CAgendaID_003Ek__BackingField;

		[CompilerGenerated]
		private int _003CFileVersion_003Ek__BackingField;

		[CompilerGenerated]
		private int _003CFinishedFileVersion_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CEncryptionKey_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CStorageFileFolder_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CStorageFileName_003Ek__BackingField;

		[CompilerGenerated]
		private long _003CDownloadBytes_003Ek__BackingField;

		[CompilerGenerated]
		private long _003CTotalBytes_003Ek__BackingField;

		[CompilerGenerated]
		private double _003CLastPercentage_003Ek__BackingField;

		[CompilerGenerated]
		private bool _003CCanUpdate_003Ek__BackingField;

		[CompilerGenerated]
		private MeetingFileType _003CFileType_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserID_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUnZipFileFolder_003Ek__BackingField;

		public string MeetingID
		{
			[CompilerGenerated]
			get
			{
				return _003CMeetingID_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CMeetingID_003Ek__BackingField = value;
			}
		}

		public string ID
		{
			[CompilerGenerated]
			get
			{
				return _003CID_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CID_003Ek__BackingField = value;
			}
		}

		public string FileName
		{
			[CompilerGenerated]
			get
			{
				return _003CFileName_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CFileName_003Ek__BackingField = value;
			}
		}

		public string Url
		{
			[CompilerGenerated]
			get
			{
				return _003CUrl_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CUrl_003Ek__BackingField = value;
			}
		}

		public string AgendaID
		{
			[CompilerGenerated]
			get
			{
				return _003CAgendaID_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CAgendaID_003Ek__BackingField = value;
			}
		}

		public int FileVersion
		{
			[CompilerGenerated]
			get
			{
				return _003CFileVersion_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CFileVersion_003Ek__BackingField = value;
			}
		}

		public int FinishedFileVersion
		{
			[CompilerGenerated]
			get
			{
				return _003CFinishedFileVersion_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CFinishedFileVersion_003Ek__BackingField = value;
			}
		}

		public string EncryptionKey
		{
			[CompilerGenerated]
			get
			{
				return _003CEncryptionKey_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CEncryptionKey_003Ek__BackingField = value;
			}
		}

		public string StorageFileFolder
		{
			[CompilerGenerated]
			get
			{
				return _003CStorageFileFolder_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CStorageFileFolder_003Ek__BackingField = value;
			}
		}

		public string StorageFileName
		{
			[CompilerGenerated]
			get
			{
				return _003CStorageFileName_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CStorageFileName_003Ek__BackingField = value;
			}
		}

		public string StorageFilePath
		{
			get
			{
				return StorageFileFolder + "\\" + StorageFileName;
			}
			private set
			{
			}
		}

		public long DownloadBytes
		{
			[CompilerGenerated]
			get
			{
				return _003CDownloadBytes_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CDownloadBytes_003Ek__BackingField = value;
			}
		}

		public long TotalBytes
		{
			[CompilerGenerated]
			get
			{
				return _003CTotalBytes_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CTotalBytes_003Ek__BackingField = value;
			}
		}

		public double LastPercentage
		{
			[CompilerGenerated]
			get
			{
				return _003CLastPercentage_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CLastPercentage_003Ek__BackingField = value;
			}
		}

		public double NowPercentage
		{
			get
			{
				if (TotalBytes == 0)
				{
					return 0.0;
				}
				return DownloadBytes * 100 / TotalBytes;
			}
			private set
			{
			}
		}

		public bool CanUpdate
		{
			[CompilerGenerated]
			get
			{
				return _003CCanUpdate_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CCanUpdate_003Ek__BackingField = value;
			}
		}

		public MeetingFileType FileType
		{
			[CompilerGenerated]
			get
			{
				return _003CFileType_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CFileType_003Ek__BackingField = value;
			}
		}

		public string UserID
		{
			[CompilerGenerated]
			get
			{
				return _003CUserID_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CUserID_003Ek__BackingField = value;
			}
		}

		public string UnZipFileFolder
		{
			[CompilerGenerated]
			get
			{
				return _003CUnZipFileFolder_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CUnZipFileFolder_003Ek__BackingField = value;
			}
		}

		public string UnZipFilePath
		{
			get
			{
				return UnZipFileFolder + "\\" + UserID + "\\" + MeetingID + "\\" + ID + "\\" + FileVersion;
			}
			private set
			{
			}
		}

		public MeetingFileCate FileCate
		{
			get
			{
				string text = "P";
				if (ID != null)
				{
					text = Enumerable.Last(ID.Split('-'));
				}
				switch (text)
				{
				case "P":
					return MeetingFileCate.電子書;
				case "H":
					return MeetingFileCate.Html5投影片;
				case "V":
					return MeetingFileCate.影片檔;
				default:
					return MeetingFileCate.電子書;
				}
			}
			private set
			{
			}
		}

		public File_DownloadItemViewModel()
		{
			FileVersion = 1;
		}
	}
}
