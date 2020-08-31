using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PaperLess_Emeeting.App_Code.Tools
{
	public class IniFileTool
	{
		public string Path;

		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

		public IniFileTool(string inipath)
		{
			Path = inipath;
		}

		public void IniWriteValue(string Section, string Key, string Value)
		{
			try
			{
				WritePrivateProfileString(Section, Key, Value, Path);
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}

		public string IniReadValue(string Section, string Key)
		{
			string text = "";
			try
			{
				StringBuilder stringBuilder = new StringBuilder(255);
				GetPrivateProfileString(Section, Key, "", stringBuilder, 255, Path);
				return stringBuilder.ToString();
			}
			catch (Exception ex)
			{
				text = "";
				LogTool.Debug(ex);
				return text;
			}
		}
	}
}
