using Microsoft.AspNetCore.Mvc;
using student_management_dotnet.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using student_management_dotnet.Data;
using Microsoft.EntityFrameworkCore;

namespace student_management_dotnet.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Report
        public IActionResult Index()
        {
            var reports = _context.Reports.Include(r => r.Class).Include(r => r.Course).Include(r => r.User).ToList();
            return View(reports);
        }

        // GET: Report/Create
        public IActionResult Create()
        {
            ViewBag.ClassId = new SelectList(_context.Classes, "ClassId", "ClassName");
            ViewBag.CourseId = new SelectList(_context.Courses, "CourseId", "CourseName");
            ViewBag.CreatedBy = new SelectList(_context.Users, "UserId", "FullName");
            return View();
        }

        // POST: Report/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Report report)
        {
            if (ModelState.IsValid)
            {
                _context.Add(report);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ClassId = new SelectList(_context.Classes, "ClassId", "ClassName", report.ClassId);
            ViewBag.CourseId = new SelectList(_context.Courses, "CourseId", "CourseName", report.CourseId);
            ViewBag.CreatedBy = new SelectList(_context.Users, "UserId", "FullName", report.CreatedBy);
            return View(report);
        }

        // GET: Report/Edit/5
        public IActionResult Edit(int id)
        {
            var report = _context.Reports.Find(id);
            if (report == null)
            {
                return NotFound();
            }
            ViewBag.ClassId = new SelectList(_context.Classes, "ClassId", "ClassName", report.ClassId);
            ViewBag.CourseId = new SelectList(_context.Courses, "CourseId", "CourseName", report.CourseId);
            ViewBag.CreatedBy = new SelectList(_context.Users, "UserId", "FullName", report.CreatedBy);
            return View(report);
        }

        // POST: Report/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Report report)
        {
            if (id != report.ReportId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(report);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(report.ReportId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ClassId = new SelectList(_context.Classes, "ClassId", "ClassName", report.ClassId);
            ViewBag.CourseId = new SelectList(_context.Courses, "CourseId", "CourseName", report.CourseId);
            ViewBag.CreatedBy = new SelectList(_context.Users, "UserId", "FullName", report.CreatedBy);
            return View(report);
        }

        // GET: Report/Delete/5
        public IActionResult Delete(int id)
        {
            var report = _context.Reports.Include(r => r.Class).Include(r => r.Course).Include(r => r.User).FirstOrDefault(r => r.ReportId == id);
            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }

        // POST: Report/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var report = _context.Reports.Find(id);
            _context.Reports.Remove(report);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportExists(int id)
        {
            return _context.Reports.Any(e => e.ReportId == id);
        }
    }
} 