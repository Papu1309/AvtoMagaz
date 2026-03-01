using System.Collections.Generic;

namespace AvtoMagaz
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public static class Cart
    {
        public static List<CartItem> Items { get; set; } = new List<CartItem>();

        public static void AddItem(int productId, string name, decimal price, int quantity = 1)
        {
            var existing = Items.Find(i => i.ProductId == productId);
            if (existing != null)
                existing.Quantity += quantity;
            else
                Items.Add(new CartItem { ProductId = productId, ProductName = name, Price = price, Quantity = quantity });
        }

        public static void RemoveItem(int productId)
        {
            Items.RemoveAll(i => i.ProductId == productId);
        }

        public static void UpdateQuantity(int productId, int newQuantity)
        {
            var item = Items.Find(i => i.ProductId == productId);
            if (item != null)
            {
                if (newQuantity <= 0)
                    RemoveItem(productId);
                else
                    item.Quantity = newQuantity;
            }
        }

        public static void Clear()
        {
            Items.Clear();
        }

        public static decimal TotalAmount()
        {
            decimal total = 0;
            foreach (var item in Items)
                total += item.Price * item.Quantity;
            return total;
        }
    }
}