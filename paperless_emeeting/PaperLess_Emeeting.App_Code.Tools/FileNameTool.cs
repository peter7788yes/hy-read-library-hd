namespace PaperLess_Emeeting.App_Code.Tools
{
	public class FileNameTool
	{
		public static string PureFileName(string fileName)
		{
			char[] array = new char[9]
			{
				'\\',
				'/',
				':',
				'*',
				'?',
				'"',
				'<',
				'>',
				'|'
			};
			char[] array2 = array;
			foreach (char oldChar in array2)
			{
				fileName = fileName.Replace(oldChar, '_');
			}
			return fileName;
		}
	}
}
