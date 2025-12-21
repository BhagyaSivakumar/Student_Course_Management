using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Course_Management.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        [Required(ErrorMessage ="Enter Name")]
        public string? StudentName { get; set; }
        [Required(ErrorMessage ="Enter Phone number")]
        [RegularExpression(@"^\d{10}$",ErrorMessage ="Phone number must be 10 digits")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Enter Email id")]
        [EmailAddress(ErrorMessage = "Invalid Email format")]
        public string? EmailId { get; set; }
        public DateTime? CreatedDate { get; set; } 
        public bool IsActive { get; set; }
    }
}
