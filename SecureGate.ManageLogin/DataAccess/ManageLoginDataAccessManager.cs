using SecureGate.Framework;
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
    public class ManageLoginDataAccessManager
    {
        public static T GetUserId<T>(LoginDTO loginDTO)
        {
            //[]
            string UserAndDeviceDetailsDTOXML = ObjectXMLSerializer<UserAndDeviceDetailsDTO>.Save(loginDTO.UserAndDeviceDetails);
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_Login_GetLoginDetails", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 50)
                {
                    Value = (loginDTO.Username == null ? DBNull.Value as object : loginDTO.Username)
                });
                command.Parameters.Add(new SqlParameter("@xmlDocUserAndDeviceDetails", SqlDbType.Xml)
                {
                    Value = UserAndDeviceDetailsDTOXML
                });
                connection.Open();
                return DBHelper.Execute<T>(ref command);
            }

        }

        public static T ValidateLogin<T>(LoginDTO loginDTO)
        {
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_ValidateLoginOTP", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@userId", SqlDbType.NVarChar, 50)
                {
                    Value = loginDTO.UserId
                });
                command.Parameters.Add(new SqlParameter("@generatedOTP", SqlDbType.NVarChar, 10)
                {
                    Value = loginDTO.OTPGenerated
                });
                connection.Open();
                return DBHelper.Execute<T>(ref command);
            }
        }

        public static T ValidateOTP<T>(LoginDTO loginDTO)
        {
            string UserAndDeviceDetailsDTOXML = ObjectXMLSerializer<UserAndDeviceDetailsDTO>.Save(loginDTO.UserAndDeviceDetails);
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_ValidateOTP", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 50)
                {
                    Value = loginDTO.Username
                });
                command.Parameters.Add(new SqlParameter("@generatedOTP", SqlDbType.NVarChar, 10)
                {
                    Value = loginDTO.OTPGenerated
                });
                command.Parameters.Add(new SqlParameter("@xmlDocUserAndDeviceDetails", SqlDbType.Xml)
                {
                    Value = UserAndDeviceDetailsDTOXML
                });
                connection.Open();
                return DBHelper.Execute<T>(ref command);
            }
        }


        public static string InsertLoginOTP(LoginDTO loginDTO)
        {
            //try
            //{
            string data = "";
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseWriteConnection))
            {
                IDbCommand command = new SqlCommand("ISP_LoginOTP", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@UserId", SqlDbType.BigInt)
                {
                    Value = loginDTO.UserId
                });
                command.Parameters.Add(new SqlParameter("@RoleMasterId", SqlDbType.Int)
                {
                    Value = loginDTO.RoleMasterId
                });
                command.Parameters.Add(new SqlParameter("@OTPGenerated", SqlDbType.NVarChar, 50)
                {
                    Value = loginDTO.OTPGenerated
                });
                command.Parameters.Add(new SqlParameter("@OTPCreatedTime", SqlDbType.DateTime)
                {
                    Value = loginDTO.OTPCreatedTime
                });
                command.Parameters.Add(new SqlParameter("@OTPValidTill", SqlDbType.DateTime)
                {
                    Value = loginDTO.OTPValidTill
                });
                command.Parameters.Add(new SqlParameter("@OTPSentByChannelId", SqlDbType.Int)
                {
                    Value = loginDTO.OTPSentByChannelId
                });
                command.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.BigInt)
                {
                    Value = loginDTO.CreatedBy
                });
                connection.Open();
                data = DBHelper.Execute<string>(ref command);
                //if (!string.IsNullOrEmpty(data))
                //{
                //    outputJson = JSONAndXMLSerializer.XMLtoJSON(data);
                //}
            }
            return data;
            //}
            //catch (Exception ex)
            //{
            //    //DataAccessExceptionHandler.HandleException(ref ex);
            //}
            //return "";
        }

        public static string UpdateLoginOTP(LoginDTO loginDTO)
        {
            try
            {
                string data = "";
                using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseWriteConnection))
                {
                    IDbCommand command = new SqlCommand("USP_LoginOTP", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@UserId", SqlDbType.BigInt)
                    {
                        Value = loginDTO.UserId
                    });
                    connection.Open();
                    data = DBHelper.Execute<string>(ref command);
                    //if (!string.IsNullOrEmpty(data))
                    //{
                    //    outputJson = JSONAndXMLSerializer.XMLtoJSON(data);
                    //}
                }
                return data;
            }
            catch (Exception ex)
            {
                //DataAccessExceptionHandler.HandleException(ref ex);
            }
            return "";
        }

        public static string UpdateRegeneratedLoginOTP(LoginDTO loginDTO)
        {
            try
            {
                string UserAndDeviceDetailsDTOXML = ObjectXMLSerializer<UserAndDeviceDetailsDTO>.Save(loginDTO.UserAndDeviceDetails);
                string data = "";
                using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseWriteConnection))
                {
                    IDbCommand command = new SqlCommand("USP_LoginOTP_Regenerated", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@UserId", SqlDbType.BigInt)
                    {
                        Value = loginDTO.UserId
                    });
                    command.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar)
                    {
                        Value = loginDTO.Username
                    });
                    command.Parameters.Add(new SqlParameter("@OTPGenerated", SqlDbType.NVarChar, 20)
                    {
                        Value = loginDTO.OTPGenerated
                    });
                    command.Parameters.Add(new SqlParameter("@OTPCreatedTime", SqlDbType.DateTime)
                    {
                        Value = loginDTO.OTPCreatedTime
                    });
                    command.Parameters.Add(new SqlParameter("@OTPValidTill", SqlDbType.DateTime)
                    {
                        Value = loginDTO.OTPValidTill
                    });
                    command.Parameters.Add(new SqlParameter("@OTPSentByChannelId", SqlDbType.Int)
                    {
                        Value = loginDTO.OTPSentByChannelId
                    });
                    command.Parameters.Add(new SqlParameter("@IsOTPUsed", SqlDbType.Bit)
                    {
                        Value = loginDTO.IsOTPUsed
                    });
                    command.Parameters.Add(new SqlParameter("@IsCurrentOTPRegenerated", SqlDbType.Bit)
                    {
                        Value = loginDTO.IsCurrentOTPRegenerated
                    });
                    command.Parameters.Add(new SqlParameter("@xmlDocUserAndDeviceDetails", SqlDbType.Xml)
                    {
                        Value = UserAndDeviceDetailsDTOXML
                    });
                    connection.Open();
                    data = DBHelper.Execute<string>(ref command);
                    //if (!string.IsNullOrEmpty(data))
                    //{
                    //    outputJson = JSONAndXMLSerializer.XMLtoJSON(data);
                    //}
                }
                return data;
            }
            catch (Exception ex)
            {
                //DataAccessExceptionHandler.HandleException(ref ex);
            }
            return "";
        }


        public static T ChangePassword<T>(LoginDTO loginDTO)
        {
            string UserAndDeviceDetailsDTOXML = ObjectXMLSerializer<UserAndDeviceDetailsDTO>.Save(loginDTO.UserAndDeviceDetails);

            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("USP_UpdateUserPassword", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@UserId", SqlDbType.BigInt)
                {
                    Value = loginDTO.UserId
                });
                command.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar)
                {
                    Value = loginDTO.Username
                });
                command.Parameters.Add(new SqlParameter("@HashedPassword", SqlDbType.NVarChar, 5000)
                {
                    Value = loginDTO.HashedPassword
                });
                command.Parameters.Add(new SqlParameter("@PasswordSalt", SqlDbType.Int)
                {
                    Value = loginDTO.PasswordSalt
                });

                command.Parameters.Add(new SqlParameter("@xmlDocUserAndDeviceDetails", SqlDbType.Xml)
                {
                    Value = UserAndDeviceDetailsDTOXML
                });

                command.Parameters.Add(new SqlParameter("@IsDigitalOnboarding", SqlDbType.Int)
                {
                    Value = loginDTO.IsDigitalOnboarding
                });

                command.Parameters.Add(new SqlParameter("@EnvironmentCode", SqlDbType.NVarChar, 5000)
                {
                    Value = loginDTO.EnvironmentCode
                });

                connection.Open();
                return DBHelper.Execute<T>(ref command);
            }

        }


        public static T ContactInformationUpDate<T>(LoginDTO loginDTO)
        {
            string UserAndDeviceDetailsDTOXML = ObjectXMLSerializer<UserAndDeviceDetailsDTO>.Save(loginDTO.UserAndDeviceDetails);
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("ISP_ContactInformationByLoginId_V2", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@UserId", SqlDbType.BigInt)
                {
                    Value = loginDTO.UserId
                });
                command.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar)
                {
                    Value = loginDTO.Username
                });
                command.Parameters.Add(new SqlParameter("@MobileNumber", SqlDbType.NVarChar, 250)
                {
                    Value = loginDTO.MobileNo
                });
                command.Parameters.Add(new SqlParameter("@AppHashKey", SqlDbType.NVarChar)
                {
                    Value = loginDTO.AppHashKey == null ? DBNull.Value as object : loginDTO.AppHashKey
                });
                command.Parameters.Add(new SqlParameter("@xmlDocUserAndDeviceDetails", SqlDbType.Xml)
                {
                    Value = UserAndDeviceDetailsDTOXML
                });
                connection.Open();
                return DBHelper.Execute<T>(ref command);
            }

        }


        public static T ValidateUserNameAndMobileNumber<T>(LoginDTO loginDTO)
        {
            string UserAndDeviceDetailsDTOXML = ObjectXMLSerializer<UserAndDeviceDetailsDTO>.Save(loginDTO.UserAndDeviceDetails);
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_ValidateUserNameAndMobileNumber_V2", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 250)
                {
                    Value = loginDTO.Username
                });
                command.Parameters.Add(new SqlParameter("@MobileNumber", SqlDbType.NVarChar, 250)
                {
                    Value = loginDTO.MobileNo
                });
                command.Parameters.Add(new SqlParameter("@xmlDocUserAndDeviceDetails", SqlDbType.Xml)
                {
                    Value = UserAndDeviceDetailsDTOXML
                });

                connection.Open();
                return DBHelper.Execute<T>(ref command);
            }

        }

        public static T GetChildUserList<T>(LoginDTO loginDTO)
        {
            string UserAndDeviceDetailsDTOXML = ObjectXMLSerializer<UserAndDeviceDetailsDTO>.Save(loginDTO.UserAndDeviceDetails);

            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_GetChildUserListV2", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@LoginId", SqlDbType.BigInt)
                {
                    Value = loginDTO.UserId
                });
                command.Parameters.Add(new SqlParameter("@xmlDocUserAndDeviceDetails", SqlDbType.Xml)
                {
                    Value = UserAndDeviceDetailsDTOXML
                });
                connection.Open();
                return DBHelper.Execute<T>(ref command);
            }

        }

        public static T GetSuperUserListV2<T>(UserOutletMappingDTO userOutletMappingDTO)
        {
            string UserAndDeviceDetailsDTOXML = ObjectXMLSerializer<UserAndDeviceDetailsDTO>.Save(userOutletMappingDTO.UserAndDeviceDetails);
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_GetSuperUserListV2", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@UserId", SqlDbType.BigInt)
                {
                    Value = (userOutletMappingDTO.UserId == null ? DBNull.Value as object : userOutletMappingDTO.UserId)
                });
                command.Parameters.Add(new SqlParameter("@xmlDocUserAndDeviceDetails", SqlDbType.Xml)
                {
                    Value = UserAndDeviceDetailsDTOXML
                });
                connection.Open();
                return DBHelper.Execute<T>(ref command);
            }

        }

        public static T SaveUserOutletMapping<T>(string jsonString)
        {
            string xmlDoc = jsonString;
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseWriteConnection))
            {
                IDbCommand command = new SqlCommand("ISP_UserOutletMapping", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@xmlDoc", SqlDbType.Xml)
                {
                    Value = xmlDoc
                });
                connection.Open();


                return DBHelper.Execute<T>(ref command);
            }
        }

        public static T UpdateUserOutletMapping<T>(string jsonString)
        {
            string xmlDoc = jsonString;
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseWriteConnection))
            {
                IDbCommand command = new SqlCommand("USP_UserOutletMapping", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@xmlDoc", SqlDbType.Xml)
                {
                    Value = xmlDoc
                });
                connection.Open();


                return DBHelper.Execute<T>(ref command);
            }
        }

        public static T GetB2BLogin<T>(string jsonString)
        {
            string xmlDoc = jsonString;
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_GetB2BLogin_New", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@xmlDoc", SqlDbType.Xml)
                {
                    Value = xmlDoc
                });
                connection.Open();
                return DBHelper.Execute<T>(ref command);
            }
        }


        public static T ActivateDeactivateCompany<T>(LoginDTO loginDTO)
        {
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseWriteConnection))
            {
                IDbCommand command = new SqlCommand("SSP_ActivateDeactivateCompany", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.BigInt)
                {
                    Value = loginDTO.CompanyId == null ? DBNull.Value as object : loginDTO.CompanyId
                });
                command.Parameters.Add(new SqlParameter("@ActivateOrDeactivate", SqlDbType.NVarChar)
                {
                    Value = (loginDTO.ActivateOrDeactivate == null ? DBNull.Value as object : loginDTO.ActivateOrDeactivate)
                });
                connection.Open();
                return DBHelper.Execute<T>(ref command);


            }
        }

        public static T UpdateLanguageForUser<T>(LoginDTO loginDTO)
        {
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseWriteConnection))
            {
                IDbCommand command = new SqlCommand("USP_UpdateLanguageForUser", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.BigInt)
                {
                    Value = loginDTO.CompanyId == null ? DBNull.Value as object : loginDTO.CompanyId
                });
                command.Parameters.Add(new SqlParameter("@DefaultLanguage", SqlDbType.BigInt)
                {
                    Value = (loginDTO.DefaultLanguage == null ? DBNull.Value as object : loginDTO.DefaultLanguage)
                });
                connection.Open();
                return DBHelper.Execute<T>(ref command);


            }
        }

        public static T GetAppNotificationsByUserId<T>(LoginDTO loginDTO)
        {
            string UserAndDeviceDetailsDTOXML = ObjectXMLSerializer<UserAndDeviceDetailsDTO>.Save(loginDTO.UserAndDeviceDetails);
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_GetAppNotificationsByUserId", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@UserId", SqlDbType.BigInt)
                {
                    Value = loginDTO.UserId
                });
                command.Parameters.Add(new SqlParameter("@xmlDocUserAndDeviceDetails", SqlDbType.Xml)
                {
                    Value = UserAndDeviceDetailsDTOXML
                });
                connection.Open();
                return DBHelper.Execute<T>(ref command);
            }

        }



        public static T ValidateUserAndOnboardingReference<T>(LoginDTO loginDTO)
        {
            string UserAndDeviceDetailsDTOXML = ObjectXMLSerializer<UserAndDeviceDetailsDTO>.Save(loginDTO.UserAndDeviceDetails);
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_ValidateUserAndOnboardingReference", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 50)
                {
                    Value = (loginDTO.Username == null ? DBNull.Value as object : loginDTO.Username)
                });
                command.Parameters.Add(new SqlParameter("@OnboardingReferenceCode", SqlDbType.NVarChar, 50)
                {
                    Value = (loginDTO.OnboardingReferenceCode == null ? DBNull.Value as object : loginDTO.OnboardingReferenceCode)
                });
                command.Parameters.Add(new SqlParameter("@xmlDocUserAndDeviceDetails", SqlDbType.Xml)
                {
                    Value = UserAndDeviceDetailsDTOXML
                });
                connection.Open();
                return DBHelper.Execute<T>(ref command);
            }

        }

        public static string GetSettingMasterBySettingParameter(string settingParameter)
        {
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_SettingMasterBySettingParameter", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@SettingParameter", SqlDbType.NVarChar)
                {
                    Value = settingParameter
                });

                connection.Open();

                string outputJson = "";


                if (DBHelper.Execute<string>(ref command) != null)
                {

                    outputJson = JSONAndXMLSerializer.XMLtoJSON(DBHelper.Execute<string>(ref command));

                }


                //string outputJson = DBHelper.Execute<string>(ref command);

                return outputJson;
            }
        }


        public static T LoadAllEnvironmentUrl<T>(UserIdentityDTO loginDTO)
        {
            string UserAndDeviceDetailsDTOXML = ObjectXMLSerializer<UserAndDeviceDetailsDTO>.Save(loginDTO.UserAndDeviceDetails);
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_GetAllEnvironmentURL", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar)
                {
                    Value = loginDTO.UserName
                });
                command.Parameters.Add(new SqlParameter("@xmlDocUserAndDeviceDetails", SqlDbType.Xml)
                {
                    Value = UserAndDeviceDetailsDTOXML
                });
                connection.Open();
                return DBHelper.Execute<T>(ref command);
            }

        }


        public static string UpdateToken(long userId, string tokenString)
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

        public static T ValidateToken<T>(LoginDTO loginDTO)
        {

            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_ValidateToken", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@TokenString", SqlDbType.NVarChar)
                {
                    Value = loginDTO.TokenString
                });
                connection.Open();
                return DBHelper.Execute<T>(ref command);
            }

        }

        public static T GetLoginDetailsById<T>(LoginDTO loginDTO)
        {
            //[]

            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
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

        public static T UpdateUserIdentityAlternateUserName<T>(UserIdentityDTO userIdentityDTO)
        {

            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("USP_UserIdentityAlternateUserName_V2", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@UserId", SqlDbType.BigInt)
                {
                    Value = userIdentityDTO.UserId
                });
                command.Parameters.Add(new SqlParameter("@AlternateUserName", SqlDbType.NVarChar)
                {
                    Value = userIdentityDTO.AlternateUserName == null ? DBNull.Value as object : userIdentityDTO.AlternateUserName
                });

                connection.Open();
                return DBHelper.Execute<T>(ref command);
            }

        }


        public static string UpdateLoginHistoryB2BApp(string jsonString)
        {
            try
            {
                string xmlDoc = JSONAndXMLSerializer.JSONtoXML(jsonString);

                using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseWriteConnection))
                {
                    IDbCommand command = new SqlCommand("USP_UpdateLoginHistoryB2BApp", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@xmlDoc", SqlDbType.Xml)
                    {
                        Value = xmlDoc
                    });
                    command.CommandTimeout = 900;
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


        public static string DeleteLoginByLoginId(string jsonString)
        {


            string outputJson = "";

            string xmlDoc = JSONAndXMLSerializer.JSONtoXML(jsonString);

            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseWriteConnection))
            {
                IDbCommand command = new SqlCommand("DSP_Login", connection);
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


        public static string GetLocationDetails(string jsonString)
        {
            try
            {
                string xmlDoc = JSONAndXMLSerializer.JSONtoXML(jsonString);
                using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseWriteConnection))
                {
                    IDbCommand command = new SqlCommand("SSP_LocationDetailsForB2BApp", connection);
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


        public static string GetProfileDetails(string jsonString)
        {
            try
            {
                string xmlDoc = JSONAndXMLSerializer.JSONtoXML(jsonString);
                using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseWriteConnection))
                {
                    IDbCommand command = new SqlCommand("SSP_CustomerApp_CustomerProfile", connection);
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



        public static T CheckDuplicateUser<T>(string jsonString)
        {
            string xmlDoc = jsonString;
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_DuplicateUserCheck", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@xmlDoc", SqlDbType.Xml)
                {
                    Value = (xmlDoc ?? DBNull.Value as object)
                });
                connection.Open();
                return DBHelper.Execute<T>(ref command);
            }
        }

        public static string CreateLogin(string jsonString)
        {


            string outputJson = "";

            string xmlDoc = JSONAndXMLSerializer.JSONtoXML(jsonString);

            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseWriteConnection))
            {
                IDbCommand command = new SqlCommand("ISP_Login", connection);
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



        public static string UpdateLogin(string jsonString)
        {


            string outputJson = "";

            string xmlDoc = JSONAndXMLSerializer.JSONtoXML(jsonString);

            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseWriteConnection))
            {
                IDbCommand command = new SqlCommand("USP_Login_New", connection);
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


        public static T UpdateDigitalOnBoardingLink<T>(string jsonString)
        {
            string xmlDoc = jsonString;
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("USP_UpdateDigitalOnBoardingLinkStatus", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@xmlDoc", SqlDbType.Xml)
                {
                    Value = (xmlDoc ?? DBNull.Value as object)
                });
                connection.Open();
                return DBHelper.Execute<T>(ref command);
            }
        }


        public static T ValidateUserTokenKey<T>(string jsonString)
        {
            string xmlDoc = jsonString;
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_ValidateUserTokenKey", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@xmlDoc", SqlDbType.Xml)
                {
                    Value = (xmlDoc ?? DBNull.Value as object)
                });
                connection.Open();
                return DBHelper.Execute<T>(ref command);
            }
        }

        public static string LoadUserProfile(string jsonString)
        {

            string xmlDoc = JSONAndXMLSerializer.JSONtoXML(jsonString);
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_UserDetails_Paging", connection);
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

        public static T GetUserUserProfileByUserLoginId<T>(long LoginId,string Username)
        {

            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_Login_SelectByUserloginId", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@LoginId", SqlDbType.BigInt)
                {
                    Value = (LoginId == 0 ? DBNull.Value as object : LoginId)
                }); 
                
                command.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar)
                {
                    Value = Username == null ? DBNull.Value as object : Username
         
                });

                connection.Open();

                return DBHelper.Execute<T>(ref command);
            }

        }

        public static T GetPolicyByRolemasterId<T>(long RoleMasterid)
        {

            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_PasswordPolicy_SelectByRoleMasterId", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@RoleMasterId", SqlDbType.NVarChar)
                {
                    Value = (RoleMasterid == null ? DBNull.Value as object : RoleMasterid)
                });

                connection.Open();
                return DBHelper.Execute<T>(ref command);
            }

        }

        public static T GetLoginByUserProfileId<T>(string ProfileId)
        {
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_Login_SelectByUserProfileId", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@ProfileId", SqlDbType.NVarChar)
                {
                    Value = (ProfileId == null ? DBNull.Value as object : ProfileId)
                });


                connection.Open();

                return DBHelper.Execute<T>(ref command);
            }

        }

        public static T UpdateLoginPassword<T>(string xmlDoc)
        {

            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("USP_Login", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@xmlDoc", SqlDbType.Xml)
                {
                    Value = (xmlDoc == null ? DBNull.Value as object : xmlDoc)
                });

                connection.Open();
                return DBHelper.Execute<T>(ref command);
            }

        }

        public static string LoadLookUpListByCritiera(string jsonString)
        {

            string xmlDoc = JSONAndXMLSerializer.JSONtoXML(jsonString);

            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_LookUpList_SelectByCritiera", connection);
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

        public static string LoadDocumetRequired()
        {

            string outputJson = "";

            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_LoadAllDocumentRequired", connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();

                string data = DBHelper.Execute<string>(ref command);
                if (!string.IsNullOrEmpty(data))
                {
                    outputJson = JSONAndXMLSerializer.XMLtoJSON(data);
                }
            }
            return outputJson;

        }

        public static string GetAllCompanyListByDropDown(string jsonString)
        {
            string xmlDoc = JSONAndXMLSerializer.JSONtoXML(jsonString);
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_CompanyDetailList_SelectByDropDown", connection);
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

        public static string GetAllShipToListByCompanyId(string jsonString)
        {
            string xmlDoc = JSONAndXMLSerializer.JSONtoXML(jsonString);
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_ShipTo_ByCompanyId", connection);
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

        public static string LoadAllRoleMaster(string jsonString)
        {
            string xmlDoc = JSONAndXMLSerializer.JSONtoXML(jsonString);
            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_GetAllRoleMaster", connection);
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

        public static string LoadLoginDetailByLoginId(string jsonString)
        {

            string outputJson = "";

            string xmlDoc = JSONAndXMLSerializer.JSONtoXML(jsonString);

            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_LoadLoginDetailByLoginId", connection);
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

        public static string GetPageRoleWiseAccessDetailByRoleORUserID(string jsonString)
        {
            string outputJson = "";

            string xmlDoc = JSONAndXMLSerializer.JSONtoXML(jsonString);

            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("SSP_GetPageRoleWiseAccessDetailByRoleORUserID", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@xmlDoc", SqlDbType.Xml)
                {
                    Value = xmlDoc
                });
                connection.Open();
                outputJson = JSONAndXMLSerializer.XMLtoJSON(DBHelper.Execute<string>(ref command));

            }

            return outputJson;

        }

        public static string InsertAndUpdateRoleWisePageMappingAndRoleWiseFieldAccess(string jsonString)
        {
            string xmlDoc = JSONAndXMLSerializer.JSONtoXML(jsonString);

            using (var connection = ConnectionManager.Create(ConnectionManager.ConnectTo.SecureGateDatabaseREADConnection))
            {
                IDbCommand command = new SqlCommand("USP_InsertAndUpdateRoleWisePageMappingAndRoleWiseFieldAccess", connection);
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

   


    }
}
