using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecureGate.APIController.Framework.AppLogger;
using SecureGate.APIController.Framework.Cache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SecureGate.APIController.Framework.Controllers
{

    /// <summary>
    /// Base class for the APIs providing the common functionality
    /// </summary>
    public abstract class BaseAPIController : ControllerBase
    {
        protected abstract EnumLoggerType LoggerName { get; }
        private BaseAppLogger _loggerInstance;

        public ICacheService cachServicepub { get; }

        public BaseAppLogger LoggerInstance
        {
            get
            {
                GetLogger(LoggerName);
                return _loggerInstance;
            }
        }

        // Get the logger for the class
        protected void GetLogger(EnumLoggerType loggerType)
        {
            this._loggerInstance = gRLoggerFactory.GetLogger(loggerType);
        }




    }
}