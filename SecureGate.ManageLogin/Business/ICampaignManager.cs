using SecureGate.ManageLogin.DTO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureGate.ManageLogin.Business
{
    public interface ICampaignManager
    {
        JObject GetCampaignData(dynamic Input);
        JObject CompanyAndUserDetails(string Input);
        JObject SearchUserDetails(dynamic Input);
        JObject CreateCampaign(string Input);
        JObject MappedCampaignToUser(string Input);
    }
}
