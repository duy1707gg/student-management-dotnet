using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace student_management_dotnet.Models
{
    public class Grade
    {
        [Key]
        public int GradeId { get; set; }
        
        [ForeignKey("Enrollment")]
        public int EnrollmentId { get; set; }
        
        [Required]
        public string ExamType { get; set; } // "Midterm", "Final", "Assignment"
        
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Score { get; set; }
        
        public string Comment { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual Enrollment Enrollment { get; set; }
    }
}
