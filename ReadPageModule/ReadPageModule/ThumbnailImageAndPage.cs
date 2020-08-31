using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ReadPageModule
{
	public class ThumbnailImageAndPage : INotifyPropertyChanged
	{
		[CompilerGenerated]
		private string a;

		[CompilerGenerated]
		private string b;

		[CompilerGenerated]
		private string c;

		public bool _isDownloaded;

		[CompilerGenerated]
		private PropertyChangedEventHandler d;

		public string pageIndex
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

		public string rightImagePath
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

		public string leftImagePath
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

		public bool isDownloaded
		{
			get
			{
				return _isDownloaded;
			}
			set
			{
				_isDownloaded = value;
				if (d != null)
				{
					d(this, new PropertyChangedEventArgs("isDownloaded"));
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged
		{
			[CompilerGenerated]
			add
			{
				PropertyChangedEventHandler propertyChangedEventHandler = d;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Combine(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange(ref d, value2, propertyChangedEventHandler2);
				}
				while ((object)propertyChangedEventHandler != propertyChangedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				PropertyChangedEventHandler propertyChangedEventHandler = d;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Remove(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange(ref d, value2, propertyChangedEventHandler2);
				}
				while ((object)propertyChangedEventHandler != propertyChangedEventHandler2);
			}
		}

		public ThumbnailImageAndPage(string _pageIndex, string _rightImagePath, string _leftImagePath, bool downloadStatus)
		{
			pageIndex = _pageIndex;
			rightImagePath = _rightImagePath;
			leftImagePath = _leftImagePath;
			_isDownloaded = downloadStatus;
		}
	}
}
