using Chain_pharmacies.Data;
using Chain_pharmacies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chain_pharmacies.Controllers
{
    public class CatalogController : Controller
    {
        private readonly NetworkOfPharmaciesContext _context;

        public CatalogController(NetworkOfPharmaciesContext context)
        {
            _context = context;
        }

        public class CatalogIndexViewModel
        {
            public List<Product> Products { get; set; }
            public List<Chain_pharmacies.Models.Type> Types { get; set; }
        }

        // GET: Catalog
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.Include(p => p.Brand).Include(p => p.Type).Include(p => p.ProductImages).Include(p => p.ProductPriceDiscount).ToListAsync();
            var productTypes = await _context.Types.ToListAsync();

            var model = new CatalogIndexViewModel
            {
                Products = products,
                Types = productTypes
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int? minPrice, int? maxPrice, string type)
        {
            IQueryable<Product> products = _context.Products.Include(p => p.Brand).Include(p => p.Type).Include(p => p.ProductImages).Include(p => p.ProductPriceDiscount);

            if (minPrice.HasValue)
            {
                products = products.Where(p => p.ProductPriceDiscount.Price >= minPrice);
            }

            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.ProductPriceDiscount.Price <= maxPrice);
            }

            if (!string.IsNullOrEmpty(type))
            {
                products = products.Where(p => p.Type.Name == type);
            }

            var productTypes = await _context.Types.ToListAsync();

            var model = new CatalogIndexViewModel
            {
                Products = await products.ToListAsync(),
                Types = productTypes
            };

            return View(model);
        }

        // GET: Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(p => p.Brand).Include(p => p.Type)
                .Include(p => p.ProductImages).Include(p => p.ProductPriceDiscount).Include(p => p.ProductQuantityInPack).FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

    }

}
