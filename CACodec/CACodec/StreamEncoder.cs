using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.Utilities.IO;
using Org.BouncyCastle.X509;
using System.IO;

namespace CACodec
{
	public class StreamEncoder
	{
		public static readonly string DesEde3Cbc = CmsEnvelopedGenerator.DesEde3Cbc;

		public static readonly string Aes128Cbc = CmsEnvelopedGenerator.Aes128Cbc;

		public static readonly string Aes192Cbc = CmsEnvelopedGenerator.Aes192Cbc;

		public static readonly string Aes256Cbc = CmsEnvelopedGenerator.Aes256Cbc;

		private Stream a;

		private X509Certificate b;

		private string c = Aes128Cbc;

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

		public StreamEncoder(Stream sourceStream, Stream certStream)
		{
			a = sourceStream;
			X509CertificateParser x509CertificateParser = new X509CertificateParser();
			b = x509CertificateParser.ReadCertificate(certStream);
		}

		public void encode(Stream outStream, bool toBase64)
		{
			CmsEnvelopedDataStreamGenerator cmsEnvelopedDataStreamGenerator = new CmsEnvelopedDataStreamGenerator();
			cmsEnvelopedDataStreamGenerator.AddKeyTransRecipient(b);
			if (toBase64)
			{
				MemoryStream memoryStream = new MemoryStream();
				Stream stream = cmsEnvelopedDataStreamGenerator.Open(memoryStream, c);
				Streams.PipeAll(a, stream);
				stream.Close();
				memoryStream.Position = 0L;
				Base64.Encode(memoryStream.ToArray(), outStream);
			}
			else
			{
				Stream stream2 = cmsEnvelopedDataStreamGenerator.Open(outStream, c);
				Streams.PipeAll(a, stream2);
				stream2.Close();
			}
		}
	}
}
