using System.Collections.Generic;

namespace CartApp
{
    public interface IReceiptPrinter
    {
        string BuildReceipt(List<Item> items, decimal subtotal, decimal discount, decimal vat, decimal shipping, decimal fragileFee, decimal total);
        void Print(string receipt);
    }
}

