using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Course_Management.Data;
using Student_Course_Management.Models;
using System.Linq;

namespace Student_Course_Management.Controllers
{
    public class StudentsController : Controller
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // Page 1: Student Management - Grid
        public IActionResult Index()
        {
            var students = _context.Students
                .FromSqlRaw("EXEC sp_GetActiveStudent") // SP to return all active students
                .ToList();
            return View(students);
        }

       
        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Database.ExecuteSqlRaw(
                    "EXEC sp_InsertStudent @p0, @p1, @p2",
                    student.StudentName,
                    student.PhoneNumber,
                    student.EmailId
                );

                return Json(new { success = true, message = "Student added successfully" });
            }
            return Json(new { success = false, message = "Validation failed" });
        }

       
        [HttpGet]
        public IActionResult GetStudentForEdit(int id)
        {
            var student = _context.Students.FromSqlRaw("EXEC sp_GetStudentById @p0", id).AsEnumerable().Select(s => new
            {
               studentId=s.StudentId,
               studentName=s.StudentName,
               phoneNumber=s.PhoneNumber,
               emailId=s.EmailId
            })
                .FirstOrDefault();
            if (student == null)
                return NotFound();
            return Json(student);
        }

        // Edit Student - POST
        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Database.ExecuteSqlRaw(
                    "EXEC sp_UpdateStudent @p0, @p1, @p2, @p3",
                    student.StudentId,
                    student.StudentName,
                    student.PhoneNumber,
                    student.EmailId
                );

                return Json(new { success = true, message = "Student updated successfully" });
            }
            return Json(new { success = false, message = "Validation failed" });
        }

        // Soft Delete Student
        [HttpPost]
        public IActionResult Delete(int id)
        {
            // Optional: Check for mappings via SP or EF
            bool hasMapping = _context.Mappings
                .Any(m => m.StudentId == id && m.IsActive);

            if (hasMapping)
                return Json(new { success = false, message = "Cannot delete: Student mapped to courses" });

            _context.Database.ExecuteSqlRaw(
                "EXEC sp_DeleteStudent @p0", id
            );

            return Json(new { success = true, message = "Student deleted successfully" });
        }

        // Search Students - AJAX
        [HttpGet]
        public IActionResult Search(string keyword)
        {
            keyword ??= "";
            var students = _context.Students
                .FromSqlRaw("EXEC sp_SearchStudents @p0", keyword)
                .AsNoTracking()
                .ToList();

            return Json(students.Select(s => new
            {
                studentId = s.StudentId,
                studentName = s.StudentName,
                phoneNumber = s.PhoneNumber,
                emailId = s.EmailId
            }));
        }

        // Get Student Details - AJAX
        public IActionResult GetDetails(int id)
        {
            var student = _context.Students
                .FromSqlRaw("EXEC sp_GetStudentById @p0", id)
                .AsEnumerable()
                .Select(s => new
                {
                    s.StudentName,
                    s.EmailId,
                    s.PhoneNumber,
                    s.CreatedDate,
                    s.IsActive
                })
                .FirstOrDefault();

            if (student == null) return NotFound();
            return Json(student);
        }
    }
}