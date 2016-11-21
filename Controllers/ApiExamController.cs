using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using luis_beuth.Models;
using luis_beuth.Models.Data;
using luis_beuth.Services;
using luis_beuth.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace luis_beuth.Controllers
{
    [Route("api/exam")]
    public class ApiExamController : Controller
    {
        private ApplicationDbContext _context;

        public ApiExamController (ApplicationDbContext context)
        {
            this._context = context;
        }

        // 
        // GET: /Exam/
        [HttpGet]
        public IEnumerable<Exam> Get()
        {
            return _context.Exam.Include(a => a.Course).Include(t => t.Teacher).ToList();
        }

        // 
        // GET: /Exam/{Id}/ 
        [HttpGet("{id}")]
        public Exam GetById(int id)
        {
            return _context.Exam.FirstOrDefault(p => p.Id == id);
        }
    }
}