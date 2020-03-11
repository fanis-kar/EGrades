using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGrades.Models.ViewModels
{
    public class CheckCourseViewModel
    {
        public Course Course { get; set; }
        public bool Checked { get; set; }
    }
}
