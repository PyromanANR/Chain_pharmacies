﻿using Chain_pharmacies.Data;
using Chain_pharmacies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Immutable;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Chain_pharmacies.Controllers
{
    public class OrderController : Controller
    {
        private readonly NetworkOfPharmaciesContext _context; 

        public OrderController(NetworkOfPharmaciesContext context) 
        {
            _context = context;
        }

        public class CartItemViewModel
        {
            public int ProductId { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string ImagePath { get; set; }
            public int Quantity { get; set; }

            public ProductPriceDiscount ProductPriceDiscount { get; set; }

            public ProductQuantityInPack ProductQuantityInPack { get; set; }
        }
        public class Receipt
        {
            public List<string> Items { get; set; }
        }


        public ActionResult Index()
        {
            // Get the current user's ID from the session
            var userJson = HttpContext.Session.GetString("User");
            if (userJson == null)
            {
                TempData["Message"] = "You need to log in before buying.";
                return RedirectToAction("Login", "Users");
            }

            var user = JsonConvert.DeserializeObject<User>(userJson);           
            var clientId = user.Id;



            // Get the user's cart
            var userCart = _context.UserCarts
    .Where(uc => uc.ClientId == clientId)
    .OrderByDescending(uc => uc.Id)
    .FirstOrDefault();

            if (userCart == null)
            {
                return NotFound();
            }


            var cartItems = _context.ProductInCarts
       .Where(pic => pic.CartId == userCart.Id)
       .Select(pic => new CartItemViewModel
       {
           ProductId = pic.Product.Id,
           Name = pic.Product.Name,
           Price = pic.Product.ProductPriceDiscount.Price,
           ImagePath = pic.Product.ProductImages.First().ImagePath,
           Quantity = pic.Quantity,
           ProductPriceDiscount = pic.Product.ProductPriceDiscount,
           ProductQuantityInPack = pic.Product.ProductQuantityInPack
       })
       .ToList();

            return View(cartItems);
        }

        [HttpPost]
        public ActionResult Index(decimal totalSum)
        {
            var userJson = HttpContext.Session.GetString("User");
            if (userJson == null)
            {
                TempData["Message"] = "You need to log in before buying.";
                return RedirectToAction("Login", "Users");
            }

            var user = JsonConvert.DeserializeObject<User>(userJson);
            var clientId = user.Id;



            // Get the user's cart
            var userCart = _context.UserCarts
     .Where(uc => uc.ClientId == clientId)
     .OrderByDescending(uc => uc.Id)
     .FirstOrDefault();

            if (userCart == null)
            {
                return NotFound();
            }

            userCart.TotalPrice = totalSum;
            _context.SaveChanges();
            return RedirectToAction("Index", "Order");
        }


        [HttpGet]
        public ActionResult Buy(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            // Get the current user's ID from the session
            var userJson = HttpContext.Session.GetString("User");
            if (userJson == null)
            {
                TempData["Message"] = "You need to log in before buying.";
                return RedirectToAction("Login", "Users");
            }

            var user = JsonConvert.DeserializeObject<User>(userJson);
            var clientId = user.Id;
            var client = _context.Clients.FirstOrDefault(c => c.UserId == user.Id);

            // Find the user's cart, or create a new one if it doesn't exist
            var userCart = _context.UserCarts
     .Where(uc => uc.ClientId == clientId)
     .OrderByDescending(uc => uc.Id)
     .FirstOrDefault();

            if (userCart == null)
            {
                userCart = new UserCart
                {
                    ClientId = clientId,
                    Date = DateTime.Now,
                    TotalPrice = 0
                };
                _context.UserCarts.Add(userCart);
                _context.SaveChanges();
            }

            // Add the product to the cart
            var productInCart = _context.ProductInCarts
                 .Include(pic => pic.Product)
                 .FirstOrDefault(pic => pic.CartId == userCart.Id && pic.ProductId == product.Id);

            if (productInCart == null)
            {
                // Add the product to the cart
                productInCart = new ProductInCart
                {
                    CartId = userCart.Id,
                    ProductId = product.Id,
                    Quantity = 1
                };
                _context.ProductInCarts.Add(productInCart);
            }
            else
            {
                TempData["Message"] = "Цей продукт вже є в корзині!";
                return RedirectToAction("Index", "Catalog");
            }


           
            List<string> items = new List<string> { "Флуоксетин", "Інфлювак", "Амоксіцилін" };
            string productName = productInCart.Product.Name;
            if (items.Contains(productName))
            {
                try
                {
                    if (!client.Receipts.Any(receiptJson =>
                    {
                        string receiptsString = new string(client.Receipts.ToArray());
                        var receipt = JsonConvert.DeserializeObject<Receipt>(receiptsString);
                        return receipt.Items.Any(item => item == productName);
                    }))
                    {
                        TempData["Message"] = "Для того щоб купити цей товар " + productInCart.Product.Name.ToString() + " потрібен рецепт від лікаря. Додайте його в особистому кабінеті!";
                        return RedirectToAction("Login", "Users");
                    }
                }
                catch 
                {
                    TempData["Message"] = "Для того щоб купити цей товар " + productInCart.Product.Name.ToString() + " потрібен рецепт від лікаря. Додайте його в осибистому кабінеті! ";
                    return RedirectToAction("Index", "Order");
                }
            }
           

            _context.SaveChanges();

            // Redirect to the shopping cart view
            return RedirectToAction("Index", "Order");
        }

        [HttpPost]
        public ActionResult UpdateQuantity(int id, int quantity)
        {
            // Get the current user's ID from the session
            var user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));
            var clientId = user.Id;

            var userCart = _context.UserCarts
    .Where(uc => uc.ClientId == clientId)
    .OrderByDescending(uc => uc.Id)
    .FirstOrDefault();


            // Find the product in the cart
            var productInCart = _context.ProductInCarts.FirstOrDefault(pic => pic.CartId == userCart.Id && pic.ProductId == id);
            if (productInCart == null)
            {
                return RedirectToAction("Index");
            }

            // Check the quantity in the main storage
            var productInMainStorage = _context.ProductInMainStorages.FirstOrDefault(p => p.ProductId == id);
            if (productInMainStorage == null || productInMainStorage.Quantity < quantity)
            {
                return BadRequest("Error: Quantity is greater than the quantity in the main storage.");
            }

            // Update the quantity
            productInCart.Quantity = quantity;
            _context.SaveChanges();

            // Redirect back to the shopping cart view
            return RedirectToAction("Index");
        }

       
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
           
            var user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));
            var clientId = user.Id;

            var userCart = _context.UserCarts
     .Where(uc => uc.ClientId == clientId)
     .OrderByDescending(uc => uc.Id)
     .FirstOrDefault();


            var productInCart = _context.ProductInCarts.FirstOrDefault(pic => pic.CartId == userCart.Id && pic.ProductId == id);
            if (productInCart == null)
            {
                return NotFound();
            }

         
            _context.ProductInCarts.Remove(productInCart);
            _context.SaveChanges();

          
            return RedirectToAction("Index");
        }

        public class PaymentViewModel
        {
            [Required]
            public string CreditCardNumber { get; set; }

            [Required]
            public string CVCode { get; set; }

            [Required]
            public string DeliveryAddress { get; set; }

            public decimal TotalSum { get; set; }
        }


        [HttpGet]
        public ActionResult Payment(decimal? totalSum)
        {
            var paymentViewModel = new PaymentViewModel
            {
                TotalSum = totalSum ?? 0
            };

            if (ModelState.IsValid && totalSum != 0)
            {
                return View(paymentViewModel);
            }
            else
            {
                TempData["Message"] = "Для того, щоб перейди до оплати кошик не повинен бути пустий!";
                return RedirectToAction("Index", "Order");
            }
        }

        [HttpPost]
        public ActionResult Payment(PaymentViewModel paymentModel)
        {
            var user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));
            var clientId = user.Id;

            var userCart = _context.UserCarts
        .Where(uc => uc.ClientId == clientId)
        .OrderByDescending(uc => uc.Id)
        .FirstOrDefault();

            if (userCart == null) { TempData["Success"] = "Empty cart!"; return RedirectToAction("Index", "Home"); }

            try
            {
                // Create a new order
                var order = new Order
                {
                    Date = DateTime.Now,
                    CartId = userCart.Id,
                    DeliveryAddress = paymentModel.DeliveryAddress, 
                    TotalPrice = paymentModel.TotalSum
                };

                // Copy data from ProductInCart to ProductInOrder
                var productsInCart = _context.ProductInCarts.Where(pic => pic.CartId == userCart.Id).ToList();
                foreach (var productInCart in productsInCart)
                {
                    var productInOrder = new ProductInOrder
                    {
                        CartId = productInCart.CartId,
                        OrderProductId = productInCart.ProductId,
                        Quantity = productInCart.Quantity,
                        OrderProduct = productInCart.Product
                    };

                    _context.ProductInOrders.Add(productInOrder);

                    // Decrease quantity from storage
                    var productInMainStorage = _context.ProductInMainStorages.FirstOrDefault(p => p.ProductId == productInCart.ProductId);
                    if (productInMainStorage != null)
                    {
                        productInMainStorage.Quantity -= productInCart.Quantity;
                    }

                    // Add information about payment
                    var salesMainStorage = new SalesMainStorage
                    {
                        ProductId = productInCart.ProductId,
                        NetworkId = 1, // Assuming the NetworkId is available in the User object
                        Quantity = productInCart.Quantity,
                        SaleDate = DateTime.Now,
                        TotalPrice = paymentModel.TotalSum // Assuming the price is available in the Product object
                    };

                    _context.SalesMainStorages.Add(salesMainStorage);
                }

                _context.Orders.Add(order);

                // Delete data from UserCart and ProductInCart
                userCart = new UserCart
                {
                    ClientId = clientId,
                    Date = DateTime.Now,
                    TotalPrice = 0
                };
                _context.UserCarts.Add(userCart);
                _context.ProductInCarts.RemoveRange(productsInCart);
                _context.SaveChanges();
            }
            catch (Exception ex) { return BadRequest("Error: " + ex.ToString()); }

            TempData["Success"] = "Payment successfully!";
            return RedirectToAction("Index", "Home");
        }





    }
}
