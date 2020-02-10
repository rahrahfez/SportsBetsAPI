using System;

namespace LoggerService
{
    public interface ILoggerManager
    {
        void LogError(string message);
        void LogDebug(string message);
        void LogWarn(string message);
        void LogInfo(string message);
    }
}
