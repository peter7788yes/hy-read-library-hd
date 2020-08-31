using PaperLess_Emeeting;
using System;

public class LogTool
{
	public static void Debug(Exception ex)
	{
		string text = string.Format("\r\n#ex.Source:{0}\r\n#ex.TargetSite:{1}\r\n#ex.Message:{2}\r\n#ex.StackTrace:{3}\r\n", ex.Source, ex.TargetSite, ex.Message, ex.StackTrace);
		App.logger.Debug(text);
		Console.WriteLine(text);
	}

	public static void Debug(string message)
	{
		string text = string.Format("\r\n#ex.Source:{0}\r\n#ex.TargetSite:{1}\r\n#ex.Message:{2}\r\n#ex.StackTrace:{3}\r\n", message, "2", "3", "4");
		App.logger.Debug(text);
		Console.WriteLine(text);
	}
}
