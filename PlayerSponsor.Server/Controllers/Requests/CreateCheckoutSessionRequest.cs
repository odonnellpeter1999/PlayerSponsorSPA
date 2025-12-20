namespace PlayerSponsor.Server.Controllers.Requests;

public class CreateCheckoutSessionRequest
{
    public string ClubSlug { get; set; }
    public int[] ProductIds { get; set; }
}
