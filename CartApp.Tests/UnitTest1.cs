using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using CartApp;

namespace CartApp.Tests
{
    public class CartAppTests
    {
        private CartPriceCalculator _calculator;

        [SetUp]
        public void Setup()
        {
 
            _calculator = new CartPriceCalculator();
        }

        [Test]
        public void EmptyCart_ReturnsZero()
        {
            var items = new List<Item>();
            var total = _calculator.CalculateTotal(items, null, false, false);
            Assert.AreEqual(0.00m, total);
        }

        [Test]
        public void SingleItem_100_NoCoupon_NoVip_Total142_00()
        {
            var items = new List<Item> { new Item("X", 100m, false) };
            var total = _calculator.CalculateTotal(items, null, false, false);
            Assert.AreEqual(142.00m, total); 
        }

        [Test]
        public void SingleItem_250_NoCoupon_NoVip_Total280_00()
        {
            var items = new List<Item> { new Item("X", 250m, false) };
            var total = _calculator.CalculateTotal(items, null, false, false);
            Assert.AreEqual(280.00m, total); 
        }

        [Test]
        public void TwoItems_80_60_WithPROMO10_Total171_12()
        {
            var items = new List<Item> { new Item("A", 80m, false), new Item("B", 60m, false) };
            var total = _calculator.CalculateTotal(items, "PROMO10", false, false);
            Assert.AreEqual(171.12m, total);
        }

        [Test]
        public void Fragile_210_WithPROMO10_Total226_68()
        {
            var items = new List<Item> { new Item("FragileItem", 210m, true) };
            var total = _calculator.CalculateTotal(items, "PROMO10", false, false);
            Assert.AreEqual(226.68m, total);
        }

        [Test]
        public void TwoFragile_90_120_NoCoupon_Total250_20()
        {
            var items = new List<Item> { new Item("A", 90m, true), new Item("B", 120m, true) };
            var total = _calculator.CalculateTotal(items, null, false, false);
            Assert.AreEqual(250.20m, total);
        }

        [Test]
        public void ShippingBoundary_199_99_vs_200_00()
        {
            var items1 = new List<Item> { new Item("X", 199.99m, false) };
            var items2 = new List<Item> { new Item("X", 200.00m, false) };

            var t1 = _calculator.CalculateTotal(items1, null, false, false);
            var t2 = _calculator.CalculateTotal(items2, null, false, false);

            Assert.AreNotEqual(t1, t2);
            Assert.AreEqual(Math.Round((199.99m * 1.12m) + 30m, 2), t1);
            Assert.AreEqual(Math.Round(200.00m * 1.12m, 2), t2);
        }

        [Test]
        public void NegativePrice_ThrowsArgumentException_WithItemName()
        {
            var items = new List<Item> { new Item("BadItem", -10m, false) };
            var ex = Assert.Throws<ArgumentException>(() => _calculator.CalculateTotal(items, null, false, false));
            StringAssert.Contains("BadItem", ex.Message);
        }

        [Test]
        public void VipAndPROMO10_TwoItems_100_60_ExpectedTotals()
        {
            var items = new List<Item> { new Item("A", 100m, false), new Item("B", 60m, false) };
            var total = _calculator.CalculateTotal(items, "PROMO10", true, false);
            Assert.AreEqual(182.32m, total);
        }

        [Test]
        public void InvalidCoupon_IgnoresDiscount_TotalUnchanged()
        {
            var items = new List<Item> { new Item("A", 100m, false) };
            var total = _calculator.CalculateTotal(items, "NOEXISTE", false, false);
            Assert.AreEqual(142.00m, total);
        }

        [Test]
        public void Receipt_Prints_Keywords_ToConsole()
        {
            var items = new List<Item> { new Item("X", 50m, false) };
            var sw = new StringWriter();
            Console.SetOut(sw);

            _calculator.CalculateTotal(items, null, false, false);

            var output = sw.ToString();
            StringAssert.Contains("RECIBO", output.ToUpper());
            StringAssert.Contains("TOTAL", output.ToUpper());
        }

        [Test]
        public void EmailReceipt_FlagTrue_PrintsEmailBlock()
        {
            var items = new List<Item> { new Item("X", 100m, false) };
            var sw = new StringWriter();
            Console.SetOut(sw);

            _calculator.CalculateTotal(items, null, false, true);

            var output = sw.ToString();
            StringAssert.Contains("Enviando Email", output);
            StringAssert.Contains("cliente@example.com", output);
            StringAssert.Contains("RECIBO", output.ToUpper());
        }

        [Test]
        public void Combined_Fragile_Promo10_VIP_Email_Total224_44()
        {
            var items = new List<Item> {
                new Item("A", 120m, true),
                new Item("B", 100m, false)
            };
            var sw = new StringWriter();
            Console.SetOut(sw);

            var total = _calculator.CalculateTotal(items, "PROMO10", true, true);

            Assert.AreEqual(224.44m, total); 
            var output = sw.ToString();
            StringAssert.Contains("RECIBO", output.ToUpper());
            StringAssert.Contains("Enviando Email", output);
        }
    }
}