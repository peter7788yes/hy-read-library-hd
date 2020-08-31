using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace PaperLess_Emeeting.App_Code.Tools
{
	public static class CaptureScreenshotTool
	{
		private enum TernaryRasterOperations : uint
		{
			SRCCOPY = 13369376u,
			SRCPAINT = 15597702u,
			SRCAND = 8913094u,
			SRCINVERT = 6684742u,
			SRCERASE = 4457256u,
			NOTSRCCOPY = 3342344u,
			NOTSRCERASE = 1114278u,
			MERGECOPY = 12583114u,
			MERGEPAINT = 12255782u,
			PATCOPY = 15728673u,
			PATPAINT = 16452105u,
			PATINVERT = 5898313u,
			DSTINVERT = 5570569u,
			BLACKNESS = 66u,
			WHITENESS = 16711778u
		}

		public static BitmapSource Capture(Rect area)
		{
			IntPtr dC = GetDC(IntPtr.Zero);
			IntPtr intPtr = CreateCompatibleDC(dC);
			IntPtr intPtr2 = CreateCompatibleBitmap(dC, (int)SystemParameters.VirtualScreenWidth, (int)SystemParameters.VirtualScreenHeight);
			SelectObject(intPtr, intPtr2);
			BitBlt(intPtr, 0, 0, (int)area.Width, (int)area.Height, dC, (int)area.X, (int)area.Y, TernaryRasterOperations.SRCCOPY);
			BitmapSource result = Imaging.CreateBitmapSourceFromHBitmap(intPtr2, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
			DeleteObject(intPtr2);
			ReleaseDC(IntPtr.Zero, dC);
			ReleaseDC(IntPtr.Zero, intPtr);
			return result;
		}

		[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

		[DllImport("gdi32.dll")]
		private static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

		[DllImport("gdi32.dll", SetLastError = true)]
		private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

		[DllImport("gdi32.dll")]
		private static extern bool DeleteObject(IntPtr hObject);

		[DllImport("gdi32.dll")]
		private static extern IntPtr CreateBitmap(int nWidth, int nHeight, uint cPlanes, uint cBitsPerPel, IntPtr lpvBits);

		[DllImport("user32.dll")]
		private static extern IntPtr GetDC(IntPtr hWnd);

		[DllImport("user32.dll")]
		private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

		[DllImport("gdi32.dll")]
		private static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, TernaryRasterOperations dwRop);
	}
}
