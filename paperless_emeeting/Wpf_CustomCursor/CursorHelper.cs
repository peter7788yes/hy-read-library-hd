using Microsoft.Win32.SafeHandles;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Wpf_CustomCursor
{
	public class CursorHelper
	{
		private static class NativeMethods
		{
			public struct IconInfo
			{
				public bool fIcon;

				public int xHotspot;

				public int yHotspot;

				public IntPtr hbmMask;

				public IntPtr hbmColor;
			}

			[DllImport("user32.dll")]
			public static extern SafeIconHandle CreateIconIndirect(ref IconInfo icon);

			[DllImport("user32.dll")]
			public static extern bool DestroyIcon(IntPtr hIcon);

			[DllImport("user32.dll")]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);
		}

		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		private class SafeIconHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			public SafeIconHandle()
				: base(true)
			{
			}

			protected override bool ReleaseHandle()
			{
				return NativeMethods.DestroyIcon(handle);
			}
		}

		private static Cursor InternalCreateCursor(Bitmap bitmap, int xHotSpot, int yHotSpot)
		{
			NativeMethods.IconInfo icon = default(NativeMethods.IconInfo);
			NativeMethods.GetIconInfo(bitmap.GetHicon(), ref icon);
			icon.xHotspot = xHotSpot;
			icon.yHotspot = yHotSpot;
			icon.fIcon = false;
			SafeIconHandle cursorHandle = NativeMethods.CreateIconIndirect(ref icon);
			return CursorInteropHelper.Create(cursorHandle);
		}

		public static Cursor CreateCursor(UIElement element, int xHotSpot = 0, int yHotSpot = 0)
		{
			element.Measure(new System.Windows.Size(double.PositiveInfinity, double.PositiveInfinity));
			element.Arrange(new Rect(default(System.Windows.Point), element.DesiredSize));
			RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)element.DesiredSize.Width, (int)element.DesiredSize.Height, 96.0, 96.0, PixelFormats.Pbgra32);
			renderTargetBitmap.Render(element);
			PngBitmapEncoder pngBitmapEncoder = new PngBitmapEncoder();
			pngBitmapEncoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
			using (MemoryStream stream = new MemoryStream())
			{
				pngBitmapEncoder.Save(stream);
				using (Bitmap bitmap = new Bitmap(stream))
				{
					return InternalCreateCursor(bitmap, xHotSpot, yHotSpot);
				}
			}
		}
	}
}
