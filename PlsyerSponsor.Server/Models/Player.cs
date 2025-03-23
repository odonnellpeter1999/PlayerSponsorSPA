namespace PlayerSponsor.Server.Models;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
    public Sponsor? AwaySponsor { get; set; }
    public Sponsor? HomeSponsor { get; set; }
    public bool AdminConfirmed { get; set; }
    public string Source { get; set; }  // Who entered the players details Admin or Player
}
