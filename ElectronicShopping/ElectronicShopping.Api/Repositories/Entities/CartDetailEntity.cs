using ElectronicShopping.Api.Repositories.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicShopping.Api.Repositories.Entities
{
    public class CartDetailEntity : BaseEntity
    {
        public long CartId { get; set; }
        public long ItemId { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("CartId")]
        public CartEntity Cart { get; set; }

        [ForeignKey("ItemId")]
        public ItemEntity Item { get; set; }

        public void AddQuantity(int quantity)
        {
            Quantity += quantity;
            Cart.AddAmount(quantity * Item.Price);
            Update();
        }
        public void ChangeQuantity(int quantity)
        {
            Cart.AddAmount((quantity - Quantity) * Item.Price);
            Quantity = quantity;
            Update();
        }
    }
}
