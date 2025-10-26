using Microsoft.AspNetCore.Identity;

namespace PlayerSponsor.Server.Models;

public class ApplicationUser : IdentityUser
{
    public ICollection<ClubAdmin> ClubAdmins { get; set; }
}
