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
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext context;

        public StudentController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // ~/Secretary/Student
        public IActionResult Index()
        {
            // Λίστα με όλους τους μαθητές

            var students = context.Students
                .Include(s => s.Direction)
                .ToList();

            return View(students);
        }

        // ~/Secretary/Student/Details/{id}
        public IActionResult Details(int id)
        {
            // Τα στοιχεία ενός φοιτητή

            var student = context.Students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                TempData["ErrorMessage"] = "Ο φοιτητής δε βρέθηκε.";

                return RedirectToAction("Index", "Student");
            }

            var direction = context.Directions.FirstOrDefault(d => d.Id == student.DirectionId);

            List<Enrollment> enrollments = context
                .Enrollments
                .Include(e => e.AcademicPeriod)
                .Where(e => e.StudentId == id)
                .ToList();

            StudentDetailsViewModel studentdetailsViewModel = new StudentDetailsViewModel()
            {
                Title = "Στοιχεία φοιτητή " + student.FullName,
                Student = student,
                Direction = direction,
                Enrollments = enrollments
            };

            return View(studentdetailsViewModel);
        }
    }
}