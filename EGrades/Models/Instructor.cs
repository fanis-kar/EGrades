using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EGrades.Models
{
    public class Instructor
    {
        public int Id { get; set; }

        [Display(Name = "Αριθμός Μητρώου")]
        public string RegistrationNumber { get; set; }

        [Display(Name = "Όνομα")]
        public string FirstName { get; set; }

        [Display(Name = "Επώνυμο")]
        public string LastName { get; set; }

        [Display(Name = "Φύλο")]
        public string Gender { get; set; }

        [Display(Name = "Πατρώνυμο")]
        public string FatherName { get; set; }

        [Display(Name = "Μητρώνυμο")]
        public string MotherName { get; set; }

        [Display(Name = "Διεύθυνση Κατοικίας")]
        public string Address { get; set; }

        [Display(Name = "Ημερομηνία Γέννησης")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Ημερομηνία Πρόσληψης")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime HireDate { get; set; }

        public ICollection<CourseInstructor> CourseInstructors { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
