using Microsoft.AspNetCore.Mvc;

namespace Chain_pharmacies.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
