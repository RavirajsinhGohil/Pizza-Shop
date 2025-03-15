using System.ComponentModel.DataAnnotations;

namespace Repository.ViewModel;

public class ForgotPasswordViewModel
{   

    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
}
