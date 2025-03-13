using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace student_management_dotnet.Models
{
    public class Class
    {
        [Key]
        public int ClassId { get; set; }
        
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string ClassName { get; set; }
        
        [Required]
        [StringLength(100)]
        public string TeacherName { get; set; }
        
        [Required]
        public int MaxStudents { get; set; }
        
        public int CurrentStudents { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime EndDate { get; set; }
        
        [Required]
        public string Status { get; set; } // "Open", "Closed", "Cancelled"
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual Course Course { get; set; }
        public virtual ICollection<ClassSchedule> ClassSchedules { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}