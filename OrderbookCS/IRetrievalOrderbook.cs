using System;
using System.Collections.Generic;
using TradingEngine.Orderbook;

namespace TradingEngine.OrderbookCS
{
	public interface IRetrievalOrderbook : IOrderEntryOrderbook
	{
		List<Entry> AskOrders();
		List<Entry> BidOrders();
	}
}

