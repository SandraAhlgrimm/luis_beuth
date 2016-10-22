using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using luis_beuth.Models;
using luis_beuth.Models.ExamViewModels;
using luis_beuth.Models.Data;
using luis_beuth.Services;
using luis_beuth.Data;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace luis_beuth.Controllers
{
    public class ExamController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ExamController> _logger;

        public ExamController (ApplicationDbContext context, ILogger<ExamController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Exams
        public async Task<IActionResult> Index()
        {
            var exams = await this._context.Exam.ToListAsync();

            return View(exams);
        }

        public IActionResult Create()
        {
            var teachers = _context.Teacher.ToList().Concat(new []
            {
                new Teacher
                {
                    Id = -1, Name = "Neuer Dozent",
                }
            });

            var courses = _context.Courses.ToList().Concat(new []
            {
                new Course
                {
                    Id = -1, Name = "Neuer Kurs",
                }
            });

            var model = new CreateExamViewModel
            {
                Teachers = new SelectList(teachers, "Id", "Name"),
                Courses = new SelectList(courses, "Id", "Name"),
                Periods = new []
                {
                    new PeriodViewModel{Period = Period.First, Name= "Erster Prüfungszeitraum"},
                    new PeriodViewModel{Period = Period.Second, Name= "Zweiter Prüfungszeitraum"}
                }
            };

            // _logger.LogDebug(ViewData["teachers"].ToString());

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind] CreateExamViewModel examViewModel)
        {
            // _logger.LogDebug(exam.ToString());

            try
            {
                if (ModelState.IsValid)
                {
                    var exam = new Exam
                    {
                        Semester = examViewModel.Semester,
                        Grade = examViewModel.Grade,
                    };

                    if(examViewModel.TeacherId != -1)
                    {
                        exam.TeacherId = examViewModel.TeacherId;
                    }
                    else
                    {
                        exam.Teacher = new Teacher { Name = examViewModel.NewTeacherName };
                    }
                    
                    if(examViewModel.CourseId != -1)
                    {
                        exam.CourseId = examViewModel.CourseId;
                    }
                    else
                    {
                        exam.Course = new Course { Name = examViewModel.NewCourseName };
                    }
                    

                    _context.Add(exam);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException ex )
            {
                _logger.LogError(new EventId(42), ex, ex.Message);
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            var teachers = _context.Teacher.ToList().Concat(new []
            {
                new Teacher
                {
                    Id = -1, Name = "Neuer Dozent",
                }
            });
            var courses = _context.Courses.ToList().Concat(new []
            {
                new Course
                {
                    Id = -1, Name = "Neuer Kurs",
                }
            });

            examViewModel.Teachers = new SelectList(teachers, "Id", "Name");
            examViewModel.Periods = new []
                {
                    new PeriodViewModel{Period = Period.First, Name= "Erster Prüfungszeitraum"},
                    new PeriodViewModel{Period = Period.Second, Name= "Zweiter Prüfungszeitraum"}
                };

            return View(examViewModel);
        }

        // GET: exam/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await this._context.Exam.SingleAsync(p => p.Id == id);
            if (exam == null)
            {
                return NotFound();
            }
            return View(exam);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind] CreateExamViewModel exam)
        {
            if (id != exam.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    this._context.Update(exam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.ExamExists(exam.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(exam);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var exam = await _context.Exam.SingleOrDefaultAsync(p => p.Id == id);
            if (exam == null)
            {
                return NotFound();
            }
            return View(exam);
        }
        
        // POST: Exam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exam = await _context.Exam.SingleOrDefaultAsync(p => p.Id == id);
            this._context.Exam.Remove(exam);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ExamExists(int id)
        {
            return this._context.Exam.Any(p => p.Id == id);
        }
    }
}