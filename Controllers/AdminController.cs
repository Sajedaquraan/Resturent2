using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resturent2.Models;

namespace Resturent2.Controllers
{
    public class AdminController : Controller
    {

        private readonly ModelContext _context;

        public AdminController(ModelContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {

            ViewBag.name = HttpContext.Session.GetString("AdminName");
            var id = HttpContext.Session.GetInt32("AdminID");
            var users = _context.Rcustomers.Where(x => x.Id == id).SingleOrDefault();
            ViewBag.Lname = users.Lname;

            ViewBag.numberOfProducts = _context.Rproducts.Count();
            ViewBag.TotalSales = _context.Rproducts.Sum(x => x.Sale);

            //DataDictionary
            ViewData["Customers"] = _context.Rcustomers.Count();
            ViewData["MaxPrice"] = _context.Rproducts.Max(x => x.Price);


            //object
            ViewBag.Products = _context.Rproducts.ToList();

            //ViewData["Products"] = _context.Rproducts.ToList();

            ViewData["Categories"] = _context.Rcategories.ToList();

            ViewBag.Customer = _context.Rcustomers.ToList();



            //return View();
            return View(users);
        }
        public IActionResult Index2()
        {


            ViewBag.name = HttpContext.Session.GetString("AdminName");
            var id = HttpContext.Session.GetInt32("AdminID");
            var users = _context.Rcustomers.Where(x => x.Id == id).SingleOrDefault();
            ViewBag.Lname = users.Lname;

            ViewBag.numberOfProducts = _context.Rproducts.Count();
            ViewBag.TotalSales = _context.Rproducts.Sum(x => x.Sale);

            //DataDictionary
            ViewData["Customers"] = _context.Rcustomers.Count();
            ViewData["MaxPrice"] = _context.Rproducts.Max(x => x.Price);


            //object
            ViewBag.Products = _context.Rproducts.ToList();

            //ViewData["Products"] = _context.Rproducts.ToList();

            ViewData["Categories"] = _context.Rcategories.ToList();

            ViewBag.Customer = _context.Rcustomers.ToList();



            var Products = _context.Rproducts.ToList();
            var Customers = _context.Rcustomers.ToList();
            var Categories = _context.Rcategories.ToList();

            var finalResult = Tuple.Create<IEnumerable<Rproduct>, IEnumerable<Rcustomer>, IEnumerable<Rcategory>>
                (Products, Customers, Categories);


            return View(finalResult);
        }


        public IActionResult JoinTables()
        {
            var Products = _context.Rproducts.ToList();
            var Customers = _context.Rcustomers.ToList();
            var Categories = _context.Rcategories.ToList();
            var ProductsCustomers = _context.Rproductcustomers.ToList();

            var result = from c in Customers
                         join pc in ProductsCustomers on c.Id equals pc.CustomerId
                         join p in Products on pc.ProductId equals p.Id

                         join cat in Categories on p.CategoryId equals cat.Id

                         select new JoinTables
                         {
                             Products = p,
                             Customers = c,
                             Categories = cat,
                             ProductsCustomers = pc
                         };


            return View(result);
        }

        [HttpGet]
        public IActionResult Search()
        {
            var modelContext = _context.Rproductcustomers.Include(p => p.Customer).Include(p => p.Product);

            return View(modelContext);
        }

        [HttpPost]
        public IActionResult Search(DateTime? startDate, DateTime? endDate)
        {
            var modelContext = _context.Rproductcustomers.Include(p => p.Customer).Include(p => p.Product).ToList();

            if (startDate == null && endDate == null)
            {
                ViewBag.TotalPrice = modelContext.Sum(x => x.Product.Price * x.Quantity);
                return View(modelContext);
            }
            else if (startDate != null && endDate == null) //1/4/2024 
            {
                modelContext = modelContext.Where(x => x.DateFrom.Value.Date >= startDate).ToList();
                ViewBag.TotalPrice = modelContext.Sum(x => x.Product.Price * x.Quantity);
                return View(modelContext);
            }

            else if (startDate == null && endDate != null) //1/5/2024
            {
                modelContext = modelContext.Where(x => x.DateFrom.Value.Date <= endDate).ToList();
                ViewBag.TotalPrice = modelContext.Sum(x => x.Product.Price * x.Quantity);
                return View(modelContext);
            }
            else // 1/4/2024 - 1/5/2024 
            {
                modelContext = modelContext.Where(x => x.DateFrom.Value.Date >= startDate &&
                x.DateFrom.Value.Date <= endDate
                ).ToList();
                ViewBag.TotalPrice = modelContext.Sum(x => x.Product.Price * x.Quantity);
                return View(modelContext);

            }
        }


        /*

        [HttpGet]
        public IActionResult Report()
        {
            var Products = _context.Rproducts.ToList();
            var Customers = _context.Rcustomers.ToList();
            var Categories = _context.Rcategories.ToList();
            var ProductsCustomers = _context.Rproductcustomers.ToList();

            var result = from c in Customers
                         join pc in ProductsCustomers on c.Id equals pc.CustomerId
                         join p in Products on pc.ProductId equals p.Id

                         join cat in Categories on p.CategoryId equals cat.Id

                         select new JoinTables
                         {
                             Products = p,
                             Customers = c,
                             Categories = cat,
                             ProductsCustomers = pc
                         };

            var modelContext = _context.Rproductcustomers.Include(p => p.Customer).Include(p => p.Product).ToList();

            ViewBag.TotalPrice = modelContext.Sum(x => x.Product.Price * x.Quantity);

            var model3 = Tuple.Create<IEnumerable<JoinTables>, IEnumerable<Rproductcustomer>>(result, modelContext);
            return View(model3);


        }

        
        [HttpPost]
        public async Task<IActionResult> Report(DateTime? startDate, DateTime? endDate)
        {
            var Products = _context.Rproducts.ToList();
            var Customers = _context.Rcustomers.ToList();
            var Categories = _context.Rcategories.ToList();
            var ProductsCustomers = _context.Rproductcustomers.ToList();

            var result = from c in Customers
                         join pc in ProductsCustomers on c.Id equals pc.CustomerId
                         join p in Products on pc.ProductId equals p.Id

                         join cat in Categories on p.CategoryId equals cat.Id

                         select new JoinTables
                         {
                             Products = p,
                             Customers = c,
                             Categories = cat,
                             ProductsCustomers = pc
                         };

            var modelContext = _context.Rproductcustomers.Include(p => p.Customer).Include(p => p.Product).ToList();

            ViewBag.TotalPrice = modelContext.Sum(x => x.Product.Price * x.Quantity);

            //var model3 = Tuple.Create<IEnumerable<JoinTables>, IEnumerable<Rproductcustomer>>(result, modelContext);

            if (startDate == null && endDate == null)
            {
                ViewBag.TotalPrice = modelContext.Sum(x => x.Product.Price * x.Quantity);
                var model3 = Tuple.Create<IEnumerable<JoinTables>, IEnumerable<Rproductcustomer>>(result, modelContext);
                return View(model3);

            }
            else if (startDate != null && endDate == null) //1/4/2024 
            {

                ViewBag.TotalQuantity = modelContext.Where(x => x.DateFrom.Value.Date == startDate).Sum(x => x.Quantity);
                ViewBag.TotalPrice = modelContext.Where(x => x.DateFrom.Value.Date == startDate).Sum(x => x.Product.Price * x.Quantity);
                var model3 = Tuple.Create<IEnumerable<JoinTables>, IEnumerable<Rproductcustomer>>(result, modelContext.Where(x => x.DateFrom.Value.Date == startDate));
                return View(model3);
            }

            else if (startDate == null && endDate != null) //1/5/2024
            {
                ViewBag.TotalQuantity = modelContext.Where(x => x.DateFrom.Value.Date == endDate).Sum(x => x.Quantity);
                ViewBag.TotalPrice = modelContext.Where(x => x.DateFrom.Value.Date == endDate).Sum(x => x.Product.Price * x.Quantity);
                var model3 = Tuple.Create<IEnumerable<JoinTables>, IEnumerable<Rproductcustomer>>(result, modelContext.Where(x => x.DateFrom.Value.Date == endDate));
                return View(model3);
            }
            else // 1/4/2024 - 1/5/2024 
            {

                ViewBag.TotalQuantity = modelContext.Where(x => x.DateFrom >= startDate && x.DateFrom <= endDate).Sum(x => x.Quantity);
                ViewBag.TotalPrice = modelContext.Where(x => x.DateFrom >= startDate && x.DateFrom <= endDate).Sum(x => x.Product.Price * x.Quantity);
                var model3 = Tuple.Create<IEnumerable<JoinTables>, IEnumerable<Rproductcustomer>>(result, modelContext.Where(x => x.DateFrom >= startDate && x.DateFrom <= endDate));
                return View(model3);

            }
        }
        */


        [HttpGet]
        public IActionResult Report()
        {
            var products = _context.Rproducts.ToList();
            var Customers = _context.Rcustomers.ToList();
            var ProductsCustomers = _context.Rproductcustomers.ToList();
            var Categories = _context.Rcategories.ToList();
            var result = from c in Customers
                             join pc in ProductsCustomers on c.Id equals pc.CustomerId
                             join p in products on pc.ProductId equals p.Id
                             join cat in Categories on p.CategoryId equals cat.Id
                             select new JoinTables 
                             { 
                                 Products = p, 
                                 Customers = c, 
                                 ProductsCustomers = pc, 
                                 Categories = cat 
                             };

            var modelContext = _context.Rproductcustomers.Include(p => p.Customer).Include(p => p.Product).ToList();
            ViewBag.TotalPrice = modelContext.Sum(x => x.Product.Price * x.Quantity);
            var model3 = Tuple.Create<IEnumerable<JoinTables>, IEnumerable<Rproductcustomer>>(result, modelContext);
            return View(model3);
        }

        [HttpPost]
        public async Task<IActionResult> Report(DateTime? startDate, DateTime? endDate)
        {
            var customer = _context.Rcustomers.ToList();
            var procustomers = _context.Rproductcustomers.ToList();
            var products = _context.Rproducts.ToList();
            var categoreys = _context.Rcategories.ToList();
            var multiTable = from c in customer
                             join pc in procustomers on c.Id equals pc.CustomerId
                             join p in products on pc.ProductId equals p.Id
                             join cat in categoreys on p.CategoryId equals cat.Id
                             select new JoinTables 
                             { 
                                 Products = p, 
                                 Customers = c, 
                                 ProductsCustomers = pc, 
                                 Categories = cat 
                             };

            var modelContext = _context.Rproductcustomers.Include(p => p.Customer).Include(p => p.Product);
            var model3 = Tuple.Create<IEnumerable<JoinTables>, IEnumerable<Rproductcustomer>>(multiTable, await modelContext.ToListAsync());

            if (startDate == null && endDate == null)
            {
                ViewBag.TotalPrice = modelContext.Sum(x => x.Product.Price * x.Quantity);
                return View(model3);

            }

            else if (startDate != null && endDate == null)
            {
                ViewBag.TotalPrice = modelContext.Where(x => x.DateFrom.Value.Date == startDate).Sum(x => x.Product.Price * x.Quantity);
                model3 = Tuple.Create<IEnumerable<JoinTables>, IEnumerable<Rproductcustomer>>(multiTable, await modelContext.Where
                    (x => x.DateFrom.Value.Date >= startDate).ToListAsync());
                return View(model3);
            }


            else if (startDate == null && endDate != null)
            {
                ViewBag.TotalPrice = modelContext.Where(x => x.DateFrom.Value.Date == endDate).Sum(x => x.Product.Price * x.Quantity);
                model3 = Tuple.Create<IEnumerable<JoinTables>, IEnumerable<Rproductcustomer>>(multiTable, await modelContext.Where
                    (x => x.DateFrom.Value.Date <= endDate).ToListAsync());
                return View(model3);
            }

            else
            {
                ViewBag.TotalPrice = modelContext.Where(x => x.DateFrom >= startDate && x.DateFrom <= endDate).Sum
                    (x => x.Product.Price * x.Quantity);
                model3 = Tuple.Create<IEnumerable<JoinTables>, IEnumerable<Rproductcustomer>>(multiTable, await modelContext.Where
                    (x => x.DateFrom >= startDate && x.DateFrom <= endDate).ToListAsync());
                return View(model3);
            }
        }
    }
}