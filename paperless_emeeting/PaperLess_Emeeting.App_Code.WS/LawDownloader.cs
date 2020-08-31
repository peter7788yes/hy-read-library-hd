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
	public class LawDownloader
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClassd
		{
			public string lawItem_ID;

			public bool _003CPause_003Eb__b(Law_DownloadItemViewModel x)
			{
				return x.ID.Equals(lawItem_ID);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass13
		{
			public string lawDataLaw_ID;

			public bool _003CGetInList_003Eb__11(Law_DownloadItemViewModel x)
			{
				return x.ID.Equals(lawDataLaw_ID);
			}
		}

		private static readonly object thisLock = new object();

		private int buffer = 1048576;

		private List<Law_DownloadItemViewModel> list = new List<Law_DownloadItemViewModel>();

		[CompilerGenerated]
		private Law_DownloadItemViewModel _003CNowLawItem_003Ek__BackingField;

		[CompilerGenerated]
		private DownloaderType _003CdownloaderType_003Ek__BackingField;

		public Law_DownloadItemViewModel NowLawItem
		{
			[CompilerGenerated]
			get
			{
				return _003CNowLawItem_003Ek__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				_003CNowLawItem_003Ek__BackingField = value;
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

		public event LawListCT_DownloadFileStart_Function LawListCT_DownloadFileStart_Event;

		public event LawListCT_DownloadProgressChanged_Function LawListCT_DownloadProgressChanged_Event;

		public event LawListCT_DownloadFileCompleted_Function LawListCT_DownloadFileCompleted_Event;

		public event LawListCT_UnZip_Function LawListCT_UnZip_Event;

		public event LawListCT_UnZipError_Function LawListCT_UnZipError_Event;

		public event Home_UnZipError_Function Home_UnZipError_Event;

		public LawDownloader()
		{
			downloaderType = DownloaderType.沒有任何檔案下載中;
			NowLawItem = null;
			int downloadBuffer_KB = Settings.Default.DownloadBuffer_KB;
			buffer = 1024 * downloadBuffer_KB;
		}

		public void ClearAllEvent()
		{
			this.LawListCT_DownloadFileStart_Event = null;
			this.LawListCT_DownloadProgressChanged_Event = null;
			this.LawListCT_DownloadFileCompleted_Event = null;
			this.LawListCT_UnZip_Event = null;
			this.LawListCT_UnZipError_Event = null;
		}

		public void ClearHomeEvent()
		{
			this.Home_UnZipError_Event = null;
		}

		public void ClearLawListCTEvent()
		{
			this.LawListCT_DownloadFileStart_Event = null;
			this.LawListCT_DownloadProgressChanged_Event = null;
			this.LawListCT_DownloadFileCompleted_Event = null;
			this.LawListCT_UnZip_Event = null;
			this.LawListCT_UnZipError_Event = null;
		}

		public void Start()
		{
			downloaderType = DownloaderType.正在下載中;
			try
			{
				if (list.Count > 0)
				{
					lock (thisLock)
					{
						if (list.Count > 0)
						{
							NowLawItem = list[0];
							list.Remove(NowLawItem);
						}
					}
					if (NowLawItem != null)
					{
						try
						{
							DownloadFileStart(NowLawItem);
							HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(NowLawItem.Link);
							httpWebRequest.CachePolicy = new RequestCachePolicy(RequestCacheLevel.Revalidate);
							httpWebRequest.Proxy = null;
							httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip;
							long downloadByte = NowLawItem.DownloadBytes;
							int num2 = 0;
							WebResponse response = httpWebRequest.GetResponse();
							if (NowLawItem.TotalBytes == 0)
							{
								NowLawItem.TotalBytes = response.ContentLength;
							}
							Directory.CreateDirectory(NowLawItem.StorageFileFolder);
							using (FileStream fileStream = new FileStream(NowLawItem.StorageFilePath, FileMode.OpenOrCreate))
							{
								try
								{
									fileStream.Seek(0L, SeekOrigin.End);
									using (Stream stream = response.GetResponseStream())
									{
										while (NowLawItem.DownloadBytes < NowLawItem.TotalBytes && downloaderType != DownloaderType.暫停 && downloaderType != DownloaderType.停止)
										{
											byte[] array = new byte[buffer];
											int num = stream.Read(array, 0, array.Length);
											if (num > 0)
											{
												fileStream.Write(array, 0, num);
												fileStream.Flush();
												NowLawItem.DownloadBytes += num;
											}
											if (NowLawItem.DownloadBytes >= NowLawItem.TotalBytes)
											{
												downloaderType = DownloaderType.檔案下載完成;
												break;
											}
											if (downloaderType == DownloaderType.暫停 || downloaderType == DownloaderType.停止)
											{
												break;
											}
											double percent = GetPercent(NowLawItem.DownloadBytes, NowLawItem.TotalBytes);
											if (percent - NowLawItem.LastPercentage > (double)Settings.Default.Downloader_InvokePercent)
											{
												NowLawItem.LastPercentage = percent;
												DownloadProgressChanged(NowLawItem);
											}
										}
										if (NowLawItem.DownloadBytes >= NowLawItem.TotalBytes)
										{
											DownloadProgressChanged(NowLawItem);
											downloaderType = DownloaderType.檔案下載完成;
										}
									}
								}
								catch (Exception ex)
								{
									downloaderType = DownloaderType.下載出錯;
									try
									{
										if (this.Home_UnZipError_Event != null)
										{
											this.Home_UnZipError_Event(string.Format("檔名: {0}，{1}", (NowLawItem.Name == null) ? "" : NowLawItem.Name, Enum.GetName(typeof(DownloaderType), DownloaderType.下載出錯)));
											Thread.Sleep(1100);
										}
										LogTool.Debug(ex);
									}
									catch
									{
										downloaderType = DownloaderType.下載出錯;
									}
								}
							}
						}
						catch (Exception ex2)
						{
							downloaderType = DownloaderType.下載出錯;
							try
							{
								if (this.Home_UnZipError_Event != null)
								{
									this.Home_UnZipError_Event(string.Format("檔名: {0}，{1}", (NowLawItem.Name == null) ? "" : NowLawItem.Name, Enum.GetName(typeof(DownloaderType), DownloaderType.下載出錯)));
									Thread.Sleep(1100);
								}
								LogTool.Debug(ex2);
							}
							catch
							{
								downloaderType = DownloaderType.下載出錯;
							}
						}
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
			}
			catch (Exception ex3)
			{
				downloaderType = DownloaderType.下載出錯;
				try
				{
					if (this.Home_UnZipError_Event != null)
					{
						this.Home_UnZipError_Event(string.Format("檔名: {0}，{1}", (NowLawItem.Name == null) ? "" : NowLawItem.Name, Enum.GetName(typeof(DownloaderType), DownloaderType.下載出錯)));
						Thread.Sleep(1100);
					}
					LogTool.Debug(ex3);
				}
				catch
				{
					downloaderType = DownloaderType.下載出錯;
				}
			}
			switch (downloaderType)
			{
			case DownloaderType.沒有任何檔案下載中:
			case (DownloaderType)1:
			case DownloaderType.正在下載中:
				break;
			case DownloaderType.停止:
				StartNextFileItemDownload(NowLawItem);
				break;
			case DownloaderType.下載出錯:
				downloaderType = DownloaderType.沒有任何檔案下載中;
				StartNextFileItemDownload(NowLawItem);
				break;
			case DownloaderType.暫停:
				StartNextFileItemDownload(NowLawItem);
				break;
			case DownloaderType.檔案下載完成:
				DownloadFileCompleted(NowLawItem);
				StartNextFileItemDownload(NowLawItem);
				break;
			}
		}

		private void UpdateToDB(Law_DownloadItemViewModel lawItem)
		{
		}

		private void StartNextFileItemDownload(Law_DownloadItemViewModel lawItem)
		{
			Action action = null;
			lock (this)
			{
				if (downloaderType == DownloaderType.停止)
				{
					list.Clear();
				}
				lawItem = null;
				downloaderType = DownloaderType.沒有任何檔案下載中;
				if (downloaderType == DownloaderType.沒有任何檔案下載中)
				{
					TaskFactory factory = Task.Factory;
					if (action == null)
					{
						action = new Action(_003CStartNextFileItemDownload_003Eb__2);
					}
					factory.StartNew(action, TaskCreationOptions.LongRunning);
				}
			}
		}

		public void AddItem(Law_DownloadItemViewModel lawItem)
		{
			Action action = null;
			lock (this)
			{
				if (lawItem != null)
				{
					list.Add(lawItem);
					if (downloaderType == DownloaderType.沒有任何檔案下載中)
					{
						TaskFactory factory = Task.Factory;
						if (action == null)
						{
							action = new Action(_003CAddItem_003Eb__5);
						}
						factory.StartNew(action, TaskCreationOptions.LongRunning);
					}
				}
			}
		}

		public void AddItem(List<Law_DownloadItemViewModel> lawItemList)
		{
			Action action = null;
			lock (this)
			{
				list.AddRange(lawItemList);
				if (downloaderType == DownloaderType.沒有任何檔案下載中)
				{
					TaskFactory factory = Task.Factory;
					if (action == null)
					{
						action = new Action(_003CAddItem_003Eb__8);
					}
					factory.StartNew(action);
				}
			}
		}

		public void Pause(string lawItem_ID)
		{
			Predicate<Law_DownloadItemViewModel> predicate = null;
			_003C_003Ec__DisplayClassd _003C_003Ec__DisplayClassd = new _003C_003Ec__DisplayClassd();
			_003C_003Ec__DisplayClassd.lawItem_ID = lawItem_ID;
			lock (this)
			{
				List<Law_DownloadItemViewModel> obj = list;
				if (predicate == null)
				{
					predicate = new Predicate<Law_DownloadItemViewModel>(_003C_003Ec__DisplayClassd._003CPause_003Eb__b);
				}
				obj.RemoveAll(predicate);
				if (NowLawItem != null && NowLawItem.ID.Equals(_003C_003Ec__DisplayClassd.lawItem_ID))
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
				if (downloaderType != 0)
				{
					downloaderType = DownloaderType.停止;
				}
			}
		}

		public Law_DownloadItemViewModel GetInList(string lawDataLaw_ID)
		{
			Func<Law_DownloadItemViewModel, bool> func = null;
			_003C_003Ec__DisplayClass13 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass13();
			_003C_003Ec__DisplayClass.lawDataLaw_ID = lawDataLaw_ID;
			lock (this)
			{
				if (NowLawItem != null && NowLawItem.ID.Equals(_003C_003Ec__DisplayClass.lawDataLaw_ID))
				{
					return NowLawItem;
				}
				List<Law_DownloadItemViewModel> source = list;
				if (func == null)
				{
					func = new Func<Law_DownloadItemViewModel, bool>(_003C_003Ec__DisplayClass._003CGetInList_003Eb__11);
				}
				return Enumerable.FirstOrDefault(Enumerable.Where(source, func));
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

		private void DownloadFileStart(Law_DownloadItemViewModel lawItem)
		{
			if (lawItem != null)
			{
				if (lawItem.StorageFileName.EndsWith(".update"))
				{
					lawItem.FileType = LawFileType.更新檔正在下載中;
				}
				else
				{
					lawItem.FileType = LawFileType.正在下載中;
				}
				if (this.LawListCT_DownloadFileStart_Event != null)
				{
					this.LawListCT_DownloadFileStart_Event(lawItem);
				}
			}
		}

		private void DownloadProgressChanged(Law_DownloadItemViewModel lawItem)
		{
			if (lawItem != null && this.LawListCT_DownloadProgressChanged_Event != null)
			{
				this.LawListCT_DownloadProgressChanged_Event(lawItem);
			}
		}

		private void DownloadFileCompleted(Law_DownloadItemViewModel lawItem)
		{
			if (lawItem != null)
			{
				if (lawItem.StorageFileName.EndsWith(".update"))
				{
					lawItem.FileType = LawFileType.更新檔已下載完成;
					File.Move(lawItem.StorageFilePath, lawItem.StorageFilePath.Replace(".update", ""));
					lawItem.StorageFileName = lawItem.StorageFileName.Replace(".update", "");
				}
				else
				{
					lawItem.FileType = LawFileType.已下載完成;
				}
				UnzipTrigger(lawItem);
				if (this.LawListCT_DownloadFileCompleted_Event != null)
				{
					this.LawListCT_DownloadFileCompleted_Event(lawItem);
				}
				string text = "update  LawRow set AtDownloadFinished_XmlUpDate=@1\r\n                       , Link=@2,StorageFileName=@3\r\n                       , DownloadBytes=@4,TotalBytes=@5 where ID=@6 and UserID=@7";
				int num = MSCE.ExecuteNonQuery(text, lawItem.UpDate.ToString("yyyy/MM/dd HH:mm:ss"), lawItem.Link, lawItem.StorageFileName, lawItem.DownloadBytes.ToString(), lawItem.TotalBytes.ToString(), lawItem.ID, lawItem.UserID);
				if (num < 1)
				{
					LogTool.Debug(new Exception("DB失敗:" + text));
				}
			}
		}

		private void UnzipTrigger(Law_DownloadItemViewModel lawItem)
		{
			if (lawItem == null)
			{
				return;
			}
			if (lawItem.FileType == LawFileType.已下載完成)
			{
				lawItem.FileType = LawFileType.解壓縮中;
			}
			else
			{
				lawItem.FileType = LawFileType.更新檔解壓縮中;
			}
			bool flag = false;
			if (this.LawListCT_UnZip_Event != null)
			{
				this.LawListCT_UnZip_Event(lawItem);
			}
			try
			{
				UnZipTool unZipTool = new UnZipTool();
				flag = unZipTool.UnZip(lawItem.StorageFilePath, lawItem.UnZipFilePath, "", true);
			}
			catch (Exception ex)
			{
				LogTool.Debug(ex);
			}
			if (flag)
			{
				if (lawItem.FileType == LawFileType.解壓縮中)
				{
					lawItem.FileType = LawFileType.已下載完成;
				}
				else
				{
					lawItem.FileType = LawFileType.更新檔已下載完成;
				}
				return;
			}
			if (lawItem.FileType == LawFileType.解壓縮中)
			{
				lawItem.DownloadBytes = 0L;
				lawItem.TotalBytes = 0L;
				lawItem.LastPercentage = 0.0;
				if (File.Exists(lawItem.StorageFilePath))
				{
					File.Delete(lawItem.StorageFilePath);
				}
				string text = "update  LawRow set AtDownloadFinished_XmlUpDate=@1\r\n                                           , Link=@2,StorageFileName=@3\r\n                                           , DownloadBytes=@4,TotalBytes=@5 where ID=@6 and UserID=@7";
				int num = MSCE.ExecuteNonQuery(text, lawItem.UpDate.ToString("yyyy/MM/dd HH:mm:ss"), lawItem.Link, lawItem.StorageFileName, lawItem.DownloadBytes.ToString(), lawItem.TotalBytes.ToString(), lawItem.ID, lawItem.UserID);
				if (num < 1)
				{
					LogTool.Debug(new Exception("DB失敗:" + text));
				}
			}
			if (lawItem.FileType == LawFileType.解壓縮中)
			{
				lawItem.FileType = LawFileType.解壓縮失敗;
				if (this.LawListCT_UnZipError_Event != null)
				{
					this.LawListCT_UnZipError_Event(lawItem);
				}
				if (this.Home_UnZipError_Event != null)
				{
					this.Home_UnZipError_Event(string.Format("{0} {1}", Settings.Default.LawButtonName, Enum.GetName(typeof(LawFileType), LawFileType.解壓縮失敗)));
				}
				lawItem.FileType = LawFileType.從未下載;
			}
			else
			{
				lawItem.FileType = LawFileType.更新檔解壓縮失敗;
				if (this.LawListCT_UnZipError_Event != null)
				{
					this.LawListCT_UnZipError_Event(lawItem);
				}
				if (this.Home_UnZipError_Event != null)
				{
					this.Home_UnZipError_Event(string.Format("檔名: {0}，{1}", Settings.Default.LawButtonName, Enum.GetName(typeof(LawFileType), LawFileType.更新檔解壓縮失敗)));
				}
				lawItem.FileType = LawFileType.更新檔未下載;
			}
		}

		[CompilerGenerated]
		private void _003CStartNextFileItemDownload_003Eb__2()
		{
			Start();
		}

		[CompilerGenerated]
		private void _003CAddItem_003Eb__5()
		{
			Start();
		}

		[CompilerGenerated]
		private void _003CAddItem_003Eb__8()
		{
			Start();
		}
	}
}
