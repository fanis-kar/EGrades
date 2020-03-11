using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EGrades.Models.ViewModels
{
    public class EnrollmentFormViewModel
    {
        public int? Id { get; set; }

        public Student Student { get; set; }

        public AcademicPeriod AcademicPeriod { get; set; }

        public List<CheckCourseViewModel> Courses { get; set; }

        public EnrollmentFormViewModel()
        {
            Id = 0;
            Courses = new List<CheckCourseViewModel>();
        }

        public string Title
        {
            get
            {
                return Id != 0 ? "Ενημέρωση Δήλωσης" : "Νέα Δήλωση";
            }
        }
    }
}
