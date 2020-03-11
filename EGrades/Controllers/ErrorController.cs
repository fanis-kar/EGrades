using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EGrades.Controllers
{
    public class ErrorController : Controller
    {
        // If there is 404 status code, the route path will become Error/404
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.Title = "Η Σελίδα δεν βρέθηκε!";
                    ViewBag.ErrorMessage = "Η σελίδα που αναζητάτε δεν υπάρχει πια ή έχει μεταφερθεί σε κάποιο άλλο μέρος της ιστοσελίδας.";
                    break;
            }

            return View("NotFound");
        }
    }
}