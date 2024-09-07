using SecureGate.Framework.Serializer;
using SecureGate.ManageLogin.DataAccess;
using SecureGate.ManageLogin.DTO;
using System.Collections.Generic;
using SecureGate.APIController.Framework.AppLogger;

namespace SecureGate.ManageLogin.Business
{
    public class SecureGateFrameworkLoginManager : ISecureGateFrameworkLoginManager
    {
        BaseAppLogger _loggerInstance;
        public SecureGateFrameworkLoginManager(BaseAppLogger loggerInstance)
        {
            _loggerInstance = loggerInstance;
        }
        public IEnumerable<LoginDTO> ValidateLogin(LoginDTO loginDTO)
        {
            IEnumerable<LoginDTO> loginDTOs = null;
            string loginXml = ObjectXMLSerializer<LoginDTO>.Save(loginDTO);
            string output = SecureGateFrameworkLoginDataAccessManager.ValidateLogin<string>(loginXml);
            if (output != null)
            {
                loginDTOs = ObjectXMLSerializer<LoginDTO>.Load(output).LoginList;
            }
            return loginDTOs;
        }

        public IEnumerable<LoginDTO> GetUserDetailsByUsername(LoginDTO loginDTO)
        {
            IEnumerable<LoginDTO> loginDTOs = null;
            string loginXml = ObjectXMLSerializer<LoginDTO>.Save(loginDTO);
            string output = SecureGateFrameworkLoginDataAccessManager.GetUserDetailsByUsername<string>(loginXml);
            if (output != null)
            {
                loginDTOs = ObjectXMLSerializer<LoginDTO>.Load(output).LoginList;
            }
            return loginDTOs;
        }

        public IEnumerable<LoginDTO> GetUserFullDetailsByUsername(LoginDTO loginDTO)
        {
            IEnumerable<LoginDTO> loginDTOs = null;
            string loginXml = ObjectXMLSerializer<LoginDTO>.Save(loginDTO);
            string output = SecureGateFrameworkLoginDataAccessManager.GetUserFullDetailsByUsername<string>(loginXml);
            if (output != null)
            {
                loginDTOs = ObjectXMLSerializer<LoginDTO>.Load(output).LoginList;
            }
            return loginDTOs;
        }



        public string UpdateToken(long userId, string tokenString, string apptype)
        {
            string output = SecureGateFrameworkLoginDataAccessManager.UpdateToken(userId, tokenString, apptype);
            return output;
        }
    }
}
