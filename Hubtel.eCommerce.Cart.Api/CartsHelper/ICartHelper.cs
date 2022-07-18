using Hubtel.eCommerce.Cart.Api.Models;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Api.CartsHelper
{
    public interface ICartHelper
    {
        Task<HelperResponse> AddItemToCart(int productId, int quantity, string phoneNumber, StringBuilder logs);
        Task<HelperResponse> GetCart(string orderBy, string searchTerm, string items, string vendors, string phoneNumbers, string categories);
        Task<HelperResponse> GetCartItem(int cartItemId);
        Task<HelperResponse> RemoveItemFromBasket(int productId, int quantity, StringBuilder logs);
    }
}