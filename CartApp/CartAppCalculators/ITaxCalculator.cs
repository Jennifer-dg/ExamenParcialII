using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartApp.Calculator
{
    internal interface ITaxCalculator
    {
        decimal CalculateTax(decimal baseAmount);
    }
}
