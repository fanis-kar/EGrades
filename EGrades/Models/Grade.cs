using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EGrades.Models
{
    public class Grade
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        [Display(Name = "Μάθημα")]
        public Course Course { get; set; }

        [Display(Name = "Βαθμολογία")]
        public double? Value { get; set; }

        public int EnrollmentId { get; set; }

        [Display(Name = "Δήλωση")]
        public Enrollment Enrollment { get; set; }

        public int? ExamPeriodId { get; set; }

        [Display(Name = "Εξεταστική Περίοδος")]
        public ExamPeriod ExamPeriod { get; set; }

        [Display(Name = "Ημερομηνία Εξέτασης")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? ExamDate { get; set; }

        public int? SecretaryId { get; set; }

        [Display(Name = "Ο Βαθμός καταχωρήθηκε από")]
        public Secretary Secretary { get; set; }

        [Display(Name = "Η Βαθμολογία καταχωρήθηκε στις")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? EntryDate { get; set; }

        [Display(Name = "Η βαθμολογία εκκρεμεί")]
        public bool IsPending
        {
            get
            {
                if (Value == null)
                    return true;
                else
                    return false;
            }
        }
    }
}
