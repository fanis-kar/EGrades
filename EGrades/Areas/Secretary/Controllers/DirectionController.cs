using System.Linq;
using EGrades.Data;
using EGrades.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EGrades.Areas.Secretary.Controllers
{
    [Area("Secretary")]
    [Authorize(Roles = "Secretary")]
    public class DirectionController : Controller
    {
        private readonly ApplicationDbContext context;

        public DirectionController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // ~/Secretary/Direction
        public IActionResult Index()
        {
            // Λίστα με όλες τις Κατευθύνσεις

            var directions = context.Directions.ToList();

            return View(directions);
        }

        // ~/Secretary/Direction/Courses/{id}
        public IActionResult Courses(int id)
        {
            // Τα μαθήματα μίας συγκεκριμένης κατεύθυνσης

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

        // ~/Secretary/Direction/Students/{id}
        public IActionResult Students(int id)
        {
            // Οι φοιτητές μίας συγκεκριμένης κατεύθυνσης

            var direction = context.Directions.FirstOrDefault(d => d.Id == id);

            if (direction == null)
            {
                TempData["ErrorMessage"] = "Η κατεύθυνση δε βρέθηκε.";

                return RedirectToAction("Index", "Direction");
            }

            var students = context
                .Students
                .Include(item => item.Direction)
                .Where(c => c.DirectionId == id)
                .ToList();

            DirectionStudentsViewModel directionstudentsViewModel = new DirectionStudentsViewModel()
            {
                Title = "Λίστα φοιτητών για " + direction.Name,
                Students = students,
                Direction = direction
            };

            return View(directionstudentsViewModel);
        }
    }
}