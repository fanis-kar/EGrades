using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGrades.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Student Student { get; set; }
        public Secretary Secretary { get; set; }
    }
}
