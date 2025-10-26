namespace PlayerSponsor.Server.Services.DTOs;

public class NewApplicationUser
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public int? ClubId { get; set; } // Optional, if the user is a club admin
}
