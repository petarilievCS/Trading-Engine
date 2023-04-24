using System;
namespace TradingEngine.Logging
{
	public record LogInfo(LogLevel level, string module, string message, DateTime now, int threadID, string threadName); 
}

namespace System.Runtime.CompilerServices
{
	internal static class IsExternalInit { };
}