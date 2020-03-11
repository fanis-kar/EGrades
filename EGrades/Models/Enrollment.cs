using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EGrades.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        [Display(Name = "Φοιτητής")]
        public Student Student { get; set; }

        public int AcademicPeriodId { get; set; }

        [Display(Name = "Ακαδημαϊκή Περίοδος")]
        public AcademicPeriod AcademicPeriod { get; set; }

        [Display(Name = "Ημερομηνία καταχώρησης")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DateAdded { get; set; }

        [Display(Name = "Βαθμολογίες")]
        public ICollection<Grade> Grades { get; set; }
    }
}
