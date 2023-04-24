using System;
namespace TradingEngine.Orderbook
{
	public class ModifyOrder : IOrderCore
	{
		public ModifyOrder(IOrderCore orderCore, long newPrice, uint newQuantity, bool isBuy)
		{
            NewPrice = newPrice;
            NewQuantity = newQuantity;
            IsBuy = isBuy;
			_orderCore = orderCore;
		}

        // Properties
        public long NewPrice { get; private set; }
        public uint NewQuantity { get; private set; }
        public bool IsBuy { get; private set; }
        public long ID => _orderCore.ID;
        public string Username => _orderCore.Username;
        public int SecurityID => _orderCore.SecurityID;

        // Methods
        public CancelOrder Cancel()
        {
            return new CancelOrder(this);
        }

        public Order New()
        {
            return new Order(this);
        }

        // Fields
        private readonly IOrderCore _orderCore;
    }
}

