using System;
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
    public class AcademicPeriodController : Controller
    {
        private readonly ApplicationDbContext context;

        public AcademicPeriodController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // ~/Secretary/AcademicPeriod
        public IActionResult Index()
        {
            // Λίστα με όλες τις Ακ. Περιόδους

            var academicperiods = context.AcademicPeriods.ToList();

            return View(academicperiods);
        }

        // ~/Secretary/AcademicPeriod/Enrollments/{id}
        public IActionResult Enrollments(int id)
        {
            // Λίστα με όλες τις Δηλώσεις ανα Ακ. Περίοδο

            var academicperiod = context.AcademicPeriods.FirstOrDefault(ap => ap.Id == id);

            if (academicperiod == null)
            {
                TempData["ErrorMessage"] = "Η Ακ. Περίοδος δε βρέθηκε.";

                return RedirectToAction("Index", "AcademicPeriod");
            }

            var enrollments = context
                .Enrollments
                .Include(item => item.Student)
                .Where(e => e.AcademicPeriodId == id)
                .ToList();

            AcademicPeriodEnrollmentsViewModel academicperiodenrollmentsViewModel = new AcademicPeriodEnrollmentsViewModel()
            {
                Title = "Λίστα δηλώσεων για " + academicperiod.Period,
                AcademicPeriod = academicperiod,
                Students = context.Enrollments.Where(e => e.AcademicPeriodId == id).Select(e => e.Student).ToList(),
                Enrollments = enrollments
            };

            return View(academicperiodenrollmentsViewModel);
        }

        // ~/Secretary/AcademicPeriod/Create
        [HttpGet]
        public ActionResult Create()
        {
            // Δημιουργία νέας Ακ. Περιόδου

            AcademicPeriodFormViewModel academicperiodformViewModel = new AcademicPeriodFormViewModel();

            return View("AcademicPeriodForm", academicperiodformViewModel);
        }

        // ~/Secretary/AcademicPeriod/Edit/{id}
        [HttpGet]
        public ActionResult Edit(int id)
        {
            // Ενημέρωση στοιχείων Ακ. Περιόδου

            var academicperiod = context.AcademicPeriods.FirstOrDefault(ap => ap.Id == id);

            if (academicperiod == null)
            {
                TempData["ErrorMessage"] = "Η Ακ. Περίοδος δε βρέθηκε.";

                return RedirectToAction("Index", "AcademicPeriod");
            }

            AcademicPeriodFormViewModel academicperiodformViewModel = new AcademicPeriodFormViewModel(academicperiod);

            return View("AcademicPeriodForm", academicperiodformViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(AcademicPeriod ap)
        {
            if (!ModelState.IsValid)
            {
                AcademicPeriodFormViewModel academicperiodformViewModel = new AcademicPeriodFormViewModel(ap);

                return View("AcademicPeriodForm", academicperiodformViewModel);
            }

            if (ap.Current)
            {
                var allacademicperiodsInDb = context.AcademicPeriods.Where(p => p.Current == true).ToList();
                allacademicperiodsInDb.ForEach(p => { p.Current = false; });
            }

            if (ap.OpenEnrollment)
            {
                var allacademicperiodsInDb = context.AcademicPeriods.Where(p => p.OpenEnrollment == true).ToList();
                allacademicperiodsInDb.ForEach(p => { p.OpenEnrollment = false; });
            }

            if (ap.Id == 0)
            {
                context.AcademicPeriods.Add(ap);
            }
            else
            {
                var academicperiodInDb = context.AcademicPeriods.Single(p => p.Id == ap.Id);
                academicperiodInDb.Year = ap.Year;
                academicperiodInDb.Semester = ap.Semester;
                academicperiodInDb.StartDate = ap.StartDate;
                academicperiodInDb.EndDate = ap.EndDate;
                academicperiodInDb.Current = ap.Current;
                academicperiodInDb.OpenEnrollment = ap.OpenEnrollment;
            }

            try
            {
                context.SaveChanges();

                TempData["SuccessMessage"] = "Η διαδικασία ολοκληρώθηκε.";
            }
            catch(Exception e)
            {
                TempData["ErrorMessage"] = "Κάτι πήγε στραβά: " + e.Message + " " + e.InnerException + " . Παρακαλώ δοκιμάστε ξανά αργότερα.";
            }

            return RedirectToAction("Index", "AcademicPeriod");
        }
    }
}