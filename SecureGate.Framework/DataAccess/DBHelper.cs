using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;

namespace SecureGate.Framework.DataAccess
{
    public static class DBHelper
    {
        /// <summary>
        ///     Execute method is a template method and determines the DB action base on the type of T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        public static T Execute<T>(ref IDbCommand command)
        {
            if (typeof(T) == typeof(DataSet))
            {
                var ds = new DataSet();
                getDataAdapter(ref command).Fill(ds);

                return (T)(object)ds;
            }
            if (typeof(T) == typeof(DataTable))
            {
                var ds = new DataSet();
                getDataAdapter(ref command).Fill(ds);

                return (T)(object)ds.Tables[0];
            }
            if (typeof(T) == typeof(XmlReader))
            {
                var xmlRdr = executeXmlReader(ref command);
                var xmlOutput = String.Empty;
                xmlRdr.Read();

                if (xmlRdr.ReadState != ReadState.EndOfFile)
                {
                    while (xmlRdr.ReadState != ReadState.EndOfFile)
                    {
                        xmlOutput += xmlRdr.ReadOuterXml();
                    }

                    xmlRdr = XmlReader.Create(new StringReader(xmlOutput));
                }
                else
                {
                    xmlRdr = null;
                }

                return (T)(object)xmlRdr;
            }
            if (typeof(T) == typeof(string) || typeof(T) == typeof(Byte[]))
            {
                var obj = command.ExecuteScalar();

                if (obj is DBNull)
                    return default(T);

                return (T)obj;
            }
            if (typeof(T).IsValueType)
            {
                var returnValue = default(T);

                try
                {
                    var sqlReturnValue = command.ExecuteScalar();
                    returnValue = (T)Convert.ChangeType(sqlReturnValue, typeof(T));
                }
                catch (Exception ex)
                {
                    return returnValue;
                }

                return returnValue;
            }
            command.ExecuteNonQuery();
            return default(T);
        }

        /// <summary>
        ///     Execute Xml Reader method available for SQL Client and other .Net provider to retrieve large xmls from the
        ///     database.
        ///     Execute Xml Reader is not available for Odbc and OleDb provider.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static XmlReader executeXmlReader(ref IDbCommand command)
        {
            XmlReader returnValue = null;


            if (command is SqlCommand)
                returnValue = ((SqlCommand)command).ExecuteXmlReader();

            return returnValue;
        }

        public static string GetReader(ref IDbCommand command)
        {
            var sbXml = new StringBuilder();
            SqlDataReader rd;
            rd = (SqlDataReader)command.ExecuteReader();
            while (rd.Read())
            {
                sbXml.Append(rd.GetString(0));
            }
            return sbXml.ToString();
        }

        /// <summary>
        ///     Returns data adapter based on type of command.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private static IDataAdapter getDataAdapter(ref IDbCommand command)
        {
            IDataAdapter returnValue = null;

            if (command is SqlCommand)
                returnValue = new SqlDataAdapter((SqlCommand)command);

            //else if (command is OleDbCommand)
            //    returnValue = new OleDbDataAdapter((OleDbCommand)command);

            //else if (command is OdbcCommand)
            //    returnValue = new OdbcDataAdapter((OdbcCommand)command);

            return returnValue;
        }
    }
}
