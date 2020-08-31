using System.Windows.Input;

public class MouseTool
{
	public static void ShowLoading()
	{
		Mouse.OverrideCursor = Cursors.AppStarting;
	}

	public static void ShowArrow()
	{
		Mouse.OverrideCursor = Cursors.Arrow;
	}

	public static void ShowIBeam()
	{
		Mouse.OverrideCursor = Cursors.IBeam;
	}

	public static void ShowHand()
	{
		Mouse.OverrideCursor = Cursors.Hand;
	}

	public static void ShowPen()
	{
		Mouse.OverrideCursor = Cursors.Pen;
	}
}
