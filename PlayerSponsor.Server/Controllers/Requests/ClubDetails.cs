using System.ComponentModel.DataAnnotations;

namespace PlayerSponsor.Server.Controllers.Requests;

public class ClubDetails
{
    [Required]
    [MinLength(3, ErrorMessage = "Club Name must be longer than 3 characters.")]
    public string Name { get; set; }
    [MinLength(50, ErrorMessage = "Club description must be longer than 50 characters.")]
    [Required]
    public string Description { get; set; }
    [Required]
    [EmailAddress(ErrorMessage = "InteracEmail has to be a valid email")]
    public string Email { get; set; }
    [Required]
    public string Slug { get; set; }
    [Required]
    public string PrimaryColour { get; set; }
    [Required]
    public string SecondaryColour { get; set; }
    public string HeroImageId { get; set; }
}
