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
    }
}
