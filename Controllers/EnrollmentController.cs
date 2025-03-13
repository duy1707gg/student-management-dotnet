using Microsoft.AspNetCore.Mvc;
using student_management_dotnet.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using student_management_dotnet.Data;
using Microsoft.EntityFrameworkCore;

namespace student_management_dotnet.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Enrollment
        public IActionResult Index()
        {
            var enrollments = _context.Enrollments.Include(e => e.User).Include(e => e.Class).ToList();
            return View(enrollments);
        }

        // GET: Enrollment/Create
        public IActionResult Create()
        {
            ViewBag.UserId = new SelectList(_context.Users, "UserId", "FullName");
            ViewBag.ClassId = new SelectList(_context.Classes, "ClassId", "ClassName");
            return View();
        }

        // POST: Enrollment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.UserId = new SelectList(_context.Users, "UserId", "FullName", enrollment.UserId);
            ViewBag.ClassId = new SelectList(_context.Classes, "ClassId", "ClassName", enrollment.ClassId);
            return View(enrollment);
        }

        // GET: Enrollment/Edit/5
        public IActionResult Edit(int id)
        {
            var enrollment = _context.Enrollments.Find(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewBag.UserId = new SelectList(_context.Users, "UserId", "FullName", enrollment.UserId);
            ViewBag.ClassId = new SelectList(_context.Classes, "ClassId", "ClassName", enrollment.ClassId);
            return View(enrollment);
        }

        // POST: Enrollment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.EnrollmentId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.UserId = new SelectList(_context.Users, "UserId", "FullName", enrollment.UserId);
            ViewBag.ClassId = new SelectList(_context.Classes, "ClassId", "ClassName", enrollment.ClassId);
            return View(enrollment);
        }

        // GET: Enrollment/Delete/5
        public IActionResult Delete(int id)
        {
            var enrollment = _context.Enrollments.Include(e => e.User).Include(e => e.Class).FirstOrDefault(e => e.EnrollmentId == id);
            if (enrollment == null)
            {
                return NotFound();
            }
            return View(enrollment);
        }

        // POST: Enrollment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var enrollment = _context.Enrollments.Find(id);
            _context.Enrollments.Remove(enrollment);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollments.Any(e => e.EnrollmentId == id);
        }
    }
}