using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EGrades.Models
{
    public class ExamPeriod
    {
        public int Id { get; set; }

        [Display(Name = "Έτος")]
        public int Year { get; set; }

        [Display(Name = "Εξεταστική Περίοδος")]
        public string Period { get; set; }

        [Display(Name = "Ημερομηνία Έναρξης")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Ημερομηνία Λήξης")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Βαθμολογίες")]
        public ICollection<Grade> Grades { get; set; }

        [Display(Name = "Ακαδημαϊκό έτος")]
        public string AcademicYear
        {
            get
            {
                return "Ακαδημαϊκό έτος " + Year + "-" + Year++;
            }
        }
    }
}
