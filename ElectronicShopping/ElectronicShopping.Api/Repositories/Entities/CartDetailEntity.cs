using ElectronicShopping.Api.Repositories.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicShopping.Api.Repositories.Entities
{
    public class CartDetailEntity : BaseEntity
    {
        public long CartId { get; set; }
        public long ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        [ForeignKey("CartId")]
        public CartEntity Cart { get; set; }

        [ForeignKey("ItemId")]
        public ItemEntity Item { get; set; }

        public void AddQuantity(int quantity)
        {
            var price = GetPrice();
            AddAmount(quantity, price);
            Quantity += quantity;
            Update();
        }
        public void ChangeQuantity(int quantity)
        {
            var price = GetPrice();
            AddAmount((quantity - Quantity));
            Cart.AddAmount((quantity - Quantity) * price);
            Quantity = quantity;
            Update();
        }
        public void AddAmount(int quantity, decimal? price = null)
        {
            if (!price.HasValue)
                price = GetPrice();
            Amount += price.Value * quantity;
            Cart.AddAmount(price.Value * quantity);
        }
        public decimal GetPrice()
        {
            return Amount / Quantity;
        }
    }
}
