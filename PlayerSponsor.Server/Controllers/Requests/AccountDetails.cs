using System.ComponentModel.DataAnnotations;

namespace PlayerSponsor.Server.Controllers.Requests;

public class AccountDetails
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string ConfirmPassword { get; set; }
    [Phone]
    public string PhoneNumber { get; set; }
    public string? UserName { get; set; }
}
