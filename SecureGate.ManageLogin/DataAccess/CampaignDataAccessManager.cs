using SecureGate.Framework.DataAccess;
using SecureGate.Framework.Serializer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SecureGate.ManageLogin.DataAccess
{
    public class CampaignDataAccessManager
    {


        public static string CreateCampaign(string jsonString)
        {
            string xmlDoc = JSONAndXMLSerializer.JSONtoXML(jsonString);
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.glassRUNDatabaseWriteConnection))
            {
                IDbCommand command = new SqlCommand("ISP_CreateCampaign", connection);
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


        public static string CompanyAndUserDetails(string jsonString)
        {
            string xmlDoc = JSONAndXMLSerializer.JSONtoXML(jsonString);
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.glassRUNDatabaseWriteConnection))
            {
                IDbCommand command = new SqlCommand("SSP_CompanyAndUserDetails", connection);
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

        


        public static string MappedCampaignToUser(string jsonString)
        {
            string xmlDoc = JSONAndXMLSerializer.JSONtoXML(jsonString);
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.glassRUNDatabaseWriteConnection))
            {
                IDbCommand command = new SqlCommand("USP_MappedCampaignToUser", connection);
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



        public static T SearchUserDetails<T>(dynamic Json)
        {
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.glassRUNDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_SearchUserDetailsV2", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 900;
                #region Parameter

                command.Parameters.Add(new SqlParameter("@RoleId", SqlDbType.BigInt)
                {
                    Value = (Json["RoleId"].Value == null ? DBNull.Value as object : Json["RoleId"].Value)
                });
                command.Parameters.Add(new SqlParameter("@PageIndex", SqlDbType.Int)
                {
                    Value = (Json["PageIndex"].Value == null ? DBNull.Value as object : Json["PageIndex"].Value)
                });
                command.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int)
                {
                    Value = (Json["PageSize"].Value == null ? DBNull.Value as object : Json["PageSize"].Value)
                });
                command.Parameters.Add(new SqlParameter("@PageName", SqlDbType.NVarChar)
                {
                    Value = (Json["PageName"].Value == null ? DBNull.Value as object : Json["PageName"].Value)
                });
                command.Parameters.Add(new SqlParameter("@OrderBy", SqlDbType.NVarChar)
                {
                    Value = (Json["OrderBy"].Value == null ? DBNull.Value as object : Json["OrderBy"].Value)
                });
                command.Parameters.Add(new SqlParameter("@OrderByCriteria", SqlDbType.NVarChar)
                {
                    Value = (Json["OrderByCriteria"].Value == null ? DBNull.Value as object : Json["OrderByCriteria"].Value)
                });
                command.Parameters.Add(new SqlParameter("@PageControlName", SqlDbType.NVarChar)
                {
                    Value = (Json["PageControlName"].Value == null ? DBNull.Value as object : Json["PageControlName"].Value)
                });
                command.Parameters.Add(new SqlParameter("@LoginId", SqlDbType.BigInt)
                {
                    Value = (Json["LoginId"].Value == null ? DBNull.Value as object : Json["LoginId"].Value)
                });
                command.Parameters.Add(new SqlParameter("@whereClause", SqlDbType.NVarChar)
                {
                    Value = (Json["whereClause"].Value == null ? DBNull.Value as object : Json["whereClause"].Value)
                });

                command.Parameters.Add(new SqlParameter("@IsExportToExcel", SqlDbType.BigInt)
                {
                    Value = (Json["IsExportToExcel"].Value == null ? DBNull.Value as object : Json["IsExportToExcel"].Value)
                });
                #endregion
                connection.Open();
                return DBHelper.Execute<T>(ref command);
            }

        }

        public static T GetCampaignData<T>(dynamic Json)
        {
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.glassRUNDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_SearchCampaignDetailsV2", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 900;
                #region Parameter

                command.Parameters.Add(new SqlParameter("@RoleId", SqlDbType.BigInt)
                {
                    Value = (Json["RoleId"].Value == null ? DBNull.Value as object : Json["RoleId"].Value)
                });
                command.Parameters.Add(new SqlParameter("@PageIndex", SqlDbType.Int)
                {
                    Value = (Json["PageIndex"].Value == null ? DBNull.Value as object : Json["PageIndex"].Value)
                });
                command.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int)
                {
                    Value = (Json["PageSize"].Value == null ? DBNull.Value as object : Json["PageSize"].Value)
                });
                command.Parameters.Add(new SqlParameter("@PageName", SqlDbType.NVarChar)
                {
                    Value = (Json["PageName"].Value == null ? DBNull.Value as object : Json["PageName"].Value)
                });
                command.Parameters.Add(new SqlParameter("@OrderBy", SqlDbType.NVarChar)
                {
                    Value = (Json["OrderBy"].Value == null ? DBNull.Value as object : Json["OrderBy"].Value)
                });
                command.Parameters.Add(new SqlParameter("@OrderByCriteria", SqlDbType.NVarChar)
                {
                    Value = (Json["OrderByCriteria"].Value == null ? DBNull.Value as object : Json["OrderByCriteria"].Value)
                });
                command.Parameters.Add(new SqlParameter("@PageControlName", SqlDbType.NVarChar)
                {
                    Value = (Json["PageControlName"].Value == null ? DBNull.Value as object : Json["PageControlName"].Value)
                });
                command.Parameters.Add(new SqlParameter("@LoginId", SqlDbType.BigInt)
                {
                    Value = (Json["LoginId"].Value == null ? DBNull.Value as object : Json["LoginId"].Value)
                });
                command.Parameters.Add(new SqlParameter("@whereClause", SqlDbType.NVarChar)
                {
                    Value = (Json["whereClause"].Value == null ? DBNull.Value as object : Json["whereClause"].Value)
                });

                command.Parameters.Add(new SqlParameter("@IsExportToExcel", SqlDbType.BigInt)
                {
                    Value = (Json["IsExportToExcel"].Value == null ? DBNull.Value as object : Json["IsExportToExcel"].Value)
                });
                #endregion
                connection.Open();
                return DBHelper.Execute<T>(ref command);
            }

        }
    }
}
