using System;
using System.Windows.Media.Imaging;

namespace PaperLess_Emeeting
{
	public class imageSourceRenderedResultEventArgs : EventArgs
	{
		public BitmapImage imgSource;

		public int renderPageIndex;

		public float sourceScale;

		public imageSourceRenderedResultEventArgs(BitmapImage imgSource, int renderPageIndex, float sourceScale)
		{
			this.imgSource = imgSource;
			this.renderPageIndex = renderPageIndex;
			this.sourceScale = sourceScale;
		}
	}
}
