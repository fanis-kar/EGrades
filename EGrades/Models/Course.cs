using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EGrades.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Display(Name = "Τίτλος")]
        public string Title { get; set; }

        [Display(Name = "Τύπος")]
        public string Type { get; set; }

        [Display(Name = "Εξάμηνο")]
        public int Semester { get; set; }

        public int DirectionId { get; set; }

        [Display(Name = "Κατεύθυνση")]
        public Direction Direction { get; set; }

        [Display(Name = "Ώρες Θεωρίας")]
        public int TheoryHours { get; set; }

        [Display(Name = "Ώρες Ασκήσεων Πράξης")]
        public int ExerciseHours { get; set; }

        [Display(Name = "Ώρες Εργαστηρίου")]
        public int LabHours { get; set; }

        [Display(Name = "ECTS")]
        public int Ects { get; set; }

        [Display(Name = "Βαθμολογίες")]
        public ICollection<Grade> Grades { get; set; }

        public ICollection<CourseInstructor> CourseInstructors { get; set; }

        [Display(Name = "Εβδομαδιαίες Ώρες")]
        public int TotalHours
        {
            get
            {
                return TheoryHours + ExerciseHours + LabHours;
            }
        }
    }
}
