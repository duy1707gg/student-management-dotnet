using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace student_management_dotnet.Models
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }
        
        [Required]
        public string ReportType { get; set; }
        
        [Required]
        public DateTime ReportDate { get; set; }
        
        [ForeignKey("Class")]
        public int? ClassId { get; set; }
        
        [ForeignKey("Course")]
        public int? CourseId { get; set; }
        
        [Required]
        public string Data { get; set; } // Can store JSON or file path
        
        [ForeignKey("User")]
        public int CreatedBy { get; set; }
        
        // Navigation properties
        public virtual Class Class { get; set; }
        public virtual Course Course { get; set; }
        public virtual User User { get; set; }
    }
}