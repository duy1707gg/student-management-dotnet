using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using student_management_dotnet.Models;

namespace student_management_dotnet.ViewModel
{
    // View model for creating/editing a grade
    public class ManageGradeViewModel
    {
        // Primary key, null for new grades
        public int? GradeId { get; set; }

        [Required(ErrorMessage = "Please select a student enrollment")]
        [Display(Name = "Student Enrollment")]
        public int EnrollmentId { get; set; }

        [Required(ErrorMessage = "Please select an exam type")]
        [Display(Name = "Exam Type")]
        public string ExamType { get; set; }

        [Required(ErrorMessage = "Score is required")]
        [Range(0, 100, ErrorMessage = "Score must be between 0 and 100")]
        [Display(Name = "Score")]
        public decimal Score { get; set; }

        [Display(Name = "Comments")]
        public string Comment { get; set; }

        // For dropdown lists
        public SelectList EnrollmentSelectList { get; set; }
        public List<string> ExamTypeOptions { get; set; }

        // For display in edit mode
        public string StudentName { get; set; }
        public string ClassName { get; set; }
    }

    // View model for displaying a student's grades
    public class StudentGradesViewModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public List<ClassGradesSummary> ClassGrades { get; set; }

        // Overall statistics
        public decimal OverallGPA { get; set; }
        public int TotalCredits { get; set; }
        public Dictionary<string, decimal> ExamTypeAverages { get; set; }
    }

    // Helper class for StudentGradesViewModel
    public class ClassGradesSummary
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string CourseName { get; set; }
        public string Status { get; set; } // "In Progress", "Completed", "Withdrawn"

        public List<GradeDetails> Grades { get; set; }

        public decimal ClassAverage { get; set; }
        public decimal FinalGrade { get; set; }
        public string LetterGrade { get; set; }
    }

    // Helper class for ClassGradesSummary
    public class GradeDetails
    {
        public int GradeId { get; set; }
        public string ExamType { get; set; }
        public decimal Score { get; set; }
        public string Comment { get; set; }
        public DateTime GradedDate { get; set; }
    }

    // View model for grade statistics by class
    public class ClassGradeStatisticsViewModel
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string CourseName { get; set; }
        public string TeacherName { get; set; }

        public int EnrolledStudents { get; set; }
        public decimal ClassAverage { get; set; }
        public decimal MedianGrade { get; set; }
        public decimal HighestGrade { get; set; }
        public decimal LowestGrade { get; set; }

        // Breakdown by exam type
        public Dictionary<string, decimal> ExamTypeAverages { get; set; }

        // Grade distribution (A, B, C, D, F)
        public Dictionary<string, int> GradeDistribution { get; set; }

        // Top performers
        public List<StudentGradeSummary> TopPerformers { get; set; }

        // Students at risk (low grades)
        public List<StudentGradeSummary> StudentsAtRisk { get; set; }
    }

    // Helper class for ClassGradeStatisticsViewModel
    public class StudentGradeSummary
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public decimal AverageGrade { get; set; }
        public string LetterGrade { get; set; }
        public int CompletedAssignments { get; set; }
        public int MissingAssignments { get; set; }
    }
}
