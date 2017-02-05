using System.Collections.Generic;
using System.Linq;
using luis_beuth.Data;
using luis_beuth.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace luis_beuth.Controllers
{

    [Route("api/login")]
    public class ApiLoginController : Controller
    {
        private ApplicationDbContext _context;

        public ApiLoginController (ApplicationDbContext context)
        {
            this._context = context;
        }

        //
        // GET: /api/login?{Matriculationnumber}&{PasswordHash}/
        [HttpGet("{matriculationNumber}")]
        public IActionResult Get(int matriculationNumber, string passwordHash)
        {
            if (_context.Logins.FirstOrDefault(l => l.PasswordHash == passwordHash) != null)
                return Ok("approved");
            else
            {
                return NotFound();
            }
        }
    }
}
