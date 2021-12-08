using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APPZ_new.Data;
using APPZ_new.Enums;
using Task = APPZ_new.Models.Task;
using APPZ_new.Models;

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
            ViewBag.DependencyId = task.Id;
            Question _quest = new Question
            {
                TaskId = task.Id
            };
            return View("CreateQuestion", _quest);
        }

        [HttpPost]
        public IActionResult CreateQuestion(APPZ_new.Models.Question obj)
        {
            obj.Task = _db.Tasks.FirstOrDefault(s => s.Id == obj.TaskId);
            obj.Id = 0;
            _db.Questions.Add(obj);
            _db.SaveChanges();

            var questions = _db.Questions.ToList().FindAll(s => s.TaskId == obj.TaskId);
            var task = _db.Tasks.FirstOrDefault(s => s.Id == obj.TaskId);
            ViewBag.TaskName = task.Title;
            return View("OpenedTask", questions);
        }

        public IActionResult OpenQuestion(int? id)
        {
            var answers = _db.Answers.ToList().FindAll(s => s.QuestionId == id);
            var question = _db.Questions.FirstOrDefault(s => s.Id == id);
            ViewBag.QuestionName = question.QuestionTest;
            ViewBag.QuestionId = question.Id;
            ViewBag.TaskId = question.TaskId;
            return View("OpenQuestion", answers);
        }

        public IActionResult ViewAnswers(int? id)
        {
            var answers = _db.Answers.ToList().FindAll(s => s.QuestionId == id);
            ViewBag.QuestionId = id;
            ViewBag.TaskId = _db.Questions.FirstOrDefault(s => s.Id == id).TaskId;
            return View("ViewAnswers", answers);
        }

        [HttpGet]
        public IActionResult CreateAnswer(int? id)
        {
            var question = _db.Questions.FirstOrDefault(s => s.Id == id);
            ViewBag.QuestionId = question.Id;
            ViewBag.QuestionName = question.QuestionTest;
            var correctAnswers = _db.Answers.ToList().FindAll(s => s.IsCorrect == true && s.QuestionId == question.Id);
            bool correctIsPossible = true;
            if(correctAnswers.Count > 0)
            {
                correctIsPossible = false;
            }
            ViewBag.CorrectNotPossible = !correctIsPossible;
            ViewBag.DeleteInfo = correctIsPossible;
            Answer answer = new Answer
            {
                QuestionId = question.Id,
                Id = 0
            };
            return View("CreateAnswer", answer);

        }

        public IActionResult DeleteAnswer(int? id)
        {
            var answer = _db.Answers.FirstOrDefault(s => s.Id == id);
            int questionId = answer.QuestionId;
            _db.Answers.Remove(answer);
            _db.SaveChanges();

            var question = _db.Questions.FirstOrDefault(s => s.Id == questionId);
            var answers = _db.Answers.ToList().FindAll(s => s.QuestionId == questionId);
            ViewBag.QuestionName = question.QuestionTest;
            ViewBag.QuestionId = question.Id;
            ViewBag.TaskId = question.TaskId;
            return View("OpenQuestion", answers);
        }
        [HttpPost]
        public IActionResult CreateAnswer(Answer obj)
        {
            obj.Id = 0;
            _db.Answers.Add(obj);
            _db.SaveChanges();

            var answers = _db.Answers.ToList().FindAll(s => s.QuestionId == obj.QuestionId);
            var question = _db.Questions.FirstOrDefault(s => s.Id == obj.QuestionId);
            ViewBag.QuestionName = question.QuestionTest;
            ViewBag.QuestionId = question.Id;
            ViewBag.TaskId = question.TaskId;
            return View("OpenQuestion", answers);
        }

        public IActionResult DeleteQuestion(int? id)
        {
            var question = _db.Questions.FirstOrDefault(s => s.Id == id);
            int taskId = question.TaskId;
            _db.Questions.Remove(question);
            _db.SaveChanges();

            ViewBag.TaskName = _db.Tasks.FirstOrDefault(s => s.Id == taskId).Title;
            ViewBag.TaskId = taskId;
            var questions = _db.Questions.ToList().FindAll(s => s.TaskId == taskId);
            return View("OpenedTask", questions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Task obj)
        {
            _db.Tasks.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteTask(int? id)
        {
            var obj = _db.Tasks.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Tasks.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Tasks.Find(id);
            ViewBag.Severity = obj.Severity;
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Tasks.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            ViewBag.Category = _db.Categorys.ToList();

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Task obj)
        {
            if (ModelState.IsValid)//то не обовязкове
            {
                _db.Tasks.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);//нахуй можна забрати
        }
    }
}

