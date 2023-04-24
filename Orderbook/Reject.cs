using System;
using TradingEngine.Orderbook;

namespace TradingEngine.Reject
{
	public class Reject : IOrderCore
	{
		public Reject(IOrderCore rejectedOrder, RejectionReason rejectionReason)
		{
			_orderCore = rejectedOrder;
			RejectionReason = rejectionReason;
		}

		// Properties
		public RejectionReason RejectionReason { get; private set; }
        public long ID => _orderCore.ID;
        public string Username => _orderCore.Username;
        public int SecurityID => _orderCore.SecurityID;

		// Fields
        private readonly IOrderCore _orderCore;
	}
}

