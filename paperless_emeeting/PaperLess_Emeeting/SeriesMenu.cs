using PaperLess_ViewModel;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PaperLess_Emeeting
{
	public class SeriesMenu : UserControl, IComponentConnector
	{
		internal TextBlock txtSeriesName;

		internal Image btnImg;

		private bool _contentLoaded;

		[CompilerGenerated]
		private SeriesDataSeriesMeetingSeries _003CseriesDataSeriesMeetingSeries_003Ek__BackingField;

		public SeriesDataSeriesMeetingSeries seriesDataSeriesMeetingSeries
		{
			[CompilerGenerated]
			get
			{
				return _003CseriesDataSeriesMeetingSeries_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CseriesDataSeriesMeetingSeries_003Ek__BackingField = value;
			}
		}

		public event SeriesMeetingCT_ChangeMeetingRoomWP_Function SeriesMeetingCT_ChangeMeetingRoomWP_Event;

		public SeriesMenu(SeriesDataSeriesMeetingSeries seriesDataSeriesMeetingSeries, SeriesMeetingCT_ChangeMeetingRoomWP_Function callback)
		{
			InitializeComponent();
			this.seriesDataSeriesMeetingSeries = seriesDataSeriesMeetingSeries;
			this.SeriesMeetingCT_ChangeMeetingRoomWP_Event = callback;
			base.Loaded += new RoutedEventHandler(SeriesMenu_Loaded);
			base.Unloaded += new RoutedEventHandler(SeriesMenu_Unloaded);
		}

		private void SeriesMenu_Loaded(object sender, RoutedEventArgs e)
		{
			Task.Factory.StartNew(new Action(_003CSeriesMenu_Loaded_003Eb__0));
		}

		private void SeriesMenu_Unloaded(object sender, RoutedEventArgs e)
		{
		}

		private void InitSelectDB()
		{
		}

		private void InitEvent()
		{
			base.MouseEnter += new MouseEventHandler(_003CInitEvent_003Eb__2);
			base.MouseLeave += new MouseEventHandler(_003CInitEvent_003Eb__3);
			base.MouseLeftButtonDown += new MouseButtonEventHandler(SeriesMenu_MouseLeftButtonDown);
		}

		private void SeriesMenu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			MouseTool.ShowLoading();
			btnImg.Source = new BitmapImage(new Uri("images/icon_arrow_active.png", UriKind.Relative));
			base.Background = ColorTool.HexColorToBrush("#019fde");
			txtSeriesName.Foreground = Brushes.White;
			if (this.SeriesMeetingCT_ChangeMeetingRoomWP_Event != null)
			{
				this.SeriesMeetingCT_ChangeMeetingRoomWP_Event(seriesDataSeriesMeetingSeries.ID);
			}
		}

		private void InitUI()
		{
			txtSeriesName.Text = seriesDataSeriesMeetingSeries.Name;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/seriesmenu.xaml", UriKind.Relative);
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
				txtSeriesName = (TextBlock)target;
				break;
			case 2:
				btnImg = (Image)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}

		[CompilerGenerated]
		private void _003CSeriesMenu_Loaded_003Eb__0()
		{
			InitSelectDB();
			base.Dispatcher.BeginInvoke(new Action(_003CSeriesMenu_Loaded_003Eb__1));
		}

		[CompilerGenerated]
		private void _003CSeriesMenu_Loaded_003Eb__1()
		{
			InitUI();
			InitEvent();
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__2(object sender, MouseEventArgs e)
		{
			MouseTool.ShowHand();
			if (!btnImg.Source.ToString().Contains("images/icon_arrow_active.png"))
			{
				base.Background = ColorTool.HexColorToBrush("#f1f5f6");
			}
		}

		[CompilerGenerated]
		private void _003CInitEvent_003Eb__3(object sender, MouseEventArgs e)
		{
			MouseTool.ShowArrow();
			if (!btnImg.Source.ToString().Contains("images/icon_arrow_active.png"))
			{
				base.Background = Brushes.Transparent;
			}
		}
	}
}
