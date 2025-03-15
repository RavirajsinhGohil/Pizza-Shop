using System.ComponentModel.DataAnnotations;

namespace Repository.ViewModel;

public class LoginViewModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    public string password{ get;set;}

    public bool RememberMe { get; set; }

}
