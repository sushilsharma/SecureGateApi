using SecureGate.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SecureGate.ManageLogin.DTO
{
    [Serializable]
    [XmlRoot(ElementName = "PasswordPolicy", IsNullable = false)]
    public class PasswordPolicyDTO
    {
        [XmlElement(ElementName = "PasswordPolicyId")]
        public long PasswordPolicyId { get; set; }
        [XmlElement(ElementName = "PasswordPolicyName")]
        public string PasswordPolicyName { get; set; }
        [XmlElement(ElementName = "RoleMasterId")]
        public long RoleMasterId { get; set; }
        [XmlElement(ElementName = "RolePasswordPolicyMappingId")]
        public long RolePasswordPolicyMappingId { get; set; }
        [XmlElement(ElementName = "RoleName")]
        public string RoleName { get; set; }
        [XmlElement(ElementName = "IsUpperCaseAllowed")]
        public bool? IsUpperCaseAllowed { get; set; }
        [XmlElement(ElementName = "IsLowerCaseAllowed")]
        public bool? IsLowerCaseAllowed { get; set; }
        [XmlElement(ElementName = "IsNumberAllowed")]
        public bool? IsNumberAllowed { get; set; }
        [XmlElement(ElementName = "IsSpecialCharacterAllowed")]
        public bool? IsSpecialCharacterAllowed { get; set; }
        [XmlElement(ElementName = "SpecialCharactersToBeExcluded")]
        public string SpecialCharactersToBeExcluded { get; set; }
        [XmlElement(ElementName = "MinimumUppercaseCharactersRequired")]
        public int? MinimumUppercaseCharactersRequired { get; set; }
        [XmlElement(ElementName = "MinimumLowercaseCharactersRequired")]
        public int? MinimumLowercaseCharactersRequired { get; set; }
        [XmlElement(ElementName = "MinimumSpecialCharactersRequired")]
        public int? MinimumSpecialCharactersRequired { get; set; }
        [XmlElement(ElementName = "MinimumNumericsRequired")]
        public int? MinimumNumericsRequired { get; set; }
        [XmlElement(ElementName = "PasswordExpiryPeriod")]
        public int? PasswordExpiryPeriod { get; set; }
        [XmlElement(ElementName = "NewPasswordShouldNotMatchNoOfLastPassword")]
        public bool? NewPasswordShouldNotMatchNoOfLastPassword { get; set; }
        [XmlElement(ElementName = "MinimumPasswordLength")]
        public int? MinimumPasswordLength { get; set; }
        [XmlElement(ElementName = "MaximumPasswordLength")]
        public int? MaximumPasswordLength { get; set; }
        [XmlElement(ElementName = "CanPasswordBeSameAsUserName")]
        public bool? CanPasswordBeSameAsUserName { get; set; }
        [XmlElement(ElementName = "NumberOfSecurityQuestionsForRegistration")]
        public int? NumberOfSecurityQuestionsForRegistration { get; set; }
        [XmlElement(ElementName = "NumberOfSecurityQuestionsForRecovery")]
        public int? NumberOfSecurityQuestionsForRecovery { get; set; }
        [XmlElement(ElementName = "OneTimePasswordExpireTime")]
        public int? OneTimePasswordExpireTime { get; set; }


        [XmlElement(ElementName = "PasswordPolicyList")]
        public List<PasswordPolicyDTO> PasswordPolicyList { get; set; }

        public PasswordPolicyDTO()
        {
            PasswordPolicyList = new List<PasswordPolicyDTO>();
        }
    }
}
