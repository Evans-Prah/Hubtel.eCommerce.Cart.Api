# Hubtel.eCommerce.Cart.Api

Hubtel eCommerce Cart Api is built with .NET Core 3.1 Framework, Entity Framework with 
PostgreSQL (Npgsql Entity Framework Core Provider). 

All Seed data and Migrations will be applied when the application is executed for the first time
to create the database tables and populate the seed data to its respective database tables.

Features
1. Add item to cart and increase item quantity
2. Remove item from cart
3. Get the list of cart items and you can perfrom sorting, searching and filtering.
4. Get details of a cart item

Project Folder Structure

* Data Folder
  It contains StoreContext class which defines the Dbcontext. DbInitializer class contains the 
  Seed Data used for the application. Migrations folder contains the executed Database migrations files.
  
* Entity folder
  It contains the database entities(Tables): Product class, Basket class and BasketItem class.
  
* Models folder
  It contains some general classes used for the application and the payload classes.
  
* ProductsHelper folder
  It contains the ProductHelper class and IProductHelper interface used for all business logic related to product
  
* CartsHelper folder
  It contains the CartHelper class and ICartHelper interface used for all business logic related to cart/basket
  
* Extensions folder
  It contains CartExtension class for Sorting, Search and Filter functionalities related to cart/basket items.
  
  -- Sorting: You can sort cart items by these parameters: price, priceDesc, vendor, date, 
			  quantity and the default case when no 'orderBy' parameters provided is sorted by 'vendor'
			  
  -- Search: You can search cart items by 'item/product name' and 'vendor'.
  
  -- Filter: You can filter cart items by items/product names, vendors, phoneNumbers, and cartgories.
			 In the case where you want to filter by multiple values, provide the parameters with comma separated with 
			 no space between the names as it uses query parameters.
			 
			 Example: To filter by categories: food,accessories
  
  
* Logger folder
  It contains a custome logger class used for logging
  
* Controllers folder
  It contains Product controller class for all product related api endpoints, Cart controller class for all cart related endpoints
  
  
STEPS TO LUNCH THE APPLICATION

1. appsettings.Development.json file contains the database connection string. Change it to your PostgreSQL database host and port.

2. You can choose to lunch the application with IIS Express profile or Hubtel.eCommerce.Cart.Api profile which is in 
   the lunchsettings.json file in the pProperties folder.
   
   When using the Hubtel.eCommerce.Cart.Api lunch profile, use commandline and cd 'Hubtel.eCommerce.Cart.Api'.
   Execute 'dotnet watch run' or 'dotnet run' to start the application.
   
3. When the application successfully lunches, the swagger document url will be opened in your default browser.
   You can either use Swagger or Postman to test all the endpoints.
   
 

 