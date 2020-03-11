using System.Linq;
using EGrades.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EGrades.Areas.Secretary.Controllers
{
    [Area("Secretary")]
    [Authorize(Roles = "Secretary")]
    public class SecretaryController : Controller
    {
        private readonly ApplicationDbContext context;

        public SecretaryController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // ~/Secretary/Secretary
        public IActionResult Index()
        {
            // Λίστα Γραμματέων

            var secretaries = context.Secretaries.ToList();

            return View(secretaries);
        }

        public IActionResult Details(int id)
        {
            // Τα στοιχεία μία γραμματείας

            var secretary = context.Secretaries.FirstOrDefault(s => s.Id == id);

            if (secretary == null)
            {
                TempData["ErrorMessage"] = "Η Γραμματεία δε βρέθηκε.";

                return RedirectToAction("Index", "Secretary");
            }

            return View(secretary);
        }
    }
}