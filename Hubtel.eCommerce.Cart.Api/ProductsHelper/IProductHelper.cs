using Hubtel.eCommerce.Cart.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Api.ProductsHelper
{
    public interface IProductHelper
    {
        Task<Product> GetProduct(int id);
        Task<List<Product>> GetProducts();
    }
}