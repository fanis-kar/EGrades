using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EGrades.Models
{
    public class StudentCertificate
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }

        public int CertificateId { get; set; }

        public Certificate Certificate { get; set; }

        [Display(Name = "Κατάσταση")]
        public string Status { get; set; }

        [Display(Name = "Ημερομηνία που ζητήθηκε")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime RequestedDate { get; set; }
    }
}
