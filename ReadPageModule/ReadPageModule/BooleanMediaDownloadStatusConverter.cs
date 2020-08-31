using System;
using System.Globalization;
using System.Windows.Data;

namespace ReadPageModule
{
	public class BooleanMediaDownloadStatusConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (targetType != typeof(string))
			{
				throw new InvalidOperationException("it is not a string");
			}
			if (bool.Parse(value.ToString()))
			{
				return "已下載";
			}
			return "未下載";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
