using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace PaperLess_Emeeting.WS
{
	[GeneratedCode("System.Web.Services", "4.0.30319.18408")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class LawDataCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public XmlNode Result
		{
			get
			{
				RaiseExceptionIfNecessary();
				return (XmlNode)results[0];
			}
		}

		internal LawDataCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
}
