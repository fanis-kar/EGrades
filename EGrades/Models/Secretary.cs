using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EGrades.Models
{
    public class Secretary
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

        [Display(Name = "Βαθμολογίες")]
        public ICollection<Grade> Grades { get; set; }

        public string UserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

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
