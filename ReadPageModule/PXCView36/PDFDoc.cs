using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace PXCView36
{
	public class PDFDoc : IDisposable
	{
		private IntPtr m_a = IntPtr.Zero;

		private bool b;

		public bool IsInitialized
		{
			get
			{
				return !b;
			}
		}

		[DllImport("gdi32")]
		public static extern int DeleteObject(IntPtr hObject);

		public PDFDoc()
		{
			b = true;
		}

		~PDFDoc()
		{
			a(false);
		}

		void IDisposable.Dispose()
		{
			a(true);
			GC.SuppressFinalize(this);
		}

		private void a(bool A_0)
		{
			Delete();
		}

		public void Init(string aKey, string aDevCode)
		{
			if (!b)
			{
				throw new ApplicationException("PDFLib.Init");
			}
			if (PXCV_Error.IS_DS_FAILED(PXCV_Lib36.PXCV_Init(out this.m_a, aKey, aDevCode)))
			{
				throw new ApplicationException("PDFDoc.Init.PXCV_Init");
			}
			b = false;
		}

		public void Delete()
		{
			if (!b)
			{
				if (PXCV_Error.IS_DS_FAILED(PXCV_Lib36.PXCV_Delete(this.m_a)))
				{
					throw new ApplicationException("PDFDoc.Delete.PXCV_Delete");
				}
				b = true;
			}
		}

		public void Open(string aFileName)
		{
			if (b)
			{
				throw new ApplicationException("PDFDoc.OpenFromIStream");
			}
			if (PXCV_Error.IS_DS_FAILED(PXCV_Lib36.PXCV_ReadDocumentW(this.m_a, aFileName, 0)))
			{
				throw new ApplicationException("PDFDoc.Open.PXCV_ReadDocumentW");
			}
		}

		public void OpenFromIStream(IStream aIStream, int flags)
		{
			if (b)
			{
				throw new ApplicationException("PDFDoc.OpenFromIStream");
			}
			if (PXCV_Error.IS_DS_FAILED(PXCV_Lib36.PXCV_ReadDocumentFromIStream(this.m_a, aIStream, flags)))
			{
				throw new ApplicationException("PDFDoc.OpenFromPXCV_ReadDocumentFromIStream");
			}
		}

		public void OpenFromMemory(byte[] pMemory, uint size, int flags)
		{
			if (b)
			{
				throw new ApplicationException("PDFDoc.OpenFromMemory");
			}
			if (PXCV_Error.IS_DS_FAILED(PXCV_Lib36.PXCV_ReadDocumentFromMemory(this.m_a, pMemory, size, flags)))
			{
				throw new ApplicationException("PDFDoc.OpenFromPXCV_ReadDocumentFromMemory");
			}
		}

		public void GetPageDimensions(int pn, out double w, out double h)
		{
			if (b)
			{
				throw new ApplicationException("PDFDoc.GetPageDimensions");
			}
			if (PXCV_Error.IS_DS_FAILED(PXCV_Lib36.PXCV_GetPageDimensions(this.m_a, pn, out w, out h)))
			{
				throw new ApplicationException("PDFDoc.OpenFromPXCV_GetPageDimensions");
			}
		}

		public int GetPagesCount()
		{
			int count = 0;
			if (b)
			{
				throw new ApplicationException("PDFDoc.GetPagesCount");
			}
			if (PXCV_Error.IS_DS_FAILED(PXCV_Lib36.PXCV_GetPagesCount(this.m_a, out count)))
			{
				throw new ApplicationException("PDFDoc.OpenFromPXCV_GetPageDimensions");
			}
			return count;
		}

		public void ReleasePageCachedData(int pn, int flags)
		{
			if (b)
			{
				throw new ApplicationException("PDFDoc.DrawPageToDIBSection");
			}
			if (PXCV_Error.IS_DS_FAILED(PXCV_Lib36.PXCV_ReleasePageCachedData(this.m_a, pn, flags)))
			{
				throw new ApplicationException("PDFDoc.ReleasePageCachedData.PXCV_ReleasePageCachedData");
			}
		}

		public void ReleaseCachedData(int flags)
		{
			if (b)
			{
				throw new ApplicationException("PDFDoc.ReleaseCachedData");
			}
			if (PXCV_Error.IS_DS_FAILED(PXCV_Lib36.PXCV_ReleaseCachedData(this.m_a, flags)))
			{
				throw new ApplicationException("PDFDoc.ReleaseCachedData.PXCV_ReleaseCachedData");
			}
		}

		public void DrawPageToDIBSection(IntPtr hBaseDC, int pn, Color aBkgColor, PXCV_Lib36.PXV_CommonRenderParameters aCommonRenderParams, out Bitmap aBitmap)
		{
			aBitmap = null;
			if (b)
			{
				throw new ApplicationException("PDFDoc.DrawPageToDIBSection");
			}
			IntPtr hBitmap = IntPtr.Zero;
			int backcolor = aBkgColor.ToArgb();
			if (PXCV_Error.IS_DS_FAILED(PXCV_Lib36.PXCV_DrawPageToDIBSection(this.m_a, pn, ref aCommonRenderParams, hBaseDC, backcolor, ref hBitmap, IntPtr.Zero, 0)))
			{
				throw new ApplicationException("PDFDoc.DrawPageToDIBSection.PXCV_DrawPageToDIBSection");
			}
			aBitmap = Image.FromHbitmap(hBitmap);
			DeleteObject(hBitmap);
			GC.Collect();
		}

		public void DrawPageToDC(IntPtr hDC, int pn, PXCV_Lib36.PXV_CommonRenderParameters aCommonRenderParams)
		{
			if (b)
			{
				throw new ApplicationException("PDFDoc.DrawPage");
			}
			if (PXCV_Error.IS_DS_FAILED(PXCV_Lib36.PXCV_DrawPageToDC(this.m_a, pn, hDC, ref aCommonRenderParams)))
			{
				throw new ApplicationException("PDFDoc.DrawPageToDC.PXCV_DrawPageToDC");
			}
		}
	}
}
