namespace Basket.API.Entities
{
    public class ShoppingCartItem
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; } // maybe like table name in sql db
        public string ProductId { get; set; } // id in table
    }
}
