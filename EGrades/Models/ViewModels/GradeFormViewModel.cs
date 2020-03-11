using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EGrades.Models.ViewModels
{
    public class GradeFormViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Μάθημα")]
        public string Course { get; set; }

        [Display(Name = "Φοιτητής")]
        public string Student { get; set; }

        [Display(Name = "Ακαδημαϊκή Περίοδος")]
        public string AcademicPeriod { get; set; }

        [Display(Name = "Εξεταστική Περίοδος")]
        public int? ExamPeriod { get; set; }

        public List<SelectListItem> ExamPeriodList { get; set; }

        [Display(Name = "Ημερομηνία Εξέτασης")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? ExamDate { get; set; }

        [Display(Name = "Βαθμολογία")]
        [Range(0.0, 10.0, ErrorMessage = "Η βαθμολογία πρέπει να είναι >= 0 και <= 10")]
        public double? Grade { get; set; }
    }
}
