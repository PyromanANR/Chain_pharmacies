using Chain_pharmacies.Data;
using Chain_pharmacies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace Chain_pharmacies.Controllers
{
    public class ProfileMainAdminController : Controller
    {
        private readonly NetworkOfPharmaciesContext _context;

        public ProfileMainAdminController(NetworkOfPharmaciesContext context)
        {
            _context = context;
        }

        public class MainAdminViewModel
        {
            public User User { get; set; }
            public MainAdmin MainAdmin { get; set; }
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetString("User");
            var user = JsonConvert.DeserializeObject<User>(userId);
            var mainAdmin = _context.MainAdmins.FirstOrDefault(a => a.AdminId == user.Id);

            if (mainAdmin == null)
            {
                return NotFound();
            }

            var model = new MainAdminViewModel
            {
                User = user,
                MainAdmin = mainAdmin
            };

            return View(model);
        }

        public async Task<IActionResult> CreateLog()
        {
            var salesData = await _context.SalesPharmacies
        .GroupBy(s => new { s.PharmacyId, s.ProductId, SaleDate = s.SaleDate.Date }) // Use the Date property of SaleDate
        .Select(g => new
        {
            g.Key.PharmacyId,
            g.Key.ProductId,
            g.Key.SaleDate,
            SalesCount = g.Sum(s => s.Quantity),
            TotalPrice = g.Sum(s => s.TotalPrice)
        })
        .ToListAsync();

            // Get pharmacy and product data from the database
            var pharmacyData = await _context.Pharmacies.ToListAsync();
            var productData = await _context.Products.ToListAsync();

            // Prepare data for the chart
            var chartData = new List<dynamic>();
            var profitData = new List<dynamic>();

            foreach (var data in salesData)
            {
                var pharmacy = pharmacyData.FirstOrDefault(p => p.Id == data.PharmacyId);
                var product = productData.FirstOrDefault(p => p.Id == data.ProductId);
                if (pharmacy != null && product != null)
                {
                    chartData.Add(new
                    {
                        PharmacyLocation = pharmacy.Location,
                        ProductName = product.Name,
                        SaleDate = data.SaleDate,
                        SalesCount = data.SalesCount
                    });

                    var existingProfitData = profitData.FirstOrDefault(p => p.PharmacyLocation == pharmacy.Location);
                    if (existingProfitData != null)
                    {
                        var index = profitData.IndexOf(existingProfitData);
                        profitData[index] = new
                        {
                            PharmacyLocation = existingProfitData.PharmacyLocation,
                            TotalProfit = existingProfitData.TotalProfit + data.TotalPrice
                        };
                    }
                    else
                    {
                        profitData.Add(new
                        {
                            PharmacyLocation = pharmacy.Location,
                            TotalProfit = data.TotalPrice
                        });
                    }
                }
            }

            var chartDataJson = JsonConvert.SerializeObject(chartData);
            var profitDataJson = JsonConvert.SerializeObject(profitData);

            return View(new { ChartData = chartDataJson, ProfitData = profitDataJson });
        }







        public IActionResult ViewOrderRequests()
        {
            return View();
            // Ваш код для перегляду запитів на замовлення
        }

        public IActionResult ViewItemsOnMainStorage()
        {
            return View();
            // Ваш код для перегляду товарів на основному складі
        }

        public IActionResult CreateLogBySalesForMainStorage()
        {
            return View();
            // Ваш код для створення журналу продажів для основного складу
        }
    }
}
