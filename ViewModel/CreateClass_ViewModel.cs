using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace student_management_dotnet.ViewModel
{
    public class CreateClassViewModel
    {
        [Required(ErrorMessage = "Please select a course")]
        [Display(Name = "Course")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Class name is required")]
        [StringLength(100, ErrorMessage = "Class name cannot be longer than 100 characters")]
        [Display(Name = "Class Name")]
        public string ClassName { get; set; }

        [Required(ErrorMessage = "Teacher name is required")]
        [StringLength(100, ErrorMessage = "Teacher name cannot be longer than 100 characters")]
        [Display(Name = "Teacher Name")]
        public string TeacherName { get; set; }

        [Required(ErrorMessage = "Maximum number of students is required")]
        [Range(1, 100, ErrorMessage = "Please enter a value between 1 and 100")]
        [Display(Name = "Maximum Students")]
        public int MaxStudents { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Please select a status")]
        [Display(Name = "Status")]
        public string Status { get; set; }

        // Properties for dropdown lists
        [BindNever]
        [ValidateNever]
        public SelectList CourseSelectList { get; set; }

        [BindNever]
        [ValidateNever]
        public List<string> StatusOptions { get; set; }
    }
}