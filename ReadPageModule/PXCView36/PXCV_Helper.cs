namespace PXCView36
{
	public class PXCV_Helper
	{
		public struct RECT
		{
			public int left;

			public int top;

			public int right;

			public int bottom;
		}

		public static string BytesToString(byte[] bytes, int len)
		{
			string text = "";
			for (int i = 0; i < len && bytes[i] != 0; i++)
			{
				string str = text;
				char c = (char)bytes[i];
				text = str + c;
			}
			return text;
		}

		public static byte[] StringToBytes(string text)
		{
			byte[] array = new byte[text.Length];
			for (int i = 0; i < text.Length; i++)
			{
				array[i] = (byte)text[i];
			}
			return array;
		}
	}
}
