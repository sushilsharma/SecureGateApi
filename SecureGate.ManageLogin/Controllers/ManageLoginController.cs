using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SecureGate.APIController.Framework.AppLogger;
using SecureGate.APIController.Framework.Controllers;
using SecureGate.ManageLogin.Business;
using SecureGate.ManageLogin.DTO;
using System.Net;
using Newtonsoft.Json;
using SecureGate.Framework.PasswordUtility;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Dynamic;
using Newtonsoft.Json.Converters;
using SecureGate.Framework;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using SecureGate.APIController.Framework;
using SecureGate.DataAccess.Common;
using SecureGate.Framework.DataAccess;

namespace SecureGate.ManageLogin.Controllers
{
    public class ManageLoginController : BaseAPIController
    {
        protected override EnumLoggerType LoggerName
        {
            get { return EnumLoggerType.Login; }
        }

        public string AccesKey = "AIzaSyD9I7yVwNbw86m1F1";

        //[GRAuthorize]
        [HttpPost]
        [Route("/api/[controller]/InsertLoginOTP")]
        public IActionResult InsertLoginOTP([FromBody] LoginDTO loginDTO) // Error code: 404 Not found
        {
            try
            {
                // Get Records for Approve Enquiry 
                //string Input1 = JsonConvert.SerializeObject(json);
                ILoginManager loginManager = new LoginManager(LoggerInstance);
                //LoginDTO loginDTO = new LoginDTO();
                string a = loginManager.InsertLoginOTP(loginDTO);
                return Ok(a);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error(ex.Message, 404);
                return new CustomErrorActionResult(HttpStatusCode.NotFound, "Not Found");
            }
        }

        [HttpPost]
        [Route("/api/[controller]/GetProfileDetails")]
        public IActionResult GetProfileDetails(dynamic Json)
        {
            JObject returnObject = new JObject();
            try
            {
                string Input = JsonConvert.SerializeObject(Json);
                ILoginManager loginManager = new LoginManager(LoggerInstance);
                string output = loginManager.GetProfileDetails(Input);
                returnObject = (JObject)JsonConvert.DeserializeObject(output);
            }
            catch (Exception)
            {

            }

            return Ok(returnObject);

        }


        [HttpPost]
        [Route("/api/[controller]/ValidateLogin")]
        public IActionResult ValidateLogin([FromBody] LoginDTO loginDTO) // Error code: 404 Not found
        {
            try
            {
                LoggerInstance.Information("ValidateLogin Call Start", 502);
                // Get Records for Approve Enquiry 
                ILoginManager loginManager = new LoginManager(LoggerInstance);
                List<LoginDTO> loginDTOs = loginManager.ValidateLogin(loginDTO);
                LoggerInstance.Information("ValidateLogin Call End", 502);
                return Ok(loginDTOs);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error("ValiateLogidn API Exception: " + ex.Message, 404);
                return new CustomErrorActionResult(HttpStatusCode.NotFound, "Not Found");
            }
        }


        [HttpPost]
        [Route("/api/[controller]/ValidateOTP")]
        public IActionResult ValidateOTP([FromBody] LoginDTO loginDTO) // Error code: 404 Not found
        {
            try
            {
                LoggerInstance.Information("ValidateOTP Call Start", 502);
                // Get Records for Approve Enquiry 
                ILoginManager loginManager = new LoginManager(LoggerInstance);
                List<LoginDTO> loginDTOs = loginManager.ValidateOTP(loginDTO);
                LoggerInstance.Information("ValidateOTP Call End", 502);
                return Ok(loginDTOs);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error("ValidateOTP API Exception: " + ex.Message, 404);
                return new CustomErrorActionResult(HttpStatusCode.NotFound, "Not Found");
            }
        }


        [HttpPost]
        [Route("/api/[controller]/ValidateUserAndOnboardingReference")]
        public IActionResult ValidateUserAndOnboardingReference([FromBody] LoginDTO loginDTO) // Error code: 404 Not found
        {
            try
            {
                LoggerInstance.Information("ValidateLogin Call Start", 502);
                // Get Records for Approve Enquiry 
                ILoginManager loginManager = new LoginManager(LoggerInstance);
                List<LoginDTO> loginDTOs = loginManager.ValidateUserAndOnboardingReference(loginDTO);
                LoggerInstance.Information("ValidateLogin Call End", 502);
                return Ok(loginDTOs);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error("ValidateLogin API Exception: " + ex.Message, 404);
                return new CustomErrorActionResult(HttpStatusCode.NotFound, "Not Found");
            }
        }


        [HttpPost]
        [Route("/api/[controller]/RegenerateOTP")]
        public IActionResult RegenerateOTP([FromBody] LoginDTO loginObject)
        {
            LoggerInstance.Information("Enter In RegenerateOTP API", 502);
            dynamic returnObject = new ExpandoObject();
            try
            {

                LoggerInstance.Information("RegenerateOTP " + loginObject.UserId, 502);
                LoggerInstance.Information("RegenerateOTP Username " + loginObject.Username, 502);

                #region Insert in OTP


                var configurationBuilder = new ConfigurationBuilder();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                configurationBuilder.AddJsonFile(path, false);


                var root = configurationBuilder.Build();
                var validityDurationInMins = root.GetSection("ChannelValidityPeriodManual").Value;


                var loginIdC = loginObject.UserId;
                var otpPrefix = "";
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

                var loginDTO = new LoginDTO();
                loginDTO.UserId = Convert.ToInt64(loginIdC);
                loginDTO.Username = loginObject.Username;
                loginDTO.OTPValidTill = DateTime.Now.AddMinutes(otpValidityInMins);
                loginDTO.OTPPrefix = otpPrefix.ToString();
                loginDTO.UserAndDeviceDetails = loginObject.UserAndDeviceDetails;

                LoggerInstance.Information("Business Manager RegenerateOTP " + loginObject.UserId, 502);

                ILoginManager loginManager = new LoginManager(LoggerInstance);
                string updatedUserOtp = loginManager.UpdateLoginOTPRegenerated(loginDTO);

                LoggerInstance.Information("output Business Manager RegenerateOTP " + Convert.ToString(updatedUserOtp), 502);

                if (!String.IsNullOrEmpty(Convert.ToString(updatedUserOtp)))
                {
                    loginObject.ErrorMessage = "success";
                }
                else
                {
                    loginObject.ErrorMessage = "error";
                }
                #endregion
            }
            catch (Exception ex)
            {
                LoggerInstance.Error("Regen OTP error:" + ex.Message + " StackTrace:" + ex.StackTrace, 404);
            }
            return Ok(loginObject);
        }

        [HttpPost]
        [Route("/api/[controller]/ChangePassword")]
        public IActionResult ChangePassword([FromBody] LoginDTO loginDTO) // Error code: 404 Not found
        {
            try
            {
                int salt = Password.CreateRandomSalt();
                string password = loginDTO.Password;
                string haspassword = Password.HashPassword(password, salt);
                loginDTO.PasswordSalt = salt;
                loginDTO.HashedPassword = haspassword;
                ILoginManager loginManager = new LoginManager(LoggerInstance);
                List<LoginDTO> loginDTOs = loginManager.ChangePassword(loginDTO);
                return Ok(loginDTOs);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error(ex.Message, 404);
                return new CustomErrorActionResult(HttpStatusCode.NotFound, "Not Found");
            }
        }


        [HttpPost]
        [Route("/api/[controller]/ContactInformationUpDate")]
        public IActionResult ContactInformationUpDate([FromBody] LoginDTO loginDTO) // Error code: 404 Not found
        {
            try
            {
                ILoginManager loginManager = new LoginManager(LoggerInstance);
                List<LoginDTO> loginDTOs = loginManager.ContactInformationUpDate(loginDTO);
                return Ok(loginDTOs);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error(ex.Message, 404);
                return new CustomErrorActionResult(HttpStatusCode.NotFound, "Not Found");
            }
        }


        [HttpPost]
        [Route("/api/[controller]/ValidateUserNameAndMobileNumber")]
        public IActionResult ValidateUserNameAndMobileNumber([FromBody] LoginDTO loginDTO) // Error code: 404 Not found
        {
            try
            {
                ILoginManager loginManager = new LoginManager(LoggerInstance);
                List<LoginDTO> a = loginManager.ValidateUserNameAndMobileNumber(loginDTO);
                return Ok(a);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error(ex.Message, 404);
                return new CustomErrorActionResult(HttpStatusCode.NotFound, "Not Found");
            }
        }


        [HttpPost]
        [Route("/api/[controller]/GetChildUserList")]
        public IActionResult GetChildUserList([FromBody] LoginDTO loginDTO) // Error code: 404 Not found
        {
            try
            {
                ILoginManager loginManager = new LoginManager(LoggerInstance);
                LoggerInstance.Information("GetChildUserList Call Started", 5002);
                IEnumerable<LoginUserDTO> output = loginManager.GetChildUserList(loginDTO);
                LoggerInstance.Information("GetChildUserList Call End", 5002);
                return Ok(output);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error(ex.Message, 404);
                return new CustomErrorActionResult(HttpStatusCode.NotFound, "Not Found");
            }
        }

        [HttpPost]
        [Route("/api/[controller]/GetSuperUserList")]
        public IActionResult GetSuperUserList([FromBody] UserOutletMappingDTO userOutletMappingDTO) // Error code: 404 Not found
        {
            try
            {
                ILoginManager loginManager = new LoginManager(LoggerInstance);
                LoggerInstance.Information("GetSuperUserList Call Started", 5002);
                IEnumerable<UserOutletMappingDTO> output = loginManager.GetSuperUserListV2(userOutletMappingDTO);
                LoggerInstance.Information("GetSuperUserList Call End", 5002);
                return Ok(output);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error(ex.Message, 404);
                return new CustomErrorActionResult(HttpStatusCode.NotFound, "Not Found");
            }
        }

        [HttpPost]
        [Route("/api/[controller]/SaveUserOutletMapping")]
        public IActionResult SaveUserOutletMapping([FromBody] UserOutletMappingDTO userOutletMappingDTO)
        {
            ILoginManager loginManager = new LoginManager(LoggerInstance);
            UserOutletMappingDTO output = new UserOutletMappingDTO();

            if (userOutletMappingDTO.UserOutletMappingList.Count > 0)
            {
                if (userOutletMappingDTO.UserOutletMappingList[0].NewAddedItem == 1)
                {
                    LoggerInstance.Information("UpdateUserOutletMapping Call Started", 5002);
                    output = loginManager.UpdateUserOutletMapping(userOutletMappingDTO);
                    LoggerInstance.Information("UpdateUserOutletMapping Call End", 5002);
                }
                else
                {
                    LoggerInstance.Information("SaveUserOutletMapping Call Started", 5002);
                    output = loginManager.SaveUserOutletMapping(userOutletMappingDTO);
                    LoggerInstance.Information("SaveUserOutletMapping Call End", 5002);
                }
            }

            return Ok(output);
        }

        [HttpPost]
        [Route("/api/[controller]/ValidateUserName")]
        public IActionResult ValidateUserName([FromBody] LoginDTO loginDTO) // Error code: 404 Not found
        {
            try
            {
                LoggerInstance.Information("ValidateUserName process start", 50011);

                //var obj = Convert.ToString(json);
                //JObject jsonObj = JObject.Parse(obj);
                //string Input = JsonConvert.SerializeObject(jsonObj);
                LoggerInstance.Information("ValidateUserName process input: " + loginDTO.Username, 50011);
                // Get Records for Approve Enquiry 
                ILoginManager loginManager = new LoginManager(LoggerInstance);
                IEnumerable<LoginDTO> obj = loginManager.ValidateUserName(loginDTO);

                LoggerInstance.Information("ValidateUserName process End", 50011);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error(ex.Message, 404);
                return new CustomErrorActionResult(HttpStatusCode.NotFound, "Not Found");
            }
        }


        [HttpPost]
        [Route("/api/[controller]/ActivateDeactivateCompany")]
        public IActionResult ActivateDeactivateCompany([FromBody] LoginDTO loginDTO)
        {
            ILoginManager loginManager = new LoginManager(LoggerInstance);

            LoggerInstance.Information("ActivateDeactivateCompany Call Started", 5002);
            LoginDTO loginDTOOutput = loginManager.ActivateDeactivateCompany(loginDTO);
            LoggerInstance.Information("ActivateDeactivateCompany Call End", 5002);

            return Ok(loginDTOOutput);
        }

        [HttpPost]
        [Route("/api/[controller]/UpdateLanguageForUser")]
        public IActionResult UpdateLanguageForUser([FromBody] LoginDTO loginDTO)
        {
            ILoginManager loginManager = new LoginManager(LoggerInstance);

            LoggerInstance.Information("UpdateLanguageForUser Call Started", 5002);
            LoginDTO loginDTOOutput = loginManager.UpdateLanguageForUser(loginDTO);
            LoggerInstance.Information("UpdateLanguageForUser Call End", 5002);

            return Ok(loginDTO);
        }

        [HttpPost]
        [Route("/api/[controller]/GetAppNotificationsByUserId")]
        public IActionResult GetAppNotificationsByUserId([FromBody] LoginDTO loginDTO) // Error code: 404 Not found
        {

            try
            {
                ILoginManager loginManager = new LoginManager(LoggerInstance);
                LoggerInstance.Information("GetAppNotificationsByUserId Call Started", 5002);
                IEnumerable<AppNotificationsDTO> output = loginManager.GetAppNotificationsByUserId(loginDTO);
                LoggerInstance.Information("GetAppNotificationsByUserId Call End", 5002);
                return Ok(output);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error(ex.Message, 404);
                return new CustomErrorActionResult(HttpStatusCode.NotFound, "Not Found");
            }
        }

        [HttpPost]
        [Route("/api/[controller]/ValidateB2BLogin")]
        public IActionResult ValidateB2BLogin([FromBody] LoginDTO loginDTO)
        {
            //string Input = JsonConvert.SerializeObject(Json);
            JObject returnObject = new JObject();
            LoggerInstance.Information("UserName : " + loginDTO.Username, 5002);
            try
            {
                ILoginManager loginManager = new LoginManager(LoggerInstance);

                IEnumerable<LoginDTO> output = loginManager.GetB2BLogin(loginDTO);


                if (output != null)
                {
                    if (output.Any())
                    {
                        List<LoginDTO> loginDTOs = output.ToList();

                        for (int i = 0; i < loginDTOs.Count; i++)
                        {

                            var userPassword = loginDTO.UserPassword.ToString();
                            var PasswordSalt = int.Parse(Convert.ToString(loginDTOs[i].PasswordSalt));
                            string hasehedPassword = Password.HashPassword(userPassword, PasswordSalt);
                            if (loginDTOs[i].HashedPassword.ToString() == hasehedPassword)
                            {
                                var tokenHandler = new JwtSecurityTokenHandler();
                                var key = Encoding.ASCII.GetBytes("GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk");

                                var now = DateTime.Now;
                                var claims = new Claim[]
                                    {
                                        new Claim(JwtRegisteredClaimNames.Sub, loginDTOs[i].UserId.ToString()),
                                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                        new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
                                    };

                                var tokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuerSigningKey = true,
                                    IssuerSigningKey = new SymmetricSecurityKey(key),
                                    ValidateIssuer = true,
                                    ValidIssuer = "glassRUN",
                                    ValidateAudience = true,
                                    ValidAudience = "enduser",
                                    ValidateLifetime = true,
                                    ClockSkew = TimeSpan.Zero,
                                    RequireExpirationTime = true,
                                };

                                var jwt = new JwtSecurityToken(
                                       issuer: "glassRUN",
                                       audience: "enduser",
                                       claims: claims,
                                       notBefore: now,
                                       expires: now.Add(TimeSpan.FromMinutes(240)),
                                       signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                                    );

                                var tokenString = tokenHandler.WriteToken(jwt);
                                loginDTOs[i].Token = tokenString;

                                string op = loginManager.UpdateToken(Convert.ToInt64(loginDTOs[i].UserId), tokenString);

                            }
                            else
                            {
                                output = new List<LoginDTO>();
                            }

                        }
                    }
                }
                return Ok(output);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error(ex.Message, 404);
                return new CustomErrorActionResult(HttpStatusCode.NotFound, "Not Found");
            }

        }



        [HttpPost]
        [Route("/api/[controller]/UpdateRegeneratedLoginOTP")]
        public IActionResult UpdateRegeneratedLoginOTP([FromBody] LoginDTO loginDTO) // Error code: 404 Not found
        {
            try
            {
                // Get Records for Approve Enquiry 
                //string Input1 = JsonConvert.SerializeObject(json);
                ILoginManager loginManager = new LoginManager(LoggerInstance);
                //LoginDTO loginDTO = new LoginDTO();
                string a = loginManager.UpdateLoginOTPRegenerated(loginDTO);
                return Ok(a);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error(ex.Message, 404);
                return new CustomErrorActionResult(HttpStatusCode.NotFound, "Not Found");
            }
        }


        [HttpPost]
        [Route("/api/[controller]/DeleteLoginByLoginId")]
        public IActionResult DeleteLoginByLoginId([FromBody] dynamic Json)
        {
            string Input = JsonConvert.SerializeObject(Json);

            ILoginManager loginManager = new LoginManager(LoggerInstance);

            string output = loginManager.DeleteLoginByLoginId(Input);

            JObject returnObject = new JObject();

            if (output != null)
            {
                returnObject = (JObject)JsonConvert.DeserializeObject(output);
            }

            return Ok(returnObject);
        }




        [HttpPost]
        [Route("/api/[controller]/LoadProfileLocationDetails")]
        public IActionResult LoadProfileLocationDetails([FromBody] dynamic Json)
        {
            JObject returnObject = new JObject();
            try
            {
                ILoginManager loginManager = new LoginManager(LoggerInstance);
                string Input = JsonConvert.SerializeObject(Json);
                string output = loginManager.GetLocationDetails(Input);
                returnObject = (JObject)JsonConvert.DeserializeObject(output);
            }
            catch (Exception ex)
            {



            }



            return Ok(returnObject);

        }


        [HttpPost]
        [Route("/api/[controller]/GetAllData")]
        public IActionResult GetAllData([FromBody] dynamic Json)
        {
            LoggerInstance.Information("GetAllData", 5002);

            JObject returnObject = new JObject();
            try
            {
                string Input = JsonConvert.SerializeObject(Json);

                LoggerInstance.Information("GetAllData" + Input, 5002);


                dynamic objOutput = new ExpandoObject();

                var expConverter = new ExpandoObjectConverter();

                objOutput = JsonConvert.DeserializeObject<ExpandoObject>(Input, expConverter);

                List<dynamic> updateApiList = new List<dynamic>();
                updateApiList = objOutput.Json.UpdatedAPIList;





                dynamic newJson = new ExpandoObject();
                dynamic FinalJson = new ExpandoObject();


                ///Get All Item According to Company
                //GetItemsForB2B
                List<dynamic> itemB2B = updateApiList.Where(x => x.APIName == "GetItemsForB2B").ToList();
                if (itemB2B.Count > 0)
                {
                    string GetItemsForB2BApi = ServiceConfigurationDataAccessManager.GetServiceActionURL("GetItemsForB2B");
                    GRClientRequest GetItemsForB2BRequest = new GRClientRequest(GetItemsForB2BApi, "POST", Input);
                    string GetItemsForB2BResponse = GetItemsForB2BRequest.GetResponse();
                    JObject jsonItemsForB2BResponse = (JObject)JsonConvert.DeserializeObject(GetItemsForB2BResponse);
                    newJson.GetItemsForB2B = jsonItemsForB2BResponse;
                }
                else
                {
                    newJson.GetItemsForB2B = "";
                }







                //Get Profile Details
                string GetSQLCurrentDatetimeAndZoneApi = ServiceConfigurationDataAccessManager.GetServiceActionURL("GetSQLCurrentDatetimeAndZone");
                GRClientRequest GetSQLCurrentDatetimeAndZoneRequest = new GRClientRequest(GetSQLCurrentDatetimeAndZoneApi, "POST", Input);
                string GetSQLCurrentDatetimeAndZoneResponse = GetSQLCurrentDatetimeAndZoneRequest.GetResponse();
                JObject jsonDatetimeAndZoneResponse = (JObject)JsonConvert.DeserializeObject(GetSQLCurrentDatetimeAndZoneResponse);
                newJson.GetSQLCurrentDatetimeAndZone = jsonDatetimeAndZoneResponse;



                //Get Profile Details
                List<dynamic> loadPhotoSubDApp = updateApiList.Where(x => x.APIName == "LoadPhotoSubDApp").ToList();
                if (loadPhotoSubDApp.Count > 0)
                {
                    string ProfileApi = ServiceConfigurationDataAccessManager.GetServiceActionURL("LoadPhotoSubDApp");
                    GRClientRequest ProfileRequest = new GRClientRequest(ProfileApi, "POST", Input);
                    string outputProfileResponse = ProfileRequest.GetResponse();
                    JObject jsonObjOutputResponse = (JObject)JsonConvert.DeserializeObject(outputProfileResponse);
                    newJson.LoadPhotoSubDApp = jsonObjOutputResponse;
                }
                else
                {
                    newJson.LoadPhotoSubDApp = "";
                }




                List<dynamic> GetFavouriteItemsForB2B = updateApiList.Where(x => x.APIName == "GetFavouriteItemsForB2B").ToList();
                if (GetFavouriteItemsForB2B.Count > 0)
                {
                    string tFavouriteItemsApi = ServiceConfigurationDataAccessManager.GetServiceActionURL("GetFavouriteItemsForB2B");
                    GRClientRequest tFavouriteItemsRequest = new GRClientRequest(tFavouriteItemsApi, "POST", Input);
                    string outputtFavouriteItemsResponse = tFavouriteItemsRequest.GetResponse();
                    JObject jsonoutputtFavouriteItemsResponse = (JObject)JsonConvert.DeserializeObject(outputtFavouriteItemsResponse);
                    newJson.GetFavouriteItemsForB2B = jsonoutputtFavouriteItemsResponse;
                }
                else
                {
                    newJson.GetFavouriteItemsForB2B = "";
                }





                List<dynamic> GetAllSupplierDetailsB2BApp = updateApiList.Where(x => x.APIName == "GetAllSupplierDetailsB2BApp").ToList();
                if (GetAllSupplierDetailsB2BApp.Count > 0)
                {
                    string SupplierItemsApi = ServiceConfigurationDataAccessManager.GetServiceActionURL("GetAllSupplierDetailsB2BApp");
                    GRClientRequest SupplierRequest = new GRClientRequest(SupplierItemsApi, "POST", Input);
                    string SupplierResponse = SupplierRequest.GetResponse();
                    JObject jsonSupplierResponseResponse = (JObject)JsonConvert.DeserializeObject(SupplierResponse);
                    newJson.GetAllSupplierDetailsB2BApp = jsonSupplierResponseResponse;
                }
                else
                {
                    newJson.GetAllSupplierDetailsB2BApp = "";
                }




                List<dynamic> LoadProfileLocationDetails = updateApiList.Where(x => x.APIName == "LoadProfileLocationDetails").ToList();
                if (LoadProfileLocationDetails.Count > 0)
                {
                    string LoadProfileLocationDetailsApi = ServiceConfigurationDataAccessManager.GetServiceActionURL("LoadProfileLocationDetails");
                    GRClientRequest LoadProfileLocationRequest = new GRClientRequest(LoadProfileLocationDetailsApi, "POST", Input);
                    string LoadProfileLocationResponse = LoadProfileLocationRequest.GetResponse();
                    JObject jsonLoadProfileLocationResponse = (JObject)JsonConvert.DeserializeObject(LoadProfileLocationResponse);
                    newJson.LoadProfileLocationDetails = jsonLoadProfileLocationResponse;
                }
                else
                {
                    newJson.LoadProfileLocationDetails = "";
                }


                List<dynamic> GetAllNotificationB2BApp = updateApiList.Where(x => x.APIName == "GetAllNotificationB2BApp").ToList();
                if (GetAllNotificationB2BApp.Count > 0)
                {
                    string NotificationApi = ServiceConfigurationDataAccessManager.GetServiceActionURL("GetAllNotificationB2BApp");
                    GRClientRequest NotificationRequest = new GRClientRequest(NotificationApi, "POST", Input);
                    string NotificationResponse = NotificationRequest.GetResponse();
                    JObject jsonObjectNotificationResponse = (JObject)JsonConvert.DeserializeObject(NotificationResponse);
                    newJson.GetAllNotificationB2BApp = jsonObjectNotificationResponse;
                }
                else
                {
                    newJson.GetAllNotificationB2BApp = "";
                }


                List<dynamic> GetAllCustomerListB2BApp = updateApiList.Where(x => x.APIName == "GetAllCustomerListB2BApp").ToList();
                if (GetAllCustomerListB2BApp.Count > 0)
                {
                    Input = Input.Replace("ManageSupplier", "ManageCustomer");
                    string CustomerListApi = ServiceConfigurationDataAccessManager.GetServiceActionURL("GetAllCustomerListB2BApp");
                    GRClientRequest CustomerListRequest = new GRClientRequest(CustomerListApi, "POST", Input);
                    string CustomerListResponse = CustomerListRequest.GetResponse();
                    JObject jsonCustomerListResponse = (JObject)JsonConvert.DeserializeObject(CustomerListResponse);
                    newJson.GetAllCustomerListB2BApp = jsonCustomerListResponse;
                }
                else
                {
                    newJson.GetAllCustomerListB2BApp = "";
                }



                string ActivePromotionApi = ServiceConfigurationDataAccessManager.GetServiceActionURL("GetAllActivePromotionItemListForCustomer");
                GRClientRequest ActivePromotionRequest = new GRClientRequest(ActivePromotionApi, "POST", Input);
                string ActivePromotionResponse = ActivePromotionRequest.GetResponse();
                JArray jsonActivePromotionResponse = (JArray)JsonConvert.DeserializeObject(ActivePromotionResponse);
                newJson.GetAllActivePromotionItemListForCustomer = jsonActivePromotionResponse;



                List<dynamic> GetOwnCatalogItemsForB2B = updateApiList.Where(x => x.APIName == "GetOwnCatalogItemsForB2B").ToList();
                if (GetOwnCatalogItemsForB2B.Count > 0)
                {
                    string CatalogItemsApi = ServiceConfigurationDataAccessManager.GetServiceActionURL("GetOwnCatalogItemsForB2B");
                    GRClientRequest CatalogItemsRequest = new GRClientRequest(CatalogItemsApi, "POST", Input);
                    string CatalogItemsResponse = CatalogItemsRequest.GetResponse();
                    JArray jsonCatalogItemsResponse = (JArray)JsonConvert.DeserializeObject(CatalogItemsResponse);
                    newJson.GetOwnCatalogItemsForB2B = jsonCatalogItemsResponse;
                }
                else
                {
                    newJson.GetOwnCatalogItemsForB2B = "";
                }



                List<dynamic> GetCustomerDetailsBySupplier = updateApiList.Where(x => x.APIName == "GetCustomerDetailsBySupplier").ToList();
                if (GetCustomerDetailsBySupplier.Count > 0)
                {
                    string CustumerGroupApi = ServiceConfigurationDataAccessManager.GetServiceActionURL("GetCustomerDetailsBySupplier");
                    GRClientRequest CustumerGroupRequest = new GRClientRequest(CustumerGroupApi, "POST", Input);
                    string CustumerGroupResponse = CustumerGroupRequest.GetResponse();
                    JObject jsonCustumerGroupResponse = (JObject)JsonConvert.DeserializeObject(CustumerGroupResponse);
                    newJson.GetCustomerDetailsBySupplier = jsonCustumerGroupResponse;

                }
                else
                {
                    newJson.GetCustomerDetailsBySupplier = "";
                }

                string glassRUNApiUrl = ServiceConfigurationDataAccessManager.GetValueFormAppSettings("glassRUNApiUrl");

                string EnquiryDetailsApi = glassRUNApiUrl + "/ManageEnquiry/EnquiryList";



                JObject obj = (JObject)JsonConvert.DeserializeObject(Input);

                JObject jsonObj = JObject.Parse(obj["Json"]["EnquirySearchDto"].ToString());

                string Input1 = JsonConvert.SerializeObject(jsonObj);

                GRClientRequest EnquiryDetailsRequest = new GRClientRequest(EnquiryDetailsApi, "POST", Input1);
                string EnquiryDetailsResponse = EnquiryDetailsRequest.GetResponse();
                JArray jsonEnquiryDetailsResponse = (JArray)JsonConvert.DeserializeObject(EnquiryDetailsResponse);
                newJson.GetEnquiryDetailsOfCustomerB2B = jsonEnquiryDetailsResponse;

                string glassRUNOrederApiUrl = ServiceConfigurationDataAccessManager.GetValueFormAppSettings("glassRUNApiUrlOrder");

                string OrderDetailsApi = glassRUNOrederApiUrl + "/Order/OrderList";



                JObject objOrder = (JObject)JsonConvert.DeserializeObject(Input);

                JObject jsonOrderObj = JObject.Parse(obj["Json"]["OrderSearchDto"].ToString());

                string InputOrder = JsonConvert.SerializeObject(jsonOrderObj);
                LoggerInstance.Information("Inpute of order" + InputOrder + "And Url =" + OrderDetailsApi, 5002);
                GRClientRequest OrderDetailsRequest = new GRClientRequest(OrderDetailsApi, "POST", InputOrder);
                string OrderDetailsResponse = OrderDetailsRequest.GetResponse();
                JArray jsonOrderDetailsResponse = (JArray)JsonConvert.DeserializeObject(OrderDetailsResponse);
                newJson.GetOrderDetailsOfCustomerB2B = jsonOrderDetailsResponse;
                LoggerInstance.Information("Rsponse of order" + newJson.GetOrderDetailsOfCustomerB2B, 5002);

                FinalJson.Json = newJson;


                string Input11 = JsonConvert.SerializeObject(FinalJson);

                JObject FinalJobject = (JObject)JsonConvert.DeserializeObject(Input11);

                returnObject = FinalJobject;
            }
            catch (Exception ex)
            {
                string d = ex.Message;
                LoggerInstance.Information("Exception : " + ex.Message, 5002);
            }
            return Ok(returnObject);
        }





        [HttpPost]
        [Route("/api/[controller]/CheckDuplicateUser")]
        public IActionResult CheckDuplicateUser([FromBody] LoginDTO loginDTO) // Error code: 404 Not found
        {
            try
            {
                ILoginManager loginManager = new LoginManager(LoggerInstance);
                LoginDTO loginDTONew = new LoginDTO();
                loginDTONew = loginManager.CheckDuplicateUser(loginDTO);
                return Ok(loginDTONew);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error(ex.Message, 404);
                return new CustomErrorActionResult(HttpStatusCode.NotFound, "Not Found");
            }
        }

        [HttpPost]
        [Route("/api/[controller]/CreateLogin")]
        public IActionResult CreateLogin([FromBody] dynamic json) // Error code: 404 Not found
        {
            try
            {
                LoggerInstance.Information("CreateLogin process start", 50011);

                var obj = Convert.ToString(json);
                JObject jsonObj = JObject.Parse(obj);
                string Input = JsonConvert.SerializeObject(jsonObj);
                LoggerInstance.Information("CreateLogin process input: " + Input, 50011);
                // Get Records for Approve Enquiry 
                ILoginManager loginManager = new LoginManager(LoggerInstance);
                obj = loginManager.CreateLogin(jsonObj);

                LoggerInstance.Information("CreateLogin process End", 50011);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error(ex.Message, 404);
                return new CustomErrorActionResult(HttpStatusCode.NotFound, "Not Found");
            }
        }

        [HttpPost]
        [Route("/api/[controller]/LoadAllEnvironmentUrl")]
        public IActionResult LoadAllEnvironmentUrl([FromBody] UserIdentityDTO loginDTO) // Error code: 404 Not found
        {
            LoggerInstance.Information("LoadAllEnvironmentUrl - ", 502);
            try
            {
                ILoginManager loginManager = new LoginManager(LoggerInstance);
                List<UserIdentityDTO> userIdentityDTOs = loginManager.LoadAllEnvironmentUrl(loginDTO);
                return Ok(userIdentityDTOs);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error(ex.Message, 404);
                return new CustomErrorActionResult(HttpStatusCode.NotFound, "Not Found");
            }
        }


        [HttpPost]
        [Route("/api/[controller]/ValidateToken")]
        public IActionResult ValidateToken([FromBody] LoginDTO loginDTO) // Error code: 404 Not found
        {
            try
            {
                LoggerInstance.Information("ValidateToken Call Start", 502);
                ILoginManager loginManager = new LoginManager(LoggerInstance);

                if (loginDTO.TokenString != null)
                {
                    // get value from header
                    Microsoft.Extensions.Primitives.StringValues tString = loginDTO.TokenString;
                    string tokenString = tString.ToString().Replace("Bearer ", "");

                    var key = Encoding.ASCII.GetBytes("GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk");
                    var handler = new JwtSecurityTokenHandler();

                    var jwtOutput = string.Empty;

                    if (tokenString != "")
                    {
                        if (!handler.CanReadToken(tokenString))
                            throw new Exception("The token doesn't seem to be in a proper JWT format.");

                        var token1 = handler.ReadJwtToken(tokenString);

                        var jwtHeader = JsonConvert.SerializeObject(token1.Header.Select(h => new { h.Key, h.Value }));
                        var jwtPayload = JsonConvert.SerializeObject(token1.Claims.Select(c => new { c.Type, c.Value }));

                        JArray jArray = (JArray)JsonConvert.DeserializeObject(jwtPayload);

                        Dictionary<string, string> a = new Dictionary<string, string>();
                        foreach (JObject parsedObject in jArray.Children<JObject>())
                        {
                            string type = "", value = "";
                            foreach (JProperty parsedProperty in parsedObject.Properties())
                            {
                                string propertyName = parsedProperty.Name;
                                if (propertyName.ToLower().Equals("type"))
                                {
                                    type = (string)parsedProperty.Value;
                                }
                                if (propertyName.ToLower().Equals("value"))
                                {
                                    value = (string)parsedProperty.Value;
                                }
                            }
                            a.Add(type, value);
                        }
                        long userId = Convert.ToInt64(a["sub"]);
                        DateTime expirationTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(a["exp"])).LocalDateTime;
                        if (expirationTime > DateTime.Now)
                        {
                            LoginDTO loginDtoValidate = new LoginDTO();
                            loginDtoValidate.UserId = userId;
                            List<LoginDTO> lDTO = loginManager.GetLoginDetailsById(loginDtoValidate);

                            if (lDTO.Count > 0)
                            {
                                loginDtoValidate = lDTO[0];
                            }

                            if (loginDtoValidate != null && !string.IsNullOrEmpty(loginDtoValidate.AccessKey))
                            {
                                if (tokenString != loginDtoValidate.AccessKey)
                                {
                                    return new UnauthorizedResult();
                                }
                                if (!loginDtoValidate.IsActive)
                                {
                                    return new UnauthorizedResult();
                                }
                            }
                            else
                            {
                                return new UnauthorizedResult();
                            }
                        }
                        else
                        {
                            return new UnauthorizedResult();
                        }
                    }
                    else
                    {
                        return new UnauthorizedResult();
                    }
                }
                else
                {
                    return new UnauthorizedResult();
                }

                LoginDTO loginDto = loginManager.ValidateToken(loginDTO);
                LoggerInstance.Information("ValidateToken Call End", 502);

                string returnData = "";
                JObject returnObject = new JObject();
                if (loginDto != null)
                {
                    returnData = "{\"ExternalClientId\": \"" + loginDto.CustomerCode.Trim() + "\",\"CustomerName\": \"" + loginDto.CustomerName.Trim() + "\"}";
                    returnObject = (JObject)JsonConvert.DeserializeObject(returnData);
                }
                else
                {
                    return new UnauthorizedResult();
                }

                return Ok(returnObject);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error("ValidateToken API Exception: " + ex.Message, 404);
                return new CustomErrorActionResult(HttpStatusCode.NotFound, "Not Found");
            }
        }

        [HttpPost]
        [Route("/api/[controller]/UpdateUserIdentityAlternateUserName")]
        public IActionResult UpdateUserIdentityAlternateUserName([FromBody] UserIdentityDTO userIdentityDTO) // Error code: 404 Not found
        {
            try
            {
                ILoginManager loginManager = new LoginManager(LoggerInstance);
                List<UserIdentityDTO> a = loginManager.UpdateUserIdentityAlternateUserName(userIdentityDTO);
                return Ok(a);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error(ex.Message, 404);
                return new CustomErrorActionResult(HttpStatusCode.NotFound, "Not Found");
            }
        }


        [HttpPost]
        [Route("/api/[controller]/UpdateLoginHistoryB2BApp")]
        public IActionResult UpdateLoginHistoryB2BApp([FromBody] dynamic Json)
        {
            JObject returnObject = new JObject();
            try
            {
                string Input = JsonConvert.SerializeObject(Json);
                LoggerInstance.Information("UpdateLoginHistoryB2BApp --> Input String : " + Input, 502);
                ILoginManager loginManager = new LoginManager(LoggerInstance);
                string output = loginManager.UpdateLoginHistoryB2BApp(Input);
                LoggerInstance.Information("UpdateLoginHistoryB2BApp --> Output String : " + output, 502);
                returnObject = (JObject)JsonConvert.DeserializeObject(output);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error("UpdateLoginHistoryB2BApp Exception : " + ex.Message, 404);
            }

            return Ok(returnObject);

        }



        [HttpPost]
        [Route("/api/[controller]/UpdateDigitalOnBoardingLink")]
        public IActionResult UpdateDigitalOnBoardingLink([FromBody] LoginDTO loginDTO)
        {

            try
            {
                LoggerInstance.Information("UpdateDigitalOnBoardingLink --> Input ", 502);
                ILoginManager loginManager = new LoginManager(LoggerInstance);
                LoginDTO loginDTOOutput = loginManager.UpdateDigitalOnBoardingLink(loginDTO);
                LoggerInstance.Information("UpdateDigitalOnBoardingLink --> Output ", 502);
                return Ok(loginDTOOutput);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error("UpdateLoginHistoryB2BApp Exception : " + ex.Message, 404);
                return new CustomErrorActionResult(HttpStatusCode.NotFound, "Not Found");
            }



        }



        [HttpPost]
        [Route("/api/[controller]/ValidateUserTokenKey")]
        public IActionResult ValidateUserTokenKey([FromBody] LoginDTO loginDTO) // Error code: 404 Not found
        {
            try
            {
                ILoginManager loginManager = new LoginManager(LoggerInstance);
                LoginDTO loginDTOs = loginManager.ValidateUserTokenKey(loginDTO);
                return Ok(loginDTOs);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error(ex.Message, 404);
                return new CustomErrorActionResult(HttpStatusCode.NotFound, "Not Found");
            }
        }

        [HttpPost]
        [Route("/api/[controller]/LoadUserProfile")]
        public IActionResult LoadUserProfile([FromBody] dynamic Json)
        {
            JObject returnObject = new JObject();
            try
            {
                string Input = JsonConvert.SerializeObject(Json);
                LoggerInstance.Information("LoadUserProfile --> Input String : " + Input, 502);
                ILoginManager loginManager = new LoginManager(LoggerInstance);
                string output = loginManager.LoadUserProfile(Input);
                LoggerInstance.Information("LoadUserProfile --> Output String : " + output, 502);
                returnObject = (JObject)JsonConvert.DeserializeObject(output);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error("LoadUserProfile Exception : " + ex.Message, 404);
            }

            return Ok(returnObject);

        }

        [HttpPost]
        [Route("/api/[controller]/RegenerateOTPV2")]
        public IActionResult RegenerateOTPV2([FromBody] LoginDTO loginObject)
        {
            LoggerInstance.Information("Enter In RegenerateOTPV2 API", 502);
            dynamic returnObject = new ExpandoObject();
            try
            {

                LoggerInstance.Information("RegenerateOTPV2 " + loginObject.UserId, 502);
                LoggerInstance.Information("RegenerateOTPV2 Username " + loginObject.Username, 502);

                #region Insert in OTP


                var configurationBuilder = new ConfigurationBuilder();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                configurationBuilder.AddJsonFile(path, false);


                var root = configurationBuilder.Build();
                var validityDurationInMins = root.GetSection("ChannelValidityPeriodManual").Value;


                var loginIdC = loginObject.UserId;
                var otpPrefix = "";
                DataTable dtStatusesForAudit =SettingMasterDataAccessManager.GetSettingMasterBySettingParameter<DataTable>("ProposedDeliveryDate");
                if (dtStatusesForAudit.Rows.Count > 0)
                {
                    otpPrefix = dtStatusesForAudit.Rows[0]["SettingValue"].ToString();
                }

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

                var loginDTO = new LoginDTO();
                loginDTO.UserId = Convert.ToInt64(loginIdC);
                loginDTO.Username = loginObject.Username;
                loginDTO.OTPValidTill = DateTime.Now.AddMinutes(otpValidityInMins);
                loginDTO.OTPPrefix = otpPrefix.ToString();
                loginDTO.UserAndDeviceDetails = loginObject.UserAndDeviceDetails;

                LoggerInstance.Information("Business Manager RegenerateOTPV2 " + loginObject.UserId, 502);

                ILoginManager loginManager = new LoginManager(LoggerInstance);
                string updatedUserOtp = loginManager.UpdateLoginOTPRegenerated(loginDTO);

                LoggerInstance.Information("output Business Manager RegenerateOTPV2 " + Convert.ToString(updatedUserOtp), 502);

                if (!String.IsNullOrEmpty(Convert.ToString(updatedUserOtp)))
                {
                    loginObject.ErrorMessage = "success";
                }
                else
                {
                    loginObject.ErrorMessage = "error";
                }
                #endregion
            }
            catch (Exception ex)
            {
                LoggerInstance.Error("Regen OTP V2 error:" + ex.Message + " StackTrace:" + ex.StackTrace, 404);
            }
            return Ok(loginObject);
        }


        [HttpPost]
        [Route("/api/[controller]/ChangePasswordV2")]
        public IActionResult ChangePasswordV2([FromBody] LoginDTO loginDTO) // Error code: 404 Not found
        {
            try
            {
                ErrorInfo errorInfo = new ErrorInfo();
                var loginDto = new LoginDTO();
                //var loginDto1 = new LoginDTO();
                long retVal = 0;
                int currentPasswordsalt = Password.CreateRandomSalt();

                int salt = Password.CreateRandomSalt();
                string haspassword = Password.HashPassword(loginDTO.Password, salt);
                byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(loginDTO.Password);
                string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

                if (AccesKey == loginDTO.AccessKey)
                {

                    ILoginManager loginManager = new LoginManager(LoggerInstance);
                    var objloginDto1 = new LoginDTO
                    {
                        LoginId = loginDTO.UserId,
                        Username = loginDTO.Username
                    };
                    loginDto = loginManager.GetLoginUserProfileByUserLoginId(objloginDto1);

                    bool IsAllowSameaslastpassword = false;
                    PasswordPolicyDTO passwordPolicyDto = new PasswordPolicyDTO();

                    var objPasswordPolicyDto = new PasswordPolicyDTO
                    {
                        RoleMasterId = Convert.ToInt64(loginDto.LoginList[0].RoleMasterId)
                    };
                    passwordPolicyDto.PasswordPolicyList = loginManager.GetPasswordPolicyByRoleMasterId(objPasswordPolicyDto).PasswordPolicyList;
                    if (passwordPolicyDto.PasswordPolicyList.Count > 0)
                    {
                        IsAllowSameaslastpassword = passwordPolicyDto.PasswordPolicyList.FirstOrDefault().NewPasswordShouldNotMatchNoOfLastPassword ?? false;
                    }


                    if (IsAllowSameaslastpassword)
                    {
                        bool ststus = GetpasswordPolicybyRoleMasterid(Convert.ToString(loginDto.LoginList[0].RoleMasterId), loginDTO.Password);
                        if (ststus)
                        {
                            loginDTO.PasswordSalt = salt;
                            loginDTO.HashedPassword = haspassword;
                            List<LoginDTO> loginDTOs = loginManager.ChangePassword(loginDTO);
                        }
                        else
                        {
                            errorInfo.retVal = 3;

                            string errorMessage = "";
                            for (int i = 0; i < passwordPolicyDto.PasswordPolicyList.Count; i++)
                            {
                                var Passpolicy = passwordPolicyDto.PasswordPolicyList[i];
                                errorMessage = "Password length should be between " + Passpolicy.MinimumPasswordLength + "-" + Passpolicy.MaximumPasswordLength + " characters";

                                bool IsLowerCaseAllowed = Passpolicy.IsLowerCaseAllowed ?? false;
                                bool IsNumberAllowed = Passpolicy.IsNumberAllowed ?? false;
                                bool IsSpecialCharacterAllowed = Passpolicy.IsSpecialCharacterAllowed ?? false;
                                bool IsUpperCaseAllowed = Passpolicy.IsUpperCaseAllowed ?? false;

                                bool ValExists = false;
                                if (IsUpperCaseAllowed)
                                {
                                    ValExists = true;
                                    errorMessage = errorMessage + " with " + Passpolicy.MinimumUppercaseCharactersRequired + " Upper Case character";
                                }
                                if (IsLowerCaseAllowed)
                                {
                                    ValExists = true;
                                    errorMessage = errorMessage + ", " + Passpolicy.MinimumLowercaseCharactersRequired + " Lower Case character";
                                }
                                if (IsNumberAllowed)
                                {
                                    ValExists = true;
                                    errorMessage = errorMessage + ", " + Passpolicy.MinimumNumericsRequired + " number";
                                }
                                if (IsSpecialCharacterAllowed)
                                {
                                    ValExists = true;
                                    errorMessage = errorMessage + ", " + Passpolicy.MinimumSpecialCharactersRequired + " special character";
                                }

                                if (ValExists)
                                {
                                    errorMessage = errorMessage + " is required.";
                                }

                            }

                            errorInfo.ErrorMessage = errorMessage;
                        }

                    }
                    else
                    {
                        loginDTO.PasswordSalt = salt;
                        loginDTO.HashedPassword = haspassword;
                        List<LoginDTO> loginDTOs = loginManager.ChangePassword(loginDTO);
                    }


                    if (haspassword != loginDto.LoginList[0].HashedPassword)
                    {

                        loginDTO.PasswordSalt = salt;
                        loginDTO.HashedPassword = haspassword;
                        List<LoginDTO> loginDTOs = loginManager.ChangePassword(loginDTO);

                    }
                }
                return Ok(errorInfo);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error(ex.Message, 404);
                return new CustomErrorActionResult(HttpStatusCode.NotFound, "Not Found");
            }
        }

        public bool GetpasswordPolicybyRoleMasterid(string RoleMasterid, string Password)
        {
            bool Ismatched = false;

            PasswordPolicyDTO passwordPolicyDto = new PasswordPolicyDTO();
            ILoginManager loginManager = new LoginManager(LoggerInstance);
            var objPasswordPolicyDto = new PasswordPolicyDTO
            {
                RoleMasterId = Convert.ToInt64(RoleMasterid)
            };
            passwordPolicyDto.PasswordPolicyList = loginManager.GetPasswordPolicyByRoleMasterId(objPasswordPolicyDto).PasswordPolicyList;
            if (passwordPolicyDto.PasswordPolicyList.Count > 0)
            {
                // "^(?=.*?[A-Z])(?=(.*[a-z]){1,})(?=(.*[\d]){1,})(?=(.*[\W]){1,})(?!.*\s).{8,}$"

                // "^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]{2,})[A-Za-z\d@$!%*?&]{8,}$"
                var Passpolicy = passwordPolicyDto.PasswordPolicyList[0];

                bool IsLowerCaseAllowed = Passpolicy.IsLowerCaseAllowed ?? false;
                bool IsNumberAllowed = Passpolicy.IsNumberAllowed ?? false;
                bool IsSpecialCharacterAllowed = Passpolicy.IsSpecialCharacterAllowed ?? false;
                bool IsUpperCaseAllowed = Passpolicy.IsUpperCaseAllowed ?? false;
                string pattern = "^";
                string pattern12 = "[";
                if (IsUpperCaseAllowed)
                {
                    pattern = pattern + "(?=(.*[A-Z]){" + Passpolicy.MinimumUppercaseCharactersRequired + ",})";
                    pattern12 = pattern12 + "A-Z";
                }
                if (IsLowerCaseAllowed)
                {
                    pattern = pattern + "(?=(.*[a-z]){" + Passpolicy.MinimumLowercaseCharactersRequired + ",})";
                    pattern12 = pattern12 + "a-z";
                }
                if (IsNumberAllowed)
                {
                    pattern = pattern + @"(?=(.*[\d]){" + Passpolicy.MinimumNumericsRequired + ",})";
                    pattern12 = pattern12 + @"\d";
                }
                if (IsSpecialCharacterAllowed)
                {
                    string SpecialChar = "@$!%*?&:;.#+-";
                    if (Passpolicy.SpecialCharactersToBeExcluded != null)
                    {
                        var splitChar = (Passpolicy.SpecialCharactersToBeExcluded).Split(',');
                        foreach (var item in splitChar)
                        {
                            SpecialChar = SpecialChar.Replace(item, "");
                        }

                    }
                    pattern = pattern + @"(?=(.*[" + SpecialChar + "]){" + Passpolicy.MinimumSpecialCharactersRequired + ",})" + @"(?!.*\s)";
                    pattern12 = pattern12 + SpecialChar;
                }
                pattern12 = pattern12 + "]";
                pattern = pattern + pattern12 + "{" + Passpolicy.MinimumPasswordLength + "," + Passpolicy.MaximumPasswordLength + "}$";

                if (!string.IsNullOrEmpty(Password))
                {
                    if (!Regex.IsMatch(Password, pattern))
                    {
                        Ismatched = false;
                    }
                    else
                    {
                        Ismatched = true;
                    }

                }


            }
            else
            {
                Ismatched = true;

            }

            return Ismatched;
        }

        [HttpPost]
        [Route("/api/[controller]/LoadLoginCreation")]
        public IActionResult LoadLoginCreation([FromBody] dynamic Json) // Error code: 404 Not found
        {
            try
            {
                dynamic returnObject = new ExpandoObject();
                LoggerInstance.Information("LoadLoginCreation", 502);

                dynamic objOutput = new ExpandoObject();

                string Input = JsonConvert.SerializeObject(Json);

                var expConverter = new ExpandoObjectConverter();
                objOutput = JsonConvert.DeserializeObject<ExpandoObject>(Input, expConverter);

                ILoginManager loginManager = new LoginManager(LoggerInstance);

                dynamic inputObject = new ExpandoObject();

                inputObject.UserId = objOutput.Json.UserId;
                inputObject.PageName = objOutput.Json.PageName;
                inputObject.PageControlName = "CompanyTypeDropDown";

                dynamic orderinJson = new ExpandoObject();
                orderinJson.Json = inputObject;

                string companyTypeStringdata = loginManager.LoadLookUpListByCritiera(JsonConvert.SerializeObject(orderinJson));
                objOutput = JsonConvert.DeserializeObject<ExpandoObject>(companyTypeStringdata, expConverter);
                returnObject.CompanyTypeList = objOutput.Json.LookUpList;

                inputObject.PageControlName = "ContactTypeDropDown";
                string contactTypeListStringdata = loginManager.LoadLookUpListByCritiera(JsonConvert.SerializeObject(orderinJson));
                objOutput = JsonConvert.DeserializeObject<ExpandoObject>(contactTypeListStringdata, expConverter);
                returnObject.ContactTypeList = objOutput.Json.LookUpList;

                inputObject.PageControlName = "LicenseTypeDropDown";
                string licenseTypeListStringdata = loginManager.LoadLookUpListByCritiera(JsonConvert.SerializeObject(orderinJson));
                objOutput = JsonConvert.DeserializeObject<ExpandoObject>(licenseTypeListStringdata, expConverter);
                returnObject.LicenseTypeList = objOutput.Json.LookUpList;


                inputObject.PageControlName = "LanguageTypeDropDown";
                string languageTypeListStringdata = loginManager.LoadLookUpListByCritiera(JsonConvert.SerializeObject(orderinJson));
                if (!string.IsNullOrEmpty(languageTypeListStringdata))
                {
                    objOutput = JsonConvert.DeserializeObject<ExpandoObject>(languageTypeListStringdata, expConverter);
                    returnObject.LanguageTypeList = objOutput.Json.LookUpList;
                }


                inputObject.PageControlName = "DocumentTypeDropDown";
                string documentTypeListStringdata = loginManager.LoadLookUpListByCritiera(JsonConvert.SerializeObject(orderinJson));
                if (!string.IsNullOrEmpty(documentTypeListStringdata))
                {
                    objOutput = JsonConvert.DeserializeObject<ExpandoObject>(documentTypeListStringdata, expConverter);
                    returnObject.DocumentTypeList = objOutput.Json.LookUpList;
                }

                string documentRequiredListStringdata = loginManager.LoadDocumetRequired();
                if (!string.IsNullOrEmpty(documentRequiredListStringdata))
                {
                    objOutput = JsonConvert.DeserializeObject<ExpandoObject>(documentRequiredListStringdata, expConverter);
                    returnObject.DocumentRequiredList = objOutput.Json.DocumentRequiredList;
                }

                return Ok(returnObject);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error(ex.Message, 404);
                return new CustomErrorActionResult(HttpStatusCode.NotFound, "Not Found");
            }
        }

        [HttpPost]
        [Route("/api/[controller]/LoadAllCompanyDetailListByDropDown")]
        public IActionResult LoadAllCompanyDetailListByDropDown([FromBody] dynamic Json)
        {
            JObject returnObject = new JObject();
            try
            {
                string Input = JsonConvert.SerializeObject(Json);
                LoggerInstance.Information("LoadAllCompanyDetailListByDropDown --> Input String : " + Input, 502);
                ILoginManager loginManager = new LoginManager(LoggerInstance);
                string output = loginManager.GetAllCompanyListByDropDown(Input);
                LoggerInstance.Information("LoadAllCompanyDetailListByDropDown --> Output String : " + output, 502);
                returnObject = (JObject)JsonConvert.DeserializeObject(output);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error("LoadAllCompanyDetailListByDropDown Exception : " + ex.Message, 404);
            }

            return Ok(returnObject);

        }

        [HttpPost]
        [Route("/api/[controller]/LoadAllShipToDetailListByDropDown")]
        public IActionResult LoadAllShipToDetailListByDropDown([FromBody] dynamic Json)
        {
            JObject returnObject = new JObject();
            try
            {
                string Input = JsonConvert.SerializeObject(Json);
                LoggerInstance.Information("LoadAllShipToDetailListByDropDown --> Input String : " + Input, 502);
                ILoginManager loginManager = new LoginManager(LoggerInstance);
                string output = loginManager.GetAllShipToListByCompanyId(Input);
                LoggerInstance.Information("LoadAllShipToDetailListByDropDown --> Output String : " + output, 502);
                returnObject = (JObject)JsonConvert.DeserializeObject(output);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error("LoadAllShipToDetailListByDropDown Exception : " + ex.Message, 404);
            }

            return Ok(returnObject);

        }

        [HttpPost]
        [Route("/api/[controller]/LoadAllRoleMaster")]
        public IActionResult LoadAllRoleMaster([FromBody] dynamic Json)
        {
            JObject returnObject = new JObject();
            try
            {
                string Input = JsonConvert.SerializeObject(Json);
                LoggerInstance.Information("LoadAllRoleMaster --> Input String : " + Input, 502);
                ILoginManager loginManager = new LoginManager(LoggerInstance);
                string output = loginManager.LoadAllRoleMaster(Input);
                LoggerInstance.Information("LoadAllRoleMaster --> Output String : " + output, 502);
                returnObject = (JObject)JsonConvert.DeserializeObject(output);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error("LoadAllRoleMaster Exception : " + ex.Message, 404);
            }

            return Ok(returnObject);

        }

        [HttpPost]
        [Route("/api/[controller]/LoadLoginDetailByLoginId")]
        public IActionResult LoadLoginDetailByLoginId([FromBody] dynamic Json)
        {
            JObject returnObject = new JObject();
            try
            {
                string Input = JsonConvert.SerializeObject(Json);
                LoggerInstance.Information("LoadLoginDetailByLoginId --> Input String : " + Input, 502);
                ILoginManager loginManager = new LoginManager(LoggerInstance);
                string output = loginManager.LoadLoginDetailByLoginId(Input);
                LoggerInstance.Information("LoadLoginDetailByLoginId --> Output String : " + output, 502);
                returnObject = (JObject)JsonConvert.DeserializeObject(output);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error("LoadLoginDetailByLoginId Exception : " + ex.Message, 404);
            }

            return Ok(returnObject);

        }

        [HttpPost]
        [Route("/api/[controller]/GetPageRoleWiseAccessDetailByRoleORUserID")]
        public IActionResult GetPageRoleWiseAccessDetailByRoleORUserID([FromBody] dynamic Json)
        {
            JObject returnObject = new JObject();
            try
            {
                string Input = JsonConvert.SerializeObject(Json);
                LoggerInstance.Information("GetPageRoleWiseAccessDetailByRoleORUserID --> Input String : " + Input, 502);
                ILoginManager loginManager = new LoginManager(LoggerInstance);
                string output = loginManager.GetPageRoleWiseAccessDetailByRoleORUserID(Input);
                LoggerInstance.Information("GetPageRoleWiseAccessDetailByRoleORUserID --> Output String : " + output, 502);
                returnObject = (JObject)JsonConvert.DeserializeObject(output);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error("GetPageRoleWiseAccessDetailByRoleORUserID Exception : " + ex.Message, 404);
            }

            return Ok(returnObject);

        }

        [HttpPost]
        [Route("/api/[controller]/InsertAndUpdateRoleWisePageMappingAndRoleWiseFieldAccess")]
        public IActionResult InsertAndUpdateRoleWisePageMappingAndRoleWiseFieldAccess([FromBody] dynamic Json)
        {
            JObject returnObject = new JObject();
            try
            {
                string Input = JsonConvert.SerializeObject(Json);
                LoggerInstance.Information("InsertAndUpdateRoleWisePageMappingAndRoleWiseFieldAccess --> Input String : " + Input, 502);
                ILoginManager loginManager = new LoginManager(LoggerInstance);
                string output = loginManager.InsertAndUpdateRoleWisePageMappingAndRoleWiseFieldAccess(Input);
                LoggerInstance.Information("InsertAndUpdateRoleWisePageMappingAndRoleWiseFieldAccess --> Output String : " + output, 502);
                returnObject = (JObject)JsonConvert.DeserializeObject(output);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error("InsertAndUpdateRoleWisePageMappingAndRoleWiseFieldAccess Exception : " + ex.Message, 404);
            }

            return Ok(returnObject);

        }

        [HttpPost]
        [Route("/api/[controller]/GetPasswordPolicyByRoleMasterId")]
        public IActionResult GetPasswordPolicyByRoleMasterId([FromBody] PasswordPolicyDTO passwordPolicyDto)
        {
            ErrorInfo errorInfo = new ErrorInfo();
            try
            {
                LoggerInstance.Information("GetPasswordPolicyByRoleMasterId Start Role Id: " + passwordPolicyDto.RoleMasterId.ToString(), 502);
                ILoginManager loginManager = new LoginManager(LoggerInstance);
                passwordPolicyDto.PasswordPolicyList = loginManager.GetPasswordPolicyByRoleMasterId(passwordPolicyDto).PasswordPolicyList;

                string errorMessage = "";
                for (int i = 0; i < passwordPolicyDto.PasswordPolicyList.Count; i++)
                {
                    var Passpolicy = passwordPolicyDto.PasswordPolicyList[i];
                    errorMessage = "Password length should be between " + Passpolicy.MinimumPasswordLength + "-" + Passpolicy.MaximumPasswordLength + " characters";

                    bool IsLowerCaseAllowed = Passpolicy.IsLowerCaseAllowed ?? false;
                    bool IsNumberAllowed = Passpolicy.IsNumberAllowed ?? false;
                    bool IsSpecialCharacterAllowed = Passpolicy.IsSpecialCharacterAllowed ?? false;
                    bool IsUpperCaseAllowed = Passpolicy.IsUpperCaseAllowed ?? false;

                    bool ValExists = false;
                    if (IsUpperCaseAllowed)
                    {
                        ValExists = true;
                        errorMessage = errorMessage + " with " + Passpolicy.MinimumUppercaseCharactersRequired + " Upper Case character";
                    }
                    if (IsLowerCaseAllowed)
                    {
                        ValExists = true;
                        errorMessage = errorMessage + ", " + Passpolicy.MinimumLowercaseCharactersRequired + " Lower Case character";
                    }
                    if (IsNumberAllowed)
                    {
                        ValExists = true;
                        errorMessage = errorMessage + ", " + Passpolicy.MinimumNumericsRequired + " number";
                    }
                    if (IsSpecialCharacterAllowed)
                    {
                        ValExists = true;
                        errorMessage = errorMessage + ", " + Passpolicy.MinimumSpecialCharactersRequired + " special character";
                    }

                    if (ValExists)
                    {
                        errorMessage = errorMessage + " is required.";
                    }

                    errorInfo.PasswordExpiryPeriod = Passpolicy.PasswordExpiryPeriod;

                }

                errorInfo.ErrorMessage = errorMessage;
                LoggerInstance.Information("GetPasswordPolicyByRoleMasterId End Role Id: " + passwordPolicyDto.RoleMasterId.ToString(), 502);

            }
            catch (Exception ex)
            {
                LoggerInstance.Error("GetPasswordPolicyByRoleMasterId Exception : " + ex.Message, 404);
            }

            return Ok(errorInfo);

        }

        [HttpPost]
        [Route("/api/[controller]/CheckPasswordPolicy")]
        public IActionResult CheckPasswordPolicy([FromBody] LoginDTO loginDTO) // Error code: 404 Not found
        {
            try
            {
                ErrorInfo errorInfo = new ErrorInfo();
                var loginDto = new LoginDTO();
                //var loginDto1 = new LoginDTO();
                long retVal = 0;
                int currentPasswordsalt = Password.CreateRandomSalt();

                int salt = Password.CreateRandomSalt();
                string haspassword = Password.HashPassword(loginDTO.Password, salt);
                byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(loginDTO.Password);
                string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

                if (AccesKey == loginDTO.AccessKey)
                {

                    ILoginManager loginManager = new LoginManager(LoggerInstance);

                    bool IsAllowSameaslastpassword = false;
                    PasswordPolicyDTO passwordPolicyDto = new PasswordPolicyDTO();

                    var objPasswordPolicyDto = new PasswordPolicyDTO
                    {
                        RoleMasterId = Convert.ToInt64(loginDTO.RoleMasterId)
                    };
                    passwordPolicyDto.PasswordPolicyList = loginManager.GetPasswordPolicyByRoleMasterId(objPasswordPolicyDto).PasswordPolicyList;
                    if (passwordPolicyDto.PasswordPolicyList.Count > 0)
                    {
                        IsAllowSameaslastpassword = passwordPolicyDto.PasswordPolicyList.FirstOrDefault().NewPasswordShouldNotMatchNoOfLastPassword ?? false;
                    }


                    bool ststus = GetpasswordPolicybyRoleMasterid(Convert.ToString(loginDTO.RoleMasterId), loginDTO.Password);
                    if (ststus)
                    {
                        errorInfo.retVal = 2;
                        errorInfo.ErrorMessage = "";
                        if (passwordPolicyDto.PasswordPolicyList.Count > 0)
                        {
                            errorInfo.PasswordExpiryPeriod = passwordPolicyDto.PasswordPolicyList.FirstOrDefault().PasswordExpiryPeriod;
                        }
                    }
                    else
                    {
                        errorInfo.retVal = 3;

                        string errorMessage = "";
                        for (int i = 0; i < passwordPolicyDto.PasswordPolicyList.Count; i++)
                        {
                            var Passpolicy = passwordPolicyDto.PasswordPolicyList[i];
                            errorMessage = "Password length should be between " + Passpolicy.MinimumPasswordLength + "-" + Passpolicy.MaximumPasswordLength + " characters";

                            bool IsLowerCaseAllowed = Passpolicy.IsLowerCaseAllowed ?? false;
                            bool IsNumberAllowed = Passpolicy.IsNumberAllowed ?? false;
                            bool IsSpecialCharacterAllowed = Passpolicy.IsSpecialCharacterAllowed ?? false;
                            bool IsUpperCaseAllowed = Passpolicy.IsUpperCaseAllowed ?? false;

                            bool ValExists = false;
                            if (IsUpperCaseAllowed)
                            {
                                ValExists = true;
                                errorMessage = errorMessage + " with " + Passpolicy.MinimumUppercaseCharactersRequired + " Upper Case character";
                            }
                            if (IsLowerCaseAllowed)
                            {
                                ValExists = true;
                                errorMessage = errorMessage + ", " + Passpolicy.MinimumLowercaseCharactersRequired + " Lower Case character";
                            }
                            if (IsNumberAllowed)
                            {
                                ValExists = true;
                                errorMessage = errorMessage + ", " + Passpolicy.MinimumNumericsRequired + " number";
                            }
                            if (IsSpecialCharacterAllowed)
                            {
                                ValExists = true;
                                errorMessage = errorMessage + ", " + Passpolicy.MinimumSpecialCharactersRequired + " special character";
                            }

                            if (ValExists)
                            {
                                errorMessage = errorMessage + " is required.";
                            }

                            errorInfo.PasswordExpiryPeriod = Passpolicy.PasswordExpiryPeriod;

                        }

                        errorInfo.ErrorMessage = errorMessage;
                    }

                }
                return Ok(errorInfo);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error(ex.Message, 404);
                return new CustomErrorActionResult(HttpStatusCode.NotFound, "Not Found");
            }
        }

        [HttpPost]
        [Route("/api/[controller]/ResetPasswordBySelf")]
        public IActionResult ResetPasswordBySelf([FromBody] LoginDTO loginDTO) // Error code: 404 Not found
        {
            try
            {
                ErrorInfo errorInfo = new ErrorInfo();
                var loginDto = new LoginDTO();
                //var loginDto1 = new LoginDTO();
                long retVal = 0;
                int currentPasswordsalt = Password.CreateRandomSalt();

                int salt = Password.CreateRandomSalt();
                string haspassword = Password.HashPassword(loginDTO.Password, salt);
                byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(loginDTO.Password);
                string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

                if (AccesKey == loginDTO.AccessKey)
                {

                    ILoginManager loginManager = new LoginManager(LoggerInstance);
                    var objloginDto1 = new LoginDTO
                    {
                        LoginId = loginDTO.UserId
                    };
                    loginDto = loginManager.GetLoginUserProfileByUserLoginId(objloginDto1);

                    string currentPasswordHaspassword = Password.HashPassword(loginDTO.CurrentPassword, int.Parse(Convert.ToString(loginDto.LoginList[0].PasswordSalt)));
                    if (currentPasswordHaspassword == loginDto.LoginList[0].HashedPassword)
                    {

                        bool IsAllowSameaslastpassword = false;
                        PasswordPolicyDTO passwordPolicyDto = new PasswordPolicyDTO();

                        var objPasswordPolicyDto = new PasswordPolicyDTO
                        {
                            RoleMasterId = Convert.ToInt64(loginDto.LoginList[0].RoleMasterId)
                        };
                        passwordPolicyDto.PasswordPolicyList = loginManager.GetPasswordPolicyByRoleMasterId(objPasswordPolicyDto).PasswordPolicyList;
                        if (passwordPolicyDto.PasswordPolicyList.Count > 0)
                        {
                            IsAllowSameaslastpassword = passwordPolicyDto.PasswordPolicyList.FirstOrDefault().NewPasswordShouldNotMatchNoOfLastPassword ?? false;
                        }


                        if (IsAllowSameaslastpassword)
                        {
                            if (loginDTO.CurrentPassword != loginDTO.Password)
                            {
                                bool ststus = GetpasswordPolicybyRoleMasterid(Convert.ToString(loginDto.LoginList[0].RoleMasterId), loginDTO.Password);
                                if (ststus)
                                {
                                    loginDTO.PasswordSalt = salt;
                                    loginDTO.HashedPassword = haspassword;
                                    List<LoginDTO> loginDTOs = loginManager.ChangePassword(loginDTO);
                                    errorInfo.retVal = 1;
                                }
                                else
                                {
                                    errorInfo.retVal = 3;

                                    string errorMessage = "";
                                    for (int i = 0; i < passwordPolicyDto.PasswordPolicyList.Count; i++)
                                    {
                                        var Passpolicy = passwordPolicyDto.PasswordPolicyList[i];
                                        errorMessage = "Password length should be between " + Passpolicy.MinimumPasswordLength + "-" + Passpolicy.MaximumPasswordLength + " characters";

                                        bool IsLowerCaseAllowed = Passpolicy.IsLowerCaseAllowed ?? false;
                                        bool IsNumberAllowed = Passpolicy.IsNumberAllowed ?? false;
                                        bool IsSpecialCharacterAllowed = Passpolicy.IsSpecialCharacterAllowed ?? false;
                                        bool IsUpperCaseAllowed = Passpolicy.IsUpperCaseAllowed ?? false;

                                        bool ValExists = false;
                                        if (IsUpperCaseAllowed)
                                        {
                                            ValExists = true;
                                            errorMessage = errorMessage + " with " + Passpolicy.MinimumUppercaseCharactersRequired + " Upper Case character";
                                        }
                                        if (IsLowerCaseAllowed)
                                        {
                                            ValExists = true;
                                            errorMessage = errorMessage + ", " + Passpolicy.MinimumLowercaseCharactersRequired + " Lower Case character";
                                        }
                                        if (IsNumberAllowed)
                                        {
                                            ValExists = true;
                                            errorMessage = errorMessage + ", " + Passpolicy.MinimumNumericsRequired + " number";
                                        }
                                        if (IsSpecialCharacterAllowed)
                                        {
                                            ValExists = true;
                                            errorMessage = errorMessage + ", " + Passpolicy.MinimumSpecialCharactersRequired + " special character";
                                        }

                                        if (ValExists)
                                        {
                                            errorMessage = errorMessage + " is required.";
                                        }

                                    }

                                    errorInfo.ErrorMessage = errorMessage;
                                }
                            }
                            else
                            {
                                errorInfo.retVal = 2;
                            }
                        }
                        else
                        {
                            loginDTO.PasswordSalt = salt;
                            loginDTO.HashedPassword = haspassword;
                            List<LoginDTO> loginDTOs = loginManager.ChangePassword(loginDTO);
                            errorInfo.retVal = 1;
                        }

                    }
                    else
                    {
                        errorInfo.retVal = 2;
                    }

                }
                return Ok(errorInfo);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error(ex.Message, 404);
                return new CustomErrorActionResult(HttpStatusCode.NotFound, "Not Found");
            }
        }

        class ErrorInfo
        {
            public string ErrorMessage { get; set; }
            public long retVal { get; set; }
            public int? PasswordExpiryPeriod { get; set; }

            public ErrorInfo()
            {
                retVal = 0;
            }

        }

    }
}
