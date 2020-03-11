using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EGrades.Models.ViewModels
{
    public class DegreeViewModel
    {
        public Student Student { get; set; }

        [Display(Name = "Περασμένα Μαθήματα Κατεύθυνσης: ")]
        public int PassedDirectionCourses { get; set; }

        [Display(Name = "Περασμένα Μαθήματα Άλλων Κατευθύνσεων: ")]
        public int PassedNonDirectionCourses { get; set; }

        [Display(Name = "Παίρνω δίπλωμα;")]
        public string Degree { get; set; }
    }
}
