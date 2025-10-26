using CartApp.Calculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartApp.CartApp
{
    internal class DefaultShippingCalculator : IShippingCalculator
    {
        public decimal CalculateShipping(decimal subtotal)
        {
            return subtotal < 200m ? 30.00m : 0.00m;
        }
    }
}
