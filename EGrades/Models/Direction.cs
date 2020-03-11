using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGrades.Models
{
    public class Direction
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}
