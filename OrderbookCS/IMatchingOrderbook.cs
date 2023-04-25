using System;
namespace TradingEngine.OrderbookCS
{
	public interface IMatchingOrderbook : IRetrievalOrderbook
	{
		MatchResult Match();
	}
}

