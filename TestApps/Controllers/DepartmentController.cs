using CodeFirstDB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApps.Controllers
{
    public class DepartmentController : Controller
    {
        DatabaseContext _db;
        public DepartmentController(DatabaseContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var data = _db.Departments.ToList();
            return View(data);
        }


        
        public IActionResult Create()
        {
           
            ViewBag.Data = _db.Departments.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department model)
        {
            ModelState.Remove("Deptid");
            if(ModelState.IsValid)
            {

                _db.Departments.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }

            ViewBag["data"] = _db.Departments.ToList();
            return View();
        }



        public IActionResult Edit(int id)
        {

            ViewBag.Data = _db.Departments.ToList();
            Department model = _db.Departments.Find(id);
            return View("Create",model);
        }

        [HttpPost]
        public IActionResult Edit(Department model)
        {
            if (ModelState.IsValid)
            {

                _db.Departments.Update(model);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }

            ViewBag["data"] = _db.Departments.ToList();
            return View("Create",model);
        }


        public IActionResult Delete(int id)
        {
            Department model = _db.Departments.Find(id);
            if (model != null) 
            {
                _db.Departments.Remove(model);
                _db.SaveChanges();
            
            }

            return RedirectToAction("Index");
        }
    }
}
