using System.Runtime.CompilerServices;

namespace PaperLess_Emeeting
{
	public class SearchRecord
	{
		[CompilerGenerated]
		private int _003CtargetPage_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CshowedPage_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CtargetLine_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CimagePath_003Ek__BackingField;

		public int targetPage
		{
			[CompilerGenerated]
			get
			{
				return _003CtargetPage_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CtargetPage_003Ek__BackingField = value;
			}
		}

		public string showedPage
		{
			[CompilerGenerated]
			get
			{
				return _003CshowedPage_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CshowedPage_003Ek__BackingField = value;
			}
		}

		public string targetLine
		{
			[CompilerGenerated]
			get
			{
				return _003CtargetLine_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CtargetLine_003Ek__BackingField = value;
			}
		}

		public string imagePath
		{
			[CompilerGenerated]
			get
			{
				return _003CimagePath_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CimagePath_003Ek__BackingField = value;
			}
		}

		public SearchRecord(string _showedPage, string _targetLine, int _targetPage)
		{
			targetPage = _targetPage;
			targetLine = _targetLine;
			showedPage = _showedPage;
		}
	}
}
