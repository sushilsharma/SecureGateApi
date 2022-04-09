using SecureGate.Framework.DataAccess;
using SecureGate.Framework.Serializer;
using SecureGate.APIController.Framework.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SecureGate.APIController.Framework.DataAccess
{

    public class ManageLoginDataAccessManager
    {
        public static T GetLoginDetailsById<T>(LoginDTO loginDTO)
        {
            //[]

            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.glassRUNDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_Login_GetLoginDetailsById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@LoginId", SqlDbType.BigInt)
                {
                    Value = (loginDTO.UserId == 0 ? DBNull.Value as object : loginDTO.UserId)
                });
                connection.Open();
                return DBHelper.Execute<T>(ref command);
            }

        }
    }
}
