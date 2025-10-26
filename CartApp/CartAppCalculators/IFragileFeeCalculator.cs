using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartApp.Calculator
{
    internal interface IFragileFeeCalculator
    {
        decimal CalculateFragileFee(List<Item> items);
    }
}
