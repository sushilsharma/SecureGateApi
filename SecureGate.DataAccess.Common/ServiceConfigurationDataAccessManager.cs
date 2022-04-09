using SecureGate.Framework.DataAccess;
using SecureGate.Framework.Serializer;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace SecureGate.DataAccess.Common
{
    /// <summary>
	/// Class to manage service configurations in the database
	/// </summary>
	public static class ServiceConfigurationDataAccessManager
    {
		//Return all service configurations
		public static string GetAllServiceConfigurationList()
		{
			using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.glassRUNDatabaseREADConnection))
			{
				IDbCommand command = new System.Data.SqlClient.SqlCommand("SSP_AllServiceConfigurationList", connection);
				command.CommandType = CommandType.StoredProcedure;
				connection.Open();

                string outputJson = JSONAndXMLSerializer.XMLtoJSON(DBHelper.Execute<string>(ref command));
                return outputJson;

			}
		}


        public static string GetServiceActionURL(string serviceActionName)
        {
            string customApi = "";
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();

            customApi = root.GetSection("ServiceConfiguration").GetSection(serviceActionName).Value;

            return customApi;
        }





        //public static T GetServicesConfiguartionURL<T>(string servicesAction)
        //{

        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        string customApi = "";
        //        customApi = GetServiceActionURL(servicesAction);

        //        dt.Columns.Add("ServicesAction");
        //        dt.Columns.Add("ServicesURL");
        //        dt.Columns.Add("RequestFormat");

        //        DataRow urlRow = dt.NewRow();
        //        urlRow["ServicesAction"] = servicesAction;
        //        urlRow["ServicesURL"] = customApi;
        //        urlRow["RequestFormat"] = "{ }";
        //        dt.Rows.Add(urlRow);

        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return (T)(object)dt;

        //}

        public static T GetServicesConfiguartionFromSQL<T>(string servicesAction)
        {
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.glassRUNDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_GetServiceConfigurationbyServiceActionV2", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@ServicesAction", SqlDbType.NVarChar)
                {
                    Value = servicesAction
                });
                connection.Open();
                return DBHelper.Execute<T>(ref command);
            }
        }



        public static T GetServicesConfiguartionURL<T>(string servicesAction)
        {
            DataTable dt = new DataTable();
            try
            {
                string customApi = "";
                customApi = GetServiceActionURL(servicesAction);

                dt.Columns.Add("ServicesAction");
                dt.Columns.Add("ServicesURL");
                dt.Columns.Add("RequestFormat");

                DataRow urlRow = dt.NewRow();
                urlRow["ServicesAction"] = servicesAction;
                urlRow["ServicesURL"] = customApi;
                urlRow["RequestFormat"] = "{ }";
                dt.Rows.Add(urlRow);

            }
            catch (Exception ex)
            {

            }

            return (T)(object)dt;

        }

        public static string GetValueFormAppSettings(string sectionName)
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);


            var root = configurationBuilder.Build();
            var settingValue = root.GetSection(sectionName).Value;

            return settingValue;
        }
    }
}
