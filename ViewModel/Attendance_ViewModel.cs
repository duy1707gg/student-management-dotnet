using Microsoft.AspNetCore.Mvc.Rendering;
using student_management_dotnet.Models;
using System;
using System.Collections.Generic;

namespace student_management_dotnet.ViewModel
{
    // Existing AttendanceViewModel with some modifications
    public class AttendanceViewModel
    {
        public int AttendanceId { get; set; }
        public int EnrollmentId { get; set; }
        public DateTime ClassDate { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        
        // Display properties
        public string StudentName { get; set; }
        public string ClassName { get; set; }
        
        // For dropdown lists
        public SelectList EnrollmentList { get; set; }
        public SelectList StatusList { get; set; }
        
        // For listing
        public List<Attendance> Attendances { get; set; }
    }
    
    // New ViewModel for course selection
    public class CourseListViewModel
    {
        public List<Course> Courses { get; set; }
    }
    
    // New ViewModel for class selection
    public class ClassListViewModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public List<Class> Classes { get; set; }
    }
    
    // New ViewModel for student attendance in a class
    public class ClassAttendanceViewModel
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string CourseName { get; set; }
        public DateTime AttendanceDate { get; set; }
        public List<StudentAttendanceViewModel> Students { get; set; }
    }
    
    // Individual student attendance record
    public class StudentAttendanceViewModel
    {
        public int EnrollmentId { get; set; }
        public int UserId { get; set; }
        public string StudentName { get; set; }
        public int? AttendanceId { get; set; }  // Nullable because attendance might not exist yet
        public string Status { get; set; }
        public string Note { get; set; }
    }
    
    // ViewModel for batch attendance submission
    public class BatchAttendanceViewModel
    {
        public int ClassId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public List<StudentAttendanceRecord> AttendanceRecords { get; set; }
    }
    
    // Individual record in batch submission
    public class StudentAttendanceRecord
    {
        public int EnrollmentId { get; set; }
        public int? AttendanceId { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
    }
}