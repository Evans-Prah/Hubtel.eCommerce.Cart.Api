﻿using Hubtel.eCommerce.Cart.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hubtel.eCommerce.Cart.Api.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
    }
}
