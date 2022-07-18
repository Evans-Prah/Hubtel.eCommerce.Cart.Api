namespace Hubtel.eCommerce.Cart.Api.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductUuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Vendor { get; set; }
        public decimal Price { get; set; }
        public int? QuantityInStock { get; set; }
       
    }
}
