namespace Student_Course_Management.Models
{
    public class Mapping
    {
        public int MappingId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }

        public  Student? Student { get; set; }
        public Course? Course { get; set; }
        
    }
}
