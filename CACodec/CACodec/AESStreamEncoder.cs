using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;
using System.IO;

namespace CACodec
{
	public class AESStreamEncoder
	{
		private const int a = 8192;

		private Stream b;

		private byte[] c;

		public AESStreamEncoder(Stream source, byte[] key)
		{
			b = source;
			c = key;
		}

		public void encode(Stream stream, bool toBase64)
		{
			IBufferedCipher cipher = CipherUtilities.GetCipher("AES/ECB/PKCS5PADDING");
			cipher.Init(true, new KeyParameter(c));
			byte[] buffer = new byte[8192];
			BufferedStream bufferedStream = new BufferedStream(b, 8192);
			MemoryStream memoryStream = new MemoryStream();
			BufferedStream bufferedStream2 = new BufferedStream(new CipherStream(memoryStream, null, cipher), 8192);
			int count;
			while ((count = bufferedStream.Read(buffer, 0, 8192)) > 0)
			{
				bufferedStream2.Write(buffer, 0, count);
			}
			bufferedStream2.Flush();
			bufferedStream2.Close();
			if (toBase64)
			{
				Base64.Encode(memoryStream.ToArray(), stream);
			}
			else
			{
				CopyStream(new MemoryStream(memoryStream.ToArray()), stream);
			}
		}

		public static void CopyStream(Stream input, Stream output)
		{
			byte[] array = new byte[32768];
			int count;
			while ((count = input.Read(array, 0, array.Length)) > 0)
			{
				output.Write(array, 0, count);
			}
		}
	}
}
