using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using luis_beuth.Models;
using luis_beuth.Models.Data;
using luis_beuth.Services;

namespace luis_beuth.Controllers
{
    public class ExamController : Controller
    {
        // 
        // GET: /Exam/

        public string Index()
        {
            return "This is my exam action...";
        }

        // 
        // GET: /Exam/Welcome/ 

        public string Welcome()
        {
            return "This is the Welcome exam action method...";
        }

        // 
        // POST: /Exam/
    }
}