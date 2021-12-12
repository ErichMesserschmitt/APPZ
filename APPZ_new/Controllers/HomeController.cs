using APPZ_new.Data;
using APPZ_new.Enums;
using APPZ_new.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace APPZ_new.Controllers
{
    public class HomeController : Controller
    {



        private readonly ILogger<HomeController> _logger;
        private readonly AppDBContext _context;

        public HomeController(
            ILogger<HomeController> logger, 
            AppDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RedirectToAria()
        {
            string currentUserName = User.Identity.Name;
            var user = _context.Users.Where(q => q.Name == currentUserName).FirstOrDefault();

            if (user.Role == UserRole.User)
            {
                return RedirectToAction("ChooseCategory", "UserTasks");
            }
            return RedirectToAction("SqlTaskList", "Task");
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
