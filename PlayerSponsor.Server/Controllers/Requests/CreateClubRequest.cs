using System.ComponentModel.DataAnnotations;

namespace PlayerSponsor.Server.Controllers.Requests;

public class CreateClubRequest
{
    [Required]
    public required AccountDetails AdminAccountDetails { get; set; }
    [Required]
    public required ClubDetails ClubDetails { get; set; }
}

