using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace ReadPageModule.Properties
{
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings a = (Settings)SettingsBase.Synchronized(new Settings());

		public static Settings Default
		{
			get
			{
				return a;
			}
		}

		[UserScopedSetting]
		[DefaultSettingValue("zh-TW")]
		[DebuggerNonUserCode]
		public CultureInfo DefaultCulture
		{
			get
			{
				return (CultureInfo)this["DefaultCulture"];
			}
			set
			{
				this["DefaultCulture"] = value;
			}
		}
	}
}
