using Chain_pharmacies.Data;
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
            var userCart = _context.UserCarts.FirstOrDefault(uc => uc.ClientId == clientId);
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
            var userCart = _context.UserCarts.FirstOrDefault(uc => uc.ClientId == clientId);
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
            var userCart = _context.UserCarts.FirstOrDefault(uc => uc.ClientId == clientId);
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


            // Assuming you have a List<string> of items
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

            var userCart = _context.UserCarts.FirstOrDefault(uc => uc.ClientId == clientId);

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

            var userCart = _context.UserCarts.FirstOrDefault(uc => uc.ClientId == clientId);
          
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

            if (ModelState.IsValid)
            {
                return View(paymentViewModel);
            }
            else
            {
                ViewBag.ErrorMessage = "There was a problem with your submission. Please correct the errors and try again.";
                return View(paymentViewModel);
            }
        }

        [HttpPost]
        public ActionResult Payment(PaymentViewModel paymentModel)
        {
            var user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));
            var clientId = user.Id;

            var userCart = _context.UserCarts.FirstOrDefault(uc => uc.ClientId == clientId);

            // Copy data from UserCart to OrderCart
            var orderCart = new OrderCart
            {
                Id = userCart.Id,
                ClientId = userCart.ClientId,
                Date = userCart.Date,
                TotalPrice = userCart.TotalPrice,
                User = userCart.User,
                Orders = new List<Order>()
            };

            _context.OrderCarts.Add(orderCart);

            // Copy data from ProductInCart to ProductInOrder
            var productsInCart = _context.ProductInCarts.Where(pic => pic.CartId == userCart.Id);
            foreach (var productInCart in productsInCart)
            {
                var productInOrder = new ProductInOrder
                {
                    CartId = productInCart.CartId,
                    ProductId = productInCart.ProductId,
                    Quantity = productInCart.Quantity,
                    Product = productInCart.Product
                };

                _context.ProductInOrders.Add(productInOrder);
            }

            // Delete data from UserCart and ProductInCart
            _context.ProductInCarts.RemoveRange(productsInCart);
            _context.UserCarts.Remove(userCart);

            _context.SaveChanges();


            TempData["Success"] = "Payment successfully!";
            return RedirectToAction("Index", "Home");
        }





    }
}
