using Hubtel.eCommerce.Cart.Api.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Hubtel.eCommerce.Cart.Api.Data
{
    public static class DbInitializer
    {
        public static void Initialize(StoreContext context)
        {
            if (context.Products.Any()) return;

            var products = new List<Product>
            {
                new Product
                {
                    ProductUuid = "5ea4d823-52d9-4892-8379-99282911fedd",
                    Name = "Ayeshetu Waakye",
                    Description = "Special waakye by Ayeshetu",
                    Price = 20,
                    Category = "Food",
                    Vendor = "Ayeshetu"
                },
                new Product
                {
                    ProductUuid = "5ea4d823-52d9-4892-8379-99282911fedd",
                    Name = "Ayeshetu Jollof",
                    Description = "Special jollof by Ayeshetu",
                    Price = 40,
                    Category = "Food",
                    Vendor = "Ayeshetu"
                }, 
                new Product
                {
                    ProductUuid = "c2bd83c5-f7b5-4f26-88da-f628013c826f",
                    Name = "Samsung S22",
                    Description = "Brand new Samsung S22 phone",
                    Price = 6000,
                    Category = "Accessories",
                    Vendor = "Jake Phones and Accessories"
                },
                new Product
                {
                    ProductUuid = "ffa5e9fa-f1e5-4abe-92d9-9321e64ad644",
                    Name = "Samsung A72",
                    Description = "Brand new Samsung A72 phone",
                    Price = 3000,
                    Category = "Accessories",
                    Vendor = "Gina Phones",
                    QuantityInStock = 5
                },
                new Product
                {
                    ProductUuid = "4544d75c-f301-46d8-a411-f6745dc6e09c",
                    Name = "iPhone 13",
                    Description = "Brand new iPhone 13",
                    Price = 3000,
                    Category = "Accessories",
                    Vendor = "Gina Phones",
                    QuantityInStock = 10
                },
                new Product
                {
                    ProductUuid = "2d63ceba-f013-46f8-aa53-fd5fd37d5a9f",
                    Name = "Paul Smith Suit",
                    Description = "Brand new Paul Smith suit",
                    Price = 3500,
                    Category = "Clothing",
                    Vendor = "Genesis Clothing"
                },
                new Product
                {
                    ProductUuid = "6efad430-4140-44da-825d-7e297849cffe",
                    Name = "Fufu and Goat soup",
                    Description = "Hot afternoon fufu",
                    Price = 50,
                    Category = "Food",
                    Vendor = "Obaapa Kitchen"
                },
                new Product
                {
                    ProductUuid = "d55fc0c1-5eb4-49f5-8376-8d670a11d685",
                    Name = "Fufu and Tilapia soup",
                    Description = "Delicious fufu with tiliapia soup",
                    Price = 70,
                    Category = "Food",
                    Vendor = "Obaapa Kitchen"
                },
                new Product
                {
                    ProductUuid = "61e6eea0-c666-488f-a926-a41a4f955b64",
                    Name = "Jollof and Chicken",
                    Description = "Jollof and chicken like no other",
                    Price = 50,
                    Category = "Food",
                    Vendor = "Obaapa Kitchen"
                },
                new Product
                {
                    ProductUuid = "ec3c90de-fc6e-4b7a-9e53-60a6492e31c1",
                    Name = "Hp Envy Notebook Laptod",
                    Description = "High performance laptod for office work",
                    Price = 5000,
                    Category = "Accessories",
                    Vendor = "Dave Accessories"
                },
                new Product
                {
                    ProductUuid = "86cc665e-fc08-47db-9955-f230fe5dece2",
                    Name = "Armani Lacoste",
                    Description = "High quality lacoste for men",
                    Price = 300,
                    Category = "Clothing",
                    Vendor = "Cedar Clothing"
                },
            };

            foreach (var product in products)
            {
                context.Products.Add(product);
            }

            context.SaveChanges();
        }
    }
}
