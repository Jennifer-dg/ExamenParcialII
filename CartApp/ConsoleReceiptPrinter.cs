using System;
using System.Text;
using System.Collections.Generic;

namespace CartApp
{
    public class ConsoleReceiptPrinter : ReceiptPrinter
    {
        public string BuildReceipt(List<Item> items, decimal subtotal, decimal discount, decimal vat, decimal shipping, decimal fragileFee, decimal total)
        {
            var lines = new StringBuilder();
            lines.AppendLine("RECIBO DE COMPRA");
            foreach (var it in items)
            {
                lines.AppendLine($"- {it.Name}  Q{it.Price:F2} {(it.IsFragile ? "(Frágil)" : "")}");
            }
            lines.AppendLine($"Subtotal: Q{subtotal:F2}");
            lines.AppendLine($"Descuento: -Q{discount:F2}");
            lines.AppendLine($"IVA: Q{vat:F2}");
            lines.AppendLine($"Envío: Q{shipping:F2}");
            lines.AppendLine($"Recargo frágil: Q{fragileFee:F2}");
            lines.AppendLine($"TOTAL: Q{total:F2}");
            return lines.ToString();
        }

        public void Print(string receipt)
        {
            Console.WriteLine(receipt);
        }
    }
}