using Hubtel.eCommerce.Cart.Api.Data;
using Hubtel.eCommerce.Cart.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Api.ProductsHelper
{
    public class ProductHelper : IProductHelper
    {
        private readonly StoreContext _context;

        public ProductHelper(StoreContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProducts() => await _context.Products.ToListAsync();

        public async Task<Product> GetProduct(int id) => await _context.Products.FindAsync(id);
    }
}
