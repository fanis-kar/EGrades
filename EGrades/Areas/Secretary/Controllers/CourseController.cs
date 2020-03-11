using System.Collections.Generic;
using System.Linq;
using EGrades.Data;
using EGrades.Models;
using EGrades.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EGrades.Areas.Secretary.Controllers
{
    [Area("Secretary")]
    [Authorize(Roles = "Secretary")]
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext context;

        public CourseController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // ~/Secretary/Course
        public IActionResult Index()
        {
            // Λίστα με όλα τα μαθήματα

            var courses = context.Courses
                .Include(c => c.Direction)
                .ToList();

            return View(courses);
        }

        // ~/Secretary/Course/Details/{id}
        public IActionResult Details(int id)
        {
            // Τα στοιχεία κάθε μαθήματος

            var course = context.Courses.FirstOrDefault(c => c.Id == id);

            if (course == null)
            {
                TempData["ErrorMessage"] = "Το μάθημα δε βρέθηκε.";

                return RedirectToAction("Index", "Course");
            }

            var direction = context.Directions.FirstOrDefault(d => d.Id == course.DirectionId);
            List<CourseInstructor> instructors = context
                .CourseInstructors
                .Include(item => item.Instructor)
                .Where(ci => ci.CourseId == id)
                .ToList();

            CourseDetailsViewModel coursedetailsViewModel = new CourseDetailsViewModel()
            {
                Title = "Στοιχεία μαθήματος " + course.Title,
                Course = course,
                Direction = direction,
                Instructors = instructors
            };

            return View(coursedetailsViewModel);
        }
    }
}