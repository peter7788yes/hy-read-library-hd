using System.Runtime.CompilerServices;

namespace ReadPageModule
{
	public class ShareButton
	{
		[CompilerGenerated]
		private string a;

		[CompilerGenerated]
		private string b;

		[CompilerGenerated]
		private bool c;

		[CompilerGenerated]
		private SharedPlatform d;

		public string imagePath
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

		public string textShown
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

		public bool isShareButtonEnabled
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

		public SharedPlatform sharePlatForm
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

		public ShareButton(string _imagePath, string _textShown, SharedPlatform _sharePlatForm, bool _isShareButtonEnabled)
		{
			imagePath = _imagePath;
			textShown = _textShown;
			sharePlatForm = _sharePlatForm;
			isShareButtonEnabled = _isShareButtonEnabled;
		}
	}
}
