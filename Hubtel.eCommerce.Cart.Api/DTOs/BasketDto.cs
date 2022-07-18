using System.Collections.Generic;

namespace Hubtel.eCommerce.Cart.Api.DTOs
{
    public class BasketDto
    {
        public int Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
    }
}
