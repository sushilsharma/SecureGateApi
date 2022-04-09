using SecureGate.ManageLogin.DTO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureGate.ManageLogin.Business
{
    public interface ILoginManager
    {
        List<LoginDTO> ValidateLogin(LoginDTO loginDTO);

        List<LoginDTO> ValidateOTP(LoginDTO loginDTO);

        string InsertLoginOTP(LoginDTO loginDTO);

 
        string UpdateLoginOTPRegenerated(LoginDTO loginDTO);

         List<LoginDTO>  ChangePassword(LoginDTO loginDTO);

        List<LoginDTO> ContactInformationUpDate(LoginDTO loginDTO);
        List<LoginDTO> ValidateUserNameAndMobileNumber(LoginDTO loginDTO);

        IEnumerable<LoginUserDTO> GetChildUserList(LoginDTO loginDTO);

        IEnumerable<UserOutletMappingDTO> GetSuperUserListV2(UserOutletMappingDTO userOutletMappingDTO);

        UserOutletMappingDTO SaveUserOutletMapping(UserOutletMappingDTO userOutletMappingDTO);

        UserOutletMappingDTO UpdateUserOutletMapping(UserOutletMappingDTO userOutletMappingDTO);

        IEnumerable<LoginDTO> ValidateUserName(LoginDTO loginDTO);

        LoginDTO ActivateDeactivateCompany(LoginDTO loginDTO);

        LoginDTO UpdateLanguageForUser(LoginDTO loginDTO);

        IEnumerable<AppNotificationsDTO> GetAppNotificationsByUserId(LoginDTO loginDTO);


        List<LoginDTO> ValidateUserAndOnboardingReference(LoginDTO loginDTO);

        string GetSettingMasterBySettingParameter(string settingParameter);

        List<UserIdentityDTO> LoadAllEnvironmentUrl(UserIdentityDTO loginDTO);

        string UpdateToken(long userId, string tokenString);

        LoginDTO ValidateToken(LoginDTO loginDTO);

        List<LoginDTO> GetLoginDetailsById(LoginDTO loginDTO);

        List<UserIdentityDTO> UpdateUserIdentityAlternateUserName(UserIdentityDTO userIdentityDTO);
        string UpdateLoginHistoryB2BApp(string input);
        IEnumerable<LoginDTO> GetB2BLogin(LoginDTO loginDTO);

        string DeleteLoginByLoginId(dynamic Json);
        
        string GetLocationDetails(dynamic input);

        string GetProfileDetails(dynamic Json);

        LoginDTO CheckDuplicateUser(LoginDTO loginDTO);

        public JObject CreateLogin(dynamic json);

        LoginDTO UpdateDigitalOnBoardingLink(LoginDTO loginDTO);


        LoginDTO ValidateUserTokenKey(LoginDTO loginDTO);

        string LoadUserProfile(string input);

        LoginDTO GetLoginUserProfileByUserLoginId(LoginDTO loginDto);

        PasswordPolicyDTO GetPasswordPolicyByRoleMasterId(PasswordPolicyDTO passwordPolicyDto);

        LoginDTO GetLoginByUserProfileId(LoginDTO loginDetail);

        Int64 UpdateLoginPassword(LoginDTO loginDto);

        string LoadLookUpListByCritiera(string input);

        string LoadDocumetRequired();

        string GetAllCompanyListByDropDown(string input);

        string GetAllShipToListByCompanyId(string input);

        string LoadAllRoleMaster(string input);

        string LoadLoginDetailByLoginId(string input);

        string GetPageRoleWiseAccessDetailByRoleORUserID(string input);

        string InsertAndUpdateRoleWisePageMappingAndRoleWiseFieldAccess(string input);

    }
}
