using System;
using System.Globalization;
using System.Windows.Data;

namespace PaperLess_Emeeting
{
	[ValueConversion(typeof(string), typeof(bool?))]
	internal class ConverterYN2TF : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			switch (System.Convert.ToString(value))
			{
			case "Y":
				return true;
			case "N":
				return false;
			default:
				return null;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool? flag = System.Convert.ToBoolean(value);
			bool valueOrDefault = flag.GetValueOrDefault();
			if (flag.HasValue)
			{
				switch (valueOrDefault)
				{
				case true:
					return "Y";
				case false:
					return "N";
				}
			}
			return "Null";
		}
	}
}
