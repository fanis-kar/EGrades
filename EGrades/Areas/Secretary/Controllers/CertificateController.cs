using System;
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
    public class CertificateController : Controller
    {
        private readonly ApplicationDbContext context;

        public CertificateController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // ~/Secretary/Certificate
        public IActionResult Index()
        {
            // Λίστα με όλα τα αιτήματα

            var certificates = context.StudentCertificates
                .Include(sc => sc.Student)
                .Include(sc => sc.Certificate)
                .ToList();

            return View(certificates);
        }

        // ~/Secretary/Certificate/Edit/{id}
        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Διεκπεραίωση αιτήματος

            var certificate = context.StudentCertificates
                .Include(sc => sc.Student)
                .Include(sc => sc.Certificate)
                .FirstOrDefault(c => c.Id == id);

            if (certificate == null)
            {
                TempData["ErrorMessage"] = "Το αίτημα δε βρέθηκε.";

                return RedirectToAction("Index", "Certificate");
            }

            ProcessingCertificateFormViewModel processingcertificateViewModel = new ProcessingCertificateFormViewModel()
            {
                Id = id,
                Title = "Διεκπεραίωση αιτήματος για πιστοποιητικό",
                Certificate = certificate.Certificate.Title,
                Student = certificate.Student.FullName,
                Status = certificate.Status,
                RequestedDate = certificate.RequestedDate
            };

            return View(processingcertificateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(ProcessingCertificateFormViewModel pc)
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                TempData["ErrorMessage"] = "Κάτι πήγε στραβά: " + message + " . Παρακαλώ δοκιμάστε ξανά αργότερα.";

                return RedirectToAction("Index", "Certificate");
            }

            var studentCertificate = context.StudentCertificates.FirstOrDefault(sc => sc.Id == pc.Id);
            studentCertificate.Status = pc.Status;

            try
            {
                context.StudentCertificates.Update(studentCertificate);
                context.SaveChanges();

                TempData["SuccessMessage"] = "Η διαδικασία ολοκληρώθηκε.";
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Κάτι πήγε στραβά: " + e.Message + " " + e.InnerException + " . Παρακαλώ δοκιμάστε ξανά αργότερα.";
            }

            return RedirectToAction("Index", "Certificate");
        }
    }
}