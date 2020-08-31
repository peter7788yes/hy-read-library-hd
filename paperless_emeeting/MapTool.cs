using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

public class MapTool
{
	public static void FillEntity(object model, DataRow dr)
	{
		if (model != null)
		{
			Type type = model.GetType();
			if (dr != null)
			{
				foreach (DataColumn column in dr.Table.Columns)
				{
					string columnName = column.ColumnName;
					if (dr[columnName] != null)
					{
						PropertyInfo property = type.GetProperty(columnName);
						if (property != null)
						{
							Type propertyType = type.GetProperty(columnName).PropertyType;
							if (dr[columnName] is DBNull)
							{
								property.SetValue(model, null, null);
							}
							else
							{
								property.SetValue(model, Convert.ChangeType(dr[columnName], propertyType), null);
							}
						}
					}
				}
			}
		}
	}

	public static void FillEntity<T>(List<T> list, DataTable dt)
	{
		if (list == null)
		{
			list = new List<T>();
		}
		foreach (DataRow row in dt.Rows)
		{
			T val = (T)typeof(T).GetConstructor(Type.EmptyTypes).Invoke(null);
			FillEntity(val, row);
			list.Add(val);
		}
	}
}
