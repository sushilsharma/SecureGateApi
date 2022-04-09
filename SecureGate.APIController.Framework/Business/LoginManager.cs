using SecureGate.APIController.Framework;
using SecureGate.Framework.Serializer;
using SecureGate.APIController.Framework.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SecureGate.APIController.Framework.DataAccess;

namespace SecureGate.APIController.Framework.Business
{

    public class LoginManager : ILoginManager
    {
        public bool ValidateData(LoginDTO loginDTO)
        {
            if (loginDTO.Username == "" && loginDTO.UserId == 0)
            {
                return false;
            }
            return true;
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
                        string accessKey = "";
                        if (loginDTO.DeviceType == "Web")
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(servicesConfiguartionURL["Login"]["LoginList"]["WebAccessKey"])))
                            {
                                accessKey = servicesConfiguartionURL["Login"]["LoginList"]["WebAccessKey"].ToString();
                            }
                            else
                            {
                                accessKey = "";
                            }

                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(servicesConfiguartionURL["Login"]["LoginList"]["AccessKey"])))
                            {
                                accessKey = servicesConfiguartionURL["Login"]["LoginList"]["AccessKey"].ToString();
                            }
                            else
                            {
                                accessKey = "";
                            }
                        }
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
    }
}
