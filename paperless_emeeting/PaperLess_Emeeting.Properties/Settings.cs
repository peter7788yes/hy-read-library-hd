using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace PaperLess_Emeeting.Properties
{
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
	[CompilerGenerated]
	public sealed class Settings : ApplicationSettingsBase
	{
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

		public static Settings Default
		{
			get
			{
				return defaultInstance;
			}
		}

		[DefaultSettingValue("Public\\LawFiles")]
		[DebuggerNonUserCode]
		[ApplicationScopedSetting]
		public string Law_StorageFileFolder2
		{
			get
			{
				return (string)this["Law_StorageFileFolder2"];
			}
		}

		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("Public\\FileUnZip")]
		public string Law_UnZipFileFolder
		{
			get
			{
				return (string)this["Law_UnZipFileFolder"];
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("Public\\MeetingFiles")]
		[ApplicationScopedSetting]
		public string File_StorageFileFolder
		{
			get
			{
				return (string)this["File_StorageFileFolder"];
			}
		}

		[DefaultSettingValue("Public\\FileUnZip")]
		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		public string File_UnZipFileFolder
		{
			get
			{
				return (string)this["File_UnZipFileFolder"];
			}
		}

		[ApplicationScopedSetting]
		[DefaultSettingValue("1024")]
		[DebuggerNonUserCode]
		public int DownloadBuffer_KB
		{
			get
			{
				return (int)this["DownloadBuffer_KB"];
			}
		}

		[DefaultSettingValue("10")]
		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		public int HttpWebRequest_TimeoutSeconds
		{
			get
			{
				return (int)this["HttpWebRequest_TimeoutSeconds"];
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("{\"params\":[\"bookId\",\"annotation\",\"pageIndex\",\"spline\",\"bookmark\",\"animation\",\"x\",\"y\",\"scale\",\"hide\",\"execTime\",\"action\",\"actionTime\",\"page\",\"animations\"],\"cmds\":{\"R.OB\":{\"set\":[\"bookId\"],\"append\":[],\"unset\":[\"annotation\",\"pageIndex\",\"spline\",\"bookmark\",\"animation\",\"x\",\"y\",\"scale\",\"hide\",\"page\",\"animations\",\"execTime\",\"action\",\"actionTime\"]},\"R.CB\":{\"set\":[],\"append\":[],\"unset\":[\"bookId\",\"annotation\",\"pageIndex\",\"spline\",\"bookmark\",\"animation\",\"x\",\"y\",\"scale\",\"hide\",\"page\",\"animations\",\"execTime\",\"action\",\"actionTime\"]},\"R.TP\":{\"set\":[\"pageIndex\"],\"append\":[],\"unset\":[\"annotation\",\"spline\",\"bookmark\",\"animation\",\"x\",\"y\",\"scale\"]},\"R.SA\":{\"set\":[\"annotation\"],\"append\":[],\"unset\":[]},\"R.SS\":{\"set\":[\"spline\",\"pageIndex\"],\"append\":[],\"unset\":[]},\"R.SB\":{\"set\":[\"bookmark\"],\"append\":[],\"unset\":[]},\"R.PA\":{\"set\":[\"pageIndex\"],\"append\":[\"animation\"],\"unset\":[]},\"R.HA\":{\"set\":[\"hide\"],\"append\":[],\"unset\":[]},\"R.ZC\":{\"set\":[\"x\",\"y\",\"scale\"],\"append\":[],\"unset\":[]},\"R.PP\":{\"set\":[\"page\", \"animations\"],\"append\":[],\"unset\":[]},\"R.PP.V\":{\"set\":[\"bookId\", \"path\", \"action\"],\"append\":[],\"unset\":[]},\"R.SV\":{\"set\":[\"execTime\",\"action\",\"actionTime\"],\"append\":[],\"unset\":[]}}}")]
		[ApplicationScopedSetting]
		public string InitConfig
		{
			get
			{
				return (string)this["InitConfig"];
			}
		}

		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("DB\\PaperLessDB.sdf")]
		public string PaperLessDB_Path
		{
			get
			{
				return (string)this["PaperLessDB_Path"];
			}
		}

		[DebuggerNonUserCode]
		[ApplicationScopedSetting]
		[DefaultSettingValue("DB\\bookInfo.mdb")]
		public string bookInfo_Path
		{
			get
			{
				return (string)this["bookInfo_Path"];
			}
		}

		[ApplicationScopedSetting]
		[DefaultSettingValue("A3")]
		[DebuggerNonUserCode]
		public string DebugUserID
		{
			get
			{
				return (string)this["DebugUserID"];
			}
		}

		[DefaultSettingValue("h1")]
		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		public string DebugUserPWD
		{
			get
			{
				return (string)this["DebugUserPWD"];
			}
		}

		[DefaultSettingValue("zh-TW")]
		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		public CultureInfo DefaultCulture
		{
			get
			{
				return (CultureInfo)this["DefaultCulture"];
			}
		}

		[DefaultSettingValue("5")]
		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		public int DetectSyncServerSeconds
		{
			get
			{
				return (int)this["DetectSyncServerSeconds"];
			}
		}

		[DefaultSettingValue("Production")]
		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		public string Environment
		{
			get
			{
				return (string)this["Environment"];
			}
		}

		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("Public\\SignInPics")]
		public string SignInFolder
		{
			get
			{
				return (string)this["SignInFolder"];
			}
		}

		[DebuggerNonUserCode]
		[ApplicationScopedSetting]
		[DefaultSettingValue("1024")]
		public int ServicePoint_DefaultConnectionLimit
		{
			get
			{
				return (int)this["ServicePoint_DefaultConnectionLimit"];
			}
		}

		[ApplicationScopedSetting]
		[DefaultSettingValue("1")]
		[DebuggerNonUserCode]
		public int Downloader_InvokePercent
		{
			get
			{
				return (int)this["Downloader_InvokePercent"];
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("Config.ini")]
		[ApplicationScopedSetting]
		public string ConfigIni_Path
		{
			get
			{
				return (string)this["ConfigIni_Path"];
			}
		}

		[DefaultSettingValue("")]
		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		public string Setting
		{
			get
			{
				return (string)this["Setting"];
			}
		}

		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		public bool MeetingDataCT_AutoUpdate
		{
			get
			{
				return (bool)this["MeetingDataCT_AutoUpdate"];
			}
		}

		[DefaultSettingValue("Public\\LawFiles")]
		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		public string Law_StorageFileFolder
		{
			get
			{
				return (string)this["Law_StorageFileFolder"];
			}
		}

		[DebuggerNonUserCode]
		[ApplicationScopedSetting]
		[DefaultSettingValue("UserName")]
		public string DisplayUserNameMode
		{
			get
			{
				return (string)this["DisplayUserNameMode"];
			}
		}

		[ApplicationScopedSetting]
		[DefaultSettingValue("False")]
		[DebuggerNonUserCode]
		public bool GetBookVMs_ByMeetingFileCate_ByAgenda
		{
			get
			{
				return (bool)this["GetBookVMs_ByMeetingFileCate_ByAgenda"];
			}
		}

		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		public bool EnableSignOut
		{
			get
			{
				return (bool)this["EnableSignOut"];
			}
		}

		[DefaultSettingValue("False")]
		[DebuggerNonUserCode]
		[ApplicationScopedSetting]
		public bool HasOfflineMode
		{
			get
			{
				return (bool)this["HasOfflineMode"];
			}
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string e123
		{
			get
			{
				return (string)this["e123"];
			}
			set
			{
				this["e123"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("10")]
		[ApplicationScopedSetting]
		public int SignPenThickness
		{
			get
			{
				return (int)this["SignPenThickness"];
			}
		}

		[ApplicationScopedSetting]
		[DefaultSettingValue("False")]
		[DebuggerNonUserCode]
		public string HasOffLine_nouse
		{
			get
			{
				return (string)this["HasOffLine_nouse"];
			}
		}

		[DefaultSettingValue("60")]
		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		public int AutoLogoutMinutes
		{
			get
			{
				return (int)this["AutoLogoutMinutes"];
			}
		}

		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("False")]
		public string HasOffLine
		{
			get
			{
				return (string)this["HasOffLine"];
			}
		}

		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("False")]
		public bool MeetingDataCT_AutoDownload
		{
			get
			{
				return (bool)this["MeetingDataCT_AutoDownload"];
			}
		}

		[DefaultSettingValue("True")]
		[DebuggerNonUserCode]
		[ApplicationScopedSetting]
		public bool UserMeeting_Reverse
		{
			get
			{
				return (bool)this["UserMeeting_Reverse"];
			}
		}

		[DefaultSettingValue("False")]
		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		public bool IsNewMeeting_PopupDialog
		{
			get
			{
				return (bool)this["IsNewMeeting_PopupDialog"];
			}
		}

		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		public bool HasRemeberLogin
		{
			get
			{
				return (bool)this["HasRemeberLogin"];
			}
		}

		[DebuggerNonUserCode]
		[ApplicationScopedSetting]
		[DefaultSettingValue("False")]
		public bool IsFlatUIReader
		{
			get
			{
				return (bool)this["IsFlatUIReader"];
			}
		}

		[DefaultSettingValue("10")]
		[DebuggerNonUserCode]
		[ApplicationScopedSetting]
		public int CacheMinuteTTL
		{
			get
			{
				return (int)this["CacheMinuteTTL"];
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("http://broadcast.hyweb.com.tw:8080/DataService/1/classes/")]
		[ApplicationScopedSetting]
		public string AbstractSyncCenter_BASE_URL_Debug
		{
			get
			{
				return (string)this["AbstractSyncCenter_BASE_URL_Debug"];
			}
		}

		[DefaultSettingValue("http://broadcast.hyweb.com.tw:8080")]
		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		public string SyncServerUrl_Debug
		{
			get
			{
				return (string)this["SyncServerUrl_Debug"];
			}
		}

		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("False")]
		public bool HasSyncCenterModule
		{
			get
			{
				return (bool)this["HasSyncCenterModule"];
			}
		}

		[DefaultSettingValue("4")]
		[DebuggerNonUserCode]
		[ApplicationScopedSetting]
		public int NowDBVersion
		{
			get
			{
				return (int)this["NowDBVersion"];
			}
		}

		[DebuggerNonUserCode]
		[ApplicationScopedSetting]
		[DefaultSettingValue("文件庫")]
		public string LawButtonName
		{
			get
			{
				return (string)this["LawButtonName"];
			}
		}

		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("images/tabbar_ic_law@2x.png")]
		public string LawButton_Image
		{
			get
			{
				return (string)this["LawButton_Image"];
			}
		}

		[DebuggerNonUserCode]
		[ApplicationScopedSetting]
		[DefaultSettingValue("True")]
		public bool IsClearSyncOwnerPenLine
		{
			get
			{
				return (bool)this["IsClearSyncOwnerPenLine"];
			}
		}

		[DebuggerNonUserCode]
		[ApplicationScopedSetting]
		[DefaultSettingValue("http://com-meeting.ntpc.gov.tw/PaperLess_Emeeting_PC/")]
		public string Deploy_Address
		{
			get
			{
				return (string)this["Deploy_Address"];
			}
		}

		[DebuggerNonUserCode]
		[ApplicationScopedSetting]
		[DefaultSettingValue("http://com-meeting.ntpc.gov.tw/PaperLess_Emeeting_PC/")]
		public string Install_Address
		{
			get
			{
				return (string)this["Install_Address"];
			}
		}

		[ApplicationScopedSetting]
		[DefaultSettingValue("False")]
		[DebuggerNonUserCode]
		public bool IsDebugMode
		{
			get
			{
				return (bool)this["IsDebugMode"];
			}
		}

		[ApplicationScopedSetting]
		[DefaultSettingValue("http://com-br.ntpc.gov.tw:8080/DataService/1/classes/")]
		[DebuggerNonUserCode]
		public string AbstractSyncCenter_BASE_URL
		{
			get
			{
				return (string)this["AbstractSyncCenter_BASE_URL"];
			}
		}

		[DefaultSettingValue("會議")]
		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		public string CourseOrMeeting_String
		{
			get
			{
				return (string)this["CourseOrMeeting_String"];
			}
		}

		[DefaultSettingValue("新北市政府")]
		[DebuggerNonUserCode]
		[ApplicationScopedSetting]
		public string Publisher
		{
			get
			{
				return (string)this["Publisher"];
			}
		}

		[DefaultSettingValue("\\\\10.10.25.163\\Project\\NTPCeMeeting\\Common-Frontend\\PaperLess_Emeeting_PC\\")]
		[DebuggerNonUserCode]
		[ApplicationScopedSetting]
		public string Deploy_Address_Debug
		{
			get
			{
				return (string)this["Deploy_Address_Debug"];
			}
		}

		[ApplicationScopedSetting]
		[DefaultSettingValue("PaperLess_Emeeting_NTPC")]
		[DebuggerNonUserCode]
		public string AssemblyName
		{
			get
			{
				return (string)this["AssemblyName"];
			}
		}

		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string SignListEmptyDash
		{
			get
			{
				return (string)this["SignListEmptyDash"];
			}
		}

		[DefaultSettingValue("http://com-meeting.ntpc.gov.tw/WebService/MeetingService.asmx")]
		[SpecialSetting(SpecialSetting.WebServiceUrl)]
		[DebuggerNonUserCode]
		[ApplicationScopedSetting]
		public string PaperLess_Emeeting_WS_MeetingService
		{
			get
			{
				return (string)this["PaperLess_Emeeting_WS_MeetingService"];
			}
		}

		[DefaultSettingValue("新北市政府電子化會議系統")]
		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		public string AppName
		{
			get
			{
				return (string)this["AppName"];
			}
		}

		[DefaultSettingValue("http://com-br.ntpc.gov.tw:8080")]
		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		public string SyncServerUrl
		{
			get
			{
				return (string)this["SyncServerUrl"];
			}
		}

		[DefaultSettingValue("http://imp-br.ntpc.gov.tw:8080")]
		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		public string SyncServerUrl_Imp
		{
			get
			{
				return (string)this["SyncServerUrl_Imp"];
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("http://com-meeting.ntpc.hyweb.com.tw/WebService/MeetingService.asmx")]
		[ApplicationScopedSetting]
		public string PaperLess_Emeeting_WS_MeetingService_Debug
		{
			get
			{
				return (string)this["PaperLess_Emeeting_WS_MeetingService_Debug"];
			}
		}

		[DefaultSettingValue("http://com-meeting.ntpc.hyweb.com.tw/PaperLess_Emeeting_PC/")]
		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		public string Install_Address_Debug
		{
			get
			{
				return (string)this["Install_Address_Debug"];
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("images/NTPC_login_header@2x.png")]
		[ApplicationScopedSetting]
		public string Login_Header_Image
		{
			get
			{
				return (string)this["Login_Header_Image"];
			}
		}

		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("images/NTPC_logo@2x.png")]
		public string Home_Logo_Image
		{
			get
			{
				return (string)this["Home_Logo_Image"];
			}
		}

		[DebuggerNonUserCode]
		[ApplicationScopedSetting]
		[DefaultSettingValue("NTPC_app-icon.ico")]
		public string AppIcon
		{
			get
			{
				return (string)this["AppIcon"];
			}
		}

		[DefaultSettingValue("NTPC_app-icon_beta.ico")]
		[DebuggerNonUserCode]
		[ApplicationScopedSetting]
		public string AppIcon_Debug
		{
			get
			{
				return (string)this["AppIcon_Debug"];
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("PaperLess_Emeeting_NTPC_Beta")]
		[ApplicationScopedSetting]
		public string AssemblyName_Debug
		{
			get
			{
				return (string)this["AssemblyName_Debug"];
			}
		}

		[DebuggerNonUserCode]
		[ApplicationScopedSetting]
		[DefaultSettingValue("新北市政府電子化會議系統(Beta)")]
		public string AppName_Debug
		{
			get
			{
				return (string)this["AppName_Debug"];
			}
		}

		[DefaultSettingValue("False")]
		[DebuggerNonUserCode]
		[ApplicationScopedSetting]
		public bool HasAutoTodaySync
		{
			get
			{
				return (bool)this["HasAutoTodaySync"];
			}
		}

		private void SettingChangingEventHandler(object sender, SettingChangingEventArgs e)
		{
		}

		private void SettingsSavingEventHandler(object sender, CancelEventArgs e)
		{
		}
	}
}
