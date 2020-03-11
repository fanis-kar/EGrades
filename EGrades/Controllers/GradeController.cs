using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EGrades.Data;
using EGrades.Models;
using EGrades.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EGrades.Controllers
{
    public class GradeController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public GradeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        // ~/Grade
        public IActionResult Index()
        {
            // Όλες οι βαθμολογίες του εκάστοτε χρήστη

            var userId = userManager.GetUserId(HttpContext.User);
            var student = context.Students.Include(s => s.ApplicationUser).FirstOrDefault(u => u.UserId == userId);

            var grades = context.Grades
                  .Include(i => i.Course)
                  .Include(i => i.Enrollment)
                    .ThenInclude(i => i.Student)
                  .Include(i => i.Enrollment)
                    .ThenInclude(i => i.AcademicPeriod)
                  .Include(i => i.ExamPeriod)
                  .Where(e => e.Enrollment.StudentId == student.Id)
                  .ToList();

            return View(grades);
        }

        // ~/Grade/Filter/{id}
        [Route("[controller]/[action]/{search}")]
        public IActionResult Filter(string search)
        {
            if (search == "Passed")
            {
                // Όλα τα περασμένα μαθήματα του εκάστοτε χρήστη

                var userId = userManager.GetUserId(HttpContext.User);
                var student = context.Students.Include(s => s.ApplicationUser).FirstOrDefault(u => u.UserId == userId);

                var grades = context.Grades
                      .Include(g => g.Course)
                      .Include(g => g.Enrollment)
                        .ThenInclude(g => g.Student)
                      .Include(g => g.Enrollment)
                        .ThenInclude(g => g.AcademicPeriod)
                      .Include(g => g.ExamPeriod)
                      .Where(g => g.Enrollment.StudentId == student.Id)
                      .Where(g => g.Value >= 5)
                      .ToList();

                return View(grades);
            }
            else if (search == "Failed")
            {
                // Οι αποτυχημένες προσπάθειες του εκάστοτε χρήστη

                var userId = userManager.GetUserId(HttpContext.User);
                var student = context.Students.Include(s => s.ApplicationUser).FirstOrDefault(u => u.UserId == userId);

                var grades = context.Grades
                      .Include(g => g.Course)
                      .Include(g => g.Enrollment)
                        .ThenInclude(g => g.Student)
                      .Include(g => g.Enrollment)
                        .ThenInclude(g => g.AcademicPeriod)
                      .Include(g => g.ExamPeriod)
                      .Where(g => g.Enrollment.StudentId == student.Id)
                      .Where(g => g.Value < 5)
                      .ToList();

                return View(grades);
            }

            return RedirectToAction("Index");
        }

        //-------------------------------------------------------------------------------//

        public int PassedDirectionCourses(int studentId)
        {
            var student = context.Students.FirstOrDefault(s => s.Id == studentId);

            if (student != null)
            {
                return context.Grades
                      .Include(g => g.Course)
                         .ThenInclude(g => g.Direction)
                      .Include(g => g.Enrollment)
                        .ThenInclude(g => g.Student)
                      .Where(g => g.Enrollment.StudentId == student.Id)
                      .Where(g => g.Course.DirectionId == student.DirectionId)
                      .Where(g => g.Value >= 5)
                      .Count();
            }

            return 0;
        }

        public int PassedNonDirectionCourses(int studentId)
        {
            var student = context.Students.FirstOrDefault(s => s.Id == studentId);

            if (student != null)
            {
                return context.Grades
                      .Include(g => g.Course)
                         .ThenInclude(g => g.Direction)
                      .Include(g => g.Enrollment)
                        .ThenInclude(g => g.Student)
                      .Where(g => g.Enrollment.StudentId == student.Id)
                      .Where(g => g.Course.DirectionId != student.DirectionId)
                      .Where(g => g.Value >= 5)
                      .Count();
            }

            return 0;
        }

        public bool DegreeCondition(int studentId)
        {
            var student = context.Students.FirstOrDefault(s => s.Id == studentId);

            if (student == null)
            {
                return false;
            }

            int numberPassedDirectionCourses = PassedDirectionCourses(studentId);

            if(numberPassedDirectionCourses >= 8)
            {
                int numberPassedNonDirectionCourses = PassedNonDirectionCourses(studentId);

                if (numberPassedDirectionCourses + numberPassedNonDirectionCourses >= 10)
                {
                    return true;
                }
            }

            return false;
        }

        //-------------------------------------------------------------------------------//

        // ~/Grade/Degree
        public IActionResult Degree()
        {
            var userId = userManager.GetUserId(HttpContext.User);
            var student = context.Students.Include(s => s.ApplicationUser).FirstOrDefault(u => u.UserId == userId);
            string degreecond = "Όχι";

            if(DegreeCondition(student.Id))
            {
                degreecond = "Ναι";
            }

            DegreeViewModel degreeViewModel = new DegreeViewModel()
            {
                Student = student,
                PassedDirectionCourses = PassedDirectionCourses(student.Id),
                PassedNonDirectionCourses = PassedNonDirectionCourses(student.Id),
                Degree = degreecond
            };

            return View(degreeViewModel);
        }
    }
}