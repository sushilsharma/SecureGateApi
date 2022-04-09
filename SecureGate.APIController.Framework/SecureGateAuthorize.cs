using Microsoft.AspNetCore.Authorization;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using System.Net;
using SecureGate.APIController.Framework.DTO;
using SecureGate.APIController.Framework.Business;

namespace SecureGate.SecureGate.APIController.Framework
{
    public class SecureGateAuthorize : ActionFilterAttribute
    {
        public SecureGateAuthorize()
        {

        }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (actionContext.HttpContext.Request.Headers != null)
            {
                // get value from header
                Microsoft.Extensions.Primitives.StringValues tString = "";
                bool aut = actionContext.HttpContext.Request.Headers.TryGetValue("Authorization", out tString);
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
                    string deviceType = a["typ"];
                    DateTime expirationTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(a["exp"])).LocalDateTime;
                    if (expirationTime > DateTime.Now)
                    {
                        LoginDTO loginDto = new LoginDTO();
                        loginDto.UserId = userId;
                        loginDto.DeviceType = deviceType;
                        ILoginManager loginManager = new LoginManager();
                        List<LoginDTO> lDTO = loginManager.GetLoginDetailsById(loginDto);

                        if (lDTO.Count > 0)
                        {
                            loginDto = lDTO[0];
                        }

                        if (loginDto != null && !string.IsNullOrEmpty(loginDto.AccessKey))
                        {
                            if (tokenString != loginDto.AccessKey)
                            {
                                actionContext.Result = new UnauthorizedResult();
                                return;
                            }
                            if (!loginDto.IsActive)
                            {
                                actionContext.Result = new UnauthorizedResult();
                                return;
                            }
                        }
                        else
                        {
                            actionContext.Result = new UnauthorizedResult();
                            return;
                        }
                    }
                    else
                    {
                        actionContext.Result = new UnauthorizedResult();
                        return;
                    }
                }
                else
                {
                    actionContext.Result = new UnauthorizedResult();
                    return;
                }
            }
            return;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // Do something after the action executes.
        }

    }
}
