using Chain_pharmacies.Data;
using Chain_pharmacies.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Immutable;

namespace Chain_pharmacies.Controllers
{
    public class OrderController : Controller
    {
        private readonly NetworkOfPharmaciesContext _context; // replace with your actual DbContext

        public OrderController(NetworkOfPharmaciesContext context) // replace with your actual DbContext
        {
            _context = context;
        }

        public ActionResult Index()
        {
            // Get the current user's ID from the session
            var user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));
            var clientId = user.Id;

            // Get the user's cart
            var userCart = _context.UserCarts.FirstOrDefault(uc => uc.ClientId == clientId);
            if (userCart == null)
            {
                return NotFound();
            }

            // Get the products in the cart
            var productsInCart = _context.ProductInCarts.Where(pic => pic.CartId == userCart.Id).ToList();

            return View(productsInCart);
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
            var productInCart = new ProductInCart
            {
                CartId = userCart.Id,
                ProductId = product.Id,
                Quantity = 1
            };
            _context.ProductInCarts.Add(productInCart);
            _context.SaveChanges();

            // Redirect to the shopping cart view
            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public ActionResult UpdateQuantity(int id, int quantity)
        {
            // Get the current user's ID from the session
            var user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));
            var clientId = user.Id;

            // Find the product in the cart
            var productInCart = _context.ProductInCarts.FirstOrDefault(pic => pic.CartId == clientId && pic.ProductId == id);
            if (productInCart == null)
            {
                return NotFound();
            }

            // Update the quantity
            productInCart.Quantity = quantity;
            _context.SaveChanges();

            // Redirect back to the shopping cart view
            return RedirectToAction("Index");
        }


        public ActionResult Reserve(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

           

            return View(product);
        }
    }
}
