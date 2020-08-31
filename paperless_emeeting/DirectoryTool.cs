using System;
using System.IO;

public class DirectoryTool
{
	public static void FullDeleteDirectories(string dirPath)
	{
		try
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(dirPath);
			if (directoryInfo.Exists)
			{
				foreach (DirectoryInfo item in directoryInfo.EnumerateDirectories())
				{
					FullDeleteDirectories(item);
				}
				directoryInfo.Delete(true);
			}
		}
		catch (Exception ex)
		{
			LogTool.Debug(ex);
		}
	}

	public static void FullDeleteDirectories(DirectoryInfo baseDir)
	{
		try
		{
			if (baseDir.Exists)
			{
				foreach (DirectoryInfo item in baseDir.EnumerateDirectories())
				{
					FullDeleteDirectories(item);
				}
				baseDir.Delete(true);
			}
		}
		catch (Exception ex)
		{
			LogTool.Debug(ex);
		}
	}

	public static void FullCopyDirectories(string SourcePath, string DestinationPath)
	{
		string[] directories = Directory.GetDirectories(SourcePath, "*", SearchOption.AllDirectories);
		foreach (string text in directories)
		{
			Directory.CreateDirectory(text.Replace(SourcePath, DestinationPath));
		}
		string[] files = Directory.GetFiles(SourcePath, "*.*", SearchOption.AllDirectories);
		foreach (string text2 in files)
		{
			File.Copy(text2, text2.Replace(SourcePath, DestinationPath), true);
		}
	}
}
