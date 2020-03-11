using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EGrades.Models.ViewModels
{
    public class RequestCertificateFormViewModel
    {
        public string Title { get; set; }

        public Student Student { get; set; }

        [Display(Name = "Πιστοποιητικό")]
        public string Certificate { get; set; }

        public List<SelectListItem> CertificateList { get; set; }

        public RequestCertificateFormViewModel()
        {

            List<SelectListItem> CertificateListItems = new List<SelectListItem>
            {
                new SelectListItem() { Text = "Βεβαίωση Σπουδών", Value = "Βεβαίωση Σπουδών" },
                new SelectListItem() { Text = "Αναλυτική Βαθμολογία", Value = "Αναλυτική Βαθμολογία" }
            };
            CertificateList = CertificateListItems;
        }
    }
}
