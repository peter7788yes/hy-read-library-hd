using System;
using System.Globalization;
using System.Runtime.CompilerServices;

public class DateTool
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass1
	{
		public float birthdayF;

		public bool _003CBirthTransAtom_003Eb__0(float w)
		{
			if (w <= birthdayF)
			{
				return w + 1f > birthdayF;
			}
			return false;
		}
	}

	public static bool Is366DaysYear(int year)
	{
		return DateTime.IsLeapYear(year);
	}

	public static bool Is366DaysYear(DateTime date)
	{
		return DateTime.IsLeapYear(int.Parse(date.ToString("yyyy")));
	}

	public static int HowMuchDays(int year, int month)
	{
		return DateTime.DaysInMonth(year, month);
	}

	public static int HowMuchDays(int year)
	{
		int result = 365;
		if (Is366DaysYear(year))
		{
			result = 366;
		}
		return result;
	}

	public static DateTime MonthFirstDate(DateTime date)
	{
		return new DateTime(date.Year, date.Month, 1, 0, 0, 0);
	}

	public static DateTime MonthFirstDate(int year, int month)
	{
		return new DateTime(year, month, 1, 0, 0, 0);
	}

	public static DateTime MonthLastDate(DateTime date)
	{
		return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month), 23, 59, 59);
	}

	public static DateTime MonthLastDate(int year, int month)
	{
		return new DateTime(year, month, DateTime.DaysInMonth(year, month));
	}

	public static bool IsSameDate(DateTime date1, DateTime date2)
	{
		return date1.Date == date2.Date;
	}

	public static string DayOfWeek(DateTime date)
	{
		return DateTimeFormatInfo.CurrentInfo.DayNames[(byte)date.DayOfWeek];
	}

	public static int DayOfYear(DateTime date)
	{
		return date.DayOfYear;
	}

	public static DateTime StringToDate(string dateString)
	{
		DateTime result = DateTime.Now;
		DateTime.TryParse(dateString, out result);
		return result;
	}

	public static bool Between(DateTime input, DateTime date1, DateTime date2)
	{
		if (input > date1)
		{
			return input < date2;
		}
		return false;
	}

	public static bool CheckInTime(DateTime date1, DateTime date2)
	{
		if (IsSameDate(date1, DateTime.Now))
		{
			return Between(DateTime.Now, date1, date2);
		}
		return false;
	}

	public static long GetCurrentTimeInUnixMillis()
	{
		return decimal.ToInt64(decimal.Divide(DateTime.Now.Ticks - new DateTime(1970, 1, 1, 0, 0, 0).Ticks, 10000m));
	}

	public static string BirthTransAtom(DateTime birthday)
	{
		_003C_003Ec__DisplayClass1 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass1();
		_003C_003Ec__DisplayClass.birthdayF = 0f;
		string str = (birthday.Day < 10) ? "0" : "";
		_003C_003Ec__DisplayClass.birthdayF = ((birthday.Month == 1 && birthday.Day < 20) ? float.Parse(string.Format("13." + str + "{0}", birthday.Day)) : float.Parse(string.Format("{0}." + str + "{1}", birthday.Month, birthday.Day)));
		float[] array = new float[13]
		{
			1.2f,
			2.2f,
			3.21f,
			4.21f,
			5.21f,
			6.22f,
			7.23f,
			8.23f,
			9.23f,
			10.23f,
			11.21f,
			12.22f,
			13.2f
		};
		string[] array2 = new string[12]
		{
			"水瓶座",
			"雙魚座",
			"白羊座",
			"金牛座",
			"雙子座",
			"巨蟹座",
			"獅子座",
			"處女座",
			"天秤座",
			"天蠍座",
			"射手座",
			"魔羯座"
		};
		int num = Array.FindIndex(array, new Predicate<float>(_003C_003Ec__DisplayClass._003CBirthTransAtom_003Eb__0));
		return array2[num];
	}
}
