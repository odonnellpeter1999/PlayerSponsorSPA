using PlayerSponsor.Server.Data.Context;
using PlayerSponsor.Server.Models;

namespace PlayerSponsor.Server.Repositories;

public class ClubAdminRepository: Repository<ClubAdmin>, IClubAdminRepository
{
    public ClubAdminRepository(ApplicationDbContext context) : base(context) { }
}
