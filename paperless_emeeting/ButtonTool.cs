using PaperLess_Emeeting.Properties;
using System;
using System.Windows.Media.Imaging;

public class ButtonTool
{
	public static BitmapImage GetButtonImage(string ButtonID, bool IsActived = false)
	{
		string text = "";
		switch (ButtonID)
		{
		case "BtnHome":
			text = "images/tabBarIcon_home@2x.png";
			break;
		case "BtnSeries":
			text = "images/tabBarIcon_meetingSet@2x.png";
			break;
		case "BtnLaw":
			text = Settings.Default.LawButton_Image;
			break;
		case "BtnLogout":
			text = "images/tabBarIcon_logout@2x.png";
			break;
		case "BtnBroadcast":
			text = "images/tabBarIcon_brocast@2x.png";
			break;
		case "BtnSignin":
			text = "images/tabBarIcon_signature@2x.png";
			break;
		case "BtnMeeting":
			text = "images/tabBarIcon_meetingRecord@2x.png";
			break;
		case "BtnIndividualSign":
			text = "images/tabBarIcon_individualSign@2x.png";
			break;
		case "BtnQuit":
			text = "images/tabBarIcon_exit@2x.png";
			break;
		case "BtnSigninList":
			text = "images/tabBarIcon_signCheck@2x.png";
			break;
		case "BtnVote":
			text = "images/tabBarIcon_vote@2x.png";
			break;
		case "BtnSync":
			text = "images/status-onair-off@2x.png";
			break;
		case "BtnAttendance":
			text = "images/tabBarIcon_signCheck@2x.png";
			break;
		case "BtnExportPDF":
			text = "images/tabBarIcon_pdf@2x.png";
			break;
		}
		if (IsActived)
		{
			text = ((!ButtonID.Equals("BtnSync")) ? text.Replace("@2x.png", "_actived@2x.png") : "images/status-onair-audience@2x.png");
		}
		return new BitmapImage(new Uri(text, UriKind.Relative));
	}

	public static BitmapImage GetSyncButtonImage(bool IsInSync, bool IsSyncOwner)
	{
		string text = "";
		text = ((!IsInSync) ? "images/status-onair-off@2x.png" : ((!IsSyncOwner) ? "images/status-onair-audience@2x.png" : "images/status-onair-chairman@2x.png"));
		return new BitmapImage(new Uri(text, UriKind.Relative));
	}
}
