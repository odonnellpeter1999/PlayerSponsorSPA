namespace PlayerSponsor.Server.Services.Commands;

public class CreateCheckoutSessionCommand
{
    public string ClubSlug { get; set; }
    public int[] ProductIds { get; set; }
}