using BookManagerModule;
using NLog;
using PaperLess_Emeeting.App_Code.ClickOnce;
using PaperLess_Emeeting.App_Code.Socket;
using PaperLess_Emeeting.App_Code.Tools;
using PaperLess_Emeeting.Properties;
using System;
using System.CodeDom.Compiler;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace PaperLess_Emeeting
{
	public class App : Application
	{
		public static Logger logger = LogManager.GetCurrentClassLogger();

		private bool _contentLoaded;

		[CompilerGenerated]
		private static Action CS_0024_003C_003E9__CachedAnonymousMethodDelegate2;

		[CompilerGenerated]
		private static Func<string, string> CS_0024_003C_003E9__CachedAnonymousMethodDelegate3;

		public App()
		{
			base.ShutdownMode = ShutdownMode.OnLastWindowClose;
			Singleton_Socket.Init();
			ServicePointManager.DefaultConnectionLimit = Settings.Default.ServicePoint_DefaultConnectionLimit;
			ServicePointManager.UseNagleAlgorithm = false;
			ServicePointManager.Expect100Continue = true;
			ServicePointManager.MaxServicePointIdleTime = 3600000;
			ServicePointManager.DnsRefreshTimeout = 240000;
			CheckDBVersion();
			StartMenuShortCutTool.DeleteDirectory("hyweb");
			StartMenuShortCutTool.DeleteDirectory("凌網科技股份有限公司");
			Process currentProcess = Process.GetCurrentProcess();
			currentProcess.PriorityClass = ProcessPriorityClass.AboveNormal;
		}

		private void Application_Exit(object sender, ExitEventArgs e)
		{
			CopyLog();
			Application.Current.Shutdown();
			Environment.Exit(0);
		}

		private void CopyLog()
		{
			if (ApplicationDeployment.IsNetworkDeployed)
			{
				TaskFactory factory = Task.Factory;
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate2 == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate2 = new Action(_003CCopyLog_003Eb__0);
				}
				factory.StartNew(CS_0024_003C_003E9__CachedAnonymousMethodDelegate2);
			}
		}

		private void CheckDBVersion()
		{
			try
			{
				int result = 0;
				int nowDBVersion = Settings.Default.NowDBVersion;
				Path.Combine(ClickOnceTool.GetDataPath(), Settings.Default.PaperLessDB_Path);
				string text = Path.Combine(ClickOnceTool.GetDataPath(), Settings.Default.ConfigIni_Path);
				if (File.Exists(text))
				{
					IniFileTool iniFileTool = new IniFileTool(text);
					try
					{
						int.TryParse(iniFileTool.IniReadValue("DB", "Version"), out result);
					}
					catch (Exception ex)
					{
						LogTool.Debug(ex);
					}
					if (!UpdateDBVersion(result))
					{
						throw new Exception("DB更新失敗!");
					}
					iniFileTool.IniWriteValue("DB", "Version", nowDBVersion.ToString());
				}
				else
				{
					UpdateDBVersion(result);
					using (new FileStream(text, FileMode.OpenOrCreate))
					{
					}
					IniFileTool iniFileTool2 = new IniFileTool(text);
					iniFileTool2.IniWriteValue("DB", "Version", nowDBVersion.ToString());
				}
			}
			catch (Exception ex2)
			{
				LogTool.Debug(ex2);
			}
		}

		private bool UpdateDBVersion(int LocalDBVersion)
		{
			TableAction("alter table nowlogin add  MeetingUserType nvarchar(100),MeetingBeginTime datetime,MeetingEndTime datetime");
			TableAction("alter table filerow add EncryptionKey nvarchar(100) ");
			TableAction("CREATE TABLE [LoginInfo] ([UserID] nvarchar(100), [UserPWD] nvarchar(100),UserJson nvarchar(2000))");
			TableAction("CREATE TABLE [UserData] ([UserID] nvarchar(100),[ListDate] nvarchar(100), [UserJson] nvarchar(2000))");
			TableAction("CREATE TABLE [MeetingData] ([MeetingID] nvarchar(100), [MeetingJson] ntext, [UserID] nvarchar(100))");
			TableAction("CREATE TABLE [SeriesData] ([SeriesJson] ntext, [UserID] nvarchar(100))");
			TableAction("CREATE TABLE [LawData] ([LawJson] ntext, [UserID] nvarchar(100))");
			TableAction("ALTER TABLE LoginInfo ALTER COLUMN UserJson ntext");
			TableAction("ALTER TABLE UserData ALTER COLUMN UserJson ntext");
			TableAction("ALTER TABLE FileRow ALTER COLUMN StorageFileName nvarchar(100);");
			TableAction("ALTER TABLE LawRow ALTER COLUMN StorageFileName nvarchar(100);");
			TableAction("ALTER TABLE FileRow Add COLUMN PDFFactoryParameterJson nvarchar(1000);");
			AccessTableAction("ALTER TABLE booknoteDetail alter COLUMN notes longText;");
			return true;
		}

		private void AccessTableAction(string sql)
		{
			try
			{
				BookManager bookManager = new BookManager(Path.Combine(ClickOnceTool.GetDataPath(), Settings.Default.bookInfo_Path));
				bookManager.sqlCommandNonQuery(sql);
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}

		private bool UpdateDBVersion_legacy(int LocalDBVersion)
		{
			bool result = false;
			try
			{
				switch (LocalDBVersion)
				{
				case 0:
					MSCE.ExecuteNonQuery("alter table nowlogin add  MeetingUserType nvarchar(50),MeetingBeginTime datetime,MeetingEndTime datetime");
					MSCE.ExecuteNonQuery("alter table filerow add EncryptionKey nvarchar(100) ");
					MSCE.ExecuteNonQuery("CREATE TABLE [LoginInfo] ([UserID] nvarchar(50), [UserPWD] nvarchar(50),UserJson nvarchar(2000))");
					MSCE.ExecuteNonQuery("CREATE TABLE [UserData] ([UserID] nvarchar(50),[ListDate] nvarchar(50), [UserJson] nvarchar(2000))");
					MSCE.ExecuteNonQuery("CREATE TABLE [MeetingData] ([MeetingID] nvarchar(50), [MeetingJson] ntext, [UserID] nvarchar(50))");
					MSCE.ExecuteNonQuery("CREATE TABLE [SeriesData] ([SeriesJson] ntext, [UserID] nvarchar(50))");
					break;
				case 1:
					MSCE.ExecuteNonQuery("alter table nowlogin add  MeetingUserType nvarchar(50)\r\n                                                ,MeetingBeginTime datetime\r\n                                                ,MeetingEndTime datetime");
					MSCE.ExecuteNonQuery("alter table filerow add EncryptionKey nvarchar(100) ");
					MSCE.ExecuteNonQuery("CREATE TABLE [LoginInfo] ([UserID] nvarchar(50), [UserPWD] nvarchar(50),UserJson nvarchar(2000))");
					MSCE.ExecuteNonQuery("CREATE TABLE [UserData] ([UserID] nvarchar(50),[ListDate] nvarchar(50), [UserJson] nvarchar(2000))");
					MSCE.ExecuteNonQuery("CREATE TABLE [MeetingData] ([MeetingID] nvarchar(50), [MeetingJson] ntext, [UserID] nvarchar(50))");
					MSCE.ExecuteNonQuery("CREATE TABLE [SeriesData] ([SeriesJson] ntext, [UserID] nvarchar(50))");
					break;
				case 2:
					MSCE.ExecuteNonQuery("alter table nowlogin add  MeetingUserType nvarchar(50)\r\n                                                ,MeetingBeginTime datetime\r\n                                                ,MeetingEndTime datetime");
					MSCE.ExecuteNonQuery("alter table filerow add EncryptionKey nvarchar(100) ");
					MSCE.ExecuteNonQuery("CREATE TABLE [LoginInfo] ([UserID] nvarchar(50), [UserPWD] nvarchar(50),UserJson nvarchar(2000))");
					MSCE.ExecuteNonQuery("CREATE TABLE [UserData] ([UserID] nvarchar(50),[ListDate] nvarchar(50), [UserJson] nvarchar(2000))");
					MSCE.ExecuteNonQuery("CREATE TABLE [MeetingData] ([MeetingID] nvarchar(50), [MeetingJson] ntext, [UserID] nvarchar(50))");
					MSCE.ExecuteNonQuery("CREATE TABLE [SeriesData] ([SeriesJson] ntext, [UserID] nvarchar(50))");
					break;
				case 3:
					MSCE.ExecuteNonQuery("alter table nowlogin add  MeetingUserType nvarchar(50)\r\n                                                ,MeetingBeginTime datetime\r\n                                                ,MeetingEndTime datetime");
					MSCE.ExecuteNonQuery("alter table filerow add EncryptionKey nvarchar(100) ");
					MSCE.ExecuteNonQuery("CREATE TABLE [LoginInfo] ([UserID] nvarchar(50), [UserPWD] nvarchar(50),UserJson nvarchar(2000))");
					MSCE.ExecuteNonQuery("CREATE TABLE [UserData] ([UserID] nvarchar(50),[ListDate] nvarchar(50), [UserJson] nvarchar(2000))");
					MSCE.ExecuteNonQuery("CREATE TABLE [MeetingData] ([MeetingID] nvarchar(50), [MeetingJson] ntext, [UserID] nvarchar(50))");
					MSCE.ExecuteNonQuery("CREATE TABLE [SeriesData] ([SeriesJson] ntext, [UserID] nvarchar(50))");
					break;
				case 4:
					MSCE.ExecuteNonQuery("alter table filerow add EncryptionKey nvarchar(100) ");
					MSCE.ExecuteNonQuery("CREATE TABLE [LoginInfo] ([UserID] nvarchar(50), [UserPWD] nvarchar(50),UserJson nvarchar(2000))");
					MSCE.ExecuteNonQuery("CREATE TABLE [UserData] ([UserID] nvarchar(50),[ListDate] nvarchar(50), [UserJson] nvarchar(2000))");
					MSCE.ExecuteNonQuery("CREATE TABLE [MeetingData] ([MeetingID] nvarchar(50), [MeetingJson] ntext, [UserID] nvarchar(50))");
					MSCE.ExecuteNonQuery("CREATE TABLE [SeriesData] ([SeriesJson] ntext, [UserID] nvarchar(50))");
					break;
				case 5:
					MSCE.ExecuteNonQuery("alter table filerow add EncryptionKey nvarchar(100) ");
					MSCE.ExecuteNonQuery("CREATE TABLE [LoginInfo] ([UserID] nvarchar(50), [UserPWD] nvarchar(50),UserJson nvarchar(2000))");
					MSCE.ExecuteNonQuery("CREATE TABLE [UserData] ([UserID] nvarchar(50),[ListDate] nvarchar(50), [UserJson] nvarchar(2000))");
					MSCE.ExecuteNonQuery("CREATE TABLE [MeetingData] ([MeetingID] nvarchar(50), [MeetingJson] ntext, [UserID] nvarchar(50))");
					MSCE.ExecuteNonQuery("CREATE TABLE [SeriesData] ([SeriesJson] ntext, [UserID] nvarchar(50))");
					break;
				}
				result = true;
				return result;
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
				return result;
			}
		}

		private void TableAction(string SQL)
		{
			try
			{
				MSCE.ExecuteNonQuery(SQL);
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
			Application.Current.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(Application_DispatcherUnhandledException);
		}

		private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			Exception exception = e.Exception;
			logger.Debug(string.Format("\r\n{0}\r\n{1}\r\n{2}\r\n{3}\r\n", exception.Source, exception.TargetSite, exception.Message, exception.StackTrace));
			e.Handled = true;
		}

		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			logger.Debug(string.Format("\r\n{0}\r\n{1}\r\n{2}\r\n{3}\r\n", "1", "2", "3", "4"));
		}

		private void Application_SessionEnding(object sender, SessionEndingCancelEventArgs e)
		{
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				base.Startup += new StartupEventHandler(Application_Startup);
				base.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(Application_DispatcherUnhandledException);
				base.SessionEnding += new SessionEndingCancelEventHandler(Application_SessionEnding);
				base.Exit += new ExitEventHandler(Application_Exit);
				base.StartupUri = new Uri("Login.xaml", UriKind.Relative);
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/app.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[STAThread]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[DebuggerNonUserCode]
		public static void Main()
		{
			App app = new App();
			app.InitializeComponent();
			app.Run();
		}

		[CompilerGenerated]
		private static void _003CCopyLog_003Eb__0()
		{
			try
			{
				string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
				string[] directories = Directory.GetDirectories(Path.Combine(baseDirectory, "Logs"));
				if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate3 == null)
				{
					CS_0024_003C_003E9__CachedAnonymousMethodDelegate3 = new Func<string, string>(_003CCopyLog_003Eb__1);
				}
				string sourcePath = Enumerable.First(Enumerable.OrderByDescending(directories, CS_0024_003C_003E9__CachedAnonymousMethodDelegate3));
				DirectoryTool.FullCopyDirectories(sourcePath, ClickOnceTool.GetFilePath());
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
		}

		[CompilerGenerated]
		private static string _003CCopyLog_003Eb__1(string f)
		{
			return f;
		}
	}
}
