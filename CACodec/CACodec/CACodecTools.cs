using CACodec.Security;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace CACodec
{
	public class CACodecTools
	{
		public byte[] _aesKey = new byte[24]
		{
			243,
			168,
			204,
			0,
			173,
			49,
			61,
			0,
			251,
			125,
			156,
			167,
			87,
			81,
			252,
			107,
			247,
			124,
			222,
			68,
			190,
			164,
			36,
			118
		};

		public CACodecTools(byte[] aesKey)
		{
			_aesKey = aesKey;
		}

		public CACodecTools()
		{
		}

		public byte[] encryptStringDecode2ByteArray(string cValue, string p12Path, string password, bool useBase64)
		{
			byte[] array = new byte[24];
			MemoryStream memoryStream = new MemoryStream();
			Stream stream = null;
			if (cValue.Equals(""))
			{
				return _aesKey;
			}
			try
			{
				stream = new FileStream(p12Path, FileMode.Open, FileAccess.Read);
				MemoryStream memoryStream2 = new MemoryStream(Encoding.ASCII.GetBytes(cValue));
				memoryStream2.Position = 0L;
				new RSAStreamDecoder(memoryStream2, stream, password, password, useBase64).decode(memoryStream);
				memoryStream.Position = 0L;
				memoryStream.Read(array, 0, 24);
				return array;
			}
			catch
			{
				return array;
			}
			finally
			{
				memoryStream.Close();
				stream.Close();
			}
		}

		public bool fileCerEncode(string sourcePath, string targetPath, string cerPath)
		{
			Stream stream = null;
			Stream stream2 = null;
			Stream stream3 = null;
			bool result = false;
			if (File.Exists(sourcePath))
			{
				try
				{
					stream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read);
					stream2 = new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.Write);
					stream3 = new FileStream(cerPath, FileMode.Open, FileAccess.Read);
					new StreamEncoder(stream, stream3).encode(stream2, false);
					result = true;
					return result;
				}
				catch (Exception)
				{
					return result;
				}
				finally
				{
					stream.Close();
					stream2.Close();
					stream3.Close();
				}
			}
			return result;
		}

		public bool fileP12Decode(string sourcePath, string targetPath, string p12Path, string password, bool useBase64)
		{
			Stream stream = null;
			Stream stream2 = null;
			Stream stream3 = null;
			bool result = false;
			if (File.Exists(sourcePath))
			{
				try
				{
					stream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read);
					stream2 = new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.Write);
					stream3 = new FileStream(p12Path, FileMode.Open, FileAccess.Read);
					new StreamDecoder(stream, stream3, password, password, useBase64).decode(stream2);
					result = true;
					return result;
				}
				catch (Exception)
				{
					return result;
				}
				finally
				{
					stream.Close();
					stream2.Close();
					stream3.Close();
				}
			}
			return result;
		}

		public byte[] fileAESDecode(string sourcePath, byte[] aeskey)
		{
			_aesKey = aeskey;
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			rijndaelManaged.Mode = CipherMode.ECB;
			rijndaelManaged.KeySize = 192;
			rijndaelManaged.Padding = PaddingMode.PKCS7;
			rijndaelManaged.Key = aeskey;
			ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor();
			FileInfo fileInfo = new FileInfo(sourcePath);
			byte[] inputBuffer = File.ReadAllBytes(sourcePath);
			int inputCount = (int)fileInfo.Length;
			return cryptoTransform.TransformFinalBlock(inputBuffer, 0, inputCount);
		}

		public MemoryStream fileAESDecodeMode(string sourcePath, byte[] aeskey, bool usebase64, string mode = "")
		{
			_aesKey = aeskey;
			return fileAESDecode(sourcePath, usebase64, mode);
		}

		public MemoryStream fileAESDecode(string sourcePath, byte[] aeskey, bool usebase64)
		{
			_aesKey = aeskey;
			return fileAESDecode(sourcePath, usebase64);
		}

		public MemoryStream fileAESDecode(string sourcePath, bool usebase64, string mode = "")
		{
			Stream stream = null;
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				stream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read);
				new AESStreamDecoder(stream, _aesKey, usebase64, mode).decode(memoryStream);
			}
			catch
			{
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
			}
			memoryStream.Position = 0L;
			return memoryStream;
		}

		public string stringDecode(string str, byte[] aeskey, bool usebase64)
		{
			_aesKey = aeskey;
			return stringDecode(str, usebase64);
		}

		public string stringDecode(string str, bool usebase64)
		{
			AESStreamDecoder aESStreamDecoder = new AESStreamDecoder(new MemoryStream(Encoding.UTF8.GetBytes(str)), _aesKey, usebase64);
			MemoryStream memoryStream = new MemoryStream();
			aESStreamDecoder.decode(memoryStream);
			memoryStream.Position = 0L;
			return new StreamReader(memoryStream).ReadToEnd();
		}

		public string stringEncode(string str, byte[] aesKey, bool usebase64)
		{
			_aesKey = aesKey;
			return stringEncode(str, usebase64);
		}

		public string stringEncode(string str, bool usebase64)
		{
			AESStreamEncoder aESStreamEncoder = new AESStreamEncoder(new MemoryStream(Encoding.UTF8.GetBytes(str)), _aesKey);
			MemoryStream memoryStream = new MemoryStream();
			aESStreamEncoder.encode(memoryStream, usebase64);
			memoryStream.Position = 0L;
			return new StreamReader(memoryStream).ReadToEnd();
		}

		public string CreateMD5Hash(string input)
		{
			try
			{
				System.Security.Cryptography.MD5 mD = System.Security.Cryptography.MD5.Create();
				byte[] bytes = Encoding.ASCII.GetBytes(input);
				byte[] array = mD.ComputeHash(bytes);
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < array.Length; i++)
				{
					stringBuilder.Append(array[i].ToString("x2"));
				}
				return stringBuilder.ToString();
			}
			catch (TargetInvocationException)
			{
				CACodec.Security.MD5 mD2 = new CACodec.Security.MD5();
				mD2.Value = input;
				return mD2.FingerPrint.ToLower();
			}
		}

		public string Base64Encode(string str)
		{
			return Convert.ToBase64String(Encoding.GetEncoding("utf-8").GetBytes(str));
		}

		public string Base64Decode(string base64EncodedData)
		{
			byte[] bytes = Convert.FromBase64String(base64EncodedData);
			return Encoding.UTF8.GetString(bytes);
		}

		public Dictionary<string, string> getDigestHeaders(string p12Path, string vendorId, string colibId, string userId, string password, string bodyDoc)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			long curTimeStamp = getCurTimeStamp();
			password = CreateMD5Hash(password) + ":";
			if (File.Exists(p12Path))
			{
				string digest = getDigest(p12Path, password, bodyDoc, curTimeStamp);
				string value = "=?UTF-8?B?" + Base64Encode(userId) + "?=";
				dictionary.Add("Hyread-API-Version", "2.5");
				dictionary.Add("Hyread-API-Key", "HyreadDRM");
				dictionary.Add("Hyread-Domain-Id", value);
				dictionary.Add("Hyread-Message-Id", "lendlist");
				dictionary.Add("Hyread-Message-Timestamp", curTimeStamp.ToString());
				dictionary.Add("Hyread-Message-Digest", digest);
			}
			return dictionary;
		}

		public string getDigest(string p12Path, string password, string bodyString, long requestTime)
		{
			Stream stream = new FileStream(p12Path, FileMode.Open, FileAccess.Read);
			AsymmetricKeyParameter privateKey = getPrivateKey(stream, password);
			string s = new StringBuilder(bodyString).Append("||").Append(requestTime).ToString();
			byte[] array = Base64.Encode(encodeByRSA(DigestUtilities.CalculateDigest("SHA-256", Encoding.UTF8.GetBytes(s)), privateKey));
			string @string = Encoding.UTF8.GetString(array, 0, array.Length);
			stream.Flush();
			stream.Close();
			return @string;
		}

		public static byte[] encodeByRSA(byte[] contentBytes, AsymmetricKeyParameter prikey)
		{
			byte[] result = null;
			try
			{
				IBufferedCipher cipher = CipherUtilities.GetCipher("RSA/ECB/PKCS1Padding");
				cipher.Init(true, prikey);
				result = cipher.DoFinal(contentBytes);
				return result;
			}
			catch (Exception)
			{
				return result;
			}
		}

		public static byte[] decodeByRSA(byte[] contentBytes, ICipherParameters key)
		{
			byte[] result = null;
			try
			{
				IBufferedCipher cipher = CipherUtilities.GetCipher("RSA/ECB/PKCS1Padding");
				cipher.Init(false, key);
				result = cipher.DoFinal(contentBytes);
				return result;
			}
			catch (Exception)
			{
				return result;
			}
		}

		public static AsymmetricKeyParameter getPublicKey(byte[] input)
		{
			return new X509CertificateParser().ReadCertificate(input).GetPublicKey();
		}

		public static AsymmetricKeyParameter getPrivateKey(Stream certStream, string password)
		{
			Pkcs12Store pkcs12Store = new Pkcs12StoreBuilder().Build();
			pkcs12Store.Load(certStream, password.ToCharArray());
			AsymmetricKeyParameter asymmetricKeyParameter = null;
			foreach (string alias in pkcs12Store.Aliases)
			{
				if (pkcs12Store.IsKeyEntry(alias))
				{
					asymmetricKeyParameter = pkcs12Store.GetKey(alias).Key;
					if (asymmetricKeyParameter.IsPrivate)
					{
						return asymmetricKeyParameter;
					}
				}
			}
			return asymmetricKeyParameter;
		}

		public long getCurTimeStamp()
		{
			DateTime value = new DateTime(1970, 1, 1);
			return DateTime.Now.ToUniversalTime().Subtract(value).Ticks / 10000000;
		}

		public bool checkResponseHeader(Dictionary<string, string> reqHeaders, Dictionary<string, string> responseHeaders, string responseContent, string password)
		{
			string text = CreateMD5Hash(password) + ":";
			responseContent = responseContent.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", "");
			if (!reqHeaders["Hyread-Message-Id"].Equals(responseHeaders["Hyread-Origin-Message-Id"]))
			{
				return false;
			}
			long ticks = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
			new DateTime(ticks + Convert.ToInt64(reqHeaders["Hyread-Message-Timestamp"]) * 10000000);
			new DateTime(ticks + Convert.ToInt64(responseHeaders["Hyread-Message-Timestamp"]) * 10000000);
			string fileName = Directory.GetCurrentDirectory() + "\\HyreadDRMRI.cer";
			ICipherParameters publicKey = getPublicKey(FileToByteArray(fileName));
			byte[] array = Base64.Encode(decodeByRSA(Base64.Decode(Encoding.UTF8.GetBytes(responseHeaders["Hyread-Message-Digest"])), publicKey));
			string @string = Encoding.UTF8.GetString(array, 0, array.Length);
			string s = new StringBuilder(responseContent).Append("||").Append(responseHeaders["Hyread-Message-Timestamp"]).ToString();
			byte[] array2 = Base64.Encode(DigestUtilities.CalculateDigest("SHA-256", Encoding.UTF8.GetBytes(s)));
			string string2 = Encoding.UTF8.GetString(array2, 0, array2.Length);
			if (!@string.Equals(string2))
			{
				return false;
			}
			return true;
		}

		public byte[] FileToByteArray(string fileName)
		{
			BinaryReader binaryReader = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
			long length = new FileInfo(fileName).Length;
			return binaryReader.ReadBytes((int)length);
		}

		public string getCipherValue(string encryptionFile)
		{
			string result = "";
			if (!File.Exists(encryptionFile))
			{
				return result;
			}
			XmlDocument xmlDocument = new XmlDocument();
			try
			{
				xmlDocument.Load(encryptionFile);
				result = xmlDocument.GetElementsByTagName("enc:CipherValue")[0].InnerText;
				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine("getCipherValue error=" + ex.ToString());
				return result;
			}
		}
	}
}
