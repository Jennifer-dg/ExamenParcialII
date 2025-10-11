namespace CartApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Sistema de Carrito de Compras (Versión Defectuosa para Examen) ===");

            List<Item> items = new List<Item>
            {
                new Item("Taza", 100m, false),
                new Item("Plato", 60m, false),
                new Item("Florero", 50m, true)
            };

            Console.Write("Ingrese cupón (PROMO10 o ninguno): ");
            string? coupon = Console.ReadLine();

            Console.Write("¿Cliente VIP? (s/n): ");
            bool isVip = Console.ReadLine()?.Trim().ToLower() == "s";

            Console.Write("¿Enviar recibo por email? (s/n): ");
            bool sendEmail = Console.ReadLine()?.Trim().ToLower() == "s";

            CartPriceCalculator calculator = new CartPriceCalculator();
            decimal total = calculator.CalculateTotal(items, coupon, isVip, sendEmail);

            Console.WriteLine($"Total calculado: Q{total:F2}");
            Console.WriteLine("Gracias por su compra!");
        }
    }
}
