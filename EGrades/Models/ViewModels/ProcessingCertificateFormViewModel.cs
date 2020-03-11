using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EGrades.Models.ViewModels
{
    public class ProcessingCertificateFormViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Πιστοποιητικό")]
        public string Certificate { get; set; }

        [Display(Name = "Φοιτητής")]
        public string Student { get; set; }

        [Display(Name = "Κατάσταση")]
        public string Status { get; set; }

        [Display(Name = "Ημερομηνία που ζητήθηκε")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime RequestedDate { get; set; }

        public List<SelectListItem> StatusList { get; set; }

        public ProcessingCertificateFormViewModel()
        {

            List<SelectListItem> StatusListItems = new List<SelectListItem>
            {
                new SelectListItem() { Text = "Εκκρεμεί", Value = "Εκκρεμεί" },
                new SelectListItem() { Text = "Έτοιμο", Value = "Έτοιμο" }
            };
            StatusList = StatusListItems;
        }
    }
}
