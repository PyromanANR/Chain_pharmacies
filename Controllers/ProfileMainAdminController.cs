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

        public async Task<IActionResult> ViewItemsOnMainStorage()
        {
            // Get product data from the database
            var productData = await _context.ProductInMainStorages
                .Include(p => p.Product)
                .Include(p => p.Storage)
                .ToListAsync();

            return View(productData);
        }

        public async Task<IActionResult> CreateLogBySalesForMainStorage()
        {
            // Get sales data from the database
            var salesData = await _context.SalesMainStorages
                .GroupBy(s => s.ProductId)
                .Select(g => new { ProductId = g.Key, SalesCount = g.Sum(s => s.Quantity) })
                .ToListAsync();

            // Get product data from the database
            var productData = await _context.Products.ToListAsync();

            // Prepare data for the chart
            var chartData = new List<dynamic>();

            foreach (var data in salesData)
            {
                var product = productData.FirstOrDefault(p => p.Id == data.ProductId);
                if (product != null)
                {
                    chartData.Add(new
                    {
                        ProductName = product.Name,
                        SalesCount = data.SalesCount
                    });
                }
            }

            return View(chartData);
        }

    }
}
