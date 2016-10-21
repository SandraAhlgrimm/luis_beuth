
using System.Collections.Generic;

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
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int CourseId { get; set; } 
        public Course Course { get; set; }
        public Period Period { get; set; }
        public double Grade { get; set; }
        public List<Rent> Rents { get; }
    }
}