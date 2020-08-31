using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace PXCView36
{
	public class PXCV_Lib36
	{
		public delegate bool PXV36_CALLBACK_FUNC(int stage, int level, int param);

		public enum PXV36_CallbackStage
		{
			PXCVClb_Start = 1,
			PXCVClb_Processing,
			PXCVClb_Finish
		}

		public enum PXCV_RenderMode
		{
			pxvrm_Viewing,
			pxvrm_Printing,
			pxvrm_Exporting
		}

		public enum PXCV_ReleaseCachedDataFlags
		{
			pxvrcd_ReleaseDocumentImages = 1,
			pxvrcd_ReleaseDocumentFonts = 2,
			pxvrcd_ReleaseGlobalFonts = 4
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PXV_CommonRenderParameters
		{
			public IntPtr WholePageRect;

			public IntPtr DrawRect;

			public int Flags;

			public PXCV_RenderMode RenderTarget;
		}

		public enum PXV_CommonRenderParametersFlags
		{
			pxvrpf_None = 0,
			pxvrpf_Rotate_NoRotate = 0,
			pxvrpf_Rotate_Rotate90CW = 1,
			pxvrpf_Rotate_Rotate180 = 2,
			pxvrpf_Rotate_Rotate90CCW = 3,
			pxvrpf_Rotate_RotateMask = 3,
			pxvrpf_UseVectorRenderer = 4
		}

		[DllImport("pxcview")]
		public static extern int PXCV_Init(out IntPtr hDoc, string Key, string DevCode);

		[DllImport("pxcview")]
		public static extern int PXCV_Delete(IntPtr hDoc);

		[DllImport("pxcview")]
		public static extern int PXCV_SetCallBack(IntPtr hDoc, PXV36_CALLBACK_FUNC pProc, int UserData);

		[DllImport("pxcview")]
		public static extern int PXCV_ReadDocumentW(IntPtr hDoc, [MarshalAs(UnmanagedType.LPWStr)] string pwFileName, int Flags);

		[DllImport("pxcview")]
		public static extern int PXCV_CheckPassword(IntPtr hDoc, byte[] pPassword, int PassLen);

		[DllImport("pxcview")]
		public static extern int PXCV_FinishReadDocument(IntPtr hDoc, int Flags);

		[DllImport("pxcview")]
		public static extern int PXCV_GetPagesCount(IntPtr hDoc, out int count);

		[DllImport("pxcview")]
		public static extern int PXCV_GetPageDimensions(IntPtr hDoc, int page_num, out double width, out double height);

		[DllImport("pxcview")]
		public static extern int PXCV_GetPageRotation(IntPtr hDoc, int page_num, ref int angle);

		[DllImport("pxcview")]
		public static extern int PXCV_DrawPageToDC(IntPtr hDoc, int page_num, IntPtr hDC, ref PXV_CommonRenderParameters pParams);

		[DllImport("pxcview")]
		public static extern int PXCV_DrawPageToDIBSection(IntPtr hDoc, int page_num, ref PXV_CommonRenderParameters pParams, IntPtr hBaseDC, int backcolor, ref IntPtr hBitmap, IntPtr hSection, int dwOffset);

		[DllImport("pxcview")]
		public static extern int PXCV_ReadDocumentFromIStream(IntPtr hDoc, IStream aIStream, int dwFlags);

		[DllImport("pxcview")]
		public static extern int PXCV_ReadDocumentFromMemory(IntPtr hDoc, byte[] pMemory, uint size, int dwFlags);

		[DllImport("pxcview")]
		public static extern int PXCV_ReleasePageCachedData(IntPtr hDoc, int page_num, int dwFlags);

		[DllImport("pxcview")]
		public static extern int PXCV_ReleaseCachedData(IntPtr hDoc, int dwFlags);

		[DllImport("pxcview")]
		public static extern int PXCV_Err_FormatSeverity(int errorcode, byte[] buf, int maxlen);

		[DllImport("pxcview")]
		public static extern int PXCV_Err_FormatFacility(int errorcode, byte[] buf, int maxlen);

		[DllImport("pxcview")]
		public static extern int PXCV_Err_FormatErrorCode(int errorcode, byte[] buf, int maxlen);
	}
}
