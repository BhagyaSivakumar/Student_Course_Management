using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Course_Management.Data;
using Student_Course_Management.Models;

namespace Student_Course_Management.Controllers
{
    public class MappingsController : Controller
    {
        private readonly AppDbContext _context;
        public MappingsController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CreateCourse(string courseName, string courseCode)
        {
            if (string.IsNullOrEmpty(courseName) || string.IsNullOrEmpty(courseCode))
                return Json(new { success = false, message = "All fields required" });

            var exists = _context.Courses.Any(x => x.CourseCode == courseCode);
            if (exists)
                return Json(new { success = false, message = "Course code already exists" });

            var course = new Course
            {
                CourseName = courseName,
                CourseCode = courseCode,
                IsActive = true
            };

            _context.Courses.Add(course);
            _context.SaveChanges();

            return Json(new { success = true, message = "Course added successfully" });
        }
        

        [HttpPost]
        public IActionResult MapCourse(int studentId, int courseId)
        {
            try
            {
                if (!_context.Students.Any(s => s.StudentId == studentId))
                    return Json(new { success = false, message = "Invalid Student ID" });

                if (!_context.Courses.Any(c => c.CourseId == courseId))
                    return Json(new { success = false, message = "Invalid Course ID" });

                bool duplicate = _context.Mappings.Any(x =>
                    x.StudentId == studentId &&
                    x.CourseId == courseId &&
                    x.IsActive);

                if (duplicate)
                    return Json(new { success = false, message = "Already mapped" });

                var mapping = new Mapping
                {
                    StudentId = studentId,
                    CourseId = courseId,
                    IsActive = true
                };

                _context.Mappings.Add(mapping);
                _context.SaveChanges();

                return Json(new { success = true, message = "Mapped successfully" });
            }
            catch (Exception)
            {
                return Json(new
                {
                    success = false,
                    message = "Invalid data. Please select student and course correctly."
                });
            }
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            // Find the mapping by ID
            var mapping = _context.Mappings.FirstOrDefault(m => m.MappingId == id);

            if (mapping == null)
            {
                return Json(new { success = false, message = "Mapping not found." });
            }

            // Soft delete
            mapping.IsActive = false;

            try
            {
                _context.SaveChanges();
                return Json(new { success = true, message = "Mapping deleted successfully." });
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return Json(new { success = false, message = "Error deleting mapping: " + ex.Message });
            }
        }





        //----------Get all active students for autocomplete ----------
        [HttpGet]
        public IActionResult GetStudents(string term)
        {
          var data = _context.Students
                .Where(s => s.IsActive && (string.IsNullOrEmpty(term) || s.StudentName.Contains(term)))
                .Select(s => new { id = s.StudentId.ToString(), label = s.StudentName, value = s.StudentName })
                .ToList();

          return Json(data);
        }

        // ---------- Get all active courses for autocomplete ----------
        [HttpGet]
        public IActionResult GetCourses(string term)
        {
            var data = _context.Courses
                        .Where(c => c.IsActive && (string.IsNullOrEmpty(term) || c.CourseName.Contains(term)))
                        .Select(c => new { id = c.CourseId.ToString(), label = c.CourseName, value = c.CourseName })
                        .ToList();

            return Json(data);
        }


        [HttpGet]

        // ---------- Get all active mappings for grid ----------
        public IActionResult GetMappings()
        {
            var data = _context.Mappings
                        .Where(m => m.IsActive)  // soft delete check
                        .Select(m => new {
                            m.MappingId,
                            studentName = m.Student.StudentName,
                            courseName = m.Course.CourseName,
                            courseCode = m.Course.CourseCode
                        })
                        .ToList();

            return Json(data);
        }
    }
}




