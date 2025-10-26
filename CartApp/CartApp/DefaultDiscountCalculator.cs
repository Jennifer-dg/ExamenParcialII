using CartApp.Calculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartApp.CartApp
{
    internal class DefaultDiscountCalculator : IDiscountCalculator
    {
        public decimal CalculateDiscount(decimal subtotal, string? coupon, bool isVip)
        {
            decimal discountPercent = 0m;

            if (!string.IsNullOrWhiteSpace(coupon) && coupon.Trim().ToUpper() == "PROMO10")
                discountPercent += 0.10m;

            if (isVip)
                discountPercent += 0.05m;

            return Math.Round(subtotal * discountPercent, 2, MidpointRounding.AwayFromZero);
        }
    }
}
