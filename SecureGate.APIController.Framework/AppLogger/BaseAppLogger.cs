using NLog;

namespace SecureGate.APIController.Framework.AppLogger
{

    /// <summary>
    /// Base class providing the logging functionality
    /// </summary>
    public class BaseAppLogger
    {
        private ILogger _logger;
        protected BaseAppLogger() { }

        protected BaseAppLogger(EnumLoggerType loggerType)
        {
            _logger = LogManager.GetLogger(loggerType.ToString());
        }

        #region Common
        public void Information(string message,int eventId)
        {
            _logger.Info(message,eventId);
        }

        public void Warning(string message, int eventId)
        {
            _logger.Warn(message, eventId);
        }

        public void Debug(string message, int eventId)
        {
            _logger.Debug(message, eventId);
        }

        public void Error(string message, int eventId)
        {
            _logger.Error(message, eventId);
        }
        public void Fatal(string message, int eventId)
        {
            _logger.Fatal(message, eventId);
        }
        #endregion
    }
}
