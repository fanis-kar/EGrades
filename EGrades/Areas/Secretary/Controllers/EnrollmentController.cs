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
    public class EnrollmentController : Controller
    {
        private readonly ApplicationDbContext context;

        public EnrollmentController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // ~/Secretary/Enrollment
        public IActionResult Index()
        {
            // Λίστα με όλες τις Δηλώσεις

            var enrollments = context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.AcademicPeriod)
                .ToList();

            return View(enrollments);
        }

        // ~/Secretary/Enrollment/Details/{id}
        public IActionResult Details(int id)
        {
            // Στοιχεία μίας συγκεκριμένης δήλωσης

            var enrollment = context
                .Enrollments
                .Include(item => item.Student)
                .Include(item => item.AcademicPeriod)
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (enrollment == null)
            {
                TempData["ErrorMessage"] = "Η δήλωση δε βρέθηκε.";

                return RedirectToAction("Index", "Enrollment");
            }

            var grades = context
                .Grades
                .Include(item => item.Course)
                .Include(item => item.ExamPeriod)
                .Include(item => item.Secretary)
                .Where(g => g.EnrollmentId == id)
                .ToList();

            EnrollmentDetailsViewModel enrollmentdetailsViewModel = new EnrollmentDetailsViewModel()
            {
                Title = "Δήλωση: " + enrollment.AcademicPeriod.Period + " του Φοιτητή: " + enrollment.Student.FullName,
                Enrollment = enrollment,
                Student = enrollment.Student,
                AcademicPeriod = enrollment.AcademicPeriod,
                Grades = grades
            };

            return View(enrollmentdetailsViewModel);
        }
    }
}