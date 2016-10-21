using System.Collections.Generic;

namespace luis_beuth.Models.Data
{
    public class Student 
    {
        public int Id { get; set; }
        public int MatriculationNumber { get; set;}
        public bool Approved { get; set; }
        public List<Rent> Rents { get; }
        public string Name { get; set; }
    }
}