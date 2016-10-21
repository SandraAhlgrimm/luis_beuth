namespace luis_beuth.Models.Data
{
    public class Exam 
    {
        public int Id { get; set; }
        public string Semester { get; set;}
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int CourseId { get; set; } 
        public Course Course { get; set; }
    }
}