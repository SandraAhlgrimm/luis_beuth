using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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

        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(await this._context.Student.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,MatriculationNumber,Approved")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this._context.Add(student);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(student);
        }

        // GET: student/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await this._context.Student.SingleAsync(p => p.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,MatriculationNumber,Approved")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    this._context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.StudentExists(student.Id))
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
            return View(student);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var student = await _context.Student.SingleOrDefaultAsync(p => p.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        
        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Student.SingleOrDefaultAsync(p => p.Id == id);
            this._context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool StudentExists(int id)
        {
            return this._context.Student.Any(p => p.Id == id);
        }
    }
}