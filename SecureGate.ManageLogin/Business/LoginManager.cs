using APIController.Framework;
using SecureGate.APIController.Framework.AppLogger;
using SecureGate.Framework;
using SecureGate.Framework.DataAccess;
using SecureGate.Framework.PasswordUtility;
using SecureGate.Framework.Serializer;
//using glassRUNProduct.DataAccess.Common;
using SecureGate.ManageLogin.DataAccess;
using SecureGate.ManageLogin.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using SecureGate.DataAccess.Common;

namespace SecureGate.ManageLogin.Business
{
    public class LoginManager : ILoginManager
    {
        BaseAppLogger _loggerInstance;
        public LoginManager(BaseAppLogger loggerInstance)
        {
            _loggerInstance = loggerInstance;
        }

        public List<LoginDTO> ValidateLogin(LoginDTO loginDTO)
        {
            _loggerInstance.Information("Login Data received - " + loginDTO.ToString(), 502);
            List<LoginDTO> getAprroved = new List<LoginDTO>();
            try
            {
                if (ValidateData(loginDTO))
                {
                    DataTable dt = new DataTable();
                    string a = ManageLoginDataAccessManager.GetUserId<string>(loginDTO);
                    if (a != null)
                    {
                        a = JSONAndXMLSerializer.XMLtoJSON(a);
                        JObject servicesConfiguartionURL = (JObject)JsonConvert.DeserializeObject(a);
                        string userId = servicesConfiguartionURL["Login"]["LoginList"]["LoginId"].ToString();
                        loginDTO.UserId = Convert.ToInt64(userId);
                        string dataRcvd = ManageLoginDataAccessManager.ValidateLogin<string>(loginDTO);
                        dataRcvd = JSONAndXMLSerializer.XMLtoJSON(dataRcvd);
                        JObject receivedData = (JObject)JsonConvert.DeserializeObject(dataRcvd);
                        _loggerInstance.Information("RData - " + receivedData.ToString(), 502);
                        if (receivedData["LoginOTP"]["LoginOTPs"]["IsOTPUsed"].ToString() == "0")
                        {
                            loginDTO.IsOTPUsed = false;
                        }
                        else if (receivedData["LoginOTP"]["LoginOTPs"]["IsOTPUsed"].ToString() == "1")
                        {
                            loginDTO.IsOTPUsed = true;
                        }
                        _loggerInstance.Information("Passed ", 502);
                        loginDTO.OTPValidTill = Convert.ToDateTime(receivedData["LoginOTP"]["LoginOTPs"]["OTPValidTill"]);
                        loginDTO.MobileNo = servicesConfiguartionURL["Login"]["LoginList"]["MobileNo"].ToString();
                        loginDTO.EulaAgreement = servicesConfiguartionURL["Login"]["LoginList"]["EulaAgreement"].ToString();
                        if (!loginDTO.IsOTPUsed)
                        {

                            if (receivedData["LoginOTP"]["LoginOTPs"]["OTPGenerated"].ToString() == loginDTO.OTPGenerated)
                            {
                                if (loginDTO.OTPValidTill.Subtract(DateTime.Now).TotalSeconds > 0)
                                {
                                    loginDTO.IsValid = "YES";
                                    loginDTO.ReasonCode = "OK";
                                    loginDTO.IsOTPUsed = true;
                                    try
                                    {
                                        string strMsg = ManageLoginDataAccessManager.UpdateLoginOTP(loginDTO);
                                        if (strMsg == loginDTO.UserId.ToString())
                                        {

                                        }
                                        else
                                        {
                                            loginDTO.IsValid = "NO";
                                            loginDTO.ReasonCode = "There is some problem while activation. Please contact system administrator";
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        loginDTO.IsValid = "NO";
                                        loginDTO.ReasonCode = "There is some problem while activation. Please contact system administrator";
                                        _loggerInstance.Information("ValidateData internal Exception - " + ex.ToString(), 502);
                                    }
                                }
                                else
                                {
                                    loginDTO.IsValid = "NO";
                                    loginDTO.ReasonCode = "The OTP entered has already expired. Please contact system administrator.";
                                }
                            }
                            else
                            {
                                loginDTO.IsValid = "NO";
                                loginDTO.ReasonCode = "The OTP entered is incorrect. Please enter correct OTP or contact system administrator.";
                            }
                        }
                        else
                        {
                            loginDTO.IsValid = "NO";
                            loginDTO.ReasonCode = "The OTP provided is not longer available for activation. Please contact system administrator.";
                        }
                    }
                    else
                    {
                        loginDTO.IsValid = "NO";
                        loginDTO.ReasonCode = "User not found";
                    }
                    getAprroved.Add(loginDTO);
                    //getAprroved = Extension.DataTableToList<LoginDTO>(dt);
                    return getAprroved;
                }
                else
                {
                    loginDTO.IsValid = "NO";
                    loginDTO.ReasonCode = "Invalid Data";
                    getAprroved.Add(loginDTO);
                    return getAprroved;
                }
            }
            catch (Exception ex)
            {
                loginDTO.IsValid = "NO";
                loginDTO.ReasonCode = "Error!! Please contact system administrator.";
                _loggerInstance.Information("ValidateData Exception - " + ex.ToString(), 502);
                getAprroved.Add(loginDTO);
                return getAprroved;
            }

        }

        public List<LoginDTO> ValidateOTP(LoginDTO loginDTO)
        {
            _loggerInstance.Information("ValidateOTP Data received - " + loginDTO.ToString(), 502);
            List<LoginDTO> getAprroved = new List<LoginDTO>();
            try
            {
                if (ValidateData(loginDTO))
                {
                    string dataRcvd = ManageLoginDataAccessManager.ValidateOTP<string>(loginDTO);
                    dataRcvd = JSONAndXMLSerializer.XMLtoJSON(dataRcvd);
                    JObject receivedData = (JObject)JsonConvert.DeserializeObject(dataRcvd);
                    _loggerInstance.Information("RData - " + receivedData.ToString(), 502);
                    if (receivedData["LoginOTP"]["LoginOTPs"]["IsOTPUsed"].ToString() == "0")
                    {
                        loginDTO.IsOTPUsed = false;
                    }
                    else if (receivedData["LoginOTP"]["LoginOTPs"]["IsOTPUsed"].ToString() == "1")
                    {
                        loginDTO.IsOTPUsed = true;
                    }
                    _loggerInstance.Information("Passed ", 502);
                    loginDTO.OTPValidTill = Convert.ToDateTime(receivedData["LoginOTP"]["LoginOTPs"]["OTPValidTill"]);
                    if (!loginDTO.IsOTPUsed)
                    {

                        if (receivedData["LoginOTP"]["LoginOTPs"]["OTPGenerated"].ToString() == loginDTO.OTPGenerated)
                        {
                            if (loginDTO.OTPValidTill.Subtract(DateTime.Now).TotalSeconds > 0)
                            {
                                loginDTO.IsValid = "YES";
                                loginDTO.ReasonCode = "OK";
                                loginDTO.IsOTPUsed = true;
                                try
                                {
                                    loginDTO.UserId = Convert.ToInt64(receivedData["LoginOTP"]["LoginOTPs"]["UserId"]);
                                    string strMsg = ManageLoginDataAccessManager.UpdateLoginOTP(loginDTO);
                                    if (strMsg == loginDTO.UserId.ToString())
                                    {

                                    }
                                    else
                                    {
                                        loginDTO.IsValid = "NO";
                                        loginDTO.ReasonCode = "There is some problem while activation. Please contact system administrator";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    loginDTO.IsValid = "NO";
                                    loginDTO.ReasonCode = "There is some problem while activation. Please contact system administrator";
                                    _loggerInstance.Information("ValidateData internal Exception - " + ex.ToString(), 502);
                                }
                            }
                            else
                            {
                                loginDTO.IsValid = "NO";
                                loginDTO.ReasonCode = "The OTP entered has already expired. Please contact system administrator.";
                            }
                        }
                        else
                        {
                            loginDTO.IsValid = "NO";
                            loginDTO.ReasonCode = "The OTP entered is incorrect. Please enter correct OTP or contact system administrator.";
                        }
                    }
                    else
                    {
                        loginDTO.IsValid = "NO";
                        loginDTO.ReasonCode = "The OTP provided is not longer available for activation. Please contact system administrator.";
                    }

                    getAprroved.Add(loginDTO);
                    //getAprroved = Extension.DataTableToList<LoginDTO>(dt);
                    return getAprroved;
                }
                else
                {
                    loginDTO.IsValid = "NO";
                    loginDTO.ReasonCode = "Invalid Data";
                    getAprroved.Add(loginDTO);
                    return getAprroved;
                }
            }
            catch (Exception ex)
            {
                loginDTO.IsValid = "NO";
                loginDTO.ReasonCode = "Error!! Please contact system administrator.";
                _loggerInstance.Information("ValidateData Exception - " + ex.ToString(), 502);
                getAprroved.Add(loginDTO);
                return getAprroved;
            }

        }

        public bool ValidateData(LoginDTO loginDTO)
        {
            if (loginDTO.Username == "" && loginDTO.UserId == 0)
            {
                return false;
            }
            return true;
        }

        public string GenerateOTP()
        {
            try
            {
                //Here Random class used for Random Number
                //Instead of Random Number You can pass Customer ID/User Id Here
                Random rng = new Random();
                //Fetching OTP Characters
                string OtpCharacters = OTPCharacters();
                //Createing More Secure OTP Password by Using MD5 algorithm
                string OTPPassword = OTPGenerator(OtpCharacters, rng.Next(10).ToString());
                return OTPPassword;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string OTPCharacters()
        {
            string OTPLength = "6";
            string NewCharacters = "";
            //This one tells you which characters are allowed in this new password
            string allowedChars = "";
            //Here Specify your OTP Characters
            allowedChars = "1,2,3,4,5,6,7,8,9,0";
            //If you Need more secure OTP then uncomment Below Lines 
            //allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";        
            //allowedChars += "~,!,@,#,$,%,^,&,*,+,?";
            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            string IDString = "";
            string temp = "";
            //utilize the "random" class
            Random rand = new Random();
            for (int i = 0; i < Convert.ToInt32(OTPLength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                IDString += temp;
                NewCharacters = IDString;
            }
            return NewCharacters;
        }

        public string OTPGenerator(string uniqueIdentity, string uniqueCustomerIdentity)
        {
            int length = 6;
            string oneTimePassword = "";
            DateTime dateTime = DateTime.Now;
            string _strParsedReqNo = dateTime.Day.ToString();
            _strParsedReqNo = _strParsedReqNo + dateTime.Month.ToString();
            _strParsedReqNo = _strParsedReqNo + dateTime.Year.ToString();
            _strParsedReqNo = _strParsedReqNo + dateTime.Hour.ToString();
            _strParsedReqNo = _strParsedReqNo + dateTime.Minute.ToString();
            _strParsedReqNo = _strParsedReqNo + dateTime.Second.ToString();
            _strParsedReqNo = _strParsedReqNo + dateTime.Millisecond.ToString();
            _strParsedReqNo = _strParsedReqNo + uniqueCustomerIdentity;

            //_strParsedReqNo = uniqueIdentity + uniqueCustomerIdentity;
            using (MD5 md5 = MD5.Create())
            {
                //Get hash code of entered request id in byte format.
                byte[] _reqByte = md5.ComputeHash(Encoding.UTF8.GetBytes(_strParsedReqNo));
                //convert byte array to integer.
                int _parsedReqNo = BitConverter.ToInt32(_reqByte, 0);
                string _strParsedReqId = Math.Abs(_parsedReqNo).ToString();
                //Check if length of hash code is less than 9.
                //If so, then prepend multiple zeros upto the length becomes atleast 9 characters.
                if (_strParsedReqId.Length < 9)
                {
                    StringBuilder sb = new StringBuilder(_strParsedReqId);
                    for (int k = 0; k < (9 - _strParsedReqId.Length); k++)
                    {
                        sb.Insert(0, '1');
                    }
                    _strParsedReqId = sb.ToString();
                }
                oneTimePassword = _strParsedReqId;
            }

            if (oneTimePassword.Length >= length)
            {
                oneTimePassword = oneTimePassword.Substring(0, length);
            }
            return oneTimePassword;
        }

        public string InsertLoginOTP(LoginDTO loginDTO)
        {
            _loggerInstance.Information("Insert Login Data received - " + loginDTO.ToString(), 502);
            try
            {
                LoginDTO loginDTOIns = new LoginDTO();
                loginDTOIns.UserId = loginDTO.UserId;
                loginDTOIns.RoleMasterId = loginDTO.RoleMasterId;
                loginDTOIns.OTPGenerated = loginDTO.OTPPrefix + GenerateOTP();
                loginDTOIns.OTPCreatedTime = DateTime.Now;
                loginDTOIns.OTPValidTill = loginDTO.OTPValidTill;
                loginDTOIns.OTPSentByChannelId = 1;
                loginDTOIns.CreatedBy = loginDTO.CreatedBy;
                _loggerInstance.Information("InsertLoginOTP OTPCreatedTime: " + loginDTOIns.OTPCreatedTime.ToString(), 502);
                _loggerInstance.Information("InsertLoginOTP OTPValidTill: " + loginDTOIns.OTPValidTill.ToString(), 502);
                _loggerInstance.Information("InsertLoginOTP sp start", 502);
                string strMsg = ManageLoginDataAccessManager.InsertLoginOTP(loginDTOIns);
                _loggerInstance.Information("InsertLoginOTP sp end : " + strMsg, 502);
                return strMsg;
            }
            catch (Exception ex)
            {
                _loggerInstance.Information("InsertLoginOTP Exception - " + ex.ToString(), 502);
                return "Error";
                //DataAccessExceptionHandler.HandleExceptioan(ref ex);
            }
        }



        public List<LoginDTO> ChangePassword(LoginDTO loginDTO)
        {
            _loggerInstance.Information("Change Password Data received - " + loginDTO.ToString(), 502);

            List<LoginDTO> getAprroved = new List<LoginDTO>();

            try
            {
                string EnvironmentCode =ServiceConfigurationDataAccessManager.GetValueFormAppSettings("EnvironmentCode");
                loginDTO.EnvironmentCode = EnvironmentCode;
                string strMsg = ManageLoginDataAccessManager.ChangePassword<string>(loginDTO);
                strMsg = JSONAndXMLSerializer.XMLtoJSON(strMsg);
                JObject servicesConfiguartionURL = (JObject)JsonConvert.DeserializeObject(strMsg);
                string userId = servicesConfiguartionURL["Login"]["LoginList"]["LoginId"].ToString();
                string UserName = servicesConfiguartionURL["Login"]["LoginList"]["UserName"].ToString();
                loginDTO.UserId = Convert.ToInt64(userId);
                loginDTO.Username = UserName;
                getAprroved.Add(loginDTO);
                return getAprroved;
            }
            catch (Exception ex)
            {
                _loggerInstance.Information("Change Password Error - " + ex.ToString(), 502);
                return getAprroved;
                //DataAccessExceptionHandler.HandleExceptioan(ref ex);
            }
        }


        public List<LoginDTO> ContactInformationUpDate(LoginDTO loginDTO)
        {
            _loggerInstance.Information("Change Password Data received - " + loginDTO.ToString(), 502);

            List<LoginDTO> getAprroved = new List<LoginDTO>();

            try
            {
                string strMsg = ManageLoginDataAccessManager.ContactInformationUpDate<string>(loginDTO);
                strMsg = JSONAndXMLSerializer.XMLtoJSON(strMsg);
                JObject servicesConfiguartionURL = (JObject)JsonConvert.DeserializeObject(strMsg);
                string userId = servicesConfiguartionURL["Login"]["LoginList"]["LoginId"].ToString();
                string isValid = servicesConfiguartionURL["Login"]["LoginList"]["IsValid"].ToString();
                loginDTO.UserId = Convert.ToInt64(userId);
                loginDTO.IsValid = isValid;
                getAprroved.Add(loginDTO);

                string apiRequest = "http://localhost:56393/api/ManageLogin/UpdateUserIdentityAlternateUserName"; // API Url for testing
                string Jsonformat = "{\"UserId\": " + loginDTO.UserId + ",\"AlternateUserName\": \"" + loginDTO.MobileNo + "\"}";
                string userIdentityServiceAction = "UpdateUserIdentityAlternateUserName";
                DataTable dtUserIdentityInformationURL =ServiceConfigurationDataAccessManager.GetServicesConfiguartionURL<DataTable>(userIdentityServiceAction);
                if (dtUserIdentityInformationURL.Rows.Count > 0)
                    apiRequest = dtUserIdentityInformationURL.Rows[0]["ServicesURL"].ToString();

                //Sending the API HTTP request
                if (apiRequest != string.Empty)
                {
                    GRClientRequest orderListRequest = new GRClientRequest(apiRequest, "POST", Jsonformat);
                    string respText = orderListRequest.GetResponse();
                }

                return getAprroved;
            }
            catch (Exception ex)
            {
                _loggerInstance.Information("Change Password Error - " + ex.ToString(), 502);
                return getAprroved;
                //DataAccessExceptionHandler.HandleExceptioan(ref ex);
            }
        }


        public List<LoginDTO> ValidateUserNameAndMobileNumber(LoginDTO loginDTO)
        {
            _loggerInstance.Information("Change Password Data received - " + loginDTO.ToString(), 502);

            List<LoginDTO> getAprroved = new List<LoginDTO>();

            try
            {
                string strMsg = ManageLoginDataAccessManager.ValidateUserNameAndMobileNumber<string>(loginDTO);
                strMsg = JSONAndXMLSerializer.XMLtoJSON(strMsg);
                JObject servicesConfiguartionURL = (JObject)JsonConvert.DeserializeObject(strMsg);
                string userId = servicesConfiguartionURL["Login"]["LoginList"]["LoginId"].ToString();
                loginDTO.UserId = Convert.ToInt64(userId);
                getAprroved.Add(loginDTO);
                return getAprroved;
            }
            catch (Exception ex)
            {
                _loggerInstance.Information("Change Password Error - " + ex.ToString(), 502);
                return getAprroved;
                //DataAccessExceptionHandler.HandleExceptioan(ref ex);
            }
        }



        public string UpdateLoginOTPRegenerated(LoginDTO loginDTO)
        {
            _loggerInstance.Information("Update Login Data received - " + loginDTO.ToString(), 502);
            try
            {
                LoginDTO loginDTOIns = new LoginDTO();
                loginDTOIns.UserId = loginDTO.UserId;
                loginDTOIns.Username = loginDTO.Username;
                loginDTOIns.OTPGenerated = loginDTO.OTPPrefix + GenerateOTP();
                loginDTOIns.OTPCreatedTime = DateTime.Now;
                loginDTOIns.OTPValidTill = loginDTO.OTPValidTill;
                loginDTOIns.OTPSentByChannelId = 1;
                loginDTOIns.IsOTPUsed = false;
                loginDTOIns.IsCurrentOTPRegenerated = true;
                loginDTOIns.UserAndDeviceDetails = loginDTO.UserAndDeviceDetails;
                string strMsg = ManageLoginDataAccessManager.UpdateRegeneratedLoginOTP(loginDTOIns);

                return strMsg;
            }
            catch (Exception ex)
            {
                _loggerInstance.Information("UpdateLoginOTPRegenerated Error - " + ex.ToString(), 502);
                return "Error";
                //DataAccessExceptionHandler.HandleExceptioan(ref ex);
            }
        }


        public IEnumerable<LoginUserDTO> GetChildUserList(LoginDTO loginDTO)
        {
            IEnumerable<LoginUserDTO> loginList = null;

            string xmlenquiryList = ManageLoginDataAccessManager.GetChildUserList<string>(loginDTO);
            if (xmlenquiryList != null)
            {
                loginList = ObjectXMLSerializer<LoginUserDTO>.Load(xmlenquiryList).LoginList;
            }
            return loginList;
        }

        public IEnumerable<UserOutletMappingDTO> GetSuperUserListV2(UserOutletMappingDTO userOutletMappingDTO)
        {
            IEnumerable<UserOutletMappingDTO> loginList = null;

            string xmlenquiryList = ManageLoginDataAccessManager.GetSuperUserListV2<string>(userOutletMappingDTO);
            if (xmlenquiryList != null)
            {
                loginList = ObjectXMLSerializer<UserOutletMappingDTO>.Load(xmlenquiryList).UserOutletMappingList;
            }
            return loginList;
        }

        public UserOutletMappingDTO SaveUserOutletMapping(UserOutletMappingDTO userOutletMappingDTO)
        {
            string enquiryDTOXML = ObjectXMLSerializer<UserOutletMappingDTO>.Save(userOutletMappingDTO);

            DataTable dtSaveProduct = new DataTable();
            dtSaveProduct = ManageLoginDataAccessManager.SaveUserOutletMapping<DataTable>(enquiryDTOXML);
            if (dtSaveProduct.Rows.Count > 0)
            {
                userOutletMappingDTO.UserOutletMappingId = Convert.ToInt64(dtSaveProduct.Rows[0]["UserOutletMappingId"].ToString());
            }

            return userOutletMappingDTO;

        }

        public UserOutletMappingDTO UpdateUserOutletMapping(UserOutletMappingDTO userOutletMappingDTO)
        {
            string enquiryDTOXML = ObjectXMLSerializer<UserOutletMappingDTO>.Save(userOutletMappingDTO);

            DataTable dtSaveProduct = new DataTable();
            dtSaveProduct = ManageLoginDataAccessManager.UpdateUserOutletMapping<DataTable>(enquiryDTOXML);
            if (dtSaveProduct.Rows.Count > 0)
            {
                userOutletMappingDTO.UserId = Convert.ToInt64(dtSaveProduct.Rows[0]["UserId"].ToString());
            }

            return userOutletMappingDTO;

        }


        public IEnumerable<LoginDTO> ValidateUserName(LoginDTO loginDTO)
        {

            IEnumerable<LoginDTO> returnObject = null;
            try
            {

                string loginXml = ObjectXMLSerializer<LoginDTO>.Save(loginDTO);
                string output = ManageLoginDataAccessManager.GetB2BLogin<string>(loginXml);
                if (output != null)
                {
                    returnObject = ObjectXMLSerializer<LoginDTO>.Load(output).LoginList;
                }
                return returnObject;
            }
            catch (Exception ex)
            {
                _loggerInstance.Information("ValidateUserName exception : " + ex.Message, 502);
            }
            return returnObject;

        }

        public LoginDTO ActivateDeactivateCompany(LoginDTO loginDTO)
        {
            DataSet dsCompany = new DataSet();
            DataTable dtSaveProduct = new DataTable();
            dsCompany = ManageLoginDataAccessManager.ActivateDeactivateCompany<DataSet>(loginDTO);
            if (dsCompany.Tables.Count > 0)
            {
                if (dsCompany.Tables[0].Rows.Count > 0)
                {
                    loginDTO.ErrorMessage = Convert.ToString(dsCompany.Tables[0].Rows[0]["ErrorMessage"].ToString());
                }
                if (dsCompany.Tables[1].Rows.Count > 0)
                {
                    string output = Convert.ToString(dsCompany.Tables[1].Rows[0][0].ToString());
                    if (output != null)
                    {
                        loginDTO.LoginList = ObjectXMLSerializer<LoginDTO>.Load(output).LoginList;
                    }
                }
            }

            return loginDTO;

        }

        public LoginDTO UpdateLanguageForUser(LoginDTO loginDTO)
        {

            DataTable dtSaveProduct = new DataTable();
            dtSaveProduct = ManageLoginDataAccessManager.UpdateLanguageForUser<DataTable>(loginDTO);

            return loginDTO;

        }

        public IEnumerable<AppNotificationsDTO> GetAppNotificationsByUserId(LoginDTO loginDTO)
        {
            IEnumerable<AppNotificationsDTO> loginList = null;

            string xmlenquiryList = ManageLoginDataAccessManager.GetAppNotificationsByUserId<string>(loginDTO);
            if (xmlenquiryList != null)
            {
                loginList = ObjectXMLSerializer<AppNotificationsDTO>.Load(xmlenquiryList).AppNotificationsList;
            }
            return loginList;
        }


        public IEnumerable<LoginDTO> GetB2BLogin(LoginDTO loginDTO)
        {
            IEnumerable<LoginDTO> loginDTOs = null;
            string loginXml = ObjectXMLSerializer<LoginDTO>.Save(loginDTO);
            string output = ManageLoginDataAccessManager.GetB2BLogin<string>(loginXml);
            if (output != null)
            {
                loginDTOs = ObjectXMLSerializer<LoginDTO>.Load(output).LoginList;
            }
            return loginDTOs;
        }

        public string UpdateToken(long userId, string tokenString)
        {
            string output = ManageLoginDataAccessManager.UpdateToken(userId, tokenString);
            return output;
        }

        public string DeleteLoginByLoginId(dynamic Json)
        {
            _loggerInstance.Information("DeleteLoginByLoginId Manager Call Start", 502);
            string output = ManageLoginDataAccessManager.DeleteLoginByLoginId(Json);
            _loggerInstance.Information("DeleteLoginByLoginId Manager Call End", 502);
            return output;
        }


        public string GetLocationDetails(dynamic Json)
        {
            _loggerInstance.Information("GetLocationDetails Manager Call Start", 502);
            string output = ManageLoginDataAccessManager.GetLocationDetails(Json);
            _loggerInstance.Information("GetLocationDetails Manager Call End", 502);
            return output;
        }


        public string GetProfileDetails(dynamic Json)
        {
            _loggerInstance.Information("GetProfileDetails Manager Call Start", 502);
            string output = ManageLoginDataAccessManager.GetProfileDetails(Json);
            _loggerInstance.Information("GetProfileDetails Manager Call End", 502);
            return output;
        }

        public LoginDTO CheckDuplicateUser(LoginDTO loginDTO)
        {
            string loginDTOXML = ObjectXMLSerializer<LoginDTO>.Save(loginDTO);
            string xmluserList = ManageLoginDataAccessManager.CheckDuplicateUser<string>(loginDTOXML);
            if (!string.IsNullOrEmpty(xmluserList))
            {
                loginDTO = ObjectXMLSerializer<LoginDTO>.Load(xmluserList);
            }
            return loginDTO;
        }

        public JObject CreateLogin(dynamic json)
        {


            //JObject returnObject = new JObject();
            dynamic returnObject = new ExpandoObject();
            String output = "";

            try
            {
                string notValidFields = "";
                string Input1 = JsonConvert.SerializeObject(json);
                JObject obj1 = (JObject)JsonConvert.DeserializeObject(Input1);
                dynamic jsonobj = obj1["Json"]["Login"];

                var newObj = new JObject
                {
                    ["Json"] = jsonobj,
                };


                string Input = JsonConvert.SerializeObject(json);

                JObject obj = (JObject)JsonConvert.DeserializeObject(Input);

                int salt = Password.CreateRandomSalt();
                string haspassword = Password.HashPassword(obj["Json"]["Login"]["Password"].ToString(), salt);
                string roleId = obj["Json"]["Login"]["RoleMasterId"].ToString();
                string createdBy = obj["Json"]["Login"]["CreatedBy"].ToString();
                if (createdBy == "")
                {
                    createdBy = "1";
                }
                obj["Json"]["Login"]["HashedPassword"] = haspassword;
                obj["Json"]["Login"]["PasswordSalt"] = salt;

                string EnvironmentCode = ServiceConfigurationDataAccessManager.GetValueFormAppSettings("EnvironmentCode");
                obj["Json"]["Login"]["EnvironmentCode"] = EnvironmentCode;

                Input = JsonConvert.SerializeObject(obj);

                var loginId = obj["Json"]["Login"]["LoginId"].ToString();
                if (loginId == "0")
                {
                    output = ManageLoginDataAccessManager.CreateLogin(Input);
                    #region Insert in OTP
                    string settingsOutput = SettingMasterDataAccessManager.GetSettingMasterBySettingParameter("ChannelValidityPeriodManual");
                    JObject retObj = (JObject)JsonConvert.DeserializeObject(output);
                    JObject retObjSetting = (JObject)JsonConvert.DeserializeObject(settingsOutput);
                    string settingsOutputOTP = SettingMasterDataAccessManager.GetSettingMasterBySettingParameter("OTPPrefix");
                    JObject retObjSettingOTP = (JObject)JsonConvert.DeserializeObject(settingsOutputOTP);
                    string settingsOutputCBUrl = SettingMasterDataAccessManager.GetSettingMasterBySettingParameter("ClientBaseURLForLoginOTP");
                    JObject retObjSettingCBUrl = (JObject)JsonConvert.DeserializeObject(settingsOutputCBUrl);
                    var loginIdC = retObj["Json"]["LoginId"].ToString();
                    var validityDurationInMins = retObjSetting["Json"]["SettingValue"].ToString();
                    var otpPrefix = retObjSettingOTP["Json"]["SettingValue"].ToString();
                    var clientBaseAddress = retObjSettingCBUrl["Json"]["SettingValue"].ToString();
                    int otpValidityInMins = 60;
                    if (validityDurationInMins != null)
                    {
                        if (validityDurationInMins.ToString() != "")
                        {
                            bool res;
                            int a;
                            res = int.TryParse(validityDurationInMins, out a);
                            if (res)
                            {
                                otpValidityInMins = Convert.ToInt32(validityDurationInMins);
                            }
                        }
                    }
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(clientBaseAddress.ToString());
                    //client.BaseAddress = new Uri("http://54.72.44.112:8085/");
                    //client.BaseAddress = new Uri("http://localhost:51201/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var loginDTO = new LoginDTO();
                    loginDTO.UserId = Convert.ToInt64(loginIdC);
                    loginDTO.RoleMasterId = Convert.ToInt32(roleId);
                    loginDTO.OTPCreatedTime = DateTime.Now;
                    loginDTO.OTPValidTill = DateTime.Now.AddMinutes(otpValidityInMins);
                    loginDTO.OTPSentByChannelId = 1;
                    loginDTO.CreatedBy = Convert.ToInt64(createdBy);
                    loginDTO.OTPPrefix = otpPrefix.ToString();
                    loginDTO.OTPGenerated = loginDTO.OTPPrefix + GenerateOTP();

                    _loggerInstance.Information("InsertLoginOTP OTPCreatedTime: " + loginDTO.OTPCreatedTime.ToString(), 502);
                    _loggerInstance.Information("InsertLoginOTP OTPValidTill: " + loginDTO.OTPValidTill.ToString(), 502);
                    _loggerInstance.Information("InsertLoginOTP sp start", 502);
                    string strMsg = ManageLoginDataAccessManager.InsertLoginOTP(loginDTO);
                    _loggerInstance.Information("InsertLoginOTP sp end : " + strMsg, 502);

                    //var response = client.PostAsJsonAsync("api/ManageLogin/InsertLoginOTP", loginDTO).Result;
                    //if (response.IsSuccessStatusCode)
                    //{
                    //}
                    //else
                    //{
                    //}
                    #endregion
                }
                else
                {
                    output = ManageLoginDataAccessManager.UpdateLogin(Input);
                }
                if (output != null)
                {
                    returnObject = (JObject)JsonConvert.DeserializeObject(output);

                    string MultitanencyApiUrl =ServiceConfigurationDataAccessManager.GetValueFormAppSettings("MultitanencyApiUrl");

                    string CreateMultitanencyLoginApi = MultitanencyApiUrl + "/Multitenancy/CreateMultitanencyLogin";

                    string EnvironmentCode1 =ServiceConfigurationDataAccessManager.GetValueFormAppSettings("EnvironmentCode");
                    returnObject["Json"]["EnvironmentCode"] = EnvironmentCode1;
                    JObject jsonObj12 = returnObject["Json"];
                    
                    var newLoginObj = new JObject
                    {
                        ["Login"] = jsonObj12
                    };

                    var newObj1 = new JObject
                    {
                        ["Json"] = newLoginObj
                    };

                    string newJsonString = Convert.ToString(newObj1);

                    JObject objMul = new JObject();
                    //create the constructor with post type and few data
                    GRClientRequest myRequest = new GRClientRequest(CreateMultitanencyLoginApi, "POST", newJsonString);
                    //show the response string on the console screen.
                    string outputResponse = myRequest.GetResponse();
                    if (outputResponse != null)
                    {
                        objMul = (JObject)JsonConvert.DeserializeObject(outputResponse);
                    }

                }




            }
            catch (Exception ex)
            {

            }
            return returnObject;
        }

        public List<LoginDTO> ValidateUserAndOnboardingReference(LoginDTO loginDTO)
        {
            _loggerInstance.Information("Login Data received - " + loginDTO.ToString(), 502);
            List<LoginDTO> getAprroved = new List<LoginDTO>();
            try
            {
                if (ValidateData(loginDTO))
                {
                    string a = ManageLoginDataAccessManager.ValidateUserAndOnboardingReference<string>(loginDTO);
                    if (a != null)
                    {
                        a = JSONAndXMLSerializer.XMLtoJSON(a);
                        JObject servicesConfiguartionURL = (JObject)JsonConvert.DeserializeObject(a);
                        string userId = servicesConfiguartionURL["Login"]["LoginList"]["LoginId"].ToString();
                        loginDTO.UserId = Convert.ToInt64(userId);
                        loginDTO.IsValid = "YES";
                        loginDTO.ReasonCode = "OK";
                    }
                    else
                    {
                        loginDTO.IsValid = "NO";
                        loginDTO.ReasonCode = "User not found";
                    }
                    getAprroved.Add(loginDTO);
                    return getAprroved;
                }
                else
                {
                    loginDTO.IsValid = "NO";
                    loginDTO.ReasonCode = "Invalid Data";
                    getAprroved.Add(loginDTO);
                    return getAprroved;
                }

            }
            catch (Exception ex)
            {
                loginDTO.IsValid = "NO";
                loginDTO.ReasonCode = "Error!! Please contact system administrator.";
                _loggerInstance.Information("ValidateData Exception - " + ex.ToString(), 502);
                getAprroved.Add(loginDTO);
                return getAprroved;
            }
        }

        public List<UserIdentityDTO> LoadAllEnvironmentUrl(UserIdentityDTO loginDTO)
        {
            _loggerInstance.Information("Manager LoadAllEnvironmentUrl - " + loginDTO.ToString(), 502);

            List<UserIdentityDTO> userIdentityDTOs = new List<UserIdentityDTO>();

            try
            {
                DataTable dtConfigurationApiList = new DataTable();
                dtConfigurationApiList = ManageLoginDataAccessManager.LoadAllEnvironmentUrl<DataTable>(loginDTO);
                userIdentityDTOs = Extension.DataTableToList<UserIdentityDTO>(dtConfigurationApiList);
                return userIdentityDTOs;
            }
            catch (Exception ex)
            {
                _loggerInstance.Information("LoadAllEnvironmentUrl Error - " + ex.ToString(), 502);
                return userIdentityDTOs;
                //DataAccessExceptionHandler.HandleExceptioan(ref ex);
            }
        }

        public string GetSettingMasterBySettingParameter(string settingParameter)
        {
            string settingParam = ManageLoginDataAccessManager.GetSettingMasterBySettingParameter(settingParameter);
            return settingParam;
        }

        public LoginDTO ValidateToken(LoginDTO loginDTO)
        {
            LoginDTO loginDtoNew = null;

            string xmlLoginDto = ManageLoginDataAccessManager.ValidateToken<string>(loginDTO);
            if (!string.IsNullOrEmpty(xmlLoginDto))
            {
                loginDtoNew = ObjectXMLSerializer<LoginDTO>.Load(xmlLoginDto);
            }
            return loginDtoNew;
        }


        public List<LoginDTO> GetLoginDetailsById(LoginDTO loginDTO)
        {
            List<LoginDTO> getAprroved = new List<LoginDTO>();
            try
            {
                if (ValidateData(loginDTO))
                {
                    DataTable dt = new DataTable();
                    string a = ManageLoginDataAccessManager.GetLoginDetailsById<string>(loginDTO);
                    if (a != null)
                    {
                        a = JSONAndXMLSerializer.XMLtoJSON(a);
                        JObject servicesConfiguartionURL = (JObject)JsonConvert.DeserializeObject(a);
                        string accessKey = servicesConfiguartionURL["Login"]["LoginList"]["AccessKey"].ToString();
                        string isActive = servicesConfiguartionURL["Login"]["LoginList"]["IsActive"].ToString();
                        loginDTO.AccessKey = accessKey;
                        loginDTO.IsActive = isActive == "1" ? true : false;
                    }
                    getAprroved.Add(loginDTO);
                    return getAprroved;
                }
                else
                {
                    return getAprroved;
                }
            }
            catch (Exception ex)
            {
                return getAprroved;
            }
        }

        public List<UserIdentityDTO> UpdateUserIdentityAlternateUserName(UserIdentityDTO userIdentityDTO)
        {
            _loggerInstance.Information("Update AlternateUserName Data received - " + userIdentityDTO.ToString(), 502);
            List<UserIdentityDTO> getAprroved = new List<UserIdentityDTO>();
            try
            {
                getAprroved.Add(userIdentityDTO);
                string strMsg = ManageLoginDataAccessManager.UpdateUserIdentityAlternateUserName<string>(userIdentityDTO);
                return getAprroved;
            }
            catch (Exception ex)
            {
                _loggerInstance.Information("Update AlternateUserName Error - " + ex.ToString(), 502);
                return getAprroved;
            }
        }


        public string UpdateLoginHistoryB2BApp(string input)
        {
            string settingParam = "";
            try
            {
                settingParam = ManageLoginDataAccessManager.UpdateLoginHistoryB2BApp(input);
            }
            catch (Exception ex)
            {
                _loggerInstance.Information("Update AlternateUserName Error - " + ex.ToString(), 502);
            }


            return settingParam;
        }


        public LoginDTO UpdateDigitalOnBoardingLink(LoginDTO loginDTO)
        {
            string loginDTOXML = ObjectXMLSerializer<LoginDTO>.Save(loginDTO);
            string xmluserList = ManageLoginDataAccessManager.UpdateDigitalOnBoardingLink<string>(loginDTOXML);
            return loginDTO;
        }


        public LoginDTO ValidateUserTokenKey(LoginDTO loginDTO)
        {
            string loginDTOXML = ObjectXMLSerializer<LoginDTO>.Save(loginDTO);
            string xmluserList = ManageLoginDataAccessManager.ValidateUserTokenKey<string>(loginDTOXML);
            if (!string.IsNullOrEmpty(xmluserList))
            {
                LoginDTO login = ObjectXMLSerializer<LoginDTO>.Load(xmluserList);
                if (login.AccessKey == loginDTO.AccessKey)
                {
                    loginDTO.IsValidLogin = "true";
                }
                else
                {
                    loginDTO.IsValidLogin = "false";
                }
            }
            return loginDTO;
        }

        public string LoadUserProfile(string input)
        {
            string LoadUserProfileParam = "";
            try
            {
                LoadUserProfileParam = ManageLoginDataAccessManager.LoadUserProfile(input);
            }
            catch (Exception ex)
            {
                _loggerInstance.Information("LoadUserProfile Error - " + ex.ToString(), 502);
            }


            return LoadUserProfileParam;
        }

        public LoginDTO GetLoginUserProfileByUserLoginId(LoginDTO loginDto)
        {
            var objLoginDto = new LoginDTO();
            var xmlReturn = ManageLoginDataAccessManager.GetUserUserProfileByUserLoginId<string>(loginDto.LoginId,loginDto.Username);
            if (xmlReturn != null)
            {
                objLoginDto = ObjectXMLSerializer<LoginDTO>.Load(xmlReturn);
            }
            return objLoginDto;

        }

        public PasswordPolicyDTO GetPasswordPolicyByRoleMasterId(PasswordPolicyDTO passwordPolicyDto)
        {
            var passwordPolicyData = new PasswordPolicyDTO();

            var xmlReturn = ManageLoginDataAccessManager.GetPolicyByRolemasterId<string>(passwordPolicyDto.RoleMasterId);
            if (xmlReturn != null)
            {
                passwordPolicyData = ObjectXMLSerializer<PasswordPolicyDTO>.Load(xmlReturn);
            }

            return passwordPolicyData;
        }

        public LoginDTO GetLoginByUserProfileId(LoginDTO loginDetail)
        {
            var lstUsers = new LoginDTO();
            var xmlReturn = ManageLoginDataAccessManager.GetLoginByUserProfileId<string>(Convert.ToString(loginDetail.ProfileId));
            if (xmlReturn != null)
            {
                lstUsers = ObjectXMLSerializer<LoginDTO>.Load(xmlReturn);
            }
            return lstUsers;
        }

        public Int64 UpdateLoginPassword(LoginDTO loginDto)
        {
            Int64 _returnvalue;
            var xmlInward = ObjectXMLSerializer<LoginDTO>.Save(loginDto);
            _returnvalue = ManageLoginDataAccessManager.UpdateLoginPassword<int>(xmlInward);
            return _returnvalue;
        }

        public string LoadLookUpListByCritiera(string input)
        {
            string LoadLookUpListByCritiera = "";
            try
            {
                LoadLookUpListByCritiera = ManageLoginDataAccessManager.LoadLookUpListByCritiera(input);
            }
            catch (Exception ex)
            {
                _loggerInstance.Information("LoadLookUpListByCritiera Error - " + ex.ToString(), 502);
            }


            return LoadLookUpListByCritiera;
        }

        public string LoadDocumetRequired()
        {
            string LoadDocumetRequired = "";
            try
            {
                LoadDocumetRequired = ManageLoginDataAccessManager.LoadDocumetRequired();
            }
            catch (Exception ex)
            {
                _loggerInstance.Information("LoadDocumetRequired Error - " + ex.ToString(), 502);
            }


            return LoadDocumetRequired;
        }

        public string GetAllCompanyListByDropDown(string input)
        {
            string GetAllCompanyListByDropDown = "";
            try
            {
                GetAllCompanyListByDropDown = ManageLoginDataAccessManager.GetAllCompanyListByDropDown(input);
            }
            catch (Exception ex)
            {
                _loggerInstance.Information("GetAllCompanyListByDropDown Error - " + ex.ToString(), 502);
            }


            return GetAllCompanyListByDropDown;
        }

        public string GetAllShipToListByCompanyId(string input)
        {
            string GetAllShipToListByCompanyId = "";
            try
            {
                GetAllShipToListByCompanyId = ManageLoginDataAccessManager.GetAllShipToListByCompanyId(input);
            }
            catch (Exception ex)
            {
                _loggerInstance.Information("GetAllShipToListByCompanyId Error - " + ex.ToString(), 502);
            }


            return GetAllShipToListByCompanyId;
        }

        public string LoadAllRoleMaster(string input)
        {
            string LoadAllRoleMaster = "";
            try
            {
                LoadAllRoleMaster = ManageLoginDataAccessManager.LoadAllRoleMaster(input);
            }
            catch (Exception ex)
            {
                _loggerInstance.Information("LoadAllRoleMaster Error - " + ex.ToString(), 502);
            }


            return LoadAllRoleMaster;
        }

        public string LoadLoginDetailByLoginId(string input)
        {
            string LoadLoginDetailByLoginId = "";
            try
            {
                LoadLoginDetailByLoginId = ManageLoginDataAccessManager.LoadLoginDetailByLoginId(input);
            }
            catch (Exception ex)
            {
                _loggerInstance.Information("LoadLoginDetailByLoginId Error - " + ex.ToString(), 502);
            }


            return LoadLoginDetailByLoginId;
        }

        public string GetPageRoleWiseAccessDetailByRoleORUserID(string input)
        {
            string GetPageRoleWiseAccessDetailByRoleORUserID = "";
            try
            {
                GetPageRoleWiseAccessDetailByRoleORUserID = ManageLoginDataAccessManager.GetPageRoleWiseAccessDetailByRoleORUserID(input);
            }
            catch (Exception ex)
            {
                _loggerInstance.Information("GetPageRoleWiseAccessDetailByRoleORUserID Error - " + ex.ToString(), 502);
            }


            return GetPageRoleWiseAccessDetailByRoleORUserID;
        }

        public string InsertAndUpdateRoleWisePageMappingAndRoleWiseFieldAccess(string input)
        {
            string InsertAndUpdateRoleWisePageMappingAndRoleWiseFieldAccess = "";
            try
            {
                InsertAndUpdateRoleWisePageMappingAndRoleWiseFieldAccess = ManageLoginDataAccessManager.InsertAndUpdateRoleWisePageMappingAndRoleWiseFieldAccess(input);
            }
            catch (Exception ex)
            {
                _loggerInstance.Information("InsertAndUpdateRoleWisePageMappingAndRoleWiseFieldAccess Error - " + ex.ToString(), 502);
            }


            return InsertAndUpdateRoleWisePageMappingAndRoleWiseFieldAccess;
        }


    }
}
