using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace student_management_dotnet.ViewModel
{
    public class EditClassViewModel
    {
        public int ClassId { get; set; }
        
        [Required]
        [Display(Name = "Course")]
        public int CourseId { get; set; }
        
        [Required]
        [StringLength(100)]
        [Display(Name = "Class Name")]
        public string ClassName { get; set; }
        
        [Required]
        [StringLength(100)]
        [Display(Name = "Teacher Name")]
        public string TeacherName { get; set; }
        
        [Required]
        [Display(Name = "Maximum Students")]
        [Range(1, 100, ErrorMessage = "Maximum students must be between 1 and 100")]
        public int MaxStudents { get; set; }
        
        [Display(Name = "Current Students")]
        public int CurrentStudents { get; set; }
        
        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        
        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        
        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; } // "Open", "Closed", "Cancelled"
        
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }
        
        [Display(Name = "Updated At")]
        public DateTime UpdatedAt { get; set; }
        
        // For dropdown options
        [BindNever]
        [ValidateNever]
        public SelectList CourseSelectList { get; set; }

        [BindNever]
        [ValidateNever]
        public List<string> StatusOptions { get; set; } = new List<string> { "Open", "Closed", "Cancelled" };
    }
}