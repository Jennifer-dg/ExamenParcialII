namespace CartApp
{
    public static class StaticConfig
    {
        public static decimal VatRate = 0.12m;
        public static decimal ShippingThreshold = 200m;
        public static decimal ShippingBelowFee = 30m;
        public static decimal ShippingAboveOrEqualFee = 0m;
        public static decimal Promo10DiscountRate = 0.10m;
        public static decimal VipExtraDiscountRate = 0.05m;
        public static decimal FragileSurcharge = 15m;
        public static string ReceiptEmailTo = "cliente@example.com";
        public static string ReceiptEmailSubject = "Recibo de compra";
    }
}
