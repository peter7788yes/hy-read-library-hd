using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace PXCView36
{
	public class PDFDoc : IDisposable
	{
		private IntPtr m_Doc = IntPtr.Zero;

		private bool m_bDisposed;

		public bool IsInitialized
		{
			get
			{
				return !m_bDisposed;
			}
		}

		[DllImport("gdi32")]
		public static extern int DeleteObject(IntPtr hObject);

		public PDFDoc()
		{
			m_bDisposed = true;
		}

		~PDFDoc()
		{
			Dispose(false);
		}

		void IDisposable.Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool bDisposing)
		{
			Delete();
		}

		public void Init(string aKey, string aDevCode)
		{
			if (!m_bDisposed)
			{
				throw new ApplicationException("PDFLib.Init");
			}
			if (PXCV_Error.IS_DS_FAILED(PXCV_Lib36.PXCV_Init(out m_Doc, aKey, aDevCode)))
			{
				throw new ApplicationException("PDFDoc.Init.PXCV_Init");
			}
			m_bDisposed = false;
		}

		public void Delete()
		{
			if (!m_bDisposed)
			{
				if (PXCV_Error.IS_DS_FAILED(PXCV_Lib36.PXCV_Delete(m_Doc)))
				{
					throw new ApplicationException("PDFDoc.Delete.PXCV_Delete");
				}
				m_bDisposed = true;
			}
		}

		public void Open(string aFileName)
		{
			if (m_bDisposed)
			{
				throw new ApplicationException("PDFDoc.OpenFromIStream");
			}
			if (PXCV_Error.IS_DS_FAILED(PXCV_Lib36.PXCV_ReadDocumentW(m_Doc, aFileName, 0)))
			{
				throw new ApplicationException("PDFDoc.Open.PXCV_ReadDocumentW");
			}
		}

		public void OpenFromIStream(IStream aIStream, int flags)
		{
			if (m_bDisposed)
			{
				throw new ApplicationException("PDFDoc.OpenFromIStream");
			}
			if (PXCV_Error.IS_DS_FAILED(PXCV_Lib36.PXCV_ReadDocumentFromIStream(m_Doc, aIStream, flags)))
			{
				throw new ApplicationException("PDFDoc.OpenFromPXCV_ReadDocumentFromIStream");
			}
		}

		public void OpenFromMemory(byte[] pMemory, uint size, int flags)
		{
			if (m_bDisposed)
			{
				throw new ApplicationException("PDFDoc.OpenFromMemory");
			}
			if (PXCV_Error.IS_DS_FAILED(PXCV_Lib36.PXCV_ReadDocumentFromMemory(m_Doc, pMemory, size, flags)))
			{
				throw new ApplicationException("PDFDoc.OpenFromPXCV_ReadDocumentFromMemory");
			}
		}

		public void GetPageDimensions(int pn, out double w, out double h)
		{
			if (m_bDisposed)
			{
				throw new ApplicationException("PDFDoc.GetPageDimensions");
			}
			if (PXCV_Error.IS_DS_FAILED(PXCV_Lib36.PXCV_GetPageDimensions(m_Doc, pn, out w, out h)))
			{
				throw new ApplicationException("PDFDoc.OpenFromPXCV_GetPageDimensions");
			}
		}

		public int GetPagesCount()
		{
			int count = 0;
			if (m_bDisposed)
			{
				throw new ApplicationException("PDFDoc.GetPagesCount");
			}
			if (PXCV_Error.IS_DS_FAILED(PXCV_Lib36.PXCV_GetPagesCount(m_Doc, out count)))
			{
				throw new ApplicationException("PDFDoc.OpenFromPXCV_GetPageDimensions");
			}
			return count;
		}

		public void ReleasePageCachedData(int pn, int flags)
		{
			if (m_bDisposed)
			{
				throw new ApplicationException("PDFDoc.DrawPageToDIBSection");
			}
			if (PXCV_Error.IS_DS_FAILED(PXCV_Lib36.PXCV_ReleasePageCachedData(m_Doc, pn, flags)))
			{
				throw new ApplicationException("PDFDoc.ReleasePageCachedData.PXCV_ReleasePageCachedData");
			}
		}

		public void ReleaseCachedData(int flags)
		{
			if (m_bDisposed)
			{
				throw new ApplicationException("PDFDoc.ReleaseCachedData");
			}
			if (PXCV_Error.IS_DS_FAILED(PXCV_Lib36.PXCV_ReleaseCachedData(m_Doc, flags)))
			{
				throw new ApplicationException("PDFDoc.ReleaseCachedData.PXCV_ReleaseCachedData");
			}
		}

		public void DrawPageToDIBSection(IntPtr hBaseDC, int pn, Color aBkgColor, PXCV_Lib36.PXV_CommonRenderParameters aCommonRenderParams, out Bitmap aBitmap)
		{
			aBitmap = null;
			if (m_bDisposed)
			{
				throw new ApplicationException("PDFDoc.DrawPageToDIBSection");
			}
			IntPtr hBitmap = IntPtr.Zero;
			int backcolor = aBkgColor.ToArgb();
			if (PXCV_Error.IS_DS_FAILED(PXCV_Lib36.PXCV_DrawPageToDIBSection(m_Doc, pn, ref aCommonRenderParams, hBaseDC, backcolor, ref hBitmap, IntPtr.Zero, 0)))
			{
				throw new ApplicationException("PDFDoc.DrawPageToDIBSection.PXCV_DrawPageToDIBSection");
			}
			aBitmap = Image.FromHbitmap(hBitmap);
			DeleteObject(hBitmap);
		}

		public void DrawPageToDC(IntPtr hDC, int pn, PXCV_Lib36.PXV_CommonRenderParameters aCommonRenderParams)
		{
			if (m_bDisposed)
			{
				throw new ApplicationException("PDFDoc.DrawPage");
			}
			if (PXCV_Error.IS_DS_FAILED(PXCV_Lib36.PXCV_DrawPageToDC(m_Doc, pn, hDC, ref aCommonRenderParams)))
			{
				throw new ApplicationException("PDFDoc.DrawPageToDC.PXCV_DrawPageToDC");
			}
		}
	}
}
