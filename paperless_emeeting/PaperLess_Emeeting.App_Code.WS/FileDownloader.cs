using PaperLess_Emeeting.App_Code.DownloadItem;
using PaperLess_Emeeting.App_Code.Tools;
using PaperLess_Emeeting.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace PaperLess_Emeeting.App_Code.WS
{
	public class FileDownloader
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClasse
		{
			public string fileItem_ID;

			public bool _003CPause_003Eb__c(File_DownloadItemViewModel x)
			{
				return x.ID.Equals(fileItem_ID);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass14
		{
			public string MeetingDataDownloadFileFile_ID;

			public bool _003CGetInList_003Eb__12(File_DownloadItemViewModel x)
			{
				return x.ID.Equals(MeetingDataDownloadFileFile_ID);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass1b
		{
			public File_DownloadItemViewModel item;

			public bool _003CGetNotInList_003Eb__18(File_DownloadItemViewModel x)
			{
				return x.ID.Equals(item.ID);
			}
		}

		private static readonly object thisLock = new object();

		private int buffer = 1024000;

		private List<File_DownloadItemViewModel> list = new List<File_DownloadItemViewModel>();

		[CompilerGenerated]
		private File_DownloadItemViewModel _003CNowFileItem_003Ek__BackingField;

		[CompilerGenerated]
		private DownloaderType _003CdownloaderType_003Ek__BackingField;

		public File_DownloadItemViewModel NowFileItem
		{
			[CompilerGenerated]
			get
			{
				return _003CNowFileItem_003Ek__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				_003CNowFileItem_003Ek__BackingField = value;
			}
		}

		public DownloaderType downloaderType
		{
			[CompilerGenerated]
			get
			{
				return _003CdownloaderType_003Ek__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				_003CdownloaderType_003Ek__BackingField = value;
			}
		}

		public event MeetingDataCT_DownloadFileStart_Function MeetingDataCT_DownloadFileStart_Event;

		public event MeetingDataCT_DownloadProgressChanged_Function MeetingDataCT_DownloadProgressChanged_Event;

		public event MeetingDataCT_DownloadFileCompleted_Function MeetingDataCT_DownloadFileCompleted_Event;

		public event MeetingDataCT_UnZip_Function MeetingDataCT_UnZip_Event;

		public event MeetingDataCT_UnZipError_Function MeetingDataCT_UnZipError_Event;

		public event MeetingDataCT_DownloadError_Function MeetingDataCT_DownloadError_Event;

		public event MeetingRoom_DownloadFileStart_Function MeetingRoom_DownloadFileStart_Event;

		public event MeetingRoom_DownloadProgressChanged_Function MeetingRoom_DownloadProgressChanged_Event;

		public event MeetingRoom_DownloadFileToErrorCompleted_Function MeetingRoom_DownloadFileToErrorCompleted_Event;

		public event Home_UnZipError_Function Home_UnZipError_Event;

		public FileDownloader()
		{
			downloaderType = DownloaderType.沒有任何檔案下載中;
			NowFileItem = null;
			int downloadBuffer_KB = Settings.Default.DownloadBuffer_KB;
			buffer = 1024 * downloadBuffer_KB;
		}

		public void ClearHomeEvent()
		{
			this.Home_UnZipError_Event = null;
		}

		public void ClearMeetingDataCTEvent()
		{
			this.MeetingDataCT_DownloadFileStart_Event = null;
			this.MeetingDataCT_DownloadProgressChanged_Event = null;
			this.MeetingDataCT_DownloadFileCompleted_Event = null;
			this.MeetingDataCT_UnZip_Event = null;
			this.MeetingDataCT_UnZipError_Event = null;
		}

		public void ClearMeetingRoomEvent()
		{
			this.MeetingRoom_DownloadFileStart_Event = null;
			this.MeetingRoom_DownloadProgressChanged_Event = null;
			this.MeetingRoom_DownloadFileToErrorCompleted_Event = null;
		}

		public bool HasMeetingRoom_DownloadFileStart_Event()
		{
			if (this.MeetingRoom_DownloadFileStart_Event != null)
			{
				return true;
			}
			return false;
		}

		public bool HasMeetingRoom_DownloadProgressChanged_Event()
		{
			if (this.MeetingRoom_DownloadProgressChanged_Event != null)
			{
				return true;
			}
			return false;
		}

		public bool HasMeetingRoom_DownloadFileToErrorCompleted_Event()
		{
			if (this.MeetingRoom_DownloadFileToErrorCompleted_Event != null)
			{
				return true;
			}
			return false;
		}

		public void ClearAllEvent()
		{
			this.Home_UnZipError_Event = null;
			this.MeetingDataCT_DownloadFileStart_Event = null;
			this.MeetingDataCT_DownloadProgressChanged_Event = null;
			this.MeetingDataCT_DownloadFileCompleted_Event = null;
			this.MeetingDataCT_UnZip_Event = null;
			this.MeetingDataCT_UnZipError_Event = null;
			this.MeetingRoom_DownloadFileStart_Event = null;
			this.MeetingRoom_DownloadProgressChanged_Event = null;
			this.MeetingRoom_DownloadFileToErrorCompleted_Event = null;
		}

		public void Start()
		{
			downloaderType = DownloaderType.正在下載中;
			try
			{
				HttpWebRequest httpWebRequest;
				if (list.Count > 0)
				{
					lock (thisLock)
					{
						if (list.Count > 0)
						{
							NowFileItem = list[0];
							list.Remove(NowFileItem);
						}
					}
					if (NowFileItem != null)
					{
						DownloadFileStart(NowFileItem);
						httpWebRequest = (HttpWebRequest)WebRequest.Create(NowFileItem.Url);
						httpWebRequest.CachePolicy = new RequestCachePolicy(RequestCacheLevel.Revalidate);
						httpWebRequest.Proxy = null;
						httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip;
						if (NowFileItem.DownloadBytes <= 0)
						{
							goto IL_013a;
						}
						if (!File.Exists(NowFileItem.StorageFilePath))
						{
							httpWebRequest.AddRange(NowFileItem.DownloadBytes);
							goto IL_013a;
						}
						NowFileItem.DownloadBytes = new FileInfo(NowFileItem.StorageFilePath).Length;
						if (NowFileItem.DownloadBytes < NowFileItem.TotalBytes)
						{
							httpWebRequest.AddRange(NowFileItem.DownloadBytes);
							goto IL_013a;
						}
						downloaderType = DownloaderType.檔案下載完成;
					}
					else
					{
						downloaderType = DownloaderType.沒有任何檔案下載中;
					}
				}
				else
				{
					downloaderType = DownloaderType.沒有任何檔案下載中;
				}
				goto end_IL_0007;
				IL_013a:
				WebResponse response = httpWebRequest.GetResponse();
				if (NowFileItem.TotalBytes == 0)
				{
					NowFileItem.TotalBytes = response.ContentLength;
				}
				Directory.CreateDirectory(NowFileItem.StorageFileFolder);
				using (FileStream fileStream = new FileStream(NowFileItem.StorageFilePath, FileMode.OpenOrCreate))
				{
					try
					{
						fileStream.Seek(0L, SeekOrigin.End);
						using (Stream stream = response.GetResponseStream())
						{
							while (NowFileItem.DownloadBytes < NowFileItem.TotalBytes)
							{
								if (downloaderType == DownloaderType.暫停 || downloaderType == DownloaderType.停止)
								{
									UpdateToDB(NowFileItem);
									break;
								}
								byte[] array = new byte[buffer];
								int num = stream.Read(array, 0, array.Length);
								if (num > 0)
								{
									fileStream.Write(array, 0, num);
									fileStream.Flush();
									NowFileItem.DownloadBytes += num;
								}
								if (NowFileItem.DownloadBytes >= NowFileItem.TotalBytes)
								{
									downloaderType = DownloaderType.檔案下載完成;
									break;
								}
								if (downloaderType == DownloaderType.暫停 || downloaderType == DownloaderType.停止)
								{
									UpdateToDB(NowFileItem);
									break;
								}
								double percent = GetPercent(NowFileItem.DownloadBytes, NowFileItem.TotalBytes);
								if (this.MeetingRoom_DownloadFileStart_Event != null)
								{
									this.MeetingRoom_DownloadFileStart_Event(NowFileItem);
								}
								if (percent - NowFileItem.LastPercentage > (double)Settings.Default.Downloader_InvokePercent)
								{
									NowFileItem.LastPercentage = percent;
									DownloadProgressChanged(NowFileItem);
									UpdateToDB(NowFileItem);
								}
							}
							if (NowFileItem.DownloadBytes >= NowFileItem.TotalBytes)
							{
								DownloadProgressChanged(NowFileItem);
								downloaderType = DownloaderType.檔案下載完成;
							}
						}
					}
					catch (Exception ex)
					{
						downloaderType = DownloaderType.下載出錯;
						DownloadError(NowFileItem);
						LogTool.Debug(ex);
					}
				}
				end_IL_0007:;
			}
			catch (Exception ex2)
			{
				downloaderType = DownloaderType.下載出錯;
				DownloadError(NowFileItem);
				LogTool.Debug(ex2);
			}
			switch (downloaderType)
			{
			case DownloaderType.沒有任何檔案下載中:
			case (DownloaderType)1:
			case DownloaderType.正在下載中:
				break;
			case DownloaderType.停止:
				StartNextFileItemDownload(NowFileItem);
				break;
			case DownloaderType.下載出錯:
				downloaderType = DownloaderType.沒有任何檔案下載中;
				StartNextFileItemDownload(NowFileItem);
				break;
			case DownloaderType.暫停:
				StartNextFileItemDownload(NowFileItem);
				break;
			case DownloaderType.檔案下載完成:
				DownloadFileCompleted(NowFileItem);
				StartNextFileItemDownload(NowFileItem);
				break;
			}
		}

		private void DownloadError(File_DownloadItemViewModel fileItem)
		{
			if (fileItem != null)
			{
				DeleteFiles(fileItem);
				ResetFileItemDB(fileItem);
				if (this.MeetingDataCT_DownloadError_Event != null)
				{
					this.MeetingDataCT_DownloadError_Event(fileItem);
				}
				if (this.Home_UnZipError_Event != null)
				{
					this.Home_UnZipError_Event(string.Format("檔名: {0}，{1}", (NowFileItem.FileName == null) ? "" : NowFileItem.FileName, Enum.GetName(typeof(DownloaderType), DownloaderType.下載出錯)));
					Thread.Sleep(1100);
				}
				bool flag = false;
				lock (thisLock)
				{
					if (list.Count == 0)
					{
						flag = true;
					}
				}
				if (flag && this.MeetingRoom_DownloadFileToErrorCompleted_Event != null)
				{
					this.MeetingRoom_DownloadFileToErrorCompleted_Event(fileItem);
				}
			}
		}

		private void UpdateToDB(File_DownloadItemViewModel fileItem, bool IsFinished = false)
		{
			if (fileItem != null)
			{
				string text = "";
				int num = 0;
				try
				{
					if (IsFinished)
					{
						text = "update FileRow set Url=@1,StorageFileName=@2\r\n                           , DownloadBytes=@3,TotalBytes=@4 ,FileVersion=@5,FinishedFileVersion=@6 where ID=@7 and UserID=@8 and MeetingID=@9";
						fileItem.CanUpdate = false;
						num = MSCE.ExecuteNonQuery(text, fileItem.Url, fileItem.StorageFileName, fileItem.DownloadBytes.ToString(), fileItem.TotalBytes.ToString(), fileItem.FileVersion.ToString(), fileItem.FileVersion.ToString(), fileItem.ID, fileItem.UserID, fileItem.MeetingID);
					}
					else
					{
						text = "update FileRow set Url=@1,StorageFileName=@2\r\n                           , DownloadBytes=@3,TotalBytes=@4 where ID=@6 and UserID=@7 and MeetingID=@8";
						num = MSCE.ExecuteNonQuery(text, fileItem.Url, fileItem.StorageFileName, fileItem.DownloadBytes.ToString(), fileItem.TotalBytes.ToString(), fileItem.ID, fileItem.UserID, fileItem.MeetingID);
					}
				}
				catch (Exception ex)
				{
					LogTool.Debug(ex);
				}
				if (num < 1)
				{
					LogTool.Debug(new Exception("DB失敗:" + text));
				}
			}
		}

		private void StartNextFileItemDownload(File_DownloadItemViewModel fileItem)
		{
			Action action = null;
			if (downloaderType == DownloaderType.停止)
			{
				lock (thisLock)
				{
					list.Clear();
				}
			}
			NowFileItem = null;
			downloaderType = DownloaderType.沒有任何檔案下載中;
			if (downloaderType == DownloaderType.沒有任何檔案下載中)
			{
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003CStartNextFileItemDownload_003Eb__3);
				}
				factory.StartNew(action, TaskCreationOptions.LongRunning);
			}
		}

		public void AddItem(File_DownloadItemViewModel fileItem)
		{
			Action action = null;
			lock (this)
			{
				if (fileItem == null)
				{
					return;
				}
				list.Add(fileItem);
			}
			if (downloaderType == DownloaderType.沒有任何檔案下載中 || downloaderType == DownloaderType.停止)
			{
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003CAddItem_003Eb__6);
				}
				factory.StartNew(action);
			}
		}

		public void AddItem(List<File_DownloadItemViewModel> fileItemList)
		{
			Action action = null;
			lock (this)
			{
				list.AddRange(fileItemList);
			}
			if (downloaderType == DownloaderType.沒有任何檔案下載中 || downloaderType == DownloaderType.停止)
			{
				TaskFactory factory = Task.Factory;
				if (action == null)
				{
					action = new Action(_003CAddItem_003Eb__9);
				}
				factory.StartNew(action, TaskCreationOptions.LongRunning);
			}
		}

		public void Pause(string fileItem_ID)
		{
			Predicate<File_DownloadItemViewModel> predicate = null;
			_003C_003Ec__DisplayClasse _003C_003Ec__DisplayClasse = new _003C_003Ec__DisplayClasse();
			_003C_003Ec__DisplayClasse.fileItem_ID = fileItem_ID;
			lock (this)
			{
				List<File_DownloadItemViewModel> obj = list;
				if (predicate == null)
				{
					predicate = new Predicate<File_DownloadItemViewModel>(_003C_003Ec__DisplayClasse._003CPause_003Eb__c);
				}
				obj.RemoveAll(predicate);
				if (NowFileItem != null && NowFileItem.ID.Equals(_003C_003Ec__DisplayClasse.fileItem_ID))
				{
					downloaderType = DownloaderType.暫停;
				}
			}
		}

		public void Stop()
		{
			lock (this)
			{
				list.Clear();
			}
			if (downloaderType == DownloaderType.正在下載中)
			{
				downloaderType = DownloaderType.停止;
			}
		}

		public File_DownloadItemViewModel GetInList(string MeetingDataDownloadFileFile_ID)
		{
			Func<File_DownloadItemViewModel, bool> func = null;
			_003C_003Ec__DisplayClass14 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass14();
			_003C_003Ec__DisplayClass.MeetingDataDownloadFileFile_ID = MeetingDataDownloadFileFile_ID;
			File_DownloadItemViewModel file_DownloadItemViewModel = new File_DownloadItemViewModel();
			lock (this)
			{
				if (NowFileItem != null && NowFileItem.ID.Equals(_003C_003Ec__DisplayClass.MeetingDataDownloadFileFile_ID))
				{
					return NowFileItem;
				}
				List<File_DownloadItemViewModel> source = list;
				if (func == null)
				{
					func = new Func<File_DownloadItemViewModel, bool>(_003C_003Ec__DisplayClass._003CGetInList_003Eb__12);
				}
				return Enumerable.FirstOrDefault(Enumerable.Where(source, func));
			}
		}

		public List<File_DownloadItemViewModel> GetNotInList(List<File_DownloadItemViewModel> fileItemList)
		{
			Predicate<File_DownloadItemViewModel> predicate = null;
			lock (this)
			{
				if (NowFileItem != null)
				{
					if (predicate == null)
					{
						predicate = new Predicate<File_DownloadItemViewModel>(_003CGetNotInList_003Eb__17);
					}
					fileItemList.RemoveAll(predicate);
				}
				using (List<File_DownloadItemViewModel>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Predicate<File_DownloadItemViewModel> predicate2 = null;
						_003C_003Ec__DisplayClass1b _003C_003Ec__DisplayClass1b = new _003C_003Ec__DisplayClass1b();
						_003C_003Ec__DisplayClass1b.item = enumerator.Current;
						if (predicate2 == null)
						{
							predicate2 = new Predicate<File_DownloadItemViewModel>(_003C_003Ec__DisplayClass1b._003CGetNotInList_003Eb__18);
						}
						fileItemList.RemoveAll(predicate2);
					}
				}
				return fileItemList;
			}
		}

		private double GetPercent(double downloadBytes, double totalBytes)
		{
			double result = 0.0;
			if (totalBytes > 0.0)
			{
				result = downloadBytes * 100.0 / totalBytes;
			}
			return result;
		}

		private void DownloadFileStart(File_DownloadItemViewModel fileItem)
		{
			if (fileItem != null)
			{
				if (this.MeetingDataCT_DownloadFileStart_Event != null)
				{
					this.MeetingDataCT_DownloadFileStart_Event(fileItem);
				}
				if (this.MeetingRoom_DownloadFileStart_Event != null)
				{
					this.MeetingRoom_DownloadFileStart_Event(NowFileItem);
				}
			}
		}

		private void DownloadProgressChanged(File_DownloadItemViewModel fileItem)
		{
			if (fileItem != null)
			{
				if (this.MeetingDataCT_DownloadProgressChanged_Event != null)
				{
					this.MeetingDataCT_DownloadProgressChanged_Event(fileItem);
				}
				if (this.MeetingRoom_DownloadProgressChanged_Event != null)
				{
					this.MeetingRoom_DownloadProgressChanged_Event(fileItem);
				}
			}
		}

		private void DownloadFileCompleted(File_DownloadItemViewModel fileItem)
		{
			if (fileItem != null)
			{
				switch (fileItem.FileCate)
				{
				case MeetingFileCate.電子書:
				case MeetingFileCate.Html5投影片:
					UnzipTrigger(fileItem);
					break;
				case MeetingFileCate.影片檔:
					try
					{
						if (File.Exists(fileItem.StorageFilePath))
						{
							Directory.CreateDirectory(fileItem.UnZipFilePath);
							File.Copy(fileItem.StorageFilePath, fileItem.UnZipFilePath + "\\" + fileItem.StorageFileName, true);
						}
					}
					catch (Exception ex)
					{
						LogTool.Debug(ex);
					}
					UpdateToDB(fileItem, true);
					break;
				}
				if (this.MeetingDataCT_DownloadFileCompleted_Event != null)
				{
					this.MeetingDataCT_DownloadFileCompleted_Event(fileItem);
				}
				if (this.MeetingRoom_DownloadProgressChanged_Event != null)
				{
					this.MeetingRoom_DownloadProgressChanged_Event(fileItem);
				}
			}
		}

		private void UnzipTrigger(File_DownloadItemViewModel fileItem)
		{
			if (fileItem == null)
			{
				return;
			}
			bool flag = false;
			if (this.MeetingDataCT_UnZip_Event != null)
			{
				this.MeetingDataCT_UnZip_Event(fileItem);
			}
			try
			{
				UnZipTool unZipTool = new UnZipTool();
				flag = unZipTool.UnZip(fileItem.StorageFilePath, fileItem.UnZipFilePath, "", true);
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
			if (flag)
			{
				UpdateToDB(fileItem, true);
				fileItem.FileType = MeetingFileType.已下載完成;
				return;
			}
			ResetFileItemDB(fileItem);
			fileItem.FileType = MeetingFileType.解壓縮失敗;
			DeleteFiles(fileItem);
			if (this.MeetingDataCT_UnZipError_Event != null)
			{
				this.MeetingDataCT_UnZipError_Event(fileItem);
			}
			if (this.Home_UnZipError_Event != null)
			{
				this.Home_UnZipError_Event(string.Format("檔名: {0}，{1}", fileItem.FileName, Enum.GetName(typeof(MeetingFileType), MeetingFileType.解壓縮失敗)));
			}
		}

		private void DeleteFiles(File_DownloadItemViewModel fileItem)
		{
			if (File.Exists(fileItem.StorageFilePath))
			{
				File.Delete(fileItem.StorageFilePath);
			}
			DirectoryTool.FullDeleteDirectories(fileItem.UnZipFilePath);
		}

		private void ResetFileItemDB(File_DownloadItemViewModel fileItem)
		{
			if (fileItem != null)
			{
				fileItem.DownloadBytes = 0L;
				fileItem.TotalBytes = 0L;
				fileItem.LastPercentage = 0.0;
				if (File.Exists(fileItem.StorageFilePath))
				{
					File.Delete(fileItem.StorageFilePath);
				}
				string text = "update  FileRow set Url=@1,StorageFileName=@2\r\n                       , DownloadBytes=@3,TotalBytes=@4 where ID=@6 and UserID=@7 and MeetingID=@8";
				int num = MSCE.ExecuteNonQuery(text, fileItem.Url, fileItem.StorageFileName, fileItem.DownloadBytes.ToString(), fileItem.TotalBytes.ToString(), fileItem.ID, fileItem.UserID, fileItem.MeetingID);
				if (num < 1)
				{
					LogTool.Debug(new Exception("DB失敗:" + text));
				}
			}
		}

		[CompilerGenerated]
		private void _003CStartNextFileItemDownload_003Eb__3()
		{
			Start();
		}

		[CompilerGenerated]
		private void _003CAddItem_003Eb__6()
		{
			Start();
		}

		[CompilerGenerated]
		private void _003CAddItem_003Eb__9()
		{
			Start();
		}

		[CompilerGenerated]
		private bool _003CGetNotInList_003Eb__17(File_DownloadItemViewModel x)
		{
			return x.ID.Equals(NowFileItem);
		}
	}
}
