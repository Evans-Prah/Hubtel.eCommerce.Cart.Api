using Hubtel.eCommerce.Cart.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Api.Entities
{
    public class Basket
    {

        public int Id { get; set; }
        public string BuyerId { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();

        public HelperResponse AddItem(Product product, int quantity, string phoneNumber)
        {
            if (Items.All(item => item.ProductId != product.Id))
            {
                Items.Add(new BasketItem
                {
                    Product = product,
                    Quantity = quantity,
                    PhoneNumber = phoneNumber
                });

                return new HelperResponse { Successful = true, ResponseMessage = "Item added to cart" };
            }

            var existingItem = Items.FirstOrDefault(item => item.ProductId == product.Id);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                existingItem.DateModified = DateTime.UtcNow;
            }

            return new HelperResponse { Successful = true, ResponseMessage = "Item quantity increased" };
        }

        public HelperResponse RemoveItem(int productId, int quantity)
        {
            var item = Items.FirstOrDefault(item => item.ProductId == productId);

            if (item == null) return new HelperResponse { Successful = false, ResponseMessage = "Item does not exist in cart, check and try again." };

            if(quantity > item.Quantity) return new HelperResponse { Successful = false, ResponseMessage = $"Item quantity cannot be reduced. You have quantity of {item.Quantity} in your cart" };

            item.Quantity -= quantity;

            if (item.Quantity == 0) Items.Remove(item);

            return new HelperResponse { Successful = true, ResponseMessage = "Item quantity reduced" };
        }
    }
}
