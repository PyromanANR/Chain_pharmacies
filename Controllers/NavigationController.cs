using Chain_pharmacies.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;

namespace Chain_pharmacies.Controllers
{
    public class LatLng
    {
        public string Lat { get; set; }
        public string Lng { get; set; }
    }

    public class Location
    {
        public LatLng LatLng { get; set; }
    }

    public class Result
    {
        public List<Location> Locations { get; set; }
    }

    public class MapQuestResponse
    {
        public List<Result> Results { get; set; }
    }

    public class NavigationController : Controller
    {
        private readonly NetworkOfPharmaciesContext _context;
        private readonly string _mapQuestApiKey = "GUBE6KOgB02K5nwAAlKkkvT8QJZZHBim";

        public NavigationController(NetworkOfPharmaciesContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Map()
        {
            var pharmacies = await _context.Pharmacies.ToListAsync();

            foreach (var pharmacy in pharmacies)
            {
                var requestUrl = $"http://www.mapquestapi.com/geocoding/v1/address?key={_mapQuestApiKey}&location={pharmacy.Location}";
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetStringAsync(requestUrl);
                    var mapQuestResponse = JsonConvert.DeserializeObject<MapQuestResponse>(response);
                    var latLng = mapQuestResponse.Results[0].Locations[0].LatLng;
                    pharmacy.Latitude = double.Parse(latLng.Lat, CultureInfo.InvariantCulture);
                    pharmacy.Longitude = double.Parse(latLng.Lng, CultureInfo.InvariantCulture);
                }
            }

            return View(pharmacies);
        }
    }
}

