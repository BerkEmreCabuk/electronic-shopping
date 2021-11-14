namespace ElectronicShopping.Api.Features.Cart.Models
{
    public class AddProductRequestModel
    {
        public long ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
