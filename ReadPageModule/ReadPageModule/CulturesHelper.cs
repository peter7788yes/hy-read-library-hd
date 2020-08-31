using ReadPageModule.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;

namespace ReadPageModule
{
	public class CulturesHelper
	{
		private static bool a = false;

		private static List<CultureInfo> b = new List<CultureInfo>();

		private static ObjectDataProvider c;

		private static CultureInfo d = new CultureInfo("zh-TW");

		public static List<CultureInfo> SupportedCultures
		{
			get
			{
				return b;
			}
		}

		public static ObjectDataProvider ResourceProvider
		{
			get
			{
				if (c == null)
				{
					c = (ObjectDataProvider)System.Windows.Application.Current.FindResource("Resources");
				}
				return c;
			}
		}

		public CulturesHelper()
		{
			if (!a)
			{
				CultureInfo cultureInfo = new CultureInfo("");
				string[] directories = Directory.GetDirectories(System.Windows.Forms.Application.StartupPath);
				foreach (string path in directories)
				{
					try
					{
						DirectoryInfo directoryInfo = new DirectoryInfo(path);
						cultureInfo = CultureInfo.GetCultureInfo(directoryInfo.Name);
						if (directoryInfo.GetFiles(Path.GetFileNameWithoutExtension(System.Windows.Forms.Application.ExecutablePath) + ".resources.dll").Length != 0)
						{
							b.Add(cultureInfo);
						}
					}
					catch (ArgumentException)
					{
					}
				}
				if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
				{
					Resources.Culture = d;
					Settings.Default.DefaultCulture = d;
				}
				else if (b.Count > 0 && Settings.Default.DefaultCulture != null)
				{
					Resources.Culture = Settings.Default.DefaultCulture;
				}
				a = true;
			}
		}

		public Resources GetResourceInstance()
		{
			return new Resources();
		}

		public Resources GetResourceInstance(string cultureName)
		{
			ChangeCulture(new CultureInfo(cultureName));
			return new Resources();
		}

		public static void ChangeCulture(CultureInfo culture)
		{
			if (b.Contains(culture))
			{
				Resources.Culture = culture;
				Settings.Default.DefaultCulture = culture;
				Settings.Default.Save();
				ResourceProvider.Refresh();
			}
		}
	}
}
