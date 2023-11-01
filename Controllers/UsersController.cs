using Chain_pharmacies.Data;
using Chain_pharmacies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
                    user.RoleId = 1;
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    // Створюємо новий екземпляр клієнта
                    var client = new Client
                    {
                        UserId = user.Id,
                    };
                    _context.Add(client);
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

        // GET: Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string name, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == name && u.Password == password);
            if (user != null)
            {
                TempData["Success"] = "Log in successful!";
                HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login credentials");
            }
            return View();
        }

    }
}

