using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PaperLess_Emeeting
{
	public class UriToCachedImageConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return null;
			}
			if (!string.IsNullOrEmpty(value.ToString()))
			{
				BitmapImage bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.UriSource = new Uri(value.ToString());
				bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapImage.EndInit();
				return bitmapImage;
			}
			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException("Two way conversion is not supported.");
		}
	}
}
