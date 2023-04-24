using System;
namespace TradingEngine.Orderbook
{
	public  sealed class OrderStatusCreator
	{
		public static CancelOrderStatus CreateCancelOrderStatus(CancelOrder cancelOrder)
		{
			return new CancelOrderStatus();
		}

        public static NewOrderStatus CreateNewOrderStatus(Order newOrder)
        {
            return new NewOrderStatus();
        }

        public static ModifyOrderStatus CreateModifyOrderStatus(ModifyOrder modifyOrder)
        {
            return new ModifyOrderStatus();
        }
    }
}

