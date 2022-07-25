using Hubtel.eCommerce.Cart.Api.Data;
using Hubtel.eCommerce.Cart.Api.DTOs;
using Hubtel.eCommerce.Cart.Api.Entities;
using Hubtel.eCommerce.Cart.Api.Extensions;
using Hubtel.eCommerce.Cart.Api.Models;
using Hubtel.eCommerce.Cart.Api.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Api.CartsHelper
{
    public class CartHelper : ICartHelper
    {
        private readonly StoreContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartHelper(StoreContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<HelperResponse> GetCart(string orderBy, string searchTerm, string items, string vendors, string phoneNumbers, string categories)
        {
            var cart = _context.BasketItems
                .Include(p => p.Product)
                .Where(x => x.Basket.BuyerId == _httpContextAccessor.HttpContext.Request.Cookies["buyerId"])
                .Sort(orderBy)
                .Search(searchTerm)
                .Filter(items, vendors, phoneNumbers, categories)
                .AsQueryable();

            if (cart == null) return new HelperResponse { Successful = false, ResponseMessage = "Cart does not exist" };

            var result = await cart.ToListAsync();

            return new HelperResponse
            {
                Successful = true,
                ResponseMessage = "Cart fetched successfully",
                Data = result
            };
        }

        public async Task<HelperResponse> GetCartItem(int cartItemId)
        {
            var cartItem = await RetrieveCart(cartItemId);

            if (cartItem == null) return new HelperResponse { Successful = false, ResponseMessage = "Item does not exist in cart" };

            return new HelperResponse
            {
                Successful = true,
                ResponseMessage = "Cart item details fetched successfully",
                Data = cartItem
            };
        }

        public async Task<HelperResponse> AddItemToCart(int productId, int quantity, string phoneNumber, StringBuilder logs)
        {
            logs.AppendLine("-- AddItemToCart");
            logs.AppendLine($"Payload: {JsonConvert.SerializeObject(new { productId, quantity, phoneNumber })}");

            if (productId < 1) return new HelperResponse { Successful = false, ResponseMessage = "Product Id is required" };

            if (quantity < 1) return new HelperResponse { Successful = false, ResponseMessage = "Quantity should be at least 1" };

            if (string.IsNullOrWhiteSpace(phoneNumber)) return new HelperResponse { Successful = false, ResponseMessage = "Phone number is required" };

            if (!PhoneNumberUtility.IsValidMsisdn12Digits(phoneNumber)) return new HelperResponse { Successful = false, ResponseMessage = "Enter a valid phone number" };

            phoneNumber = PhoneNumberUtility.FormatNumber12Digits(phoneNumber);

            var cart = await RetrieveCart();

            if (cart == null) cart = CreateCart();

            var product = await _context.Products.FindAsync(productId);

            if (product == null) return new HelperResponse { Successful = false, ResponseMessage = "Product does not exist" };

            var process = cart.AddItem(product, quantity, phoneNumber);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return new HelperResponse { Successful = true, ResponseMessage = process.ResponseMessage, Data = MapBasketToDto(cart) };

            return new HelperResponse { Successful = false, ResponseMessage = "Unable to save item to cart, try again later" };

        }


        public async Task<HelperResponse> RemoveItemFromBasket(int productId, int quantity, StringBuilder logs)
        {
            logs.AppendLine("-- RemoveItemFromBasket");
            logs.AppendLine($"Payload: {JsonConvert.SerializeObject(new { productId, quantity })}");

            if (productId < 1) return new HelperResponse { Successful = false, ResponseMessage = "Product Id is required" };

            if (quantity < 1) return new HelperResponse { Successful = false, ResponseMessage = "Quantity should be at least 1" };

            var cart = await RetrieveCart();

            if (cart == null) return new HelperResponse { Successful = false, ResponseMessage = "Cart does not exist" };

            var process = cart.RemoveItem(productId, quantity);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return new HelperResponse { Successful = true, ResponseMessage = process.ResponseMessage };

            return new HelperResponse { Successful = false, ResponseMessage = process.ResponseMessage };
        }

        private async Task<Basket> RetrieveCart()
        {
            return await _context.Baskets
                .Include(i => i.Items)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(x => x.BuyerId == _httpContextAccessor.HttpContext.Request.Cookies["buyerId"]);
        }

        private async Task<BasketItem> RetrieveCart(int cartId)
        {
            return await _context.BasketItems
                .Include(p => p.Product)
                .FirstOrDefaultAsync(x => x.Id == cartId);
        }

        private Basket CreateCart()
        {
            var buyerId = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(30) };

            _httpContextAccessor.HttpContext.Response.Cookies.Append("buyerId", buyerId, cookieOptions);

            var cart = new Basket { BuyerId = buyerId };

            _context.Baskets.Add(cart);

            return cart;
        }

        private BasketDto MapBasketToDto(Basket cart)
        {
            return new BasketDto
            {
                Id = cart.Id,
                Items = cart.Items.Select(item => new BasketItemDto
                {
                    ProductId = item.ProductId,
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    Description = item.Product.Description,
                    Category = item.Product.Category,
                    Quantity = item.Quantity,
                    Vendor = item.Product.Vendor,
                }).ToList()
            };
        }

    }
}
