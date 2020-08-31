using System.IO;
using System.IO.Compression;
using System.Threading;

public class GZipTool
{
	public static int seeder;

	public static void CompressToFile(byte[] input)
	{
		string path = string.Format("{0}.zip", Interlocked.Increment(ref seeder));
		using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write))
		{
			using (GZipStream gZipStream = new GZipStream(stream, CompressionMode.Compress))
			{
				gZipStream.Write(input, 0, input.Length);
			}
		}
	}

	public static void CompressToMemory(byte[] input)
	{
		byte[] buffer = new byte[input.Length];
		using (MemoryStream stream = new MemoryStream(buffer))
		{
			using (GZipStream gZipStream = new GZipStream(stream, CompressionMode.Compress))
			{
				gZipStream.Write(buffer, 0, input.Length);
			}
		}
	}

	public static byte[] GetMemoryStream(string inputFile)
	{
		using (FileStream fileStream = new FileStream(inputFile, FileMode.Open))
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				memoryStream.SetLength(fileStream.Length);
				fileStream.Read(memoryStream.GetBuffer(), 0, (int)fileStream.Length);
				fileStream.Flush();
				return memoryStream.ToArray();
			}
		}
	}
}
