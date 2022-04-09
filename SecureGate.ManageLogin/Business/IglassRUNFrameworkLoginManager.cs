using SecureGate.ManageLogin.DTO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureGate.ManageLogin.Business
{
    public interface IglassRUNFrameworkLoginManager
    {
        IEnumerable<LoginDTO> ValidateLogin(LoginDTO loginDTO);
        IEnumerable<LoginDTO> GetUserDetailsByUsername(LoginDTO loginDTO);
        IEnumerable<LoginDTO> GetUserFullDetailsByUsername(LoginDTO loginDTO);

        string UpdateToken(long userId, string tokenString, string apptype);
    }
}
