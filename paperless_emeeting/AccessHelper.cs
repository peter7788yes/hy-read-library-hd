using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;

public static class AccessHelper
{
	public static readonly string conn = "";

	private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

	public static int ExecuteNonQuery(string connectionString, string cmdText, params OleDbParameter[] commandParameters)
	{
		OleDbCommand oleDbCommand = new OleDbCommand();
		using (OleDbConnection oleDbConnection = new OleDbConnection(connectionString))
		{
			PrepareCommand(oleDbCommand, oleDbConnection, null, cmdText, commandParameters);
			int result = oleDbCommand.ExecuteNonQuery();
			oleDbCommand.Parameters.Clear();
			return result;
		}
	}

	public static int ExecuteNonQuery(OleDbConnection connection, string cmdText, params OleDbParameter[] commandParameters)
	{
		OleDbCommand oleDbCommand = new OleDbCommand();
		PrepareCommand(oleDbCommand, connection, null, cmdText, commandParameters);
		int result = oleDbCommand.ExecuteNonQuery();
		oleDbCommand.Parameters.Clear();
		return result;
	}

	public static int ExecuteNonQuery(OleDbTransaction trans, string cmdText, params OleDbParameter[] commandParameters)
	{
		OleDbCommand oleDbCommand = new OleDbCommand();
		PrepareCommand(oleDbCommand, trans.Connection, trans, cmdText, commandParameters);
		int result = oleDbCommand.ExecuteNonQuery();
		oleDbCommand.Parameters.Clear();
		return result;
	}

	public static OleDbDataReader ExecuteReader(string connectionString, string cmdText, params OleDbParameter[] commandParameters)
	{
		OleDbCommand oleDbCommand = new OleDbCommand();
		OleDbConnection oleDbConnection = new OleDbConnection(connectionString);
		try
		{
			PrepareCommand(oleDbCommand, oleDbConnection, null, cmdText, commandParameters);
			OleDbDataReader result = oleDbCommand.ExecuteReader(CommandBehavior.CloseConnection);
			oleDbCommand.Parameters.Clear();
			return result;
		}
		catch
		{
			oleDbConnection.Close();
			throw;
		}
	}

	public static DataSet ExecuteDataSet(string connectionString, string cmdText, params OleDbParameter[] commandParameters)
	{
		OleDbCommand oleDbCommand = new OleDbCommand();
		using (OleDbConnection oleDbConnection = new OleDbConnection(connectionString))
		{
			PrepareCommand(oleDbCommand, oleDbConnection, null, cmdText, commandParameters);
			OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand);
			DataSet dataSet = new DataSet();
			try
			{
				oleDbDataAdapter.Fill(dataSet);
				oleDbCommand.Parameters.Clear();
				return dataSet;
			}
			catch
			{
				oleDbConnection.Close();
				throw;
			}
		}
	}

	public static object ExecuteScalar(string connectionString, string cmdText, params OleDbParameter[] commandParameters)
	{
		OleDbCommand oleDbCommand = new OleDbCommand();
		using (OleDbConnection oleDbConnection = new OleDbConnection(connectionString))
		{
			PrepareCommand(oleDbCommand, oleDbConnection, null, cmdText, commandParameters);
			object result = oleDbCommand.ExecuteScalar();
			oleDbCommand.Parameters.Clear();
			return result;
		}
	}

	public static object ExecuteScalar(OleDbConnection connection, string cmdText, params OleDbParameter[] commandParameters)
	{
		OleDbCommand oleDbCommand = new OleDbCommand();
		PrepareCommand(oleDbCommand, connection, null, cmdText, commandParameters);
		object result = oleDbCommand.ExecuteScalar();
		oleDbCommand.Parameters.Clear();
		return result;
	}

	public static void CacheParameters(string cacheKey, params OleDbParameter[] commandParameters)
	{
		parmCache[cacheKey] = commandParameters;
	}

	public static OleDbParameter[] GetCachedParameters(string cacheKey)
	{
		OleDbParameter[] array = (OleDbParameter[])parmCache[cacheKey];
		if (array == null)
		{
			return null;
		}
		OleDbParameter[] result = new OleDbParameter[array.Length];
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			result = (OleDbParameter[])((ICloneable)array).Clone();
		}
		return result;
	}

	private static void PrepareCommand(OleDbCommand cmd, OleDbConnection conn, OleDbTransaction trans, string cmdText, OleDbParameter[] cmdParms)
	{
		if (conn.State != ConnectionState.Open)
		{
			conn.Open();
		}
		cmd.Connection = conn;
		cmd.CommandText = cmdText;
		if (trans != null)
		{
			cmd.Transaction = trans;
		}
		cmd.CommandType = CommandType.Text;
		if (cmdParms != null)
		{
			foreach (OleDbParameter value in cmdParms)
			{
				cmd.Parameters.Add(value);
			}
		}
	}
}
