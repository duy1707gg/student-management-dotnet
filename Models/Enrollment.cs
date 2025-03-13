using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace student_management_dotnet.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentId { get; set; }
        
        [ForeignKey("User")]
        public int UserId { get; set; }
        
        [ForeignKey("Class")]
        public int ClassId { get; set; }
        
        [Required]
        public DateTime EnrollmentDate { get; set; }
        
        [Required]
        public string Status { get; set; } // "Active", "Completed", "Cancelled"
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual User User { get; set; }
        public virtual Class Class { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}