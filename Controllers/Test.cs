using Chain_pharmacies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Chain_pharmacies.Data;

namespace Chain_pharmacies.Controllers
{
    public class Test : Controller
    {
        private readonly NetworkOfPharmaciesContext _context;

        public Test(NetworkOfPharmaciesContext db)
        {
            _context = db;            
        }

        //GET
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.Include(p => p.Brand).Include(p => p.Type).Include(p => p.ProductImages).ToListAsync();
            return View(products);
        }
    }
}
