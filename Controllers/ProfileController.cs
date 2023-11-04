using Chain_pharmacies.Data;
using Chain_pharmacies.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace Chain_pharmacies.Controllers
{
    public class ProfileController : Controller
    {
        private readonly NetworkOfPharmaciesContext _context;

        public ProfileController(NetworkOfPharmaciesContext context)
        {
            _context = context;
        }

        public class UserProfileViewModel
        {
            [Required]
            public int Id { get; set; }
            [Required]
            public string Name { get; set; }
            [Required]
            public string Email { get; set; }
            public string? PhoneNumber { get; set; }
            public string? Receipts { get; set; }

            public List<string> Items { get; set; }
        }


        public IActionResult Index()
        {
            if (TempData["Error"] != null)
            {
                ModelState.AddModelError("receiptsFile", TempData["Error"].ToString());
            }
            var userId = HttpContext.Session.GetString("User");
            var user = JsonConvert.DeserializeObject<User>(userId);

            var client = _context.Clients.FirstOrDefault(c => c.UserId == user.Id);
            var itemsList = new List<string>();

            if (client != null && client.Receipts != null)
            {
                var json = JObject.Parse(client.Receipts);
                itemsList = ((JArray)json["items"]).ToObject<List<string>>();
            } else { itemsList = null; }

            var viewModel = new UserProfileViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = client.PhoneNumber,
                Receipts = client.Receipts,
                Items = itemsList
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePhoneNumber(UserProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(model.Id);
                if (user != null)
                {
                    var client = _context.Clients.FirstOrDefault(c => c.UserId == user.Id);
                    if (client != null)
                    {
                        client.PhoneNumber = model.PhoneNumber;

                        _context.Clients.Update(client);
                        await _context.SaveChangesAsync();
                        TempData["Success"] = "Update phone successful!";
                    }

                    HttpContext.Session.Remove("User");
                    HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));
                }
            }

            return RedirectToAction("Index");
        }

        private bool IsValidJson(string json)
        {
            try
            {
                JToken.Parse(json);
                return true;
            }
            catch (JsonReaderException)
            {
                return false;
            }
        }


        [HttpPost]
        public async Task<IActionResult> UpdateReceipts(UserProfileViewModel model, IFormFile receiptsFile)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(model.Id);
                if (user != null)
                {
                    var client = _context.Clients.FirstOrDefault(c => c.UserId == user.Id);
                    if (client != null)
                    {
                        if (receiptsFile != null && receiptsFile.ContentType == "application/json")
                        {
                            try
                            {
                                using (var reader = new StreamReader(receiptsFile.OpenReadStream()))
                                {
                                    var jsonString = await reader.ReadToEndAsync();

                                    if (IsValidJson(jsonString))
                                    {
                                        var json = JObject.Parse(jsonString);

                                        if (json["items"] is JArray itemsArray)
                                        {
                                            var itemsList = itemsArray.ToObject<List<string>>();
                                            client.Receipts = jsonString;
                                            _context.Clients.Update(client);
                                            await _context.SaveChangesAsync();
                                            TempData["Success"] = "Update receipts successful!";
                                        }
                                        else
                                        {
                                            TempData["Error"] = "The JSON must contain an 'items' array.";
                                        }
                                    }
                                    else
                                    {
                                        TempData["Error"] = "The provided JSON file is not in the correct format.";
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                TempData["Error"] = "Error reading the JSON file: " + ex.Message;
                            }
                        }
                        else
                        {
                            TempData["Error"] = "Please upload a valid JSON file.";
                        }

                        HttpContext.Session.Remove("User");
                        HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));
                    }
                }
            }

            return RedirectToAction("Index");
        }





    }
}
