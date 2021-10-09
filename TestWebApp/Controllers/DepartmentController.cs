using CodeFirstDB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApp.Controllers
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

          //  var data = (from prd in _db.Departments

            //            select prd).ToList();

            return View();
        }
    }
}
