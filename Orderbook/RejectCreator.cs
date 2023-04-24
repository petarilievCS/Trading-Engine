using System;
using TradingEngine.Orderbook;

namespace TradingEngine.Reject
{
	public sealed class RejectCreator
	{
        public static Reject CreateOrderCoreReject(IOrderCore rejectedOrder, RejectionReason rejectionReason)
        {
            return new Reject(rejectedOrder, rejectionReason);
        }

    }
}

