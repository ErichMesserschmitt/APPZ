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

            if(task == null)
            {
                //back to task list
            }

            var isAlteadyCompleted = _context.UserTasks.Any(x => x.TaskId == taskId && x.UserId == userId);
            if (isAlteadyCompleted)
            {
                //back to task list
            }

            var dto = new DoTaskModel { UserId = userId, TaskId = taskId, TaskTitle = task.Title, Questions = task.Questions };

            return View(dto);
        }

        public async Task<ActionResult> CompleteTask(IFormCollection form)
        {
            var test = form;
            var userId = int.Parse(form["UserId"]);
            var taskId = int.Parse(form["TaskId"]);
            string[] answerIdsStr = form["item.Answers"];
            var answerIds = Array.ConvertAll(answerIdsStr, id => int.Parse(id));

            //save results to db calculate mark and some it
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

        public async Task<ActionResult> ViewResult(ResultDTO resultDto)
        {
            return View(resultDto);
        }


        // GET: UserTasks/Create
        public IActionResult Create()
        {
            ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "Title");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: UserTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,TaskId,Mark,Status")] UserTask userTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "Title", userTask.TaskId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userTask.UserId);
            return View(userTask);
        }

 

        // POST: UserTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,TaskId,Mark,Status")] UserTask userTask)
        {
            if (id != userTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserTaskExists(userTask.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "Title", userTask.TaskId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userTask.UserId);
            return View(userTask);
        }


        private bool UserTaskExists(int id)
        {
            return _context.UserTasks.Any(e => e.Id == id);
        }
    }
}
