using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Repository.ViewModel;

public class EditUserViewModel
{
     public int UserId { get; set; }

    [Required(ErrorMessage = "First Name is required")]
    public string? Firstname { get; set; }

    [Required(ErrorMessage = "Last Name is required")]
    public string? Lastname { get; set; }

    [Required(ErrorMessage = "Username is required")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Email is required")]
    // [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Please enter the phone number")]
    // [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")] 
    public string? Phone { get; set; }

    public int RoleId { get; set; }

    public string? Rolename { get; set; }

    public string? Status { get; set; }

    public string? Country { get; set; }

    public string? State { get; set; }

    public string? City { get; set; }

    [Required(ErrorMessage = "Zip is Required")]
    public int? Zipcode { get; set; }

    [Required(ErrorMessage = "Address is required")]
    public string? Address { get; set; }

    public string? Createdby { get; set; }

    public IFormFile? ProfileImage { get; set; }

    public string? ProfileImagePath { get; set; }
}
