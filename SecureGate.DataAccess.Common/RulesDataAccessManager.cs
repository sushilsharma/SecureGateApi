using SecureGate.Framework.DataAccess;
using SecureGate.Framework.Serializer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SecureGate.DataAccess.Common
{
    public static class RulesDataAccessManager
    {
		public static string GetRuleDetailsByCompanyId(string jsonString)
		{
			try
			{

				string xmlDoc = JSONAndXMLSerializer.JSONtoXML(jsonString);

				using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.glassRUNDatabaseREADConnection))
				{
					IDbCommand command = new SqlCommand("SSP_GetRulesByCompanyId", connection);
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.Add(new SqlParameter("@xmlDoc", SqlDbType.Xml)
					{
						Value = xmlDoc
					});
					connection.Open();

					string outputJson = JSONAndXMLSerializer.XMLtoJSON(DBHelper.Execute<string>(ref command));

					return outputJson;


				}
			}
			catch (Exception ex)
			{
				//DataAccessExceptionHandler.HandleException(ref ex);
			}
			return default(string);
		}

	}
}
