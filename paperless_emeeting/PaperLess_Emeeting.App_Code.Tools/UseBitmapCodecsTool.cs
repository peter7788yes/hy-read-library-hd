using System.IO;
using System.Windows.Media.Imaging;

namespace PaperLess_Emeeting.App_Code.Tools
{
	public class UseBitmapCodecsTool
	{
		public static void WriteJpeg(string fileName, int quality, BitmapSource bmp)
		{
			JpegBitmapEncoder jpegBitmapEncoder = new JpegBitmapEncoder();
			BitmapFrame item = BitmapFrame.Create(bmp);
			jpegBitmapEncoder.Frames.Add(item);
			jpegBitmapEncoder.QualityLevel = quality;
			using (FileStream stream = File.OpenWrite(fileName))
			{
				jpegBitmapEncoder.Save(stream);
			}
		}
	}
}
