using PaperLess_Emeeting.App_Code.ClickOnce;
using PaperLess_Emeeting.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;

public class MSCE
{
	private static string Conn = string.Format("Data Source = '{0}';Password = '{1}';", Path.Combine(ClickOnceTool.GetDataPath(), Settings.Default.PaperLessDB_Path), "hywebhdlw");

	private static char[] SplitChars = new char[12]
	{
		' ',
		',',
		'(',
		')',
		';',
		'=',
		'+',
		'\'',
		'+',
		'\r',
		'\n',
		'\t'
	};

	public static string ExecuteScalar(string SQL, params string[] parameters)
	{
		string text = "";
		try
		{
			return ExecuteScalarWithConn(Conn, SQL, parameters);
		}
		catch
		{
			throw;
		}
	}

	public static string ExecuteScalarWithConn(string ConnectionString, string SQL, params string[] parameters)
	{
		string result = "";
		try
		{
			List<string> parameters2 = GetParameters(SQL);
			using (SqlCeConnection sqlCeConnection = new SqlCeConnection(ConnectionString))
			{
				using (SqlCeCommand sqlCeCommand = new SqlCeCommand(SQL, sqlCeConnection))
				{
					for (int i = 0; i <= parameters2.Count - 1; i++)
					{
						sqlCeCommand.Parameters.AddWithValue(parameters2[i], parameters[i]);
					}
					sqlCeConnection.Open();
					object obj = sqlCeCommand.ExecuteScalar();
					if (obj != null)
					{
						return obj.ToString();
					}
					return result;
				}
			}
		}
		catch
		{
			throw;
		}
	}

	public static int ExecuteNonQuery(string SQL, params string[] parameters)
	{
		int num = 0;
		try
		{
			return ExecuteNonQueryWithConn(Conn, SQL, parameters);
		}
		catch (Exception)
		{
			throw;
		}
	}

	public static int ExecuteNonQueryWithConn(string ConnectionString, string SQL, params string[] parameters)
	{
		int num = 0;
		try
		{
			List<string> parameters2 = GetParameters(SQL);
			using (SqlCeConnection sqlCeConnection = new SqlCeConnection(ConnectionString))
			{
				using (SqlCeCommand sqlCeCommand = new SqlCeCommand(SQL, sqlCeConnection))
				{
					for (int i = 0; i <= parameters2.Count - 1; i++)
					{
						sqlCeCommand.Parameters.AddWithValue(parameters2[i], parameters[i]);
					}
					sqlCeConnection.Open();
					return sqlCeCommand.ExecuteNonQuery();
				}
			}
		}
		catch
		{
			throw;
		}
	}

	public static DataTable GetDataTable(string SQL, params string[] parameters)
	{
		DataTable dataTable = new DataTable();
		try
		{
			return GetDataTableWithConn(Conn, SQL, parameters);
		}
		catch (Exception)
		{
			throw;
		}
	}

	public static DataTable GetDataTableWithConn(string ConnectionString, string SQL, params string[] parameters)
	{
		DataTable dataTable = new DataTable();
		try
		{
			List<string> parameters2 = GetParameters(SQL);
			using (SqlCeConnection sqlCeConnection = new SqlCeConnection(ConnectionString))
			{
				using (SqlCeCommand sqlCeCommand = new SqlCeCommand(SQL, sqlCeConnection))
				{
					for (int i = 0; i <= parameters2.Count - 1; i++)
					{
						sqlCeCommand.Parameters.AddWithValue(parameters2[i], parameters[i]);
					}
					string commandText = sqlCeCommand.CommandText;
					using (SqlCeDataAdapter sqlCeDataAdapter = new SqlCeDataAdapter(sqlCeCommand))
					{
						sqlCeConnection.Open();
						sqlCeDataAdapter.Fill(dataTable);
						return dataTable;
					}
				}
			}
		}
		catch (Exception ex)
		{
			string message = ex.Message;
			throw;
		}
	}

	public static DataSet GetDataSet(string SQL, params string[] parameters)
	{
		DataSet dataSet = new DataSet();
		try
		{
			return GetDataSetWithConn(Conn, SQL, parameters);
		}
		catch (Exception)
		{
			throw;
		}
	}

	public static DataSet GetDataSetWithConn(string ConnectionString, string SQL, params string[] parameters)
	{
		DataSet dataSet = new DataSet();
		try
		{
			List<string> parameters2 = GetParameters(SQL);
			using (SqlCeConnection sqlCeConnection = new SqlCeConnection(ConnectionString))
			{
				using (SqlCeCommand sqlCeCommand = new SqlCeCommand(SQL, sqlCeConnection))
				{
					for (int i = 0; i <= parameters2.Count - 1; i++)
					{
						sqlCeCommand.Parameters.AddWithValue(parameters2[i], parameters[i]);
					}
					using (SqlCeDataAdapter sqlCeDataAdapter = new SqlCeDataAdapter(sqlCeCommand))
					{
						sqlCeConnection.Open();
						sqlCeDataAdapter.Fill(dataSet);
						return dataSet;
					}
				}
			}
		}
		catch
		{
			throw;
		}
	}

	public static bool TransactionSQLs(List<string> SQLs, params string[] parameters)
	{
		bool flag = false;
		try
		{
			return TransactionSQLsWithConn(Conn, SQLs, parameters);
		}
		catch
		{
			throw;
		}
	}

	public static bool TransactionSQLsWithConn(string ConnectionString, List<string> SQLs, params string[] parameters)
	{
		bool flag = false;
		try
		{
			List<string[]> list = new List<string[]>();
			foreach (string SQL in SQLs)
			{
				list.Add(SQL.Split(SplitChars, StringSplitOptions.RemoveEmptyEntries));
			}
			List<List<string>> list2 = new List<List<string>>();
			foreach (string[] item in list)
			{
				List<string> list3 = new List<string>();
				string[] array = item;
				foreach (string text in array)
				{
					if (!text.Contains("@@") && text.Contains("@"))
					{
						list3.Add(text.Substring(text.IndexOf("@"), text.Length - text.IndexOf("@")));
					}
				}
				list2.Add(list3);
			}
			using (SqlCeConnection sqlCeConnection = new SqlCeConnection(ConnectionString))
			{
				sqlCeConnection.Open();
				SqlCeTransaction sqlCeTransaction = sqlCeConnection.BeginTransaction();
				int num = 0;
				string value = "";
				try
				{
					for (int j = 0; j <= SQLs.Count - 1; j++)
					{
						SqlCeCommand sqlCeCommand = new SqlCeCommand(SQLs[j], sqlCeConnection);
						sqlCeCommand.Transaction = sqlCeTransaction;
						sqlCeCommand.Parameters.Clear();
						List<string> list4 = list2[j];
						for (int k = 0; k <= list4.Count - 1; k++)
						{
							if (j != 0 && k == 0)
							{
								sqlCeCommand.Parameters.AddWithValue(list4[k], value);
							}
							else
							{
								sqlCeCommand.Parameters.AddWithValue(list4[k], parameters[num]);
							}
							num++;
						}
						if (SQLs[j].Contains("@@"))
						{
							DataTable dataTable = new DataTable();
							using (SqlCeDataAdapter sqlCeDataAdapter = new SqlCeDataAdapter(sqlCeCommand))
							{
								sqlCeDataAdapter.Fill(dataTable);
							}
							if (dataTable.Rows.Count > 0)
							{
								value = dataTable.Rows[0][0].ToString();
							}
						}
						else
						{
							sqlCeCommand.ExecuteNonQuery();
						}
					}
					sqlCeTransaction.Commit();
					return true;
				}
				catch
				{
					sqlCeTransaction.Rollback();
					flag = false;
					throw;
				}
			}
		}
		catch
		{
			flag = false;
			throw;
		}
	}

	public static bool StartTransaction(string SQL, DataTable dt)
	{
		bool flag = false;
		try
		{
			return StartTransactionWithConn(Conn, SQL, dt);
		}
		catch
		{
			throw;
		}
	}

	public static bool StartTransactionWithConn(string ConnectionString, string SQL, DataTable dt)
	{
		bool flag = false;
		try
		{
			List<string> parameters = GetParameters(SQL);
			using (SqlCeConnection sqlCeConnection = new SqlCeConnection(ConnectionString))
			{
				sqlCeConnection.Open();
				SqlCeTransaction sqlCeTransaction = sqlCeConnection.BeginTransaction();
				try
				{
					SqlCeCommand sqlCeCommand = new SqlCeCommand(SQL, sqlCeConnection);
					sqlCeCommand.Transaction = sqlCeTransaction;
					foreach (DataRow row in dt.Rows)
					{
						sqlCeCommand.Parameters.Clear();
						for (int i = 0; i <= parameters.Count - 1; i++)
						{
							sqlCeCommand.Parameters.AddWithValue(parameters[i], row[i]);
						}
						sqlCeCommand.ExecuteNonQuery();
					}
					sqlCeTransaction.Commit();
					return true;
				}
				catch
				{
					sqlCeTransaction.Rollback();
					flag = false;
					throw;
				}
			}
		}
		catch
		{
			flag = false;
			throw;
		}
	}

	public static int insertTable(string TableName, params string[] parameters)
	{
		int num = 0;
		try
		{
			return insertTableWithConn(Conn, TableName, parameters);
		}
		catch
		{
			throw;
		}
	}

	public static int insertTableWithConn(string ConnectionString, string TableName, params string[] parameters)
	{
		int result = 0;
		try
		{
			int num = parameters.Length / 2 - 1;
			string text = string.Format("insert into {0} (", TableName);
			for (int i = 0; i <= num; i++)
			{
				text += string.Format("{0},", parameters[i].TrimStart('@'));
			}
			text = text.TrimEnd(',') + ") values(";
			List<string> list = new List<string>();
			for (int j = num + 1; j <= parameters.Length - 1; j++)
			{
				text += string.Format("@{0},", parameters[j]);
				list.Add(parameters[j]);
			}
			text = text.TrimEnd(',') + ")";
			ExecuteNonQueryWithConn(ConnectionString, text, list.ToArray());
			return result;
		}
		catch
		{
			throw;
		}
	}

	public static int updateTable(string TableName, Dictionary<string, string> whereDict, params string[] parameters)
	{
		int num = 0;
		try
		{
			return updateTableWithConn(Conn, TableName, whereDict, parameters);
		}
		catch
		{
			throw;
		}
	}

	public static int updateTableWithConn(string ConnectionString, string TableName, Dictionary<string, string> whereDict, params string[] parameters)
	{
		int result = 0;
		if (!CheckParam(parameters))
		{
			throw new Exception("請檢查，參數有錯誤。");
		}
		try
		{
			int num = parameters.Length / 2;
			int num2 = num - 1;
			string text = string.Format("update {0} set ", TableName);
			for (int i = 0; i <= num2; i++)
			{
				text += string.Format("{0}=#{1},", parameters[i].TrimStart('@'), i);
			}
			text = string.Format("{0} where ", text.TrimEnd(','));
			List<string> list = new List<string>();
			for (int j = num2 + 1; j <= parameters.Length - 1; j++)
			{
				text = text.Replace(string.Format("#{0}", j - num), "@" + parameters[j]);
				list.Add(parameters[j]);
			}
			foreach (string key in whereDict.Keys)
			{
				string text2 = key.TrimStart('@');
				text = ((!text2.Contains("(in)")) ? (text + string.Format("{0}=@{1}  and ", text2, whereDict[key])) : (text + string.Format("{0} in ({1}) and ", text2.Substring(0, text2.Length - 4), whereDict[key])));
				list.Add(whereDict[key]);
				text = text.Substring(0, text.Length - 4);
			}
			ExecuteNonQueryWithConn(ConnectionString, text, list.ToArray());
			return result;
		}
		catch
		{
			throw;
		}
	}

	public static int deleteTable(string TableName, Dictionary<string, string> whereDict, params string[] parameters)
	{
		int num = 0;
		try
		{
			return deleteTableWithConn(Conn, TableName, whereDict);
		}
		catch
		{
			throw;
		}
	}

	public static int deleteTableWithConn(string ConnectionString, string TableName, Dictionary<string, string> whereDict)
	{
		int result = 0;
		try
		{
			string text = string.Format("delete {0}  where ", TableName);
			List<string> list = new List<string>();
			foreach (string key in whereDict.Keys)
			{
				string text2 = key.TrimStart('@');
				text = ((!text2.Contains("(in)")) ? (text + string.Format("{0}=@{1}  and ", text2, whereDict[key])) : (text + string.Format("{0} in ({1}) and ", text2.Substring(0, text2.Length - 4), whereDict[key])));
				list.Add(whereDict[key]);
				text = text.Substring(0, text.Length - 4);
			}
			ExecuteNonQueryWithConn(ConnectionString, text, list.ToArray());
			return result;
		}
		catch
		{
			throw;
		}
	}

	public static DataTable selectTable(string TableName, Dictionary<string, string> selectDict = null, Dictionary<string, string> whereDict = null)
	{
		DataTable dataTable = new DataTable();
		try
		{
			return selectTableWithConn(Conn, TableName, selectDict, whereDict);
		}
		catch
		{
			throw;
		}
	}

	public static DataTable selectTableWithConn(string ConnectionString, string TableName, Dictionary<string, string> selectDict = null, Dictionary<string, string> whereDict = null)
	{
		DataTable dataTable = new DataTable();
		try
		{
			string text = "select ";
			if (selectDict != null)
			{
				foreach (string key in selectDict.Keys)
				{
					text = ((!selectDict[key].Trim().Equals("")) ? (text + string.Format(" {0} as '{1}',", key.TrimStart('@'), selectDict[key])) : (text + string.Format(" {0}, ", key.TrimStart('@'))));
				}
				text = text.TrimEnd(',');
			}
			else
			{
				text += "*";
			}
			text += string.Format(" from {0} ", TableName);
			List<string> list = new List<string>();
			if (whereDict != null)
			{
				text += " where ";
				foreach (string key2 in whereDict.Keys)
				{
					string text2 = key2.TrimStart('@');
					text = ((!text2.Contains("(in)")) ? (text + string.Format("{0}=@{1}  and ", text2, whereDict[key2])) : (text + string.Format("{0} in ({1}) and ", text2.Substring(0, text2.Length - 4), whereDict[key2])));
					list.Add(whereDict[key2]);
				}
				text = text.Substring(0, text.Length - 4);
			}
			return GetDataTableWithConn(ConnectionString, text, list.ToArray());
		}
		catch
		{
			throw;
		}
	}

	public static Dictionary<string, string> GetWhere(params string[] parameters)
	{
		if (!CheckParam(parameters))
		{
			throw new Exception("請檢查，參數有錯誤。");
		}
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		for (int i = 0; i <= parameters.Length - 1; i++)
		{
			if (i % 2 == 0 && i != parameters.Length - 1)
			{
				if (parameters[i].Contains("(in)"))
				{
					string[] value = parameters[i + 1].Split(new char[1]
					{
						','
					}, StringSplitOptions.RemoveEmptyEntries);
					string value2 = "'" + string.Join("','", value) + "'";
					dictionary.Add(parameters[i], value2);
				}
				else
				{
					dictionary.Add(parameters[i], parameters[i + 1]);
				}
			}
		}
		return dictionary;
	}

	public static Dictionary<string, string> GetSelect(params string[] parameters)
	{
		if (!CheckParam(parameters))
		{
			throw new Exception("請檢查，參數有錯誤。");
		}
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		for (int i = 0; i <= parameters.Length - 1; i++)
		{
			if (i % 2 == 0 && i != parameters.Length - 1)
			{
				dictionary.Add(parameters[i], parameters[i + 1]);
			}
		}
		return dictionary;
	}

	private static List<string> GetParameters(string SQL)
	{
		List<string> list = new List<string>();
		string[] array = SQL.Split(SplitChars, StringSplitOptions.RemoveEmptyEntries);
		string[] array2 = array;
		foreach (string text in array2)
		{
			if (!text.Contains("@@") && text.Contains("@"))
			{
				list.Add(text.Substring(text.IndexOf("@"), text.Length - text.IndexOf("@")));
			}
		}
		return new List<string>(Enumerable.Distinct(list));
	}

	private static bool CheckParam(params string[] parameters)
	{
		bool result = true;
		if (parameters.Length % 2 == 1)
		{
			result = false;
		}
		for (int i = 0; i <= parameters.Length - 1; i++)
		{
			if (i % 2 == 0 && !parameters[i].StartsWith("@"))
			{
				result = false;
			}
		}
		return result;
	}
}
