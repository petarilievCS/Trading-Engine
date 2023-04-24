using System;
namespace TradingEngine.Orderbook
{
	public class OrderCore : IOrderCore
	{
		public OrderCore(long ID, string Username, int SecurityID)
		{
			this.ID = ID;
			this.Username = Username;
			this.SecurityID = SecurityID;
		}

		public long ID { get; private set; }
		public int SecurityID { get; private set; }
		public string Username { get; private set; }
    }
}

