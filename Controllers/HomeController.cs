using Chain_pharmacies.Data;
using Chain_pharmacies.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using iTextSharp.text.pdf;
using iTextSharp.text;


namespace Chain_pharmacies.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NetworkOfPharmaciesContext _context;

        public HomeController(ILogger<HomeController> logger, NetworkOfPharmaciesContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Sequential Query
            var sequentialQueryStartTime = Stopwatch.StartNew();
            var sequentialData = await _context.Table2s.Take(100000).ToListAsync();
            sequentialQueryStartTime.Stop();
            ViewBag.SequentialQueryTime = sequentialQueryStartTime.ElapsedMilliseconds;

            // Parallel Query using Task.Run
            var parallelQueryStartTime = Stopwatch.StartNew();
            var parallelData = await Task.Run(() => _context.Table2s.Take(100000).ToList());
            parallelQueryStartTime.Stop();
            ViewBag.ParallelQueryTime = parallelQueryStartTime.ElapsedMilliseconds;

            // Create PDF
            var pdfPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pdf", "report.pdf");
            CreatePdf(pdfPath, ViewBag.SequentialQueryTime, ViewBag.ParallelQueryTime);

            return View();
        }

        private void CreatePdf(string filePath, long sequentialQueryTime, long parallelQueryTime)
        {
            // Create a new PDF document
            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

            // Open the document
            document.Open();

            // Add a title to the PDF
            document.Add(new Paragraph("Query Performance Report"));

            // Add query times to the PDF
            document.Add(new Paragraph($"async Sequential Query Time: {sequentialQueryTime} ms"));
            document.Add(new Paragraph($"multiCore Query Time: {parallelQueryTime} ms"));

            // Close the document
            document.Close();
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}