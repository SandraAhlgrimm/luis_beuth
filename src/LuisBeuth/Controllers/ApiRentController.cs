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
            return _context.Rent.ToList();
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
            newRent.StartDate = DateTime.Now;
            _context.Rent.Add(newRent);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
