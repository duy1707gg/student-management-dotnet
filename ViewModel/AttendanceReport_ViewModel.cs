using System;
using System.Collections.Generic;

namespace student_management_dotnet.ViewModel
{
    // View model for class attendance report
    public class ClassAttendanceReportViewModel
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string CourseName { get; set; }
        public int CourseId { get; set; }
        public int TotalStudents { get; set; }
        public List<AttendanceReportDate> AttendanceDates { get; set; } = new List<AttendanceReportDate>();
        public List<StudentAttendanceReportViewModel> StudentReports { get; set; } = new List<StudentAttendanceReportViewModel>();
    }

    // Information about a specific attendance date
    public class AttendanceReportDate
    {
        public DateTime Date { get; set; }
        public int PresentCount { get; set; }
        public int AbsentCount { get; set; }
        public int LateCount { get; set; }
        public double AttendanceRate => TotalCount > 0 ? (double)(PresentCount + LateCount) / TotalCount * 100 : 0;
        public int TotalCount => PresentCount + AbsentCount + LateCount;
    }

    // Attendance summary for a specific student
    public class StudentAttendanceReportViewModel
    {
        public int UserId { get; set; }
        public int EnrollmentId { get; set; }
        public string StudentName { get; set; }
        public int PresentCount { get; set; }
        public int AbsentCount { get; set; }
        public int LateCount { get; set; }
        public int TotalClasses { get; set; }
        public double AttendanceRate => TotalClasses > 0 ? (double)(PresentCount + LateCount) / TotalClasses * 100 : 0;
        public List<StudentDateAttendance> DateAttendances { get; set; } = new List<StudentDateAttendance>();
    }

    // Individual date attendance record for a student
    public class StudentDateAttendance
    {
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
    }
}