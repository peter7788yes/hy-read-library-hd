using CACodec;
using PXCView36;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PaperLess_Emeeting
{
	public class ReadPagePair
	{
		public class NativeMethods
		{
			[DllImport("ole32.dll")]
			public static extern void CoTaskMemFree(IntPtr pv);

			[DllImport("ole32.dll")]
			public static extern IntPtr CoTaskMemAlloc(IntPtr cb);

			[DllImport("libpdf2jpg.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
			public static extern IntPtr pdfLoadFromMemory(int dpi, float scale, IntPtr ibuf, int ilen, IntPtr obptr, IntPtr olptr, int pgs);

			[DllImport("libpdf2jpg.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
			public static extern int pdfNumberOfPages(IntPtr ibuf, int pgs);

			[DllImport("libpdf2jpg.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
			public static extern int pdfPageSize(int dpi, float scale, IntPtr ibuf, int ilen, IntPtr pWidth, IntPtr pHeight, int pgs);

			[DllImport("libpdf2jpg.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
			public static extern IntPtr pdfLoadFromMemoryPartial(int dpi, float scale, IntPtr ibuf, int ilen, IntPtr obptr, IntPtr olptr, int x0, int y0, int x1, int y1, int pgs);
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass6
		{
			public ReadPagePair _003C_003E4__this;

			public CACodecTools caTool;

			public byte[] curKey;

			public Border border;

			public float PDFScale;

			public bool isSinglePage;

			public void _003CresizeLargePHEJBitmapImage_003Eb__0()
			{
				_003C_003E4__this.getPHEJDoubleBitmapImageAsync(caTool, curKey, _003C_003E4__this.leftImagePath, _003C_003E4__this.rightImagePath, PDFScale, _003C_003E4__this.leftPageIndex, border, new EventHandler<imageSourceRenderedResultEventArgs>(_003C_003E4__this.zoomimageSourceRendered), isSinglePage);
			}

			public void _003CresizeLargePHEJBitmapImage_003Eb__1()
			{
				_003C_003E4__this.getPHEJSingleBitmapImageAsync(caTool, curKey, _003C_003E4__this.leftImagePath, PDFScale, _003C_003E4__this.leftPageIndex, border, new EventHandler<imageSourceRenderedResultEventArgs>(_003C_003E4__this.zoomimageSourceRendered), isSinglePage);
			}

			public void _003CresizeLargePHEJBitmapImage_003Eb__2()
			{
				_003C_003E4__this.getPHEJSingleBitmapImageAsync(caTool, curKey, _003C_003E4__this.rightImagePath, PDFScale, _003C_003E4__this.leftPageIndex, border, new EventHandler<imageSourceRenderedResultEventArgs>(_003C_003E4__this.zoomimageSourceRendered), isSinglePage);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClasse
		{
			public ReadPagePair _003C_003E4__this;

			public CACodecTools caTool;

			public byte[] curKey;

			public Border border;

			public bool isSinglePage;

			public void _003CcreateLargePHEJBitmapImage_003Eb__8()
			{
				_003C_003E4__this.getPHEJDoubleBitmapImageAsync(caTool, curKey, _003C_003E4__this.leftImagePath, _003C_003E4__this.rightImagePath, _003C_003E4__this.PDFScale, _003C_003E4__this.leftPageIndex, border, new EventHandler<imageSourceRenderedResultEventArgs>(_003C_003E4__this.leftimageSourceRendered), isSinglePage);
			}

			public void _003CcreateLargePHEJBitmapImage_003Eb__9()
			{
				_003C_003E4__this.getPHEJSingleBitmapImageAsync(caTool, curKey, _003C_003E4__this.leftImagePath, _003C_003E4__this.PDFScale, _003C_003E4__this.leftPageIndex, border, new EventHandler<imageSourceRenderedResultEventArgs>(_003C_003E4__this.leftimageSourceRendered), isSinglePage);
			}

			public void _003CcreateLargePHEJBitmapImage_003Eb__a()
			{
				_003C_003E4__this.getPHEJSingleBitmapImageAsync(caTool, curKey, _003C_003E4__this.rightImagePath, _003C_003E4__this.PDFScale, _003C_003E4__this.leftPageIndex, border, new EventHandler<imageSourceRenderedResultEventArgs>(_003C_003E4__this.leftimageSourceRendered), isSinglePage);
			}
		}

		public int leftPageIndex;

		public int rightPageIndex;

		public ImageStatus imgStatus;

		public string leftImagePath;

		public string rightImagePath;

		public string leftPageId;

		public string rightPageId;

		public ImageSource leftImageSource;

		public ImageSource rightImageSource;

		public ImageSource resizedImageSource;

		public byte[][] decodedPDFPages = new byte[2][];

		private int PDFdpi = 96;

		public float PDFScale = 1f;

		public double baseScale = 1.0;

		public int zoomStep;

		public bool isRendering;

		public List<Thread> zoomThread = new List<Thread>();

		private DateTime lastTimeOfZooming;

		private bool isResizing;

		public event EventHandler<imageSourceRenderedResultEventArgs> leftImageSourceRendered;

		public event EventHandler<imageSourceRenderedResultEventArgs> rightImageSourceRendered;

		private event EventHandler<imageSourceRenderedResultEventArgs> resizeImageSourceRendered;

		public event EventHandler<imageSourceRenderedResultEventArgs> sendresizeImageSourceRendered;

		public ReadPagePair(int leftPageIndex, int rightPageIndex, string leftImgPath, string rightImgPath, string _leftPageId, string _rightPageId, int PDFdpi)
		{
			this.PDFdpi = PDFdpi;
			this.leftPageIndex = leftPageIndex;
			this.rightPageIndex = rightPageIndex;
			imgStatus = ImageStatus.SMALLIMAGE;
			leftImagePath = leftImgPath;
			rightImagePath = rightImgPath;
			leftPageId = _leftPageId;
			rightPageId = _rightPageId;
		}

		public void resizeLargePHEJBitmapImage(CACodecTools caTool, byte[] curKey, Border border, float PDFScale, int zoomStep, DateTime resizeTime, bool isSinglePage)
		{
			ThreadStart threadStart = null;
			ThreadStart threadStart2 = null;
			ThreadStart threadStart3 = null;
			_003C_003Ec__DisplayClass6 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass6();
			_003C_003Ec__DisplayClass.caTool = caTool;
			_003C_003Ec__DisplayClass.curKey = curKey;
			_003C_003Ec__DisplayClass.border = border;
			_003C_003Ec__DisplayClass.PDFScale = PDFScale;
			_003C_003Ec__DisplayClass.isSinglePage = isSinglePage;
			_003C_003Ec__DisplayClass._003C_003E4__this = this;
			lastTimeOfZooming = resizeTime;
			this.PDFScale = _003C_003Ec__DisplayClass.PDFScale;
			this.zoomStep = zoomStep;
			if (leftImagePath.Length > 0)
			{
				if (rightImagePath.Length > 0)
				{
					if (threadStart == null)
					{
						threadStart = new ThreadStart(_003C_003Ec__DisplayClass._003CresizeLargePHEJBitmapImage_003Eb__0);
					}
					Thread thread = new Thread(threadStart);
					thread.Name = _003C_003Ec__DisplayClass.PDFScale.ToString();
					zoomThread.Add(thread);
				}
				else
				{
					if (threadStart2 == null)
					{
						threadStart2 = new ThreadStart(_003C_003Ec__DisplayClass._003CresizeLargePHEJBitmapImage_003Eb__1);
					}
					Thread thread2 = new Thread(threadStart2);
					thread2.Name = _003C_003Ec__DisplayClass.PDFScale.ToString();
					zoomThread.Add(thread2);
				}
			}
			else if (rightImagePath.Length > 0)
			{
				if (threadStart3 == null)
				{
					threadStart3 = new ThreadStart(_003C_003Ec__DisplayClass._003CresizeLargePHEJBitmapImage_003Eb__2);
				}
				Thread thread3 = new Thread(threadStart3);
				thread3.Name = _003C_003Ec__DisplayClass.PDFScale.ToString();
				zoomThread.Add(thread3);
			}
			if (zoomThread.Count.Equals(0) || isResizing)
			{
				return;
			}
			for (int num = zoomThread.Count - 1; num >= 0; num--)
			{
				if (_003C_003Ec__DisplayClass.PDFScale.Equals((float)Convert.ToDouble(zoomThread[num].Name)))
				{
					try
					{
						resizeImageSourceRendered += new EventHandler<imageSourceRenderedResultEventArgs>(zoomimageSourceRendered);
						zoomThread[num].Start();
						zoomThread.Clear();
						isResizing = true;
						return;
					}
					catch
					{
					}
				}
			}
		}

		public void createLargeHEJBitmapImage(CACodecTools caTool, byte[] curKey)
		{
			if (leftImagePath.Length > 0)
			{
				if (rightImagePath.Length > 0)
				{
					leftImageSource = getHEJDoubleBitmapImage(caTool, curKey);
				}
				else
				{
					leftImageSource = getHEJSingleBitmapImage(caTool, curKey);
				}
			}
			else if (rightImagePath.Length > 0)
			{
				rightImageSource = getHEJSingleBitmapImage(caTool, curKey);
			}
		}

		public void createLargePHEJBitmapImage(CACodecTools caTool, byte[] curKey, Border border, bool isSinglePage)
		{
			ThreadStart threadStart = null;
			ThreadStart threadStart2 = null;
			ThreadStart threadStart3 = null;
			_003C_003Ec__DisplayClasse _003C_003Ec__DisplayClasse = new _003C_003Ec__DisplayClasse();
			_003C_003Ec__DisplayClasse.caTool = caTool;
			_003C_003Ec__DisplayClasse.curKey = curKey;
			_003C_003Ec__DisplayClasse.border = border;
			_003C_003Ec__DisplayClasse.isSinglePage = isSinglePage;
			_003C_003Ec__DisplayClasse._003C_003E4__this = this;
			if (leftImagePath.Length > 0)
			{
				if (rightImagePath.Length > 0)
				{
					leftImageSourceRendered += new EventHandler<imageSourceRenderedResultEventArgs>(leftimageSourceRendered);
					if (threadStart == null)
					{
						threadStart = new ThreadStart(_003C_003Ec__DisplayClasse._003CcreateLargePHEJBitmapImage_003Eb__8);
					}
					Thread thread = new Thread(threadStart);
					thread.Name = PDFScale.ToString();
					thread.Start();
					isRendering = true;
				}
				else
				{
					leftImageSourceRendered += new EventHandler<imageSourceRenderedResultEventArgs>(leftimageSourceRendered);
					if (threadStart2 == null)
					{
						threadStart2 = new ThreadStart(_003C_003Ec__DisplayClasse._003CcreateLargePHEJBitmapImage_003Eb__9);
					}
					Thread thread2 = new Thread(threadStart2);
					thread2.Name = PDFScale.ToString();
					thread2.Start();
					isRendering = true;
				}
			}
			else if (rightImagePath.Length > 0)
			{
				rightImageSourceRendered += new EventHandler<imageSourceRenderedResultEventArgs>(rightimageSourceRendered);
				if (threadStart3 == null)
				{
					threadStart3 = new ThreadStart(_003C_003Ec__DisplayClasse._003CcreateLargePHEJBitmapImage_003Eb__a);
				}
				Thread thread3 = new Thread(threadStart3);
				thread3.Name = PDFScale.ToString();
				thread3.Start();
				isRendering = true;
			}
		}

		private Bitmap renPdfToBitmap(CACodecTools caTool, string pageFile, byte[] key, int pg, int dpi, float scal, int decodedPageIndex, Border border, bool isSinglePage)
		{
			System.Drawing.Color white = System.Drawing.Color.White;
			Bitmap aBitmap = null;
			try
			{
				if (decodedPDFPages[decodedPageIndex] == null)
				{
					decodedPDFPages[decodedPageIndex] = caTool.fileAESDecode(pageFile, key);
				}
			}
			catch (Exception ex)
			{
				decodedPDFPages[decodedPageIndex] = null;
				LogTool.Debug(ex);
			}
			try
			{
				PDFDoc pDFDoc = new PDFDoc();
				pDFDoc.Init("PVD20-M4IRG-QYZK9-MNJ2U-DFTK1-MAJ4L", "PDFX3$Henry$300604_Allnuts#");
				pDFDoc.OpenFromMemory(decodedPDFPages[decodedPageIndex], (uint)decodedPDFPages[decodedPageIndex].Length, 0);
				PXCV_Lib36.PXV_CommonRenderParameters aCommonRenderParams = prepareCommonRenderParameter(pDFDoc, dpi, pg, scal, 0, 0, border, isSinglePage);
				pDFDoc.DrawPageToDIBSection(IntPtr.Zero, pg, white, aCommonRenderParams, out aBitmap);
				pDFDoc.ReleasePageCachedData(pg, 1);
				pDFDoc.Delete();
				return aBitmap;
			}
			catch (Exception ex2)
			{
				LogTool.Debug(ex2);
				return aBitmap;
			}
		}

		private Bitmap renPdfToBitmap(string pageFile, byte[] key, int pg, int dpi, float scal, int decodedPageIndex, Border border, bool isSinglePage)
		{
			System.Drawing.Color white = System.Drawing.Color.White;
			Bitmap aBitmap = null;
			if (decodedPDFPages[decodedPageIndex] == null)
			{
				try
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						FileStream fileStream = new FileStream(pageFile, FileMode.Open);
						fileStream.CopyTo(memoryStream);
						decodedPDFPages[decodedPageIndex] = memoryStream.ToArray();
					}
				}
				catch
				{
					return aBitmap;
				}
			}
			try
			{
				PDFDoc pDFDoc = new PDFDoc();
				pDFDoc.Init("PVD20-M4IRG-QYZK9-MNJ2U-DFTK1-MAJ4L", "PDFX3$Henry$300604_Allnuts#");
				pDFDoc.OpenFromMemory(decodedPDFPages[decodedPageIndex], (uint)decodedPDFPages[decodedPageIndex].Length, 0);
				PXCV_Lib36.PXV_CommonRenderParameters aCommonRenderParams = prepareCommonRenderParameter(pDFDoc, dpi, pg, scal, 0, 0, border, isSinglePage);
				pDFDoc.DrawPageToDIBSection(IntPtr.Zero, pg, white, aCommonRenderParams, out aBitmap);
				pDFDoc.ReleasePageCachedData(pg, 1);
				pDFDoc.Delete();
				return aBitmap;
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
				return aBitmap;
			}
		}

		private PXCV_Lib36.PXV_CommonRenderParameters prepareCommonRenderParameter(PDFDoc pdfDoc, int dpi, int pageNumber, float zoom, int offsetX, int offsetY, Border border, bool isSinglePage)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(PXCV_Helper.RECT)));
			IntPtr intPtr2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(PXCV_Helper.RECT)));
			Point point = new Point(offsetX, offsetY);
			Size empty = Size.Empty;
			PXCV_Helper.RECT rECT = default(PXCV_Helper.RECT);
			PXCV_Helper.RECT rECT2 = default(PXCV_Helper.RECT);
			PXCV_Lib36.PXV_CommonRenderParameters result = default(PXCV_Lib36.PXV_CommonRenderParameters);
			PageDimension pageDimension = default(PageDimension);
			pdfDoc.GetPageDimensions(pageNumber, out pageDimension.w, out pageDimension.h);
			double num = border.ActualHeight / 96.0 * (double)dpi;
			double num2 = border.ActualWidth / 96.0 * (double)dpi;
			if (zoomStep == 0)
			{
				empty.Width = (int)(pageDimension.w / 72.0 * (double)dpi * (double)zoom);
				empty.Height = (int)(pageDimension.h / 72.0 * (double)dpi * (double)zoom);
				double num3 = num2 / num;
				double num4 = 0.0;
				num4 = ((!isSinglePage) ? ((double)(empty.Width * 2) / (double)empty.Height) : ((double)empty.Width / (double)empty.Height));
				if ((double)empty.Width < num2 && (double)empty.Height < num)
				{
					double num5;
					double num6;
					if (num4 > num3)
					{
						num5 = num2 / 2.0;
						baseScale = num5 / (double)empty.Width;
						num6 = Math.Round(baseScale * (double)empty.Height, 2);
					}
					else
					{
						num6 = num;
						baseScale = num6 / (double)empty.Height;
						num5 = Math.Round(baseScale * (double)empty.Width, 2);
					}
					empty.Width = (int)num5;
					empty.Height = (int)num6;
				}
				else
				{
					double num7;
					double num8;
					if (num4 > num3)
					{
						num7 = num2 / 2.0;
						baseScale = num7 / (double)empty.Width;
						num8 = Math.Round(baseScale * (double)empty.Height, 2);
					}
					else
					{
						num8 = num;
						baseScale = num8 / (double)empty.Height;
						num7 = Math.Round(baseScale * (double)empty.Width, 2);
					}
					empty.Width = (int)num7;
					empty.Height = (int)num8;
				}
			}
			else
			{
				empty.Width = (int)(pageDimension.w / 72.0 * (double)dpi * (double)zoom * baseScale);
				empty.Height = (int)(pageDimension.h / 72.0 * (double)dpi * (double)zoom * baseScale);
			}
			rECT.left = -point.X;
			rECT.top = -point.Y;
			rECT.right = rECT.left + empty.Width;
			rECT.bottom = rECT.top + empty.Height;
			rECT2.left = 0;
			rECT2.top = 0;
			if (zoomStep == 0)
			{
				if ((double)empty.Width < num2)
				{
					rECT2.right = empty.Width;
				}
				else
				{
					rECT2.right = (int)num2;
				}
				if ((double)empty.Height < num)
				{
					rECT2.bottom = empty.Height;
				}
				else
				{
					rECT2.bottom = (int)num;
				}
			}
			else
			{
				rECT2.right = empty.Width;
				rECT2.bottom = empty.Height;
			}
			Marshal.StructureToPtr((object)rECT, intPtr, false);
			Marshal.StructureToPtr((object)rECT2, intPtr2, false);
			result.WholePageRect = intPtr;
			result.DrawRect = intPtr2;
			result.RenderTarget = PXCV_Lib36.PXCV_RenderMode.pxvrm_Viewing;
			result.Flags = 0;
			return result;
		}

		private void getPHEJSingleBitmapImageAsync(CACodecTools caTool, byte[] curKey, string pagePath, float scal, int curPageIndex, Border border, EventHandler<imageSourceRenderedResultEventArgs> ImageSourceRendered, bool isSinglePage)
		{
			BitmapImage bitmapImage = new BitmapImage();
			Bitmap bitmap = (curKey == null || curKey.Length <= 1) ? renPdfToBitmap(pagePath, curKey, 0, PDFdpi, scal, 0, border, isSinglePage) : renPdfToBitmap(caTool, pagePath, curKey, 0, PDFdpi, scal, 0, border, isSinglePage);
			using (MemoryStream memoryStream = new MemoryStream())
			{
				try
				{
					bitmap.Save(memoryStream, ImageFormat.Bmp);
					bitmapImage.BeginInit();
					bitmapImage.StreamSource = memoryStream;
					bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
					bitmapImage.EndInit();
					bitmapImage.CacheOption = BitmapCacheOption.None;
					bitmapImage.StreamSource.Close();
					bitmapImage.StreamSource = null;
					bitmapImage.Freeze();
					memoryStream.Dispose();
					memoryStream.Close();
					bitmap.Dispose();
					bitmap = null;
				}
				catch (Exception ex)
				{
					LogTool.Debug(ex);
				}
			}
			if (ImageSourceRendered != null)
			{
				ImageSourceRendered(this, new imageSourceRenderedResultEventArgs(bitmapImage, curPageIndex, scal));
			}
		}

		private void zoomimageSourceRendered(object sender, imageSourceRenderedResultEventArgs e)
		{
			resizeImageSourceRendered -= new EventHandler<imageSourceRenderedResultEventArgs>(zoomimageSourceRendered);
			isResizing = false;
			if (leftPageIndex.Equals(e.renderPageIndex))
			{
				if (PDFScale.Equals(e.sourceScale))
				{
					EventHandler<imageSourceRenderedResultEventArgs> eventHandler = this.sendresizeImageSourceRendered;
					if (eventHandler != null)
					{
						eventHandler(sender, e);
					}
					isRendering = false;
					zoomThread.Clear();
					zoomStep = 0;
					PDFScale = 1f;
					return;
				}
				for (int num = zoomThread.Count - 1; num >= 0; num--)
				{
					if (PDFScale.Equals((float)Convert.ToDouble(zoomThread[num].Name)))
					{
						try
						{
							zoomThread[num].Start();
							resizeImageSourceRendered += new EventHandler<imageSourceRenderedResultEventArgs>(zoomimageSourceRendered);
							isResizing = true;
							return;
						}
						catch
						{
						}
					}
				}
			}
			else
			{
				isResizing = false;
				zoomThread.Clear();
			}
		}

		private void leftimageSourceRendered(object sender, imageSourceRenderedResultEventArgs e)
		{
			leftImageSourceRendered -= new EventHandler<imageSourceRenderedResultEventArgs>(leftimageSourceRendered);
			if (e.imgSource != null)
			{
				leftImageSource = e.imgSource;
				isRendering = false;
			}
		}

		private void rightimageSourceRendered(object sender, imageSourceRenderedResultEventArgs e)
		{
			rightImageSourceRendered -= new EventHandler<imageSourceRenderedResultEventArgs>(rightimageSourceRendered);
			if (e.imgSource != null)
			{
				rightImageSource = e.imgSource;
				isRendering = false;
			}
		}

		private void getPHEJDoubleBitmapImageAsync(CACodecTools caTool, byte[] curKey, string leftPagePath, string rightPagePath, float scal, int curPageIndex, Border border, EventHandler<imageSourceRenderedResultEventArgs> ImageSourceRendered, bool isSinglePage)
		{
			BitmapImage bitmapImage = new BitmapImage();
			Bitmap bitmap = null;
			Bitmap bitmap2 = null;
			Bitmap bitmap3 = null;
			try
			{
				bitmap = renPdfToBitmap(leftPagePath, curKey, 0, PDFdpi, scal, 0, border, isSinglePage);
				bitmap2 = renPdfToBitmap(rightPagePath, curKey, 0, PDFdpi, scal, 1, border, isSinglePage);
				int width = Convert.ToInt32(bitmap.Width + bitmap2.Width);
				int height = Convert.ToInt32(Math.Max(bitmap.Height, bitmap2.Height));
				bitmap3 = new Bitmap(width, height);
				using (Graphics graphics = Graphics.FromImage(bitmap3))
				{
					graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
					graphics.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
					graphics.DrawImage(bitmap2, bitmap.Width, 0, bitmap2.Width, bitmap2.Height);
					graphics.Dispose();
				}
				using (MemoryStream memoryStream = new MemoryStream())
				{
					bitmap3.Save(memoryStream, ImageFormat.Bmp);
					bitmapImage.BeginInit();
					bitmapImage.StreamSource = memoryStream;
					bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
					bitmapImage.EndInit();
					bitmapImage.CacheOption = BitmapCacheOption.None;
					bitmapImage.StreamSource.Close();
					bitmapImage.StreamSource = null;
					bitmapImage.Freeze();
					memoryStream.Dispose();
					memoryStream.Close();
					bitmap3.Dispose();
					bitmap3 = null;
				}
				bitmap = null;
				bitmap2 = null;
			}
			catch
			{
				bitmap = null;
				bitmap2 = null;
				bitmap3 = null;
			}
			if (ImageSourceRendered != null)
			{
				ImageSourceRendered(this, new imageSourceRenderedResultEventArgs(bitmapImage, curPageIndex, scal));
			}
		}

		private BitmapImage getHEJSingleBitmapImage(CACodecTools caTool, byte[] curKey)
		{
			BitmapImage bitmapImage = new BitmapImage();
			try
			{
				using (MemoryStream memoryStream = caTool.fileAESDecode(leftImagePath, curKey, false))
				{
					bitmapImage.BeginInit();
					bitmapImage.StreamSource = memoryStream;
					bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
					bitmapImage.EndInit();
					bitmapImage.CacheOption = BitmapCacheOption.None;
					bitmapImage.StreamSource.Close();
					bitmapImage.StreamSource = null;
					bitmapImage.Freeze();
					memoryStream.Dispose();
					memoryStream.Close();
					return bitmapImage;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private BitmapImage getHEJDoubleBitmapImage(CACodecTools caTool, byte[] curKey)
		{
			BitmapImage bitmapImage = new BitmapImage();
			try
			{
				using (MemoryStream memoryStream = caTool.fileAESDecode(leftImagePath, curKey, false))
				{
					using (MemoryStream memoryStream2 = caTool.fileAESDecode(rightImagePath, curKey, false))
					{
						Bitmap bitmap = new Bitmap(memoryStream);
						Bitmap bitmap2 = new Bitmap(memoryStream2);
						int width = Convert.ToInt32(bitmap.Width + bitmap2.Width);
						int height = Convert.ToInt32(Math.Max(bitmap.Height, bitmap2.Height));
						Bitmap bitmap3 = new Bitmap(width, height);
						using (Graphics graphics = Graphics.FromImage(bitmap3))
						{
							graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
							graphics.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
							graphics.DrawImage(bitmap2, bitmap.Width, 0, bitmap2.Width, bitmap2.Height);
							graphics.Dispose();
						}
						using (MemoryStream memoryStream3 = new MemoryStream())
						{
							bitmap3.Save(memoryStream3, ImageFormat.Bmp);
							bitmapImage.BeginInit();
							bitmapImage.StreamSource = memoryStream3;
							bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
							bitmapImage.EndInit();
							bitmapImage.CacheOption = BitmapCacheOption.None;
							bitmapImage.StreamSource.Close();
							bitmapImage.StreamSource = null;
							bitmapImage.Freeze();
							memoryStream3.Dispose();
							memoryStream3.Close();
							bitmap3.Dispose();
							bitmap3 = null;
						}
						memoryStream.Dispose();
						memoryStream.Close();
						memoryStream2.Dispose();
						memoryStream2.Close();
						bitmap = null;
						bitmap2 = null;
						return bitmapImage;
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private byte[] getByteArrayFromImage(BitmapImage imageC)
		{
			JpegBitmapEncoder jpegBitmapEncoder = new JpegBitmapEncoder();
			if (imageC.UriSource != null)
			{
				jpegBitmapEncoder.Frames.Add(BitmapFrame.Create(imageC.UriSource));
			}
			else
			{
				jpegBitmapEncoder.Frames.Add(BitmapFrame.Create(imageC));
			}
			using (MemoryStream memoryStream = new MemoryStream())
			{
				jpegBitmapEncoder.Save(memoryStream);
				byte[] result = memoryStream.ToArray();
				memoryStream.Close();
				memoryStream.Dispose();
				jpegBitmapEncoder = null;
				imageC = null;
				return result;
			}
		}
	}
}
