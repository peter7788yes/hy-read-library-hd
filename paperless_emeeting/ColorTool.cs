using System.Globalization;
using System.Windows.Media;

public class ColorTool
{
	public static Brush HexColorToBrush(string color)
	{
		color = color.Replace("#", "");
		if (color.Length == 6)
		{
			return new SolidColorBrush(Color.FromArgb(byte.MaxValue, byte.Parse(color.Substring(0, 2), NumberStyles.HexNumber), byte.Parse(color.Substring(2, 2), NumberStyles.HexNumber), byte.Parse(color.Substring(4, 2), NumberStyles.HexNumber)));
		}
		return null;
	}
}
