using System;
namespace TradingEngine.Orderbook
{
	public record OrderRecord
	{
		public OrderRecord(long ID, uint Quantity, long Price, bool isBuy, string Username, int SecurityID, uint QueuePosition)
		{

		}
	}
}

namespace System.Runtime.CompilerServices
{
	internal static class IsExternalLimit { };
}