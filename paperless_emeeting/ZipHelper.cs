using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;

public class ZipHelper
{
	public static void ZipFile(string fileToZip, string zipedFile, int compressionLevel, int blockSize)
	{
		if (!File.Exists(fileToZip))
		{
			throw new FileNotFoundException("指定要壓縮的文件: " + fileToZip + " 不存在!");
		}
		using (FileStream fileStream = File.Create(zipedFile))
		{
			using (ZipOutputStream zipOutputStream = new ZipOutputStream(fileStream))
			{
				using (FileStream fileStream2 = new FileStream(fileToZip, FileMode.Open, FileAccess.Read))
				{
					string name = fileToZip.Substring(fileToZip.LastIndexOf("\\") + 1);
					ZipEntry entry = new ZipEntry(name);
					zipOutputStream.PutNextEntry(entry);
					zipOutputStream.SetLevel(compressionLevel);
					byte[] array = new byte[blockSize];
					int num = 0;
					try
					{
						do
						{
							num = fileStream2.Read(array, 0, array.Length);
							zipOutputStream.Write(array, 0, num);
						}
						while (num > 0);
					}
					catch (Exception ex)
					{
						throw ex;
					}
					fileStream2.Close();
				}
				zipOutputStream.Finish();
				zipOutputStream.Close();
			}
			fileStream.Close();
		}
	}

	public static void ZipFile(string fileToZip, string zipedFile)
	{
		if (!File.Exists(fileToZip))
		{
			throw new FileNotFoundException("指定要壓縮的文件: " + fileToZip + " 不存在!");
		}
		using (FileStream fileStream = File.OpenRead(fileToZip))
		{
			byte[] array = new byte[fileStream.Length];
			fileStream.Read(array, 0, array.Length);
			fileStream.Close();
			using (FileStream baseOutputStream = File.Create(zipedFile))
			{
				using (ZipOutputStream zipOutputStream = new ZipOutputStream(baseOutputStream))
				{
					string name = fileToZip.Substring(fileToZip.LastIndexOf("\\") + 1);
					ZipEntry entry = new ZipEntry(name);
					zipOutputStream.PutNextEntry(entry);
					zipOutputStream.SetLevel(5);
					zipOutputStream.Write(array, 0, array.Length);
					zipOutputStream.Finish();
					zipOutputStream.Close();
				}
			}
		}
	}

	public static void ZipFileDirectory(string strDirectory, string zipedFile)
	{
		using (FileStream baseOutputStream = File.Create(zipedFile))
		{
			using (ZipOutputStream s = new ZipOutputStream(baseOutputStream))
			{
				ZipSetp(strDirectory, s, "");
			}
		}
	}

	private static void ZipSetp(string strDirectory, ZipOutputStream s, string parentPath)
	{
		if (strDirectory[strDirectory.Length - 1] != Path.DirectorySeparatorChar)
		{
			strDirectory += Path.DirectorySeparatorChar;
		}
		Crc32 crc = new Crc32();
		string[] fileSystemEntries = Directory.GetFileSystemEntries(strDirectory);
		string[] array = fileSystemEntries;
		foreach (string text in array)
		{
			if (Directory.Exists(text))
			{
				string str = parentPath;
				str += text.Substring(text.LastIndexOf("\\") + 1);
				str += "\\";
				ZipSetp(text, s, str);
			}
			else
			{
				using (FileStream fileStream = File.OpenRead(text))
				{
					byte[] array2 = new byte[fileStream.Length];
					fileStream.Read(array2, 0, array2.Length);
					string name = parentPath + text.Substring(text.LastIndexOf("\\") + 1);
					ZipEntry zipEntry = new ZipEntry(name);
					zipEntry.DateTime = DateTime.Now;
					zipEntry.Size = fileStream.Length;
					fileStream.Close();
					crc.Reset();
					crc.Update(array2);
					zipEntry.Crc = crc.Value;
					s.PutNextEntry(zipEntry);
					s.Write(array2, 0, array2.Length);
				}
			}
		}
	}

	public void UnZip(string zipedFile, string strDirectory, string password, bool overWrite)
	{
		ZipInputStream zipInputStream = null;
		try
		{
			if (strDirectory == "")
			{
				strDirectory = Directory.GetCurrentDirectory();
			}
			if (!strDirectory.EndsWith("\\"))
			{
				strDirectory += "\\";
			}
			using (zipInputStream = new ZipInputStream(File.OpenRead(zipedFile)))
			{
				zipInputStream.Password = password;
				ZipEntry nextEntry;
				while ((nextEntry = zipInputStream.GetNextEntry()) != null)
				{
					nextEntry.IsUnicodeText = true;
					string str = "";
					string text = "";
					text = nextEntry.Name;
					if (text.Contains("?"))
					{
						text = text.Replace('?', '_');
					}
					if (text != "")
					{
						str = Path.GetDirectoryName(text) + "\\";
					}
					string fileName = Path.GetFileName(text);
					Directory.CreateDirectory(strDirectory + str);
					if (fileName != "")
					{
						try
						{
							if ((File.Exists(strDirectory + str + fileName) && overWrite) || !File.Exists(strDirectory + str + fileName))
							{
								using (FileStream fileStream = File.Create(strDirectory + str + fileName))
								{
									int num = 2048;
									byte[] array = new byte[2048];
									try
									{
										while (true)
										{
											num = zipInputStream.Read(array, 0, array.Length);
											if (num <= 0)
											{
												break;
											}
											fileStream.Write(array, 0, num);
										}
										fileStream.Close();
									}
									catch (Exception)
									{
										fileStream.Close();
									}
								}
							}
						}
						catch (Exception)
						{
						}
					}
				}
				zipInputStream.Close();
			}
		}
		catch (Exception)
		{
			try
			{
				zipInputStream.Close();
			}
			catch
			{
			}
		}
	}

	public int GetZipFileCount(string zipedFile)
	{
		int num = 0;
		ZipInputStream zipInputStream = null;
		try
		{
			using (zipInputStream = new ZipInputStream(File.OpenRead(zipedFile)))
			{
				while (zipInputStream.GetNextEntry() != null)
				{
					num++;
				}
				zipInputStream.Close();
			}
		}
		catch (Exception)
		{
			try
			{
				zipInputStream.Close();
			}
			catch
			{
			}
			num = 0;
		}
		return num - 1;
	}
}
