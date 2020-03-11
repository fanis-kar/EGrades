using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGrades.Models.ViewModels
{
    public class DirectionStudentsViewModel
    {
        public string Title { get; set; }

        public Direction Direction { get; set; }

        public IEnumerable<Student> Students { get; set; }
    }
}
