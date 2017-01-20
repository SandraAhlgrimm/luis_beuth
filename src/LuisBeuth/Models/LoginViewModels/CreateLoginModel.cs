using System;
namespace luis_beuth.Models.LoginViewModels
{
    public class CreateLoginModel
    {
        public int Id { get; set; }
        public string NewPassword { get; set;}
        public DateTime Created { get; set; }
    }
}