using System.ComponentModel.DataAnnotations;

namespace student_management_dotnet.ViewModel {
    public class EditUserViewModel
{
    public int UserId { get; set; }
    
    [Required]
    public string Username { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string FullName { get; set; }
    
    [Required]
    public string Role { get; set; }
    
    // Optional password change
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }
    
    // You might want to add confirmation if implementing password change
    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}
}