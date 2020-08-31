using BookFormatLoader;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Xml;

namespace ReadPageModule
{
	public class TocButtonController
	{
		[CompilerGenerated]
		private ObservableCollection<navPoint> m_a;

		public List<PagePath> LImgList;

		[CompilerGenerated]
		private Dictionary<int, string> b;

		public ObservableCollection<navPoint> TocContent
		{
			[CompilerGenerated]
			get
			{
				return this.m_a;
			}
			[CompilerGenerated]
			set
			{
				this.m_a = value;
			}
		}

		public Dictionary<int, string> indexContentTable
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
								try
								{
									navPoint navPoint = new navPoint();
									a(childNode3, navPoint);
									navPoint.IsExpanded = true;
									TocContent.Add(navPoint);
								}
								catch
								{
								}
							}
						}
					}
				}
			}
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
				else if (childNode.HasChildNodes || childNode.Name == "navPoint ")
				{
					navPoint navPoint = new navPoint();
					a(childNode, navPoint);
					navPoint.IsExpanded = true;
					A_1.subNavPoint.Add(navPoint);
				}
			}
		}

		public TocButtonController(List<PagePath> LImgList)
		{
			this.LImgList = LImgList;
			TocContent = new ObservableCollection<navPoint>();
			indexContentTable = new Dictionary<int, string>();
		}

		public string getNavLabelByIndex(int index)
		{
			if (indexContentTable.ContainsKey(index))
			{
				return indexContentTable[index];
			}
			for (int i = 1; i < indexContentTable.Count; i++)
			{
				int key = index - i;
				int num = index + i;
				int num2 = indexContentTable.ContainsKey(key) ? 1 : 0;
				bool flag = indexContentTable.ContainsKey(num) ? true : false;
				if (num2 != 0)
				{
					return indexContentTable[key];
				}
				if (flag)
				{
					string result = "";
					foreach (KeyValuePair<int, string> item in indexContentTable)
					{
						if (item.Key.Equals(num))
						{
							return result;
						}
						result = item.Value;
					}
				}
			}
			return "";
		}
	}
}
