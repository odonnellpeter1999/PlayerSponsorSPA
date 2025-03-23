namespace PlayerSponsor.Server.Models;

public class Sponsorship
{
    public int Id { get; set; }
    public string Status { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public decimal Price { get; set; }
    public Player? Player { get; set; }
    public Team? Team { get; set; }
    public string SponsorshipType { get; set; } // "Personal" or "Professional"
}
