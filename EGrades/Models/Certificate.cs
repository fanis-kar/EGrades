using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EGrades.Models
{
    public class Certificate
    {
        public int Id { get; set; }

        [Display(Name = "Πιστοποιητικό")]
        public string Title { get; set; }

        [Display(Name = "Περιγραφή")]
        public string Description { get; set; }

        public ICollection<StudentCertificate> StudentCertificates { get; set; }
    }
}
