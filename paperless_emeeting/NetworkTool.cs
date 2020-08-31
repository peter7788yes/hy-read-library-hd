using Network;
using PaperLess_Emeeting.Properties;
using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

public class NetworkTool
{
	private delegate IPHostEntry GetHostEntryHandler(string DomainName);

	[CompilerGenerated]
	private static Func<IPAddress, bool> CS_0024_003C_003E9__CachedAnonymousMethodDelegate1;

	[CompilerGenerated]
	private static Func<IPAddress, bool> CS_0024_003C_003E9__CachedAnonymousMethodDelegate3;

	public static int CheckNetwork()
	{
		if (!Settings.Default.HasOfflineMode)
		{
			return 1;
		}
		try
		{
			HttpRequest httpRequest = new HttpRequest();
			string text = "";
			string text2 = "";
			text = WsTool.GetUrl();
			text2 = new Uri(text).DnsSafeHost;
			if (!GetDomainNameIP(text2, 1000).Equals("") && httpRequest.checkNetworkStatus("", text) == NetworkStatusCode.OK)
			{
				return 1;
			}
			return 0;
		}
		catch (Exception ex)
		{
			LogTool.Debug(ex);
			return 1;
		}
	}

	public static void GetNetWork()
	{
		NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
		NetworkInterface[] array = allNetworkInterfaces;
		foreach (NetworkInterface networkInterface in array)
		{
			if (networkInterface.OperationalStatus == OperationalStatus.Up)
			{
				Console.WriteLine("1");
			}
			else
			{
				Console.WriteLine("2");
			}
		}
	}

	public static bool CheckNetWork()
	{
		return NetworkInterface.GetIsNetworkAvailable();
	}

	public static IPAddress LAN_IPAddress()
	{
		if (!CheckNetWork())
		{
			return null;
		}
		IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
		IPAddress[] addressList = hostEntry.AddressList;
		if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate1 == null)
		{
			CS_0024_003C_003E9__CachedAnonymousMethodDelegate1 = new Func<IPAddress, bool>(_003CLAN_IPAddress_003Eb__0);
		}
		return Enumerable.FirstOrDefault(addressList, CS_0024_003C_003E9__CachedAnonymousMethodDelegate1);
	}

	public static IPAddress WAN_IPAddress()
	{
		if (!CheckNetWork())
		{
			return null;
		}
		IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
		IPAddress[] addressList = hostEntry.AddressList;
		if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate3 == null)
		{
			CS_0024_003C_003E9__CachedAnonymousMethodDelegate3 = new Func<IPAddress, bool>(_003CWAN_IPAddress_003Eb__2);
		}
		return Enumerable.FirstOrDefault(addressList, CS_0024_003C_003E9__CachedAnonymousMethodDelegate3);
	}

	public static string GetDomainNameIP(string DomainName, int timeout)
	{
		try
		{
			GetHostEntryHandler getHostEntryHandler = new GetHostEntryHandler(Dns.GetHostEntry);
			IAsyncResult asyncResult = getHostEntryHandler.BeginInvoke(new Uri(DomainName).DnsSafeHost, null, null);
			if (asyncResult.AsyncWaitHandle.WaitOne(timeout, false))
			{
				IPAddress iPAddress = Enumerable.FirstOrDefault(getHostEntryHandler.EndInvoke(asyncResult).AddressList);
				if (iPAddress != null)
				{
					return iPAddress.ToString();
				}
				return "";
			}
			return "";
		}
		catch (Exception)
		{
			return "";
		}
	}

	public static string GetReverseDNS(string ip, int timeout)
	{
		try
		{
			GetHostEntryHandler getHostEntryHandler = new GetHostEntryHandler(Dns.GetHostEntry);
			IAsyncResult asyncResult = getHostEntryHandler.BeginInvoke(ip, null, null);
			if (asyncResult.AsyncWaitHandle.WaitOne(timeout, false))
			{
				return getHostEntryHandler.EndInvoke(asyncResult).HostName;
			}
			return ip;
		}
		catch (Exception)
		{
			return ip;
		}
	}

	[CompilerGenerated]
	private static bool _003CLAN_IPAddress_003Eb__0(IPAddress ip)
	{
		if (ip.AddressFamily == AddressFamily.InterNetwork)
		{
			return !ip.ToString().StartsWith("169");
		}
		return false;
	}

	[CompilerGenerated]
	private static bool _003CWAN_IPAddress_003Eb__2(IPAddress ip)
	{
		if (ip.AddressFamily != AddressFamily.InterNetwork)
		{
			return !ip.ToString().StartsWith("169");
		}
		return false;
	}
}
