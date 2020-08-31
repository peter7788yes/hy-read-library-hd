using PaperLess_Emeeting.App_Code.ViewModel;
using System.Collections.Generic;

namespace PaperLess_Emeeting
{
	public delegate void Home_OpenBookFromReader_Function(string MeetingID, BookVM bookVM, Dictionary<string, BookVM> cbBooksData, string watermark = "");
}
