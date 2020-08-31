using System;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

public class NetHelper
{
	public static string LocalHostName
	{
		get
		{
			return Dns.GetHostName();
		}
	}

	public static string LANIP
	{
		get
		{
			IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
			if (addressList.Length < 1)
			{
				return "";
			}
			return addressList[0].ToString();
		}
	}

	public static string WANIP
	{
		get
		{
			IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
			if (addressList.Length < 2)
			{
				return "";
			}
			return addressList[1].ToString();
		}
	}

	public static int GetValidPort(string port)
	{
		int result = -1;
		try
		{
			if (port == "")
			{
				throw new Exception("端口號不能為空！");
			}
			if (Convert.ToInt32(port) < 0 || Convert.ToInt32(port) > 65535)
			{
				throw new Exception("端口號範圍無效！");
			}
			result = Convert.ToInt32(port);
			return result;
		}
		catch (Exception ex)
		{
			string message = ex.Message;
			return result;
		}
	}

	public static IPAddress StringToIPAddress(string ip)
	{
		return IPAddress.Parse(ip);
	}

	public static string GetClientIP(Socket clientSocket)
	{
		IPEndPoint iPEndPoint = (IPEndPoint)clientSocket.RemoteEndPoint;
		return iPEndPoint.Address.ToString();
	}

	public static IPEndPoint CreateIPEndPoint(string ip, int port)
	{
		IPAddress address = StringToIPAddress(ip);
		return new IPEndPoint(address, port);
	}

	public static TcpListener CreateTcpListener()
	{
		IPAddress any = IPAddress.Any;
		IPEndPoint localEP = new IPEndPoint(any, 0);
		return new TcpListener(localEP);
	}

	public static TcpListener CreateTcpListener(string ip, int port)
	{
		IPAddress address = StringToIPAddress(ip);
		IPEndPoint localEP = new IPEndPoint(address, port);
		return new TcpListener(localEP);
	}

	public static Socket CreateTcpSocket()
	{
		return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
	}

	public static Socket CreateUdpSocket()
	{
		return new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
	}

	public static IPEndPoint GetLocalPoint(TcpListener tcpListener)
	{
		return (IPEndPoint)tcpListener.LocalEndpoint;
	}

	public static string GetLocalPoint_IP(TcpListener tcpListener)
	{
		IPEndPoint iPEndPoint = (IPEndPoint)tcpListener.LocalEndpoint;
		return iPEndPoint.Address.ToString();
	}

	public static int GetLocalPoint_Port(TcpListener tcpListener)
	{
		IPEndPoint iPEndPoint = (IPEndPoint)tcpListener.LocalEndpoint;
		return iPEndPoint.Port;
	}

	public static IPEndPoint GetLocalPoint(Socket socket)
	{
		return (IPEndPoint)socket.LocalEndPoint;
	}

	public static string GetLocalPoint_IP(Socket socket)
	{
		IPEndPoint iPEndPoint = (IPEndPoint)socket.LocalEndPoint;
		return iPEndPoint.Address.ToString();
	}

	public static int GetLocalPoint_Port(Socket socket)
	{
		IPEndPoint iPEndPoint = (IPEndPoint)socket.LocalEndPoint;
		return iPEndPoint.Port;
	}

	public static void BindEndPoint(Socket socket, IPEndPoint endPoint)
	{
		if (!socket.IsBound)
		{
			socket.Bind(endPoint);
		}
	}

	public static void BindEndPoint(Socket socket, string ip, int port)
	{
		IPEndPoint localEP = CreateIPEndPoint(ip, port);
		if (!socket.IsBound)
		{
			socket.Bind(localEP);
		}
	}

	public static void StartListen(Socket socket, int port)
	{
		IPEndPoint endPoint = CreateIPEndPoint(LocalHostName, port);
		BindEndPoint(socket, endPoint);
		socket.Listen(100);
	}

	public static void StartListen(Socket socket, int port, int maxConnection)
	{
		IPEndPoint endPoint = CreateIPEndPoint(LocalHostName, port);
		BindEndPoint(socket, endPoint);
		socket.Listen(maxConnection);
	}

	public static void StartListen(Socket socket, string ip, int port, int maxConnection)
	{
		BindEndPoint(socket, ip, port);
		socket.Listen(maxConnection);
	}

	public static bool Connect(Socket socket, string ip, int port)
	{
		try
		{
			socket.Connect(ip, port);
			return socket.Poll(-1, SelectMode.SelectWrite);
		}
		catch (SocketException ex)
		{
			throw new Exception(ex.Message);
		}
	}

	public static void SendMsg(Socket socket, byte[] msg)
	{
		socket.Send(msg, msg.Length, SocketFlags.None);
	}

	public static void SendMsg(Socket socket, string msg)
	{
		byte[] bytes = Encoding.Default.GetBytes(msg);
		socket.Send(bytes, bytes.Length, SocketFlags.None);
	}

	public static void ReceiveMsg(Socket socket, byte[] buffer)
	{
		socket.Receive(buffer);
	}

	public static string ReceiveMsg(Socket socket)
	{
		byte[] array = new byte[5000];
		int num = socket.Receive(array);
		byte[] dst = new byte[num];
		Buffer.BlockCopy(array, 0, dst, 0, num);
		return "";
	}

	public static void Close(Socket socket)
	{
		try
		{
			socket.Shutdown(SocketShutdown.Both);
		}
		catch (SocketException ex)
		{
			throw ex;
		}
		finally
		{
			socket.Close();
		}
	}

	public static bool SendEmail(string receiveEmail, string msgSubject, string msgBody, bool IsEnableSSL)
	{
		MailMessage mailMessage = new MailMessage();
		mailMessage.To.Add(receiveEmail);
		mailMessage.Subject = msgSubject;
		mailMessage.Body = msgBody;
		mailMessage.IsBodyHtml = true;
		SmtpClient smtpClient = new SmtpClient();
		smtpClient.EnableSsl = IsEnableSSL;
		try
		{
			smtpClient.Send(mailMessage);
			return true;
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	[DllImport("wininet")]
	private static extern bool InternetGetConnectedState(out int connectionDescription, int reservedValue);

	public static bool IsConnectedInternet()
	{
		int connectionDescription = 0;
		if (InternetGetConnectedState(out connectionDescription, 0))
		{
			return true;
		}
		return false;
	}
}
