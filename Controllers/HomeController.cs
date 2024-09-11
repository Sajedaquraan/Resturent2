using Microsoft.AspNetCore.Mvc;
using Resturent2.Models;
using System.Diagnostics;

namespace Resturent2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;

        public HomeController(ILogger<HomeController> logger, ModelContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            //List of category
            var categories = _context.Rcategories.ToList();
            return View(categories);
        }

        public IActionResult GetProductByCategoryId(int id) 
        {
            var product = _context.Rproducts.Where(x=>x.CategoryId==id).ToList();
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
