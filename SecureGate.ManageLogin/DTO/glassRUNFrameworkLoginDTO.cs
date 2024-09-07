using SecureGate.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SecureGate.ManageLogin.DTO
{
    [Serializable]
    [XmlRoot(ElementName = "SecureGateFrameworkLogin", IsNullable = false)]
    public class SecureGateFrameworkLoginDTO
    {

        public string AppLatestVersionNo { get; set; }
        public string AppLatestBuildNo { get; set; }
        public string AppLatestDownloadLinkForAndroid { get; set; }
        public string AppLatestDownloadLinkForIOS { get; set; }
        public string AppLatestDownloadLinkForWindows { get; set; }
        public bool AppLatestVersionMandatory { get; set; }
        public string AppLatestVersionIsMandatory { get; set; }
        public long AppType { get; set; }
        public string IsAppNeedToUpdate { get; set; }
        public long UserId { get; set; }
        public int RoleMasterId { get; set; }
        public string Username { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string LastSyncDateTime { get; set; }

        [XmlElement(ElementName = "SecureGateFrameworkLoginList")]
        public List<SecureGateFrameworkLoginDTO> AppLatestVersionList { get; set; }

        [XmlElement(ElementName = "UserAndDeviceDetails")]
        public UserAndDeviceDetailsDTO UserAndDeviceDetails { get; set; }

        public SecureGateFrameworkLoginDTO()
        {
            AppLatestVersionList = new List<SecureGateFrameworkLoginDTO>();
        }

    }
}
