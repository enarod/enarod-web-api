using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace Infopulse.EDemocracy.Data
{
	public static class EnumerableExtensions
	{
		public static DataTable ToDataTable<T>(this IEnumerable<T> list)
		{
			var table = new DataTable();
			
			var properties = TypeDescriptor.GetProperties(typeof(T));
			for (var i = 0; i < properties.Count; i++)
			{
				var property = properties[i];
				table.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
			}

			foreach (var item in list)
			{
				var value = new object[properties.Count];
				var valueIndex = 0;
				for (var i = 0; i < properties.Count; i++)
				{
					value[valueIndex] = properties[i].GetValue(item) ?? DBNull.Value;
					valueIndex++;
				}

				table.Rows.Add(value);
			}

			return table;
		}
	}
}