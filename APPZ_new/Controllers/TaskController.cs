using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APPZ_new.Data;
using Task = APPZ_new.Models.Task;

namespace APPZ_new.Controllers
{
    public class TaskController : Controller
    {
        public AppDBContext _db;

        public TaskController(AppDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Models.Task> objList = _db.Tasks;
            return View(objList);
        }
        public IActionResult Create()
        {
            ViewBag.Category = _db.Categorys.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Task obj)
        {
            _db.Tasks.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
