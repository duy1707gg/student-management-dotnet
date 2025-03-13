using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using student_management_dotnet.Data;
using student_management_dotnet.Models;
using System.Linq;

namespace student_management_dotnet.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Attendance
        public IActionResult Index()
        {
            var attendances = _context.Attendances.Include(a => a.Enrollment).ToList();
            return View(attendances);
        }

        // GET: Attendance/Create
        public IActionResult Create()
        {
            ViewBag.EnrollmentId = new SelectList(_context.Enrollments, "EnrollmentId", "EnrollmentId");
            return View();
        }

        // POST: Attendance/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attendance);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.EnrollmentId = new SelectList(_context.Enrollments, "EnrollmentId", "EnrollmentId", attendance.EnrollmentId);
            return View(attendance);
        }

        // GET: Attendance/Edit/5
        public IActionResult Edit(int id)
        {
            var attendance = _context.Attendances.Find(id);
            if (attendance == null)
            {
                return NotFound();
            }
            ViewBag.EnrollmentId = new SelectList(_context.Enrollments, "EnrollmentId", "EnrollmentId", attendance.EnrollmentId);
            return View(attendance);
        }

        // POST: Attendance/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Attendance attendance)
        {
            if (id != attendance.AttendanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendance);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceExists(attendance.AttendanceId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.EnrollmentId = new SelectList(_context.Enrollments, "EnrollmentId", "EnrollmentId", attendance.EnrollmentId);
            return View(attendance);
        }

        // GET: Attendance/Delete/5
        public IActionResult Delete(int id)
        {
            var attendance = _context.Attendances.Include(a => a.Enrollment).FirstOrDefault(a => a.AttendanceId == id);
            if (attendance == null)
            {
                return NotFound();
            }
            return View(attendance);
        }

        // POST: Attendance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var attendance = _context.Attendances.Find(id);
            _context.Attendances.Remove(attendance);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceExists(int id)
        {
            return _context.Attendances.Any(e => e.AttendanceId == id);
        }
    }
}