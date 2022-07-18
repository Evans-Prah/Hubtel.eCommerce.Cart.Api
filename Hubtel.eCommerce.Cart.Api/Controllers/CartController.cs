using Hubtel.eCommerce.Cart.Api.CartsHelper;
using Hubtel.eCommerce.Cart.Api.Logger;
using Hubtel.eCommerce.Cart.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartHelper _cartHelper;
        private readonly IFileLogger _logger;

        public CartController(ICartHelper cartHelper, IFileLogger logger)
        {
            _cartHelper = cartHelper;
            _logger = logger;
        }

        /// <summary>
        /// Gets Cart Items
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="searchTerm"></param>
        /// <param name="items"></param>
        /// <param name="vendors"></param>
        /// <param name="phoneNumbers"></param>
        /// <param name="categories"></param>
        /// <returns>The list of items in a Cart</returns>
        [HttpGet]
        public async Task<ApiResponse> GetCart(string orderBy, string searchTerm, string items, string vendors, string phoneNumbers, string categories)
        {
            StringBuilder logs = new StringBuilder();
            logs.AppendLine($"Request @ {DateTime.Now}, Path: {Request.Path}");

            try
            {
                var request = await _cartHelper.GetCart(orderBy, searchTerm, items, vendors, phoneNumbers, categories);
                if (!request.Successful) return new ApiResponse { Success = false, ResponseMessage = request.ResponseMessage };

                return new ApiResponse { Success = true, ResponseMessage = request.ResponseMessage, Data = request.Data };
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return new ApiResponse { Success = false, ResponseMessage = "A system error occurred while fetching cart, try again later." };
            }
        }

        /// <summary>
        /// Gets Cart Item
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Details of cart item</returns>
        [HttpGet("{id}")]
        public async Task<ApiResponse> GetCartItem(int id)
        {
            StringBuilder logs = new StringBuilder();
            logs.AppendLine($"Request @ {DateTime.Now}, Path: {Request.Path}");

            try
            {
                var request = await _cartHelper.GetCartItem(id);
                if (!request.Successful) return new ApiResponse { Success = false, ResponseMessage = request.ResponseMessage };

                return new ApiResponse { Success = true, ResponseMessage = request.ResponseMessage, Data = request.Data };
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return new ApiResponse { Success = false, ResponseMessage = "A system error occurred while fetching cart item details, try again later." };
            }
        }

        /// <summary>
        /// Add item to Cart
        /// </summary>
        /// <remarks>
        /// Sample request: 
        ///     POST api/Cart 
        ///     {
        ///         "ProductId": 10,
        ///         "Quantity": 3,
        ///         "PhoneNumber": "02222222222"
        ///     }
        /// </remarks>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        public async Task<ApiResponse> AddItem([FromBody] AddItemPayload payload)
        {
            StringBuilder logs = new StringBuilder();
            logs.AppendLine($"Request: {Request.Path}");
            logs.AppendLine($"Request Payload: {JsonConvert.SerializeObject(payload)}");

            try
            {
                var request = await _cartHelper.AddItemToCart(payload.ProductId, payload.Quantity, payload.PhoneNumber, logs);

                if (!request.Successful) return new ApiResponse { Success = false, ResponseMessage = request.ResponseMessage };

                return new ApiResponse { Success = true, ResponseMessage = request.ResponseMessage, Data = request.Data };
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return new ApiResponse { Success = false, ResponseMessage = "A system error occurred while adding item to cart, try again later." };
            }

        }

        
        /// <summary>
        /// Remove Item from Cart
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResponse> RemoveItem(int productId, int quantity)
        {
            StringBuilder logs = new StringBuilder();
            logs.AppendLine($"Request: {Request.Path}");

            try
            {
                var request = await _cartHelper.RemoveItemFromBasket(productId, quantity, logs);

                if (!request.Successful) return new ApiResponse { Success = false, ResponseMessage = request.ResponseMessage };

                return new ApiResponse { Success = true, ResponseMessage = request.ResponseMessage };
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                return new ApiResponse { Success = false, ResponseMessage = "A system error occurred while removing item from cart, try again later." };
            }
        }
    }
}
