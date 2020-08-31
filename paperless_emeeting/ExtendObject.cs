using System.Runtime.CompilerServices;
using System.Windows;

[Extension]
public static class ExtendObject
{
	[Extension]
	public static Rect GetAbsoltutePlacement(FrameworkElement element, bool relativeToScreen = false)
	{
		Point point = element.PointToScreen(new Point(0.0, 0.0));
		if (relativeToScreen)
		{
			return new Rect(point.X, point.Y, element.ActualWidth, element.ActualHeight);
		}
		Point point2 = Application.Current.MainWindow.PointToScreen(new Point(0.0, 0.0));
		point = new Point(point.X - point2.X, point.Y - point2.Y);
		return new Rect(point.X, point.Y, element.ActualWidth, element.ActualHeight);
	}
}
