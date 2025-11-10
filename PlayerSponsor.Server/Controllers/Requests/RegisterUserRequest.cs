using System.ComponentModel.DataAnnotations;

namespace PlayerSponsor.Server.Controllers.Requests;

public class RegisterUserRequest
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }
    [StringLength(100, ErrorMessage = "Password must be at least {2} characters long.", MinimumLength = 6)]
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    [Required(ErrorMessage = "Confirm password is required")]
    public string ConfirmPassword { get; set; }
    [Required(ErrorMessage = "User name is required")]
    public string UserName { get; set; }
    public int? ClubId { get; set; } // Optional, if the user is a club admin
}
