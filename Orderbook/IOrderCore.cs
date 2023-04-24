using System;

namespace TradingEngine.Orderbook
{
    public interface IOrderCore
    {
        public long ID { get; }
        public string Username { get; }
        public int SecurityID { get; }
    }
}

