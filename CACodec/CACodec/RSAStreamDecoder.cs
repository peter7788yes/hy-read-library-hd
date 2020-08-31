using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;
using System.IO;

namespace CACodec
{
	public class RSAStreamDecoder
	{
		private const int a = 8192;

		private Stream b;

		private X509CertificateEntry c;

		private AsymmetricKeyEntry d;

		private RsaPrivateCrtKeyParameters e;

		public RSAStreamDecoder(Stream source, Stream p12, string storePass, string keyPass, bool isBase64)
		{
			Pkcs12Store pkcs12Store = new Pkcs12StoreBuilder().Build();
			pkcs12Store.Load(p12, storePass.ToCharArray());
			string alias = null;
			foreach (string alias3 in pkcs12Store.Aliases)
			{
				if (pkcs12Store.IsKeyEntry(alias3))
				{
					alias = alias3;
					break;
				}
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
			c = pkcs12Store.GetCertificate(alias);
			d = pkcs12Store.GetKey(alias);
			foreach (string alias4 in pkcs12Store.Aliases)
			{
				if (pkcs12Store.IsKeyEntry(alias4))
				{
					AsymmetricKeyEntry key = pkcs12Store.GetKey(alias4);
					if (key.Key.IsPrivate)
					{
						e = (key.Key as RsaPrivateCrtKeyParameters);
					}
				}
			}
		}

		public void decode(Stream outStream)
		{
			IBufferedCipher cipher = CipherUtilities.GetCipher("RSA/None/NoPadding");
			cipher.Init(false, e);
			byte[] buffer = new byte[8192];
			BufferedStream bufferedStream = new BufferedStream(new CipherStream(b, cipher, null), 8192);
			BufferedStream bufferedStream2 = new BufferedStream(outStream, 8192);
			int count;
			while ((count = bufferedStream.Read(buffer, 0, 8192)) > 0)
			{
				bufferedStream2.Write(buffer, 0, count);
			}
			bufferedStream2.Flush();
			bufferedStream.Close();
		}
	}
}
