using System;
using System.Collections.Generic;
using System.Linq;

namespace CartApp
{
    public class CartPriceCalculator
    {
        private readonly ILogger _logger;
        private readonly IReceiptPrinter _printer;
        private readonly EmailSender _emailSender;

        public CartPriceCalculator() : this(new ConsoleLogger(), new ConsoleReceiptPrinter(), new ConsoleEmailSender())
        {
        }

        public CartPriceCalculator(ILogger logger, IReceiptPrinter printer, EmailSender emailSender)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _printer = printer ?? throw new ArgumentNullException(nameof(printer));
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        }

        public decimal CalculateTotal(List<Item> items, string? coupon, bool isVip, bool emailReceipt)
        {
            _logger.Info("Inicio de cálculo de carrito.");

            if (items == null || items.Count == 0)
            {
                _logger.Info("Carrito vacío: total = 0.");
                return 0m;
            }

            foreach (var it in items)
            {
                if (it.Price < 0)
                {
                    var msg = $"Precio inválido en item '{it.Name}'";
                    _logger.Error(msg);
                    throw new ArgumentException(msg);
                }
            }

            decimal subtotal = items.Sum(i => i.Price);

            decimal shipping = subtotal < 200m ? 30.00m : 0.00m;

            decimal discountPercent = 0m;
            if (!string.IsNullOrWhiteSpace(coupon) && coupon.Trim().ToUpper() == "PROMO10")
            {
                discountPercent += 0.10m;
            }
            if (isVip)
            {
                discountPercent += 0.05m;
            }

            decimal discountAmount = Math.Round(subtotal * discountPercent, 2, MidpointRounding.AwayFromZero);
            decimal baseAfterDiscount = subtotal - discountAmount;

            decimal vat = Math.Round(baseAfterDiscount * 0.12m, 2, MidpointRounding.AwayFromZero);

            decimal totalPartial = baseAfterDiscount + vat;

            totalPartial += shipping;

            decimal fragileFee = items.Any(i => i.IsFragile) ? 15.00m : 0.00m;
            totalPartial += fragileFee;

            decimal finalTotal = Math.Round(totalPartial, 2, MidpointRounding.AwayFromZero);

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
