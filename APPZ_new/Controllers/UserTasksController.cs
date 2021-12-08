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

        public async Task<IActionResult> StartTask()
        {
            var taskId = 3;
            var userId = 1;
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
            var userId = int.Parse(form["UserId"]);
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
        public async Task<ActionResult> ViewDetailedResult()
        {
            int userTaskId = 16;
            var userTask = _context.UserTasks
                              .Include(x => x.UserAnswers)
                              .ThenInclude(X => X.Question)
                              .FirstOrDefault(x => x.Id == userTaskId);

            //var answers = _context.Answers.Where(x => x.)

            var task = _context.Tasks.FirstOrDefault(x => x.Id == userTask.TaskId);
            return View(userTask);
        }

        //private bool UserTaskExists(int id)
        //{
        //    return _context.UserTasks.Any(e => e.Id == id);
        //}
    }
}
