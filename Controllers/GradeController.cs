using Microsoft.AspNetCore.Mvc;
using student_management_dotnet.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using student_management_dotnet.Data;
using Microsoft.EntityFrameworkCore;

namespace student_management_dotnet.Controllers
{
    public class GradeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GradeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Grade
        public IActionResult Index()
        {
            var grades = _context.Grades.Include(g => g.Enrollment).ToList();
            return View(grades);
        }

        // GET: Grade/Create
        public IActionResult Create()
        {
            ViewBag.EnrollmentId = new SelectList(_context.Enrollments, "EnrollmentId", "EnrollmentId");
            return View();
        }

        // POST: Grade/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Grade grade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grade);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.EnrollmentId = new SelectList(_context.Enrollments, "EnrollmentId", "EnrollmentId", grade.EnrollmentId);
            return View(grade);
        }

        // GET: Grade/Edit/5
        public IActionResult Edit(int id)
        {
            var grade = _context.Grades.Find(id);
            if (grade == null)
            {
                return NotFound();
            }
            ViewBag.EnrollmentId = new SelectList(_context.Enrollments, "EnrollmentId", "EnrollmentId", grade.EnrollmentId);
            return View(grade);
        }

        // POST: Grade/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Grade grade)
        {
            if (id != grade.GradeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grade);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeExists(grade.GradeId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.EnrollmentId = new SelectList(_context.Enrollments, "EnrollmentId", "EnrollmentId", grade.EnrollmentId);
            return View(grade);
        }

        // GET: Grade/Delete/5
        public IActionResult Delete(int id)
        {
            var grade = _context.Grades.Include(g => g.Enrollment).FirstOrDefault(g => g.GradeId == id);
            if (grade == null)
            {
                return NotFound();
            }
            return View(grade);
        }

        // POST: Grade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var grade = _context.Grades.Find(id);
            _context.Grades.Remove(grade);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeExists(int id)
        {
            return _context.Grades.Any(e => e.GradeId == id);
        }
    }
}