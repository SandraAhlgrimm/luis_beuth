using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using luis_beuth.Models;
using luis_beuth.Models.Data;
using luis_beuth.Services;
using luis_beuth.Data;

namespace luis_beuth.Controllers
{
    public class StudentController : Controller
    {
        private ApplicationDbContext _context;

        public StudentController (ApplicationDbContext context)
        {
            this._context = context;
        }

        // 
        // GET: /Student/
        public string Index()
        {
            return "This is my Student action...";
        }

        // 
        // GET: /Student/Welcome/ 

        public string Welcome()
        {
            return "This is the Welcome Student action method...";
        }

        // 
        // POST: /Student
    }
}