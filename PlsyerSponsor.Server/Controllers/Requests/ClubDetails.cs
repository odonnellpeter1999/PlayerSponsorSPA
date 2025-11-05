using System.ComponentModel.DataAnnotations;

namespace PlayerSponsor.Server.Controllers.Requests;

public class ClubDetails
{
    [Required]
    [MinLength(3, ErrorMessage = "Club Name must be longer than 3 charactors.")]
    public string Name { get; set; }
    [MinLength(3, ErrorMessage = "Club description must be longer than 50 charactors.")]
    [Required]
    public string Description { get; set; }
    [Required]
    [EmailAddress(ErrorMessage = "InteracEmail has to be a valid email")]
    public string InteracEmail { get; set; }
}
