using System.ComponentModel.DataAnnotations;

namespace student_management_dotnet.ViewModel
{
    public class CreateUserViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
