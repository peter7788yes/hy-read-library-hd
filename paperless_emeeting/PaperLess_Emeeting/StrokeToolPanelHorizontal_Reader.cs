using MultiLanquageModule;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Ink;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PaperLess_Emeeting
{
	public class StrokeToolPanelHorizontal_Reader : UserControl, IComponentConnector
	{
		private Color currentColor;

		private double currentRadiusWidth;

		private DrawingAttributes strokeAtt;

		private bool isHighlighter;

		public DrawingAttributes drawingAttr;

		public MultiLanquageManager langMng;

		internal StrokeToolPanelHorizontal_Reader mainPanel;

		internal Button undoButton;

		internal Button redoButton;

		internal Button penButton;

		internal Popup penTypePopup;

		internal Grid penSubPanelGrid;

		internal Button curveButton;

		internal Button straightPanelButton;

		internal Button colorPanelButton;

		internal Popup colorPopup;

		internal Grid penToolPanelGrid;

		internal Button transparentButton;

		internal Button nonTransparentButton;

		internal Path demoStroke;

		internal Slider strokeWidthSlider;

		internal Grid colorPanel;

		internal Button eraserButton;

		internal Button deleteAllButton;

		private bool _contentLoaded;

		public event StrokeChangeEvent strokeChange;

		public event StrokeUndoEvent strokeUndo;

		public event StrokeRedoEvent strokeRedo;

		public event StrokeDeleteAllEvent strokeDelAll;

		public event StrokeDeleteEvent strokeDel;

		public event StrokeEraseEvent strokeErase;

		public event showPenToolPanelEvent showPenToolPanel;

		public event StrokeCurveEvent strokeCurve;

		public event StrokeLineEvent strokeLine;

		public StrokeToolPanelHorizontal_Reader()
		{
			InitializeComponent();
			currentColor = Colors.Gray;
			currentRadiusWidth = 5.0;
			strokeAtt = new DrawingAttributes();
			isHighlighter = true;
		}

		public void setColor(object sender, RoutedEventArgs e)
		{
			Button button = sender as Button;
			string text = (string)button.Tag;
			Color color = Colors.Red;
			if (button.Background is SolidColorBrush)
			{
				color = (button.Background as SolidColorBrush).Color;
			}
			currentColor = color;
			demoStroke.Stroke = new SolidColorBrush(color);
			dispatchStrokeAttChanged();
		}

		public void dispatchStrokeAttChanged()
		{
			if (strokeAtt != null && this.strokeChange != null)
			{
				strokeAtt.IsHighlighter = isHighlighter;
				strokeAtt.Color = currentColor;
				this.strokeChange(strokeAtt);
			}
		}

		private void strokeHeight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (strokeAtt != null)
			{
				DrawingAttributes drawingAttributes = strokeAtt;
				DrawingAttributes drawingAttributes2 = strokeAtt;
				double num = demoStroke.StrokeThickness = strokeWidthSlider.Value;
				double num4 = drawingAttributes.Width = (drawingAttributes2.Height = (currentRadiusWidth = num));
				dispatchStrokeAttChanged();
			}
		}

		private void transparentButton_Click(object sender, RoutedEventArgs e)
		{
			isHighlighter = true;
			dispatchStrokeAttChanged();
			nonTransparentButton.Opacity = 0.5;
			transparentButton.Opacity = 1.0;
		}

		private void curveButtonClick(object sender, RoutedEventArgs e)
		{
			this.strokeCurve();
			curveButton.Opacity = 1.0;
			straightPanelButton.Opacity = 0.5;
			Image image = new Image();
			image.Source = ((Image)curveButton.Content).Source;
			Image content = image;
			penButton.Content = content;
			penTypePopup.IsOpen = false;
			this.showPenToolPanel(false);
		}

		private void LineButtonClick(object sender, RoutedEventArgs e)
		{
			this.strokeLine();
			curveButton.Opacity = 0.5;
			straightPanelButton.Opacity = 1.0;
			Image image = new Image();
			image.Source = ((Image)straightPanelButton.Content).Source;
			Image content = image;
			penButton.Content = content;
			penTypePopup.IsOpen = false;
			this.showPenToolPanel(false);
		}

		private void nonTransparentButton_Click(object sender, RoutedEventArgs e)
		{
			isHighlighter = false;
			dispatchStrokeAttChanged();
			nonTransparentButton.Opacity = 1.0;
			transparentButton.Opacity = 0.5;
		}

		private void undoClickButton_Click(object sender, RoutedEventArgs e)
		{
			this.strokeUndo();
		}

		private void deleteAllClickButton_Click(object sender, RoutedEventArgs e)
		{
			this.strokeDelAll();
		}

		private void redoClickButton_Click(object sender, RoutedEventArgs e)
		{
			this.strokeRedo();
		}

		private void delClickButton_Click(object sender, RoutedEventArgs e)
		{
			this.strokeDel();
		}

		private void penButtonClick(object sender, RoutedEventArgs e)
		{
			penButton.Opacity = 1.0;
			colorPanelButton.Opacity = 0.5;
			eraserButton.Opacity = 0.5;
			deleteAllButton.Opacity = 0.5;
			redoButton.Opacity = 0.5;
			undoButton.Opacity = 0.5;
			if (!penTypePopup.IsOpen)
			{
				penTypePopup.IsOpen = true;
				this.showPenToolPanel(true);
				colorPopup.IsOpen = false;
				return;
			}
			if (straightPanelButton.Opacity == 1.0)
			{
				this.strokeLine();
			}
			else
			{
				this.strokeCurve();
			}
			penTypePopup.IsOpen = false;
			this.showPenToolPanel(false);
		}

		private void eraserButtonClick(object sender, RoutedEventArgs e)
		{
			penTypePopup.IsOpen = false;
			colorPopup.IsOpen = false;
			penButton.Opacity = 0.5;
			colorPanelButton.Opacity = 0.5;
			eraserButton.Opacity = 1.0;
			deleteAllButton.Opacity = 0.5;
			redoButton.Opacity = 0.5;
			undoButton.Opacity = 0.5;
			this.strokeErase();
			this.showPenToolPanel(false);
		}

		private void deleteAllButtonClick(object sender, RoutedEventArgs e)
		{
			penTypePopup.IsOpen = false;
			colorPopup.IsOpen = false;
			penButton.Opacity = 0.5;
			colorPanelButton.Opacity = 0.5;
			eraserButton.Opacity = 0.5;
			deleteAllButton.Opacity = 1.0;
			redoButton.Opacity = 0.5;
			undoButton.Opacity = 0.5;
			this.showPenToolPanel(true);
			if (MessageBox.Show(langMng.getLangString("sureDelAllStrokes"), langMng.getLangString("submit"), MessageBoxButton.YesNo) == MessageBoxResult.Yes)
			{
				this.strokeDelAll();
			}
			penButton.Opacity = 1.0;
			colorPanelButton.Opacity = 0.5;
			eraserButton.Opacity = 0.5;
			deleteAllButton.Opacity = 0.5;
			redoButton.Opacity = 0.5;
			undoButton.Opacity = 0.5;
			this.showPenToolPanel(false);
			if (straightPanelButton.Opacity == 1.0)
			{
				this.strokeLine();
			}
			else
			{
				this.strokeCurve();
			}
		}

		private void redoButtonClick(object sender, RoutedEventArgs e)
		{
			penButton.Opacity = 0.5;
			colorPanelButton.Opacity = 0.5;
			eraserButton.Opacity = 0.5;
			deleteAllButton.Opacity = 0.5;
			redoButton.Opacity = 1.0;
			undoButton.Opacity = 0.5;
		}

		private void undoButtonClick(object sender, RoutedEventArgs e)
		{
			penButton.Opacity = 0.5;
			colorPanelButton.Opacity = 0.5;
			eraserButton.Opacity = 0.5;
			deleteAllButton.Opacity = 0.5;
			redoButton.Opacity = 0.5;
			undoButton.Opacity = 1.0;
		}

		public void determineDrawAtt(DrawingAttributes d, bool isStrokeLine)
		{
			drawingAttr = d;
			double num3 = demoStroke.StrokeThickness = (strokeWidthSlider.Value = (currentRadiusWidth = drawingAttr.Width));
			demoStroke.Stroke = new SolidColorBrush(drawingAttr.Color);
			if (d.IsHighlighter)
			{
				isHighlighter = true;
				nonTransparentButton.Opacity = 0.5;
				transparentButton.Opacity = 1.0;
			}
			else
			{
				isHighlighter = false;
				nonTransparentButton.Opacity = 1.0;
				transparentButton.Opacity = 0.5;
			}
			if (isStrokeLine)
			{
				Image image = new Image();
				image.Source = ((Image)straightPanelButton.Content).Source;
				Image content = image;
				penButton.Content = content;
				curveButton.Opacity = 0.5;
				straightPanelButton.Opacity = 1.0;
			}
			else
			{
				Image image2 = new Image();
				image2.Source = ((Image)curveButton.Content).Source;
				Image content2 = image2;
				penButton.Content = content2;
				curveButton.Opacity = 1.0;
				straightPanelButton.Opacity = 0.5;
			}
		}

		private void colorPanelButtonClick(object sender, RoutedEventArgs e)
		{
			if (!colorPopup.IsOpen)
			{
				penButton.Opacity = 0.5;
				colorPanelButton.Opacity = 1.0;
				eraserButton.Opacity = 0.5;
				deleteAllButton.Opacity = 0.5;
				redoButton.Opacity = 0.5;
				undoButton.Opacity = 0.5;
				penTypePopup.IsOpen = false;
				colorPopup.IsOpen = true;
				this.showPenToolPanel(true);
			}
			else
			{
				penButton.Opacity = 1.0;
				colorPanelButton.Opacity = 0.5;
				eraserButton.Opacity = 0.5;
				deleteAllButton.Opacity = 0.5;
				redoButton.Opacity = 0.5;
				undoButton.Opacity = 0.5;
				penTypePopup.IsOpen = false;
				colorPopup.IsOpen = false;
				this.showPenToolPanel(false);
			}
		}

		public void closePopup()
		{
			penButton.Opacity = 1.0;
			colorPanelButton.Opacity = 0.5;
			eraserButton.Opacity = 0.5;
			deleteAllButton.Opacity = 0.5;
			redoButton.Opacity = 0.5;
			undoButton.Opacity = 0.5;
			penTypePopup.IsOpen = false;
			colorPopup.IsOpen = false;
			if (straightPanelButton.Opacity == 1.0)
			{
				this.strokeLine();
			}
			else
			{
				this.strokeCurve();
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/PaperLess_Emeeting_NTPC;component/stroketoolpanelhorizontal_reader.xaml", UriKind.Relative);
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
				mainPanel = (StrokeToolPanelHorizontal_Reader)target;
				break;
			case 2:
				undoButton = (Button)target;
				undoButton.Click += new RoutedEventHandler(undoButtonClick);
				break;
			case 3:
				redoButton = (Button)target;
				redoButton.Click += new RoutedEventHandler(redoButtonClick);
				break;
			case 4:
				penButton = (Button)target;
				penButton.Click += new RoutedEventHandler(penButtonClick);
				break;
			case 5:
				penTypePopup = (Popup)target;
				break;
			case 6:
				penSubPanelGrid = (Grid)target;
				break;
			case 7:
				curveButton = (Button)target;
				curveButton.Click += new RoutedEventHandler(curveButtonClick);
				break;
			case 8:
				straightPanelButton = (Button)target;
				straightPanelButton.Click += new RoutedEventHandler(LineButtonClick);
				break;
			case 9:
				colorPanelButton = (Button)target;
				colorPanelButton.Click += new RoutedEventHandler(colorPanelButtonClick);
				break;
			case 10:
				colorPopup = (Popup)target;
				break;
			case 11:
				penToolPanelGrid = (Grid)target;
				break;
			case 12:
				transparentButton = (Button)target;
				transparentButton.Click += new RoutedEventHandler(transparentButton_Click);
				break;
			case 13:
				nonTransparentButton = (Button)target;
				nonTransparentButton.Click += new RoutedEventHandler(nonTransparentButton_Click);
				break;
			case 14:
				demoStroke = (Path)target;
				break;
			case 15:
				strokeWidthSlider = (Slider)target;
				strokeWidthSlider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(strokeHeight_ValueChanged);
				break;
			case 16:
				colorPanel = (Grid)target;
				break;
			case 17:
				((Button)target).Click += new RoutedEventHandler(setColor);
				break;
			case 18:
				((Button)target).Click += new RoutedEventHandler(setColor);
				break;
			case 19:
				((Button)target).Click += new RoutedEventHandler(setColor);
				break;
			case 20:
				((Button)target).Click += new RoutedEventHandler(setColor);
				break;
			case 21:
				((Button)target).Click += new RoutedEventHandler(setColor);
				break;
			case 22:
				((Button)target).Click += new RoutedEventHandler(setColor);
				break;
			case 23:
				((Button)target).Click += new RoutedEventHandler(setColor);
				break;
			case 24:
				((Button)target).Click += new RoutedEventHandler(setColor);
				break;
			case 25:
				((Button)target).Click += new RoutedEventHandler(setColor);
				break;
			case 26:
				((Button)target).Click += new RoutedEventHandler(setColor);
				break;
			case 27:
				((Button)target).Click += new RoutedEventHandler(setColor);
				break;
			case 28:
				((Button)target).Click += new RoutedEventHandler(setColor);
				break;
			case 29:
				((Button)target).Click += new RoutedEventHandler(setColor);
				break;
			case 30:
				((Button)target).Click += new RoutedEventHandler(setColor);
				break;
			case 31:
				((Button)target).Click += new RoutedEventHandler(setColor);
				break;
			case 32:
				((Button)target).Click += new RoutedEventHandler(setColor);
				break;
			case 33:
				((Button)target).Click += new RoutedEventHandler(setColor);
				break;
			case 34:
				((Button)target).Click += new RoutedEventHandler(setColor);
				break;
			case 35:
				((Button)target).Click += new RoutedEventHandler(setColor);
				break;
			case 36:
				((Button)target).Click += new RoutedEventHandler(setColor);
				break;
			case 37:
				eraserButton = (Button)target;
				eraserButton.Click += new RoutedEventHandler(eraserButtonClick);
				break;
			case 38:
				deleteAllButton = (Button)target;
				deleteAllButton.Click += new RoutedEventHandler(deleteAllButtonClick);
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
}
