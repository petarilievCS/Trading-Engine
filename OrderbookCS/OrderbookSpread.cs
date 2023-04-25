using System;
namespace TradingEngine.OrderbookCS
{
	public class OrderbookSpread
	{
		public OrderbookSpread(long? bid, long? ask) 
		{
			Bid = bid;
			Ask = ask;
		}

		public long? Bid { get; private set; }
        public long? Ask { get; private set; }
		public long? Spread
		{
			get
			{
				if (Ask != null && Bid != null)
				{
					return Ask.Value - Bid.Value;
				}
				return null;
			}
		}
    }
}

