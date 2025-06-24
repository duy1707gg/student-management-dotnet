using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using student_management_dotnet.Models;

namespace student_management_dotnet.ViewModel
{
    // View model for creating a new report by students
    public class CreateReportViewModel
    {
        [Required(ErrorMessage = "Please select a report type")]
        [Display(Name = "Report Type")]
        public string ReportType { get; set; }

        [Required(ErrorMessage = "Please select a class")]
        [Display(Name = "Class")]
        public int? ClassId { get; set; }

        [Display(Name = "Course")]
        public int? CourseId { get; set; }

        [Required(ErrorMessage = "Please enter report description")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Attach File")]
        public IFormFile ReportFile { get; set; }

        // Will be set automatically from logged-in user
        public int CreatedBy { get; set; }

        // Added ReportDate field
        [Display(Name = "Report Date")]
        [DataType(DataType.Date)]
        public DateTime ReportDate { get; set; }

        // For select lists in the view
        public SelectList ClassSelectList { get; set; }
        public SelectList CourseSelectList { get; set; }
        public List<string> ReportTypeOptions { get; set; } =
            new List<string>
            {
                "Assignment",
                "Project",
                "Research Paper",
                "Presentation",
                "Lab Report",
                "Other",
            };
    }

    // View model for displaying reports with related data
    public class ReportDetailsViewModel
    {
        public int ReportId { get; set; }
        public string ReportType { get; set; }
        public DateTime ReportDate { get; set; }
        public string ClassName { get; set; }
        public string CourseName { get; set; }
        public string StudentName { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public string Status { get; set; } // "Submitted", "Graded", "Returned"

        // Grade information if available
        public bool IsGraded { get; set; }
        public decimal? Score { get; set; }
        public string Comment { get; set; }
        public DateTime? GradedDate { get; set; }
        public string GradedBy { get; set; }
    }

    // View model for listing reports
    public class ReportListViewModel
    {
        public List<ReportDetailsViewModel> Reports { get; set; }
        public Dictionary<string, int> ReportTypeStats { get; set; }
        public Dictionary<string, decimal> ClassAverageScores { get; set; }
        public Dictionary<string, decimal> CourseAverageScores { get; set; }

        // Filtering options
        public string SelectedReportType { get; set; }
        public int? SelectedClassId { get; set; }
        public int? SelectedCourseId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // For select lists in the view
        public SelectList ClassSelectList { get; set; }
        public SelectList CourseSelectList { get; set; }
        public SelectList ReportTypeSelectList { get; set; }
    }

    // View model for teachers/admins to grade reports
    public class GradeReportViewModel
    {
        public int ReportId { get; set; }
        public string ReportType { get; set; }
        public DateTime ReportDate { get; set; }
        public string ClassName { get; set; }
        public string CourseName { get; set; }
        public string StudentName { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; } // Added FileType property

        [Required(ErrorMessage = "Please enter a score")]
        [Range(0, 100, ErrorMessage = "Score must be between 0 and 100")]
        [Display(Name = "Score")]
        public decimal Score { get; set; }

        [Display(Name = "Comments")]
        public string Comment { get; set; }

        // Create Grade Record option
        [Display(Name = "Create Grade Record")]
        public bool CreateGradeRecord { get; set; } = true; // Default to true

        // Will be populated with existing grade if available
        public int? GradeId { get; set; }
    }

    // View model for class statistics
    public class ClassReportStatisticsViewModel
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string CourseName { get; set; }
        public string TeacherName { get; set; }

        public int TotalStudents { get; set; }
        public int TotalReports { get; set; }
        public int ReportsGraded { get; set; }
        public int ReportsPending { get; set; }

        public decimal ClassAverageScore { get; set; }
        public decimal HighestScore { get; set; }
        public decimal LowestScore { get; set; }

        public Dictionary<string, decimal> ReportTypeAverages { get; set; }
        public Dictionary<string, int> StudentSubmissionCounts { get; set; }

        // Distribution of grades (A, B, C, D, F)
        public Dictionary<string, int> GradeDistribution { get; set; }
    }
}
