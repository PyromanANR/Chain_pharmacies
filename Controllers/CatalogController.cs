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


        // GET: Catalog
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.Include(p => p.Brand).Include(p => p.Type).Include(p => p.ProductImages).Include(p => p.ProductPriceDiscount).ToListAsync();
            return View(products);
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
