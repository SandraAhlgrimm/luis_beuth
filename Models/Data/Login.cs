using System;

namespace luis_beuth.Models.Data
{
    public class Login
    {
        public int Id { get; set; }
        public string PasswordHash { get; set;}
        public DateTime Created { get; set; }
    }
}