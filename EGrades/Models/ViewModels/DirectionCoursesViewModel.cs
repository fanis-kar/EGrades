using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGrades.Models.ViewModels
{
    public class DirectionCoursesViewModel
    {
        public string Title { get; set; }

        public Direction Direction { get; set; }

        public IEnumerable<Course> Courses { get; set; }
    }
}
