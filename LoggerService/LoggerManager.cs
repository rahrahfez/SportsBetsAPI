﻿using System;
using NLog;

namespace LoggerService
{
    public class LoggerManager : ILoggerManager
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public LoggerManager()
        {
        }
        public void LogInfo(string message)
        {
            logger.Info(message);
        }
        public void LogError(string message)
        {
            logger.Error(message);
        }
        public void LogWarn(string message)
        {
            logger.Warn(message);
        }
        public void LogDebug(string message)
        {
            logger.Debug(message);
        }
    }
}
