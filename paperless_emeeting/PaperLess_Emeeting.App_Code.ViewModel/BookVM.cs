using System.Runtime.CompilerServices;

namespace PaperLess_Emeeting.App_Code.ViewModel
{
	public class BookVM
	{
		[CompilerGenerated]
		private string _003CFileID_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CBookPath_003Ek__BackingField;

		[CompilerGenerated]
		private MeetingFileCate _003CFileCate_003Ek__BackingField;

		public string FileID
		{
			[CompilerGenerated]
			get
			{
				return _003CFileID_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CFileID_003Ek__BackingField = value;
			}
		}

		public string BookPath
		{
			[CompilerGenerated]
			get
			{
				return _003CBookPath_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CBookPath_003Ek__BackingField = value;
			}
		}

		public MeetingFileCate FileCate
		{
			[CompilerGenerated]
			get
			{
				return _003CFileCate_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CFileCate_003Ek__BackingField = value;
			}
		}

		public BookVM(string FileID, string BookPath, MeetingFileCate FileCate)
		{
			this.FileID = FileID;
			this.BookPath = BookPath;
			this.FileCate = FileCate;
		}
	}
}
