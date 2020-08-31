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

namespace ReadPageModule
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
		private sealed class a
		{
			public ReadPagePair a;

			public CACodecTools b;

			public byte[] c;

			public float d;

			public Border e;

			public bool f;

			internal void g()
			{
				a.a(b, c, a.leftImagePath, a.rightImagePath, d, a.leftPageIndex, e, new EventHandler<imageSourceRenderedResultEventArgs>(a.c), f);
			}

			internal void h()
			{
				a.a(b, c, a.leftImagePath, d, a.leftPageIndex, e, new EventHandler<imageSourceRenderedResultEventArgs>(a.c), f);
			}

			internal void i()
			{
				a.a(b, c, a.rightImagePath, d, a.leftPageIndex, e, new EventHandler<imageSourceRenderedResultEventArgs>(a.c), f);
			}
		}

		[CompilerGenerated]
		private sealed class b
		{
			public ReadPagePair a;

			public CACodecTools b;

			public byte[] c;

			public Border d;

			public bool e;

			internal void f()
			{
				a.a(b, c, a.leftImagePath, a.rightImagePath, a.PDFScale, a.leftPageIndex, d, new EventHandler<imageSourceRenderedResultEventArgs>(a.b), e);
			}

			internal void g()
			{
				a.a(b, c, a.leftImagePath, a.PDFScale, a.leftPageIndex, d, new EventHandler<imageSourceRenderedResultEventArgs>(a.b), e);
			}

			internal void h()
			{
				a.a(b, c, a.rightImagePath, a.PDFScale, a.leftPageIndex, d, new EventHandler<imageSourceRenderedResultEventArgs>(a.b), e);
			}
		}

		[CompilerGenerated]
		private EventHandler<imageSourceRenderedResultEventArgs> m_a;

		[CompilerGenerated]
		private EventHandler<imageSourceRenderedResultEventArgs> m_b;

		[CompilerGenerated]
		private EventHandler<imageSourceRenderedResultEventArgs> m_c;

		[CompilerGenerated]
		private EventHandler<imageSourceRenderedResultEventArgs> d;

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

		private int e = 96;

		public float PDFScale = 1f;

		public double baseScale = 1.0;

		public int zoomStep;

		public bool isRendering;

		public List<Thread> zoomThread = new List<Thread>();

		private DateTime f;

		private bool g;

		private int h = 9;

		public event EventHandler<imageSourceRenderedResultEventArgs> leftImageSourceRendered
		{
			[CompilerGenerated]
			add
			{
				EventHandler<imageSourceRenderedResultEventArgs> eventHandler = this.m_a;
				EventHandler<imageSourceRenderedResultEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<imageSourceRenderedResultEventArgs> value2 = (EventHandler<imageSourceRenderedResultEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange(ref this.m_a, value2, eventHandler2);
				}
				while ((object)eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<imageSourceRenderedResultEventArgs> eventHandler = this.m_a;
				EventHandler<imageSourceRenderedResultEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<imageSourceRenderedResultEventArgs> value2 = (EventHandler<imageSourceRenderedResultEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange(ref this.m_a, value2, eventHandler2);
				}
				while ((object)eventHandler != eventHandler2);
			}
		}

		public event EventHandler<imageSourceRenderedResultEventArgs> rightImageSourceRendered
		{
			[CompilerGenerated]
			add
			{
				EventHandler<imageSourceRenderedResultEventArgs> eventHandler = this.m_b;
				EventHandler<imageSourceRenderedResultEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<imageSourceRenderedResultEventArgs> value2 = (EventHandler<imageSourceRenderedResultEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange(ref this.m_b, value2, eventHandler2);
				}
				while ((object)eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<imageSourceRenderedResultEventArgs> eventHandler = this.m_b;
				EventHandler<imageSourceRenderedResultEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<imageSourceRenderedResultEventArgs> value2 = (EventHandler<imageSourceRenderedResultEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange(ref this.m_b, value2, eventHandler2);
				}
				while ((object)eventHandler != eventHandler2);
			}
		}

		private event EventHandler<imageSourceRenderedResultEventArgs> resizeImageSourceRendered
		{
			[CompilerGenerated]
			add
			{
				EventHandler<imageSourceRenderedResultEventArgs> eventHandler = this.m_c;
				EventHandler<imageSourceRenderedResultEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<imageSourceRenderedResultEventArgs> value2 = (EventHandler<imageSourceRenderedResultEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange(ref this.m_c, value2, eventHandler2);
				}
				while ((object)eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<imageSourceRenderedResultEventArgs> eventHandler = this.m_c;
				EventHandler<imageSourceRenderedResultEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<imageSourceRenderedResultEventArgs> value2 = (EventHandler<imageSourceRenderedResultEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange(ref this.m_c, value2, eventHandler2);
				}
				while ((object)eventHandler != eventHandler2);
			}
		}

		public event EventHandler<imageSourceRenderedResultEventArgs> sendresizeImageSourceRendered
		{
			[CompilerGenerated]
			add
			{
				EventHandler<imageSourceRenderedResultEventArgs> eventHandler = d;
				EventHandler<imageSourceRenderedResultEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<imageSourceRenderedResultEventArgs> value2 = (EventHandler<imageSourceRenderedResultEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange(ref d, value2, eventHandler2);
				}
				while ((object)eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<imageSourceRenderedResultEventArgs> eventHandler = d;
				EventHandler<imageSourceRenderedResultEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<imageSourceRenderedResultEventArgs> value2 = (EventHandler<imageSourceRenderedResultEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange(ref d, value2, eventHandler2);
				}
				while ((object)eventHandler != eventHandler2);
			}
		}

		public ReadPagePair(int leftPageIndex, int rightPageIndex, string leftImgPath, string rightImgPath, string _leftPageId, string _rightPageId, int PDFdpi)
		{
			e = PDFdpi;
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
			a a = new a();
			a.a = this;
			a.b = caTool;
			a.c = curKey;
			a.d = PDFScale;
			a.e = border;
			a.f = isSinglePage;
			f = resizeTime;
			this.PDFScale = a.d;
			this.zoomStep = zoomStep;
			if (leftImagePath.Length > 0)
			{
				if (rightImagePath.Length > 0)
				{
					Thread thread = new Thread(new ThreadStart(a.g));
					thread.Name = a.d.ToString();
					zoomThread.Add(thread);
				}
				else
				{
					Thread thread2 = new Thread(new ThreadStart(a.h));
					thread2.Name = a.d.ToString();
					zoomThread.Add(thread2);
				}
			}
			else if (rightImagePath.Length > 0)
			{
				Thread thread3 = new Thread(new ThreadStart(a.i));
				thread3.Name = a.d.ToString();
				zoomThread.Add(thread3);
			}
			if (zoomThread.Count.Equals(0) || g)
			{
				return;
			}
			for (int num = zoomThread.Count - 1; num >= 0; num--)
			{
				if (a.d.Equals((float)Convert.ToDouble(zoomThread[num].Name)))
				{
					try
					{
						resizeImageSourceRendered += new EventHandler<imageSourceRenderedResultEventArgs>(c);
						zoomThread[num].Start();
						zoomThread.Clear();
						g = true;
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
					leftImageSource = a(caTool, curKey);
				}
				else
				{
					leftImageSource = b(caTool, curKey);
				}
			}
			else if (rightImagePath.Length > 0)
			{
				rightImageSource = b(caTool, curKey);
			}
		}

		public void createLargePHEJBitmapImage(CACodecTools caTool, byte[] curKey, Border border, bool isSinglePage)
		{
			b b = new b();
			b.a = this;
			b.b = caTool;
			b.c = curKey;
			b.d = border;
			b.e = isSinglePage;
			if (leftImagePath.Length > 0)
			{
				if (rightImagePath.Length > 0)
				{
					leftImageSourceRendered += new EventHandler<imageSourceRenderedResultEventArgs>(this.b);
					Thread thread = new Thread(new ThreadStart(b.f));
					thread.Name = PDFScale.ToString();
					thread.Start();
					isRendering = true;
				}
				else
				{
					leftImageSourceRendered += new EventHandler<imageSourceRenderedResultEventArgs>(this.b);
					Thread thread2 = new Thread(new ThreadStart(b.g));
					thread2.Name = PDFScale.ToString();
					thread2.Start();
					isRendering = true;
				}
			}
			else if (rightImagePath.Length > 0)
			{
				rightImageSourceRendered += new EventHandler<imageSourceRenderedResultEventArgs>(a);
				Thread thread3 = new Thread(new ThreadStart(b.h));
				thread3.Name = PDFScale.ToString();
				thread3.Start();
				isRendering = true;
			}
		}

		private Bitmap a(CACodecTools A_0, string A_1, byte[] A_2, int A_3, int A_4, float A_5, int A_6, Border A_7, bool A_8)
		{
			System.Drawing.Color white = System.Drawing.Color.White;
			Bitmap aBitmap = null;
			try
			{
				if (decodedPDFPages[A_6] == null)
				{
					decodedPDFPages[A_6] = A_0.fileAESDecode(A_1, A_2);
				}
			}
			catch (Exception ex)
			{
				decodedPDFPages[A_6] = null;
				throw ex;
			}
			try
			{
				PDFDoc pDFDoc = new PDFDoc();
				pDFDoc.Init("PVD20-M4IRG-QYZK9-MNJ2U-DFTK1-MAJ4L", "PDFX3$Henry$300604_Allnuts#");
				pDFDoc.OpenFromMemory(decodedPDFPages[A_6], (uint)decodedPDFPages[A_6].Length, 0);
				PXCV_Lib36.PXV_CommonRenderParameters aCommonRenderParams = a(pDFDoc, A_4, A_3, A_5, 0, 0, A_7, A_8);
				pDFDoc.DrawPageToDIBSection(IntPtr.Zero, A_3, white, aCommonRenderParams, out aBitmap);
				pDFDoc.ReleasePageCachedData(A_3, 1);
				pDFDoc.Delete();
				return aBitmap;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		private PXCV_Lib36.PXV_CommonRenderParameters a(PDFDoc A_0, int A_1, int A_2, float A_3, int A_4, int A_5, Border A_6, bool A_7)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(PXCV_Helper.RECT)));
			IntPtr intPtr2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(PXCV_Helper.RECT)));
			Point point = new Point(A_4, A_5);
			Size empty = Size.Empty;
			PXCV_Helper.RECT rECT = default(PXCV_Helper.RECT);
			PXCV_Helper.RECT rECT2 = default(PXCV_Helper.RECT);
			PXCV_Lib36.PXV_CommonRenderParameters result = default(PXCV_Lib36.PXV_CommonRenderParameters);
			PageDimension pageDimension = default(PageDimension);
			A_0.GetPageDimensions(A_2, out pageDimension.w, out pageDimension.h);
			double num = A_6.ActualHeight / 96.0 * (double)A_1;
			double num2 = A_6.ActualWidth / 96.0 * (double)A_1;
			if (zoomStep == 0)
			{
				empty.Width = (int)(pageDimension.w / 72.0 * (double)A_1 * (double)A_3);
				empty.Height = (int)(pageDimension.h / 72.0 * (double)A_1 * (double)A_3);
				double num3 = num2 / num;
				double num4 = 0.0;
				num4 = ((!A_7) ? ((double)(empty.Width * 2) / (double)empty.Height) : ((double)empty.Width / (double)empty.Height));
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
				empty.Width = (int)(pageDimension.w / 72.0 * (double)A_1 * (double)A_3 * baseScale);
				empty.Height = (int)(pageDimension.h / 72.0 * (double)A_1 * (double)A_3 * baseScale);
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

		private void a(CACodecTools A_0, byte[] A_1, string A_2, float A_3, int A_4, Border A_5, EventHandler<imageSourceRenderedResultEventArgs> A_6, bool A_7)
		{
			BitmapImage bitmapImage = new BitmapImage();
			if (!A_2.Contains("tryPageEndLarge"))
			{
				Bitmap bitmap = null;
				try
				{
					bitmap = a(A_0, A_2, A_1, 0, e, A_3, 0, A_5, A_7);
					using (MemoryStream memoryStream = new MemoryStream())
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
				}
				catch (Exception)
				{
					bitmapImage = null;
					bitmap = null;
				}
				if (A_6 != null)
				{
					A_6(this, new imageSourceRenderedResultEventArgs(bitmapImage, A_4, A_3));
				}
			}
		}

		private void c(object A_0, imageSourceRenderedResultEventArgs A_1)
		{
			resizeImageSourceRendered -= new EventHandler<imageSourceRenderedResultEventArgs>(c);
			g = false;
			if (leftPageIndex.Equals(A_1.renderPageIndex))
			{
				if (PDFScale.Equals(A_1.sourceScale))
				{
					EventHandler<imageSourceRenderedResultEventArgs> eventHandler = d;
					if (eventHandler != null)
					{
						eventHandler(A_0, A_1);
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
							resizeImageSourceRendered += new EventHandler<imageSourceRenderedResultEventArgs>(c);
							g = true;
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
				g = false;
				zoomThread.Clear();
			}
		}

		private void b(object A_0, imageSourceRenderedResultEventArgs A_1)
		{
			leftImageSourceRendered -= new EventHandler<imageSourceRenderedResultEventArgs>(b);
			if (A_1.imgSource != null)
			{
				leftImageSource = A_1.imgSource;
				isRendering = false;
			}
		}

		private void a(object A_0, imageSourceRenderedResultEventArgs A_1)
		{
			rightImageSourceRendered -= new EventHandler<imageSourceRenderedResultEventArgs>(a);
			if (A_1.imgSource != null)
			{
				rightImageSource = A_1.imgSource;
				isRendering = false;
			}
		}

		private void a(CACodecTools A_0, byte[] A_1, string A_2, string A_3, float A_4, int A_5, Border A_6, EventHandler<imageSourceRenderedResultEventArgs> A_7, bool A_8)
		{
			BitmapImage bitmapImage = new BitmapImage();
			Bitmap bitmap = null;
			Bitmap bitmap2 = null;
			Bitmap bitmap3 = null;
			bool flag = false;
			try
			{
				if (A_2.Contains("tryPageEndLarge"))
				{
					if (A_2.Contains("tryPageEndLarge.pdf"))
					{
						A_2 = A_2.Replace("tryPageEndLarge.pdf", "tryPageEnd.jpg");
					}
					else if (A_2.Contains("tryPageEndLargeNTPC.pdf"))
					{
						A_2 = A_2.Replace("tryPageEndLargeNTPC.pdf", "tryPageEndNTPC.jpg");
					}
					flag = true;
					BitmapImage source = new BitmapImage(new Uri(A_2, UriKind.RelativeOrAbsolute));
					using (MemoryStream stream = new MemoryStream())
					{
						BmpBitmapEncoder bmpBitmapEncoder = new BmpBitmapEncoder();
						bmpBitmapEncoder.Frames.Add(BitmapFrame.Create(source));
						bmpBitmapEncoder.Save(stream);
						bitmap = new Bitmap(stream);
					}
				}
				else
				{
					bitmap = a(A_0, A_2, A_1, 0, e, A_4, 0, A_6, A_8);
				}
			}
			catch (Exception)
			{
				bitmap = null;
				bitmap2 = null;
				bitmap3 = null;
				bitmapImage = null;
				if (A_7 != null)
				{
					A_7(this, new imageSourceRenderedResultEventArgs(bitmapImage, A_5, A_4));
				}
				return;
			}
			try
			{
				if (A_3.Contains("tryPageEndLarge"))
				{
					if (A_3.Contains("tryPageEndLarge.pdf"))
					{
						A_3 = A_3.Replace("tryPageEndLarge.pdf", "tryPageEnd.jpg");
					}
					else if (A_3.Contains("tryPageEndLargeNTPC.pdf"))
					{
						A_3 = A_3.Replace("tryPageEndLargeNTPC.pdf", "tryPageEndNTPC.jpg");
					}
					flag = true;
					BitmapImage source2 = new BitmapImage(new Uri(A_3, UriKind.RelativeOrAbsolute));
					using (MemoryStream stream2 = new MemoryStream())
					{
						BmpBitmapEncoder bmpBitmapEncoder2 = new BmpBitmapEncoder();
						bmpBitmapEncoder2.Frames.Add(BitmapFrame.Create(source2));
						bmpBitmapEncoder2.Save(stream2);
						bitmap2 = new Bitmap(stream2);
					}
				}
				else
				{
					bitmap2 = a(A_0, A_3, A_1, 0, e, A_4, 1, A_6, A_8);
				}
			}
			catch (Exception)
			{
				bitmap = null;
				bitmap2 = null;
				bitmap3 = null;
				bitmapImage = null;
				if (A_7 != null)
				{
					A_7(this, new imageSourceRenderedResultEventArgs(bitmapImage, A_5, A_4));
				}
				return;
			}
			try
			{
				int width = Convert.ToInt32(bitmap.Width + bitmap2.Width);
				int height = Convert.ToInt32(Math.Max(bitmap.Height, bitmap2.Height));
				if (flag)
				{
					width = Math.Min(bitmap.Width, bitmap2.Width) * 2;
					height = Math.Min(bitmap.Height, bitmap2.Height);
					if (bitmap.Width > bitmap2.Width)
					{
						bitmap = ResizeImage(bitmap, new Size(bitmap2.Width, bitmap2.Height));
						width = bitmap2.Width * 2;
					}
					else
					{
						bitmap2 = ResizeImage(bitmap2, new Size(bitmap.Width, bitmap.Height));
						width = bitmap.Width * 2;
					}
				}
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
				bitmapImage = null;
				if (A_7 != null)
				{
					A_7(this, new imageSourceRenderedResultEventArgs(bitmapImage, A_5, A_4));
				}
				return;
			}
			if (A_7 != null)
			{
				A_7(this, new imageSourceRenderedResultEventArgs(bitmapImage, A_5, A_4));
			}
		}

		public static Bitmap ResizeImage(Bitmap imgToResize, Size size)
		{
			try
			{
				Bitmap bitmap = new Bitmap(size.Width, size.Height);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
					graphics.DrawImage(imgToResize, 0, 0, size.Width, size.Height);
				}
				return bitmap;
			}
			catch
			{
			}
			return null;
		}

		private BitmapImage b(CACodecTools A_0, byte[] A_1)
		{
			BitmapImage bitmapImage = new BitmapImage();
			try
			{
				using (MemoryStream memoryStream = A_0.fileAESDecode(leftImagePath, A_1, false))
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

		private BitmapImage a(CACodecTools A_0, byte[] A_1)
		{
			BitmapImage bitmapImage = new BitmapImage();
			try
			{
				using (MemoryStream memoryStream = A_0.fileAESDecode(leftImagePath, A_1, false))
				{
					using (MemoryStream memoryStream2 = A_0.fileAESDecode(rightImagePath, A_1, false))
					{
						Bitmap bitmap = new Bitmap(memoryStream);
						Bitmap bitmap2 = new Bitmap(memoryStream2);
						int width = Convert.ToInt32(bitmap.Width + bitmap2.Width);
						int num = Convert.ToInt32(Math.Max(bitmap.Height, bitmap2.Height));
						if (bitmap.Width == bitmap.Height)
						{
							num--;
						}
						Bitmap bitmap3 = new Bitmap(width, num);
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

		private byte[] a(BitmapImage A_0)
		{
			JpegBitmapEncoder jpegBitmapEncoder = new JpegBitmapEncoder();
			if (A_0.UriSource != null)
			{
				jpegBitmapEncoder.Frames.Add(BitmapFrame.Create(A_0.UriSource));
			}
			else
			{
				jpegBitmapEncoder.Frames.Add(BitmapFrame.Create(A_0));
			}
			using (MemoryStream memoryStream = new MemoryStream())
			{
				jpegBitmapEncoder.Save(memoryStream);
				byte[] result = memoryStream.ToArray();
				memoryStream.Close();
				memoryStream.Dispose();
				jpegBitmapEncoder = null;
				A_0 = null;
				return result;
			}
		}
	}
}
