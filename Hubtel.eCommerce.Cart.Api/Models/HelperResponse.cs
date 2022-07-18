namespace Hubtel.eCommerce.Cart.Api.Models
{
    public class HelperResponse
    {
        public bool Successful { get; set; }
        public string ResponseMessage { get; set; }
        public dynamic Data { get; set; }
    }
}
