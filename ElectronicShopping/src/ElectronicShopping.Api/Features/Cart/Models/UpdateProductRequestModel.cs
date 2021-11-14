namespace ElectronicShopping.Api.Features.Cart.Models
{
    public class UpdateProductRequestModel
    {
        public long ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
