using Microsoft.Win32.SafeHandles;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

public class CursorTool
{
	private struct IconInfo
	{
		public bool fIcon;

		public int xHotspot;

		public int yHotspot;

		public IntPtr hbmMask;

		public IntPtr hbmColor;
	}

	[DllImport("user32.dll")]
	private static extern IntPtr CreateIconIndirect(ref IconInfo piconinfo);

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo piconinfo);

	public static Cursor CreatCursor(Bitmap b, int x, int y)
	{
		IconInfo piconinfo = default(IconInfo);
		GetIconInfo(b.GetHicon(), ref piconinfo);
		piconinfo.xHotspot = x;
		piconinfo.yHotspot = y;
		piconinfo.fIcon = false;
		IntPtr preexistingHandle = CreateIconIndirect(ref piconinfo);
		return CursorInteropHelper.Create(new SafeFileHandle(preexistingHandle, true));
	}

	public static Cursor ConvertToCursor(FrameworkElement visual, System.Windows.Point hotSpot)
	{
		int num = (int)visual.Width;
		int num2 = (int)visual.Height;
		RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(num, num2, 96.0, 96.0, PixelFormats.Pbgra32);
		renderTargetBitmap.Render(visual);
		int[] array = new int[num * num2];
		renderTargetBitmap.CopyPixels(array, num, 0);
		Bitmap bitmap = new Bitmap(num, num2, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
		for (int i = 0; i < num2; i++)
		{
			for (int j = 0; j < num; j++)
			{
				bitmap.SetPixel(j, i, System.Drawing.Color.FromArgb(array[i * num + j]));
			}
		}
		MemoryStream memoryStream = new MemoryStream();
		Icon.FromHandle(bitmap.GetHicon()).Save(memoryStream);
		memoryStream.Seek(2L, SeekOrigin.Begin);
		memoryStream.WriteByte(2);
		memoryStream.Seek(10L, SeekOrigin.Begin);
		memoryStream.WriteByte((byte)(int)(hotSpot.X * (double)num));
		memoryStream.WriteByte((byte)(int)(hotSpot.Y * (double)num2));
		memoryStream.Seek(0L, SeekOrigin.Begin);
		return new Cursor(memoryStream);
	}

	public static Cursor CreatCursor(UIElement u, System.Windows.Point p)
	{
		u.Measure(new System.Windows.Size(double.PositiveInfinity, double.PositiveInfinity));
		u.Arrange(new Rect(0.0, 0.0, u.DesiredSize.Width, u.DesiredSize.Height));
		RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)u.DesiredSize.Width, (int)u.DesiredSize.Height, 96.0, 96.0, PixelFormats.Pbgra32);
		renderTargetBitmap.Render(u);
		u.Measure(new System.Windows.Size(0.0, 0.0));
		using (MemoryStream stream = new MemoryStream())
		{
			PngBitmapEncoder pngBitmapEncoder = new PngBitmapEncoder();
			pngBitmapEncoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
			pngBitmapEncoder.Save(stream);
			Bitmap b = new Bitmap(stream);
			return CreatCursor(b, (int)p.X, (int)p.Y);
		}
	}
}
