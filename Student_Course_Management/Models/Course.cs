namespace Student_Course_Management.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string? CourseName{ get; set; }

        public string? CourseCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class SpResult
    {
        public int Success { get; set; }
        public string Message { get; set; }
    }
}
