using Chain_pharmacies.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;

namespace Chain_pharmacies.Controllers
{
    public class GeocodeData
    {
        public string Lat { get; set; }
        public string Lon { get; set; }
    }

    public class NavigationController : Controller
    {
        private readonly NetworkOfPharmaciesContext _context;

        public NavigationController(NetworkOfPharmaciesContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Map()
        {
            var pharmacies = await _context.Pharmacies.ToListAsync();

            foreach (var pharmacy in pharmacies)
            {
                var requestUrl = $"https://nominatim.openstreetmap.org/search?format=json&q={pharmacy.Location}";
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetStringAsync(requestUrl);
                    var geocodeData = JsonConvert.DeserializeObject<List<GeocodeData>>(response);
                    pharmacy.Latitude = double.Parse(geocodeData[0].Lat, CultureInfo.InvariantCulture);
                    pharmacy.Longitude = double.Parse(geocodeData[0].Lon, CultureInfo.InvariantCulture);
                }
            }

            return View(pharmacies);
        }


    }
}
