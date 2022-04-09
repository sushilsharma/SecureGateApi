using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System.Threading.Tasks;
using SecureGate.Framework;

namespace SecureGate.ManageLogin.DTO
{
    [Serializable, XmlRoot(ElementName = "UserOutletMapping", IsNullable = false)]
    public class UserOutletMappingDTO
    {
        public UserOutletMappingDTO()
        {
            UserOutletMappingList = new List<UserOutletMappingDTO>();
        }

        public long UserOutletMappingId { get; set; }

        public long UserId { get; set; }

        public long CompanyId { get; set; }

        public string CompanyCode { get; set; }

        public string CompanyName { get; set; }

        public string CompanyType { get; set; }

        public long NewAddedItem { get; set; }

        public long RoleId { get; set; }

        public bool IsSuperUser { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public long CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Postcode { get; set; }

        public long UpdatedBy { get; set; }

        public string UserName { get; set; }

        [XmlElement(ElementName = "UserOutletMappingList")]
        public List<UserOutletMappingDTO> UserOutletMappingList { get; set; }

        [XmlElement(ElementName = "UserAndDeviceDetails")]
        public UserAndDeviceDetailsDTO UserAndDeviceDetails { get; set; }
    }
}
