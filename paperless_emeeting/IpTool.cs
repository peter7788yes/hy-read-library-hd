using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

public class IpTool
{
	[CompilerGenerated]
	private static Func<IPAddress, bool> CS_0024_003C_003E9__CachedAnonymousMethodDelegate1;

	[CompilerGenerated]
	private static Func<IPAddress, bool> CS_0024_003C_003E9__CachedAnonymousMethodDelegate3;

	public static bool CheckNetWork()
	{
		return NetworkInterface.GetIsNetworkAvailable();
	}

	public static IPAddress LocalIPAddress()
	{
		if (!CheckNetWork())
		{
			return null;
		}
		IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
		IPAddress[] addressList = hostEntry.AddressList;
		if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate1 == null)
		{
			CS_0024_003C_003E9__CachedAnonymousMethodDelegate1 = new Func<IPAddress, bool>(_003CLocalIPAddress_003Eb__0);
		}
		return Enumerable.FirstOrDefault(addressList, CS_0024_003C_003E9__CachedAnonymousMethodDelegate1);
	}

	public static string LocalIPAddressString()
	{
		if (!NetworkInterface.GetIsNetworkAvailable())
		{
			return "";
		}
		IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
		IPAddress[] addressList = hostEntry.AddressList;
		if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate3 == null)
		{
			CS_0024_003C_003E9__CachedAnonymousMethodDelegate3 = new Func<IPAddress, bool>(_003CLocalIPAddressString_003Eb__2);
		}
		return Enumerable.FirstOrDefault(addressList, CS_0024_003C_003E9__CachedAnonymousMethodDelegate3).ToString();
	}

	public static IPAddress[] GetIPs()
	{
		if (!NetworkInterface.GetIsNetworkAvailable())
		{
			return null;
		}
		IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
		return hostEntry.AddressList;
	}

	public static bool CheckInNowWifi(string Allow_IP_Range)
	{
		bool result = false;
		IPAddress[] iPs = GetIPs();
		string[] array = Allow_IP_Range.Split(new char[1]
		{
			';'
		}, StringSplitOptions.RemoveEmptyEntries);
		string[] array2 = array;
		foreach (string text in array2)
		{
			string[] array3 = text.Split(new char[1]
			{
				'-'
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array3.Length < 2)
			{
				if (iPs == null)
				{
					return false;
				}
				IPAddress[] array4 = iPs;
				foreach (IPAddress iPAddress in array4)
				{
					if (iPAddress.ToString().Equals(array3[0]))
					{
						result = true;
						return true;
					}
				}
				continue;
			}
			if (iPs == null)
			{
				return false;
			}
			IPAddress[] array5 = iPs;
			foreach (IPAddress iPAddress2 in array5)
			{
				if (IsIpInRange(iPAddress2.ToString(), array3[0], array3[1]))
				{
					result = true;
					return true;
				}
			}
		}
		return result;
	}

	public static bool IsIpInRange(string ip, string ipStart, string ipEnd)
	{
		IPAddress iPAddress = IPAddress.Parse(ip);
		IPAddress iPAddress2 = IPAddress.Parse(ipStart);
		IPAddress iPAddress3 = IPAddress.Parse(ipEnd);
		byte[] value = Enumerable.ToArray(Enumerable.Reverse(iPAddress.GetAddressBytes()));
		byte[] value2 = Enumerable.ToArray(Enumerable.Reverse(iPAddress2.GetAddressBytes()));
		byte[] value3 = Enumerable.ToArray(Enumerable.Reverse(iPAddress3.GetAddressBytes()));
		uint num = BitConverter.ToUInt32(value, 0);
		uint num2 = BitConverter.ToUInt32(value2, 0);
		uint num3 = BitConverter.ToUInt32(value3, 0);
		if (num >= num2)
		{
			return num <= num3;
		}
		return false;
	}

	[CompilerGenerated]
	private static bool _003CLocalIPAddress_003Eb__0(IPAddress ip)
	{
		if (ip.AddressFamily == AddressFamily.InterNetwork)
		{
			return !ip.ToString().StartsWith("169");
		}
		return false;
	}

	[CompilerGenerated]
	private static bool _003CLocalIPAddressString_003Eb__2(IPAddress ip)
	{
		if (ip.AddressFamily == AddressFamily.InterNetwork)
		{
			return !ip.ToString().StartsWith("169");
		}
		return false;
	}
}
