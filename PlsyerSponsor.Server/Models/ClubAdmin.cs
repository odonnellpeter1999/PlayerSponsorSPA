namespace PlayerSponsor.Server.Models;

public class ClubAdmin
{
    public int Id { get; set; }
    public Club Club { get; set; }
    public int ClubId { get; set; }
    public ApplicationUser User { get; set; }
    public string UserId { get; set; }
}