using SecureGate.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SecureGate.ManageLogin.DTO
{
    [Serializable, XmlRoot(ElementName = "RoleWiseDefaultPersona", IsNullable = false)]
    public class RoleWiseDefaultPersonaDTO
    {
        public long RoleWiseDefaultPersonaId { get; set; }
        public long RoleMasterId { get; set; }
        public long UserPersonaMasterId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        public bool IsDefaultUserPersona { get; set; }
        public string WorkflowCode { get; set; }

        [XmlElement(ElementName = "RoleWiseDefaultPersonaList")]
        public List<RoleWiseDefaultPersonaDTO> RoleWiseDefaultPersonaList { get; set; }

        public RoleWiseDefaultPersonaDTO()
        {
            RoleWiseDefaultPersonaList = new List<RoleWiseDefaultPersonaDTO>();
        }
    }
}
