using CartApp.Calculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartApp.CartApp
{
    internal class DefaultFragileFeeCalculator : IFragileFeeCalculator
    {
        public decimal CalculateFragileFee(List<Item> items)
        {
            return items.Any(i => i.IsFragile) ? 15.00m : 0.00m;
        }
    }
}
