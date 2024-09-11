using Microsoft.AspNetCore.Mvc;
using Resturent2.Models;

namespace Resturent2.Controllers
{
    public class RegisterAndLoginController : Controller

    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _environment;


        public RegisterAndLoginController(ModelContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,Fname,Lname,ImageFile,ImagePath")] Rcustomer rcustomer, string UserName, string Passwordd)
        {
            if (ModelState.IsValid)
            {
                if (rcustomer.ImageFile != null)
                {
                    string wwwRootPath = _environment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString()
                                      + "_"
                                      + rcustomer.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);


                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await rcustomer.ImageFile.CopyToAsync(fileStream);
                    }

                    rcustomer.ImagePath = fileName;
                }

                _context.Add(rcustomer);
                await _context.SaveChangesAsync();

                Ruserlogin login = new Ruserlogin();

                login.UserName = UserName;
                login.Passwordd = Passwordd;
                login.RoleId = 3;
                login.CustomerId = rcustomer.Id;
                _context.Add(login);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            return View(rcustomer);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login([Bind("UserName,Passwordd")] Ruserlogin userlogin)
        {
            var auth = _context.Ruserlogins.Where
                (x => x.UserName == userlogin.UserName && x.Passwordd == userlogin.Passwordd).SingleOrDefault();
            if (auth != null)
            {
                switch (auth.RoleId)
                {
                    case 1:
                        HttpContext.Session.SetInt32("AdminID", (int)auth.CustomerId);
                        HttpContext.Session.SetString("AdminName", auth.UserName);
                        return RedirectToAction("Index", "Admin");
                    
                    case 3:
                        HttpContext.Session.SetInt32("CustomerID", (int)auth.CustomerId);
                        return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

    }
}