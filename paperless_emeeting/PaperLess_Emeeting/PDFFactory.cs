using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

namespace PaperLess_Emeeting
{
	public class PDFFactory : Window, IComponentConnector
	{
		private bool _contentLoaded;

		public PDFFactory(string myBookPath, int totalPage, float penMemoCanvasWidth, float penMemoCanvasHeight, string account, string bookId, string dbPath)
		{
			InitializeComponent();
			Close();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/pdffactory.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			_contentLoaded = true;
		}
	}
}
