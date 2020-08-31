using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace PaperLess_Emeeting.WS
{
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("System.Web.Services", "4.0.30319.18408")]
	public class UserDataCompletedEventArgs : AsyncCompletedEventArgs
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

		internal UserDataCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
}
