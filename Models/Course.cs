using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace student_management_dotnet.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string CourseName { get; set; }
        
        public string Description { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual ICollection<Class> Classes { get; set; }
    }
}