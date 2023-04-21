using System;
namespace TradingEngine
{
	public interface ITradingEngineServer
	{
		public Task Run(CancellationToken token); 
	}
}

