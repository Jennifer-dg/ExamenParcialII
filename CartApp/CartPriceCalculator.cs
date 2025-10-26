using CartApp.Calculator;
using CartApp.CartApp;
using System;
using System.Collections.Generic;
using System.Linq;
using static Class1;

namespace CartApp
{
    public class CartPriceCalculator
    {
        private readonly ILogger _logger;
        private readonly IReceiptPrinter _printer;
        private readonly IEmailSender _emailSender;
        private readonly IDiscountCalculator _discountCalculator;
        private readonly ITaxCalculator _taxCalculator;
        private readonly IShippingCalculator _shippingCalculator;
        private readonly IFragileFeeCalculator _fragileFeeCalculator;

        public CartPriceCalculator(
            ILogger logger,
            IReceiptPrinter printer,
            IEmailSender emailSender,
            IDiscountCalculator discountCalculator,
            ITaxCalculator taxCalculator,
            IShippingCalculator shippingCalculator,
            IFragileFeeCalculator fragileFeeCalculator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _printer = printer ?? throw new ArgumentNullException(nameof(printer));
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
            _discountCalculator = discountCalculator ?? throw new ArgumentNullException(nameof(discountCalculator));
            _taxCalculator = taxCalculator ?? throw new ArgumentNullException(nameof(taxCalculator));
            _shippingCalculator = shippingCalculator ?? throw new ArgumentNullException(nameof(shippingCalculator));
            _fragileFeeCalculator = fragileFeeCalculator ?? throw new ArgumentNullException(nameof(fragileFeeCalculator));
        }

        // Constructor por defecto (usa implementaciones por defecto)
        public CartPriceCalculator()
            : this(new ConsoleLogger(),
                   new ConsoleReceiptPrinter(),
                   new ConsoleEmailSender(),
                   new DefaultDiscountCalculator(),
                   new DefaultTaxCalculator(),
                   new DefaultShippingCalculator(),
                   new DefaultFragileFeeCalculator())
        {
        }

        public decimal CalculateTotal(List<Item> items, string? coupon, bool isVip, bool emailReceipt)
        {
            _logger.Info("Inicio de cálculo de carrito.");

            if (items == null || items.Count == 0)
            {
                _logger.Info("Carrito vacío: total = 0.");
                return 0m;
            }

            if (items.Any(i => i.Price < 0))
            {
                var msg = "Precio inválido en uno o más items.";
                _logger.Error(msg);
                throw new ArgumentException(msg);
            }

            decimal subtotal = items.Sum(i => i.Price);

            decimal discountAmount = _discountCalculator.CalculateDiscount(subtotal, coupon, isVip);
            decimal baseAfterDiscount = subtotal - discountAmount;
            decimal vat = _taxCalculator.CalculateTax(baseAfterDiscount);
            decimal shipping = _shippingCalculator.CalculateShipping(subtotal);
            decimal fragileFee = _fragileFeeCalculator.CalculateFragileFee(items);

            decimal finalTotal = Math.Round(baseAfterDiscount + vat + shipping + fragileFee, 2, MidpointRounding.AwayFromZero);

            var receipt = _printer.BuildReceipt(items, subtotal, discountAmount, vat, shipping, fragileFee, finalTotal);
            _printer.Print(receipt);

            if (emailReceipt)
            {
                var subject = $"Recibo de compra - Total Q{finalTotal:F2}";
                _emailSender.Send("cliente@example.com", subject, receipt);
            }

            _logger.Info($"Cálculo finalizado. Total Q{finalTotal:F2}.");
            return finalTotal;
        }
    }
}