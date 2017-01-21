
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace luis_beuth.Models.Data
{
    public enum Period
    {
        First,
        Second
    }
    public class Exam 
    {
        public int Id { get; set; }
        public string Semester { get; set;}
        [Display(Name = "Dozent")]
        public int TeacherId { get; set; }

        [Display(Name = "Dozent")]
        public Teacher Teacher { get; set; }
        [Display(Name = "Modul")]
        public int CourseId { get; set; }
        [Display(Name = "Modul")]
        public Course Course { get; set; }
        [Display(Name = "Prüfungszeitraum")]
        public Period Period { get; set; }
        [Display(Name = "Note")]
        public double Grade { get; set; }
        public List<Rent> Rents { get; set; }
    }
}