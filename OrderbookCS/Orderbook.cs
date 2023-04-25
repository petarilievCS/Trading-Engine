using System;
using System.Collections.Generic;
using TradingEngine.Orderbook;
using TradingEngine.Instrument;
using System.Linq;

namespace TradingEngine.OrderbookCS
{
	public class Orderbook : IRetrievalOrderbook
	{
        private readonly Security _instrument;
        private readonly Dictionary<long, Entry> _orders = new Dictionary<long, Entry>();
        private readonly SortedSet<Limit> _askLimits = new SortedSet<Limit>(AskLimitComparer.Comparer);
        private readonly SortedSet<Limit> _bidLimits = new SortedSet<Limit>(BidLimitComparer.Comparer);

        public Orderbook(Security instrument)
		{
            _instrument = instrument;
		}

        public int Count => _orders.Count;

        public void AddOrder(Order order)
        {
            var baseLimit = new Limit(order.Price);
            AddOrder(order, baseLimit, order.IsBuy ? _bidLimits : _askLimits, _orders);
        }

        private void AddOrder(Order order, Limit baseLimit, SortedSet<Limit> limits, Dictionary<long, Entry> orders)
        {
            if (limits.TryGetValue(baseLimit, out Limit limit))
            {
                Entry orderbookEntry = new Entry(order, baseLimit);
                if (limit.Head == null)
                {
                    limit.Head = orderbookEntry;
                    limit.Tail = orderbookEntry;
                } else
                {
                    Entry tailPtr = limit.Tail;
                    tailPtr.Next = orderbookEntry;
                    orderbookEntry.Prev = tailPtr;
                    limit.Tail = orderbookEntry;
                }
                _orders.Add(order.ID, orderbookEntry);
            }
            else
            {
                limits.Add(baseLimit);
                Entry orderbookEntry = new Entry(order, baseLimit);
                baseLimit.Head = orderbookEntry;
                baseLimit.Tail = orderbookEntry;
                _orders.Add(order.ID, orderbookEntry);
            }
        }

        public List<Entry> AskOrders()
        {
            List<Entry> list = new List<Entry>();
            foreach (var ask in _askLimits)
            {
                if (ask.isEmpty)
                    continue;
                else
                {
                    Entry current = ask.Head;
                    while (current != null)
                    {
                        list.Add(current);
                        current = current.Next;
                    }
                }
            }
            return list;
        }

        public List<Entry> BidOrders()
        {
            List<Entry> list = new List<Entry>();
            foreach (var bid in _bidLimits)
            {
                if (bid.isEmpty)
                    continue;
                else
                {
                    Entry current = bid.Head;
                    while (current != null)
                    {
                        list.Add(current);
                        current = current.Next;
                    }
                }
            }
            return list;
        }

        public void CancelOrder(CancelOrder order)
        {
            if (_orders.TryGetValue(order.ID, out Entry entry))
            {
                RemoveOrder(order, entry, _orders);
            }
        }

        private void RemoveOrder(CancelOrder order, Entry entry, Dictionary<long, Entry> orders)
        {
            // Middle 
            if (entry.Next != null && entry.Prev != null)
            {
                entry.Next.Prev = entry.Prev;
                entry.Prev.Next = entry.Next;
            }
            else if (entry.Prev != null) // Front
            {
                entry.Prev.Next = null;
            }
            else if (entry.Next != null) // Back
            {
                entry.Next.Prev = null;
            }

            // Remove orderbook entry from limits
            if (entry._limit.Head == entry && entry._limit.Tail == entry)
            {
                entry._limit.Head = null;
                entry._limit.Tail = null;
            }
            else if (entry._limit.Head == entry)
            {
                entry._limit.Head = entry.Next;
            }
            else if (entry._limit.Tail == entry)
            {
                entry._limit.Tail = entry.Prev;
            }

            _orders.Remove(entry._order.ID);
        }

        public void ChangeOrder(ModifyOrder order)
        {
            if (_orders.TryGetValue(order.ID, out Entry entry))
            {
                CancelOrder(order.Cancel());
                AddOrder(order.New(), entry._limit, order.IsBuy ? _bidLimits : _askLimits, _orders);
            }
        }

        public bool ContainsOrder(long ID)
        {
           return _orders.ContainsKey(ID);
        }

        public OrderbookSpread GetSpread()
        {
            long? bestAsk = null;
            long? bestBid = null;
            if (_askLimits.Any() && !_askLimits.Min.isEmpty)
                bestAsk = _askLimits.Min.Price;
            if (_bidLimits.Any() && !_bidLimits.Max.isEmpty)
                bestAsk = _bidLimits.Max.Price;
            return new OrderbookSpread(bestBid, bestAsk);

        }
    }
}

