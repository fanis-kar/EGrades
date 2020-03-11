using System;
using System.Collections.Generic;
using System.Linq;
using EGrades.Data;
using EGrades.Models;
using EGrades.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EGrades.Controllers
{
    [Authorize(Roles = "Student")]
    public class EnrollmentController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public EnrollmentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        // ~/Enrollment
        public IActionResult Index()
        {
            // Όλες οι δηλώσεις του εκάστοτε χρήστη

            var userId = userManager.GetUserId(HttpContext.User);
            var student = context.Students.Include(s => s.ApplicationUser).FirstOrDefault(u => u.UserId == userId);

            var enrollments = context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.AcademicPeriod)
                .Where(e => e.StudentId == student.Id)
                .ToList();

            return View(enrollments);
        }

        // ~/Enrollment/Details/{id}
        public IActionResult Details(int id)
        {
            // Πληροφορίες μίας δήλωσης του εκάστοτε χρήστη

            var userId = userManager.GetUserId(HttpContext.User);
            var student = context.Students.Include(s => s.ApplicationUser).FirstOrDefault(u => u.UserId == userId);

            var enrollment = context
                .Enrollments
                .Include(item => item.Student)
                .Include(item => item.AcademicPeriod)
                .Where(e => e.Id == id)
                .Where(e => e.StudentId == student.Id)
                .FirstOrDefault();

            if (enrollment == null)
            {
                TempData["ErrorMessage"] = "Η Δήλωση δε βρέθηκε!";

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
                Title = "Η Δήλωση μου για: " + enrollment.AcademicPeriod.Period,
                Enrollment = enrollment,
                Student = student,
                AcademicPeriod = enrollment.AcademicPeriod,
                Grades = grades
            };

            return View(enrollmentdetailsViewModel);
        }

        // ~/Enrollment/Create
        [HttpGet]
        public ActionResult Create()
        {
            // Δημιουργία νέας δήλωσης

            var userId = userManager.GetUserId(HttpContext.User);
            var student = context.Students.Include(s => s.ApplicationUser).FirstOrDefault(u => u.UserId == userId);

            var academicperiod = context.AcademicPeriods.Where(ap => ap.Current == true).Where(ap => ap.OpenEnrollment == true).FirstOrDefault();
            if (academicperiod == null)
            {
                TempData["ErrorMessage"] = "Δεν μπορείς να δημιουργήσεις νέα δήλωση. Η περίοδος δηλώσεων τελείωσε.";

                return RedirectToAction("Index", "Enrollment");
            }

            var enrollmentExists = context.Enrollments
                .Where(ap => ap.StudentId == student.Id)
                .Where(ap => ap.AcademicPeriodId == academicperiod.Id)
                .FirstOrDefault();

            if(enrollmentExists != null)
            {
                return RedirectToAction("Edit", "Enrollment", new { @id = enrollmentExists.Id });
            }

            var courses = context.Courses
                .Include(c => c.Direction)
                .Where(e => e.Semester == academicperiod.SemesterNum).ToList()
                .Except(from c in context.Courses
                        join g in context.Grades on c.Id equals g.CourseId
                        join e in context.Enrollments on g.EnrollmentId equals e.Id
                        where e.StudentId == student.Id
                        where g.Value >= 5
                        select c).ToList();

            var checkedcourses = new List<CheckCourseViewModel>();

            foreach (var course in courses)
            {
                checkedcourses.Add(new CheckCourseViewModel()
                {
                    Course = course,
                    Checked = false //On the Create view, no courses are selected by default
                });
            }

            EnrollmentFormViewModel enrollmentformViewModel = new EnrollmentFormViewModel
            {
                Student = student,
                AcademicPeriod = academicperiod,
                Courses = checkedcourses
            };

            return View("EnrollmentForm", enrollmentformViewModel);
        }

        // ~/Enrollment/Edit/{id}
        [HttpGet]
        public ActionResult Edit(int id)
        {
            // Ενημέρωση δήλωσης

            var en = context.Enrollments.FirstOrDefault(e => e.Id == id);
            if (en == null)
            {
                TempData["ErrorMessage"] = "Η Δήλωση δε βρέθηκε";

                return RedirectToAction("Index", "Enrollment");
            }

            var userId = userManager.GetUserId(HttpContext.User);
            var student = context.Students.Include(s => s.ApplicationUser).FirstOrDefault(u => u.UserId == userId);

            var academicperiod = context.AcademicPeriods.Where(ap => ap.Current == true).Where(ap => ap.OpenEnrollment == true).FirstOrDefault();
            if (academicperiod == null)
            {
                TempData["ErrorMessage"] = "Δεν μπορείς να ενημερώσεις αυτή τη δήλωση. Η περίοδος δηλώσεων τελείωσε.";

                return RedirectToAction("Index", "Enrollment");
            }

            var courses = context.Courses
                .Include(c => c.Direction)
                .Where(e => e.Semester == academicperiod.SemesterNum).ToList()
                .Except(from c in context.Courses
                        join g in context.Grades on c.Id equals g.CourseId
                        join e in context.Enrollments on g.EnrollmentId equals e.Id
                        where e.StudentId == student.Id
                        where g.Value >= 5
                        select c).ToList();

            var selectedcourses = (from c in context.Courses
                                 join g in context.Grades on c.Id equals g.CourseId
                                 join e in context.Enrollments on g.EnrollmentId equals e.Id
                                 where e.Id == id
                                 select c).ToList();

            var checkedcourses = new List<CheckCourseViewModel>();

            foreach (var course in courses)
            {
                checkedcourses.Add(new CheckCourseViewModel()
                {
                    Course = course,
                    Checked = selectedcourses.Where(x => x.Id == course.Id).Any()
                });
            }

            EnrollmentFormViewModel enrollmentformViewModel = new EnrollmentFormViewModel
            {
                Id = id,
                Student = student,
                AcademicPeriod = academicperiod,
                Courses = checkedcourses
            };

            return View("EnrollmentForm", enrollmentformViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(EnrollmentFormViewModel efvm)
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                TempData["ErrorMessage"] = "Κάτι πήγε στραβά: " + message + " . Παρακαλώ δοκιμάστε ξανά αργότερα.";

                return RedirectToAction("Index", "Enrollment");
            }

            if (efvm.Id == 0)
            {
                Enrollment enrollmentInDb = new Enrollment
                {
                    StudentId = efvm.Student.Id,
                    AcademicPeriodId = efvm.AcademicPeriod.Id,
                    DateAdded = DateTime.Today
                };

                try
                {
                    context.Enrollments.Add(enrollmentInDb);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                }

                var selectedCourses = efvm.Courses.Where(ef => ef.Checked).Select(ef => ef.Course.Id).ToList();

                var enrollment = context
                        .Enrollments
                        .Where(e => e.StudentId == enrollmentInDb.StudentId)
                        .Where(e => e.AcademicPeriodId == enrollmentInDb.AcademicPeriodId)
                        .Single();

                foreach (var course in selectedCourses)
                {
                    Grade grade = new Grade
                    {
                        CourseId = course,
                        EnrollmentId = enrollment.Id
                    };

                    context.Grades.Add(grade);
                }
            }
            else
            {
                var enrollment = context.Enrollments.FirstOrDefault(e => e.Id == efvm.Id);
                if (enrollment == null)
                {
                    TempData["ErrorMessage"] = "Η Δήλωση δε βρέθηκε!";

                    return RedirectToAction("Index", "Enrollment");
                }

                context.Grades.RemoveRange(context.Grades.Where(g => g.EnrollmentId == efvm.Id));
                context.SaveChanges();

                var selectedCourses = efvm.Courses.Where(ef => ef.Checked).Select(ef => ef.Course.Id).ToList();

                foreach (var course in selectedCourses)
                {
                    Grade grade = new Grade
                    {
                        CourseId = course,
                        EnrollmentId = enrollment.Id
                    };

                    context.Grades.Add(grade);
                }
            }

            try
            {
                context.SaveChanges();

                TempData["SuccessMessage"] = "Η Δήλωση στάλθηκε στη Γραμματεία.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Κάτι πήγε στραβά: " + ex.Message + " " + ex.InnerException + " . Παρακαλώ δοκιμάστε ξανά αργότερα.";
            }

            return RedirectToAction("Index", "Enrollment");
        }
    }
}