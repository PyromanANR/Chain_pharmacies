using Chain_pharmacies.Data;
using Chain_pharmacies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static Chain_pharmacies.Controllers.OrderController;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace Chain_pharmacies.Controllers
{
    public class ReservationController : Controller
    {
        private readonly NetworkOfPharmaciesContext _context;
        public ReservationController(NetworkOfPharmaciesContext context)
        {
            _context = context;
        }
        public class ProductViewModel
        {
            public string Name { get; set; }
            public string ImagePath { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }

            public int Id { get; set; }

            public List<Pharmacy> Pharmacies { get; set; }
        }

        private readonly string _mapQuestApiKey = "GUBE6KOgB02K5nwAAlKkkvT8QJZZHBim";

        [HttpGet]
        [Route("Index/{id}")]
        public async Task<IActionResult> Index(int id)
        {

            var product = _context.Products.Include(p => p.ProductImages)
                                           .Include(p => p.ProductInMainStorages)
                                           .Include(p => p.ProductPriceDiscount)
                                           .Include(p => p.ProductQuantityInPack)
                                           .FirstOrDefault(p => p.Id == id);

            var pharmacies =  _context.Pharmacies.ToList();

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


            foreach (var pharmacy in pharmacies)
            {
                var requestUrl = $"http://www.mapquestapi.com/geocoding/v1/address?key={_mapQuestApiKey}&location={pharmacy.Location}";
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetStringAsync(requestUrl);
                    var mapQuestResponse = JsonConvert.DeserializeObject<MapQuestResponse>(response);
                    var latLng = mapQuestResponse.Results[0].Locations[0].LatLng;
                    pharmacy.Latitude = double.Parse(latLng.Lat, CultureInfo.InvariantCulture);
                    pharmacy.Longitude = double.Parse(latLng.Lng, CultureInfo.InvariantCulture);
                }
            }

            // Create a new ProductViewModel and fill it with data
            var model = new ProductViewModel
            {
                Id = id,
                Name = product.Name,
                ImagePath = product.ProductImages.FirstOrDefault()?.ImagePath, // Use the first image if exists
                Quantity = product.ProductQuantityInPack?.Quantity ?? 0, // Use the quantity in the main storage if exists
                Price = product.ProductPriceDiscount?.Price ?? 0, // Use the price discount if exists
                Pharmacies = pharmacies
            };

            return View(model);
        }


        [HttpGet]
        [Route("Reserve/{id}")]

        public ActionResult Reserve(int id)
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


            // Assuming you have a List<string> of items
            List<string> items = new List<string> { "Флуоксетин", "Інфлювак", "Амоксіцилін" };
            string productName = product.Name;
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
                        TempData["Message"] = "Для того щоб купити цей товар " + productName + " потрібен рецепт від лікаря. Додайте його в особистому кабінеті!";
                        return RedirectToAction("Login", "Users");
                    }
                }
                catch
                {
                    TempData["Message"] = "Для того щоб купити цей товар " + productName + " потрібен рецепт від лікаря. Додайте його в осибистому кабінеті! ";
                    return RedirectToAction("Login", "Users");
                }
            }
            return RedirectToAction("Index", "Reservation", new { id = id });

        }

        public async Task<IActionResult> Payment(string location, int productId, int quantity)
        {
            // Find the pharmacy with the given location
            var pharmacy = _context.Pharmacies.FirstOrDefault(p => p.Location == location);

            if (pharmacy == null)
            {
                // No pharmacy found with the given location
                return NotFound();
            }

            // Now you have the pharmacy ID
            int pharmacyId = pharmacy.Id;

            // Get the product in the pharmacy storage
            var productInStorage = _context.PharmacyStorages
                .Include(ps => ps.ProductInStorages)
                .Where(ps => ps.PharmacyId == pharmacyId)
                .SelectMany(ps => ps.ProductInStorages)
                .FirstOrDefault(p => p.ProductId == productId);

            if (productInStorage == null || productInStorage.Quantity < quantity)
            {
                // Not enough quantity in stock
                return BadRequest("Not enough quantity in stock");
            }

            // Decrease the quantity in the pharmacy storage
            productInStorage.Quantity -= quantity;

            // Add information to SalesPharmacy
            var salesPharmacy = new SalesPharmacy
            {
                ProductId = productId,
                PharmacyId = pharmacyId,
                Quantity = quantity,
                SaleDate = DateTime.Now,
                TotalPrice = quantity * productInStorage.Product.ProductPriceDiscount.Price 
            };
            _context.SalesPharmacies.Add(salesPharmacy);

            // Add information to Order
            var order = new Order
            {
                Date = DateTime.Now,
                CartId = null, 
                TotalPrice = salesPharmacy.TotalPrice,
                DeliveryAddress = pharmacy.Location.ToString() 
            };
            _context.Orders.Add(order);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Order");
        }

    }
}
