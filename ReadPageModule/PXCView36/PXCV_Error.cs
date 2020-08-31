using System.Windows.Forms;

namespace PXCView36
{
	public class PXCV_Error
	{
		public const int PS_ERR_NOTIMPLEMENTED = -2112617296;

		public const int PS_ERR_INVALID_ARG = -2112618495;

		public const int PS_ERR_MEMALLOC = -2112617496;

		public const int PS_ERR_USER_BREAK = -2112617996;

		public const int PS_ERR_INTERNAL = -2112618479;

		public const int PS_ERR_INVALID_FILE_FORMAT = -2112618494;

		public const int PS_ERR_REQUIRED_PROP_NOT_SET = -2112608490;

		public const int PS_ERR_INVALID_PROP_TYPE = -2112608489;

		public const int PS_ERR_INVALID_PROP_VALUE = -2112608488;

		public const int PS_ERR_INVALID_OBJECT_NUM = -2112608487;

		public const int PS_ERR_INVALID_PS_OPERATOR = -2112608484;

		public const int PS_ERR_UNKNOWN_OPERATOR = -2112608377;

		public const int PS_ERR_INVALID_CONTENT_STATE = -2112608376;

		public const int PS_ERR_NoPassword = -2112608344;

		public const int PS_ERR_UnknowCryptFlt = -2112608343;

		public const int PS_ERR_WrongPassword = -2112608342;

		public const int PS_ERR_InvlaidObjStruct = -2112608341;

		public const int PS_ERR_WrongEncryptDict = -2112608340;

		public const int PS_ERR_DocEncrypted = -2112608339;

		public const int PS_ERR_DocNOTEncrypted = -2112608338;

		public const int PS_ERR_WrongObjStream = -2112608337;

		public const int PS_ERR_WrongTrailer = -2112608336;

		public const int PS_ERR_WrongXRef = -2112608335;

		public const int PS_ERR_WrongDecodeParms = -2112608334;

		public const int PS_ERR_XRefNotFounded = -2112608333;

		public const int PS_ERR_DocAlreadyRead = -2112608332;

		public const int PS_ERR_DocNotRead = -2112608331;

		public static bool IS_DS_SUCCESSFUL(int x)
		{
			return (x & 2147483648u) == 0;
		}

		public static bool IS_DS_FAILED(int x)
		{
			return (x & 2147483648u) != 0;
		}

		public static void ShowDSErrorString(IWin32Window owner, int x)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			byte[] buf = null;
			byte[] buf2 = null;
			byte[] buf3 = null;
			num = PXCV_Lib36.PXCV_Err_FormatSeverity(x, buf, 0);
			num2 = PXCV_Lib36.PXCV_Err_FormatFacility(x, buf2, 0);
			num3 = PXCV_Lib36.PXCV_Err_FormatErrorCode(x, buf3, 0);
			buf = new byte[num];
			buf2 = new byte[num2];
			buf3 = new byte[num3];
			string str = "";
			if (PXCV_Lib36.PXCV_Err_FormatSeverity(x, buf, num) > 0)
			{
				str = PXCV_Helper.BytesToString(buf, num);
			}
			str += " [";
			if (PXCV_Lib36.PXCV_Err_FormatFacility(x, buf2, num2) > 0)
			{
				str += PXCV_Helper.BytesToString(buf2, num2);
			}
			str += "]: ";
			if (PXCV_Lib36.PXCV_Err_FormatErrorCode(x, buf3, num3) > 0)
			{
				str += PXCV_Helper.BytesToString(buf3, num3);
			}
			MessageBox.Show(owner, str, "PXCV36 Demo");
		}
	}
}
