using CodeFirstDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using TestApps.ViewModel;

namespace TestApps.Controllers
{
    public class EmployeeController : Controller
    {
        DatabaseContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        public EmployeeController(DatabaseContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            var data = _db.Employees.ToList();
            return View(data);
        }

        public IActionResult Create()
        {

            ViewBag.Dept = _db.Departments.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel model)
        {
            ModelState.Remove("EmpID");

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

            //return RedirectToAction("Index");


            //if (ModelState.IsValid) 
            //{
            //    _db.Employees.Add(model);
            //    _db.SaveChanges();
            //    return RedirectToAction("Index");
            //}



            ViewBag.Dept = _db.Departments.ToList();
            return View();
        }



        public IActionResult Edit(int id)
        {
            ViewBag.Dept = _db.Departments.ToList();
            Employee model = _db.Employees.Find(id);
            
            return View("Create",model);
        }
        [HttpPost]
        public IActionResult Edit(Employee model)
        {
            if (ModelState.IsValid) 
            {
                _db.Employees.Update(model);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }

            ViewBag.Dept = _db.Departments.ToList();
            return View("Create",model);
        }

        public IActionResult Delete(int id)
        {

            ViewBag.data = _db.Employees.ToList();
            Employee model = _db.Employees.Find(id);
            if (model != null) 
            {
                _db.Employees.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("index");
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
    }
}
