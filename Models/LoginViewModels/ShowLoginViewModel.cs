using System;
namespace luis_beuth.Models.LoginViewModels
{
    public class ShowLoginViewModel
    {
        public int Id { get; set; }
        public string NewPassword { get; set;}
        public string PasswordHash { get; set; }
        public DateTime Created { get; set; }
    }
}