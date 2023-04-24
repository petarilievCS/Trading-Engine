using System;
namespace TradingEngine.Orderbook
{
	public class Order : IOrderCore
	{
		public Order(IOrderCore orderCore, long price, uint quantity, bool isBuy) 
		{
			Price = price;
			IsBuy = isBuy;
			InitialQuantity = quantity;
			CurrentQuantity = quantity;
			_orderCore = orderCore;
		}

		public Order(ModifyOrder modifyOrder) : this(modifyOrder, modifyOrder.NewPrice, modifyOrder.NewQuantity, modifyOrder.IsBuy) { }

		// Properties
		public long Price { get; private set; }
		public uint InitialQuantity { get; private set; }
		public uint CurrentQuantity { get; private set; }
		public bool IsBuy { get; private set; }
        public long ID => _orderCore.ID;
        public string Username => _orderCore.Username;
        public int SecurityID => _orderCore.SecurityID;

        // Methods
        public void increaseQuantity(uint quantityDelta)
		{
			CurrentQuantity += quantityDelta;
		}

		public void decreaseQuantity(uint quantityDelta)
		{
			if (quantityDelta > CurrentQuantity)
			{
				throw new InvalidOperationException($"Delta is bigger than current quantity for ID={ID}");
			}
			CurrentQuantity -= quantityDelta;
		}

		// Fields
		private readonly IOrderCore _orderCore;
	}
}

