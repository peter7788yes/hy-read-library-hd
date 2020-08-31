using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

public class CanvasTool
{
	public static void SaveWindow(Window window, int dpi, string filename)
	{
		RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)window.Width, (int)window.Height, dpi, dpi, PixelFormats.Pbgra32);
		renderTargetBitmap.Render(window);
		SaveRTBAsPNG(renderTargetBitmap, filename);
	}

	public static void SaveCanvas(Window window, Canvas canvas, int dpi, string filename)
	{
		Size availableSize = new Size(window.ActualWidth + 100.0, window.ActualHeight);
		canvas.Measure(availableSize);
		RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)window.ActualWidth, (int)window.ActualHeight - 200, dpi, dpi, PixelFormats.Pbgra32);
		renderTargetBitmap.Render(canvas);
		SaveRTBAsPNG(renderTargetBitmap, filename);
	}

	public static void SaveCanvas(Window window, InkCanvas canvas, int dpi, string filename)
	{
		Size availableSize = new Size(window.Width + 100.0, window.Height);
		canvas.Measure(availableSize);
		RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)window.Width, (int)window.Height - 200, dpi, dpi, PixelFormats.Pbgra32);
		renderTargetBitmap.Render(canvas);
		SaveRTBAsPNG(renderTargetBitmap, filename);
	}

	private static void SaveRTBAsPNG(RenderTargetBitmap bmp, string filename)
	{
		PngBitmapEncoder pngBitmapEncoder = new PngBitmapEncoder();
		pngBitmapEncoder.Frames.Add(BitmapFrame.Create(bmp));
		using (FileStream stream = File.Create(filename))
		{
			pngBitmapEncoder.Save(stream);
		}
	}

	private static void SaveRTBAsJPG(RenderTargetBitmap bmp, string filename)
	{
		JpegBitmapEncoder jpegBitmapEncoder = new JpegBitmapEncoder();
		jpegBitmapEncoder.Frames.Add(BitmapFrame.Create(bmp));
		using (FileStream stream = File.Create(filename))
		{
			jpegBitmapEncoder.Save(stream);
		}
	}
}
