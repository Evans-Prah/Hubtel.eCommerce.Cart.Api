using Hubtel.eCommerce.Cart.Api.Logger;
using Hubtel.eCommerce.Cart.Api.Models;
using Hubtel.eCommerce.Cart.Api.ProductsHelper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductHelper _productHelper;
        private readonly IFileLogger _logger;

        public ProductsController(IProductHelper productHelper, IFileLogger logger)
        {
            _productHelper = productHelper;
            _logger = logger;
        }

        /// <summary>
        /// Gets the list of Products
        /// </summary>
        /// <returns>The list of Products</returns>
        [HttpGet]
        public async Task<ApiResponse> GetProducts()
        {
            StringBuilder logs = new StringBuilder();
            logs.AppendLine($"Request @ {DateTime.Now}, Path: {Request.Path}");

            try
            {
                var products = await _productHelper.GetProducts();

                if (products == null || !products.Any()) return new ApiResponse { Success = false, ResponseMessage = "Sorry, there are no available products currently. Try again later" };

                return new ApiResponse { Success = true, ResponseMessage = "Products fetched successfully", Data = products };
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return new ApiResponse { Success = false, ResponseMessage = "A system error occurred while fetching products, try again later." };
            }
        }

        /// <summary>
        /// Gets details of a Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The details of a Product</returns>
        [HttpGet("{id}")]
        public async Task<ApiResponse> GetProduct(int id)
        {
            StringBuilder logs = new StringBuilder();
            logs.AppendLine($"Request @ {DateTime.Now}, Path: {Request.Path}");

            try
            {
                var product = await _productHelper.GetProduct(id);

                if (product == null) return new ApiResponse { Success = false, ResponseMessage = "Product does not exist" };

                return new ApiResponse { Success = true, ResponseMessage = "Product details fetched successfully", Data = product };
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return new ApiResponse { Success = false, ResponseMessage = "A system error occurred while fetching product details, try again later." };
            }
        }
    }
}
