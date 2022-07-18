using Hubtel.eCommerce.Cart.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hubtel.eCommerce.Cart.Api.Extensions
{
    public static class CartExtensions
    {
        public static IQueryable<BasketItem> Sort(this IQueryable<BasketItem> query, string orderBy)
        {
            if(string.IsNullOrWhiteSpace(orderBy)) return query.OrderBy(c => c.Product.Vendor);

            query = orderBy switch
            {
                "price" => query.OrderBy(c => c.Product.Price),
                "priceDesc" => query.OrderByDescending(c => c.Product.Price),
                "vendor" => query.OrderBy(c => c.Product.Vendor),
                "date" => query.OrderByDescending(c => c.DateAdded),
                "quantity" => query.OrderByDescending(c => c.Quantity),
                _ => query.OrderBy(c => c.Product.Vendor)
            };

            return query;
        }

        public static IQueryable<BasketItem> Search(this IQueryable<BasketItem> query, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return query;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return query.Where(c => c.Product.Name.ToLower().Contains(lowerCaseSearchTerm) || c.Product.Vendor.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<BasketItem> Filter(this IQueryable<BasketItem> query, string items, string vendors, string phoneNumbers, string categories)
        {
            var itemList = new List<string>();
            var vendorList = new List<string>();
            var phoneNumberList = new List<string>();
            var categoryList = new List<string>();

            if (!string.IsNullOrWhiteSpace(items)) itemList.AddRange(items.ToLower().Split(",").ToList());
            if (!string.IsNullOrWhiteSpace(vendors)) vendorList.AddRange(vendors.ToLower().Split(",").ToList());
            if (!string.IsNullOrWhiteSpace(phoneNumbers)) phoneNumberList.AddRange(phoneNumbers.ToLower().Split(",").ToList());
            if (!string.IsNullOrWhiteSpace(categories)) categoryList.AddRange(categories.ToLower().Split(",").ToList());

            query = query.Where(c => itemList.Count == 0 || itemList.Contains(c.Product.Name.ToLower()));
            query = query.Where(c => vendorList.Count == 0 || vendorList.Contains(c.Product.Vendor.ToLower()));
            query = query.Where(c => phoneNumberList.Count == 0 || phoneNumberList.Contains(c.PhoneNumber.ToLower()));
            query = query.Where(c => categoryList.Count == 0 || categoryList.Contains(c.Product.Category.ToLower()));

            return query;
        }
    }
}
