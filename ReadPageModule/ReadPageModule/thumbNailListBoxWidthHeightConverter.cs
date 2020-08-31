using System;
using System.Globalization;
using System.Windows.Data;

namespace ReadPageModule
{
	public class thumbNailListBoxWidthHeightConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (targetType != typeof(double))
			{
				throw new InvalidOperationException("it is not a double");
			}
			double num = double.Parse(value.ToString());
			if (num.Equals(0.0))
			{
				return 0;
			}
			double num2 = double.Parse(parameter.ToString());
			return num - num2;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
