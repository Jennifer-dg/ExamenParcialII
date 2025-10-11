namespace CartApp
{
    public class Item
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsFragile { get; set; }

        public Item(string name, decimal price, bool fragile)
        {
            Name = string.IsNullOrWhiteSpace(name) ? "UNKNOWN" : name;
            Price = price;
            IsFragile = fragile;
        }
    }
}
