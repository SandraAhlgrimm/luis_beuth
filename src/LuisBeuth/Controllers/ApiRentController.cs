using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using luis_beuth.Data;
using luis_beuth.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace luis_beuth.Controllers
{
    [Route("api/rent")]
    public class ApiRentController : Controller
    {
        private ApplicationDbContext _context;

        public ApiRentController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // 
        // GET: /api/rent/
        [HttpGet]
        public IEnumerable<Rent> Get()
        {
            return _context.Rent.Include(s => s.Student).Include(e => e.Exam).ToList();
        }

        // 
        // GET: /api/rent/{Id}/ 
        [HttpGet("{id}")]
        public Rent GetById(int id)
        {
            return _context.Rent.Include(s => s.Student).Include(e => e.Exam).FirstOrDefault(p => p.Id == id);
        }

        // POST: /api/rent/
        [HttpPost]
        public IActionResult Post([FromBody]Rent newRent)
        {
            if (!(newRent.Student.MatriculationNumber >= 0) || !(newRent.ExamId >= 0))
            {
                return BadRequest();
            }
            var today = DateTime.Now;
            newRent.StartDate = today;
            newRent.EndDate = today.AddDays(14);
            newRent.StudentId = _context.Student.FirstOrDefault(i => i.MatriculationNumber == newRent.Student.MatriculationNumber).Id;
            _context.Rent.Add(newRent);
            _context.SaveChanges();

            return NoContent();
        }

        // PUT api/rent

        [HttpPut]
        public IActionResult Put([FromBody]Rent newRent)
        {
            if (!(newRent.ExamId >= 0))
            {
                return BadRequest();
            }
            if (_context.Rent.Count(r => r.ExamId == newRent.ExamId) == 0)
            {
                return StatusCode(409);
            }
            if (_context.Rent.Count(r => r.StudentId == 0) != 0)
            {
                return StatusCode(418);
            }

            newRent.Id = _context.Rent.FirstOrDefault(r => r.ExamId == newRent.ExamId).Id;
            newRent.StudentId = 0;
            newRent.Student = null;
            _context.Rent.Update(newRent);

            _context.SaveChanges();

            return NoContent();
        }
    }
}
