using SecureGate.Framework.DataAccess;
using SecureGate.Framework.Serializer;
using SecureGate.ManageLogin.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SecureGate.ManageLogin.DataAccess
{
    public class SecureGateFrameworkLoginDataAccessManager
    {
        public static T ValidateLogin<T>(string jsonString)
        {
            string xmlDoc = jsonString;
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_GetLogin", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@xmlDoc", SqlDbType.Xml)
                {
                    Value = xmlDoc
                });
                connection.Open();
                return DBHelper.Execute<T>(ref command);
            }
        }


        public static T GetUserDetailsByUsername<T>(string jsonString)
        {
            string xmlDoc = jsonString;
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_GetUserDetailsByUsername", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@xmlDoc", SqlDbType.Xml)
                {
                    Value = xmlDoc
                });
                connection.Open();
                return DBHelper.Execute<T>(ref command);
            }
        }

        public static T GetUserFullDetailsByUsername<T>(string jsonString)
        {
            string xmlDoc = jsonString;
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_GetUserFullDetailsByUsername", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@xmlDoc", SqlDbType.Xml)
                {
                    Value = xmlDoc
                });
                connection.Open();
                return DBHelper.Execute<T>(ref command);
            }
        }
        public static string UpdateToken(long userId, string tokenString, string apptype)
        {
            try
            {
                using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
                {
                    IDbCommand command = new SqlCommand("USP_UpdateLoginToken", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@userId", SqlDbType.BigInt)
                    {
                        Value = userId
                    });
                    command.Parameters.Add(new SqlParameter("@tokenString", SqlDbType.NVarChar)
                    {
                        Value = tokenString
                    });

                    command.Parameters.Add(new SqlParameter("@devicetype", SqlDbType.NVarChar)
                    {
                        Value = apptype
                    });

                    connection.Open();

                    string outputJson = DBHelper.Execute<string>(ref command);

                    return outputJson;
                }
            }
            catch (Exception ex)
            {
                return default(string);
            }
        }
    }
}
