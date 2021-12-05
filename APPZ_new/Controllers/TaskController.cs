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
            ViewBag.Questions = _db.Questions;
            return View(objList);
        }
        public IActionResult Create()
        {
            ViewBag.Category = _db.Categorys.ToList();
            return View();
        }

        [HttpGet]
        public IActionResult OpenTask(int? id)
        {
            var questions = _db.Questions.ToList().FindAll(s => s.TaskId == id);
            var task = _db.Tasks.FirstOrDefault(s => s.Id == id);
            ViewBag.TaskName = task.Title;
            ViewBag.TaskId = task.Id;
            return View("OpenedTask", questions);
        }

        [HttpGet]
        public IActionResult CreateQuestion(int? id)
        {
            var task = _db.Tasks.FirstOrDefault(s => s.Id == id);
            ViewBag.TaskName = task.Title;
            return View("CreateQuestion");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateQuestion(APPZ_new.Models.Question obj)
        {
            _db.Questions.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("CreateQuestion");
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
