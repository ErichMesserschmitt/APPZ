using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using APPZ_new.Data;
using APPZ_new.Models;
using APPZ_new.DTOs;
using Microsoft.AspNetCore.Http;
using APPZ_new.Enums;
using Microsoft.CodeAnalysis.CSharp;
using TaskStatus = APPZ_new.Enums.TaskStatus;

namespace APPZ_new.Controllers
{
    //контролер виконання завдання
    public class UserTasksController : Controller
    {
        private readonly AppDBContext _context;

        public UserTasksController(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ChooseCategory()
        {
            return View();
        }

        public async Task<IActionResult> StartTask(int id)
        {
            string currentUserName = User.Identity.Name;
            IEnumerable<Models.User> user = _context.Users;
            var taskId = id;
            var userId = user.Where(q => q.Name == currentUserName).Select(p => p.Id).FirstOrDefault();
            var task = await _context.Tasks
                .Include(x => x.Questions)
                .ThenInclude(x => x.Answers)
                .FirstOrDefaultAsync(x => x.Id == taskId);

            var questionsDto = task.Questions.Select(x => new QuestionDto
            {
                Id = x.Id,
                QuestionTest = x.QuestionTest,
                Answers = x.Answers,
                TaskId = x.TaskId
            });

            var dto = new DoTaskModel { UserId = userId, TaskId = taskId, TaskTitle = task.Title, Questions = questionsDto };

            return View(dto);
        }

        public async Task<ActionResult> CompleteTask(IFormCollection form)
        {
            string currentUserName = User.Identity.Name;
            var userId = _context.Users.Where(q => q.Name == currentUserName).Select(p => p.Id).FirstOrDefault();
            var taskId = int.Parse(form["TaskId"]);
            string[] answerIdsStr = form["item.AnswerId"];
            var answerIds = Array.ConvertAll(answerIdsStr, id => int.Parse(id));

            var answers = _context.Answers.Where(x => answerIds.Contains(x.Id));

            var result = new ResultDTO
            {
                TaskId = taskId,
                TotalQuestionsCount = answers.Count(),
                CorrectAnswersCount = answers.Where(x => x.IsCorrect == true).Count()
            };
            var userTask = new UserTask
            {
                UserId = userId,
                TaskId = taskId,
                Mark = result.Mark,
                Status = result.Mark == 0 ? TaskStatus.NotPassed : TaskStatus.Passed,
                UserAnswers = answers.ToArray()
            };
            _context.UserTasks.Add(userTask);
            _context.SaveChanges();
            return RedirectToAction(nameof(ViewResult),  result);
        }

        //when user complete task
        public async Task<ActionResult> ViewResult(ResultDTO resultDto)
        {
            return View(resultDto);
        }

        //from task grid
        public async Task<ActionResult> ViewDetailedResult(int id)
        {
            int userTaskId = id;
            var userTask = _context.UserTasks
                              .Include(x => x.UserAnswers)
                              .ThenInclude(X => X.Question)
                              .FirstOrDefault(x => x.Id == userTaskId);

          

            var task = _context.Tasks.FirstOrDefault(x => x.Id == userTask.TaskId);
            return View(userTask);
        }

        public async Task<IActionResult> UsersTaskList(int? categoryId)
        {
            string currentUserName = User.Identity.Name;
            var userId = _context.Users.Where(q => q.Name == currentUserName).Select(p => p.Id).FirstOrDefault();
            IEnumerable<UserTask> userTask = _context.UserTasks.Where(p => p.UserId == userId);

            var tasks = _context.Tasks.Where(p => !userTask.Select(p => p.TaskId).Contains(p.Id)).OrderBy(x => x.Severity);

            var categoryTask = tasks.Where(p => p.CategoryId == categoryId);
            return View(categoryTask);
        }

        

        public async Task<IActionResult> UsersPassedTaskList()
        {
            string currentUserName = User.Identity.Name;
            IEnumerable<UserTask> userTask = _context.UserTasks;
            IEnumerable<Models.User> user = _context.Users;
            IEnumerable<Models.Task> tasks = _context.Tasks;


            var passedTask = tasks.Join(userTask, p => p.Id, q => q.TaskId,
                (p, q) => new
                {
                    ID = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    Severity = p.Severity,
                    UserId = q.UserId,
                    Status = q.Status,
                    CategoryId = p.CategoryId
                }).Join(user, p => p.UserId, q => q.Id, (p, q) => new
            {
                Id = p.ID,
                Title = p.Title,
                Description = p.Description,
                Severity = p.Severity,
                Status = p.Status,
                UserName = q.Name,
                CategoryId = p.CategoryId
            }).Where(p=> p.UserName == currentUserName).Select(p => new Models.Task
            {
                Id = p.Id,
                Title = p.Title,
                Severity = p.Severity,
                CategoryId = p.CategoryId,
                Description = p.Description
            });


            return View(passedTask);
        }
        //private bool UserTaskExists(int id)
        //{
        //    return _context.UserTasks.Any(e => e.Id == id);
        //}
    }
}
