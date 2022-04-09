﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace SecureGate.Framework.Serializer
{
    public class ConvertListToDataTable
    {
        public static DataTable ToDataTable<T>(List<T> items)
        {

            DataTable dataTable = new DataTable(typeof(T).Name);
            try
            {
                

                //Get all the properties

                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }

                foreach (T item in items)
                {

                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        values[i] = Props[i].GetValue(item, null);
                    }

                    dataTable.Rows.Add(values);
                }

            }
            catch (Exception ex)
            {
                //ErrorLog.ProcessException(ex);
                //throw;
            }
            
            return dataTable;
        }
    }
}
