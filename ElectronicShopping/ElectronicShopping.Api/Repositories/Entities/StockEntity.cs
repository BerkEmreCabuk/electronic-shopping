using ElectronicShopping.Api.Repositories.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicShopping.Api.Repositories.Entities
{
    public class StockEntity : BaseEntity
    {
        public long ItemId { get; set; }
        public int Quantity { get; set; }
        public long FreeQuantity { get; set; }
        [ForeignKey("ItemId")]
        public ItemEntity Item { get; set; }
        public bool InsufficientFreeQuantity(int quantity)
        {
            return quantity > this.FreeQuantity;
        }

        public void ReduceFreeQuantity(int quantity)
        {
            this.FreeQuantity -= quantity;
            this.Update();
        }
    }
}
