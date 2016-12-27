using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace luis_beuth.Models.Data
{
    public class Student 
    {
        public int Id { get; set; }

        [Display(Name = "Matrikelnummer")]
        public int MatriculationNumber { get; set; }

        [Display(Name = "Zugelassen")]
        public bool Approved { get; set; }
        public List<Rent> Rents { get; set; }
        public string Name { get; set; }
    }
}