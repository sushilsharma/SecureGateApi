using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureGate.APIController.Framework.AppLogger
{

    public class LoginLogger : BaseAppLogger
    {
        public LoginLogger() : base(EnumLoggerType.Login)
        {

        }
    }
}
