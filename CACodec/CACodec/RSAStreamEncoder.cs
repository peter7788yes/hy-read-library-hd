using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.X509;
using System.IO;

namespace CACodec
{
	public class RSAStreamEncoder
	{
		private Stream a;

		private X509Certificate b;

		private string c = StreamEncoder.Aes128Cbc;

		public string Algorithm
		{
			get
			{
				return c;
			}
			set
			{
				c = Algorithm;
			}
		}

		public RSAStreamEncoder(Stream sourceStream, Stream certStream)
		{
			a = sourceStream;
			X509CertificateParser x509CertificateParser = new X509CertificateParser();
			b = x509CertificateParser.ReadCertificate(certStream);
		}

		public void encode(Stream outStream, bool toBase64)
		{
			IBufferedCipher cipher = CipherUtilities.GetCipher("RSA/None/NoPadding");
			cipher.Init(true, b.GetPublicKey());
			byte[] buffer = new byte[8192];
			BufferedStream bufferedStream = new BufferedStream(a, 8192);
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
				Base64.Encode(memoryStream.ToArray(), outStream);
			}
			else
			{
				CopyStream(new MemoryStream(memoryStream.ToArray()), outStream);
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

		public byte[] encode(string algori, bool toBase64)
		{
			IBufferedCipher cipher = CipherUtilities.GetCipher(algori);
			cipher.Init(true, b.GetPublicKey());
			byte[] buffer = new byte[8192];
			BufferedStream bufferedStream = new BufferedStream(a, 8192);
			MemoryStream memoryStream = new MemoryStream();
			BufferedStream bufferedStream2 = new BufferedStream(new CipherStream(memoryStream, null, cipher), 8192);
			int count;
			while ((count = bufferedStream.Read(buffer, 0, 8192)) > 0)
			{
				bufferedStream2.Write(buffer, 0, count);
			}
			bufferedStream2.Flush();
			bufferedStream2.Close();
			MemoryStream stream = new MemoryStream(memoryStream.ToArray());
			byte[] array = StreamToBytes(stream);
			if (toBase64)
			{
				return Base64.Encode(array);
			}
			return array;
		}

		public byte[] StreamToBytes(Stream stream)
		{
			byte[] array = new byte[stream.Length];
			stream.Read(array, 0, array.Length);
			stream.Seek(0L, SeekOrigin.Begin);
			return array;
		}
	}
}
