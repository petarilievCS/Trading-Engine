using System;
namespace TradingEngine.Core
{
	public interface ITradingEngineServer
	{
		public Task Run(CancellationToken token); 
	}
}

