namespace Hubtel.eCommerce.Cart.Api.Models
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string ResponseMessage { get; set; }
        public dynamic Data { get; set; }
    }
}
