using System.Runtime.CompilerServices;

namespace ReadPageModule
{
	public class SearchRecord
	{
		[CompilerGenerated]
		private int a;

		[CompilerGenerated]
		private string b;

		[CompilerGenerated]
		private string c;

		[CompilerGenerated]
		private string d;

		public int targetPage
		{
			[CompilerGenerated]
			get
			{
				return a;
			}
			[CompilerGenerated]
			set
			{
				a = value;
			}
		}

		public string showedPage
		{
			[CompilerGenerated]
			get
			{
				return b;
			}
			[CompilerGenerated]
			set
			{
				b = value;
			}
		}

		public string targetLine
		{
			[CompilerGenerated]
			get
			{
				return c;
			}
			[CompilerGenerated]
			set
			{
				c = value;
			}
		}

		public string imagePath
		{
			[CompilerGenerated]
			get
			{
				return d;
			}
			[CompilerGenerated]
			set
			{
				d = value;
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
