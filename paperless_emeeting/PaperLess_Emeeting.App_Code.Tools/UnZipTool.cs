using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;

namespace PaperLess_Emeeting.App_Code.Tools
{
	public class UnZipTool
	{
		public bool UnZip(string zipedFile, string strDirectory, string password, bool overWrite)
		{
			bool result = true;
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
										while (true)
										{
											num = zipInputStream.Read(array, 0, array.Length);
											if (num <= 0)
											{
												break;
											}
											fileStream.Write(array, 0, num);
										}
									}
								}
							}
							catch (Exception ex)
							{
								LogTool.Debug(ex);
							}
						}
					}
					return result;
				}
			}
			catch (Exception ex2)
			{
				result = false;
				LogTool.Debug(ex2);
				return result;
			}
		}
	}
}
