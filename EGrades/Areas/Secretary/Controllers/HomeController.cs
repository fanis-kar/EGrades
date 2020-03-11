using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EGrades.Models;
using EGrades.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace EGrades.Areas.Secretary.Controllers
{
    [Area("Secretary")]
    [Authorize(Roles = "Secretary")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        // ~/Secretary/Home
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = userManager.GetUserId(HttpContext.User);
                var secretary_info = context.Secretaries.Include(s => s.ApplicationUser).SingleOrDefault(u => u.UserId == userId);

                return RedirectToAction("Profile", "Home", new { area = "Secretary" });
            }

            return View();
        }

        // ~/Secretary/Home/Profile
        public IActionResult Profile()
        {
            var userId = userManager.GetUserId(HttpContext.User);
            var secretary_info = context.Secretaries.Include(s => s.ApplicationUser).SingleOrDefault(u => u.UserId == userId);

            return View(secretary_info);
        }

        // ~/Secretary/Home/About
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
