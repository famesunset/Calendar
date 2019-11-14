using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace Data.Models
{
    public static class ExtentionsMethods
    {
        public static DataTable ConvertToDatatable<T>(this List<T> data, string columnName = null)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            if(props.Count.Equals(0) && data.Count > 0 && data[0] is int && columnName != null)
            {
                table.Columns.Add(columnName, typeof(int));
                foreach (var item in data)
                {
                    table.Rows.Add(item);
                }
            }
            else
            {
                for (int i = 0; i < props.Count; i++)
                {
                    PropertyDescriptor prop = props[i];
                    if (prop.PropertyType.IsGenericType &&
                        prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        table.Columns.Add(prop.Name, prop.PropertyType.GetGenericArguments()[0]);
                    else
                        table.Columns.Add(prop.Name, prop.PropertyType);
                }

                object[] values = new object[props.Count];
                foreach (T item in data)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = props[i].GetValue(item);
                    }

                    table.Rows.Add(values);
                }
            }
            
            return table;
        }
    }
}
