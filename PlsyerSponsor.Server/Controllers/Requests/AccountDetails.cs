namespace PlayerSponsor.Server.Controllers.Requests;

public class AccountDetails
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string PhoneNumber { get; set; }
    public string? UserName { get; set; }
}
