using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureGate.APIController.Framework.DTO
{

    public class LoginDTO
    {
        public long UserId { get; set; }
        public int RoleMasterId { get; set; }
        public string OTPGenerated { get; set; }
        public DateTime OTPCreatedTime { get; set; }
        public DateTime OTPValidTill { get; set; }
        public int OTPSentByChannelId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string OTPPrefix { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public string IsValid { get; set; }
        public string ReasonCode { get; set; }
        public string AccessKey { get; set; }

        public string WebAccessKey { get; set; }

        public string DeviceType { get; set; }


        public long? AppType { get; set; }
    }
}
