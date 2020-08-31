using System;
using System.Security.Cryptography;

namespace AES_ECB_PKCS5
{
	public class ReaderDecodeTool
	{
		public static string ConvertEbookKey = "ADERfQPF+rzg+RBzePv08mh4Jer5JKB3";

		public static byte[] GetReaderKey(string key3FromBase64)
		{
			return AES_Decrypt(Convert.FromBase64String(key3FromBase64), Convert.FromBase64String(ConvertEbookKey));
		}

		public static byte[] AES_Encrypt(byte[] input, byte[] keyBytes)
		{
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			new MD5CryptoServiceProvider();
			try
			{
				rijndaelManaged.KeySize = 192;
				rijndaelManaged.BlockSize = 128;
				rijndaelManaged.Key = keyBytes;
				rijndaelManaged.Mode = CipherMode.ECB;
				rijndaelManaged.Padding = PaddingMode.PKCS7;
				ICryptoTransform cryptoTransform = rijndaelManaged.CreateEncryptor();
				return cryptoTransform.TransformFinalBlock(input, 0, input.Length);
			}
			catch (Exception)
			{
				return null;
			}
		}

		public static byte[] AES_Decrypt(byte[] input, byte[] keyBytes)
		{
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			new MD5CryptoServiceProvider();
			try
			{
				rijndaelManaged.KeySize = 192;
				rijndaelManaged.BlockSize = 128;
				rijndaelManaged.Key = keyBytes;
				rijndaelManaged.Mode = CipherMode.ECB;
				rijndaelManaged.Padding = PaddingMode.PKCS7;
				ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor();
				return cryptoTransform.TransformFinalBlock(input, 0, input.Length);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return null;
			}
		}
	}
}
