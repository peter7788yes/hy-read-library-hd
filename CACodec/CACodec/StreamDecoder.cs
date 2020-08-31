using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Utilities.Encoders;
using System.IO;

namespace CACodec
{
	public class StreamDecoder
	{
		private const int a = 8192;

		private Stream b;

		private X509CertificateEntry c;

		private AsymmetricKeyEntry d;

		public StreamDecoder(Stream source, Stream p12, string storePass, string keyPass, bool isBase64)
		{
			Pkcs12Store pkcs12Store = new Pkcs12StoreBuilder().Build();
			pkcs12Store.Load(p12, storePass.ToCharArray());
			string alias = null;
			foreach (string alias2 in pkcs12Store.Aliases)
			{
				if (pkcs12Store.IsKeyEntry(alias2))
				{
					alias = alias2;
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
		}

		public void decode(Stream stream)
		{
			CmsEnvelopedDataParser cmsEnvelopedDataParser = new CmsEnvelopedDataParser(b);
			RecipientID recipientID = new RecipientID();
			recipientID.SerialNumber = c.Certificate.SerialNumber;
			recipientID.Issuer = c.Certificate.IssuerDN;
			CmsTypedStream contentStream = cmsEnvelopedDataParser.GetRecipientInfos().GetFirstRecipient(recipientID).GetContentStream(d.Key);
			byte[] buffer = new byte[8192];
			BufferedStream bufferedStream = new BufferedStream(contentStream.ContentStream, 8192);
			BufferedStream bufferedStream2 = new BufferedStream(stream, 8192);
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
