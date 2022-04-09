using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System.Threading.Tasks;

namespace SecureGate.ManageLogin.DTO
{
    [Serializable, XmlRoot(ElementName = "Login", IsNullable = false)]
    public class LoginUserDTO
    {
        public LoginUserDTO()
        {
            LoginList = new List<LoginUserDTO>();
        }

        public long LoginId { get; set; }

        
        public long ProfileId { get; set; }

        
        public long RoleMasterId { get; set; }

        
        public long ParentCompanyId { get; set; }

        
        public string UserName { get; set; }

        public string Logo { get; set; }

        public string HashedPassword { get; set; }

        public string Name { get; set; }

        public string RoleName { get; set; }

        public string PageUrl { get; set; }

        public string PageName { get; set; }

        public long ReferenceId { get; set; }

        public long ReferenceType { get; set; }

        public string CompanyMnemonic { get; set; }

        public string CompanyType { get; set; }

        public string CompanyZone { get; set; }

        public string SelfCollectValue { get; set; }

        public int PasswordSalt { get; set; }

        public int LoginAttempts { get; set; }

        public string AccessKey { get; set; }

        public System.DateTime? LastLogin { get; set; }

        public System.DateTime? ExpiryDate { get; set; }

        public System.DateTime? LastPasswordChange { get; set; }

        public bool AllowLogin { get; set; }

        public bool? ChangePasswordonFirstLoginRequired { get; set; }

        public int PasswordWarningDays { get; set; }

        public int NumberOfDaysRemainingForChangePassword { get; set; }

        public decimal CreditLimit { get; set; }

        public decimal AvailableCreditLimit { get; set; }

        public string UserProfilePicture { get; set; }

        public string UserTypeCode { get; set; }

        public string ActivationCode { get; set; }

        public string GUID { get; set; }

        public string DefaultLanguage { get; set; }

        [XmlElement(ElementName = "LoginList")]
        public List<LoginUserDTO> LoginList { get; set; }

        public string StockLocationCode { get; set; }

        public int UserPersonaMasterId { get; set; }

        public string UserPersona { get; set; }

        
    }
}
