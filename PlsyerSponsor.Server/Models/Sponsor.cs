namespace PlayerSponsor.Server.Models;

public class Sponsor
{
    public int Id { get; set; }
    public string ContactName { get; set; }
    public string ProfessionalName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Logo { get; set; }
    public List<Sponsorship> Sponsorships { get; set; } = new();
}
