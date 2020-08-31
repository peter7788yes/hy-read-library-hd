using System;
using System.Runtime.CompilerServices;

namespace PaperLess_Emeeting.App_Code.DownloadItem
{
	public class Law_DownloadItemViewModel
	{
		public MeetingFileCate FileCate;

		[CompilerGenerated]
		private string _003CID_003Ek__BackingField;

		[CompilerGenerated]
		private DateTime _003CUpDate_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CName_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CLink_003Ek__BackingField;

		[CompilerGenerated]
		private LawDataStatus _003CStatus_003Ek__BackingField;

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
		private LawFileType _003CFileType_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserID_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUnZipFileFolder_003Ek__BackingField;

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

		public DateTime UpDate
		{
			[CompilerGenerated]
			get
			{
				return _003CUpDate_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CUpDate_003Ek__BackingField = value;
			}
		}

		public string Name
		{
			[CompilerGenerated]
			get
			{
				return _003CName_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CName_003Ek__BackingField = value;
			}
		}

		public string Link
		{
			[CompilerGenerated]
			get
			{
				return _003CLink_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CLink_003Ek__BackingField = value;
			}
		}

		public LawDataStatus Status
		{
			[CompilerGenerated]
			get
			{
				return _003CStatus_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CStatus_003Ek__BackingField = value;
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

		public LawFileType FileType
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
				return UnZipFileFolder + "\\" + UserID + "\\" + ID;
			}
			private set
			{
			}
		}
	}
}
