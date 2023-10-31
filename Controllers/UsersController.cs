using Chain_pharmacies.Data;
using Chain_pharmacies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chain_pharmacies.Controllers
{
    public class UsersController : Controller
    {
        private readonly NetworkOfPharmaciesContext _context;

        public UsersController(NetworkOfPharmaciesContext context)
        {
            _context = context;
        }

        // GET: Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Name == user.Name);
                if (existingUser == null)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Registration successful!";
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                else
                {
                    ModelState.AddModelError("Name", "Ім'я вже існує");
                }
            }
            return View(user);
        }
    }
}

