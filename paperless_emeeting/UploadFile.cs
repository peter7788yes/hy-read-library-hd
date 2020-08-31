using System.IO;
using System.Runtime.CompilerServices;

public class UploadFile
{
	[CompilerGenerated]
	private string _003CName_003Ek__BackingField;

	[CompilerGenerated]
	private string _003CFilename_003Ek__BackingField;

	[CompilerGenerated]
	private string _003CContentType_003Ek__BackingField;

	[CompilerGenerated]
	private Stream _003CStream_003Ek__BackingField;

	public string Name
	{
		[CompilerGenerated]
		get
		{
			return _003CName_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CName_003Ek__BackingField = value;
		}
	}

	public string Filename
	{
		[CompilerGenerated]
		get
		{
			return _003CFilename_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CFilename_003Ek__BackingField = value;
		}
	}

	public string ContentType
	{
		[CompilerGenerated]
		get
		{
			return _003CContentType_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CContentType_003Ek__BackingField = value;
		}
	}

	public Stream Stream
	{
		[CompilerGenerated]
		get
		{
			return _003CStream_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CStream_003Ek__BackingField = value;
		}
	}

	public UploadFile()
	{
		ContentType = "application/octet-stream";
	}
}
