using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PaperLess_Emeeting
{
	public class ThumbnailImageAndPage : INotifyPropertyChanged
	{
		public bool _isDownloaded;

		[CompilerGenerated]
		private string _003CpageIndex_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CrightImagePath_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CleftImagePath_003Ek__BackingField;

		public string pageIndex
		{
			[CompilerGenerated]
			get
			{
				return _003CpageIndex_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CpageIndex_003Ek__BackingField = value;
			}
		}

		public string rightImagePath
		{
			[CompilerGenerated]
			get
			{
				return _003CrightImagePath_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CrightImagePath_003Ek__BackingField = value;
			}
		}

		public string leftImagePath
		{
			[CompilerGenerated]
			get
			{
				return _003CleftImagePath_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CleftImagePath_003Ek__BackingField = value;
			}
		}

		public bool isDownloaded
		{
			get
			{
				return _isDownloaded;
			}
			set
			{
				_isDownloaded = value;
				if (this.PropertyChanged != null)
				{
					this.PropertyChanged(this, new PropertyChangedEventArgs("isDownloaded"));
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public ThumbnailImageAndPage(string _pageIndex, string _rightImagePath, string _leftImagePath, bool downloadStatus)
		{
			pageIndex = _pageIndex;
			rightImagePath = _rightImagePath;
			leftImagePath = _leftImagePath;
			_isDownloaded = downloadStatus;
		}
	}
}
