using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EGrades.Models.ViewModels
{
    public class GradeChartViewModel
    {
        public string Title { get; set; }

        [Display(Name = "Μάθημα")]
        public string Course { get; set; }

        [Display(Name = "Εξεταστική Περίοδος")]
        public string ExamPeriod { get; set; }

        public List<SimpleReportViewModel> Reports { get; set; }
    }
}
