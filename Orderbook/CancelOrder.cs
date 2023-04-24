using System;
namespace TradingEngine.Orderbook
{
	public class CancelOrder : IOrderCore
	{
		public CancelOrder(IOrderCore orderCore)
		{
			_orderCore = orderCore;
		}

        // Properties
        public long ID => _orderCore.ID;
        public string Username => _orderCore.Username;
        public int SecurityID => _orderCore.SecurityID;

        // Fields
        private readonly IOrderCore _orderCore;
    }
}

