using Microsoft.AspNetCore.Mvc.Rendering;
using luis_beuth.Models.Data;
using System.ComponentModel.DataAnnotations;

namespace luis_beuth.Models.ExamViewModels
{
    public class CreateExamViewModel
    {
        public int Id { get; set; }

        public SelectList Teachers { get; set; }

        [Display(Name = "Dozent")]
        public int TeacherId { get; set; }

        [Display(Name = "Dozentenname")]
        public string NewTeacherName { get; set; }

        public PeriodViewModel[] Periods { get; set; }

        [Display(Name = "Prüfungszeitraum")]
        public Period Period { get; set; }

        [Display(Name = "Note")]

        public double Grade { get; set; }

        public SelectList Courses { get; set; }

        [Display(Name = "Modul")]
        public int CourseId { get; set; }

        [Display(Name = "Modulname")]
        public string NewCourseName { get; set; }

        public string Semester { get; set; }
    }

    public class PeriodViewModel
    {
        public Period Period { get; set; }

        public string Name { get; set; }
    }
}