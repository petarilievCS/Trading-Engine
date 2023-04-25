using System;
using TradingEngine.Orderbook;

namespace TradingEngine.OrderbookCS
{
	public interface IOrderEntryOrderbook : IReadOnlyOrderbook
	{
		void AddOrder(Order order);
		void ChangeOrder(ModifyOrder order);
		void CancelOrder(CancelOrder order);
	}
}

