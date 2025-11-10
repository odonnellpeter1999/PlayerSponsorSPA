namespace PlayerSponsor.Server.Models;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Player> Players { get; set; } = new();
    public List<Sponsor> Sponsors { get; set; } = new();
}
