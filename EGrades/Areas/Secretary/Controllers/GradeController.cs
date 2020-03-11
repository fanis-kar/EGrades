using System;
using System.Collections.Generic;
using System.Linq;
using EGrades.Data;
using EGrades.Models;
using EGrades.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EGrades.Areas.Secretary.Controllers
{
    [Area("Secretary")]
    [Authorize(Roles = "Secretary")]
    public class GradeController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public GradeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        // ~/Secretary/Grade
        public IActionResult Index()
        {
            // Λίστα με όλες τις βαθμολογίες

            var grades = context.Grades
                .Include(g => g.Course)
                .Include(g => g.Enrollment)
                    .ThenInclude(e => e.Student)
                .Include(g => g.Enrollment)
                    .ThenInclude(e => e.AcademicPeriod)
                .Include(g => g.ExamPeriod)
                .Include(g => g.Secretary)
                .ToList();

            return View(grades);
        }

        // ~/Secretary/Grade/Edit/{id}
        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Υποβολή βαθμολογίας

            var grade = context.Grades
                .Include(g => g.Course)
                .Include(g => g.Enrollment)
                    .ThenInclude(e => e.Student)
                .Include(g => g.Enrollment)
                    .ThenInclude(e => e.AcademicPeriod)
                .Include(g => g.ExamPeriod)
                .Include(g => g.Secretary)
                .FirstOrDefault(g => g.Id == id);

            if (grade == null)
            {
                TempData["ErrorMessage"] = "Το μάθημα δεν δηλώθηκε τη συγκεκριμένη Ακ. Περίοδο.";

                return RedirectToAction("Index", "Grade");
            }

            var examperiods = context.ExamPeriods.ToList();
            List<SelectListItem> examperiodsSelectList = new List<SelectListItem>();

            foreach (var examperiod in examperiods)
            {
                examperiodsSelectList.Add(new SelectListItem
                {
                    Value = examperiod.Id.ToString(),
                    Text = examperiod.Period
                });
            }

            if(grade.Value != null)
            {
                GradeFormViewModel gradeformViewModel = new GradeFormViewModel()
                {
                    Id = id,
                    Title = "Υποβολή βαθμολογίας",
                    Course = grade.Course.Title,
                    Student = grade.Enrollment.Student.FullName,
                    AcademicPeriod = grade.Enrollment.AcademicPeriod.Period,
                    ExamPeriod = grade.ExamPeriod.Id,
                    ExamPeriodList = examperiodsSelectList,
                    ExamDate = grade.ExamDate,
                    Grade = grade.Value
                };

                return View(gradeformViewModel);
            }
            else
            {
                GradeFormViewModel gradeformViewModel = new GradeFormViewModel()
                {
                    Id = id,
                    Title = "Υποβολή βαθμολογίας",
                    Course = grade.Course.Title,
                    Student = grade.Enrollment.Student.FullName,
                    AcademicPeriod = grade.Enrollment.AcademicPeriod.Period,
                    ExamPeriodList = examperiodsSelectList
                };

                return View(gradeformViewModel);
            }        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(GradeFormViewModel gf)
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                TempData["ErrorMessage"] = "Κάτι πήγε στραβά: " + message + " . Παρακαλώ δοκιμάστε ξανά αργότερα.";

                return RedirectToAction("Index", "Grade");
            }

            var userId = userManager.GetUserId(HttpContext.User);
            var secretary = context.Secretaries.Include(s => s.ApplicationUser).SingleOrDefault(u => u.UserId == userId);

            var gradeInDb = context.Grades.FirstOrDefault(g => g.Id == gf.Id);

            if (gradeInDb == null)
            {
                TempData["ErrorMessage"] = "Το μάθημα δεν δηλώθηκε τη συγκεκριμένη Ακ. Περίοδο.";

                return RedirectToAction("Index", "Grade");
            }

            gradeInDb.Value = gf.Grade;
            gradeInDb.ExamPeriodId = gf.ExamPeriod;
            gradeInDb.ExamDate = gf.ExamDate;
            gradeInDb.SecretaryId = secretary.Id;
            gradeInDb.EntryDate = DateTime.Today;

            try
            {
                context.Grades.Update(gradeInDb);
                context.SaveChanges();

                TempData["SuccessMessage"] = "Η διαδικασία ολοκληρώθηκε.";
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Κάτι πήγε στραβά: " + e.Message + " " + e.InnerException + " . Παρακαλώ δοκιμάστε ξανά αργότερα.";
            }

            return RedirectToAction("Index", "Grade");
        }

        // ~/Secretary/Grade/PieChart?courseId={id1}&examperiodId={id2}
        public IActionResult PieChart(int courseId, int examperiodId)
        {
            // Εξαγωγή στατιστικών για τις βαθμολογίες ενός μαθήματος ανά εξ. περίοδο.

            var course = context.Courses.FirstOrDefault(c => c.Id == courseId);
            var examperiod = context.ExamPeriods.FirstOrDefault(c => c.Id == examperiodId);

            if (course == null)
            {
                TempData["ErrorMessage"] = "Το μάθημα δε βρέθηκε.";

                return RedirectToAction("Index", "Grade");
            }

            if (examperiod == null)
            {
                TempData["ErrorMessage"] = "Η εξεταστική περίοδος δε βρέθηκε.";

                return RedirectToAction("Index", "Grade");
            }

            var grades = context.Grades
                .Include(g => g.Course)
                .Include(g => g.ExamPeriod)
                .Where(g => g.CourseId == course.Id)
                .Where(g => g.ExamPeriodId == examperiod.Id)
                .ToList();

            var srVM = new List<SimpleReportViewModel>
            {
                new SimpleReportViewModel
                {
                    DimensionOne = "[0 - 3.5)",
                    Quantity = grades.Where(g => g.Value >= 0).Where(g => g.Value < 3.5).Count()
                },

                new SimpleReportViewModel
                {
                    DimensionOne = "[3.5 - 5)",
                    Quantity = grades.Where(g => g.Value >= 3.5).Where(g => g.Value < 5).Count()
                },

                new SimpleReportViewModel
                {
                    DimensionOne = "[5 - 7.5)",
                    Quantity = grades.Where(g => g.Value >= 5).Where(g => g.Value < 7.5).Count()
                },

                new SimpleReportViewModel
                {
                    DimensionOne = "[7.5 - 9)",
                    Quantity = grades.Where(g => g.Value >= 7.5).Where(g => g.Value < 9).Count()
                },

                new SimpleReportViewModel
                {
                    DimensionOne = "[9 - 10]",
                    Quantity = grades.Where(g => g.Value >= 9).Where(g => g.Value <= 10).Count()
                }
            };

            GradeChartViewModel gradechartViewModel = new GradeChartViewModel()
            {
                Title = "Στατιστικά βαθμολογιών για το μάθημα: " + course.Title,
                Course = course.Title,
                ExamPeriod = examperiod.Period,
                Reports = srVM
            };

            return View(gradechartViewModel);
        }
    }
}