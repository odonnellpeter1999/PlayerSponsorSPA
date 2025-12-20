namespace PlayerSponsor.Server.Models;

public class Club
{
    public int Id { get; set; }
    public string Slug { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string PrimaryColour { get; set; }
    public string SecondaryColour { get; set; }
    public string HeroImageId { get; set; }
    public string Currency { get; set; }
    public bool IsPublished { get; set; }
    public List<ClubAdmin> Admins { get; set; } = new();
    public List<Product> Products { get; set; } = new();
    public List<Social> Socials { get; set; } = new();
}

