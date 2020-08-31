using System.Runtime.CompilerServices;

namespace PaperLess_Emeeting
{
	public class ShareButton
	{
		[CompilerGenerated]
		private string _003CimagePath_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CtextShown_003Ek__BackingField;

		[CompilerGenerated]
		private bool _003CisShareButtonEnabled_003Ek__BackingField;

		[CompilerGenerated]
		private SharedPlatform _003CsharePlatForm_003Ek__BackingField;

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

		public string textShown
		{
			[CompilerGenerated]
			get
			{
				return _003CtextShown_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CtextShown_003Ek__BackingField = value;
			}
		}

		public bool isShareButtonEnabled
		{
			[CompilerGenerated]
			get
			{
				return _003CisShareButtonEnabled_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CisShareButtonEnabled_003Ek__BackingField = value;
			}
		}

		public SharedPlatform sharePlatForm
		{
			[CompilerGenerated]
			get
			{
				return _003CsharePlatForm_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CsharePlatForm_003Ek__BackingField = value;
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
