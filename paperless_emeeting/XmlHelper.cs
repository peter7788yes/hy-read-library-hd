using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

public static class XmlHelper
{
	private const int POOL_BUFFER_SIZE = 25;

	private static Dictionary<Type, XmlSerializer> _pool;

	private static Dictionary<Type, XmlSerializer> m_Pool
	{
		get
		{
			if (_pool == null)
			{
				_pool = new Dictionary<Type, XmlSerializer>(25);
			}
			return _pool;
		}
	}

	private static void XmlSerializeInternal(Stream stream, object o, Encoding encoding)
	{
		if (o == null)
		{
			throw new ArgumentNullException("o");
		}
		if (encoding == null)
		{
			throw new ArgumentNullException("encoding");
		}
		XmlSerializer xmlSerializer = new XmlSerializer(o.GetType());
		XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
		xmlWriterSettings.Indent = true;
		xmlWriterSettings.NewLineChars = "\r\n";
		xmlWriterSettings.Encoding = encoding;
		xmlWriterSettings.IndentChars = "    ";
		using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
		{
			xmlSerializer.Serialize(xmlWriter, o);
			xmlWriter.Close();
		}
	}

	public static string XmlSerialize(object o, Encoding encoding)
	{
		using (MemoryStream memoryStream = new MemoryStream())
		{
			XmlSerializeInternal(memoryStream, o, encoding);
			memoryStream.Position = 0L;
			using (StreamReader streamReader = new StreamReader(memoryStream, encoding))
			{
				return streamReader.ReadToEnd();
			}
		}
	}

	public static void XmlSerializeToFile(object o, string path, Encoding encoding)
	{
		if (string.IsNullOrEmpty(path))
		{
			throw new ArgumentNullException("path");
		}
		using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write))
		{
			XmlSerializeInternal(stream, o, encoding);
		}
	}

	public static T XmlDeserialize<T>(string s, Encoding encoding)
	{
		if (string.IsNullOrEmpty(s))
		{
			throw new ArgumentNullException("s");
		}
		if (encoding == null)
		{
			throw new ArgumentNullException("encoding");
		}
		XmlSerializer xmlSerializer = GetXmlSerializer(typeof(T));
		using (MemoryStream stream = new MemoryStream(encoding.GetBytes(s)))
		{
			using (StreamReader textReader = new StreamReader(stream, encoding))
			{
				return (T)xmlSerializer.Deserialize(textReader);
			}
		}
	}

	public static T XmlDeserializeFromFile<T>(string path, Encoding encoding)
	{
		if (string.IsNullOrEmpty(path))
		{
			throw new ArgumentNullException("path");
		}
		if (encoding == null)
		{
			throw new ArgumentNullException("encoding");
		}
		string s = File.ReadAllText(path, encoding);
		return XmlDeserialize<T>(s, encoding);
	}

	private static XmlSerializer GetXmlSerializerUsedType(object obj)
	{
		Type type = obj.GetType();
		string location = type.Assembly.Location;
		string path = location.Substring(0, location.LastIndexOf(".")) + ".XmlSerializers.dll";
		if (File.Exists(path))
		{
			Type type2 = Assembly.LoadFile(path).GetType(string.Format("Microsoft.Xml.Serialization.GeneratedAssembly.{0}Serializer", type.Name));
			if (type2 != null)
			{
				return (XmlSerializer)Activator.CreateInstance(type2);
			}
		}
		return new XmlSerializer(type);
	}

	public static XmlSerializer GetXmlSerializer(Type objType)
	{
		lock (m_Pool)
		{
			if (!m_Pool.ContainsKey(objType))
			{
				string location = objType.Assembly.Location;
				string path = location.Substring(0, location.LastIndexOf(".")) + ".XmlSerializers.dll";
				if (File.Exists(path))
				{
					Type type = Assembly.LoadFile(path).GetType(string.Format("Microsoft.Xml.Serialization.GeneratedAssembly.{0}Serializer", objType.Name));
					m_Pool.Add(objType, (type == null) ? new XmlSerializer(objType) : ((XmlSerializer)Activator.CreateInstance(type)));
				}
				else
				{
					m_Pool.Add(objType, new XmlSerializer(objType));
				}
			}
		}
		return m_Pool[objType];
	}
}
