using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGrades.Models.ViewModels
{
    public class CourseDetailsViewModel
    {
        public string Title { get; set; }

        public Course Course { get; set; }

        public Direction Direction { get; set; }

        public IEnumerable<CourseInstructor> Instructors { get; set; }
    }
}
