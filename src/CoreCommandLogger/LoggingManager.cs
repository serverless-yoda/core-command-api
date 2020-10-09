using System;
using NLog;
using CoreCommandContracts;

namespace CoreCommandLogger
{
    public class LoggingManager: ILoggerManager
    {
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();
         public void  LogError(string message) => logger.Error(message);
         public void LogDebug(string message) => logger.Debug(message);
         public void LogInfo(string message) => logger.Info(message);
         public void LogWarning(string message) => logger.Warn(message);
    }
}
