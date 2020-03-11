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
    public class InstructorController : Controller
    {
        private readonly ApplicationDbContext context;

        public InstructorController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // ~/Secretary/Instructor
        public IActionResult Index()
        {
            // Λίστα με όλους τους καθηγητές

            var instructors = context.Instructors.ToList();

            return View(instructors);
        }

        // ~/Secretary/Instructor/Details/{id}
        public IActionResult Details(int id)
        {
            // Τα στοιχεία ενός καθηγητή

            var instructor = context.Instructors.FirstOrDefault(i => i.Id == id);

            if (instructor == null)
            {
                TempData["ErrorMessage"] = "Ο Καθηγητής δε βρέθηκε.";

                return RedirectToAction("Index", "Instructor");
            }

            List<CourseInstructor> courses = context
                .CourseInstructors
                .Include(item => item.Course)
                .Where(ci => ci.InstructorId == id)
                .ToList();

            InstructorDetailsViewModel instructordetailsViewModel = new InstructorDetailsViewModel()
            {
                Title = "Στοιχεία καθηγητή " + instructor.FullName,
                Instructor = instructor,
                Courses = courses
            };

            return View(instructordetailsViewModel);
        }
    }
}