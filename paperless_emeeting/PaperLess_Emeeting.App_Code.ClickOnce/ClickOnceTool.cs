using System;
using System.Deployment.Application;
using System.IO;
using System.Reflection;

namespace PaperLess_Emeeting.App_Code.ClickOnce
{
	public class ClickOnceTool
	{
		public static string GetDataPath()
		{
			string text = "";
			if (ApplicationDeployment.IsNetworkDeployed)
			{
				return ApplicationDeployment.CurrentDeployment.DataDirectory;
			}
			return AppDomain.CurrentDomain.BaseDirectory;
		}

		public static string GetFilePath()
		{
			string text = "";
			if (ApplicationDeployment.IsNetworkDeployed)
			{
				return GetMyDocument();
			}
			return AppDomain.CurrentDomain.BaseDirectory;
		}

		private static string GetMyDocument()
		{
			string text = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Hyweb\\" + Assembly.GetExecutingAssembly().GetName().Name;
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			return text;
		}

		public static string GetUserAppDataPath()
		{
			string empty = string.Empty;
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			Type typeFromHandle = typeof(AssemblyCompanyAttribute);
			object[] customAttributes = entryAssembly.GetCustomAttributes(typeFromHandle, false);
			AssemblyCompanyAttribute assemblyCompanyAttribute = (AssemblyCompanyAttribute)customAttributes[0];
			empty = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			return empty + "\\Hyweb\\" + Assembly.GetExecutingAssembly().GetName().Name;
		}

		private static string GetAPP_ProgramFilesx86()
		{
			string text = ProgramFilesx86() + "\\Hyweb\\" + Assembly.GetExecutingAssembly().GetName().Name;
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			return text;
		}

		private static string ProgramFilesx86()
		{
			if (8 == IntPtr.Size || !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432")))
			{
				return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
			}
			return Environment.GetEnvironmentVariable("ProgramFiles");
		}
	}
}
