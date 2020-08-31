using BookManagerModule;
using DataAccessObject;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using PaperLess_Emeeting.App_Code.MessageBox;
using PaperLess_Emeeting.App_Code.WS;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace PaperLess_Emeeting.App_Code
{
	public class Singleton_PDFFactory
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass1
		{
			public string BookID;

			public bool _003CRemoveBookInPDFWork_003Eb__0(string x)
			{
				return x.Equals(BookID);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass5
		{
			public string fileName;

			public string PDFFactoryDirectoryName;

			public string FinalFilePath;

			public bool isHtml;

			public string bookPath;

			public int totalPage;

			public float width;

			public float height;

			public string UserAccount;

			public string bookId;

			public string dbPath;

			public string thumbsPath_Msize;

			public string thumbsPath_Lsize;

			public int counter;

			private static Comparison<string> CS_0024_003C_003E9__CachedAnonymousMethodDelegate7;

			public void _003CSavePDF_003Eb__3()
			{
				try
				{
					AddBookInPDFWork(bookId);
					BookManager bookManager = new BookManager(dbPath);
					float num = 0f;
					float num2 = 0f;
					float num3 = 0f;
					float num4 = 0f;
					if (width > height)
					{
						num3 = width;
						num4 = height;
						num = num4;
						num2 = num3;
					}
					else
					{
						num = width;
						num2 = height;
						num3 = num2;
						num4 = num;
					}
					Rectangle pageSize = new Rectangle(width, height);
					Document document = new Document(pageSize);
					FileStream fileStream = null;
					try
					{
						fileStream = new FileStream(fileName, FileMode.Create);
						PdfWriter instance = PdfWriter.GetInstance(document, fileStream);
						string[] files = Directory.GetFiles(PDFFactoryDirectoryName, "*.bmp");
						if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate7 == null)
						{
							CS_0024_003C_003E9__CachedAnonymousMethodDelegate7 = new Comparison<string>(_003CSavePDF_003Eb__4);
						}
						Array.Sort(files, CS_0024_003C_003E9__CachedAnonymousMethodDelegate7);
						document.Open();
						int num5 = 0;
						string name = Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\..\\Fonts\\kaiu.ttf";
						BaseFont bf = BaseFont.CreateFont(name, "Identity-H", false);
						new Font(bf, 16f, 0);
						string text = "";
						text = (isHtml ? Path.Combine(bookPath, "data") : Path.Combine(bookPath, "hyweb"));
						string text2 = "";
						text2 = ((!Directory.Exists(thumbsPath_Lsize)) ? thumbsPath_Msize : thumbsPath_Lsize);
						string[] files2 = Directory.GetFiles(text, "*.pdf");
						string text3 = "";
						if (files2.Length > 0)
						{
							string name2 = new FileInfo(files2[0]).Name;
							text3 = name2.Split('_')[0];
						}
						for (int i = 1; i <= totalPage; i++)
						{
							try
							{
								Path.Combine(text, text3 + "_" + i + ".pdf");
								string text4 = ".jpg";
								if (isHtml)
								{
									text4 = ".png";
								}
								string text5 = isHtml ? Path.Combine(text2, "Slide" + i + text4) : Path.Combine(text2, text3 + "_" + i + text4);
								string destFileName = Path.Combine(PDFFactoryDirectoryName, i + ".bmp");
								Directory.CreateDirectory(Path.GetDirectoryName(text5));
								File.Exists(text5);
								if (File.Exists(text5))
								{
									File.Copy(text5, destFileName, true);
								}
								else
								{
									text5 = Path.Combine(text2, text3 + "_" + i + text4);
									if (File.Exists(text5))
									{
										File.Copy(text5, destFileName, true);
									}
								}
							}
							catch (Exception ex)
							{
								LogTool.Debug(ex);
							}
						}
						string[] array = files;
						foreach (string filename in array)
						{
							num5++;
							FileInfo fileInfo = new FileInfo(filename);
							if (fileInfo.Extension.ToLower().Equals(".bmp"))
							{
								string sqlCommand = string.Format("SELECT page,status,alpha,canvasHeight,canvasWidth,color,points,width\r\n                                                        FROM bookStrokesDetail as a inner join bookinfo as b on a.userbook_sno=b.sno \r\n                                                        where bookid='{0}' and page={1}  and account='{2}'", bookId, (num5 - 1).ToString(), UserAccount);
								QueryResult queryResult = bookManager.sqlCommandQuery(sqlCommand);
								float num6 = 0f;
								float num7 = 0f;
								if (queryResult != null && queryResult.fetchRow())
								{
									num6 = queryResult.getFloat("canvasWidth");
									num7 = queryResult.getFloat("canvasHeight");
									if (num6 > 0f && num7 > 0f)
									{
										if (num6 > num7)
										{
											if (num3 <= 0f || num4 <= 0f)
											{
												num3 = width;
												num4 = height;
												num = num4;
												num2 = num3;
											}
										}
										else if (num <= 0f || num2 <= 0f)
										{
											num = width;
											num2 = height;
											num3 = num2;
											num4 = num;
										}
									}
								}
								Image instance2 = Image.GetInstance(filename);
								float num8 = 1f;
								float num9 = 1f;
								pageSize = ((instance2.Width > instance2.Height) ? ((!(num3 > 0f) || !(num4 > 0f)) ? new Rectangle(num2, num) : new Rectangle(num3, num4)) : ((!(num > 0f) || !(num2 > 0f)) ? new Rectangle(num4, num4) : new Rectangle(num, num2)));
								if (num6 > 0f && num7 > 0f)
								{
									num8 = pageSize.Width / num6;
									num9 = pageSize.Height / num7;
								}
								document.SetPageSize(pageSize);
								document.NewPage();
								instance2.SetDpi(300, 300);
								instance2.ScaleToFit(pageSize.Width, pageSize.Height);
								instance2.SetAbsolutePosition(0f, 0f);
								document.Add(instance2);
								sqlCommand = string.Format("select notes from booknoteDetail as a inner join bookInfo as b on a.userbook_sno=b.sno   where bookid='{0}' and page='{1}' and account='{2}'", bookId, (num5 - 1).ToString(), UserAccount);
								queryResult = bookManager.sqlCommandQuery(sqlCommand);
								if (queryResult != null && queryResult.fetchRow())
								{
									document.Add(new Paragraph("\r\n"));
									document.Add(new Annotation("註解", queryResult.getString("notes")));
								}
								sqlCommand = string.Format("SELECT page,status,alpha,canvasHeight,canvasWidth,color,points,width\r\n                                                        FROM bookStrokesDetail as a inner join bookinfo as b on a.userbook_sno=b.sno \r\n                                                        where bookid='{0}' and page={1} and status='0' and account='{2}'", bookId, (num5 - 1).ToString(), UserAccount);
								try
								{
									queryResult = bookManager.sqlCommandQuery(sqlCommand);
								}
								catch (Exception ex2)
								{
									LogTool.Debug(ex2);
								}
								if (queryResult != null)
								{
									while (queryResult.fetchRow())
									{
										string @string = queryResult.getString("color");
										float @float = queryResult.getFloat("alpha");
										int red = Convert.ToInt32(@string.Substring(1, 2), 16);
										int green = Convert.ToInt32(@string.Substring(3, 2), 16);
										int blue = Convert.ToInt32(@string.Substring(5, 2), 16);
										float float2 = queryResult.getFloat("width");
										string string2 = queryResult.getString("points");
										string[] array2 = string2.Split(new char[1]
										{
											';'
										}, StringSplitOptions.RemoveEmptyEntries);
										int num10 = 0;
										float num11 = 0f;
										float num12 = 0f;
										List<float[]> list = new List<float[]>();
										List<float> list2 = new List<float>();
										string[] array3 = array2;
										foreach (string text6 in array3)
										{
											num10++;
											string s = text6.Split(new char[3]
											{
												'{',
												',',
												'}'
											}, StringSplitOptions.RemoveEmptyEntries)[0];
											string s2 = text6.Split(new char[3]
											{
												'{',
												',',
												'}'
											}, StringSplitOptions.RemoveEmptyEntries)[1];
											num11 = (float)int.Parse(s) * num8;
											num12 = (float)int.Parse(s2) * num9;
											list2.Add(num11);
											list2.Add(pageSize.Height - num12);
										}
										list.Add(list2.ToArray());
										PdfAnnotation pdfAnnotation = PdfAnnotation.CreateInk(instance, pageSize, "", list.ToArray());
										pdfAnnotation.Color = new BaseColor(red, green, blue, int.Parse(@float.ToString()));
										pdfAnnotation.BorderStyle = new PdfBorderDictionary(float2, 0);
										pdfAnnotation.Flags = 4;
										instance.AddAnnotation(pdfAnnotation);
									}
								}
							}
						}
						document.AddTitle("電子書");
						document.AddAuthor("Hyweb");
					}
					catch (Exception ex3)
					{
						LogTool.Debug(ex3);
					}
					finally
					{
						try
						{
							try
							{
								if (document.IsOpen())
								{
									document.Close();
								}
							}
							catch (Exception ex4)
							{
								LogTool.Debug(ex4);
							}
							try
							{
								if (fileStream != null)
								{
									fileStream.Dispose();
								}
							}
							catch (Exception ex5)
							{
								LogTool.Debug(ex5);
							}
						}
						catch (Exception ex6)
						{
							LogTool.Debug(ex6);
						}
					}
					if (File.Exists(fileName))
					{
						File.Copy(fileName, FinalFilePath, true);
					}
					counter++;
					if (counter <= 3)
					{
						SavePDF(isHtml, bookPath, totalPage, width, height, UserAccount, bookId, thumbsPath_Msize, thumbsPath_Lsize, dbPath, counter);
					}
					else
					{
						if (File.Exists(fileName))
						{
							File.Copy(fileName, FinalFilePath, true);
						}
						RemoveBookInPDFWork(bookId);
					}
				}
				catch (Exception ex7)
				{
					LogTool.Debug(ex7);
					if (counter > 3)
					{
						AutoClosingMessageBox.Show("轉檔失敗，請稍後再試");
					}
				}
			}

			private static int _003CSavePDF_003Eb__4(string a, string b)
			{
				int num = int.Parse(new FileInfo(a).Name.Split('.')[0]);
				int value = int.Parse(new FileInfo(b).Name.Split('.')[0]);
				return num.CompareTo(value);
			}
		}

		private static readonly object thisLock = new object();

		private static List<string> instance = new List<string>();

		public static Home_UnZipError_Function Home_UnZipError_Callback;

		private Singleton_PDFFactory()
		{
		}

		public static List<string> GetInstance()
		{
			return instance;
		}

		public static bool IsPDFInWork(string BookID)
		{
			if (instance.Contains(BookID))
			{
				return true;
			}
			return false;
		}

		public static void AddBookInPDFWork(string BookID)
		{
			RemoveBookInPDFWork(BookID);
			instance.Add(BookID);
		}

		public static void RemoveBookInPDFWork(string BookID)
		{
			_003C_003Ec__DisplayClass1 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass1();
			_003C_003Ec__DisplayClass.BookID = BookID;
			instance.RemoveAll(new Predicate<string>(_003C_003Ec__DisplayClass._003CRemoveBookInPDFWork_003Eb__0));
		}

		public static void ClearInstance()
		{
			instance.Clear();
		}

		public static void SavePDF(PDFFactoryParameter pdfFactoryParameter)
		{
			SavePDF(pdfFactoryParameter.isHtml, pdfFactoryParameter.bookPath, pdfFactoryParameter.totalPage, pdfFactoryParameter.width, pdfFactoryParameter.height, pdfFactoryParameter.UserAccount, pdfFactoryParameter.bookId, pdfFactoryParameter.dbPath, pdfFactoryParameter.thumbsPath_Msize, pdfFactoryParameter.thumbsPath_Lsize);
		}

		public static void SavePDF(bool isHtml, string bookPath, int totalPage, float width, float height, string UserAccount, string bookId, string dbPath, string thumbsPath_Msize, string thumbsPath_Lsize, int counter = 0)
		{
			_003C_003Ec__DisplayClass5 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass5();
			_003C_003Ec__DisplayClass.isHtml = isHtml;
			_003C_003Ec__DisplayClass.bookPath = bookPath;
			_003C_003Ec__DisplayClass.totalPage = totalPage;
			_003C_003Ec__DisplayClass.width = width;
			_003C_003Ec__DisplayClass.height = height;
			_003C_003Ec__DisplayClass.UserAccount = UserAccount;
			_003C_003Ec__DisplayClass.bookId = bookId;
			_003C_003Ec__DisplayClass.dbPath = dbPath;
			_003C_003Ec__DisplayClass.thumbsPath_Msize = thumbsPath_Msize;
			_003C_003Ec__DisplayClass.thumbsPath_Lsize = thumbsPath_Lsize;
			_003C_003Ec__DisplayClass.counter = counter;
			if (_003C_003Ec__DisplayClass.counter == 0)
			{
				PDFFactoryParameter pDFFactoryParameter = new PDFFactoryParameter();
				pDFFactoryParameter.isHtml = _003C_003Ec__DisplayClass.isHtml;
				pDFFactoryParameter.bookPath = _003C_003Ec__DisplayClass.bookPath;
				pDFFactoryParameter.totalPage = _003C_003Ec__DisplayClass.totalPage;
				pDFFactoryParameter.width = _003C_003Ec__DisplayClass.width;
				pDFFactoryParameter.height = _003C_003Ec__DisplayClass.height;
				pDFFactoryParameter.UserAccount = _003C_003Ec__DisplayClass.UserAccount;
				pDFFactoryParameter.bookId = _003C_003Ec__DisplayClass.bookId;
				pDFFactoryParameter.dbPath = _003C_003Ec__DisplayClass.dbPath;
				pDFFactoryParameter.thumbsPath_Msize = _003C_003Ec__DisplayClass.thumbsPath_Msize;
				pDFFactoryParameter.thumbsPath_Lsize = _003C_003Ec__DisplayClass.thumbsPath_Lsize;
				DataTable dataTable = MSCE.GetDataTable("select ID from FileRow where userid=@1 and id=@2", _003C_003Ec__DisplayClass.UserAccount.Replace("_Sync", ""), _003C_003Ec__DisplayClass.bookId);
				if (dataTable.Rows.Count > 0)
				{
					dataTable.Rows[0]["ID"].ToString();
					string text = "update  FileRow set PDFFactoryParameterJson=@1 where userid=@2 and id=@3";
					int num = MSCE.ExecuteNonQuery(text, JsonConvert.SerializeObject(pDFFactoryParameter), _003C_003Ec__DisplayClass.UserAccount.Replace("_Sync", ""), _003C_003Ec__DisplayClass.bookId);
					if (num < 1)
					{
						LogTool.Debug(new Exception("DB失敗: " + text));
					}
				}
			}
			_003C_003Ec__DisplayClass.fileName = Path.Combine(_003C_003Ec__DisplayClass.bookPath, "PDFFactory\\PDF.pdf");
			_003C_003Ec__DisplayClass.PDFFactoryDirectoryName = Path.GetDirectoryName(_003C_003Ec__DisplayClass.fileName);
			_003C_003Ec__DisplayClass.FinalFilePath = Path.Combine(_003C_003Ec__DisplayClass.bookPath, "PDF.pdf");
			Directory.CreateDirectory(_003C_003Ec__DisplayClass.PDFFactoryDirectoryName);
			Task.Factory.StartNew(new Action(_003C_003Ec__DisplayClass._003CSavePDF_003Eb__3));
		}
	}
}
