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
using SecureGate.Framework;
using System.Data;
using SecureGate.APIController.Framework;
using SecureGate.DataAccess.Common;

namespace SecureGate.ManageLogin.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class glassRUNFrameworkLoginController : BaseAPIController
    {
        protected override EnumLoggerType LoggerName
        {
            get { return EnumLoggerType.Login; }
        }


        [HttpPost]
        [Route("glassRUNLogin")]

        public IActionResult glassRUNLogin(glassRUNFrameworkLoginDTO glassRUNFrameworkLoginDto)
        {
            LoggerInstance.Information("glassRUNLogin", 5002);

            JObject returnObject = new JObject();
            try
            {
                string Input = JsonConvert.SerializeObject(glassRUNFrameworkLoginDto);

                LoggerInstance.Information("glassRUNLogin" + Input, 5002);



                dynamic newJson = new ExpandoObject();
                dynamic FinalJson = new ExpandoObject();
                newJson.AppVersion = null;
                newJson.ResourceData = null;
                newJson.LoginDetails = null;

                dynamic checkAppVersion = new ExpandoObject();
                checkAppVersion.AppLatestVersionNo = glassRUNFrameworkLoginDto.AppLatestVersionNo;
                checkAppVersion.AppLatestBuildNo = glassRUNFrameworkLoginDto.AppLatestBuildNo;
                checkAppVersion.AppType = glassRUNFrameworkLoginDto.AppType;
                checkAppVersion.UserAndDeviceDetails = glassRUNFrameworkLoginDto.UserAndDeviceDetails;

                string chkApp = JsonConvert.SerializeObject(checkAppVersion);

                string CheckAppVersion = GetServiceUrl("CheckAppVersion");
                GRClientRequest ProfileRequest = new GRClientRequest(CheckAppVersion, "POST", chkApp);
                string outputProfileResponse = ProfileRequest.GetResponse();

                if (!string.IsNullOrEmpty(outputProfileResponse) && outputProfileResponse != "{}" && outputProfileResponse != "")
                {
                    JObject jsonObjOutputResponse = (JObject)JsonConvert.DeserializeObject(outputProfileResponse);
                    newJson.AppVersion = jsonObjOutputResponse;
                }

                if (newJson.AppVersion != null)
                {
                    string AppLatestVersionIsMandatory = Convert.ToString(newJson.AppVersion.GetValue("AppLatestVersionIsMandatory"));
                    string IsAppNeedToUpdate = Convert.ToString(newJson.AppVersion.GetValue("IsAppNeedToUpdate"));

                    if (AppLatestVersionIsMandatory == "YES" && IsAppNeedToUpdate == "YES")
                    {
                        FinalJson.Json = newJson;
                        JObject FinalJobject = (JObject)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(FinalJson));
                        returnObject = FinalJobject;
                    }
                    else
                    {
                        string IsValidLogin = "NO";
                        LoginDTO loginDTO = new LoginDTO();
                        loginDTO.Username = glassRUNFrameworkLoginDto.Username;
                        loginDTO.UserPassword = glassRUNFrameworkLoginDto.Password;
                        loginDTO.UserAndDeviceDetails = glassRUNFrameworkLoginDto.UserAndDeviceDetails;
                        string loginDto = JsonConvert.SerializeObject(loginDTO);

                        string ValidateLogin = GetServiceUrl("ValidateLogin");
                        GRClientRequest loginRequest = new GRClientRequest(ValidateLogin, "POST", loginDto);
                        string outputLoginResponse = loginRequest.GetResponse();

                        if (!string.IsNullOrEmpty(outputLoginResponse) && outputLoginResponse != "{}" && outputLoginResponse != "")
                        {
                            JArray jsonObjOutputResponse = (JArray)JsonConvert.DeserializeObject(outputLoginResponse);
                            newJson.LoginDetails = jsonObjOutputResponse;
                        }

                        if (newJson.LoginDetails != null)
                        {
                            foreach (JObject item in newJson.LoginDetails) // <-- Note that here we used JObject instead of usual JProperty
                            {
                                IsValidLogin = Convert.ToString(item.GetValue("IsValidLogin"));
                            }
                        }

                        if (IsValidLogin != "NO")
                        {
                            dynamic getResources = new ExpandoObject();
                            getResources.ResourceType = Convert.ToString(glassRUNFrameworkLoginDto.AppType);
                            getResources.LastSyncDateTime = Convert.ToString(glassRUNFrameworkLoginDto.LastSyncDateTime);
                            getResources.UserAndDeviceDetails = glassRUNFrameworkLoginDto.UserAndDeviceDetails;

                            dynamic getResourcesJson = new ExpandoObject();
                            getResourcesJson.Json = getResources;
                            string resourceDataJson = JsonConvert.SerializeObject(getResourcesJson);
                            string ResourceData = GetServiceUrl("ResourceData");
                            GRClientRequest ResourceDataRequest = new GRClientRequest(ResourceData, "POST", resourceDataJson);
                            string outputResourceDataResponse = ResourceDataRequest.GetResponse();

                            if (!string.IsNullOrEmpty(outputResourceDataResponse) && outputResourceDataResponse != "{}" && outputResourceDataResponse != "")
                            {
                                JObject jsonObjOutputResponse = (JObject)JsonConvert.DeserializeObject(outputResourceDataResponse);
                                newJson.ResourceData = jsonObjOutputResponse;
                            }
                        }

                        FinalJson.Json = newJson;
                        JObject FinalJobject = (JObject)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(FinalJson));
                        returnObject = FinalJobject;

                    }


                }


            }
            catch (Exception ex)
            {
                string d = ex.Message;
                LoggerInstance.Error("Exception : " + ex.Message, 5002);
            }
            return Ok(returnObject);
        }




        [HttpPost]
        [Route("GetUserDetailsByUsername")]

        public IActionResult GetUserDetailsByUsername(LoginDTO loginDTO)
        {
            //string Input = JsonConvert.SerializeObject(Json);
            JObject returnObject = new JObject();
            LoggerInstance.Information("UserName : " + loginDTO.Username, 5002);
            try
            {
                IglassRUNFrameworkLoginManager loginManager = new glassRUNFrameworkLoginManager(LoggerInstance);

                IEnumerable<LoginDTO> output = loginManager.GetUserDetailsByUsername(loginDTO);


                if (output != null)
                {
                    if (output.Any())
                    {
                        List<LoginDTO> loginDTOs = output.ToList();

                        for (int i = 0; i < loginDTOs.Count; i++)
                        {
                            var deviceType = "App";

                            if (loginDTO.UserAndDeviceDetails != null)
                            {
                                if (string.IsNullOrEmpty(Convert.ToString(loginDTO.UserAndDeviceDetails.DeviceType)))
                                {
                                    deviceType = "Web";
                                }
                                else
                                {
                                    if (loginDTO.UserAndDeviceDetails.DeviceType.ToString().ToLower() == "Android".ToLower() || loginDTO.UserAndDeviceDetails.DeviceType.ToString().ToLower() == "Ios".ToLower() || loginDTO.UserAndDeviceDetails.DeviceType.ToString().ToLower() == "App".ToLower())
                                    {
                                        deviceType = "App";
                                    }
                                    else
                                    {
                                        deviceType = "Web";
                                    }
                                }
                            }
                            else
                            {
                                deviceType = "App";
                            }

                            var tokenHandler = new JwtSecurityTokenHandler();
                            var key = Encoding.ASCII.GetBytes("GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk");

                            var now = DateTime.Now;
                            var claims = new Claim[]
                                {
                                        new Claim(JwtRegisteredClaimNames.Sub, loginDTOs[i].UserId.ToString()),
                                        new Claim(JwtRegisteredClaimNames.Typ, deviceType),
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

                            bool CheckForLicense = ValidateLicense(loginDTOs[i]);
                            loginDTOs[i].IsLicenseValid = CheckForLicense;
                           string op = loginManager.UpdateToken(Convert.ToInt64(loginDTOs[i].UserId), tokenString, deviceType);
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


        protected bool ValidateLicense(LoginDTO loginDto)
        {
            bool IsValid = false;
            try
            {

                JObject jsonObj = new JObject();

                var newObj = new JObject
                {
                    ["Json"] = jsonObj,
                };
                string newJsonString = Convert.ToString(newObj);
                LoggerInstance.Information("newJsonString: " + newJsonString, 5002);
                string servicesAction = "GetLicenseInfoList";
                string servicesURL = "";

                DataTable dtRoleWiseStatusInformationURL = ServiceConfigurationDataAccessManager.GetServicesConfiguartionURL<DataTable>(servicesAction);
                if (dtRoleWiseStatusInformationURL.Rows.Count > 0)
                    servicesURL = dtRoleWiseStatusInformationURL.Rows[0]["ServicesURL"].ToString();


                LoggerInstance.Information("servicesURL: " + servicesURL, 5002);
                // Convert json string to custom .Net object
             
                LoggerInstance.Information("customApi: " + servicesURL, 5002);
                GRClientRequest myRequest = new GRClientRequest(servicesURL, "POST", newJsonString);
                LoggerInstance.Information("myRequest: " + myRequest, 5002);
                string outputResponse = myRequest.GetResponse();
                LoggerInstance.Information("outputResponse: " + outputResponse, 5002);
                if (!string.IsNullOrEmpty(outputResponse))
                {

                    JObject obj = new JObject();
                    JObject obj1 = new JObject();

                    obj = (JObject)JsonConvert.DeserializeObject(outputResponse);

                    // Create a dynamic output object
                    dynamic output = new ExpandoObject();

                    output.LicenseInfoList = obj["LicenseInfo"]["LicenseInfoList"];
                    output.UserTypeCode = loginDto.UserTypeCode;
                    output.UserName = loginDto.Username;
                    output.ActivationCode = loginDto.ActivationCode;

                    dynamic orderJson = new ExpandoObject();
                    orderJson.Json = output;

                    // Serialize the dynamic output object to a string
                    string outputJson = JsonConvert.SerializeObject(orderJson);

                    // Convert json object to JOSN string format
                    string jsonData = JsonConvert.SerializeObject(obj1);

                    string servicesAction1 = "ValidateLicense";
                    string servicesURL1 = "";


                    DataTable ValidateLicenseURL = ServiceConfigurationDataAccessManager.GetServicesConfiguartionURL<DataTable>(servicesAction1);
                    if (ValidateLicenseURL.Rows.Count > 0)
                        servicesURL1 = ValidateLicenseURL.Rows[0]["ServicesURL"].ToString();

                    GRClientRequest myRequest1 = new GRClientRequest(servicesURL1, "POST", outputJson);

                    string outputResponse1 = myRequest1.GetResponse();

                    if (!string.IsNullOrEmpty(outputResponse1))
                    {
                        JObject objoutputResponse = new JObject();

                        objoutputResponse = (JObject)JsonConvert.DeserializeObject(outputResponse1);
                        IsValid = Convert.ToBoolean(objoutputResponse["Json"]["IsLicenseValid"]);
                    }
                }
            }
            catch (Exception ex)
            {

                LoggerInstance.Information("ValidateLicense: " + ex, 5002);
                LoggerInstance.Information("ValidateLicenseMessage: " + ex.Message, 5002);
                LoggerInstance.Information("ValidateLicenseInnerException: " + ex.InnerException, 5002);
            }

            return IsValid;
        }



        [HttpPost]
        [Route("GetUserFullDetailsByUsername")]

        public IActionResult GetUserFullDetailsByUsername(LoginDTO loginDTO)
        {
            //string Input = JsonConvert.SerializeObject(Json);
            JObject returnObject = new JObject();
            LoggerInstance.Information("UserName : " + loginDTO.Username, 5002);
            try
            {
                IglassRUNFrameworkLoginManager loginManager = new glassRUNFrameworkLoginManager(LoggerInstance);
                IEnumerable<LoginDTO> output = loginManager.GetUserFullDetailsByUsername(loginDTO);
                return Ok(output);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error(ex.Message, 404);
                return new CustomErrorActionResult(HttpStatusCode.NotFound, "Not Found");
            }

        }



        [HttpPost]
        [Route("ValidateLogin")]
        public IActionResult ValidateLogin(LoginDTO loginDTO)
        {
            //string Input = JsonConvert.SerializeObject(Json);
            JObject returnObject = new JObject();
            LoggerInstance.Information("UserName : " + loginDTO.Username, 5002);
            try
            {
                IglassRUNFrameworkLoginManager loginManager = new glassRUNFrameworkLoginManager(LoggerInstance);

                IEnumerable<LoginDTO> output = loginManager.ValidateLogin(loginDTO);


                if (output != null)
                {
                    if (output.Any())
                    {
                        List<LoginDTO> loginDTOs = output.ToList();

                        for (int i = 0; i < loginDTOs.Count; i++)
                        {

                            var deviceType = "App";

                            if (loginDTO.UserAndDeviceDetails != null)
                            {
                                if (string.IsNullOrEmpty(Convert.ToString(loginDTO.UserAndDeviceDetails.DeviceType)))
                                {
                                    deviceType = "Web";
                                }
                                else
                                {
                                    if (loginDTO.UserAndDeviceDetails.DeviceType.ToString().ToLower() == "Android".ToLower() || loginDTO.UserAndDeviceDetails.DeviceType.ToString().ToLower() == "Ios".ToLower() || loginDTO.UserAndDeviceDetails.DeviceType.ToString().ToLower() == "App".ToLower())
                                    {
                                        deviceType = "App";
                                    }
                                    else
                                    {
                                        deviceType = "Web";
                                    }
                                }
                            }
                            else
                            {
                                deviceType = "App";
                            }


                            var userPassword = Convert.ToString(loginDTO.UserPassword);
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
                                        new Claim(JwtRegisteredClaimNames.Typ, loginDTOs[i].UserAndDeviceDetails.Apptype.ToString()),
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

                                string op = loginManager.UpdateToken(Convert.ToInt64(loginDTOs[i].UserId), tokenString, deviceType);

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

        public string GetServiceUrl(string servicesAction)
        {
            string ServicesURL = "";
            ServicesURL = ServiceConfigurationDataAccessManager.GetServiceActionURL(servicesAction);
            return ServicesURL;
        }

    }
}
