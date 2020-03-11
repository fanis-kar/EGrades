using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EGrades.Models;
using EGrades.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using EGrades.Models.ViewModels;

namespace EGrades.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        // ~/Home
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = userManager.GetUserId(HttpContext.User);

                if (User.IsInRole("Student"))
                {
                    var student_info = context.Students.Include(s => s.ApplicationUser).SingleOrDefault(u => u.UserId == userId);

                    return RedirectToAction("Profile", "Home");
                }
                else if (User.IsInRole("Secretary"))
                {
                    var secretary_info = context.Secretaries.Include(s => s.ApplicationUser).SingleOrDefault(u => u.UserId == userId);

                    return RedirectToAction("Profile", "Home", new { area = "Secretary" });
                }
            }
            else
            {
                return RedirectToPage("/Account/Login", new { Area = "Identity" });
            }

            return View();
        }

        // ~/Home/Profile
        public IActionResult Profile()
        {
            var userId = userManager.GetUserId(HttpContext.User);
            var student = context.Students.Include(s => s.ApplicationUser).SingleOrDefault(u => u.UserId == userId);
            var direction = context.Directions.Find(student.DirectionId);

            List<Enrollment> enrollments = context
                .Enrollments
                .Include(item => item.AcademicPeriod)
                .Where(ci => ci.StudentId == student.Id)
                .ToList();

            StudentDetailsViewModel studentdetailsViewModel = new StudentDetailsViewModel()
            {
                Title = "Το Προφίλ μου",
                Student = student,
                Direction = direction,
                Enrollments = enrollments
            };

            return View(studentdetailsViewModel);
        }

        // ~/Home/About
        public IActionResult About()
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
