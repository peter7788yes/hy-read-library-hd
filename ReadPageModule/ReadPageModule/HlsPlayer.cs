using BookManagerModule;
using CACodec;
using DataAccessObject;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Windows;
using System.Windows.Forms;

namespace ReadPageModule
{
	[ComVisible(true)]
	[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
	public class HlsPlayer : Form
	{
		[CompilerGenerated]
		private EventHandler<DataRequestEventArgs> m_a;

		private HttpListener m_b;

		private volatile bool m_c = true;

		private IAsyncResult d;

		private Thread e;

		private string f = "";

		private string g = "";

		private string h = "";

		private string i = "";

		private int j;

		private long k;

		private byte[] l;

		private BookManager m;

		private static CACodecTools n = new CACodecTools();

		private System.Windows.Forms.Timer o = new System.Windows.Forms.Timer();

		private bool p;

		private string q = "";

		private IContainer r;

		private WebBrowser s;

		public event EventHandler<DataRequestEventArgs> HlsPlayerDataRequest
		{
			[CompilerGenerated]
			add
			{
				EventHandler<DataRequestEventArgs> eventHandler = this.m_a;
				EventHandler<DataRequestEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<DataRequestEventArgs> value2 = (EventHandler<DataRequestEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange(ref this.m_a, value2, eventHandler2);
				}
				while ((object)eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<DataRequestEventArgs> eventHandler = this.m_a;
				EventHandler<DataRequestEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<DataRequestEventArgs> value2 = (EventHandler<DataRequestEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange(ref this.m_a, value2, eventHandler2);
				}
				while ((object)eventHandler != eventHandler2);
			}
		}

		public HlsPlayer(string title, string bookId, string userId, string BookPath, byte[] defaultKey, string sourcePath, int userBookSno, long hlsLastTime, BookManager bookManager)
		{
			a();
			Text = title;
			h = BookPath;
			l = defaultKey;
			i = sourcePath;
			j = userBookSno;
			k = hlsLastTime;
			m = bookManager;
			f = bookId;
			g = userId;
			if (!b())
			{
				if (System.Windows.MessageBox.Show(m.LanqMng.getLangString("grantUrlaclMsg"), m.LanqMng.getLangString("grantUrlacl"), MessageBoxButton.YesNo, MessageBoxImage.Exclamation).Equals(MessageBoxResult.Yes))
				{
					c();
				}
				else
				{
					p = true;
				}
			}
			if (!p)
			{
				e = new Thread(new ParameterizedThreadStart(SimpleListenerExample));
				e.Start(new string[1]
				{
					"http://+:9000/"
				});
				Thread.Sleep(1);
			}
		}

		private void c()
		{
			string arguments = string.Format("http add urlacl url={0} user=everyone", "http://+:9000/");
			ProcessStartInfo processStartInfo = new ProcessStartInfo("netsh", arguments);
			processStartInfo.Verb = "runas";
			processStartInfo.CreateNoWindow = true;
			processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			processStartInfo.UseShellExecute = true;
			Process.Start(processStartInfo).WaitForExit();
		}

		private bool b()
		{
			if (Registry.GetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\HTTP\\Parameters\\UrlAclInfo", "http://+:9000/", "").Equals(""))
			{
				return false;
			}
			return true;
		}

		private void b(object A_0, EventArgs A_1)
		{
			if (p)
			{
				Close();
				return;
			}
			s.ScriptErrorsSuppressed = true;
			s.AllowWebBrowserDrop = false;
			s.IsWebBrowserContextMenuEnabled = false;
			s.WebBrowserShortcutsEnabled = false;
			s.ObjectForScripting = this;
			s.Navigate("http://127.0.0.1:9000/flashls/index.html");
			string sqlCommand = "select hlsLastTime from userbook_metadata Where Sno= " + j;
			QueryResult queryResult = m.sqlCommandQuery(sqlCommand);
			if (queryResult.fetchRow())
			{
				try
				{
					k = queryResult.getLong("hlsLastTime");
				}
				catch
				{
					k = 0L;
				}
			}
			if (k > 0)
			{
				if (!System.Windows.MessageBox.Show(m.LanqMng.getLangString("resetHlsPlayTimeMsg"), m.LanqMng.getLangString("resetHlsPlayTime"), MessageBoxButton.YesNo, MessageBoxImage.Exclamation).Equals(MessageBoxResult.Yes))
				{
					k = 0L;
				}
				o.Interval = 3000;
				o.Tick += new EventHandler(a);
				o.Start();
			}
		}

		private void a(object A_0, EventArgs A_1)
		{
			if (s.Document != null)
			{
				o.Stop();
				o.Tick -= new EventHandler(a);
				s.Document.InvokeScript("seekToSpecificTime", new object[1]
				{
					k
				});
			}
		}

		public void SimpleListenerExample(object prefixes)
		{
			if (!HttpListener.IsSupported)
			{
				Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
				return;
			}
			string[] array = prefixes as string[];
			if (prefixes == null || array.Length == 0)
			{
				throw new ArgumentException("prefixes");
			}
			this.m_b = new HttpListener();
			this.m_b.Prefixes.Add(array[0]);
			this.m_b.Start();
			Console.WriteLine("Listening...");
			while (this.m_c)
			{
				d = this.m_b.BeginGetContext(new AsyncCallback(ListenerCallback), this.m_b);
				d.AsyncWaitHandle.WaitOne();
			}
			this.m_b.Stop();
			this.m_b.Close();
		}

		public void ListenerCallback(IAsyncResult result)
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary[".ico"] = "image/x-icon";
			dictionary[".html"] = "text/html";
			dictionary[".css"] = "text/css";
			dictionary[".js"] = "application/javascript";
			dictionary[".png"] = "image/png";
			dictionary[".jpeg"] = "image/jpeg";
			dictionary[".mp3"] = "audio/mpeg3";
			dictionary[".mp4"] = "vidio/mpeg";
			dictionary[".m3u8"] = "application/x-mpegURL";
			dictionary[".ts"] = "video/MP2T";
			dictionary[".key"] = "hyweb/key";
			dictionary[".swf"] = "application/x-shockwave-flash";
			HttpListener httpListener = (HttpListener)result.AsyncState;
			HttpListenerContext httpListenerContext;
			try
			{
				httpListenerContext = httpListener.EndGetContext(result);
			}
			catch (Exception ex)
			{
				Console.WriteLine("e=" + ex.Message);
				return;
			}
			HttpListenerRequest request = httpListenerContext.Request;
			NameValueCollection headers = request.Headers;
			string rawUrl = request.RawUrl;
			dictionary.ContainsKey(Path.GetExtension(rawUrl));
			string text2 = headers["Range"];
			string text = "." + rawUrl;
			HttpListenerResponse response = httpListenerContext.Response;
			StreamWriter streamWriter = new StreamWriter(new BufferedStream(response.OutputStream));
			if (Path.GetExtension(rawUrl) == ".m3u8")
			{
				text = h + "\\HYWEB\\" + i;
			}
			else if (Path.GetExtension(rawUrl) == ".ts" || Path.GetExtension(rawUrl) == ".key")
			{
				text = h + "\\HYWEB" + rawUrl;
			}
			Console.WriteLine("mediaFilename: {0}", text);
			if (!File.Exists(text))
			{
				if (this.m_a != null)
				{
					string filename = rawUrl.Substring(1, rawUrl.Length - 1);
					this.m_a(this, new DataRequestEventArgs(f, g, filename));
				}
				response.StatusCode = 404;
				response.KeepAlive = false;
				streamWriter.Flush();
				streamWriter.Close();
			}
			else if (Path.GetExtension(rawUrl) == ".m3u8" || Path.GetExtension(rawUrl) == ".ts" || Path.GetExtension(rawUrl) == ".key")
			{
				string[] array = rawUrl.Split('/');
				int length = array.GetLength(0);
				string extension = Path.GetExtension(array[length - 1]);
				if (Path.GetExtension(rawUrl) == ".key" && new FileInfo(text).Length < 32)
				{
					q = "AES/ECB/NoPadding";
				}
				using (Stream stream = (Path.GetExtension(rawUrl) == ".key") ? ((Stream)n.fileAESDecodeMode(text, l, false, q)) : ((Stream)new FileStream(text, FileMode.Open)))
				{
					int num = -1;
					int result2 = -1;
					if (headers["Range"] != null)
					{
						string[] array2 = headers["Range"].ToString().Replace("bytes=", "").Split('-');
						num = int.Parse(array2[0]);
						if (array2[1].Trim().Length > 0)
						{
							int.TryParse(array2[1], out result2);
						}
						if (result2 == -1)
						{
							result2 = (int)stream.Length;
						}
					}
					else
					{
						num = 0;
						result2 = (int)stream.Length;
					}
					byte[] array3 = new byte[result2 - num];
					stream.Position = num;
					stream.Read(array3, 0, result2 - num);
					stream.Flush();
					stream.Close();
					int num2 = num + array3.Length;
					try
					{
						response.StatusCode = 206;
						response.ContentType = dictionary[extension];
						response.AddHeader("Accept-Ranges", "bytes");
						response.AddHeader("Content-Range", string.Format("bytes {0}-{1}/{2}", num, num2 - 1, num2));
						response.KeepAlive = false;
						response.ContentLength64 = array3.Length;
						streamWriter.BaseStream.Write(array3, 0, array3.Length);
						streamWriter.Flush();
						streamWriter.Close();
					}
					catch (Exception ex2)
					{
						Console.WriteLine(ex2.Message);
					}
				}
			}
			else
			{
				string[] array4 = rawUrl.Split('/');
				int length2 = array4.GetLength(0);
				string extension2 = Path.GetExtension(array4[length2 - 1]);
				byte[] array5 = File.ReadAllBytes("./" + rawUrl);
				try
				{
					response.StatusCode = 200;
					response.ContentType = dictionary[extension2];
					response.KeepAlive = false;
					response.ContentLength64 = array5.Length;
					streamWriter.BaseStream.Write(array5, 0, array5.Length);
					streamWriter.Flush();
					streamWriter.Close();
				}
				catch (Exception ex3)
				{
					Console.WriteLine(ex3.Message);
				}
			}
		}

		public void getCurrentPlayTime(double playTime)
		{
			if (playTime > 3.0)
			{
				playTime -= 3.0;
			}
			string sqlCommand = "update userbook_metadata set hlsLastTime = " + playTime + " Where Sno= " + j;
			m.sqlCommandNonQuery(sqlCommand);
		}

		private void a(object A_0, FormClosingEventArgs A_1)
		{
			if (!p)
			{
				try
				{
					s.Document.InvokeScript("getCurrentPlayTime", new object[0]);
					this.m_c = false;
					d.AsyncWaitHandle.Close();
					e.Abort();
					this.m_b.Stop();
					this.m_b.Close();
				}
				catch
				{
				}
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && r != null)
			{
				r.Dispose();
			}
			base.Dispose(disposing);
		}

		private void a()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(HlsPlayer));
			s = new WebBrowser();
			SuspendLayout();
			s.Dock = DockStyle.Fill;
			s.Location = new System.Drawing.Point(0, 0);
			s.MinimumSize = new System.Drawing.Size(20, 20);
			s.Name = "webBrowser1";
			s.Size = new System.Drawing.Size(593, 469);
			s.TabIndex = 0;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(593, 469);
			base.Controls.Add(s);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.MinimizeBox = false;
			base.Name = "HlsPlayer";
			Text = "Media Player";
			base.WindowState = FormWindowState.Maximized;
			base.FormClosing += new FormClosingEventHandler(a);
			base.Load += new EventHandler(b);
			ResumeLayout(false);
		}
	}
}
