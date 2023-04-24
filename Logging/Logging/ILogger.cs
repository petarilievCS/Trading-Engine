using System;
namespace TradingEngine.Logging
{
	public interface ILogger
	{
		void Debug(string module, string message);
		void Debug(string module, Exception e);

        void Info(string module, string message);
        void Info(string module, Exception e);

        void Warning(string module, string message);
        void Warning(string module, Exception e);

        void Error(string module, string message);
        void Error(string module, Exception e);
    }
}

