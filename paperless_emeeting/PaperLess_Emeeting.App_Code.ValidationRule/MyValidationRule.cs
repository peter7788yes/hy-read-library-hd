using System.Globalization;
using System.Windows.Controls;

namespace PaperLess_Emeeting.App_Code.ValidationRule
{
	public class MyValidationRule : System.Windows.Controls.ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			double result = 0.0;
			if (double.TryParse((string)value, out result) && result >= 20.0 && result <= 35.0)
			{
				return new ValidationResult(true, "OK");
			}
			return new ValidationResult(false, "Error");
		}
	}
}
