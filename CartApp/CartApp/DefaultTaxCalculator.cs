using CartApp.Calculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartApp.CartApp
{
    internal class DefaultTaxCalculator : ITaxCalculator
    {
        public decimal CalculateTax(decimal baseAmount)
        {
            return Math.Round(baseAmount * 0.12m, 2, MidpointRounding.AwayFromZero);
        }
    }
}
