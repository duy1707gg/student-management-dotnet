using Microsoft.AspNetCore.Mvc;
using student_management_dotnet.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using student_management_dotnet.Data;
using Microsoft.EntityFrameworkCore;

namespace student_management_dotnet.Controllers
{
    public class ClassController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Class
        public IActionResult Index()
        {
            var classes = _context.Classes.Include(c => c.Course).ToList();
            return View(classes);
        }

        // GET: Class/Create
        public IActionResult Create()
        {
            ViewBag.CourseId = new SelectList(_context.Courses, "CourseId", "CourseName");
            return View();
        }

        // POST: Class/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Class classModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classModel);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CourseId = new SelectList(_context.Courses, "CourseId", "CourseName", classModel.CourseId);
            return View(classModel);
        }

        // GET: Class/Edit/5
        public IActionResult Edit(int id)
        {
            var classModel = _context.Classes.Find(id);
            if (classModel == null)
            {
                return NotFound();
            }
            ViewBag.CourseId = new SelectList(_context.Courses, "CourseId", "CourseName", classModel.CourseId);
            return View(classModel);
        }

        // POST: Class/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Class classModel)
        {
            if (id != classModel.ClassId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classModel);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassExists(classModel.ClassId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CourseId = new SelectList(_context.Courses, "CourseId", "CourseName", classModel.CourseId);
            return View(classModel);
        }

        // GET: Class/Delete/5
        public IActionResult Delete(int id)
        {
            var classModel = _context.Classes.Include(c => c.Course).FirstOrDefault(c => c.ClassId == id);
            if (classModel == null)
            {
                return NotFound();
            }
            return View(classModel);
        }

        // POST: Class/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var classModel = _context.Classes.Find(id);
            _context.Classes.Remove(classModel);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassExists(int id)
        {
            return _context.Classes.Any(e => e.ClassId == id);
        }
    }
}