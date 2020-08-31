using PaperLess_Emeeting.App_Code.DownloadItem;
using PaperLess_Emeeting.App_Code.ViewModel;
using System.Collections.Generic;

namespace PaperLess_Emeeting
{
	public delegate Dictionary<string, BookVM> LawListCT_GetBookVMs_ByMeetingFileCate_Function(Law_DownloadItemViewModel lawItem);
}
