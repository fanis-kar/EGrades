using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EGrades.Models.ViewModels
{
    public class AcademicPeriodFormViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "Έτος")]
        [Required(ErrorMessage = "Το πεδίο 'Έτος' απαιτείται!")]
        public int? Year { get; set; }

        [Display(Name = "Εξάμηνο")]
        [Required(ErrorMessage = "Το πεδίο 'Εξάμηνο' απαιτείται!")]
        public string Semester { get; set; }

        [Display(Name = "Ημερομηνία Έναρξης")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Required(ErrorMessage = "Το πεδίο 'Ημερομηνία Έναρξης' απαιτείται!")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Ημερομηνία Λήξης")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Required(ErrorMessage = "Το πεδίο 'Ημερομηνία Λήξης' απαιτείται!")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Τρέχουσα περίοδος")]
        public bool? Current { get; set; }

        [Display(Name = "Ανοικτή για δηλώσεις")]
        public bool? OpenEnrollment { get; set; }

        [NotMapped]
        public List<SelectListItem> SemesterList { get; set; }

        [NotMapped]
        public List<SelectListItem> CurrentList { get; set; }

        [NotMapped]
        public List<SelectListItem> OpenEnrollmentList { get; set; }

        public string Title
        {
            get
            {
                return Id != 0 ? "Ενημέρωση Ακ. Περιόδου" : "Νέα Ακ. Περίοδος";
            }
        }

        public AcademicPeriodFormViewModel()
        {
            Id = 0;

            List<SelectListItem> SemesterListItems = new List<SelectListItem>
            {
                new SelectListItem() { Text = "Χειμερινό Εξάμηνο", Value = "Χειμερινό Εξάμηνο" },
                new SelectListItem() { Text = "Εαρινό Εξάμηνο", Value = "Εαρινό Εξάμηνο" }
            };
            SemesterList = SemesterListItems;

            List<SelectListItem> CurrentListItems = new List<SelectListItem>
            {
                new SelectListItem() { Text = "Όχι", Value = false.ToString() },
                new SelectListItem() { Text = "Ναι", Value = true.ToString() }
            };
            CurrentList = CurrentListItems;

            List<SelectListItem> OpenEnrollmentListItems = new List<SelectListItem>
            {
                new SelectListItem() { Text = "Όχι", Value = false.ToString() },
                new SelectListItem() { Text = "Ναι", Value = true.ToString() }
            };
            OpenEnrollmentList = OpenEnrollmentListItems;
        }

        public AcademicPeriodFormViewModel(AcademicPeriod ap)
        {
            Id = ap.Id;
            Year = ap.Year;
            Semester = ap.Semester;
            StartDate = ap.StartDate;
            EndDate = ap.EndDate;
            Current = ap.Current;
            OpenEnrollment = ap.OpenEnrollment;

            List<SelectListItem> SemesterListItems = new List<SelectListItem>
            {
                new SelectListItem() { Text = "Χειμερινό Εξάμηνο", Value = "Χειμερινό Εξάμηνο" },
                new SelectListItem() { Text = "Εαρινό Εξάμηνο", Value = "Εαρινό Εξάμηνο" }
            };
            SemesterList = SemesterListItems;

            List<SelectListItem> CurrentListItems = new List<SelectListItem>
            {
                new SelectListItem() { Text = "Όχι", Value = false.ToString() },
                new SelectListItem() { Text = "Ναι", Value = true.ToString() }
            };
            CurrentList = CurrentListItems;

            List<SelectListItem> OpenEnrollmentListItems = new List<SelectListItem>
            {
                new SelectListItem() { Text = "Όχι", Value = false.ToString() },
                new SelectListItem() { Text = "Ναι", Value = true.ToString() }
            };
            OpenEnrollmentList = OpenEnrollmentListItems;
        }
    }
}
