using SecureGate.Framework;
using SecureGate.Framework.DataAccess;
using SecureGate.Framework.Serializer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SecureGate.Customer.DataAccess
{
    public class CustomerDataAccessManager
    {
        public static string GetCustomerByMnemonic(string jsonString)
        {

            string outputJson = "";

            string xmlDoc = JSONAndXMLSerializer.JSONtoXML(jsonString);

            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_GetCustomerByMnemonic", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@xmlDoc", SqlDbType.Xml)
                {
                    Value = (xmlDoc ?? DBNull.Value as object)
                });

                connection.Open();

                string data = DBHelper.Execute<string>(ref command);

                if (!string.IsNullOrEmpty(data))
                {
                    outputJson = JSONAndXMLSerializer.XMLtoJSON(data);
                }

            }
            return outputJson;

        }


        public static string UpdateCustomer(string jsonString)
        {

            string outputJson = "";

            string xmlDoc = JSONAndXMLSerializer.JSONtoXML(jsonString);

            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("USP_Customer", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@xmlDoc", SqlDbType.Xml)
                {
                    Value = (xmlDoc ?? DBNull.Value as object)
                });

                connection.Open();

                string data = DBHelper.Execute<string>(ref command);

                if (!string.IsNullOrEmpty(data))
                {
                    outputJson = JSONAndXMLSerializer.XMLtoJSON(data);
                }

            }
            return outputJson;

        }

        public static string AddCustomer(string jsonString)
        {

            string outputJson = "";

            string xmlDoc = JSONAndXMLSerializer.JSONtoXML(jsonString);

            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("ISP_Customer", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@xmlDoc", SqlDbType.Xml)
                {
                    Value = (xmlDoc ?? DBNull.Value as object)
                });

                connection.Open();

                string data = DBHelper.Execute<string>(ref command);

                if (!string.IsNullOrEmpty(data))
                {
                    outputJson = JSONAndXMLSerializer.XMLtoJSON(data);
                }

            }
            return outputJson;

        }
    }
}
