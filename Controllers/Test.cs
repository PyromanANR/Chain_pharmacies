using Chain_pharmacies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Chain_pharmacies.Data;

namespace Chain_pharmacies.Controllers
{
    public class Test : Controller
    {
        private readonly NetworkOfPharmaciesContext _db;

        public Test(NetworkOfPharmaciesContext db)
        {
            _db = db;            
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objList = _db.Products;
            return View(objList);
        }
    }
}
