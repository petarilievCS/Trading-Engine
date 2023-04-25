using System;
namespace TradingEngine.Orderbook
{ 
	public class Entry
	{
		public Entry(Order order, Limit limit)
		{
			_order = order;
			_limit = limit;
			CreationTime = DateTime.Now;
		}

		// Properties
		public DateTime CreationTime { get; private set; }
		public Order _order { get; private set; }
		public Limit _limit { get; private set; }
		public Entry Next { get; set; }
		public Entry Prev { get; set; }
	}
}

