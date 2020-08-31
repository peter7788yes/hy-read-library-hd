using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[CompilerGenerated]
internal sealed class n
{
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 72)]
	private struct a
	{
	}

	internal static readonly a a/* Not supported: data(00 00 00 00 00 00 F0 3F 00 00 00 00 00 00 F4 3F 00 00 00 00 00 00 F8 3F 00 00 00 00 00 00 FC 3F 00 00 00 00 00 00 00 40 00 00 00 00 00 00 02 40 00 00 00 00 00 00 04 40 00 00 00 00 00 00 06 40 00 00 00 00 00 00 08 40) */;

	internal static uint a(string A_0)
	{
		uint num = default(uint);
		if (A_0 != null)
		{
			num = 2166136261u;
			for (int i = 0; i < A_0.Length; i++)
			{
				num = (A_0[i] ^ num) * 16777619;
			}
		}
		return num;
	}
}
