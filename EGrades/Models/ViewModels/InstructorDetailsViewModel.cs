using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGrades.Models.ViewModels
{
    public class InstructorDetailsViewModel
    {
        public string Title { get; set; }

        public Instructor Instructor { get; set; }

        public IEnumerable<CourseInstructor> Courses { get; set; }
    }
}
