namespace PlayerSponsor.Server.Models;

public class Club
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Logo { get; set; }
    public string Bio { get; set; }
    public List<Sponsor> Sponsors { get; set; } = new();
    public string PaymentDetails { get; set; }
    public List<ClubAdmin> Admins { get; set; } = new();
    public List<Team> Teams { get; set; } = new();
    public string PlayerKey { get; set; } // Used to authenticate player detail links/forms
}

