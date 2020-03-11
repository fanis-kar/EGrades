using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGrades.Models.ViewModels
{
    public class AcademicPeriodEnrollmentsViewModel
    {
        public string Title { get; set; }

        public AcademicPeriod AcademicPeriod { get; set; }

        public IEnumerable<Student> Students { get; set; }

        public IEnumerable<Enrollment> Enrollments { get; set; }
    }
}
