using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Repository.ViewModel;

public class AddUserViewModel
{
    public int UserId { get; set; }

    [Required(ErrorMessage = "Firstname is required")]
    [MaxLength(20, ErrorMessage = "Firstname must be a maximum of 20 characters")]
    public string? Firstname { get; set; }

    [Required(ErrorMessage = "Lastname is required")]
    [MaxLength(20, ErrorMessage = "Lastname must be a maximum of 20 characters")]
    public string? Lastname { get; set; }

    [MaxLength(20, ErrorMessage = "Username must be a maximum of 20 characters")]
    [Required(ErrorMessage = "Username is required")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Email is required")]
    // [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string? Email { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is required")]
    [RegularExpression("([a-z]|[A-Z]|[0-9]|[\\W]){4}[a-zA-Z0-9\\W]{3,11}", ErrorMessage = "Password must contain Special Symbol, Number,Alphabet")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Please enter the phone number")]
    [MaxLength(10, ErrorMessage = "Phone number must be a maximum of 10 characters")]
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