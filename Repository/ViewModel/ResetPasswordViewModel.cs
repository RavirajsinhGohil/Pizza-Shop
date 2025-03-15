using System.ComponentModel.DataAnnotations;

namespace Repository.ViewModel;

public class ResetPasswordViewModel
{

    public string Email { get; set; }

    [Required(ErrorMessage = "Please Enter New Password")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character (@$!%*?&)")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "Please Enter Confirm Password")]
    [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}
