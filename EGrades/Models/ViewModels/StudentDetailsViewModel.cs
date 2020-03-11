using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGrades.Models.ViewModels
{
    public class StudentDetailsViewModel
    {
        public string Title { get; set; }

        public Student Student { get; set; }

        public Direction Direction { get; set; }

        public IEnumerable<Enrollment> Enrollments { get; set; }
    }
}
