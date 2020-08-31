using PaperLess_Emeeting.App_Code.ClickOnce;
using PaperLess_Emeeting.App_Code.DownloadItem;
using PaperLess_Emeeting.Properties;
using System;
using System.Data;
using System.IO;

public class FileItemTool
{
	public static File_DownloadItemViewModel Gen(MeetingDataDownloadFileFile meetingDataDownloadFileFile, string UserID, string MeetingID)
	{
		File_DownloadItemViewModel file_DownloadItemViewModel = new File_DownloadItemViewModel();
		try
		{
			long num = 0L;
			long num2 = 0L;
			int num3 = 0;
			string text = "";
			int num4 = 0;
			DataTable dataTable = MSCE.GetDataTable("SELECT ID,Url,StorageFileName,DownloadBytes,TotalBytes,FileVersion,FinishedFileVersion,EncryptionKey FROM FileRow where ID=@1 and UserID=@2 and MeetingID=@3", meetingDataDownloadFileFile.ID, UserID, MeetingID);
			if (dataTable.Rows.Count > 0)
			{
				dataTable.Rows[0]["ID"].ToString();
				dataTable.Rows[0]["Url"].ToString();
				dataTable.Rows[0]["StorageFileName"].ToString();
				num = long.Parse(dataTable.Rows[0]["DownloadBytes"].ToString().Equals("") ? "0" : dataTable.Rows[0]["DownloadBytes"].ToString());
				num2 = long.Parse(dataTable.Rows[0]["TotalBytes"].ToString().Equals("") ? "0" : dataTable.Rows[0]["TotalBytes"].ToString());
				int.Parse((dataTable.Rows[0]["FileVersion"].ToString().Equals("") || dataTable.Rows[0]["FileVersion"].ToString().Equals("0")) ? "1" : dataTable.Rows[0]["FileVersion"].ToString());
				num3 = int.Parse(dataTable.Rows[0]["FinishedFileVersion"].ToString().Equals("") ? "0" : dataTable.Rows[0]["FileVersion"].ToString());
				dataTable.Rows[0]["EncryptionKey"].ToString();
			}
			else
			{
				text = "INSERT INTO FileRow(ID,DownloadBytes,TotalBytes,UserID,MeetingID,DisplayFileName,FileVersion,EncryptionKey) \r\n                                                    VALUES(@1,0,0,@2,@3,@4,@5,@6)";
				num4 = MSCE.ExecuteNonQuery(text, meetingDataDownloadFileFile.ID, UserID, MeetingID, meetingDataDownloadFileFile.FileName, meetingDataDownloadFileFile.version.Equals("") ? "1" : meetingDataDownloadFileFile.version, (meetingDataDownloadFileFile.EncryptionKey == null) ? "" : meetingDataDownloadFileFile.EncryptionKey);
				if (num4 < 1)
				{
					LogTool.Debug(new Exception("DB失敗: " + text));
				}
			}
			file_DownloadItemViewModel.MeetingID = MeetingID;
			file_DownloadItemViewModel.ID = meetingDataDownloadFileFile.ID;
			file_DownloadItemViewModel.UserID = UserID;
			file_DownloadItemViewModel.FileName = meetingDataDownloadFileFile.FileName;
			file_DownloadItemViewModel.Url = meetingDataDownloadFileFile.Url;
			file_DownloadItemViewModel.AgendaID = meetingDataDownloadFileFile.AgendaID;
			file_DownloadItemViewModel.EncryptionKey = ((meetingDataDownloadFileFile.EncryptionKey == null) ? "" : meetingDataDownloadFileFile.EncryptionKey);
			file_DownloadItemViewModel.StorageFileFolder = Path.Combine(ClickOnceTool.GetFilePath(), Settings.Default.File_StorageFileFolder);
			string text2 = DateTime.Now.ToFileTime().ToString();
			try
			{
				Uri uri = new Uri(file_DownloadItemViewModel.Url);
				string fileName = Path.GetFileName(uri.LocalPath);
				if (!fileName.Equals("/"))
				{
					text2 = fileName;
				}
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
			file_DownloadItemViewModel.StorageFileName = string.Format("{0}_{1}_{2}_{3}", UserID, MeetingID, meetingDataDownloadFileFile.ID, text2);
			file_DownloadItemViewModel.UnZipFileFolder = Path.Combine(ClickOnceTool.GetFilePath(), Settings.Default.File_UnZipFileFolder);
			LogTool.Debug(ClickOnceTool.GetFilePath());
			file_DownloadItemViewModel.DownloadBytes = num;
			file_DownloadItemViewModel.TotalBytes = num2;
			int result = 0;
			int.TryParse(meetingDataDownloadFileFile.version, out result);
			if (result < 1)
			{
				result = 1;
			}
			file_DownloadItemViewModel.FileVersion = result;
			if (num3 > 0 && file_DownloadItemViewModel.FileVersion > num3)
			{
				file_DownloadItemViewModel.CanUpdate = true;
			}
			if (File.Exists(file_DownloadItemViewModel.StorageFilePath))
			{
				if (num == 0)
				{
					if (File.Exists(file_DownloadItemViewModel.StorageFilePath))
					{
						File.Delete(file_DownloadItemViewModel.StorageFilePath);
					}
					file_DownloadItemViewModel.DownloadBytes = 0L;
					file_DownloadItemViewModel.TotalBytes = 0L;
					file_DownloadItemViewModel.FileType = MeetingFileType.從未下載;
				}
				else if (num < num2)
				{
					if (file_DownloadItemViewModel.CanUpdate)
					{
						file_DownloadItemViewModel.DownloadBytes = 0L;
						file_DownloadItemViewModel.TotalBytes = 0L;
						file_DownloadItemViewModel.FileType = MeetingFileType.已經下載過一次且可以更新版本的檔案_目前下載未完成;
					}
					else
					{
						file_DownloadItemViewModel.DownloadBytes = num;
						file_DownloadItemViewModel.TotalBytes = num2;
						file_DownloadItemViewModel.FileType = MeetingFileType.已下載過但是未完成的檔案;
					}
				}
				else if (file_DownloadItemViewModel.CanUpdate)
				{
					file_DownloadItemViewModel.DownloadBytes = 0L;
					file_DownloadItemViewModel.TotalBytes = 0L;
					file_DownloadItemViewModel.FileType = MeetingFileType.已經下載過一次且可以更新版本的檔案_目前下載已完成;
				}
				else
				{
					file_DownloadItemViewModel.FileType = MeetingFileType.已下載完成;
				}
			}
			else
			{
				file_DownloadItemViewModel.DownloadBytes = 0L;
				file_DownloadItemViewModel.TotalBytes = 0L;
				file_DownloadItemViewModel.FileType = MeetingFileType.從未下載;
			}
			text = "update FileRow set DownloadBytes=@1,TotalBytes=@2,UserID=@3,MeetingID=@4,FileVersion=@5 where ID=@6";
			num4 = MSCE.ExecuteNonQuery(text, file_DownloadItemViewModel.DownloadBytes.ToString(), file_DownloadItemViewModel.TotalBytes.ToString(), UserID, MeetingID, file_DownloadItemViewModel.FileVersion.ToString(), file_DownloadItemViewModel.ID);
			if (num4 >= 1)
			{
				return file_DownloadItemViewModel;
			}
			LogTool.Debug(new Exception("DB失敗: " + text));
			return file_DownloadItemViewModel;
		}
		catch (Exception ex2)
		{
			LogTool.Debug(ex2);
			return file_DownloadItemViewModel;
		}
	}
}
