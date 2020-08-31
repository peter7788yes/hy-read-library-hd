using BookManagerModule;
using DataAccessObject;
using System;
using System.Globalization;
using System.Windows.Ink;
using System.Windows.Media;

namespace PaperLess_Emeeting
{
	public class ConfigurationManager
	{
		private bool _saveLoginInfo;

		private int _saveFullTextSize;

		private string _saveFullTextColor = "FFFFFF";

		private bool _saveShowButton = true;

		private int _saveSlideShowTime = 3;

		private bool _lastReadFirst = true;

		private string _savefilterBookStr = "111111111";

		private string _saveEpubTextColor = "FFFFFF";

		private float _saveEpubTextSize = 1f;

		private int _saveEpubChineseType = 1;

		private int _saveEpubFontSize = 16;

		private int _saveShowEpubPageArrow = 1;

		private int _savePdfPageMode = 2;

		private int _saveProxyMode = 1;

		private string _saveProxyHttpPort = "";

		private string _saveLanquage = "";

		private string _strokeColor;

		private int _isStrokeLine;

		public bool isStrokeLine;

		private int _isStrokeTransparent;

		private double _strokeradiusWidth;

		private DrawingAttributes _drawingAttr;

		private BookManager bookManager;

		public string saveLanquage
		{
			get
			{
				return _saveLanquage;
			}
			set
			{
				_saveLanquage = value;
				saveConfiguration();
			}
		}

		public string saveProxyHttpPort
		{
			get
			{
				return _saveProxyHttpPort;
			}
			set
			{
				_saveProxyHttpPort = value;
				saveConfiguration();
			}
		}

		public int saveProxyMode
		{
			get
			{
				return _saveProxyMode;
			}
			set
			{
				_saveProxyMode = value;
				saveConfiguration();
			}
		}

		public int savePdfPageMode
		{
			get
			{
				return _savePdfPageMode;
			}
			set
			{
				_savePdfPageMode = value;
				saveConfiguration();
			}
		}

		public int saveShowEpubPageArrow
		{
			get
			{
				return _saveShowEpubPageArrow;
			}
			set
			{
				_saveShowEpubPageArrow = value;
				saveConfiguration();
			}
		}

		public int saveEpubFontSize
		{
			get
			{
				return _saveEpubFontSize;
			}
			set
			{
				_saveEpubFontSize = value;
				saveConfiguration();
			}
		}

		public int saveEpubChineseType
		{
			get
			{
				return _saveEpubChineseType;
			}
			set
			{
				_saveEpubChineseType = value;
				saveConfiguration();
			}
		}

		public string saveEpubTextColor
		{
			get
			{
				return _saveEpubTextColor;
			}
			set
			{
				_saveEpubTextColor = value;
				saveConfiguration();
			}
		}

		public float saveEpubTextSize
		{
			get
			{
				return _saveEpubTextSize;
			}
			set
			{
				_saveEpubTextSize = value;
				saveConfiguration();
			}
		}

		public string savefilterBookStr
		{
			get
			{
				return _savefilterBookStr;
			}
			set
			{
				_savefilterBookStr = value;
				saveConfiguration();
			}
		}

		public bool saveLoginInfo
		{
			get
			{
				return _saveLoginInfo;
			}
			set
			{
				_saveLoginInfo = value;
				saveConfiguration();
			}
		}

		public int saveFullTextSize
		{
			get
			{
				return _saveFullTextSize;
			}
			set
			{
				_saveFullTextSize = value;
				saveConfiguration();
			}
		}

		public string saveFullTextColor
		{
			get
			{
				return _saveFullTextColor;
			}
			set
			{
				_saveFullTextColor = value;
				saveConfiguration();
			}
		}

		public bool saveShowButton
		{
			get
			{
				return _saveShowButton;
			}
			set
			{
				_saveShowButton = value;
				saveConfiguration();
			}
		}

		public int saveSlideShowTime
		{
			get
			{
				return _saveSlideShowTime;
			}
			set
			{
				_saveSlideShowTime = value;
				saveConfiguration();
			}
		}

		public bool lastReadFirst
		{
			get
			{
				return _lastReadFirst;
			}
			set
			{
				_lastReadFirst = value;
				saveConfiguration();
			}
		}

		public DrawingAttributes loadStrokeSetting()
		{
			DrawingAttributes drawingAttributes = new DrawingAttributes();
			string sqlCommand = "Select * from configuration ";
			QueryResult queryResult = bookManager.sqlCommandQuery(sqlCommand);
			if (queryResult.fetchRow())
			{
				_strokeColor = queryResult.getString("strokeColor");
				_strokeradiusWidth = queryResult.getFloat("strokeRadiusWidth");
				_isStrokeLine = queryResult.getInt("strokeIsStraight");
				_isStrokeTransparent = queryResult.getInt("strokeIsTransparent");
				double num2 = drawingAttributes.Width = (drawingAttributes.Height = _strokeradiusWidth);
				if (_isStrokeTransparent == 0)
				{
					drawingAttributes.IsHighlighter = true;
				}
				if (_isStrokeTransparent == 1)
				{
					drawingAttributes.IsHighlighter = false;
				}
				if (_isStrokeLine == 0)
				{
					isStrokeLine = true;
				}
				if (_isStrokeLine == 1)
				{
					isStrokeLine = false;
				}
				drawingAttributes.Color = ConvertHexStringToColour(_strokeColor);
			}
			return drawingAttributes;
		}

		public void saveStrokeSetting(DrawingAttributes d, bool isLine)
		{
			_strokeColor = d.Color.ToString();
			_strokeradiusWidth = d.Width;
			if (d.IsHighlighter)
			{
				_isStrokeTransparent = 0;
			}
			else
			{
				_isStrokeTransparent = 1;
			}
			if (isLine)
			{
				_isStrokeLine = 0;
			}
			else
			{
				_isStrokeLine = 1;
			}
			string sqlCommand = "Update configuration Set strokeColor='" + _strokeColor + "' , strokeRadiusWidth=" + _strokeradiusWidth + ", strokeIsStraight=" + _isStrokeLine + ", strokeIsTransparent=" + _isStrokeTransparent;
			bookManager.sqlCommandNonQuery(sqlCommand);
		}

		private Color ConvertHexStringToColour(string hexString)
		{
			byte b = 0;
			byte b2 = 0;
			byte b3 = 0;
			byte b4 = 0;
			if (hexString.StartsWith("#"))
			{
				hexString = hexString.Substring(1, 8);
			}
			b = Convert.ToByte(int.Parse(hexString.Substring(0, 2), NumberStyles.AllowHexSpecifier));
			b2 = Convert.ToByte(int.Parse(hexString.Substring(2, 2), NumberStyles.AllowHexSpecifier));
			b3 = Convert.ToByte(int.Parse(hexString.Substring(4, 2), NumberStyles.AllowHexSpecifier));
			b4 = Convert.ToByte(int.Parse(hexString.Substring(6, 2), NumberStyles.AllowHexSpecifier));
			return Color.FromArgb(b, b2, b3, b4);
		}

		public ConfigurationManager(BookManager bookManager)
		{
			this.bookManager = bookManager;
			loadConfiguration();
		}

		private void saveConfiguration()
		{
			string sqlCommand = "Update configuration Set fullTextColor='" + _saveFullTextColor + "' , fullTextSize=" + _saveFullTextSize + ", slideShowTime=" + _saveSlideShowTime + ", filterBookStr='" + _savefilterBookStr + "' , epubTextColor='" + _saveEpubTextColor + "' , epubTextSize=" + _saveEpubTextSize + " , epubChineseType=" + _saveEpubChineseType + " , epubFontSize=" + _saveEpubFontSize + " , showEpubPageArrow=" + _saveShowEpubPageArrow + " , pdfPageMode=" + _savePdfPageMode + " , proxyMode=" + _saveProxyMode + " , proxyHttpPort='" + _saveProxyHttpPort + "' ,  lanquage='" + _saveLanquage + "' ";
			bookManager.sqlCommandNonQuery(sqlCommand);
		}

		private void loadConfiguration()
		{
			string sqlCommand = "Select * from configuration ";
			QueryResult queryResult = bookManager.sqlCommandQuery(sqlCommand);
			if (queryResult.fetchRow())
			{
				_saveFullTextColor = queryResult.getString("fullTextColor");
				_saveFullTextSize = queryResult.getInt("fullTextSize");
				_saveSlideShowTime = queryResult.getInt("slideShowTime");
				_savefilterBookStr = queryResult.getString("filterBookStr");
				_saveEpubTextColor = queryResult.getString("epubTextColor");
				_saveEpubTextSize = queryResult.getFloat("epubTextSize");
				_saveEpubChineseType = queryResult.getInt("epubChineseType");
				_saveEpubFontSize = queryResult.getInt("epubFontSize");
				_saveShowEpubPageArrow = queryResult.getInt("showEpubPageArrow");
				_savePdfPageMode = queryResult.getInt("pdfPageMode");
				_saveProxyMode = queryResult.getInt("proxyMode");
				_saveProxyHttpPort = queryResult.getString("proxyHttpPort");
				_saveLanquage = queryResult.getString("lanquage");
			}
		}
	}
}
