using Newtonsoft.Json;
using NLog;
using PaperLess_Emeeting.Properties;
using PaperlessSync.Broadcast.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Xml;

namespace PaperlessSync.Broadcast.Socket
{
	public class SocketClient
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass9
		{
			public IEventManager mgr;

			public void _003Crun_003Eb__2()
			{
				try
				{
					if (mgr != null)
					{
						mgr.run();
					}
				}
				catch (Exception ex)
				{
					LogTool.Debug(ex);
				}
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClassc
		{
			public IEventManager mgr;

			public void _003Crun_003Eb__3()
			{
				try
				{
					if (mgr != null)
					{
						mgr.run();
					}
				}
				catch (Exception ex)
				{
					LogTool.Debug(ex);
				}
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClassf
		{
			public IEventManager mgr;

			public void _003Crun_003Eb__4()
			{
				try
				{
					if (mgr != null)
					{
						mgr.run();
					}
				}
				catch (Exception ex)
				{
					LogTool.Debug(ex);
				}
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass12
		{
			public IEventManager mgr;

			public void _003Crun_003Eb__5()
			{
				try
				{
					if (mgr != null)
					{
						mgr.run();
					}
				}
				catch (Exception ex)
				{
					LogTool.Debug(ex);
				}
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass15
		{
			public IEventManager mgr;

			public void _003Crun_003Eb__6()
			{
				try
				{
					if (mgr != null)
					{
						mgr.run();
					}
				}
				catch (Exception ex)
				{
					LogTool.Debug(ex);
				}
			}
		}

		private static Logger logger = LogManager.GetCurrentClassLogger();

		private TcpClient _socket;

		private StreamReader _in;

		private StreamWriter _out;

		private string _clientId;

		private string _clientName;

		private bool _isSync;

		private int _clientType;

		private string _func;

		private bool _forceLogin;

		private List<IEventManager> _eventManagerList = new List<IEventManager>();

		private List<IEventManager> _closeEventManagerList = new List<IEventManager>();

		private string meetingId;

		private int maxClient;

		private string clientId;

		private string clientName;

		private bool isSync;

		private int clientType;

		private string func;

		public string SyncServerUrl = SocketTool.GetUrl() + "/JoinSyncServer";

		private bool _joinSuccess;

		[CompilerGenerated]
		private string _003Cip_003Ek__BackingField;

		[CompilerGenerated]
		private int _003Cport_003Ek__BackingField;

		public bool JoinSuccess
		{
			get
			{
				return _joinSuccess;
			}
		}

		public string ip
		{
			[CompilerGenerated]
			get
			{
				return _003Cip_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003Cip_003Ek__BackingField = value;
			}
		}

		public int port
		{
			[CompilerGenerated]
			get
			{
				return _003Cport_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003Cport_003Ek__BackingField = value;
			}
		}

		public SocketClient(string syncServerUrl, string meetingId, int maxClient, string clientId, string clientName, bool isSync, int clientType, string func)
		{
			_joinSuccess = InitSokcetClient(syncServerUrl, meetingId, maxClient, clientId, clientName, isSync, clientType, func);
		}

		public SocketClient(string syncServerUrl, string meetingId, int maxClient, string clientId, string clientName, bool isSync, int clientType, string func, bool forceLogin = true)
		{
			_joinSuccess = InitSokcetClient(syncServerUrl, meetingId, maxClient, clientId, clientName, isSync, clientType, func, forceLogin);
		}

		public SocketClient(string meetingId, int maxClient, string clientId, string clientName, bool isSync, int clientType, string func)
		{
			string syncServerUrl = Settings.Default["SyncServerUrl"].ToString() + "/JoinSyncServer";
			_joinSuccess = InitSokcetClient(syncServerUrl, meetingId, maxClient, clientId, clientName, isSync, clientType, func);
		}

		public SocketClient(string meetingId, int maxClient, string clientId, string clientName, bool isSync, int clientType, string func, bool forceLogin = true)
		{
			string syncServerUrl = Settings.Default["SyncServerUrl"].ToString() + "/JoinSyncServer";
			_joinSuccess = InitSokcetClient(syncServerUrl, meetingId, maxClient, clientId, clientName, isSync, clientType, func, forceLogin);
		}

		public bool InitSokcetClient(string syncServerUrl, string meetingId, int maxClient, string clientId, string clientName, bool isSync, int clientType, string func, bool forceLogin = true)
		{
			bool flag = true;
			SyncServerUrl = syncServerUrl;
			this.meetingId = meetingId;
			this.maxClient = maxClient;
			this.clientId = clientId;
			this.clientName = clientName;
			this.isSync = isSync;
			this.clientType = clientType;
			this.func = func;
			logger.Debug("syncServerUrl:{0}", syncServerUrl);
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(syncServerUrl);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>").Append("<Sync>").Append("<Start ID=\"")
				.Append(meetingId)
				.Append("\" MaxClient=\"")
				.Append(maxClient)
				.Append("\"/>")
				.Append("<Init>")
				.Append(Settings.Default["InitConfig"].ToString())
				.Append("</Init>")
				.Append("</Sync>");
			string text = "";
			try
			{
				string text2 = stringBuilder.ToString();
				logger.Debug("post data-> {0}", text2);
				byte[] bytes = Encoding.UTF8.GetBytes(text2);
				httpWebRequest.Method = "POST";
				httpWebRequest.ContentType = "text/xml; encoding='utf-8'";
				httpWebRequest.ContentLength = bytes.Length;
				Stream requestStream = httpWebRequest.GetRequestStream();
				requestStream.Write(bytes, 0, bytes.Length);
				requestStream.Close();
				WebResponse response = httpWebRequest.GetResponse();
				requestStream = response.GetResponseStream();
				StreamReader streamReader = new StreamReader(requestStream);
				text = streamReader.ReadToEnd();
				logger.Debug("response from server-> {0}", text);
				streamReader.Close();
				requestStream.Close();
				response.Close();
			}
			catch
			{
			}
			if (text == "" || text.Contains("失敗"))
			{
				return false;
			}
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(text);
				XmlNode documentElement = xmlDocument.DocumentElement;
				string value = documentElement.SelectSingleNode("/Join/Start/@IP").Value;
				int port = int.Parse(documentElement.SelectSingleNode("/Join/Start/@Port").Value);
				_socket = new TcpClient(value, port);
				_clientId = clientId;
				_clientName = clientName;
				_isSync = isSync;
				_clientType = clientType;
				_func = func;
				_forceLogin = forceLogin;
				_eventManagerList = new List<IEventManager>();
				_closeEventManagerList = new List<IEventManager>();
			}
			catch
			{
				flag = false;
			}
			_joinSuccess = flag;
			return flag;
		}

		public bool GetIsConnected()
		{
			bool result = false;
			if (_in != null && _out != null && _socket != null && JoinSuccess)
			{
				result = true;
			}
			return result;
		}

		public bool ReConnect(bool isSync)
		{
			InitSokcetClient(SyncServerUrl, meetingId, maxClient, clientId, clientName, isSync, clientType, func);
			return GetIsConnected();
		}

		public void AddEventManager(IEventManager eventManager)
		{
			RemoveEventManager(eventManager);
			bool flag = true;
			foreach (IEventManager eventManager2 in _eventManagerList)
			{
				if (eventManager == eventManager2)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				_eventManagerList.Add(eventManager);
			}
		}

		public void AddEventManager(List<IEventManager> list)
		{
			_eventManagerList.AddRange(list);
		}

		public void AddCloseEventManager(List<IEventManager> list)
		{
			_closeEventManagerList.AddRange(list);
		}

		public void AddCloseEventManager(IEventManager eventManager)
		{
			RemoveCloseEventManager(eventManager);
			bool flag = true;
			foreach (IEventManager closeEventManager in _closeEventManagerList)
			{
				if (eventManager == closeEventManager)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				_closeEventManagerList.Add(eventManager);
			}
		}

		public void RemoveEventManager(IEventManager eventManager)
		{
			try
			{
				foreach (IEventManager eventManager2 in _eventManagerList)
				{
					if (eventManager.managerId.Equals(eventManager2.managerId))
					{
						_eventManagerList.Remove(eventManager2);
						break;
					}
				}
			}
			catch
			{
			}
		}

		public void RemoveCloseEventManager(IEventManager eventManager)
		{
			try
			{
				foreach (IEventManager closeEventManager in _closeEventManagerList)
				{
					if (eventManager.managerId.Equals(closeEventManager.managerId))
					{
						_closeEventManagerList.Remove(closeEventManager);
						break;
					}
				}
			}
			catch
			{
			}
		}

		public static ulong GetCurrentTimeInUnixMillis()
		{
			TimeSpan timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
			return (ulong)((long)timeSpan.Days * 86400000L + (long)timeSpan.Hours * 3600000L + (long)timeSpan.Minutes * 60000L + (long)timeSpan.Seconds * 1000L + timeSpan.Milliseconds);
		}

		public void run()
		{
			ThreadStart threadStart = null;
			try
			{
				NetworkStream stream = _socket.GetStream();
				_in = new StreamReader(stream, Encoding.UTF8);
				_out = new StreamWriter(stream, Encoding.UTF8);
				if (_clientId != null)
				{
					Dictionary<string, object> dictionary = new Dictionary<string, object>();
					dictionary.Add("cmd", "init");
					dictionary.Add("clientId", _clientId);
					dictionary.Add("clientName", _clientName);
					dictionary.Add("isSync", _isSync);
					dictionary.Add("clientType", _clientType);
					dictionary.Add("func", _func);
					dictionary.Add("forceLogin", _forceLogin);
					string text = JsonConvert.SerializeObject(dictionary);
					logger.Debug("initMap-> {0}", text);
					_out.WriteLine(text);
					_out.Flush();
				}
				if (threadStart == null)
				{
					threadStart = new ThreadStart(_003Crun_003Eb__1);
				}
				Thread thread = new Thread(threadStart);
				thread.IsBackground = false;
				thread.Start();
				while (true)
				{
					string text2 = _in.ReadLine();
					if (text2 != null)
					{
						Console.WriteLine("_in => " + text2);
					}
					if (text2 == null)
					{
						break;
					}
					if (!"_chk".Equals(text2))
					{
						logger.Debug("Receiving message from Server, pass to EventManager");
						lock (this)
						{
							try
							{
								using (List<IEventManager>.Enumerator enumerator = _eventManagerList.GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										ThreadStart threadStart2 = null;
										_003C_003Ec__DisplayClassc _003C_003Ec__DisplayClassc = new _003C_003Ec__DisplayClassc();
										_003C_003Ec__DisplayClassc.mgr = enumerator.Current;
										if (_003C_003Ec__DisplayClassc.mgr != null)
										{
											_003C_003Ec__DisplayClassc.mgr.clientId = _clientId;
											_003C_003Ec__DisplayClassc.mgr.msg = text2;
											if (threadStart2 == null)
											{
												threadStart2 = new ThreadStart(_003C_003Ec__DisplayClassc._003Crun_003Eb__3);
											}
											Thread thread2 = new Thread(threadStart2);
											thread2.Start();
										}
									}
								}
							}
							catch (Exception ex)
							{
								LogTool.Debug(ex);
								try
								{
									if (_closeEventManagerList != null)
									{
										using (List<IEventManager>.Enumerator enumerator2 = _eventManagerList.GetEnumerator())
										{
											while (enumerator2.MoveNext())
											{
												ThreadStart threadStart3 = null;
												_003C_003Ec__DisplayClassf _003C_003Ec__DisplayClassf = new _003C_003Ec__DisplayClassf();
												_003C_003Ec__DisplayClassf.mgr = enumerator2.Current;
												if (_003C_003Ec__DisplayClassf.mgr != null)
												{
													_003C_003Ec__DisplayClassf.mgr.clientId = _clientId;
													_003C_003Ec__DisplayClassf.mgr.msg = text2;
													if (threadStart3 == null)
													{
														threadStart3 = new ThreadStart(_003C_003Ec__DisplayClassf._003Crun_003Eb__4);
													}
													Thread thread3 = new Thread(threadStart3);
													thread3.Start();
												}
											}
										}
									}
								}
								catch (Exception ex2)
								{
									LogTool.Debug(ex2);
								}
							}
						}
					}
				}
				logger.Debug("InStream got null...socket may be closed...");
			}
			catch (Exception ex3)
			{
				logger.Debug("socket runtime exception: {0}", ex3.ToString());
				try
				{
					try
					{
						using (List<IEventManager>.Enumerator enumerator3 = _closeEventManagerList.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								ThreadStart threadStart4 = null;
								_003C_003Ec__DisplayClass12 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass12();
								_003C_003Ec__DisplayClass.mgr = enumerator3.Current;
								if (_003C_003Ec__DisplayClass.mgr != null)
								{
									if (threadStart4 == null)
									{
										threadStart4 = new ThreadStart(_003C_003Ec__DisplayClass._003Crun_003Eb__5);
									}
									Thread thread4 = new Thread(threadStart4);
									thread4.Start();
								}
							}
						}
					}
					catch (Exception ex4)
					{
						logger.Debug("socket close: {0}", ex4.ToString());
						if (_closeEventManagerList != null)
						{
							using (List<IEventManager>.Enumerator enumerator4 = _closeEventManagerList.GetEnumerator())
							{
								while (enumerator4.MoveNext())
								{
									ThreadStart threadStart5 = null;
									_003C_003Ec__DisplayClass15 _003C_003Ec__DisplayClass2 = new _003C_003Ec__DisplayClass15();
									_003C_003Ec__DisplayClass2.mgr = enumerator4.Current;
									if (threadStart5 == null)
									{
										threadStart5 = new ThreadStart(_003C_003Ec__DisplayClass2._003Crun_003Eb__6);
									}
									Thread thread5 = new Thread(threadStart5);
									thread5.Start();
								}
							}
						}
					}
					_out = null;
					_in = null;
					_socket = null;
				}
				catch (Exception ex5)
				{
					logger.Debug("socket close: {0}", ex5.ToString());
				}
			}
		}

		public void syncSwitch(bool isSync)
		{
			try
			{
				if (_socket != null)
				{
					Dictionary<string, object> dictionary = new Dictionary<string, object>();
					dictionary.Add("cmd", isSync ? "syncOn" : "syncOff");
					string text = JsonConvert.SerializeObject(dictionary);
					logger.Debug("syncSwitch-> {0}", text);
					_out.WriteLine(text);
					_out.Flush();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public void getUserList()
		{
			try
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("cmd", "userList");
				string value = JsonConvert.SerializeObject(dictionary);
				logger.Debug("getUserList-> {0}", JsonConvert.SerializeObject(dictionary));
				_out.WriteLine(value);
				_out.Flush();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public void setSyncOwner(string ownerId)
		{
			try
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("cmd", "syncOwner");
				dictionary.Add("clientId", (ownerId == null) ? "" : ownerId);
				string value = JsonConvert.SerializeObject(dictionary);
				logger.Debug("setSyncOwner-> {0}", JsonConvert.SerializeObject(dictionary));
				_out.WriteLine(value);
				_out.Flush();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public void broadcast(string msg)
		{
			try
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				msg = msg.Replace(Environment.NewLine, "\\n");
				dictionary.Add("msg", msg);
				dictionary.Add("sender", _clientId);
				dictionary.Add("sendTime", GetCurrentTimeInUnixMillis());
				dictionary.Add("cmd", "broadcast");
				string text = JsonConvert.SerializeObject(dictionary);
				logger.Debug("broadcast-> {0}", text);
				_out.WriteLine(text);
				_out.Flush();
			}
			catch
			{
			}
		}

		public void logout()
		{
			try
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("cmd", "offline");
				string value = JsonConvert.SerializeObject(dictionary);
				logger.Debug("logout-> {0}", JsonConvert.SerializeObject(dictionary));
				_out.WriteLine(value);
				_out.Flush();
			}
			catch
			{
			}
		}

		public bool isClosed()
		{
			bool result = false;
			try
			{
				return !_socket.Connected;
			}
			catch
			{
				return result;
			}
		}

		[CompilerGenerated]
		private void _003Crun_003Eb__1()
		{
			while (true)
			{
				try
				{
					_out.WriteLine("_chk");
					_out.Flush();
					Console.WriteLine("_out => _chk");
				}
				catch
				{
					if (_closeEventManagerList != null)
					{
						using (List<IEventManager>.Enumerator enumerator = _closeEventManagerList.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								ThreadStart threadStart = null;
								_003C_003Ec__DisplayClass9 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass9();
								_003C_003Ec__DisplayClass.mgr = enumerator.Current;
								if (threadStart == null)
								{
									threadStart = new ThreadStart(_003C_003Ec__DisplayClass._003Crun_003Eb__2);
								}
								Thread thread = new Thread(threadStart);
								thread.Start();
							}
						}
					}
					try
					{
						_out = null;
						_in = null;
						_socket = null;
					}
					catch (Exception ex)
					{
						logger.Debug("socket close: {0}", ex.ToString());
					}
					return;
				}
				Thread.Sleep(5000);
			}
		}
	}
}
