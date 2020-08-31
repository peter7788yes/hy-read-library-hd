using BookFormatLoader;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Xml;

namespace ReadPageModule
{
	public class TocButton : UserControl, IComponentConnector, IStyleConnector
	{
		[CompilerGenerated]
		private ObservableCollection<navPoint> _003CTocContent_003Ek__BackingField;

		public List<PagePath> LImgList;

		[CompilerGenerated]
		private Dictionary<int, string> _003CindexContentTable_003Ek__BackingField;

		internal Button tocButton;

		internal Popup tocPopup;

		internal Grid tocPanelGrid;

		internal TreeView tocTreeView;

		private bool _contentLoaded;

		public ObservableCollection<navPoint> TocContent
		{
			[CompilerGenerated]
			get
			{
				return _003CTocContent_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CTocContent_003Ek__BackingField = value;
			}
		}

		public Dictionary<int, string> indexContentTable
		{
			[CompilerGenerated]
			get
			{
				return _003CindexContentTable_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CindexContentTable_003Ek__BackingField = value;
			}
		}

		public new Visibility Visibility
		{
			get
			{
				return tocButton.Visibility;
			}
			set
			{
				tocButton.Visibility = value;
			}
		}

		public void SetTocXmlDocument(XmlDocument tocXML)
		{
			foreach (XmlNode childNode in tocXML.ChildNodes)
			{
				if (childNode.Name == "ncx")
				{
					foreach (XmlNode childNode2 in childNode.ChildNodes)
					{
						if (childNode2.Name == "navMap")
						{
							foreach (XmlNode childNode3 in childNode2.ChildNodes)
							{
								navPoint navPoint = new navPoint();
								a(childNode3, navPoint);
								navPoint.IsExpanded = true;
								TocContent.Add(navPoint);
							}
						}
					}
				}
			}
			tocTreeView.ItemsSource = TocContent;
		}

		private void a(XmlNode A_0, navPoint A_1)
		{
			foreach (XmlNode childNode in A_0.ChildNodes)
			{
				if (childNode.Name == "navLabel")
				{
					A_1.navLabel = childNode.InnerText;
				}
				else if (childNode.Name == "content")
				{
					A_1.content = childNode.Attributes.GetNamedItem("src").Value;
					for (int i = 0; i < LImgList.Count; i++)
					{
						if (LImgList[i].path.Replace("HYWEB\\", "").Equals(A_1.content))
						{
							A_1.targetIndex = i;
							break;
						}
					}
					if (!indexContentTable.ContainsKey(A_1.targetIndex))
					{
						indexContentTable.Add(A_1.targetIndex, A_1.navLabel);
					}
				}
				else if (childNode.HasChildNodes)
				{
					navPoint navPoint = new navPoint();
					a(childNode, navPoint);
					navPoint.IsExpanded = true;
					A_1.subNavPoint.Add(navPoint);
				}
			}
		}

		public TocButton()
		{
			InitializeComponent();
			TocContent = new ObservableCollection<navPoint>();
			indexContentTable = new Dictionary<int, string>();
		}

		private void a(object A_0, RequestBringIntoViewEventArgs A_1)
		{
			A_1.Handled = true;
		}

		private void a(object A_0, RoutedEventArgs A_1)
		{
			tocPopup.IsOpen = !tocPopup.IsOpen;
			A_1.Handled = true;
		}

		public string getNavLabelByIndex(int index)
		{
			if (indexContentTable.ContainsKey(index))
			{
				return indexContentTable[index];
			}
			return "";
		}

		private void a(object A_0, EventArgs A_1)
		{
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/ReadPageModule;component/tocbutton.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 2:
				tocButton = (Button)target;
				tocButton.Click += new RoutedEventHandler(a);
				break;
			case 3:
				tocPopup = (Popup)target;
				tocPopup.Closed += new EventHandler(a);
				break;
			case 4:
				tocPanelGrid = (Grid)target;
				break;
			case 5:
				tocTreeView = (TreeView)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				EventSetter eventSetter = new EventSetter();
				eventSetter.Event = FrameworkElement.RequestBringIntoViewEvent;
				eventSetter.Handler = new RequestBringIntoViewEventHandler(a);
				((Style)target).Setters.Add(eventSetter);
			}
		}
	}
}
