using PaperLess_Emeeting.App_Code.WS;

namespace PaperLess_Emeeting.App_Code
{
	public class Singleton_LawDownloader
	{
		private static readonly object thisLock = new object();

		private static LawDownloader instance = new LawDownloader();

		private Singleton_LawDownloader()
		{
		}

		public static LawDownloader GetInstance()
		{
			return instance;
		}
	}
}
