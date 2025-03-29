namespace PlayerSponsor.Server.Controllers.Requests;

public class CreateClubRequest
{
    public string Name { get; set; }
    public string Logo { get; set; }
    public string Bio { get; set; }
    public string PaymentDetails { get; set; }
}

