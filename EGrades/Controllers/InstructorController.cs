using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EGrades.Data;
using EGrades.Models;
using EGrades.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EGrades.Controllers
{
    [Authorize(Roles = "Student")]
    public class InstructorController : Controller
    {
        private readonly ApplicationDbContext context;

        public InstructorController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // ~/Instructor
        public IActionResult Index()
        {
            // Λίστα με όλους τους καθηγητές

            var instructors = context.Instructors.ToList();

            return View(instructors);
        }

        // ~/Instructor/Details/{id}
        public IActionResult Details(int id)
        {
            // Πληροφορίες κάποιου καθηγητή

            var instructor = context.Instructors.FirstOrDefault(i => i.Id == id);

            if (instructor == null)
            {
                TempData["ErrorMessage"] = "Ο καθηγητής δε βρέθηκε.";

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