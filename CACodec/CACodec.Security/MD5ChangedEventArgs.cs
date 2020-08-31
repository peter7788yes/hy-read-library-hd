using System;

namespace CACodec.Security
{
	public class MD5ChangedEventArgs : EventArgs
	{
		public readonly byte[] NewData;

		public readonly string FingerPrint;

		public MD5ChangedEventArgs(byte[] data, string HashedValue)
		{
			byte[] array = new byte[data.Length];
			for (int i = 0; i < data.Length; i++)
			{
				array[i] = data[i];
			}
			FingerPrint = HashedValue;
		}

		public MD5ChangedEventArgs(string data, string HashedValue)
		{
			byte[] array = new byte[data.Length];
			for (int i = 0; i < data.Length; i++)
			{
				array[i] = (byte)data[i];
			}
			FingerPrint = HashedValue;
		}
	}
}
