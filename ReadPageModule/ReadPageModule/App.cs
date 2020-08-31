using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;

namespace ReadPageModule
{
	public class App : Application
	{
		private bool a;

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!a)
			{
				a = true;
				base.StartupUri = new Uri("ReadPage.xaml", UriKind.Relative);
				Uri resourceLocator = new Uri("/ReadPageModule;component/app.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[STAThread]
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public static void Main()
		{
			App app = new App();
			app.InitializeComponent();
			app.Run();
		}
	}
}
