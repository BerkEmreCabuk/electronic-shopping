namespace ElectronicShopping.Api.Features.Cart.Models
{
    public class AddProductRequestModel
    {
        public long? CartId { get; set; }
        public long ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
