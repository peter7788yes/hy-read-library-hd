using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;
using System.IO;

namespace CACodec
{
	public class AESStreamDecoder
	{
		private const int a = 8192;

		private Stream b;

		private byte[] c;

		private string d = "AES/ECB/PKCS5PADDING";

		public AESStreamDecoder(Stream source, byte[] key, bool isBase64, string mode = "")
		{
			if (!mode.Equals(""))
			{
				d = mode;
			}
			if (isBase64)
			{
				StreamReader streamReader = new StreamReader(source);
				b = new MemoryStream();
				Base64.Decode(streamReader.ReadToEnd(), b);
				b.Position = 0L;
			}
			else
			{
				b = source;
			}
			c = key;
		}

		public void decode(Stream stream)
		{
			IBufferedCipher cipher = CipherUtilities.GetCipher(d);
			cipher.Init(false, new KeyParameter(c));
			byte[] buffer = new byte[8192];
			BufferedStream bufferedStream = new BufferedStream(new CipherStream(b, cipher, null), 8192);
			BufferedStream bufferedStream2 = new BufferedStream(stream, 8192);
			try
			{
				int count;
				while ((count = bufferedStream.Read(buffer, 0, 8192)) > 0)
				{
					bufferedStream2.Write(buffer, 0, count);
				}
			}
			catch
			{
			}
			bufferedStream2.Flush();
			bufferedStream.Close();
		}
	}
}
