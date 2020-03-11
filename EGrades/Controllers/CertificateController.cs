using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class CertificateController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public CertificateController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        // ~/Certificate
        public IActionResult Index()
        {
            // Τα πιστοποιητικά του χρήστη

            var userId = userManager.GetUserId(HttpContext.User);
            var student = context.Students.Include(s => s.ApplicationUser).FirstOrDefault(u => u.UserId == userId);

            var certificates = context.StudentCertificates
                .Include(sc => sc.Student)
                .Include(sc => sc.Certificate)
                .Where(e => e.StudentId == student.Id)
                .ToList();

            return View(certificates);
        }

        // ~/Certificate/New
        [HttpGet]
        public IActionResult New()
        {
            // Αίτηση πιστοποιητικού από τον χρήστη

            var userId = userManager.GetUserId(HttpContext.User);
            var student = context.Students.Include(s => s.ApplicationUser).FirstOrDefault(u => u.UserId == userId);

            RequestCertificateFormViewModel requestcertificateViewModel = new RequestCertificateFormViewModel()
            {
                Title = "Αίτηση νέου πιστοποιητικού",
                Student = student
            };

            return View(requestcertificateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(RequestCertificateFormViewModel rc)
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                TempData["ErrorMessage"] = "Κάτι πήγε στραβά: " + message + " . Παρακαλώ δοκιμάστε ξανά αργότερα.";

                return RedirectToAction("Index", "Certificate");
            }

            Certificate certificate = context.Certificates.FirstOrDefault(c => c.Title == rc.Certificate);

            StudentCertificate studentcertificateInDb = new StudentCertificate
            {
                StudentId = rc.Student.Id,
                CertificateId = certificate.Id,
                RequestedDate = DateTime.Today
            };

            try
            {
                context.StudentCertificates.Add(studentcertificateInDb);
                context.SaveChanges();

                TempData["SuccessMessage"] = "Το αίτημα σου μεταβιβάστηκε στη Γραμματεία.";
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Κάτι πήγε στραβά: " + e.Message + " " + e.InnerException + " . Παρακαλώ δοκιμάστε ξανά αργότερα.";
            }

            return RedirectToAction("Index", "Certificate");
        }
    }
}