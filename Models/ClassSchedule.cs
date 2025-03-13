using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace student_management_dotnet.Models
{
    public class ClassSchedule
    {
        [Key]
        public int ScheduleId { get; set; }
        
        [ForeignKey("Class")]
        public int ClassId { get; set; }
        
        [Required]
        public string DayOfWeek { get; set; }
        
        [Required]
        public TimeSpan StartTime { get; set; }
        
        [Required]
        public TimeSpan EndTime { get; set; }
        
        [Required]
        public string Room { get; set; }
        
        // Navigation properties
        public virtual Class Class { get; set; }
    }
}