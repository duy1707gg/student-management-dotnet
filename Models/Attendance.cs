using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace student_management_dotnet.Models
{
    public class Attendance
    {
        [Key]
        public int AttendanceId { get; set; }
        
        [ForeignKey("Enrollment")]
        public int EnrollmentId { get; set; }
        
        [Required]
        public DateTime ClassDate { get; set; }
        
        [Required]
        public string Status { get; set; } // "Present", "Absent", "Late"
        
        public string Note { get; set; }
        
        // Navigation properties
        public virtual Enrollment Enrollment { get; set; }
    }
}   