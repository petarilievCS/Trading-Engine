
using System;
using TradingEngine.Logging;

namespace TradingEngine.Logging
{
    public abstract class AbstractLogger : ILogger
    {

        protected AbstractLogger() { }

        public void Debug(string module, string message) => Log(LogLevel.Debug, module, message);

        public void Debug(string module, Exception e) => Log(LogLevel.Debug, module, e.ToString());

        public void Error(string module, string message) => Log(LogLevel.Error, module, message);

        public void Error(string module, Exception e) => Log(LogLevel.Error, module, e.ToString());

        public void Info(string module, string message) => Log(LogLevel.Info, module, message);

        public void Info(string module, Exception e) => Log(LogLevel.Info, module, e.ToString());

        public void Warning(string module, string message) => Log(LogLevel.Warning, module, message);

        public void Warning(string module, Exception e) => Log(LogLevel.Warning, module, e.ToString());

        protected abstract void Log(LogLevel log, string module, string message);
    }
}

