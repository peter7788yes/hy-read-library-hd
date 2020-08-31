using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace PaperLess_Emeeting.App_Code.Tools
{
	public class FindVisualChildTool
	{
		public static T ByName<T>(DependencyObject parent, string name) where T : DependencyObject
		{
			if (parent != null)
			{
				for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
				{
					DependencyObject child = VisualTreeHelper.GetChild(parent, i);
					string text = child.GetValue(FrameworkElement.NameProperty) as string;
					if (text.Equals(name))
					{
						return child as T;
					}
					T val = ByName<T>(child, name);
					if (val != null)
					{
						return val;
					}
				}
			}
			return null;
		}

		public static void ByType<T>(DependencyObject parent, ref List<DependencyObject> DPs) where T : DependencyObject
		{
			if (parent == null)
			{
				return;
			}
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
			{
				DependencyObject child = VisualTreeHelper.GetChild(parent, i);
				if (object.Equals(child.GetType(), typeof(T)))
				{
					DPs.Add(child);
				}
				if (VisualTreeHelper.GetChildrenCount(child) > 0)
				{
					ByType<T>(child, ref DPs);
				}
			}
		}
	}
}
