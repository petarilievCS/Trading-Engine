using System;

namespace TradingEngine.OrderbookCS
{
    public interface IReadOnlyOrderbook
    {
        bool ContainsOrder(long ID);
        OrderbookSpread GetSpread();
        int Count { get; }
    }
}

