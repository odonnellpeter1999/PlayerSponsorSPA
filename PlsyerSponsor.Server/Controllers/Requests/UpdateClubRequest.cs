namespace PlayerSponsor.Server.Controllers.Requests;

public class UpdateClubRequest
{
    public string Name { get; set; }
    public string Logo { get; set; }
    public string Bio { get; set; }
    public string PaymentDetails { get; set; }
}