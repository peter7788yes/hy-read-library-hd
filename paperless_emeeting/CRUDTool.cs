using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CRUDTool
{
	public static int Insert(string tbName, Dictionary<string, object> dict)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("insert into {0} (", tbName);
		string text = "";
		string str = "values(";
		foreach (string key in dict.Keys)
		{
			text = text + key + ",";
			str = str + "@" + key + ",";
		}
		text = text.Trim().TrimEnd(',') + ")";
		str = text.Trim().TrimEnd(',') + ")";
		stringBuilder.Append(text);
		stringBuilder.Append(str);
		return MSCE.ExecuteNonQuery(stringBuilder.ToString(), Enumerable.ToArray(Enumerable.Cast<string>(dict.Values)));
	}

	public static int Update(string tbName, string PKField, Dictionary<string, object> dict)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("update {0} set", tbName);
		string text = "";
		string text2 = "";
		foreach (string key in dict.Keys)
		{
			if (!string.Equals(key, PKField, StringComparison.OrdinalIgnoreCase))
			{
				text += string.Format("{0}=@{0},", key);
			}
		}
		text2 = string.Format(" where {0}= @{0}", PKField);
		stringBuilder.Append(text.Trim().TrimEnd(','));
		stringBuilder.Append(text2);
		return MSCE.ExecuteNonQuery(stringBuilder.ToString(), Enumerable.ToArray(Enumerable.Concat(Enumerable.Cast<string>(dict.Values), new string[1]
		{
			PKField
		})));
	}

	public int Delete(string tbName, string condition)
	{
		string sQL = string.Format("delete from [{0}] where {1}", tbName, condition);
		return MSCE.ExecuteNonQuery(sQL);
	}
}
