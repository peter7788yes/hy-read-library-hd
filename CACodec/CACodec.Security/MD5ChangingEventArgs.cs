using System;

namespace CACodec.Security
{
	public class MD5ChangingEventArgs : EventArgs
	{
		public readonly byte[] NewData;

		public MD5ChangingEventArgs(byte[] data)
		{
			byte[] array = new byte[data.Length];
			for (int i = 0; i < data.Length; i++)
			{
				array[i] = data[i];
			}
		}

		public MD5ChangingEventArgs(string data)
		{
			byte[] array = new byte[data.Length];
			for (int i = 0; i < data.Length; i++)
			{
				array[i] = (byte)data[i];
			}
		}
	}
}
