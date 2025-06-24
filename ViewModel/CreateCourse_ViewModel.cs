using System.ComponentModel.DataAnnotations;

namespace student_management_dotnet.ViewModel
{
    public class CreateCourseViewModel
    {
        [Required]
        public string CourseName { get; set; }

        [Required]
        public string Description { get; set; }

    }
}
