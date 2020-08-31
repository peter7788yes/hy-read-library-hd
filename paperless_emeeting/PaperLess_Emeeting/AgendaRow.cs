using PaperLess_Emeeting.App_Code.MessageBox;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace PaperLess_Emeeting
{
	public class AgendaRow : UserControl, IComponentConnector
	{
		private Dictionary<string, string> cbData;

		internal Grid imgHasFile;

		internal TextBlock txtAgendaName;

		internal TextBlock txtCaption;

		internal ComboBox cbProgress;

		internal Button btnProgress;

		private bool _contentLoaded;

		[CompilerGenerated]
		private MeetingDataAgenda _003CmeetingDataAgenda_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CMeetingID_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CUserID_003Ek__BackingField;

		[CompilerGenerated]
		private bool _003CIsHasFile_003Ek__BackingField;

		[CompilerGenerated]
		private bool _003CIsHasChildren_003Ek__BackingField;

		[CompilerGenerated]
		private bool _003CIsParent_003Ek__BackingField;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate8;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegate9;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegatea;

		[CompilerGenerated]
		private static MouseEventHandler CS_0024_003C_003E9__CachedAnonymousMethodDelegateb;

		[CompilerGenerated]
		private static Func<KeyValuePair<string, string>, string> CS_0024_003C_003E9__CachedAnonymousMethodDelegatef;

		[CompilerGenerated]
		private static Action<bool> CS_0024_003C_003E9__CachedAnonymousMethodDelegate10;

		public MeetingDataAgenda meetingDataAgenda
		{
			[CompilerGenerated]
			get
			{
				return _003CmeetingDataAgenda_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CmeetingDataAgenda_003Ek__BackingField = value;
			}
		}

		public string MeetingID
		{
			[CompilerGenerated]
			get
			{
				return _003CMeetingID_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CMeetingID_003Ek__BackingField = value;
			}
		}

		public string UserID
		{
			[CompilerGenerated]
			get
			{
				return _003CUserID_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CUserID_003Ek__BackingField = value;
			}
		}

		public bool IsHasFile
		{
			[CompilerGenerated]
			get
			{
				return _003CIsHasFile_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CIsHasFile_003Ek__BackingField = value;
			}
		}

		public bool IsHasChildren
		{
			[CompilerGenerated]
			get
			{
				return _003CIsHasChildren_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CIsHasChildren_003Ek__BackingField = value;
			}
		}

		public bool IsParent
		{
			[CompilerGenerated]
			get
			{
				return _003CIsParent_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CIsParent_003Ek__BackingField = value;
			}
		}

		public event MeetingDataCT_ShowAgendaFile_Function MeetingDataCT_ShowAgendaFile_Event;

		public event MeetingDataCT_GetAgendaInwWorkCount_Function MeetingDataCT_GetAgendaInwWorkCount_Event;

		public AgendaRow(string MeetingID, string UserID, bool IsHasFile, bool IsHasChildren, bool IsParent, MeetingDataAgenda meetingDataAgenda, MeetingDataCT_ShowAgendaFile_Function callback1, MeetingDataCT_GetAgendaInwWorkCount_Function callback2)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("未開始", "N");
			dictionary.Add("進行中", "U");
			dictionary.Add("已結束", "D");
			cbData = dictionary;
			base._002Ector();
			InitializeComponent();
			this.MeetingID = MeetingID;
			this.UserID = UserID;
			this.IsHasFile = IsHasFile;
			this.IsHasChildren = IsHasChildren;
			this.IsParent = IsParent;
			this.meetingDataAgenda = meetingDataAgenda;
			MeetingDataCT_ShowAgendaFile_Event += callback1;
			MeetingDataCT_GetAgendaInwWorkCount_Event += callback2;
			base.Loaded += new RoutedEventHandler(AgendaRow_Loaded);
		}

		private void AgendaRow_Loaded(object sender, RoutedEventArgs e)
		{
			InitUI_Part1();
			Task.Factory.StartNew(new Action(_003CAgendaRow_Loaded_003Eb__1));
		}

		private void InitEvent()
		{
			Button button = btnProgress;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate8 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate8 = new MouseEventHandler(_003CInitEvent_003Eb__3);
			}
			button.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegate8;
			Button button2 = btnProgress;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate9 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate9 = new MouseEventHandler(_003CInitEvent_003Eb__4);
			}
			button2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegate9;
			btnProgress.Click += new RoutedEventHandler(btnProgress_Click);
			cbProgress.MouseLeave += new MouseEventHandler(_003CInitEvent_003Eb__5);
			TextBlock textBlock = txtAgendaName;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegatea == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegatea = new MouseEventHandler(_003CInitEvent_003Eb__6);
			}
			textBlock.MouseEnter += CS_0024_003C_003E9__CachedAnonymousMethodDelegatea;
			TextBlock textBlock2 = txtAgendaName;
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegateb == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegateb = new MouseEventHandler(_003CInitEvent_003Eb__7);
			}
			textBlock2.MouseLeave += CS_0024_003C_003E9__CachedAnonymousMethodDelegateb;
			txtAgendaName.MouseLeftButtonDown += new MouseButtonEventHandler(txtName_MouseLeftButtonDown);
		}

		private void SelectionChangeCommitted(object sender, SelectionChangedEventArgs e)
		{
		}

		private void txtName_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Brush brush = ColorTool.HexColorToBrush("#0093b0");
			if (txtAgendaName.Foreground.ToString().Equals(brush.ToString()))
			{
				this.MeetingDataCT_ShowAgendaFile_Event(meetingDataAgenda.ID, meetingDataAgenda.ParentID, true);
			}
			else
			{
				this.MeetingDataCT_ShowAgendaFile_Event(meetingDataAgenda.ID, meetingDataAgenda.ParentID, false);
			}
			txtAgendaName.Foreground = brush;
			txtAgendaName.Inlines.LastInline.Foreground = brush;
			txtCaption.Foreground = brush;
		}

		private void cbProgress_SelectionChanged(object sender, EventArgs e)
		{
			cbProgress.Visibility = Visibility.Collapsed;
			meetingDataAgenda.Progress = cbProgress.SelectedValue.ToString();
			Button button = btnProgress;
			IEnumerable<KeyValuePair<string, string>> source = Enumerable.Where(cbData, new Func<KeyValuePair<string, string>, bool>(_003CcbProgress_SelectionChanged_003Eb__c));
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegatef == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegatef = new Func<KeyValuePair<string, string>, string>(_003CcbProgress_SelectionChanged_003Eb__d);
			}
			button.Content = Enumerable.First(Enumerable.Select(source, CS_0024_003C_003E9__CachedAnonymousMethodDelegatef));
			ChangeColor(btnProgress.Content.ToString());
			btnProgress.Visibility = Visibility.Visible;
			string meetingID = MeetingID;
			string userID = UserID;
			string iD = meetingDataAgenda.ID;
			string agendaStatus = cbProgress.SelectedValue.ToString();
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate10 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate10 = new Action<bool>(_003CcbProgress_SelectionChanged_003Eb__e);
			}
			GetProgressUpload.AsyncPOST(meetingID, userID, iD, agendaStatus, CS_0024_003C_003E9__CachedAnonymousMethodDelegate10);
		}

		private void btnProgress_Click(object sender, RoutedEventArgs e)
		{
			if (this.MeetingDataCT_GetAgendaInwWorkCount_Event(meetingDataAgenda.ID) > 0)
			{
				AutoClosingMessageBox.Show("請先完成進行中的議程");
				return;
			}
			btnProgress.Visibility = Visibility.Collapsed;
			cbProgress.Visibility = Visibility.Visible;
			cbProgress.IsDropDownOpen = true;
		}

		private void InitUI_Part1()
		{
			txtAgendaName.Inlines.Add(new Run(meetingDataAgenda.Agenda));
			if (meetingDataAgenda.Caption != null && !meetingDataAgenda.Caption.Equals(""))
			{
				txtCaption.Text = meetingDataAgenda.Caption;
				txtCaption.Foreground = new SolidColorBrush(Color.FromRgb(161, 161, 157));
				txtCaption.Visibility = Visibility.Visible;
			}
			string text = "";
			if (meetingDataAgenda.ProposalUnit != null && !meetingDataAgenda.ProposalUnit.Trim().Equals(""))
			{
				text = string.Format(" ({0})", meetingDataAgenda.ProposalUnit);
			}
			InlineCollection inlines = txtAgendaName.Inlines;
			Run run = new Run(text);
			run.Foreground = new SolidColorBrush(Color.FromRgb(161, 161, 157));
			inlines.Add(run);
			string parentID = meetingDataAgenda.ParentID;
			if (!IsParent)
			{
				txtAgendaName.Margin = new Thickness(txtAgendaName.Margin.Left + 23.0, txtAgendaName.Margin.Top, txtAgendaName.Margin.Right, txtAgendaName.Margin.Bottom);
			}
			if (IsHasFile)
			{
				imgHasFile.Visibility = Visibility.Visible;
				btnProgress.Visibility = Visibility.Visible;
			}
			if (IsParent ^ IsHasChildren)
			{
				btnProgress.Visibility = Visibility.Visible;
			}
			if (meetingDataAgenda.Progress == null || meetingDataAgenda.Progress.Equals(""))
			{
				btnProgress.Visibility = Visibility.Collapsed;
			}
		}

		private void InitUI_Part2()
		{
			cbProgress.ItemsSource = cbData;
			cbProgress.DisplayMemberPath = "Key";
			cbProgress.SelectedValuePath = "Value";
			cbProgress.SelectedValue = meetingDataAgenda.Progress;
			btnProgress.Content = cbProgress.Text;
			ChangeColor(btnProgress.Content.ToString());
		}

		private void ChangeColor(string cbDataKey)
		{
			switch (cbDataKey)
			{
			case "未開始":
				btnProgress.Foreground = ColorTool.HexColorToBrush("#3746db");
				break;
			case "已結束":
				btnProgress.Foreground = ColorTool.HexColorToBrush("#000000");
				break;
			case "進行中":
				btnProgress.Foreground = ColorTool.HexColorToBrush("#ff1a1a");
				break;
			}
		}

		private void InitSelectDB()
		{
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/agendarow.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				imgHasFile = (Grid)target;
				break;
			case 2:
				txtAgendaName = (TextBlock)target;
				break;
			case 3:
				txtCaption = (TextBlock)target;
				break;
			case 4:
				cbProgress = (ComboBox)target;
				break;
			case 5:
				btnProgress = (Button)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}

		[CompilerGenerated]
		private void _003CAgendaRow_Loaded_003Eb__1()
		{
			base.Dispatcher.BeginInvoke(new Action(_003CAgendaRow_Loaded_003Eb__2));
		}

		[CompilerGenerated]
		private void _003CAgendaRow_Loaded_003Eb__2()
		{
			InitUI_Part2();
			InitEvent();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__3(object sender, MouseEventArgs e)
		{
			MouseTool.ShowHand();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__4(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__5(object sender, MouseEventArgs e)
		{
			cbProgress_SelectionChanged(cbProgress, new EventArgs());
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__6(object sender, MouseEventArgs e)
		{
			MouseTool.ShowHand();
		}

		[CompilerGenerated]
		private static void _003CInitEvent_003Eb__7(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
		}

		[CompilerGenerated]
		private bool _003CcbProgress_SelectionChanged_003Eb__c(KeyValuePair<string, string> x)
		{
			return x.Value.Equals(cbProgress.SelectedValue);
		}

		[CompilerGenerated]
		private static string _003CcbProgress_SelectionChanged_003Eb__d(KeyValuePair<string, string> x)
		{
			return x.Key;
		}

		[CompilerGenerated]
		private static void _003CcbProgress_SelectionChanged_003Eb__e(bool x)
		{
		}
	}
}
