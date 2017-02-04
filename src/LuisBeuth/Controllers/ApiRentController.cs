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

        // GET: /api/rent/
        [HttpGet]
        public IEnumerable<Rent> Get()
        {
            return _context.Rent
                .Where(val => val.ReturnedAt == null)
                .Include(s => s.Student)
                .Include(e => e.Exam)
                .ToList();
        }

        // GET: /api/rent/{StudentId}/
        [HttpGet("{id}")]
        public IEnumerable<Rent> GetById(int id)
        {
            return _context.Rent
                .Where(val => val.ReturnedAt == null)
                .Include(s => s.Student).Include(e => e.Exam)
                .ToList()
                .FindAll(p => p.Student.MatriculationNumber == id)
                .ToList();
        }

        // POST: /api/rent/
        [HttpPost]
        public IActionResult Post([FromBody]Rent newRent)
        {
            if ((newRent?.Student?.MatriculationNumber <= 0) || newRent?.ExamId <= 0)
            {
                return BadRequest();
            }

            if(newRent.ReturnedAt != null)
                return BadRequest();

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
            var rent = _context.Rent.FirstOrDefault(r => r.ExamId == newRent.ExamId);

            if (rent == null)
                return StatusCode(218); //NotFound();

            rent.ReturnedAt = DateTime.Now;
            _context.Rent.Update(rent);
            _context.SaveChanges();

            return Ok();
        }
    }
}
