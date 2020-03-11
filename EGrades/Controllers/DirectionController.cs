using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EGrades.Data;
using EGrades.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EGrades.Controllers
{
    [Authorize(Roles = "Student")]
    public class DirectionController : Controller
    {
        private readonly ApplicationDbContext context;

        public DirectionController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // ~/Direction
        public IActionResult Index()
        {
            // Λίστα με όλες τις κατευθύνσεις

            var directions = context.Directions.ToList();

            return View(directions);
        }

        // ~/Direction/Courses/{id}
        public IActionResult Courses(int id)
        {
            var direction = context.Directions.FirstOrDefault(d => d.Id == id);

            if (direction == null)
            {
                TempData["ErrorMessage"] = "Η κατεύθυνση δε βρέθηκε.";

                return RedirectToAction("Index", "Direction");
            }

            var courses = context
                .Courses
                .Include(item => item.Direction)
                .Where(c => c.DirectionId == id)
                .ToList();

            DirectionCoursesViewModel directioncoursesViewModel = new DirectionCoursesViewModel()
            {
                Title = "Λίστα μαθημάτων για " + direction.Name,
                Courses = courses,
                Direction = direction
            };

            return View(directioncoursesViewModel);
        }
    }
}