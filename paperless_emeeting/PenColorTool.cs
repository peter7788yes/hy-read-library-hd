using System;
using System.Windows.Media.Imaging;

public class PenColorTool
{
	public static BitmapImage GetButtonImage(PenColorType PTC)
	{
		string uriString = "";
		switch (PTC)
		{
		case PenColorType.紅Thin:
			uriString = "images/markersNow-red-thin@2x.png";
			break;
		case PenColorType.紅Medium:
			uriString = "images/markersNow-red-medium@2x.png";
			break;
		case PenColorType.紅Bold:
			uriString = "images/markersNow-red-bold@2x.png";
			break;
		case PenColorType.紅透Thin:
			uriString = "images/markersNow-red50-thin@2x.png";
			break;
		case PenColorType.紅透Medium:
			uriString = "images/markersNow-red50-medium@2x.png";
			break;
		case PenColorType.紅透Bold:
			uriString = "images/markersNow-red50-bold@2x.png";
			break;
		case PenColorType.黃Thin:
			uriString = "images/markersNow-yellow-thin@2x.png";
			break;
		case PenColorType.黃Medium:
			uriString = "images/markersNow-yellow-medium@2x.png";
			break;
		case PenColorType.黃Bold:
			uriString = "images/markersNow-yellow-bold@2x.png";
			break;
		case PenColorType.黃透Thin:
			uriString = "images/markersNow-yellow50-thin@2x.png";
			break;
		case PenColorType.黃透Medium:
			uriString = "images/markersNow-yellow50-medium@2x.png";
			break;
		case PenColorType.黃透Bold:
			uriString = "images/markersNow-yellow50-bold@2x.png";
			break;
		case PenColorType.綠Thin:
			uriString = "images/markersNow-green-thin@2x.png";
			break;
		case PenColorType.綠Medium:
			uriString = "images/markersNow-green-medium@2x.png";
			break;
		case PenColorType.綠Bold:
			uriString = "images/markersNow-green-bold@2x.png";
			break;
		case PenColorType.綠透Thin:
			uriString = "images/markersNow-green50-thin@2x.png";
			break;
		case PenColorType.綠透Medium:
			uriString = "images/markersNow-green50-medium@2x.png";
			break;
		case PenColorType.綠透Bold:
			uriString = "images/markersNow-green50-bold@2x.png";
			break;
		case PenColorType.藍Thin:
			uriString = "images/markersNow-blue-thin@2x.png";
			break;
		case PenColorType.藍Medium:
			uriString = "images/markersNow-blue-medium@2x.png";
			break;
		case PenColorType.藍Bold:
			uriString = "images/markersNow-blue-bold@2x.png";
			break;
		case PenColorType.藍透Thin:
			uriString = "images/markersNow-blue50-thin@2x.png";
			break;
		case PenColorType.藍透Medium:
			uriString = "images/markersNow-blue50-medium@2x.png";
			break;
		case PenColorType.藍透Bold:
			uriString = "images/markersNow-blue50-bold@2x.png";
			break;
		case PenColorType.紫Thin:
			uriString = "images/markersNow-purple-thin@2x.png";
			break;
		case PenColorType.紫Medium:
			uriString = "images/markersNow-purple-medium@2x.png";
			break;
		case PenColorType.紫Bold:
			uriString = "images/markersNow-purple-bold@2x.png";
			break;
		case PenColorType.紫透Thin:
			uriString = "images/markersNow-purple50-thin@2x.png";
			break;
		case PenColorType.紫透Medium:
			uriString = "images/markersNow-purple50-medium@2x.png";
			break;
		case PenColorType.紫透Bold:
			uriString = "images/markersNow-purple50-bold@2x.png";
			break;
		}
		return new BitmapImage(new Uri(uriString, UriKind.Relative));
	}
}
