using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Course_Management.Data;
using Student_Course_Management.Models;
using System.Linq;

namespace Student_Course_Management.Controllers
{
    public class CoursesController : Controller
    {
        private readonly AppDbContext _context;

        public CoursesController(AppDbContext context)
        {
            _context = context;
        }

        // Add Course - POST (from modal)
        [HttpPost]
        public IActionResult AddCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                // Execute stored procedure
                _context.Database.ExecuteSqlRaw(
                    "EXEC sp_InsertCourse @p0, @p1",
                    course.CourseName,
                    course.CourseCode
                );

                return Json(new { success = true, message = "Course added successfully" });
            }
            return Json(new { success = false, message = "Validation failed" });
        }

        // Get all active courses (for autocomplete)
        public IActionResult GetActiveCourses()
        {
            var courses = _context.Courses
                .FromSqlRaw("EXEC sp_GetActiveCourses")
                .ToList()
                .Select(c => new
                {
                    c.CourseId,
                    c.CourseName,
                    c.CourseCode
                });

            return Json(courses);
        }
    }
}