using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGrades.Models.ViewModels
{
    public class EnrollmentDetailsViewModel
    {
        public string Title { get; set; }

        public Enrollment Enrollment { get; set; }

        public Student Student { get; set; }

        public AcademicPeriod AcademicPeriod { get; set; }

        public IEnumerable<Grade> Grades { get; set; }
    }
}
