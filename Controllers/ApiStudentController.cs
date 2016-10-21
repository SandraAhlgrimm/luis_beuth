using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using luis_beuth.Models;
using luis_beuth.Models.Data;
using luis_beuth.Services;
using luis_beuth.Data;
using System.Linq;
using System.Collections.Generic;

namespace luis_beuth.Controllers
{
    [Route("api/student")]
    public class ApiStudentController : Controller
    {
        private ApplicationDbContext _context;

        public ApiStudentController (ApplicationDbContext context)
        {
            this._context = context;
        }

        // 
        // GET: /Student/
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return _context.Student.ToList();
        }

        // 
        // GET: /Student/{Id}/ 
        [HttpGet("{id}")]
        public Student GetById(int id)
        {
            return _context.Student.FirstOrDefault(p => p.Id == id);
        }

        // 
        // POST: /Student
    }
}