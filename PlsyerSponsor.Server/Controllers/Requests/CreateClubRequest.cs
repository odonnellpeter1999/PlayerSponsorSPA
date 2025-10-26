namespace PlayerSponsor.Server.Controllers.Requests;

public class CreateClubRequest
{
    public required AccountDetails AdminAccountDetails { get; set; }
    public required ClubDetails ClubDetails { get; set; }
}

