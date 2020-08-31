namespace CACodec.Security
{
	public sealed class MD5Helper
	{
		private MD5Helper()
		{
		}

		public static uint RotateLeft(uint uiNumber, ushort shift)
		{
			return (uiNumber >> 32 - shift) | (uiNumber << (int)shift);
		}

		public static uint ReverseByte(uint uiNumber)
		{
			return ((uiNumber & 0xFF) << 24) | (uiNumber >> 24) | ((uiNumber & 0xFF0000) >> 8) | ((uiNumber & 0xFF00) << 8);
		}
	}
}
