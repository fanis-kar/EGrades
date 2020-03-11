using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EGrades.Models
{
    public class AcademicPeriod
    {
        public int Id { get; set; }

        [Display(Name = "Έτος")]
        [Required]
        public int Year { get; set; }

        [Display(Name = "Εξάμηνο")]
        //[Required]
        public string Semester { get; set; }

        [Display(Name = "Ημερομηνία Έναρξης")]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Ημερομηνία Λήξης")]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime EndDate { get; set; }

        public bool Current { get; set; }

        public bool OpenEnrollment { get; set; }

        [Display(Name = "Δηλώσεις")]
        public ICollection<Enrollment> Enrollments { get; set; }

        [Display(Name = "Περίοδος")]
        public string Period
        {
            get
            {
                int tmpYear = Year;
                int tmpYearnext = Year + 1;

                return Semester + " - Ακαδημαϊκού έτους: " + tmpYear + "-" + tmpYearnext;
            }
        }

        [Display(Name = "Ακαδημαϊκό έτος")]
        public string AcademicYear
        {
            get
            {
                int tmpYear = Year;
                int tmpYearnext = Year + 1;

                return "Ακαδημαϊκό έτος " + tmpYear + "-" + tmpYearnext;
            }
        }

        public int SemesterNum
        {
            get
            {
                int x;

                if (Semester == "Χειμερινό Εξάμηνο")
                {
                    x = 1;
                }
                else
                {
                    x = 2;
                }

                return x;
            }
        }
    }
}
