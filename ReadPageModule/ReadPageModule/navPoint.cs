using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ReadPageModule
{
	public class navPoint : INotifyPropertyChanged
	{
		[CompilerGenerated]
		private PropertyChangedEventHandler a;

		[CompilerGenerated]
		private string b;

		[CompilerGenerated]
		private string c;

		[CompilerGenerated]
		private string d;

		[CompilerGenerated]
		private string e;

		[CompilerGenerated]
		private int f;

		[CompilerGenerated]
		private List<navPoint> g;

		private bool h;

		public string id
		{
			[CompilerGenerated]
			get
			{
				return b;
			}
			[CompilerGenerated]
			set
			{
				b = value;
			}
		}

		public string playOrder
		{
			[CompilerGenerated]
			get
			{
				return c;
			}
			[CompilerGenerated]
			set
			{
				c = value;
			}
		}

		public string navLabel
		{
			[CompilerGenerated]
			get
			{
				return d;
			}
			[CompilerGenerated]
			set
			{
				d = value;
			}
		}

		public string content
		{
			[CompilerGenerated]
			get
			{
				return e;
			}
			[CompilerGenerated]
			set
			{
				e = value;
			}
		}

		public int targetIndex
		{
			[CompilerGenerated]
			get
			{
				return f;
			}
			[CompilerGenerated]
			set
			{
				f = value;
			}
		}

		public List<navPoint> subNavPoint
		{
			[CompilerGenerated]
			get
			{
				return g;
			}
			[CompilerGenerated]
			set
			{
				g = value;
			}
		}

		public bool IsExpanded
		{
			get
			{
				return h;
			}
			set
			{
				h = value;
				if (a != null)
				{
					a(this, new PropertyChangedEventArgs("IsExpanded"));
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged
		{
			[CompilerGenerated]
			add
			{
				PropertyChangedEventHandler propertyChangedEventHandler = a;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Combine(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange(ref a, value2, propertyChangedEventHandler2);
				}
				while ((object)propertyChangedEventHandler != propertyChangedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				PropertyChangedEventHandler propertyChangedEventHandler = a;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Remove(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange(ref a, value2, propertyChangedEventHandler2);
				}
				while ((object)propertyChangedEventHandler != propertyChangedEventHandler2);
			}
		}

		public navPoint()
		{
			subNavPoint = new List<navPoint>();
		}
	}
}
