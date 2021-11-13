using ElectronicShopping.Api.Enums;
using ElectronicShopping.Api.Repositories.Entities.Common;
using System.Collections.Generic;

namespace ElectronicShopping.Api.Repositories.Entities
{
    public class CartEntity : BaseEntity
    {
        public long UserId { get; set; }
        public decimal Amount { get; set; }
        public ICollection<CartDetailEntity> CartDetails { get; set; }
        public void AddAmount(decimal amount)
        {
            Amount += amount;
            Update();
        }
    }
}
