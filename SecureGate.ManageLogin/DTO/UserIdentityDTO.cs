using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System.Threading.Tasks;
using SecureGate.Framework;

namespace SecureGate.ManageLogin.DTO
{
    [Serializable, XmlRoot(ElementName = "UserIdentity", IsNullable = false)]
    public class UserIdentityDTO
    {
        public long UserId { get; set; }
        public long UserIdentityId { get; set; }

        public string UserName { get; set; }
        public string AlternateUserName { get; set; }

        public string EnvironmentName { get; set; }

        public string Url1 { get; set; }

        public string Url2 { get; set; }

        public string Url3 { get; set; }

        public bool IsActive { get; set; }


        public DateTime CreatedDate { get; set; }

        public long CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public long UpdatedBy { get; set; }

        [XmlElement(ElementName = "UserIdentityList")]
        public List<UserIdentityDTO> UserIdentityList { get; set; }

        [XmlElement(ElementName = "UserAndDeviceDetails")]
        public UserAndDeviceDetailsDTO UserAndDeviceDetails { get; set; }

        public UserIdentityDTO()
        {

            UserIdentityList = new List<UserIdentityDTO>();

        }
    }
}
