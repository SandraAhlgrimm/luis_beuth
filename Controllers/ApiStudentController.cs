using Microsoft.AspNetCore.Mvc;
using luis_beuth.Models.Data;
using luis_beuth.Data;
using luis_beuth.Models.ApiStudentModels;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace luis_beuth.Controllers
{
    [Route("api/student")]
    public class ApiStudentController : Controller
    {
        private ApplicationDbContext _context;
        private readonly ILogger<ExamController> _logger;


        public ApiStudentController (ApplicationDbContext context, ILogger<ExamController> logger)
        {
            _context = context;
            _logger = logger;
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
        // POST: /api/student
        [Route("")]
        public async Task<string> Post(CreateStudentApiModel content) {
            var Name = content.Name;
            var Matrikculationnumber = content.MatriculationNumber;
            _logger.LogDebug(Name + Matrikculationnumber);

            return Name;
        }
    }
}