using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace ReadPageModule.Properties
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[CompilerGenerated]
	[DebuggerNonUserCode]
	public class Resources
	{
		private static ResourceManager a;

		private static CultureInfo b;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (a == null)
				{
					a = new ResourceManager("ReadPageModule.Properties.Resources", typeof(Resources).Assembly);
				}
				return a;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return b;
			}
			set
			{
				b = value;
			}
		}

		internal Resources()
		{
		}
	}
}
