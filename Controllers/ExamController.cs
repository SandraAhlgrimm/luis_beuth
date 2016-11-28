using System.Linq;
using System.Threading.Tasks;
using luis_beuth.Data;
using luis_beuth.Models.Data;
using luis_beuth.Models.ExamViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace luis_beuth.Controllers
{
    public class ExamController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ExamController> _logger;

        public ExamController(ApplicationDbContext context, ILogger<ExamController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Exams
        public async Task<IActionResult> Index()
        {
            var exams = await _context.Exam.Include(val => val.Teacher).Include(val => val.Course).ToListAsync();

            return View(exams);
        }

        public IActionResult Create()
        {
            var model = new CreateExamViewModel();

            SetSelectors(model);

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
                        Grade = examViewModel.Grade
                    };

                    if (examViewModel.TeacherId != -1)
                    {
                        exam.TeacherId = examViewModel.TeacherId;
                    }
                    else
                    {
                        exam.Teacher = new Teacher {Name = examViewModel.NewTeacherName};
                    }

                    if (examViewModel.CourseId != -1)
                    {
                        exam.CourseId = examViewModel.CourseId;
                    }
                    else
                    {
                        exam.Course = new Course {Name = examViewModel.NewCourseName};
                    }

                    _context.Add(exam);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(new EventId(42), ex, ex.Message);
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrator.");
            }

            SetSelectors(examViewModel);

            return View(examViewModel);
        }

        private void SetSelectors(CreateExamViewModel examViewModel)
        {
            var teachers = _context.Teacher.ToList().Concat(new[]
            {
                new Teacher
                {
                    Id = -1,
                    Name = "Neuer Dozent"
                }
            });
            var courses = _context.Courses.ToList().Concat(new[]
            {
                new Course
                {
                    Id = -1,
                    Name = "Neuer Kurs"
                }
            });

            examViewModel.Teachers = new SelectList(teachers, "Id", "Name");
            examViewModel.Courses = new SelectList(courses, "Id", "Name");
            examViewModel.Periods = new[]
            {
                new PeriodViewModel {Period = Period.First, Name = "Erster Prüfungszeitraum"},
                new PeriodViewModel {Period = Period.Second, Name = "Zweiter Prüfungszeitraum"}
            };
        }

        // GET: exam/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.Exam.SingleAsync(p => p.Id == id);
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
                    _context.Update(exam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamExists(exam.Id))
                    {
                        return NotFound();
                    }
                    throw;
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
            _context.Exam.Remove(exam);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ExamExists(int id)
        {
            return _context.Exam.Any(p => p.Id == id);
        }
    }
}
