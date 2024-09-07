using SecureGate.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SecureGate.ManageLogin.DTO
{
    [Serializable, XmlRoot(ElementName = "Login", IsNullable = false)]
    public class LoginDTO
    {
        public long? AppType { get; set; }
        public long UserId { get; set; }
        public int RoleMasterId { get; set; }
        public string OTPGenerated { get; set; }
        public string ActivationCode { get; set; }
        public string UserTypeCode { get; set; }
        public DateTime OTPCreatedTime { get; set; }
        public DateTime OTPValidTill { get; set; }
        public int OTPSentByChannelId { get; set; }
        public string Username { get; set; }

        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string HashedPassword { get; set; }

        public string OnboardingReferenceCode { get; set; }

        public string AppHashKey { get; set; }

        public string WorkFlowCode { get; set; }
        public int? PasswordSalt { get; set; }
        public string OTPPrefix { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public string IsValid { get; set; }
        public string ReasonCode { get; set; }
        public string CustomerMnemonic { get; set; }
        public string AccessKey { get; set; }
        public bool IsOTPUsed { get; set; }
        public bool IsCurrentOTPRegenerated { get; set; }
        public long CompanyId { get; set; }
        public string ActivateOrDeactivate { get; set; }
        public string ErrorMessage { get; set; }
        public long DefaultLanguage { get; set; }
        public string EulaAgreement { get; set; }
        public long LoginId { get; set; }

        public long? VendorId { get; set; }

        public string UserPassword { get; set; }

        public string hashedKey { get; set; }

        public string DefaultPage { get; set; }

        public string DeviceToken { get; set; }

        public string DeviceType { get; set; }


        public string IsValidLogin { get; set; }
        public string IsRegistered { get; set; }

        public string StockLocationCode { get; set; }

        public long? ProfileId { get; set; }

        public string PhoneNumber { get; set; }

        public int? LoginAttempts { get; set; }

        public string LastPasswordChange { get; set; }

        public int? CompletedSetupStep { get; set; }

        public bool IsStepCompleted { get; set; }

        public string SupplierLogo { get; set; }


        public bool IsAgree { get; set; }
        public bool IsLicenseValid { get; set; } = false;

        public string LicenseErrorMessage { get; set; }
        public string EulAgreement { get; set; }

        public string EULARequired { get; set; }

        public string Token { get; set; }

        public string Name { get; set; }

        public long? ReferenceId { get; set; }

        public string ReferenceType { get; set; }

        public string RoleName { get; set; }


        public string CompanyMnemonic { get; set; }

        public string CompanyName { get; set; }

        public string Addressline1 { get; set; }

        public long? EmptiesLimit { get; set; }

        public long? ActualEmpties { get; set; }

        public string SalesRepName { get; set; }

        public string SalesRepMobileNumber { get; set; }

        public long? ParentLoginId { get; set; }

        public string UserPersona { get; set; }


        public long UserPersonaMasterId { get; set; }

        public string OwnerProductCode { get; set; }

        public string HeinekenProductCode { get; set; }

        public string DistributorProductCode { get; set; }

        public string ParentCompany { get; set; }

        public string TokenString { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }

        public string Area { get; set; }

        public string ProvinceDesc { get; set; }

        public string RegionValue { get; set; }

        public string SubChannel { get; set; }


        public string LanguageCode { get; set; }

        public string ShortCode { get; set; }

        public string Contacts { get; set; }


        public int IsDigitalOnboarding { get; set; }

        public string Url { get; set; }


        public string StatusCode { get; set; }



        public string WebAccessKey { get; set; }

        public long RoleWiseUserPersonaCount { get; set; }

        public string CurrentPassword { get; set; }

        public string EnvironmentCode { get; set; }

        public string IPAddress { get; set; }

        public bool? ChangePasswordonFirstLoginRequired { get; set; }

        public long UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public int NumberOfDaysRemainingForChangePassword { get; set; }

        public int PasswordWarningDays { get; set; }


        [XmlElement(ElementName = "LoginList")]
        public List<LoginDTO> LoginList { get; set; }

        [XmlElement(ElementName = "UserAndDeviceDetails")]
        public UserAndDeviceDetailsDTO UserAndDeviceDetails { get; set; }

        [XmlElement(ElementName = "RoleWiseDefaultPersonaList")]
        public List<RoleWiseDefaultPersonaDTO> RoleWiseDefaultPersonaList { get; set; }

        public LoginDTO()
        {
            LoginList = new List<LoginDTO>();
            RoleWiseDefaultPersonaList = new List<RoleWiseDefaultPersonaDTO>();
        }

    }
}
