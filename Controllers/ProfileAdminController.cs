using Chain_pharmacies.Data;
using Chain_pharmacies.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Chain_pharmacies.Controllers
{
    public class ProfileAdminController : Controller
    {
        private readonly NetworkOfPharmaciesContext _context;

        public ProfileAdminController(NetworkOfPharmaciesContext context)
        {
            _context = context;
        }

        public class AdminUserViewModel
        {
            public User User { get; set; }
            public Admin Admin { get; set; }
        }


        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetString("User");
            var user = JsonConvert.DeserializeObject<User>(userId);
            var admin = _context.Admins.FirstOrDefault(a => a.UserId == user.Id);

            if (admin == null)
            {
                return NotFound();
            }

            var model = new AdminUserViewModel
            {
                User = user,
                Admin = admin
            };

            return View(model);
        }

        public IActionResult CreateLog()
        {
            var userId = HttpContext.Session.GetString("User");
            var user = JsonConvert.DeserializeObject<User>(userId);
            var admin = _context.Admins.FirstOrDefault(a => a.UserId == user.Id);

            if (admin == null)
            {
                return NotFound();
            }

            var sales = _context.SalesPharmacies.Where(s => s.Pharmacy.AdminId == admin.Id).ToList();

            // Створюємо словник для зберігання кількості продажів для кожного продукту
            var productSales = new Dictionary<string, int>();

            foreach (var sale in sales)
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == sale.ProductId);
                if (product != null)
                {
                    if (!productSales.ContainsKey(product.Name))
                    {
                        productSales[product.Name] = 0;
                    }
                    productSales[product.Name] += sale.Quantity;
                }
            }

            // Передаємо словник у представлення
            return View(productSales);
        }

        public class ProductInStorageViewModel
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
        }

        public IActionResult ViewItems()
        {
            var userId = HttpContext.Session.GetString("User");
            var user = JsonConvert.DeserializeObject<User>(userId);
            var admin = _context.Admins.FirstOrDefault(a => a.UserId == user.Id);

            if (admin == null)
            {
                return NotFound();
            }

            var items = _context.PharmacyStorages.Where(p => p.Pharmacy.AdminId == admin.Id)
                                                 .SelectMany(p => p.ProductInStorages)
                                                 .Select(i => new ProductInStorageViewModel
                                                 {
                                                     ProductId = i.Product.Id,
                                                     ProductName = i.Product.Name,
                                                     Quantity = i.Quantity
                                                 })
                                                 .ToList();

            return View(items);
        }

        public class OrderRequestViewModel
        {
            public int SelectedProductId { get; set; }
            public int Quantity { get; set; }
            public List<Product> Products { get; set; }
        }


        public IActionResult Create()
        {
            var products = _context.Products.ToList();
            var model = new OrderRequestViewModel
            {
                Products = products,
                Quantity = 1  // За замовчуванням встановлюємо кількість на 1
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(OrderRequestViewModel model)
        {
            var userId = HttpContext.Session.GetString("User");
            var user = JsonConvert.DeserializeObject<User>(userId);
            var admin = _context.Admins.FirstOrDefault(a => a.UserId == user.Id);

            if (admin == null)
            {
                return NotFound();
            }

            var pharmacy = _context.Pharmacies.FirstOrDefault(p => p.AdminId == admin.Id);

            if (pharmacy == null)
            {
                return NotFound();
            }

            var orderRequest = new OrderRequest
            {
                ProductId = model.SelectedProductId,
                PharmacyId = pharmacy.Id,
                Quantity = model.Quantity,
                RequestDate = DateTime.Now,
                Status = "Pending"
            };

            _context.OrderRequests.Add(orderRequest);
            _context.SaveChanges();
            TempData["Success"] = "Order request successful!";
            return RedirectToAction(nameof(Index));
        }

    }
}
