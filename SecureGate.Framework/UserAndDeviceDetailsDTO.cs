using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SecureGate.Framework
{
    [Serializable]
    [XmlRoot(ElementName = "UserAndDeviceDetails", IsNullable = false)]
    public class UserAndDeviceDetailsDTO
    {

        public string UserName { get; set; }
        public string DeviceType { get; set; }
        public string Apptype { get; set; }
        public string AppVersionNo { get; set; }
        public string AppLatestBuildNo { get; set; }
        public string EnvironmentCode { get; set; }

        [XmlElement(ElementName = "UserAndDeviceDetails")]
        public UserAndDeviceDetailsDTO UserAndDeviceDetails { get; set; }

    }
}
