using SecureGate.Framework.Serializer;
using SecureGate.ManageLogin.DataAccess;
using SecureGate.ManageLogin.DTO;
using System.Collections.Generic;
using SecureGate.APIController.Framework.AppLogger;

namespace SecureGate.ManageLogin.Business
{
    public class glassRUNFrameworkLoginManager : IglassRUNFrameworkLoginManager
    {
        BaseAppLogger _loggerInstance;
        public glassRUNFrameworkLoginManager(BaseAppLogger loggerInstance)
        {
            _loggerInstance = loggerInstance;
        }
        public IEnumerable<LoginDTO> ValidateLogin(LoginDTO loginDTO)
        {
            IEnumerable<LoginDTO> loginDTOs = null;
            string loginXml = ObjectXMLSerializer<LoginDTO>.Save(loginDTO);
            string output = glassRUNFrameworkLoginDataAccessManager.ValidateLogin<string>(loginXml);
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
            string output = glassRUNFrameworkLoginDataAccessManager.GetUserDetailsByUsername<string>(loginXml);
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
            string output = glassRUNFrameworkLoginDataAccessManager.GetUserFullDetailsByUsername<string>(loginXml);
            if (output != null)
            {
                loginDTOs = ObjectXMLSerializer<LoginDTO>.Load(output).LoginList;
            }
            return loginDTOs;
        }



        public string UpdateToken(long userId, string tokenString, string apptype)
        {
            string output = glassRUNFrameworkLoginDataAccessManager.UpdateToken(userId, tokenString, apptype);
            return output;
        }
    }
}
