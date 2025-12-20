using PlayerSponsor.Server.Models;

namespace PlayerSponsor.Server.Services.Commands;

public class CreateClubCommand
{
    public string Slug { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string PrimaryColour { get; set; }
    public string SecondaryColour { get; set; }
    public string HeroImageId { get; set; }
    public List<Social> Socials { get; set; } = new();
}