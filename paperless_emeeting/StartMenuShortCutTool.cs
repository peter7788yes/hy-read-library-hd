using System;
using System.IO;

public class StartMenuShortCutTool
{
	public static void DeleteDirectory(string dirName)
	{
		string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
		string dirPath = Path.Combine(folderPath, "Programs", dirName);
		DirectoryTool.FullDeleteDirectories(dirPath);
	}
}
