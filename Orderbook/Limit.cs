using System;
using System.Collections.Generic;

namespace TradingEngine.Orderbook
{
	public class Limit
	{
		public long Price { get; set; }
		public Entry Head { get; set; }
		public Entry Tail { get; set; }

		public Limit(long price)
		{
			Price = price;
		}

		public bool isEmpty
		{
			get
			{
				return Head == null && Tail == null;
			}
		}

		public Side Side
		{
			get
			{
				if (isEmpty)
				{
					return Side.Unknown;
				}
				else
				{
					return Head._order.IsBuy ? Side.Buy : Side.Sell;
				}
			}
		}

        public uint OrderCount()
        {
			uint count = 0;
			Entry current = Head;
			while (current != null)
			{
				if (current._order.CurrentQuantity != 0)
					++count; 
				current = current.Next;
			}
			return count;
        }

		public uint OrderQuantity()
		{
            uint quantity = 0;
            Entry current = Head;
            while (current != null)
			{ 
				quantity += current._order.CurrentQuantity;
                current = current.Next;
            }
            return quantity;
        }

		public List<OrderRecord> OrderRecords()
		{
			List<OrderRecord> list = new List<OrderRecord>();
			Entry current = Head;
			uint queuePosition = 0;
			while (current != null)
			{
				if (current._order.CurrentQuantity != 0)
					list.Add(new OrderRecord(current._order.ID,
						current._order.CurrentQuantity, Price, current._order.IsBuy,
						current._order.Username, current._order.SecurityID,
						queuePosition));
				++queuePosition;
				current = current.Next;
			}
			return list;
		}
    }
}

