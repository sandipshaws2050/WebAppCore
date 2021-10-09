using CodeFirstDB;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestApps.Models;
using TestApps.ViewModel;

namespace TestApps.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;
        DatabaseContext _db;

        public HomeController(ILogger<HomeController> logger,DatabaseContext db , IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            var items = _db.Employees.ToList();
            return View(items);
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel model)
        {
            string FileName = UploadFile(model);
            var employee = new Employee
            {

                Name = model.Name,
                Address = model.Address,
                Deptid = model.Deptid,
                ImgPath = FileName

            };
            _db.Employees.Add(employee);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        private string UploadFile(EmployeeViewModel model)
        {
            string uniqueFileName = null;

            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
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
